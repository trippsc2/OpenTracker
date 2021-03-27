using System;
using System.Collections.Generic;
using System.Linq;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.DungeonItems;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.KeyLayouts
{
    /// <summary>
    /// This class contains small key layout data.
    /// </summary>
    public class SmallKeyLayout : IKeyLayout
    {
        private readonly IMode _mode;
        private readonly int _count;
        private readonly List<DungeonItemID> _smallKeyLocations;
        private readonly bool _bigKeyInLocations;
        private readonly List<IKeyLayout> _children;
        private readonly IRequirement _requirement;
        private readonly IDungeon _dungeon;

        public delegate SmallKeyLayout Factory(
            int count, List<DungeonItemID> smallKeyLocations, bool bigKeyInLocations, List<IKeyLayout> children,
            IDungeon dungeon, IRequirement requirement);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode data settings.
        /// </param>
        /// <param name="count">
        /// A 32-bit signed integer representing the number of keys that must be contained in the
        /// list of locations.
        /// </param>
        /// <param name="smallKeyLocations">
        /// The list of dungeon item IDs that the number of small keys must be contained in.
        /// </param>
        /// <param name="bigKeyInLocations">
        /// AS boolean representing whether the big key is contained in the list of locations.
        /// </param>
        /// <param name="children">
        /// The list of child key layouts, if this layout is possible.
        /// </param>
        /// <param name="dungeon">
        /// The dungeon parent class.
        /// </param>
        /// <param name="requirement">
        /// The requirement for this key layout to be valid.
        /// </param>
        public SmallKeyLayout(
            IMode mode, int count, List<DungeonItemID> smallKeyLocations, bool bigKeyInLocations,
            List<IKeyLayout> children, IDungeon dungeon, IRequirement requirement)
        {
            _mode = mode;
            _count = count;
            _smallKeyLocations = smallKeyLocations;
            _bigKeyInLocations = bigKeyInLocations;
            _children = children;
            _dungeon = dungeon;
            _requirement = requirement;
        }

        /// <summary>
        /// Returns whether the key layout satisfies the minimum number of keys collected.
        /// </summary>
        /// <param name="collectedKeys">
        /// A 32-bit signed integer representing the number of small keys collected.
        /// </param>
        /// <param name="inaccessible">
        /// A 32-bit signed integer representing the number of inaccessible locations.
        /// </param>
        /// <returns>
        /// A boolean representing whether the key layout is possible.
        /// </returns>
        private bool ValidateMinimumKeyCount(int collectedKeys, int inaccessible)
        {
            return collectedKeys >= Math.Max(0, _count - inaccessible);
        }

        /// <summary>
        /// Returns whether the key layout satisfies the maximum number of keys collected.
        /// </summary>
        /// <param name="dungeonKeys">
        /// The dungeon small key total.
        /// </param>
        /// <param name="collectedKeys">
        /// A 32-bit signed integer representing the number of small keys collected.
        /// </param>
        /// <param name="inaccessible">
        /// A 32-bit signed integer representing the number of inaccessible locations.
        /// </param>
        /// <returns>
        /// A boolean representing whether the key layout is possible.
        /// </returns>
        private bool ValidateMaximumKeyCount(int dungeonKeys, int collectedKeys, int inaccessible)
        {
            return collectedKeys <= dungeonKeys - Math.Max(0, inaccessible -
                (_smallKeyLocations.Count - _count));
        }

        /// <summary>
        /// Returns whether the key layout is possible in the current game state.
        /// </summary>
        /// <param name="dungeonData">
        /// The dungeon mutable data.
        /// </param>
        /// <param name="state">
        /// The dungeon state data.
        /// </param>
        /// <returns>
        /// A boolean representing whether the key layout is possible.
        /// </returns>
        public bool CanBeTrue(IMutableDungeon dungeonData, IDungeonState state)
        {
            if (dungeonData == null)
            {
                throw new ArgumentNullException(nameof(dungeonData));
            }

            if (!_requirement.Met)
            {
                return false;
            }

            var inaccessible = 0;

            foreach (var item in _smallKeyLocations)
            {
                switch (dungeonData.DungeonItems[item].Accessibility)
                {
                    case AccessibilityLevel.Normal:
                    case AccessibilityLevel.SequenceBreak when state.SequenceBreak:
                        break;
                    default:
                        inaccessible++;
                        break;
                }
            }

            if (_bigKeyInLocations && !state.BigKeyCollected)
            {
                inaccessible--;
            }

            var dungeonSmallKeys = _mode.KeyDropShuffle ? _dungeon.SmallKeys + _dungeon.SmallKeyDrops.Count :
                _dungeon.SmallKeys;

            return ValidateMinimumKeyCount(state.KeysCollected, inaccessible) &&
                   ValidateMaximumKeyCount(dungeonSmallKeys, state.KeysCollected, inaccessible) &&
                   _children.Any(child => child.CanBeTrue(dungeonData, state));
        }
    }
}
