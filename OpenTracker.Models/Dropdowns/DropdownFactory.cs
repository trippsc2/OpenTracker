using System;
using System.Collections.Generic;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Aggregate;
using OpenTracker.Models.Requirements.Mode;

namespace OpenTracker.Models.Dropdowns
{
    /// <summary>
    /// This class contains the creation logic for <see cref="IDropdown"/> objects.
    /// </summary>
    public class DropdownFactory : IDropdownFactory
    {
        private readonly IAggregateRequirementDictionary _aggregateRequirements;
        private readonly IEntranceShuffleRequirementDictionary _entranceShuffleRequirements;
        
        private readonly IDropdown.Factory _factory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="aggregateRequirements">
        ///     The <see cref="IAggregateRequirementDictionary"/>.
        /// </param>
        /// <param name="entranceShuffleRequirements">
        ///     The <see cref="IEntranceShuffleRequirementDictionary"/>.
        /// </param>
        /// <param name="factory">
        ///     An Autofac factory for creating new <see cref="IDropdown"/> objects.
        /// </param>
        public DropdownFactory(
            IAggregateRequirementDictionary aggregateRequirements,
            IEntranceShuffleRequirementDictionary entranceShuffleRequirements, IDropdown.Factory factory)
        {
            _factory = factory;
            _aggregateRequirements = aggregateRequirements;
            _entranceShuffleRequirements = entranceShuffleRequirements;
        }

        public IDropdown GetDropdown(DropdownID id)
        {
            return _factory(GetRequirement(id));
        }

        /// <summary>
        /// Returns the <see cref="IRequirement"/> for the specified <see cref="DropdownID"/> to be relevant.
        /// </summary>
        /// <param name="id">
        ///     The <see cref="DropdownID"/>.
        /// </param>
        /// <returns>
        ///     The <see cref="IRequirement"/> for the specified dropdown to be relevant.
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
    }
}
