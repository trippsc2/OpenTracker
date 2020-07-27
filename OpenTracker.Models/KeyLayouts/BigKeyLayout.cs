using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.DungeonItems;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Requirements;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.KeyLayouts
{
    /// <summary>
    /// This is the class containing the big key layout.
    /// </summary>
    public class BigKeyLayout : IKeyLayout
    {
        private readonly List<DungeonItemID> _bigKeyLocations;
        private readonly List<IKeyLayout> _children;
        private readonly IRequirement _requirement;

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
            List<DungeonItemID> bigKeyLocations, List<IKeyLayout> children,
            IRequirement requirement = null)
        {
            _bigKeyLocations = bigKeyLocations ?? throw new ArgumentNullException(nameof(bigKeyLocations));
            _children = children ?? throw new ArgumentNullException(nameof(children));
            _requirement = requirement ?? RequirementDictionary.Instance[RequirementType.None];
        }

        /// <summary>
        /// Returns whether the key layout is possible in the current game state.
        /// </summary>
        /// <param name="dungeonData">
        /// The dungeon mutable data.
        /// </param>
        /// <param name="smallKeys">
        /// A 32-bit signed integer representing the number of small keys collected.
        /// </param>
        /// <param name="bigKey">
        /// A boolean representing whether the big key was collected.
        /// </param>
        /// <returns>
        /// A boolean representing whether the key layout is possible.
        /// </returns>
        public bool CanBeTrue(IMutableDungeon dungeonData, int smallKeys, bool bigKey)
        {
            if (dungeonData == null)
            {
                throw new ArgumentNullException(nameof(dungeonData));
            }

            if (!_requirement.Met)
            {
                return false;
            }

            int accessible = 0;
            int inaccessible = 0;

            foreach (var item in _bigKeyLocations)
            {
                if (dungeonData.Items[item].Accessibility > AccessibilityLevel.Partial)
                {
                    accessible++;
                }
                else
                {
                    inaccessible++;
                }
            }

            if (bigKey && accessible == 0)
            {
                return false;
            }

            if (!bigKey && inaccessible == 0)
            {
                return false;
            }

            foreach (var child in _children)
            {
                if (child.CanBeTrue(dungeonData, smallKeys, bigKey))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
