using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.DungeonItems;
using OpenTracker.Models.DungeonNodes;
using OpenTracker.Models.Items;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.KeyLayouts;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Modes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Sections;
using OpenTracker.Utils;

namespace OpenTracker.Models.Dungeons
{
    /// <summary>
    /// This is the class containing dungeon data.
    /// </summary>
    public class Dungeon : Location, IDungeon
    {
        private readonly IMode _mode;
        private readonly IMutableDungeon.Factory _mutableDungeonFactory;
        private readonly IDungeonState.Factory _stateFactory;

        private static readonly ConstrainedTaskScheduler TaskScheduler =
            new ConstrainedTaskScheduler(Math.Max(1, Environment.ProcessorCount - 1));

        public event EventHandler<IMutableDungeon>? DungeonDataCreated;

        public int Map { get; }
        public int Compass { get; }
        public int SmallKeys { get; }
        public int BigKey { get; }

        public ICappedItem? MapItem { get; }
        public ICappedItem? CompassItem { get; }
        public IKeyItem SmallKeyItem { get; }
        public ICappedItem? BigKeyItem { get; }

        public List<DungeonItemID> DungeonItems { get; }
        public List<DungeonItemID> Bosses { get; }
        public List<DungeonItemID> SmallKeyDrops { get; }
        public List<DungeonItemID> BigKeyDrops { get; }
        public List<KeyDoorID> SmallKeyDoors { get; }
        public List<KeyDoorID> BigKeyDoors { get; }
        public List<IKeyLayout> KeyLayouts { get; }
        public List<DungeonNodeID> Nodes { get; }
        public List<IRequirementNode> EntryNodes { get; }
        public ConcurrentQueue<IMutableDungeon> DungeonDataQueue { get; } = new ConcurrentQueue<IMutableDungeon>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode data.
        /// </param>
        /// <param name="mutableDungeonFactory">
        /// An Autofac factory for creating mutable dungeon data.
        /// </param>
        /// <param name="locationFactory">
        /// The location factory.
        /// </param>
        /// <param name="mapLocationFactory">
        /// The map location factory.
        /// </param>
        /// <param name="sectionFactory">
        /// The section factory.
        /// </param>
        /// <param name="markingFactory">
        /// The marking factory.
        /// </param>
        /// <param name="notes">
        /// A new collection of location notes.
        /// </param>
        /// <param name="dungeonFactory">
        /// The dungeon factory.
        /// </param>
        /// <param name="keyLayoutFactory">
        /// A factory for creating key layouts.
        /// </param>
        /// <param name="stateFactory">
        /// An Autofac factory for creating dungeon state data.
        /// </param>
        /// <param name="id">
        /// The ID of the location.
        /// </param>
        public Dungeon(
            IMode mode, IMutableDungeon.Factory mutableDungeonFactory, ILocationFactory locationFactory,
            IMapLocationFactory mapLocationFactory, ISectionFactory sectionFactory, IMarking.Factory markingFactory,
            ILocationNoteCollection notes, IDungeonFactory dungeonFactory, IKeyLayoutFactory keyLayoutFactory,
            IDungeonState.Factory stateFactory, LocationID id)
            : base(locationFactory, mapLocationFactory, sectionFactory, markingFactory, notes, id)
        {
            _mode = mode;
            _mutableDungeonFactory = mutableDungeonFactory;
            _stateFactory = stateFactory;

            Map = dungeonFactory.GetDungeonMapCount(id);
            Compass = dungeonFactory.GetDungeonCompassCount(id);
            SmallKeys = dungeonFactory.GetDungeonSmallKeyCount(id);
            BigKey = dungeonFactory.GetDungeonBigKeyCount(id);

            MapItem = dungeonFactory.GetDungeonMapItem(id);
            CompassItem = dungeonFactory.GetDungeonCompassItem(id);
            SmallKeyItem = dungeonFactory.GetDungeonSmallKeyItem(id);
            BigKeyItem = dungeonFactory.GetDungeonBigKeyItem(id);

            Nodes = dungeonFactory.GetDungeonNodes(id);

            DungeonItems = dungeonFactory.GetDungeonItems(id);
            Bosses = dungeonFactory.GetDungeonBosses(id);

            SmallKeyDrops = dungeonFactory.GetDungeonSmallKeyDrops(id);
            BigKeyDrops = dungeonFactory.GetDungeonBigKeyDrops(id);

            SmallKeyDoors = dungeonFactory.GetDungeonSmallKeyDoors(id);
            BigKeyDoors = dungeonFactory.GetDungeonBigKeyDoors(id);

            KeyLayouts = keyLayoutFactory.GetDungeonKeyLayouts(this);
            EntryNodes = dungeonFactory.GetDungeonEntryNodes(id);

            foreach (var section in Sections)
            {
                section.PropertyChanged += OnSectionChanged;
            }

            foreach (var node in EntryNodes)
            {
                node.ChangePropagated += OnNodeChangePropagated;
            }

            if (BigKeyItem != null)
            {
                BigKeyItem.PropertyChanged += OnItemChanged;
            }

            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                CreateDungeonData();
            }

