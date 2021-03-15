using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.DungeonItems;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Requirements;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenTracker.Models.KeyLayouts
{
    /// <summary>
    /// This class contains the big key layout data.
    /// </summary>
    public class BigKeyLayout : IKeyLayout
    {
        private readonly List<DungeonItemID> _bigKeyLocations;
        private readonly List<IKeyLayout> _children;
        private readonly IRequirement _requirement;

        public delegate BigKeyLayout Factory(
            List<DungeonItemID> bigKeyLocations, List<IKeyLayout> children, IRequirement requirement);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bigKeyLocations">
        /// The list of dungeon item IDs that can contain the big key.
        /// </param>
        /// <param name="children">
        /// The list of child key layouts, if this layout is possible.
        /// </param>
        /// <param name="requirement">
        /// The requirement for this key layout to be valid.
        /// </param>
        public BigKeyLayout(
            List<DungeonItemID> bigKeyLocations, List<IKeyLayout> children, IRequirement requirement)
        {
            _bigKeyLocations = bigKeyLocations;
            _children = children;
            _requirement = requirement;
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

            var accessible = 0;
            var inaccessible = 0;

            foreach (var item in _bigKeyLocations)
            {
                switch (dungeonData.DungeonItems[item].Accessibility)
                {
                    case AccessibilityLevel.Normal:
                    case AccessibilityLevel.SequenceBreak when state.SequenceBreak:
                        accessible++;
                        break;
                    default:
                        inaccessible++;
                        break;
                }
            }

            switch (state.BigKeyCollected)
            {
                case true when accessible == 0:
                case false when inaccessible == 0:
                    return false;
            }

            return _children.Any(child => child.CanBeTrue(dungeonData, state));
        }
    }
}
