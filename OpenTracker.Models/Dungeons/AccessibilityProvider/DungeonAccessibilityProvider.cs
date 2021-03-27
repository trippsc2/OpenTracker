using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.DungeonItems;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Items;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Sections;
using OpenTracker.Utils;
using ReactiveUI;

namespace OpenTracker.Models.Dungeons.AccessibilityProvider
{
    /// <summary>
    /// This class contains the logic for updating the dungeon accessibility.
    /// </summary>
    public class DungeonAccessibilityProvider : ReactiveObject
    {
        private readonly ConstrainedTaskScheduler _taskScheduler;
        private readonly IMode _mode;

        private readonly IDungeonState.Factory _stateFactory;
        
        private readonly IDungeon _dungeon;
        private readonly IMutableDungeonQueue _mutableDungeonQueue;

        private bool _visible;
        public bool Visible
        {
            get => _visible;
            private set => this.RaiseAndSetIfChanged(ref _visible, value);
        }

        private bool _sequenceBreak;
        public bool SequenceBreak
        {
            get => _sequenceBreak;
            private set => this.RaiseAndSetIfChanged(ref _sequenceBreak, value);
        }

        private int _accessible;
        public int Accessible
        {
            get => _accessible;
            private set => this.RaiseAndSetIfChanged(ref _accessible, value);
        }

        public List<IBossAccessibilityProvider> BossAccessibilityProviders { get; } = new();

        private List<KeyDoorID> SmallKeyDoors => _dungeon.SmallKeyDoors;
        private ICappedItem? BigKey => _dungeon.BigKeyItem;
        private IKeyItem SmallKey => _dungeon.SmallKeyItem;
        private List<DungeonItemID> Bosses => _dungeon.Bosses;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="taskScheduler">
        /// The task scheduler for all accessibility tasks.
        /// </param>
        /// <param name="mode">
        /// The mode settings data.
        /// </param>
        /// <param name="stateFactory">
        /// An Autofac factory for creating dungeon states.
        /// </param>
        /// <param name="mutableDungeonQueue">
        /// An Autofac factory for creating the mutable dungeon queue.
        /// </param>
        /// <param name="dungeon">
        /// The dungeon data.
        /// </param>
        public DungeonAccessibilityProvider(
            ConstrainedTaskScheduler taskScheduler, IMode mode, IDungeonState.Factory stateFactory, IBossAccessibilityProvider.Factory bossProviderFactory,
            IMutableDungeonQueue.Factory mutableDungeonQueue, IDungeon dungeon)
        {
            _taskScheduler = taskScheduler;

            _dungeon = dungeon;
            _mode = mode;
            _stateFactory = stateFactory;
            _mutableDungeonQueue = mutableDungeonQueue(_dungeon);

            foreach (var _ in Bosses)
            {
                BossAccessibilityProviders.Add(bossProviderFactory());
            }
        }

        /// <summary>
        /// Updates all values in the accessibility provider.
        /// </summary>
        private void UpdateValues()
        {
            var resultInLogicQueue = new BlockingCollection<IDungeonResult>();
            var resultOutOfLogicQueue = new BlockingCollection<IDungeonResult>();

            ProcessKeyDoorPermutations(resultInLogicQueue, resultOutOfLogicQueue);
            ProcessResults(resultInLogicQueue, resultOutOfLogicQueue);
        }

        /// <summary>
        /// Processes all key door permutations and places results in the proper result queue.
        /// </summary>
        /// <param name="resultInLogicQueue">
        /// The blocking collection queue of in-logic results.
        /// </param>
        /// <param name="resultOutOfLogicQueue">
        /// The blocking collection queue of out-of-logic results.
        /// </param>
        private void ProcessKeyDoorPermutations(BlockingCollection<IDungeonResult> resultInLogicQueue, BlockingCollection<IDungeonResult> resultOutOfLogicQueue)
        {
            var keyDoorTasks = new List<Task>();
            var finalQueue = new BlockingCollection<IDungeonState>();
            var keyDoorPermutationQueue = CreateKeyDoorPermutationQueues();

            PopulateInitialDungeonStates(keyDoorPermutationQueue[0]);

            for (var i = 0; i < keyDoorPermutationQueue.Count; i++)
            {
                var currentQueue = keyDoorPermutationQueue[i];
                var nextQueue = i == keyDoorPermutationQueue.Count - 1 ? null :
                    keyDoorPermutationQueue[i + 1];

                keyDoorTasks.Add(Task.Factory.StartNew(
                    () => { ProcessPermutationQueue(currentQueue, nextQueue, finalQueue); },
                    CancellationToken.None, TaskCreationOptions.None, _taskScheduler));
            }

            Task.WaitAll(keyDoorTasks.ToArray());
            finalQueue.CompleteAdding();

            var finalKeyDoorTask = Task.Factory.StartNew(
                () => ProcessFinalKeyDoorPermutationQueue(finalQueue, resultInLogicQueue, resultOutOfLogicQueue),
                CancellationToken.None, TaskCreationOptions.None, _taskScheduler);

            finalKeyDoorTask.Wait();
            resultInLogicQueue.CompleteAdding();
            resultOutOfLogicQueue.CompleteAdding();
        }

