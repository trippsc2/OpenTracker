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
        public DungeonNodeDictionary RequirementNodes { get; }
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
            RequirementNodes = new DungeonNodeDictionary(this, dungeon);
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

            foreach (KeyDoor smallKeyDoor in SmallKeyDoors.Values)
            {
                if (unlockedDoors.Contains(smallKeyDoor.ID))
                {
                    smallKeyDoor.Unlocked = true;
                }
                else
                {
                    smallKeyDoor.Unlocked = false;
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
            foreach (KeyDoor bigKeyDoor in BigKeyDoors.Values)
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

            foreach (DungeonNode node in RequirementNodes.Values)
            {
                if (node.Accessibility >= AccessibilityLevel.SequenceBreak)
                {
                    freeKeys += node.FreeKeysProvided;
                }
            }

            return freeKeys;
        }

        /// <summary>
        /// Returns a list of locked key doors that are accessible.
        /// </summary>
        /// <returns>
        /// A list of locked key doors that are accessible.
        /// </returns>
        public List<KeyDoorID> GetAccessibleKeyDoors()
        {
            List<KeyDoorID> accessibleKeyDoors = new List<KeyDoorID>();

            foreach (KeyDoor keyDoor in SmallKeyDoors.Values)
            {
                if (keyDoor.Accessibility >= AccessibilityLevel.SequenceBreak && !keyDoor.Unlocked)
                {
                    accessibleKeyDoors.Add(keyDoor.ID);
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
        /// <returns>
        /// A tuple of the accessibility of items in the dungeon and a 32-bit integer representing
        /// the number of accessible items in the dungeon.
        /// </returns>
        public (AccessibilityLevel, int) GetItemAccessibility(int smallKeyValue, bool bigKeyValue)
        {
            int inaccessibleItems = 0;
            bool sequenceBreak = false;

            foreach (DungeonItem item in Items.Values)
            {
                switch (item.Accessibility)
                {
                    case AccessibilityLevel.None:
                        {
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
                    return (AccessibilityLevel.SequenceBreak, _dungeon.Sections[0].Available);
                }
                else
                {
                    return (AccessibilityLevel.Normal, _dungeon.Sections[0].Available);
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
                    return (AccessibilityLevel.SequenceBreak, _dungeon.Sections[0].Available);
                }
                else
                {
                    return (AccessibilityLevel.Normal, _dungeon.Sections[0].Available);
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
                    return (AccessibilityLevel.SequenceBreak, _dungeon.Sections[0].Available);
                }
                else
                {
                    return (AccessibilityLevel.Normal, _dungeon.Sections[0].Available);
                }
            }

            if (!Mode.Instance.MapCompassShuffle)
            {
                inaccessibleItems -= _dungeon.Map + _dungeon.Compass;
            }

            if (inaccessibleItems <= 0)
            {
                return (AccessibilityLevel.SequenceBreak, _dungeon.Sections[0].Available);
            }

            if (inaccessibleItems >= _dungeon.Sections[0].Available)
            {
                return (AccessibilityLevel.None, 0);
            }
            else
            {
                return (AccessibilityLevel.Partial, Math.Max(0,
                    _dungeon.Sections[0].Available - inaccessibleItems));
            }
        }
    }
}
