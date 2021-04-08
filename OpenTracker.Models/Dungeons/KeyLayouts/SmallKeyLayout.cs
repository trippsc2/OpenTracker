using System;
using System.Collections.Generic;
using System.Linq;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.State;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.Dungeons.KeyLayouts
{
    /// <summary>
    ///     This class contains small key layout data.
    /// </summary>
    public class SmallKeyLayout : ISmallKeyLayout
    {
        private readonly int _count;
        private readonly IList<DungeonItemID> _smallKeyLocations;
        private readonly bool _bigKeyInLocations;
        private readonly IList<IKeyLayout> _children;
        private readonly IRequirement _requirement;
        private readonly IDungeon _dungeon;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="count">
        ///     A 32-bit signed integer representing the number of keys that must be contained in the
        ///         list of locations.
        /// </param>
        /// <param name="smallKeyLocations">
        ///     The list of dungeon item IDs that the number of small keys must be contained in.
        /// </param>
        /// <param name="bigKeyInLocations">
        ///     A boolean representing whether the big key is contained in the list of locations.
        /// </param>
        /// <param name="children">
        ///     The list of child key layouts, if this layout is possible.
        /// </param>
        /// <param name="dungeon">
        ///     The dungeon parent class.
        /// </param>
        /// <param name="requirement">
        ///     The requirement for this key layout to be valid.
        /// </param>
        public SmallKeyLayout(
            int count, IList<DungeonItemID> smallKeyLocations, bool bigKeyInLocations, IList<IKeyLayout> children,
            IDungeon dungeon, IRequirement requirement)
        {
            _count = count;
            _smallKeyLocations = smallKeyLocations;
            _bigKeyInLocations = bigKeyInLocations;
            _children = children;
            _dungeon = dungeon;
            _requirement = requirement;
        }

        public bool CanBeTrue(IList<DungeonItemID> inaccessible, IList<DungeonItemID> accessible, IDungeonState state)
        {
            if (!_requirement.Met)
            {
                return false;
            }

            var inaccessibleCount = _smallKeyLocations.Count(inaccessible.Contains);

            if (_bigKeyInLocations && !state.BigKeyCollected)
            {
                inaccessibleCount--;
            }

            var dungeonSmallKeys = _dungeon.SmallKey.Maximum;

            return ValidateMinimumKeyCount(state.KeysCollected, inaccessibleCount) &&
                   ValidateMaximumKeyCount(dungeonSmallKeys, state.KeysCollected, inaccessibleCount) &&
                   _children.Any(child => child.CanBeTrue(inaccessible, accessible, state));
        }

        /// <summary>
        ///     Returns whether the key layout satisfies the minimum number of keys collected.
        /// </summary>
        /// <param name="collectedKeys">
        ///     A 32-bit signed integer representing the number of small keys collected.
        /// </param>
        /// <param name="inaccessible">
        ///     A 32-bit signed integer representing the number of inaccessible locations.
        /// </param>
        /// <returns>
        ///     A boolean representing whether the key layout is possible.
        /// </returns>
        private bool ValidateMinimumKeyCount(int collectedKeys, int inaccessible)
        {
            return collectedKeys >= Math.Max(0, _count - inaccessible);
        }

        /// <summary>
        ///     Returns whether the key layout satisfies the maximum number of keys collected.
        /// </summary>
        /// <param name="dungeonKeys">
        ///     The dungeon small key total.
        /// </param>
        /// <param name="collectedKeys">
        ///     A 32-bit signed integer representing the number of small keys collected.
        /// </param>
        /// <param name="inaccessible">
        ///     A 32-bit signed integer representing the number of inaccessible locations.
        /// </param>
        /// <returns>
        ///     A boolean representing whether the key layout is possible.
        /// </returns>
        private bool ValidateMaximumKeyCount(int dungeonKeys, int collectedKeys, int inaccessible)
        {
            return collectedKeys <= dungeonKeys - Math.Max(0, inaccessible - (_smallKeyLocations.Count - _count));
        }
    }
}
