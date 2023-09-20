using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OpenTracker.Models.Dungeons.KeyDoors;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Dungeons.State;
using OpenTracker.Models.Items.Keys;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Dungeons.AccessibilityProvider;

/// <summary>
/// This class contains the logic for iterating through key doors in a dungeon.
/// </summary>
[DependencyInjection]
public sealed class KeyDoorIterator : IKeyDoorIterator
{
    private readonly ConstrainedTaskScheduler _taskScheduler;
        
    private readonly IDungeonState.Factory _stateFactory;

    private readonly IDungeon _dungeon;
    private readonly IMutableDungeonQueue _mutableDungeonQueue;

    private ISmallKeyItem SmallKey => _dungeon.SmallKey;
    private IBigKeyItem? BigKey => _dungeon.BigKey;
    private IList<KeyDoorID> SmallKeyDoors => _dungeon.SmallKeyDoors;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="taskScheduler">
    ///     The <see cref="ConstrainedTaskScheduler"/>.
    /// </param>
    /// <param name="stateFactory">
    ///     An Autofac factory for creating new <see cref="IDungeonState"/> objects.
    /// </param>
    /// <param name="dungeon">
    ///     The <see cref="IDungeon"/>.
    /// </param>
    /// <param name="mutableDungeonQueue">
    ///     The <see cref="IMutableDungeonQueue"/>.
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
        
    public void ProcessKeyDoorPermutations(BlockingCollection<IDungeonState> finalQueue)
    {
        var keyDoorPermutationQueue = CreateKeyDoorPermutationQueues();

        PopulateInitialDungeonStates(keyDoorPermutationQueue[0]);
            
        var permutationProcessingTasks = CreatePermutationQueueProcessingTasks(
            keyDoorPermutationQueue, finalQueue);

        Task.WaitAll(permutationProcessingTasks);
        finalQueue.CompleteAdding();
    }
        
    /// <summary>
    /// Creates the <see cref="IList{T}"/> of <see cref="BlockingCollection{T}"/> queues for processing
    /// <see cref="IDungeonState"/> permutations.
    /// </summary>
    /// <returns>
    ///     A <see cref="IList{T}"/> of <see cref="BlockingCollection{T}"/> queues for processing
    ///     <see cref="IDungeonState"/> permutations.
    /// </returns>
    private IList<BlockingCollection<IDungeonState>> CreateKeyDoorPermutationQueues()
    {
        var keyDoorPermutationQueue = new List<BlockingCollection<IDungeonState>>();

        for (var i = 0; i <= SmallKeyDoors.Count; i++)
        {
            keyDoorPermutationQueue.Add(new BlockingCollection<IDungeonState>());
        }

        return keyDoorPermutationQueue;
    }

    /// <summary>
    /// Populates the initial <see cref="IDungeonState"/> permutations for the initial simulations.
    /// </summary>
    /// <param name="firstQueue">
    ///     The <see cref="BlockingCollection{T}"/> to be populated.
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
    /// Returns an array of <see cref="Task"/> to process each <see cref="BlockingCollection{T}"/> queue of
    /// <see cref="IDungeonState"/> permutations.
    /// </summary>
    /// <param name="keyDoorPermutationQueue">
    ///     A <see cref="IList{T}"/> of <see cref="BlockingCollection{T}"/> queues of <see cref="IDungeonState"/>
    ///     permutations to be processed by the tasks.
    /// </param>
    /// <param name="finalQueue">
    ///     The <see cref="BlockingCollection{T}"/> queue to place final <see cref="IDungeonState"/> permutations.
    /// </param>
    /// <returns>
    ///     An array of permutation processing <see cref="Task">Tasks</see>.
    /// </returns>
    private Task[] CreatePermutationQueueProcessingTasks(
        IList<BlockingCollection<IDungeonState>> keyDoorPermutationQueue,
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

        return keyDoorTasks.ToArray();
    }
        
    /// <summary>
    /// Processes a specified <see cref="BlockingCollection{T}"/> queue of <see cref="IDungeonState"/> permutations.
    /// </summary>
    /// <param name="currentQueue">
    ///     The <see cref="BlockingCollection{T}"/> queue to be processed.
    /// </param>
    /// <param name="nextQueue">
    ///     A nullable <see cref="BlockingCollection{T}"/> queue to place generated <see cref="IDungeonState"/>
    ///     permutations for further processing.
    /// </param>
    /// <param name="finalQueue">
    ///     The <see cref="BlockingCollection{T}"/> queue to place final <see cref="IDungeonState"/> permutations.
    /// </param>
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
    /// Process the specified <see cref="IDungeonState"/> permutation.
    /// </summary>
    /// <param name="dungeonData">
    ///     The <see cref="IMutableDungeon"/>.
    /// </param>
    /// <param name="state">
    ///     The <see cref="IDungeonState"/> permutation to be processed.
    /// </param>
    /// <param name="finalQueue">
    ///     The <see cref="BlockingCollection{T}"/> queue to place final <see cref="IDungeonState"/> permutations.
    /// </param>
    /// <param name="nextQueue">
    ///     A nullable <see cref="BlockingCollection{T}"/> queue to place generated <see cref="IDungeonState"/>
    ///     permutations for further processing.
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
    /// Returns the number of keys that are available in the specified <see cref="IDungeonState"/>.
    /// </summary>
    /// <param name="dungeonData">
    ///     The <see cref="IMutableDungeon"/>.
    /// </param>
    /// <param name="state">
    ///     The <see cref="IDungeonState"/>.
    /// </param>
    /// <returns>
    ///     A <see cref="int"/> representing the number of keys available.
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
    ///     The current <see cref="IDungeonState"/> permutation.
    /// </param>
    /// <param name="nextQueue">
    ///     A <see cref="BlockingCollection{T}"/> queue to place generated <see cref="IDungeonState"/> permutations
    ///     for further processing.
    /// </param>
    /// <param name="accessibleKeyDoors">
    ///     The <see cref="IEnumerable{T}"/> of <see cref="KeyDoorID"/> representing the accessible locked key
    ///     doors.
    /// </param>
    private void QueueNextStatePermutations(
        IDungeonState state, BlockingCollection<IDungeonState> nextQueue, IEnumerable<KeyDoorID> accessibleKeyDoors)
    {
        foreach (var keyDoor in accessibleKeyDoors)
        {
            var newPermutation = new List<KeyDoorID>(state.UnlockedDoors) {keyDoor};

            nextQueue.Add(_stateFactory(
                newPermutation, state.KeysCollected, state.BigKeyCollected, state.SequenceBreak));
        }
    }
}