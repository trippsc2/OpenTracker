using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.DungeonItems;
using OpenTracker.Models.DungeonNodes;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.KeyLayouts;
using OpenTracker.Models.Modes;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.Dungeons
{
    /// <summary>
    /// This is the class containing the mutable data for a dungeon.
    /// </summary>
    public class MutableDungeon : IMutableDungeon
    {
        private readonly IDungeon _dungeon;

        public KeyDoorDictionary KeyDoorDictionary { get; }
        public DungeonItemDictionary ItemDictionary { get; }
        public Dictionary<KeyDoorID, IKeyDoor> SmallKeyDoors { get; } =
            new Dictionary<KeyDoorID, IKeyDoor>();
        public Dictionary<KeyDoorID, IKeyDoor> BigKeyDoors { get; } =
            new Dictionary<KeyDoorID, IKeyDoor>();
        public DungeonNodeDictionary Nodes { get; }
        public Dictionary<DungeonItemID, IDungeonItem> Items { get; } =
            new Dictionary<DungeonItemID, IDungeonItem>();
        public Dictionary<DungeonItemID, IDungeonItem> SmallKeyDrops { get; } =
            new Dictionary<DungeonItemID, IDungeonItem>();
        public Dictionary<DungeonItemID, IDungeonItem> BigKeyDrops { get; } =
            new Dictionary<DungeonItemID, IDungeonItem>();
        public List<IDungeonItem> BossItems { get; } =
            new List<IDungeonItem>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dungeon">
        /// The dungeon immutable data.
        /// </param>
        public MutableDungeon(IDungeon dungeon)
        {
            KeyDoorDictionary = new KeyDoorDictionary(this);
            ItemDictionary = new DungeonItemDictionary(this);
            Nodes = new DungeonNodeDictionary(this, dungeon);
            _dungeon = dungeon ?? throw new ArgumentNullException(nameof(dungeon));

            _dungeon.DungeonDataCreated += OnDungeonDataCreated;
        }

        /// <summary>
        /// Subscribes to the DungeonDataCreated event on the IDungeon interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the DungeonDataCreated event.
        /// </param>
        private void OnDungeonDataCreated(object sender, IMutableDungeon e)
        {
            if (e == this)
            {
                foreach (var smallKeyDoor in _dungeon.SmallKeyDoors)
                {
                    SmallKeyDoors.Add(smallKeyDoor, KeyDoorDictionary[smallKeyDoor]);
                }

                foreach (var bigKeyDoor in _dungeon.BigKeyDoors)
                {
                    BigKeyDoors.Add(bigKeyDoor, KeyDoorDictionary[bigKeyDoor]);
                }

                foreach (var item in _dungeon.Items)
                {
                    Items.Add(item, ItemDictionary[item]);
                }

                foreach (var boss in _dungeon.Bosses)
                {
                    BossItems.Add(ItemDictionary[boss]);
                }

                foreach (var smallKeyDrop in _dungeon.SmallKeyDrops)
                {
                    SmallKeyDrops.Add(smallKeyDrop, ItemDictionary[smallKeyDrop]);
                }

                foreach (var bigKeyDrop in _dungeon.BigKeyDrops)
                {
                    BigKeyDrops.Add(bigKeyDrop, ItemDictionary[bigKeyDrop]);
                }

                foreach (var node in _dungeon.Nodes)
                {
                    _ = Nodes[node];
                }

                _dungeon.DungeonDataCreated -= OnDungeonDataCreated;
            }
        }

        /// <summary>
        /// Sets the state of all small key doors based on a specified list of unlocked doors.
        /// </summary>
        /// <param name="unlockedDoors">
        /// A list of unlocked doors.
        /// </param>
        private void SetSmallKeyDoorState(List<KeyDoorID> unlockedDoors)
        {
            if (unlockedDoors == null)
            {
                throw new ArgumentNullException(nameof(unlockedDoors));
            }

            foreach (var smallKeyDoor in SmallKeyDoors.Keys)
            {
                if (unlockedDoors.Contains(smallKeyDoor))
                {
                    SmallKeyDoors[smallKeyDoor].Unlocked = true;
                }
                else
                {
                    SmallKeyDoors[smallKeyDoor].Unlocked = false;
                }
            }
        }

        /// <summary>
        /// Sets the state of all big key doors to a specified state.
        /// </summary>
        /// <param name="unlocked">
        /// A boolean representing whether the doors are to be unlocked.
        /// </param>
        private void SetBigKeyDoorState(bool unlocked)
        {
            foreach (IKeyDoor bigKeyDoor in BigKeyDoors.Values)
            {
                bigKeyDoor.Unlocked = unlocked;
            }
        }

        /// <summary>
        /// Returns the number of big keys that are available to be collected in the dungeon.
        /// </summary>
        /// <param name="sequenceBreak">
        /// A boolean representing whether sequence breaking is allowed for this count.
        /// </param>
        /// <returns>
        /// A 32-bit integer representing the number of big keys that are available to be collected in the
        /// dungeon.
        /// </returns>
        private int GetAvailableBigKeys(bool sequenceBreak = false)
        {
            if (Mode.Instance.KeyDropShuffle)
            {
                return 0;
            }

            int bigKeys = 0;

            foreach (var bigKeyDrop in BigKeyDrops.Values)
            {
                if (bigKeyDrop.Accessibility == AccessibilityLevel.Normal)
                {
                    bigKeys++;
                }
                else if (sequenceBreak && bigKeyDrop.Accessibility == AccessibilityLevel.SequenceBreak)
                {
                    bigKeys++;
                }
            }

            return bigKeys;
        }

        public void ApplyState(DungeonState state)
        {
            SetSmallKeyDoorState(state.UnlockedDoors);

            bool bigKeyCollected = state.BigKeyCollected || GetAvailableBigKeys() > 0;

            SetBigKeyDoorState(bigKeyCollected);
        }

        /// <summary>
        /// Returns the number of keys that are available to be collected in the dungeon.
        /// </summary>
        /// <param name="sequenceBreak">
        /// A boolean representing whether sequence breaking is allowed for this count.
        /// </param>
        /// <returns>
        /// A 32-bit integer representing the number of keys that are available to be collected in the
        /// dungeon.
        /// </returns>
        public int GetAvailableSmallKeys(bool sequenceBreak = false)
        {
            if (Mode.Instance.KeyDropShuffle)
            {
                return 0;
            }

            int smallKeys = 0;

            foreach (var smallKeyDrop in SmallKeyDrops.Values)
            {
                if (smallKeyDrop.Accessibility == AccessibilityLevel.Normal)
                {
                    smallKeys++;
                }
                else if (sequenceBreak && smallKeyDrop.Accessibility == AccessibilityLevel.SequenceBreak)
                {
                    smallKeys++;
                }
            }

            return smallKeys;
        }

        /// <summary>
        /// Returns a list of locked key doors that are accessible.
        /// </summary>
        /// <returns>
        /// A list of locked key doors that are accessible and whether they are a sequence break.
        /// </returns>
        public List<KeyDoorID> GetAccessibleKeyDoors(bool sequenceBreak = false)
        {
            var accessibleKeyDoors = new List<KeyDoorID>();

            foreach (var key in SmallKeyDoors.Keys)
            {
                var keyDoor = SmallKeyDoors[key];

                if (!keyDoor.Unlocked)
                {
                    switch (keyDoor.Accessibility)
                    {
                        case AccessibilityLevel.SequenceBreak:
                            {
                                if (sequenceBreak)
                                {
                                    accessibleKeyDoors.Add(key);
                                }
                            }
                            break;
                        case AccessibilityLevel.Normal:
                            {
                                accessibleKeyDoors.Add(key);
                            }
                            break;
                    }
                }
            }

            return accessibleKeyDoors;
        }

        /// <summary>
        /// Returns whether the specified number of collected keys and big key can occur,
        /// based on key logic.
        /// </summary>
        /// <param name="keysCollected">
        /// A 32-bit integer representing the number of small keys collected.
        /// </param>
        /// <param name="bigKeyCollected">
        /// A boolean representing whether the big key is collected.
        /// </param>
        /// <returns>
        /// A boolean representing whether the result can occur.
        /// </returns>
        public bool ValidateKeyLayout(DungeonState state)
        {
            foreach (var keyLayout in _dungeon.KeyLayouts)
            {
                if (keyLayout.CanBeTrue(this, state))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns a list of the current accessibility of each boss in the dungeon.
        /// </summary>
        /// <returns>
        /// A list of the current accessibility of each boss in the dungeon.
        /// </returns>
        private List<AccessibilityLevel> GetBossAccessibility()
        {
            List<AccessibilityLevel> bossAccessibility = new List<AccessibilityLevel>();

            foreach (DungeonItem bossItem in BossItems)
            {
                bossAccessibility.Add(bossItem.Accessibility);
            }

            return bossAccessibility;
        }

        /// <summary>
        /// Returns the current accessibility and accessible item count based on the specified
        /// number of small keys collected and whether the big key is collected.
        /// </summary>
        /// <param name="smallKeyValue">
        /// A 32-bit integer representing the number of small keys collected.
        /// </param>
        /// <param name="bigKeyValue">
        /// A boolean representing whether the big key is collected.
        /// </param>
        /// <param name="validationState">
        /// A boolean representing whether the order of key doors opened requires a sequence break.
        /// </param>
        /// <returns>
        /// A tuple of the accessibility of items in the dungeon and a 32-bit integer representing
        /// the number of accessible items in the dungeon.
        /// </returns>
        public DungeonResult GetDungeonResult(DungeonState state)
        {
            int inaccessibleBosses = 0;
            int inaccessibleItems = 0;
            bool visible = false;

            foreach (var item in Items.Keys)
            {
                switch (Items[item].Accessibility)
                {
                    case AccessibilityLevel.None:
                        {
                            if (_dungeon.Bosses.Contains(item))
                            {
                                inaccessibleBosses++;
                            }

                            inaccessibleItems++;
                        }
                        break;
                    case AccessibilityLevel.Inspect:
                        {
                            inaccessibleItems++;
                            visible = true;
                        }
                        break;
                    case AccessibilityLevel.SequenceBreak:
                        {
                            if (!state.SequenceBreak)
                            {
                                inaccessibleItems++;
                            }
                        }
                        break;
                }
            }

            if (Mode.Instance.KeyDropShuffle)
            {
                foreach (var item in SmallKeyDrops.Values)
                {
                    switch (item.Accessibility)
                    {
                        case AccessibilityLevel.None:
                            {
                                inaccessibleItems++;
                            }
                            break;
                        case AccessibilityLevel.SequenceBreak:
                            {
                                if (!state.SequenceBreak)
                                {
                                    inaccessibleItems++;
                                }
                            }
                            break;
                    }
                }

                foreach (var item in BigKeyDrops.Values)
                {
                    switch (item.Accessibility)
                    {
                        case AccessibilityLevel.None:
                            {
                                inaccessibleItems++;
                            }
                            break;
                        case AccessibilityLevel.SequenceBreak:
                            {
                                if (!state.SequenceBreak)
                                {
                                    inaccessibleItems++;
                                }
                            }
                            break;
                    }
                }
            }

            int minimumInaccessible = Mode.Instance.GuaranteedBossItems ? inaccessibleBosses : 0;
            var bossAccessibility = GetBossAccessibility();

            if (inaccessibleItems <= minimumInaccessible)
            {
                if (minimumInaccessible == 0)
                {                    
                    return new DungeonResult(
                        bossAccessibility, state.SequenceBreak ? AccessibilityLevel.SequenceBreak :
                        AccessibilityLevel.Normal, _dungeon.Sections[0].Available);
                }

                if (minimumInaccessible >= _dungeon.Sections[0].Available)
                {
                    if (visible)
                    {
                        return new DungeonResult(bossAccessibility, AccessibilityLevel.Inspect, 0);
                    }

                    return new DungeonResult(bossAccessibility, AccessibilityLevel.None, 0);
                }
                else
                {
                    return new DungeonResult(
                        bossAccessibility, AccessibilityLevel.Partial,
                        _dungeon.Sections[0].Available - minimumInaccessible);
                }
            }

            if (!Mode.Instance.BigKeyShuffle && !state.BigKeyCollected)
            {
                int bigKey = Mode.Instance.KeyDropShuffle ? 
                    _dungeon.BigKey + _dungeon.BigKeyDrops.Count : _dungeon.BigKey;
                inaccessibleItems -= bigKey;
            }

            if (inaccessibleItems <= minimumInaccessible)
            {
                if (minimumInaccessible == 0)
                {
                    return new DungeonResult(
                        bossAccessibility, state.SequenceBreak ? AccessibilityLevel.SequenceBreak :
                        AccessibilityLevel.Normal, _dungeon.Sections[0].Available);
                }

                if (minimumInaccessible >= _dungeon.Sections[0].Available)
                {
                    if (visible)
                    {
                        return new DungeonResult(bossAccessibility, AccessibilityLevel.Inspect, 0);
                    }

                    return new DungeonResult(bossAccessibility, AccessibilityLevel.None, 0);
                }
                else
                {
                    return new DungeonResult(
                        bossAccessibility, AccessibilityLevel.Partial,
                        _dungeon.Sections[0].Available - minimumInaccessible);
                }
            }

            if (!Mode.Instance.SmallKeyShuffle)
            {
                int smallKeys = Mode.Instance.KeyDropShuffle ?
                    _dungeon.SmallKeys + _dungeon.SmallKeyDrops.Count : _dungeon.SmallKeys;
                inaccessibleItems -= smallKeys - state.KeysCollected;
            }

            if (inaccessibleItems <= minimumInaccessible)
            {
                if (minimumInaccessible == 0)
                {
                    return new DungeonResult(
                        bossAccessibility, state.SequenceBreak ? AccessibilityLevel.SequenceBreak :
                        AccessibilityLevel.Normal, _dungeon.Sections[0].Available);
                }

                if (minimumInaccessible >= _dungeon.Sections[0].Available)
                {
                    if (visible)
                    {
                        return new DungeonResult(bossAccessibility, AccessibilityLevel.Inspect, 0);
                    }

                    return new DungeonResult(bossAccessibility, AccessibilityLevel.None, 0);
                }
                else
                {
                    return new DungeonResult(
                        bossAccessibility, AccessibilityLevel.Partial,
                        _dungeon.Sections[0].Available - minimumInaccessible);
                }
            }

            if (!Mode.Instance.MapShuffle)
            {
                inaccessibleItems -= _dungeon.Map;
            }
            
            if (!Mode.Instance.CompassShuffle)
            {
                inaccessibleItems -= _dungeon.Compass;
            }
            
            inaccessibleItems = Math.Max(inaccessibleItems, minimumInaccessible);

            if (inaccessibleItems <= 0)
            {
                return new DungeonResult(
                    bossAccessibility, AccessibilityLevel.SequenceBreak,
                    _dungeon.Sections[0].Available);
            }

            if (inaccessibleItems >= _dungeon.Sections[0].Available)
            {
                if (visible)
                {
                    return new DungeonResult(bossAccessibility, AccessibilityLevel.Inspect, 0);
                }

                return new DungeonResult(bossAccessibility, AccessibilityLevel.None, 0);
            }
            
            return new DungeonResult(
                bossAccessibility, AccessibilityLevel.Partial,
                _dungeon.Sections[0].Available - inaccessibleItems);
        }

        /// <summary>
        /// Resets the dungeon data for testing purposes.
        /// </summary>
        public void Reset()
        {
            foreach (var node in Nodes.Values)
            {
                node.Reset();
            }

            foreach (var door in KeyDoorDictionary.Values)
            {
                door.Unlocked = false;
            }
        }
    }
}
