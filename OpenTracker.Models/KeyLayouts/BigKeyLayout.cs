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
            _requirement = requirement ?? RequirementDictionary.Instance[RequirementType.NoRequirement];
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
        public ValidationStatus CanBeTrue(IMutableDungeon dungeonData, int smallKeys, bool bigKey)
        {
            if (dungeonData == null)
            {
                throw new ArgumentNullException(nameof(dungeonData));
            }

            if (!_requirement.Met)
            {
                return ValidationStatus.Invalid;
            }

            int normal = 0;
            int sequenceBreak = 0;
            int inaccessible = 0;

            foreach (var item in _bigKeyLocations)
            {
                switch (dungeonData.ItemDictionary[item].Accessibility)
                {
                    case AccessibilityLevel.SequenceBreak:
                        {
                            sequenceBreak++;
                        }
                        break;
                    case AccessibilityLevel.Normal:
                        {
                            normal++;
                        }
                        break;
                    default:
                        {
                            inaccessible++;
                        }
                        break;
                }
            }

            bool validWithoutSequenceBreak = true;
            bool validWithSequenceBreak = true;

            switch (bigKey)
            {
                case true:
                    {
                        if (normal == 0)
                        {
                            if (sequenceBreak == 0)
                            {
                                return ValidationStatus.Invalid;
                            }
                            else
                            {
                                validWithoutSequenceBreak = false;
                            }
                        }
                    }
                    break;
                case false:
                    {
                        if (inaccessible == 0)
                        {
                            if (sequenceBreak == 0)
                            {
                                return ValidationStatus.Invalid;
                            }
                            else
                            {
                                validWithSequenceBreak = false;
                            }
                        }
                    }
                    break;
            }

            ValidationStatus maximumResult =
                (validWithSequenceBreak ? ValidationStatus.ValidWithSeqenceBreak :
                ValidationStatus.Invalid) | (validWithoutSequenceBreak ?
                ValidationStatus.ValidWithoutSequenceBreak : ValidationStatus.Invalid);

            ValidationStatus result = ValidationStatus.Invalid;

            foreach (var child in _children)
            {
                var childResult = child.CanBeTrue(dungeonData, smallKeys, bigKey);
                result |= childResult;

                if ((maximumResult & result) == maximumResult)
                {
                    return maximumResult;
                }
            }

            return result;
        }
    }
}
