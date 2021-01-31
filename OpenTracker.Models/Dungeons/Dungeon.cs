using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.DungeonItems;
using OpenTracker.Models.DungeonNodes;
using OpenTracker.Models.Items;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.KeyLayouts;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Sections;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OpenTracker.Models.Dungeons
{
    /// <summary>
    /// This is the class containing dungeon data.
    /// </summary>
    public class Dungeon : Location, IDungeon
    {
        public event EventHandler<IMutableDungeon> DungeonDataCreated;

        public int Map { get; }
        public int Compass { get; }
        public int SmallKeys { get; }
        public int BigKey { get; }

        public IItem MapItem { get; }
        public IItem CompassItem { get; }
        public IItem SmallKeyItem { get; }
        public IItem BigKeyItem { get; }

        public List<DungeonItemID> Items { get; }
        public List<DungeonItemID> Bosses { get; }
        public List<DungeonItemID> SmallKeyDrops { get; }
        public List<DungeonItemID> BigKeyDrops { get; }
        public List<KeyDoorID> SmallKeyDoors { get; }
        public List<KeyDoorID> BigKeyDoors { get; }
        public List<IKeyLayout> KeyLayouts { get; }
        public List<DungeonNodeID> Nodes { get; }
        public List<IRequirementNode> EntryNodes { get; }
        public ConcurrentQueue<object> Queue { get; } =
            new ConcurrentQueue<object>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">
        /// The ID of the location.
        /// </param>
        /// <param name="name">
        /// A string representing the name of the dungeon.
        /// </param>
        /// <param name="mapLocations">
        /// A list of map locations.
        /// </param>
        /// <param name="map">
        /// A 32-bit signed integer representing the number of maps in the dungeon.
        /// </param>
        /// <param name="compass">
        /// A 32-bit signed integer representing the number of compasses in the dungeon.
        /// </param>
        /// <param name="smallKeys">
        /// A 32-bit signed integer representing the number of small keys in the dungeon.
        /// </param>
        /// <param name="bigKey">
        /// A 32-bit signed integer representing the number of big keys in the dungeon.
        /// </param>
        /// <param name="mapItem">
        /// The map item.
        /// </param>
        /// <param name="compassItem">
        /// The compass item.
        /// </param>
        /// <param name="smallKeyItem">
        /// The small key item.
        /// </param>
        /// <param name="bigKeyItem">
        /// The big key item.
        /// </param>
        /// <param name="nodes">
        /// A list of dungeon node IDs within the dungeon.
        /// </param>
        /// <param name="items">
        /// A list of dungeon item IDs within the dungeon.
        /// </param>
        /// <param name="bosses">
        /// A list of dungeon item IDs for bosses within the dungeon.
        /// </param>
        /// <param name="smallKeyDoors">
        /// A list of small key door IDs within the dungeon.
        /// </param>
        /// <param name="bigKeyDoors">
        /// A list of big key door IDs within the dungeon.
        /// </param>
        /// <param name="entryNodes">
        /// A list of entry nodes for this dungeon.
        /// </param>
        public Dungeon(
            LocationID id, string name, List<MapLocation> mapLocations, int map, int compass,
            int smallKeys, int bigKey, IItem mapItem, IItem compassItem, IItem smallKeyItem,
            IItem bigKeyItem, List<DungeonNodeID> nodes, List<DungeonItemID> items,
            List<DungeonItemID> bosses, List<DungeonItemID> smallKeyDrops,
            List<DungeonItemID> bigKeyDrops, List<KeyDoorID> smallKeyDoors,
            List<KeyDoorID> bigKeyDoors, List<IRequirementNode> entryNodes) : base(id, name, mapLocations)
        {
            Map = map;
            Compass = compass;
            SmallKeys = smallKeys;
            BigKey = bigKey;
            MapItem = mapItem;
            CompassItem = compassItem;
            SmallKeyItem = smallKeyItem;
            BigKeyItem = bigKeyItem;
            Nodes = nodes ?? throw new ArgumentNullException(nameof(nodes));
            Items = items ?? throw new ArgumentNullException(nameof(items));
            Bosses = bosses ?? throw new ArgumentNullException(nameof(bosses));
            SmallKeyDrops = smallKeyDrops ?? throw new ArgumentNullException(nameof(smallKeyDrops));
            BigKeyDrops = bigKeyDrops ?? throw new ArgumentNullException(nameof(bigKeyDrops));
            SmallKeyDoors = smallKeyDoors ??
                throw new ArgumentNullException(nameof(smallKeyDoors));
            BigKeyDoors = bigKeyDoors ?? throw new ArgumentNullException(nameof(bigKeyDoors));
            KeyLayouts = KeyLayoutFactory.GetDungeonKeyLayouts(this);
            EntryNodes = entryNodes ?? throw new ArgumentNullException(nameof(entryNodes));

            foreach (var section in Sections)
            {
                section.PropertyChanged += OnSectionChanged;
            }

            foreach (var node in EntryNodes)
            {
                node.PropertyChanged += OnNodeChanged;
            }

            if (SmallKeyItem != null)
            {
                SmallKeyItem.PropertyChanged += OnItemChanged;
                ItemDictionary.Instance[ItemType.SmallKey].PropertyChanged += OnItemChanged;
            }

            if (BigKeyItem != null)
            {
                BigKeyItem.PropertyChanged += OnItemChanged;
            }

            for (int i = 0; i < Environment.ProcessorCount - 1; i++)
            {
                Queue.Enqueue(new object());
            }

            Mode.Instance.PropertyChanged += OnModeChanged;
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
        private void OnItemChanged(object sender, PropertyChangedEventArgs e)
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
        private void OnNodeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IRequirementNode.Accessibility))
            {
                UpdateSectionAccessibility();
            }
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
        private void OnRequirementChangePropagated(object sender, EventArgs e)
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
        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Mode.WorldState) ||
                e.PropertyName == nameof(Mode.MapShuffle) ||
                e.PropertyName == nameof(Mode.CompassShuffle) ||
                e.PropertyName == nameof(Mode.SmallKeyShuffle) ||
                e.PropertyName == nameof(Mode.BigKeyShuffle) ||
                e.PropertyName == nameof(Mode.GenericKeys) ||
                e.PropertyName == nameof(Mode.GuaranteedBossItems) ||
                e.PropertyName == nameof(Mode.KeyDropShuffle))
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
        private void OnSectionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ISection.Available))
            {
                UpdateSectionAccessibility();
            }
        }

        /// <summary>
        /// Subscribes to PropertyChanged event on each requirement.
        /// </summary>
        private void SubscribeToConnectionRequirements()
        {
            var requirmentSubscriptions = new List<IRequirement>();
            var dungeonData = GetDungeonData(true);

            foreach (var node in Nodes)
            {
                foreach (var connection in dungeonData.Nodes[node].Connections)
                {
                    var requirement = connection.Requirement;

                    if (requirement is KeyDoorRequirement ||
                        requirmentSubscriptions.Contains(requirement))
                    {
                        continue;
                    }
                    
                    requirement.ChangePropagated += OnRequirementChangePropagated;
                    requirmentSubscriptions.Add(requirement);
                }
            }
        }

        /// <summary>
        /// Returns the next available DungeonData class in the queue.
        /// </summary>
        /// <returns>
        /// The next available DungeonData class.
        /// </returns>
        private IMutableDungeon GetDungeonData(bool force = false)
        {
            while (true)
            {
                if (Queue.TryDequeue(out var _) || force)
                {
                    var dungeonData = MutableDungeonFactory.GetMutableDungeon(this);
                    FinishMutableDungeonCreation(dungeonData);

                    return dungeonData;
                }
            }
        }

        /// <summary>
        /// Returns a list of numbers of small keys that could be collected.
        /// </summary>
        /// <returns>
        /// A list of 32-bit signed integers.
        /// </returns>
        private List<int> GetSmallKeyValues()
        {
            List<int> smallKeyValues = new List<int>();
            int maximumKeys = Mode.Instance.KeyDropShuffle ?
                SmallKeys + SmallKeyDrops.Count : SmallKeys;

            if (Mode.Instance.SmallKeyShuffle)
            {
                if (SmallKeyItem != null)
                {
                    smallKeyValues.Add(Math.Min(maximumKeys, SmallKeyItem.Current +
                        (Mode.Instance.GenericKeys ?
                        ItemDictionary.Instance[ItemType.SmallKey].Current : 0)));
                }
                else
                {
                    smallKeyValues.Add(0);
                }
            }
            else
            {
                for (int i = 0; i <= maximumKeys; i++)
                {
                    smallKeyValues.Add(i);
                }
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
            List<bool> bigKeyValues = new List<bool>();

            if (Mode.Instance.BigKeyShuffle)
            {
                if (Mode.Instance.KeyDropShuffle)
                {
                    if (BigKeyItem != null)
                    {
                        bigKeyValues.Add(BigKeyItem.Current > 0);
                    }
                    else
                    {
                        bigKeyValues.Add(false);
                    }
                }
                else
                {
                    if (BigKey > 0 && BigKeyItem != null)
                    {
                        bigKeyValues.Add(BigKeyItem.Current > 0);
                    }
                    else
                    {
                        bigKeyValues.Add(false);
                    }
                }
            }
            else
            {
                bigKeyValues.Add(false);
                
                if (BigKey > 0 || (BigKeyDrops.Count > 0 && Mode.Instance.KeyDropShuffle))
                {
                    bigKeyValues.Add(true);
                }
            }

            return bigKeyValues;
        }

        /// <summary>
        /// Updates the accessibility and number of accessible items for the contained sections.
        /// </summary>
        private void UpdateSectionAccessibility()
        {
            if (ID == LocationID.GanonsTower)
            {
                UpdateSectionAccessibility(true);
            }
            else
            {
                UpdateSectionAccessibility(false);
            }
        }

        /// <summary>
        /// Populates the initial key permutations for the initial simulations.
        /// </summary>
        /// <param name="collection">
        /// The collection to be populated.
        /// </param>
        private void PopulateInitialDungeonStates(BlockingCollection<DungeonState> collection)
        {
            List<int> smallKeyValues = GetSmallKeyValues();
            List<bool> bigKeyValues = GetBigKeyValues();

            foreach (var smallKeyValue in smallKeyValues)
            {
                foreach (var bigKeyValue in bigKeyValues)
                {
                    collection.Add(
                        new DungeonState(new List<KeyDoorID>(), smallKeyValue, bigKeyValue, false));
                    collection.Add(
                        new DungeonState(new List<KeyDoorID>(), smallKeyValue, bigKeyValue, true));
                }
            }

            collection.CompleteAdding();
        }

        /// <summary>
        /// Process the dungeon state permutation.
        /// </summary>
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
            DungeonState state, BlockingCollection<DungeonState> finalQueue,
            BlockingCollection<DungeonState> nextQueue)
        {
            var dungeonData = GetDungeonData();
            dungeonData.ApplyState(state);

            int availableKeys = dungeonData.GetAvailableSmallKeys(state.SequenceBreak) +
                state.KeysCollected - state.UnlockedDoors.Count;

            if (availableKeys == 0)
            {
                finalQueue.Add(state);
                dungeonData.Dispose();
                return;
            }

            var accessibleKeyDoors = dungeonData.GetAccessibleKeyDoors(state.SequenceBreak);

            if (accessibleKeyDoors.Count == 0)
            {
                finalQueue.Add(state);
                dungeonData.Dispose();
                return;
            }

            foreach (var keyDoor in accessibleKeyDoors)
            {
                var newPermutation = state.UnlockedDoors.GetRange(0, state.UnlockedDoors.Count);
                newPermutation.Add(keyDoor);

                nextQueue.Add(
                    new DungeonState(
                        newPermutation, state.KeysCollected, state.BigKeyCollected,
                        state.SequenceBreak));
            }

            dungeonData.Dispose();
        }

        /// <summary>
        /// Process the final dungeon state permutation.
        /// </summary>
        /// <param name="state">
        /// The permutation to be processed.
        /// </param>
        private void ProcessFinalDungeonState(
            DungeonState state, BlockingCollection<DungeonResult> inLogicQueue,
            BlockingCollection<DungeonResult> outOfLogicQueue)
        {
            var dungeonData = GetDungeonData();
            dungeonData.ApplyState(state);

            if (!dungeonData.ValidateKeyLayout(state))
            {
                dungeonData.Dispose();
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

            dungeonData.Dispose();
        }

        /// <summary>
        /// Updates the accessibility and number of accessible items for the contained sections
        /// using parallel operation.
        /// </summary>
        private void UpdateSectionAccessibility(bool parallel)
        {
            int parallelThreads = parallel ? Math.Max(1, Environment.ProcessorCount - 1) : 1;

            var keyDoorPermutationQueue = new List<BlockingCollection<DungeonState>>();
            var keyDoorTasks = new List<Task[]>();
            var finalQueue = new BlockingCollection<DungeonState>();
            var resultInLogicQueue = new BlockingCollection<DungeonResult>();
            var resultOutOfLogicQueue = new BlockingCollection<DungeonResult>();

            for (int i = 0; i <= SmallKeyDoors.Count; i++)
            {
                keyDoorPermutationQueue.Add(new BlockingCollection<DungeonState>());
            }

            PopulateInitialDungeonStates(keyDoorPermutationQueue[0]);

            for (int i = 0; i < keyDoorPermutationQueue.Count; i++)
            {
                int currentIteration = i;

                keyDoorTasks.Add(Enumerable.Range(1, parallelThreads)
                    .Select(_ => Task.Factory.StartNew(() =>
                    {
                        foreach (var item in keyDoorPermutationQueue[currentIteration].GetConsumingEnumerable())
                        {
                            if (keyDoorPermutationQueue.Count > currentIteration + 1)
                            {
                                ProcessDungeonState(item, finalQueue, keyDoorPermutationQueue[currentIteration + 1]);
                            }
                            else
                            {
                                ProcessDungeonState(item, finalQueue, null);
                            }
                        }
                    },
                    CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default)).ToArray());
            }

            for (int i = 0; i < keyDoorTasks.Count; i++)
            {
                Task.WaitAll(keyDoorTasks[i]);
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

            Task[] finalKeyDoorTasks = Enumerable.Range(1, parallelThreads)
                .Select(_ => Task.Factory.StartNew(() =>
                {
                    foreach (var item in finalQueue.GetConsumingEnumerable())
                    {
                        ProcessFinalDungeonState(item, resultInLogicQueue, resultOutOfLogicQueue);
                    }
                },
                CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default)).ToArray();

            Task.WaitAll(finalKeyDoorTasks);
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

            switch (finalAccessibility)
            {
                case AccessibilityLevel.None:
                case AccessibilityLevel.Inspect:
                    {
                        (Sections[0] as IDungeonItemSection).Accessibility = finalAccessibility;
                        (Sections[0] as IDungeonItemSection).Accessible = 0;
                    }
                    break;
                case AccessibilityLevel.Partial:
                    {
                        (Sections[0] as IDungeonItemSection).Accessibility = finalAccessibility;
                        (Sections[0] as IDungeonItemSection).Accessible = highestAccessible;
                    }
                    break;
                case AccessibilityLevel.SequenceBreak:
                case AccessibilityLevel.Normal:
                case AccessibilityLevel.Cleared:
                    {
                        (Sections[0] as IDungeonItemSection).Accessibility = finalAccessibility;
                        (Sections[0] as IDungeonItemSection).Accessible = Sections[0].Available;
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

                (Sections[i + 1] as IBossSection).Accessibility = highestBossAccessibilities[i];
            }
        }

        public void FinishMutableDungeonCreation(IMutableDungeon dungeonData)
        {
            DungeonDataCreated?.Invoke(this, dungeonData);
        }
    }
}