        /// <summary>
        /// Creates the list of blocking collection queues for processing dungeon key door permutations.
        /// </summary>
        /// <returns>
        /// A list of blocking collection queues.
        /// </returns>
        private List<BlockingCollection<IDungeonState>> CreateKeyDoorPermutationQueues()
        {
            var keyDoorPermutationQueue = new List<BlockingCollection<IDungeonState>>();

            for (var i = 0; i <= SmallKeyDoors.Count; i++)
            {
                keyDoorPermutationQueue.Add(new BlockingCollection<IDungeonState>());
            }

            return keyDoorPermutationQueue;
        }

        /// <summary>
        /// Populates the initial key permutations for the initial simulations.
        /// </summary>
        /// <param name="collection">
        /// The collection to be populated.
        /// </param>
        private void PopulateInitialDungeonStates(BlockingCollection<IDungeonState> collection)
        {
            var smallKeyValues = GetSmallKeyValues();
            var bigKeyValues = GetBigKeyValues();

            foreach (var smallKeyValue in smallKeyValues)
            {
                foreach (var bigKeyValue in bigKeyValues)
                {
                    collection.Add(_stateFactory(
                        new List<KeyDoorID>(), smallKeyValue, bigKeyValue, false));
                    collection.Add(_stateFactory(
                        new List<KeyDoorID>(), smallKeyValue, bigKeyValue, true));
                }
            }

            collection.CompleteAdding();
        }

        /// <summary>
        /// Returns a list of numbers of small keys that could be collected.
        /// </summary>
        /// <returns>
        /// A list of 32-bit signed integers.
        /// </returns>
        private IEnumerable<int> GetSmallKeyValues()
        {
            var smallKeyValues = new List<int>();

            if (_mode.SmallKeyShuffle)
            { 
                smallKeyValues.Add(SmallKey.EffectiveCurrent);
                return smallKeyValues;
            }

            for (var i = 0; i <= SmallKey.Maximum; i++)
            {
                smallKeyValues.Add(i);
            }

            return smallKeyValues;
        }

        /// <summary>
        /// Returns a list of possible states for big key collection.
        /// </summary>
        /// <returns>
        /// A list of boolean values.
        /// </returns>
        private List<bool> GetBigKeyValues()
        {
            if (BigKey is null)
            {
                return new List<bool> {false};
            }
            
            var bigKeyValues = new List<bool>();

            if (_mode.BigKeyShuffle)
            {
                bigKeyValues.Add(BigKey.Current > 0);
                return bigKeyValues;
            }

            bigKeyValues.Add(false);
                
            if (BigKey.Maximum > 0)
            {
                bigKeyValues.Add(true);
            }

            return bigKeyValues;
        }

        /// <summary>
        /// Processes a specified permutation queue.
        /// </summary>
        /// <param name="currentQueue">
        /// The blocking collection queue to be processed.
        /// </param>
        /// <param name="nextQueue">
        /// A nullable blocking collection queue to place generated permutations.
        /// </param>
        /// <param name="finalQueue"></param>
        private void ProcessPermutationQueue(
            BlockingCollection<IDungeonState> currentQueue, BlockingCollection<IDungeonState>? nextQueue,
            BlockingCollection<IDungeonState> finalQueue)
        {
            var dungeonData = _mutableDungeonQueue.GetNext();

            foreach (var state in currentQueue.GetConsumingEnumerable())
            {
                ProcessDungeonState(dungeonData, state, finalQueue, nextQueue);
            }

            nextQueue?.CompleteAdding();
            currentQueue.Dispose();
            _mutableDungeonQueue.Requeue(dungeonData);
        }

