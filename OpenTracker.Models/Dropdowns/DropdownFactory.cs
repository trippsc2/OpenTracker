using System;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.Dropdowns
{
    /// <summary>
    ///     This class contains the creation logic for dropdowns.
    /// </summary>
    public class DropdownFactory : IDropdownFactory
    {
        private readonly IDropdown.Factory _factory;
        private readonly IRequirementDictionary _requirements;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="requirements">
        ///     The requirement dictionary.
        /// </param>
        /// <param name="factory">
        ///     The factory for creating new dropdowns.
        /// </param>
        public DropdownFactory(IRequirementDictionary requirements, IDropdown.Factory factory)
        {
            _requirements = requirements;
            _factory = factory;
        }

        /// <summary>
        ///     Returns the requirement for the specified dropdown to be relevant.
        /// </summary>
        /// <param name="id">
        ///     The dropdown identity.
        /// </param>
        /// <returns>
        ///     The requirement for the specified dropdown to be relevant.
        /// </returns>
        private IRequirement GetRequirement(DropdownID id)
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
                    return _requirements[RequirementType.EntranceShuffleAllInsanity];
                case DropdownID.SWNEHole:
                case DropdownID.SWNWHole:
                case DropdownID.SWSEHole:
                case DropdownID.SWSWHole:
                    return _requirements[RequirementType.EntranceShuffleInsanity];
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }

        public IDropdown GetDropdown(DropdownID id)
        {
            return _factory(GetRequirement(id));
        }
    }
}