            _mode.PropertyChanged += OnModeChanged;
            SubscribeToConnectionRequirements();
            UpdateSectionAccessibility();
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IItem interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnItemChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IItem.Current))
            {
                UpdateSectionAccessibility();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the RequirementNode class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnNodeChangePropagated(object? sender, EventArgs e)
        {
            UpdateSectionAccessibility();
        }

        /// <summary>
        /// Subscribes to the ChangePropagated event on the IRequirement interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the ChangePropagated event.
        /// </param>
        private void OnRequirementChangePropagated(object? sender, EventArgs e)
        {
            UpdateSectionAccessibility();
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the Mode class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnModeChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IMode.WorldState) ||
                e.PropertyName == nameof(IMode.MapShuffle) ||
                e.PropertyName == nameof(IMode.CompassShuffle) ||
                e.PropertyName == nameof(IMode.SmallKeyShuffle) ||
                e.PropertyName == nameof(IMode.BigKeyShuffle) ||
                e.PropertyName == nameof(IMode.GenericKeys) ||
                e.PropertyName == nameof(IMode.GuaranteedBossItems) ||
                e.PropertyName == nameof(IMode.KeyDropShuffle))
            {
                UpdateSectionAccessibility();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ISection interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnSectionChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ISection.Available))
            {
                UpdateSectionAccessibility();
            }
        }

        /// <summary>
        /// Creates a new instance of dungeon data.
        /// </summary>
        private void CreateDungeonData()
        {
            var dungeonData = _mutableDungeonFactory(this);
            FinishMutableDungeonCreation(dungeonData);
            DungeonDataQueue.Enqueue(dungeonData);
        }

        /// <summary>
        /// Subscribes to PropertyChanged event on each requirement.
        /// </summary>
        private void SubscribeToConnectionRequirements()
        {
            var requirementSubscriptions = new List<IRequirement>();
            var dungeonData = GetDungeonData();

            foreach (var node in Nodes)
            {
                foreach (var connection in dungeonData.Nodes[node].Connections)
                {
                    var requirement = connection.Requirement;

                    if (requirement is KeyDoorRequirement ||
                        requirementSubscriptions.Contains(requirement))
                    {
                        continue;
                    }
                    
                    requirement.ChangePropagated += OnRequirementChangePropagated;
                    requirementSubscriptions.Add(requirement);
                }
            }
        }

        /// <summary>
        /// Returns the next available DungeonData class in the queue.
        /// </summary>
        /// <returns>
        /// The next available DungeonData class.
        /// </returns>
        private IMutableDungeon GetDungeonData()
        {
            while (true)
            {
                if (DungeonDataQueue.TryDequeue(out var dungeonData))
                {
                    return dungeonData;
                }
            }
        }

        /// <summary>
        /// Re-registers a dungeon data instance to the queue.
        /// </summary>
        /// <param name="dungeonData">
        /// The dungeon data instance to be registered to the queue.
        /// </param>
        private void ReturnDungeonData(IMutableDungeon dungeonData)
        {
            DungeonDataQueue.Enqueue(dungeonData);
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
                smallKeyValues.Add(SmallKeyItem.EffectiveCurrent);
                return smallKeyValues;
            }

            for (var i = 0; i <= SmallKeyItem.Maximum; i++)
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
            if (BigKeyItem is null)
            {
                return new List<bool>
                {
                    false
                };
            }
            
            var bigKeyValues = new List<bool>();

            if (_mode.BigKeyShuffle)
            {
                bigKeyValues.Add(BigKeyItem.Current > 0);
                return bigKeyValues;
            }

            bigKeyValues.Add(false);
                
            if (BigKeyItem.Maximum > 0)
            {
                bigKeyValues.Add(true);
            }

            return bigKeyValues;
        }

        /// <summary>
        /// Populates the initial key permutations for the initial simulations.
        /// </summary>
        /// <param name="collection">
        /// The collection to be populated.
        /// </param>
        private void PopulateInitialDungeonStates(BlockingCollection<IDungeonState> collection)
        {
            IEnumerable<int> smallKeyValues = GetSmallKeyValues();
            List<bool> bigKeyValues = GetBigKeyValues();

            foreach (var smallKeyValue in smallKeyValues)
            {
                foreach (var bigKeyValue in bigKeyValues)
                {
                    collection.Add(
                        _stateFactory(new List<KeyDoorID>(), smallKeyValue, bigKeyValue, false));
                    collection.Add(
                        _stateFactory(new List<KeyDoorID>(), smallKeyValue, bigKeyValue, true));
                }
            }

            collection.CompleteAdding();
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

            var availableKeys = dungeonData.GetAvailableSmallKeys(state.SequenceBreak) +
                state.KeysCollected - state.UnlockedDoors.Count;

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

            foreach (var keyDoor in accessibleKeyDoors)
            {
                var newPermutation = state.UnlockedDoors.GetRange(0, state.UnlockedDoors.Count);
                newPermutation.Add(keyDoor);

                if (nextQueue == null)
                {
                    return;
                }

                nextQueue.Add(
                    _stateFactory(
                        newPermutation, state.KeysCollected, state.BigKeyCollected,
                        state.SequenceBreak));
            }
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
            }
            else
            {
                inLogicQueue.Add(result);
            }
        }

        /// <summary>
        /// Updates the accessibility and number of accessible items for the contained sections
        /// using parallel operation.
        /// </summary>
        private void UpdateSectionAccessibility()
        {
            var keyDoorPermutationQueue = new List<BlockingCollection<IDungeonState>>();
            var keyDoorTasks = new List<Task>();
            var finalQueue = new BlockingCollection<IDungeonState>();
            var resultInLogicQueue = new BlockingCollection<IDungeonResult>();
            var resultOutOfLogicQueue = new BlockingCollection<IDungeonResult>();

            for (int i = 0; i <= SmallKeyDoors.Count; i++)
            {
                keyDoorPermutationQueue.Add(new BlockingCollection<IDungeonState>());
            }

            PopulateInitialDungeonStates(keyDoorPermutationQueue[0]);

            for (int i = 0; i < keyDoorPermutationQueue.Count; i++)
            {
                var currentIteration = i;

                keyDoorTasks.Add(Task.Factory.StartNew(() =>
                {
                    var dungeonData = GetDungeonData();

                    foreach (var item in keyDoorPermutationQueue[currentIteration].GetConsumingEnumerable())
                    {
                        ProcessDungeonState(
                            dungeonData, item, finalQueue,
                            keyDoorPermutationQueue.Count > currentIteration + 1 ?
                                keyDoorPermutationQueue[currentIteration + 1] : null);
                    }

                    ReturnDungeonData(dungeonData);
                },
                CancellationToken.None, TaskCreationOptions.None, TaskScheduler));
            }

            for (var i = 0; i < keyDoorTasks.Count; i++)
            {
                keyDoorTasks[i].Wait();
                keyDoorPermutationQueue[i].Dispose();

                if (i + 1 < keyDoorPermutationQueue.Count)
                {
                    keyDoorPermutationQueue[i + 1].CompleteAdding();
                }
                else
                {
                    finalQueue.CompleteAdding();
                }
            }

            var finalKeyDoorTask = Task.Factory.StartNew(() =>
            {
                var dungeonData = GetDungeonData();

                foreach (var item in finalQueue.GetConsumingEnumerable())
                {
                    ProcessFinalDungeonState(dungeonData, item, resultInLogicQueue, resultOutOfLogicQueue);
                }

                ReturnDungeonData(dungeonData);
            },
            CancellationToken.None, TaskCreationOptions.None, TaskScheduler);

            finalKeyDoorTask.Wait();
            finalQueue.Dispose();
            resultInLogicQueue.CompleteAdding();
            resultOutOfLogicQueue.CompleteAdding();

            List<AccessibilityLevel> lowestBossAccessibilities = new List<AccessibilityLevel>();
            List<AccessibilityLevel> highestBossAccessibilities = new List<AccessibilityLevel>();

            for (int i = 0; i < Bosses.Count; i++)
            {
                lowestBossAccessibilities.Add(AccessibilityLevel.Normal);
                highestBossAccessibilities.Add(AccessibilityLevel.None);
            }

            AccessibilityLevel lowestAccessibility = AccessibilityLevel.Normal;
            AccessibilityLevel highestAccessibility = AccessibilityLevel.None;
            int highestAccessible = 0;

            foreach (var result in resultInLogicQueue.GetConsumingEnumerable())
            {
                for (int i = 0; i < result.BossAccessibility.Count; i++)
                {
                    if (result.BossAccessibility[i] < lowestBossAccessibilities[i])
                    {
                        lowestBossAccessibilities[i] = result.BossAccessibility[i];
                    }

                    if (result.BossAccessibility[i] > highestBossAccessibilities[i])
                    {
                        highestBossAccessibilities[i] = result.BossAccessibility[i];
                    }
                }

                if (result.Accessibility < lowestAccessibility)
                {
                    lowestAccessibility = result.Accessibility;
                }

                if (result.Accessibility > highestAccessibility)
                {
                    highestAccessibility = result.Accessibility;
                }

                if (result.Accessible > highestAccessible)
                {
                    highestAccessible = result.Accessible;
                }
            }

            foreach (var result in resultOutOfLogicQueue.GetConsumingEnumerable())
            {
                for (int i = 0; i < result.BossAccessibility.Count; i++)
                {
                    if (result.BossAccessibility[i] > highestBossAccessibilities[i])
                    {
                        highestBossAccessibilities[i] = result.BossAccessibility[i];
                    }
                }

                if (result.Accessibility > highestAccessibility)
                {
                    highestAccessibility = result.Accessibility;
                }

                if (result.Accessible > highestAccessible)
                {
                    highestAccessible = result.Accessible;
                }
            }

            resultInLogicQueue.Dispose();
            resultOutOfLogicQueue.Dispose();

            AccessibilityLevel finalAccessibility = highestAccessibility;

            if (finalAccessibility == AccessibilityLevel.Normal &&
                lowestAccessibility < AccessibilityLevel.Normal)
            {
                finalAccessibility = AccessibilityLevel.SequenceBreak;
            }

            var firstSection = (Sections[0] as IDungeonItemSection) ??
                throw new Exception("The first section is not a dungeon item section.");

            switch (finalAccessibility)
            {
                case AccessibilityLevel.None:
                case AccessibilityLevel.Inspect:
                    {
                        firstSection.Accessibility = finalAccessibility;
                        firstSection.Accessible = 0;
                    }
                    break;
                case AccessibilityLevel.Partial:
                    {
                        firstSection.Accessibility = finalAccessibility;
                        firstSection.Accessible = highestAccessible;
                    }
                    break;
                case AccessibilityLevel.SequenceBreak:
                case AccessibilityLevel.Normal:
                case AccessibilityLevel.Cleared:
                    {
                        firstSection.Accessibility = finalAccessibility;
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
        /// Signals the dungeon data instance to begin initialization.
        /// </summary>
        /// <param name="dungeonData">
        /// The dungeon data instance to be signaled.
        /// </param>
        public void FinishMutableDungeonCreation(IMutableDungeon dungeonData)
        {
            DungeonDataCreated?.Invoke(this, dungeonData);
        }
    }
}