        /// <summary>
        /// Process the dungeon state permutation.
        /// </summary>
        /// <param name="dungeonData">
        /// The mutable dungeon data.
        /// </param>
        /// <param name="state">
        /// The permutation to be processed.
        /// </param>
        /// <param name="finalQueue">
        /// The final queue.
        /// </param>
        /// <param name="nextQueue">
        /// The next queue to which this permutation will be added.
        /// </param>
        private void ProcessDungeonState(
            IMutableDungeon dungeonData, IDungeonState state, BlockingCollection<IDungeonState> finalQueue,
            BlockingCollection<IDungeonState>? nextQueue)
        {
            dungeonData.ApplyState(state);

            var availableKeys = GetAvailableSmallKeys(dungeonData, state);

            if (availableKeys == 0)
            {
                finalQueue.Add(state);
                return;
            }

            var accessibleKeyDoors = dungeonData.GetAccessibleKeyDoors(state.SequenceBreak);

            if (accessibleKeyDoors.Count == 0)
            {
                finalQueue.Add(state);
                return;
            }

            if (nextQueue == null)
            {
                return;
            }

            QueueNextStatePermutations(state, nextQueue, accessibleKeyDoors);
        }

        /// <summary>
        /// Returns the number of keys that are available in the current dungeon state.
        /// </summary>
        /// <param name="dungeonData">
        /// The mutable dungeon data.
        /// </param>
        /// <param name="state">
        /// The current dungeon state.
        /// </param>
        /// <returns>
        /// A 32-bit signed integer representing the number of keys available.
        /// </returns>
        private static int GetAvailableSmallKeys(IMutableDungeon dungeonData, IDungeonState state)
        {
            return dungeonData.GetAvailableSmallKeys(state.SequenceBreak) +
                state.KeysCollected - state.UnlockedDoors.Count;
        }

        /// <summary>
        /// Queues the next set of state permutations.
        /// </summary>
        /// <param name="state">
        /// The current dungeon state.
        /// </param>
        /// <param name="nextQueue">
        /// The next permutation queue.
        /// </param>
        /// <param name="accessibleKeyDoors">
        /// The enumerable of accessible locked key doors.
        /// </param>
        private void QueueNextStatePermutations(
            IDungeonState state, BlockingCollection<IDungeonState> nextQueue, IEnumerable<KeyDoorID> accessibleKeyDoors)
        {
            foreach (var keyDoor in accessibleKeyDoors)
            {
                var newPermutation = state.UnlockedDoors.GetRange(0, state.UnlockedDoors.Count);
                newPermutation.Add(keyDoor);

                nextQueue.Add(_stateFactory(
                    newPermutation, state.KeysCollected, state.BigKeyCollected, state.SequenceBreak));
            }
        }

        /// <summary>
        /// Processes the final permutation queue.
        /// </summary>
        /// <param name="finalQueue">
        /// The blocking collection queue to be processed.
        /// </param>
        /// <param name="resultInLogicQueue">
        /// The blocking collection queue of in-logic results.
        /// </param>
        /// <param name="resultOutOfLogicQueue">
        /// The blocking collection queue of out-of-logic results.
        /// </param>
        private void ProcessFinalKeyDoorPermutationQueue(
            BlockingCollection<IDungeonState> finalQueue, BlockingCollection<IDungeonResult> resultInLogicQueue,
            BlockingCollection<IDungeonResult> resultOutOfLogicQueue)
        {
            var dungeonData = _mutableDungeonQueue.GetNext();

            foreach (var item in finalQueue.GetConsumingEnumerable())
            {
                ProcessFinalDungeonState(dungeonData, item, resultInLogicQueue, resultOutOfLogicQueue);
            }

            finalQueue.Dispose();
            resultInLogicQueue.CompleteAdding();
            resultOutOfLogicQueue.CompleteAdding();
            _mutableDungeonQueue.Requeue(dungeonData);
        }

        /// <summary>
        /// Process the final dungeon state permutation.
        /// </summary>
        /// <param name="dungeonData">
        /// The mutable dungeon data.
        /// </param>
        /// <param name="state">
        /// The permutation to be processed.
        /// </param>
        /// <param name="inLogicQueue">
        /// The queue of results that were achieved without sequence breaks.
        /// </param>
        /// <param name="outOfLogicQueue">
        /// The queue of results that were achieved with sequence breaks.
        /// </param>
        private static void ProcessFinalDungeonState(
            IMutableDungeon dungeonData, IDungeonState state, BlockingCollection<IDungeonResult> inLogicQueue,
            BlockingCollection<IDungeonResult> outOfLogicQueue)
        {
            dungeonData.ApplyState(state);

            if (!dungeonData.ValidateKeyLayout(state))
            {
                return;
            }

            var result = dungeonData.GetDungeonResult(state);

            if (state.SequenceBreak)
            {
                outOfLogicQueue.Add(result);
                return;
            }

            inLogicQueue.Add(result);
        }

