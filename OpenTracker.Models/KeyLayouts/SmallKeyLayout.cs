using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.DungeonItems;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.KeyLayouts
{
    /// <summary>
    /// This is the class containing the small key layout.
    /// </summary>
    public class SmallKeyLayout : IKeyLayout
    {
        private readonly int _count;
        private readonly List<DungeonItemID> _smallKeyLocations;
        private readonly bool _bigKeyInLocations;
        private readonly List<IKeyLayout> _children;
        private readonly IRequirement _requirement;
        private readonly IDungeon _dungeon;

        /// <summary>
        /// Constructor
        /// </summary>
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
            int count, List<DungeonItemID> smallKeyLocations, bool bigKeyInLocations,
            List<IKeyLayout> children, IDungeon dungeon, IRequirement requirement = null)
        {
            _count = count;
            _smallKeyLocations = smallKeyLocations ??
                throw new ArgumentNullException(nameof(smallKeyLocations));
            _bigKeyInLocations = bigKeyInLocations;
            _children = children ?? throw new ArgumentNullException(nameof(children));
            _dungeon = dungeon ?? throw new ArgumentNullException(nameof(dungeon));
            _requirement = requirement ?? RequirementDictionary.Instance[RequirementType.NoRequirement];
        }

        /// <summary>
        /// Returns whether the key layout satisfies the minimum number of keys collected.
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
        private ValidationStatus ValidateMinimumKeyCount(int collectedKeys, int inaccessible, int sequenceBreak)
        {
            bool withSequenceBreak = collectedKeys >= Math.Max(0, _count - inaccessible);
            bool withoutSequenceBreak = collectedKeys >= Math.Max(0, _count - inaccessible - sequenceBreak);

            return (withSequenceBreak ? ValidationStatus.ValidWithSeqenceBreak : ValidationStatus.Invalid) |
                (withoutSequenceBreak ? ValidationStatus.ValidWithoutSequenceBreak : ValidationStatus.Invalid);
        }

        /// <summary>
        /// Returns whether the key layout satisfies the maximum number of keys collected.
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
        private ValidationStatus ValidateMaximumKeyCount(int dungeonKeys, int collectedKeys, int inaccessible, int sequenceBreak)
        {
            bool withSequenceBreak = collectedKeys <= dungeonKeys - Math.Max(0, inaccessible -
                (_smallKeyLocations.Count - _count));
            bool withoutSequenceBreak = collectedKeys <= dungeonKeys - Math.Max(0, inaccessible + sequenceBreak -
                (_smallKeyLocations.Count - _count));

            return (withSequenceBreak ? ValidationStatus.ValidWithSeqenceBreak : ValidationStatus.Invalid) |
                (withoutSequenceBreak ? ValidationStatus.ValidWithoutSequenceBreak : ValidationStatus.Invalid);
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

            int sequenceBreak = 0;
            int inaccessible = 0;

            foreach (var item in _smallKeyLocations)
            {
                switch (dungeonData.ItemDictionary[item].Accessibility)
                {
                    case AccessibilityLevel.SequenceBreak:
                        {
                            sequenceBreak++;
                        }
                        break;
                    case AccessibilityLevel.Normal:
                        break;
                    default:
                        {
                            inaccessible++;
                        }
                        break;
                }
            }

            if (_bigKeyInLocations && !bigKey)
            {
                inaccessible--;
            }

            int dungeonSmallKeys = Mode.Instance.KeyDropShuffle ?
                _dungeon.SmallKeys + _dungeon.SmallKeyDrops.Count : _dungeon.SmallKeys;

            ValidationStatus maximumResult = ValidationStatus.ValidWithSeqenceBreak |
                ValidationStatus.ValidWithoutSequenceBreak;

            var minimumKeyCountStatus = ValidateMinimumKeyCount(smallKeys, inaccessible, sequenceBreak);

            if (minimumKeyCountStatus == ValidationStatus.Invalid)
            {
                return ValidationStatus.Invalid;
            }

            maximumResult &= minimumKeyCountStatus;

            var maximumKeyCountStatus = ValidateMaximumKeyCount(dungeonSmallKeys, smallKeys, inaccessible, sequenceBreak);

            if (maximumKeyCountStatus == ValidationStatus.Invalid)
            {
                return ValidationStatus.Invalid;
            }

            maximumResult &= maximumKeyCountStatus;

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
