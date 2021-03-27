using System;
using System.Collections.Generic;
using System.Linq;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.DungeonItems;
using OpenTracker.Models.DungeonNodes;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Sections;

namespace OpenTracker.Models.Dungeons.Mutable
{
    /// <summary>
    /// This class contains the mutable dungeon data.
    /// </summary>
    public class MutableDungeon : IMutableDungeon
    {
        private readonly IMode _mode;
        private readonly IDungeon _dungeon;
        private readonly IDungeonResult.Factory _resultFactory;

        private readonly Dictionary<KeyDoorID, IKeyDoor> _smallKeyDoors = new();
        private readonly Dictionary<KeyDoorID, IKeyDoor> _bigKeyDoors = new();
        private readonly Dictionary<DungeonItemID, IDungeonItem> _items = new();
        private readonly Dictionary<DungeonItemID, IDungeonItem> _smallKeyDrops = new();
        private readonly Dictionary<DungeonItemID, IDungeonItem> _bigKeyDrops = new();
        private readonly List<IDungeonItem> _bosses = new();

        public LocationID ID => _dungeon.ID;
        public IKeyDoorDictionary KeyDoors { get; }
        public IDungeonItemDictionary DungeonItems { get; }
        public IDungeonNodeDictionary Nodes { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode data.
        /// </param>
        /// <param name="keyDoors">
        /// The key door dictionary.
        /// </param>
        /// <param name="nodes">
        /// The dungeon node dictionary.
        /// </param>
        /// <param name="dungeonItems">
        /// The dungeon item dictionary.
        /// </param>
        /// <param name="resultFactory">
        /// An Autofac factory for creating dungeon results.
        /// </param>
        /// <param name="dungeon">
        /// The dungeon immutable data.
        /// </param>
        public MutableDungeon(
            IMode mode, IKeyDoorDictionary.Factory keyDoors, IDungeonNodeDictionary.Factory nodes,
            IDungeonItemDictionary.Factory dungeonItems, IDungeonResult.Factory resultFactory, IDungeon dungeon)
        {
            _mode = mode;
            _dungeon = dungeon;
            _resultFactory = resultFactory;

            KeyDoors = keyDoors(this);
            DungeonItems = dungeonItems(this);
            Nodes = nodes(this);
        }

        /// <summary>
        /// Initialize all data in this dungeon data instance.
        /// </summary>
        public void InitializeData()
        {
            PopulateSmallKeyDoors();
            PopulateBigKeyDoors();
            PopulateDungeonItems();
            PopulateBosses();
            PopulateSmallKeyDrops();
            PopulateBigKeyDrops();
            PopulateNodes();
        }

        /// <summary>
        /// Creates all small key doors and adds them to the list.
        /// </summary>
        private void PopulateSmallKeyDoors()
        {
            foreach (var smallKeyDoor in _dungeon.SmallKeyDoors)
            {
                _smallKeyDoors.Add(smallKeyDoor, KeyDoors[smallKeyDoor]);
            }
        }

        /// <summary>
        /// Creates all big key doors and adds them to the list.
        /// </summary>
        private void PopulateBigKeyDoors()
        {
            foreach (var bigKeyDoor in _dungeon.BigKeyDoors)
            {
                _bigKeyDoors.Add(bigKeyDoor, KeyDoors[bigKeyDoor]);
            }
        }

        /// <summary>
        /// Creates all dungeon items and adds them to the list.
        /// </summary>
        private void PopulateDungeonItems()
        {
            foreach (var item in _dungeon.DungeonItems)
            {
                _items.Add(item, DungeonItems[item]);
            }
        }

        /// <summary>
        /// Creates all dungeon bosses and adds them to the list.
        /// </summary>
        private void PopulateBosses()
        {
            foreach (var boss in _dungeon.Bosses)
            {
                _bosses.Add(DungeonItems[boss]);
            }
        }

        /// <summary>
        /// Creates all small key drops and adds them to the list.
        /// </summary>
        private void PopulateSmallKeyDrops()
        {
            foreach (var smallKeyDrop in _dungeon.SmallKeyDrops)
            {
                _smallKeyDrops.Add(smallKeyDrop, DungeonItems[smallKeyDrop]);
            }
        }

        /// <summary>
        /// Creates all big key drops and adds them to the list.
        /// </summary>
        private void PopulateBigKeyDrops()
        {
            foreach (var bigKeyDrop in _dungeon.BigKeyDrops)
            {
                _bigKeyDrops.Add(bigKeyDrop, DungeonItems[bigKeyDrop]);
            }
        }

        /// <summary>
        /// Creates all dungeon nodes.
        /// </summary>
        private void PopulateNodes()
        {
            foreach (var node in _dungeon.Nodes)
            {
                _ = Nodes[node];
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

            foreach (var smallKeyDoor in _smallKeyDoors.Keys)
            {
                _smallKeyDoors[smallKeyDoor].Unlocked = unlockedDoors.Contains(smallKeyDoor);
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
            foreach (IKeyDoor bigKeyDoor in _bigKeyDoors.Values)
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
            if (_mode.KeyDropShuffle)
            {
                return 0;
            }

            int bigKeys = 0;

            foreach (var bigKeyDrop in _bigKeyDrops.Values)
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

        public void ApplyState(IDungeonState state)
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
            if (_mode.KeyDropShuffle)
            {
                return 0;
            }

            int smallKeys = 0;

            foreach (var smallKeyDrop in _smallKeyDrops.Values)
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

            foreach (var key in _smallKeyDoors.Keys)
            {
                var keyDoor = _smallKeyDoors[key];

                if (keyDoor.Unlocked)
                {
                    continue;
                }
                
                switch (keyDoor.Accessibility)
                {
                    case AccessibilityLevel.SequenceBreak when sequenceBreak:
                    case AccessibilityLevel.Normal:
                        accessibleKeyDoors.Add(key);
                        break;
                }
            }

            return accessibleKeyDoors;
        }

        /// <summary>
        /// Returns whether the specified number of collected keys and big key can occur,
        /// based on key logic.
        /// </summary>
        /// <param name="state">
        /// The dungeon state data.
        /// </param>
        /// <returns>
        /// A boolean representing whether the result can occur.
        /// </returns>
        public bool ValidateKeyLayout(IDungeonState state)
        {
            return _dungeon.KeyLayouts.Any(keyLayout => keyLayout.CanBeTrue(this, state));
        }

        /// <summary>
        /// Returns a list of the current accessibility of each boss in the dungeon.
        /// </summary>
        /// <returns>
        /// A list of the current accessibility of each boss in the dungeon.
        /// </returns>
        private List<AccessibilityLevel> GetBossAccessibility()
        {
            List<AccessibilityLevel> bossAccessibility = new();

            foreach (var bossItem in _bosses)
            {
                bossAccessibility.Add(bossItem.Accessibility);
            }

            return bossAccessibility;
        }

        /// <summary>
        /// Returns the current accessibility and accessible item count based on the specified
        /// number of small keys collected and whether the big key is collected.
        /// </summary>
        /// <param name="state">
        /// The dungeon state data.
        /// </param>
        /// <returns>
        /// A tuple of the accessibility of items in the dungeon and a 32-bit integer representing
        /// the number of accessible items in the dungeon.
        /// </returns>
        public IDungeonResult GetDungeonResult(IDungeonState state)
        {
            var inaccessibleBosses = 0;
            var inaccessibleItems = 0;
            var visible = false;

            foreach (var item in _items.Keys)
            {
                switch (_items[item].Accessibility)
                {
                    case AccessibilityLevel.None:
                    case AccessibilityLevel.SequenceBreak when !state.SequenceBreak:
                        if (_dungeon.Bosses.Contains(item))
                        {
                            inaccessibleBosses++;
                        }

                        inaccessibleItems++;
                        break;
                    case AccessibilityLevel.Inspect:
                        inaccessibleItems++;
                        visible = true;
                        break;
                }
            }

            if (_mode.KeyDropShuffle)
            {
                foreach (var item in _smallKeyDrops.Values)
                {
                    switch (item.Accessibility)
                    {
                        case AccessibilityLevel.None:
                        case AccessibilityLevel.SequenceBreak when !state.SequenceBreak:
                            inaccessibleItems++;
                            break;
                    }
                }

                foreach (var item in _bigKeyDrops.Values)
                {
                    switch (item.Accessibility)
                    {
                        case AccessibilityLevel.None:
                        case AccessibilityLevel.SequenceBreak when !state.SequenceBreak:
                            inaccessibleItems++;
                            break;
                    }
                }
            }

            var minimumInaccessible = _mode.GuaranteedBossItems ? inaccessibleBosses : 0;
            var bossAccessibility = GetBossAccessibility();

            var section = (IDungeonItemSection)_dungeon.Sections[0];
            
            if (inaccessibleItems <= minimumInaccessible)
            {
                return minimumInaccessible == 0 ? _resultFactory(bossAccessibility, section.Total, false) :
                    _resultFactory(bossAccessibility, section.Total - minimumInaccessible, visible);
            }

            if (!_mode.BigKeyShuffle && !state.BigKeyCollected)
            {
                var bigKey = _mode.KeyDropShuffle ? _dungeon.BigKey + _dungeon.BigKeyDrops.Count : _dungeon.BigKey;
                inaccessibleItems -= bigKey;
            }

            if (inaccessibleItems <= minimumInaccessible)
            {
                return minimumInaccessible == 0 ? _resultFactory(bossAccessibility, section.Total, false) :
                    _resultFactory(bossAccessibility, section.Total - minimumInaccessible, visible);
            }

            if (!_mode.SmallKeyShuffle)
            {
                var smallKeys = _mode.KeyDropShuffle ?
                    _dungeon.SmallKeys + _dungeon.SmallKeyDrops.Count : _dungeon.SmallKeys;
                inaccessibleItems -= smallKeys - state.KeysCollected;
            }

            if (inaccessibleItems <= minimumInaccessible)
            {
                return minimumInaccessible == 0 ? _resultFactory(bossAccessibility, section.Total, false) :
                    _resultFactory(bossAccessibility, section.Total - minimumInaccessible, visible);
            }

            if (!_mode.MapShuffle)
            {
                inaccessibleItems -= _dungeon.Map;
            }
            
            if (!_mode.CompassShuffle)
            {
                inaccessibleItems -= _dungeon.Compass;
            }
            
            inaccessibleItems = Math.Max(inaccessibleItems, minimumInaccessible);

            return inaccessibleItems <= 0 ? _resultFactory(bossAccessibility, section.Total, false) :
                _resultFactory(bossAccessibility, section.Total - minimumInaccessible, visible);
        }

        /// <summary>
        /// Resets the dungeon data for testing purposes.
        /// </summary>
        public void Reset()
        {
            foreach (var door in KeyDoors.Values)
            {
                door.Unlocked = false;
            }
        }
    }
}
