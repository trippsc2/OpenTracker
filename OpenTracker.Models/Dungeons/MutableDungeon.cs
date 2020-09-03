using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.DungeonItems;
using OpenTracker.Models.DungeonNodes;
using OpenTracker.Models.KeyDoors;
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
                foreach (KeyDoorID smallKeyDoor in _dungeon.SmallKeyDoors)
                {
                    SmallKeyDoors.Add(smallKeyDoor, KeyDoorDictionary[smallKeyDoor]);
                }

                foreach (KeyDoorID bigKeyDoor in _dungeon.BigKeyDoors)
                {
                    BigKeyDoors.Add(bigKeyDoor, KeyDoorDictionary[bigKeyDoor]);
                }

                foreach (DungeonItemID item in _dungeon.Items)
                {
                    Items.Add(item, ItemDictionary[item]);
                }

                foreach (DungeonItemID boss in _dungeon.Bosses)
                {
                    BossItems.Add(ItemDictionary[boss]);
                }

                foreach (DungeonNodeID node in _dungeon.Nodes)
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
        public void SetSmallKeyDoorState(List<KeyDoorID> unlockedDoors)
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
        public void SetBigKeyDoorState(bool unlocked)
        {
            foreach (IKeyDoor bigKeyDoor in BigKeyDoors.Values)
            {
                bigKeyDoor.Unlocked = unlocked;
            }
        }

        /// <summary>
        /// Returns the number of keys that are available in all dungeon nodes for free.
        /// </summary>
        /// <returns>
        /// A 32-bit integer representing the number of keys that can be collected for free.
        /// </returns>
        public int GetFreeKeys()
        {
            int freeKeys = 0;

            foreach (DungeonNode node in Nodes.Values)
            {
                if (node.Accessibility >= AccessibilityLevel.SequenceBreak)
                {
                    freeKeys += node.KeysProvided;
                }
            }

            return freeKeys;
        }

        /// <summary>
        /// Returns a list of locked key doors that are accessible.
        /// </summary>
        /// <returns>
        /// A list of locked key doors that are accessible and whether they are a sequence break.
        /// </returns>
        public List<(KeyDoorID, bool)> GetAccessibleKeyDoors()
        {
            var accessibleKeyDoors = new List<(KeyDoorID, bool)>();

            foreach (var key in SmallKeyDoors.Keys)
            {
                var keyDoor = SmallKeyDoors[key];

                if (keyDoor.Accessibility >= AccessibilityLevel.SequenceBreak && !keyDoor.Unlocked)
                {
                    accessibleKeyDoors.Add((key, keyDoor.Accessibility == AccessibilityLevel.SequenceBreak));
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
        public bool ValidateKeyLayout(int keysCollected, bool bigKeyCollected)
        {
            foreach (var keyLayout in _dungeon.KeyLayouts)
            {
                if (keyLayout.CanBeTrue(this, keysCollected, bigKeyCollected))
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
        public List<AccessibilityLevel> GetBossAccessibility()
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
        /// <param name="sequenceBroken">
        /// A boolean representing whether the order of key doors opened requires a sequence break.
        /// </param>
        /// <returns>
        /// A tuple of the accessibility of items in the dungeon and a 32-bit integer representing
        /// the number of accessible items in the dungeon.
        /// </returns>
        public (AccessibilityLevel, int, bool) GetItemAccessibility(
            int smallKeyValue, bool bigKeyValue, bool sequenceBroken)
        {
            int inaccessibleBosses = 0;
            int inaccessibleItems = 0;
            bool sequenceBreak = sequenceBroken;

            foreach (DungeonItemID item in Items.Keys)
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
                    case AccessibilityLevel.SequenceBreak:
                        {
                            sequenceBreak = true;
                        }
                        break;
                }
            }

            if (inaccessibleItems <= 0)
            {
                if (sequenceBreak)
                {
                    return
                        (AccessibilityLevel.SequenceBreak, _dungeon.Sections[0].Available, sequenceBroken);
                }
                else
                {
                    return (AccessibilityLevel.Normal, _dungeon.Sections[0].Available, sequenceBroken);
                }
            }

            if (!Mode.Instance.BigKeyShuffle && !bigKeyValue)
            {
                inaccessibleItems -= _dungeon.BigKey;
            }

            if (inaccessibleItems <= 0)
            {
                if (sequenceBreak)
                {
                    return
                        (AccessibilityLevel.SequenceBreak, _dungeon.Sections[0].Available, sequenceBroken);
                }
                else
                {
                    return (AccessibilityLevel.Normal, _dungeon.Sections[0].Available, sequenceBroken);
                }
            }

            if (!Mode.Instance.SmallKeyShuffle)
            {
                inaccessibleItems -= _dungeon.SmallKeys - smallKeyValue;
            }

            if (inaccessibleItems <= 0)
            {
                if (sequenceBreak)
                {
                    return (AccessibilityLevel.SequenceBreak, _dungeon.Sections[0].Available, sequenceBroken);
                }
                else
                {
                    return (AccessibilityLevel.Normal, _dungeon.Sections[0].Available, sequenceBroken);
                }
            }

            if (!Mode.Instance.MapCompassShuffle)
            {
                inaccessibleItems -= _dungeon.Map + _dungeon.Compass;

                if (Mode.Instance.GuaranteedBossItems)
                {
                    inaccessibleItems = Math.Max(inaccessibleItems, inaccessibleBosses);
                }
            }

            if (inaccessibleItems <= 0)
            {
                return
                    (AccessibilityLevel.SequenceBreak, _dungeon.Sections[0].Available, sequenceBroken);
            }

            if (inaccessibleItems >= _dungeon.Sections[0].Available)
            {
                return (AccessibilityLevel.None, 0, sequenceBroken);
            }
            else
            {
                return (AccessibilityLevel.Partial, Math.Max(0,
                    _dungeon.Sections[0].Available - inaccessibleItems), sequenceBroken);
            }
        }
    }
}
