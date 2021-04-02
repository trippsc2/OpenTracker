using System;
using System.Collections.Generic;
using System.Linq;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.Nodes;
using OpenTracker.Models.Dungeons.Result;
using OpenTracker.Models.Dungeons.State;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.Modes;

namespace OpenTracker.Models.Dungeons.Mutable
{
    /// <summary>
    ///     This class contains the mutable dungeon data.
    /// </summary>
    public class MutableDungeon : IMutableDungeon
    {
        private readonly IMode _mode;
        private readonly IDungeon _dungeon;
        private readonly IDungeonResult.Factory _resultFactory;

        public IDungeonNodeDictionary Nodes { get; }
        public IKeyDoorDictionary KeyDoors { get; }
        public IDungeonItemDictionary DungeonItems { get; }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="mode">
        ///     The mode data.
        /// </param>
        /// <param name="keyDoors">
        ///     The key door dictionary.
        /// </param>
        /// <param name="nodes">
        ///     The dungeon node dictionary.
        /// </param>
        /// <param name="dungeonItems">
        ///     The dungeon item dictionary.
        /// </param>
        /// <param name="resultFactory">
        ///     An Autofac factory for creating dungeon results.
        /// </param>
        /// <param name="dungeon">
        ///     The dungeon immutable data.
        /// </param>
        public MutableDungeon(
            IMode mode, IKeyDoorDictionary.Factory keyDoors, IDungeonNodeDictionary.Factory nodes,
            IDungeonItemDictionary.Factory dungeonItems, IDungeonResult.Factory resultFactory, IDungeon dungeon)
        {
            _mode = mode;
            _dungeon = dungeon;
            _resultFactory = resultFactory;

            Nodes = nodes(this);
            KeyDoors = keyDoors(this);
            DungeonItems = dungeonItems(this);
        }

        public void InitializeData()
        {
            Nodes.PopulateNodes(_dungeon.Nodes);
            
            KeyDoors.PopulateDoors(_dungeon.SmallKeyDoors);
            KeyDoors.PopulateDoors(_dungeon.BigKeyDoors);
            
            DungeonItems.PopulateItems(_dungeon.DungeonItems);
            DungeonItems.PopulateItems(_dungeon.Bosses);
            DungeonItems.PopulateItems(_dungeon.SmallKeyDrops);
            DungeonItems.PopulateItems(_dungeon.BigKeyDrops);
        }

        public void ApplyState(IDungeonState state)
        {
            SetSmallKeyDoorState(state.UnlockedDoors);

            var bigKeyCollected = state.BigKeyCollected || GetAvailableBigKey(state.SequenceBreak);

            SetBigKeyDoorState(bigKeyCollected);
        }

        /// <summary>
        ///     Sets the state of all small key doors based on a specified list of unlocked doors.
        /// </summary>
        /// <param name="unlockedDoors">
        ///     A list of unlocked doors.
        /// </param>
        private void SetSmallKeyDoorState(IList<KeyDoorID> unlockedDoors)
        {
            foreach (var id in _dungeon.SmallKeyDoors)
            {
                KeyDoors[id].Unlocked = unlockedDoors.Contains(id);
            }
        }