        private void ProcessResults(
            BlockingCollection<IDungeonResult> resultInLogicQueue,
            BlockingCollection<IDungeonResult> resultOutOfLogicQueue)
        {
            List<AccessibilityLevel> lowestBossAccessibilities = new();
            List<AccessibilityLevel> highestBossAccessibilities = new();

            foreach (var _ in BossAccessibilityProviders)
            {
                lowestBossAccessibilities.Add(AccessibilityLevel.Normal);
                highestBossAccessibilities.Add(AccessibilityLevel.None);
            }

            var lowestAccessible = int.MaxValue;
            var highestAccessible = 0;
            var sequenceBreak = false;
            var visible = false;

            foreach (var result in resultInLogicQueue.GetConsumingEnumerable())
            {
                ProcessBossAccessibilityResult(result, lowestBossAccessibilities, highestBossAccessibilities);
                ProcessItemAccessibilityResult(result, ref lowestAccessible, ref highestAccessible,
                    ref _sequenceBreak, ref visible);
            }

            foreach (var result in resultOutOfLogicQueue.GetConsumingEnumerable())
            {
                ProcessBossAccessibilityResult(
                    result, lowestBossAccessibilities, highestBossAccessibilities, true);
                ProcessItemAccessibilityResult(
                    result, ref lowestAccessible, ref highestAccessible, ref _sequenceBreak,
                    ref visible, true);
            }

            resultInLogicQueue.Dispose();
            resultOutOfLogicQueue.Dispose();

            var accessible = highestAccessible;

            if (accessible == AccessibilityLevel.Normal &&
                lowestAccessible < AccessibilityLevel.Normal)
            {
                accessible = AccessibilityLevel.SequenceBreak;
            }

            var firstSection = (Sections[0] as IDungeonItemSection) ??
                               throw new Exception("The first section is not a dungeon item section.");

            switch (accessible)
            {
                case AccessibilityLevel.None:
                case AccessibilityLevel.Inspect:
                {
                    firstSection.Accessibility = accessible;
                    firstSection.Accessible = 0;
                }
                    break;
                case AccessibilityLevel.Partial:
                {
                    firstSection.Accessibility = accessible;
                    firstSection.Accessible = highestAccessible;
                }
                    break;
                case AccessibilityLevel.SequenceBreak:
                case AccessibilityLevel.Normal:
                case AccessibilityLevel.Cleared:
                {
                    firstSection.Accessibility = accessible;
                    firstSection.Accessible = firstSection.Available;
                }
                    break;
            }

            for (int i = 0; i < Bosses.Count; i++)
            {
                if (highestBossAccessibilities[i] == AccessibilityLevel.Normal &&
                    lowestBossAccessibilities[i] != AccessibilityLevel.Normal)
                {
                    highestBossAccessibilities[i] = AccessibilityLevel.SequenceBreak;
                }

                var bossSection = (Sections[i + 1] as IBossSection) ??
                                  throw new Exception($"Section {i + 1} is not a boss section.");

                bossSection.Accessibility = highestBossAccessibilities[i];
            }
        }

        /// <summary>
        /// Processes a result's boss accessibility values.
        /// </summary>
        /// <param name="result">
        /// The result to be processed.
        /// </param>
        /// <param name="lowestBossAccessibilities">
        /// A list of the lowest accessibilities for each boss so far.
        /// </param>
        /// <param name="highestBossAccessibilities">
        /// A list of the highest accessibilities for each boss so far.
        /// </param>
        private static void ProcessBossAccessibilityResult(
            IDungeonResult result, List<AccessibilityLevel> lowestBossAccessibilities,
            IList<AccessibilityLevel> highestBossAccessibilities, bool fromSequenceBreakQueue = false)
        {
            for (var i = 0; i < result.BossAccessibility.Count; i++)
            {
                highestBossAccessibilities[i] = AccessibilityLevelMethods.Max(
                    highestBossAccessibilities[i], result.BossAccessibility[i]);

                if (fromSequenceBreakQueue)
                {
                    return;
                }

                lowestBossAccessibilities[i] = AccessibilityLevelMethods.Min(
                    lowestBossAccessibilities[i], result.BossAccessibility[i]);
            }
        }

        private static void ProcessItemAccessibilityResult(
            IDungeonResult result, ref int lowestAccessible, ref int highestAccessible, ref bool visible,
            bool sequenceBreak = false)
        {
            highestAccessible = Math.Max(highestAccessible, result.Accessible);
            visible = result.Visible;

            if (sequenceBreak)
            {
                return;
            }

            lowestAccessible = Math.Min(lowestAccessible, result.Accessible);
        }
    }
}