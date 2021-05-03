using System;
using System.Collections.Generic;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Aggregate;
using OpenTracker.Models.Requirements.Mode;

namespace OpenTracker.Models.Dropdowns
{
    /// <summary>
    ///     This class contains the creation logic for dropdowns.
    /// </summary>
    public class DropdownFactory : IDropdownFactory
    {
        private readonly IAggregateRequirementDictionary _aggregateRequirements;
        private readonly IEntranceShuffleRequirementDictionary _entranceShuffleRequirements;
        
        private readonly IDropdown.Factory _factory;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="aggregateRequirements">
        ///     The aggregate requirement dictionary.
        /// </param>
        /// <param name="entranceShuffleRequirements">
        ///     The entrance shuffle requirement dictionary.
        /// </param>
        /// <param name="factory">
        ///     The factory for creating new dropdowns.
        /// </param>
        public DropdownFactory(
            IAggregateRequirementDictionary aggregateRequirements,
            IEntranceShuffleRequirementDictionary entranceShuffleRequirements, IDropdown.Factory factory)
        {
            _factory = factory;
            _aggregateRequirements = aggregateRequirements;
            _entranceShuffleRequirements = entranceShuffleRequirements;
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
                    return _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _entranceShuffleRequirements[EntranceShuffle.All],
                        _entranceShuffleRequirements[EntranceShuffle.Insanity]
                    }];
                case DropdownID.SWNEHole:
                case DropdownID.SWNWHole:
                case DropdownID.SWSEHole:
                case DropdownID.SWSWHole:
                    return _entranceShuffleRequirements[EntranceShuffle.Insanity];
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }

        public IDropdown GetDropdown(DropdownID id)
        {
            return _factory(GetRequirement(id));
        }
    }
}
