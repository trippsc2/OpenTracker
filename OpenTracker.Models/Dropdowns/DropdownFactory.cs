using OpenTracker.Models.Requirements;
using System;

namespace OpenTracker.Models.Dropdowns
{
    /// <summary>
    /// This is the class contianing the creation logic for dropdowns.
    /// </summary>
    public static class DropdownFactory
    {
        /// <summary>
        /// Returns the requirement for the specified dropdown to be relevant.
        /// </summary>
        /// <param name="id">
        /// The dropdown identity.
        /// </param>
        /// <returns>
        /// The requirement for the specified dropdown to be relevant.
        /// </returns>
        private static IRequirement GetRequirement(DropdownID id)
        {
            switch (id)
            {
                case DropdownID.LumberjackCave:
                case DropdownID.ForestHideout:
                case DropdownID.CastleSecret:
                case DropdownID.TheWell:
                case DropdownID.MagicBat:
                case DropdownID.SanctuaryGrave:
                case DropdownID.HoulihanHole:
                case DropdownID.GanonHole:
                    {
                        return RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity];
                    }
                case DropdownID.SWNEHole:
                case DropdownID.SWNWHole:
                case DropdownID.SWSEHole:
                case DropdownID.SWSWHole:
                    {
                        return RequirementDictionary.Instance[RequirementType.EntranceShuffleInsanity];
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }

        /// <summary>
        /// Returns a new dropdown instance for the specified ID.
        /// </summary>
        /// <param name="id">
        /// The dropdown ID.
        /// </param>
        /// <returns>
        /// A new dropdown instance for the specified ID.
        /// </returns>
        public static IDropdown GetDropdown(DropdownID id)
        {
            return new Dropdown(GetRequirement(id));
        }
    }
}
