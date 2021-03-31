using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Dungeons.State;
using OpenTracker.Models.Items.Keys;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Utils;

namespace OpenTracker.Models.Dungeons.AccessibilityProvider
{
    /// <summary>
    ///     This class contains the logic for iterating through key doors in a dungeon.
    /// </summary>
    public class KeyDoorIterator : IKeyDoorIterator
    {
        private readonly ConstrainedTaskScheduler _taskScheduler;
        
        private readonly IDungeonState.Factory _stateFactory;

        private readonly IDungeon _dungeon;
        private readonly IMutableDungeonQueue _mutableDungeonQueue;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="taskScheduler">
        ///     The task scheduler.
        /// </param>
        /// <param name="stateFactory">
        ///     An Autofac factory for creating new dungeon states.
        /// </param>
        /// <param name="dungeon">
        ///     The dungeon data.
        /// </param>
        /// <param name="mutableDungeonQueue">
        ///     The queue of mutable dungeon data instances.
        /// </param>
        public KeyDoorIterator(
            ConstrainedTaskScheduler taskScheduler, IDungeonState.Factory stateFactory, IDungeon dungeon,
            IMutableDungeonQueue mutableDungeonQueue)
        {
            _taskScheduler = taskScheduler;
            _stateFactory = stateFactory;
            _dungeon = dungeon;
            _mutableDungeonQueue = mutableDungeonQueue;
        }

        private ISmallKeyItem SmallKey => _dungeon.SmallKey;
        private IBigKeyItem? BigKey => _dungeon.BigKey;
        private List<KeyDoorID> SmallKeyDoors => _dungeon.SmallKeyDoors;
        
        public void ProcessKeyDoorPermutations(BlockingCollection<IDungeonState> finalQueue)
        {
            var keyDoorPermutationQueue = CreateKeyDoorPermutationQueues();

            PopulateInitialDungeonStates(keyDoorPermutationQueue[0]);
            
            var permutationProcessingTasks = CreatePermutationQueueProcessingTasks(
                keyDoorPermutationQueue, finalQueue);

            Task.WaitAll(permutationProcessingTasks.ToArray());
            finalQueue.CompleteAdding();
        }
        
        /// <summary>
        ///     Creates the list of blocking collection queues for processing dungeon key door permutations.
        /// </summary>
        /// <returns>
        ///     A list of blocking collection queues.
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
        ///     Populates the initial key permutations for the initial simulations.
        /// </summary>
        /// <param name="firstQueue">
        ///     The blocking collection to be populated.
        /// </param>
        private void PopulateInitialDungeonStates(BlockingCollection<IDungeonState> firstQueue)
        {
            var smallKeyValues = SmallKey.GetKeyValues();
            var bigKeyValues = BigKey is null ? new List<bool> {false} : BigKey.GetKeyValues();

            foreach (var smallKeyValue in smallKeyValues)
            {
                foreach (var bigKeyValue in bigKeyValues)
                {
                    firstQueue.Add(_stateFactory(
                        new List<KeyDoorID>(), smallKeyValue, bigKeyValue, false));
                    firstQueue.Add(_stateFactory(
                        new List<KeyDoorID>(), smallKeyValue, bigKeyValue, true));
                }
            }

            firstQueue.CompleteAdding();
        }

        /// <summary>
        ///     Returns a list of asynchronous tasks to process each permutation queue.
        /// </summary>
        /// <param name="keyDoorPermutationQueue">
        ///     A list of key door permutation queues to be processed by tasks.
        /// </param>
        /// <param name="finalQueue">
        ///     The blocking collection queue to place final permutations.
        /// </param>
        /// <returns>
        ///     A list of permutation processing tasks.
        /// </returns>
        private List<Task> CreatePermutationQueueProcessingTasks(
            List<BlockingCollection<IDungeonState>> keyDoorPermutationQueue,
            BlockingCollection<IDungeonState> finalQueue)
        {
            var keyDoorTasks = new List<Task>();

            for (var i = 0; i < keyDoorPermutationQueue.Count; i++)
            {
                var currentQueue = keyDoorPermutationQueue[i];
                var nextQueue = i == keyDoorPermutationQueue.Count - 1 ? null :
                    keyDoorPermutationQueue[i + 1];

                keyDoorTasks.Add(Task.Factory.StartNew(
                    () => { ProcessPermutationQueue(currentQueue, nextQueue, finalQueue); },
                    CancellationToken.None, TaskCreationOptions.None, _taskScheduler));
            }

            return keyDoorTasks;
        }
        
        /// <summary>
        ///     Processes a specified permutation queue.
        /// </summary>
        /// <param name="currentQueue">
        ///     The blocking collection queue to be processed.
        /// </param>
        /// <param name="nextQueue">
        ///     A nullable blocking collection queue to place generated permutations.
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
        ///     Process the dungeon state permutation.
        /// </summary>
        /// <param name="dungeonData">
        ///     The mutable dungeon data.
        /// </param>
        /// <param name="state">
        ///     The permutation to be processed.
        /// </param>
        /// <param name="finalQueue">
        ///     The final queue.
        /// </param>
        /// <param name="nextQueue">
        ///     The next queue to which this permutation will be added.
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
        ///     Returns the number of keys that are available in the current dungeon state.
        /// </summary>
        /// <param name="dungeonData">
        ///     The mutable dungeon data.
        /// </param>
        /// <param name="state">
        ///     The current dungeon state.
        /// </param>
        /// <returns>
        ///     A 32-bit signed integer representing the number of keys available.
        /// </returns>
        private static int GetAvailableSmallKeys(IMutableDungeon dungeonData, IDungeonState state)
        {
            return dungeonData.GetAvailableSmallKeys(state.SequenceBreak) +
                state.KeysCollected - state.UnlockedDoors.Count;
        }

        /// <summary>
        ///     Queues the next set of state permutations.
        /// </summary>
        /// <param name="state">
        ///     The current dungeon state.
        /// </param>
        /// <param name="nextQueue">
        ///     The next permutation queue.
        /// </param>
        /// <param name="accessibleKeyDoors">
        ///     The enumerable of accessible locked key doors.
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
    }
}