        /// <summary>
        ///     Returns whether the big key is available to be collected in the dungeon.
        /// </summary>
        /// <param name="sequenceBreak">
        ///     A boolean representing whether sequence breaking is allowed.
        /// </param>
        /// <returns>
        ///     A boolean representing whether the big key is available to be collected.
        /// </returns>
        private bool GetAvailableBigKey(bool sequenceBreak = false)
        {
            if (_mode.KeyDropShuffle)
            {
                return false;
            }

            foreach (var id in _dungeon.BigKeyDrops)
            {
                switch (DungeonItems[id].Accessibility)
                {
                    case AccessibilityLevel.Normal:
                    case AccessibilityLevel.SequenceBreak when sequenceBreak:
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     Sets the state of all big key doors to a specified state.
        /// </summary>
        /// <param name="unlocked">
        ///     A boolean representing whether the doors are to be unlocked.
        /// </param>
        private void SetBigKeyDoorState(bool unlocked)
        {
            foreach (var id in _dungeon.BigKeyDoors)
            {
                KeyDoors[id].Unlocked = unlocked;
            }
        }

        public int GetAvailableSmallKeys(bool sequenceBreak = false)
        {
            if (_mode.KeyDropShuffle)
            {
                return 0;
            }

            var smallKeys = 0;

            foreach (var id in _dungeon.SmallKeyDrops)
            {
                var smallKeyDrop = DungeonItems[id];
                
                switch (smallKeyDrop.Accessibility)
                {
                    case AccessibilityLevel.Normal:
                    case AccessibilityLevel.SequenceBreak when sequenceBreak:
                        smallKeys++;
                        continue;
                }
            }

            return smallKeys;
        }

        public IList<KeyDoorID> GetAccessibleKeyDoors(bool sequenceBreak = false)
        {
            var accessibleKeyDoors = new List<KeyDoorID>();

            foreach (var id in _dungeon.SmallKeyDoors)
            {
                var keyDoor = KeyDoors[id];

                if (keyDoor.Unlocked)
                {
                    continue;
                }
                
                switch (keyDoor.Accessibility)
                {
                    case AccessibilityLevel.SequenceBreak when sequenceBreak:
                    case AccessibilityLevel.Normal:
                        accessibleKeyDoors.Add(id);
                        break;
                }
            }

            return accessibleKeyDoors;
        }

        public bool ValidateKeyLayout(IDungeonState state)
        {
            return _dungeon.KeyLayouts.Any(keyLayout => keyLayout.CanBeTrue(this, state));
        }

        public IDungeonResult GetDungeonResult(IDungeonState state)
        {
            var inaccessibleBossItems = 0;
            var inaccessibleItems = 0;
            var visible = false;

            CheckItemAccessibility(
                state, _dungeon.DungeonItems, ref inaccessibleBossItems, ref inaccessibleItems, ref visible);

            if (_mode.KeyDropShuffle)
            {
                CheckItemAccessibility(
                    state, _dungeon.SmallKeyDrops, ref inaccessibleBossItems, ref inaccessibleItems, ref visible);
                CheckItemAccessibility(
                    state, _dungeon.BigKeyDrops, ref inaccessibleBossItems, ref inaccessibleItems, ref visible);
            }

            var bossAccessibility = GetBossAccessibility();

            if (!_mode.BigKeyShuffle && !state.BigKeyCollected && _dungeon.BigKey is not null)
            {
                inaccessibleItems -= _dungeon.BigKey.Maximum;
            }

            if (!_mode.SmallKeyShuffle)
            {
                var smallKeys = _dungeon.SmallKey.Maximum;
                inaccessibleItems -= smallKeys - state.KeysCollected;
            }

            var total = _dungeon.TotalWithMapAndCompass;
            inaccessibleItems = Math.Max(0, inaccessibleItems);
            var minimumInaccessible = _mode.GuaranteedBossItems ? inaccessibleBossItems : 0;

            return _resultFactory(
                bossAccessibility, total - inaccessibleItems, state.SequenceBreak, visible,
                minimumInaccessible);
        }

        /// <summary>
        ///     Returns a list of the current accessibility of each boss in the dungeon.
        /// </summary>
        /// <returns>
        ///     A list of the current accessibility of each boss in the dungeon.
        /// </returns>
        private List<AccessibilityLevel> GetBossAccessibility()
        {
            return _dungeon.Bosses.Select(id => DungeonItems[id].Accessibility).ToList();
        }

        /// <summary>
        ///     Check the accessibility of a specified list of item IDs.
        /// </summary>
        /// <param name="state">
        ///     The dungeon state to be checked.
        /// </param>
        /// <param name="items">
        ///     A list of item IDs to be checked.
        /// </param>
        /// <param name="inaccessibleBossItems">
        ///     A 32-bit signed integer representing the number of inaccessible bosses.
        /// </param>
        /// <param name="inaccessibleItems">
        ///     A 32-bit signed integer representing the number of inaccessible items.
        /// </param>
        /// <param name="visible">
        ///     A boolean representing whether the last inaccessible item is visible.
        /// </param>
        private void CheckItemAccessibility(
            IDungeonState state, IList<DungeonItemID> items, ref int inaccessibleBossItems, ref int inaccessibleItems, ref bool visible)
        {
            foreach (var id in items)
            {
                switch (DungeonItems[id].Accessibility)
                {
                    case AccessibilityLevel.None:
                    case AccessibilityLevel.SequenceBreak when !state.SequenceBreak:
                        if (_dungeon.Bosses.Contains(id))
                        {
                            inaccessibleBossItems++;
                        }

                        inaccessibleItems++;
                        break;
                    case AccessibilityLevel.Inspect:
                        inaccessibleItems++;
                        visible = true;
                        break;
                }
            }
        }
    }
}
