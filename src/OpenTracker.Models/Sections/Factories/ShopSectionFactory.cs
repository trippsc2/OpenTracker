using System;
using System.Collections.Generic;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Aggregate;
using OpenTracker.Models.Requirements.Alternative;
using OpenTracker.Models.Requirements.Mode;
using OpenTracker.Models.Requirements.ShopShuffle;
using OpenTracker.Models.Requirements.TakeAnyLocations;
using OpenTracker.Models.Sections.Boolean;

namespace OpenTracker.Models.Sections.Factories
{
    /// <summary>
    /// This class contains the creation logic for <see cref="IShopSection"/> objects.
    /// </summary>
    public class ShopSectionFactory : IShopSectionFactory
    {
        private readonly IAggregateRequirementDictionary _aggregateRequirements;
        private readonly IAlternativeRequirementDictionary _alternativeRequirements;
        private readonly IEntranceShuffleRequirementDictionary _entranceShuffleRequirements;
        private readonly IShopShuffleRequirementDictionary _shopShuffleRequirements;
        private readonly ITakeAnyLocationsRequirementDictionary _takeAnyLocationsRequirements;

        private readonly IOverworldNodeDictionary _overworldNodes;

        private readonly IShopSection.Factory _factory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="aggregateRequirements">
        ///     The <see cref="IAggregateRequirementDictionary"/>.
        /// </param>
        /// <param name="alternativeRequirements">
        ///     The <see cref="IAlternativeRequirementDictionary"/>.
        /// </param>
        /// <param name="entranceShuffleRequirements">
        ///     The <see cref="IEntranceShuffleRequirementDictionary"/>.
        /// </param>
        /// <param name="shopShuffleRequirements">
        ///     The <see cref="IShopShuffleRequirementDictionary"/>.
        /// </param>
        /// <param name="takeAnyLocationsRequirements">
        ///     The <see cref="ITakeAnyLocationsRequirementDictionary"/>.
        /// </param>
        /// <param name="overworldNodes">
        ///     The <see cref="IOverworldNodeDictionary"/>.
        /// </param>
        /// <param name="factory">
        ///     An Autofac factory for creating new <see cref="IShopSection"/> objects.
        /// </param>
        public ShopSectionFactory(
            IAggregateRequirementDictionary aggregateRequirements,
            IAlternativeRequirementDictionary alternativeRequirements,
            IEntranceShuffleRequirementDictionary entranceShuffleRequirements,
            IShopShuffleRequirementDictionary shopShuffleRequirements,
            ITakeAnyLocationsRequirementDictionary takeAnyLocationsRequirements,
            IOverworldNodeDictionary overworldNodes, IShopSection.Factory factory)
        {
            _aggregateRequirements = aggregateRequirements;
            _alternativeRequirements = alternativeRequirements;
            _entranceShuffleRequirements = entranceShuffleRequirements;
            _shopShuffleRequirements = shopShuffleRequirements;
            _takeAnyLocationsRequirements = takeAnyLocationsRequirements;
            
            _overworldNodes = overworldNodes;
            
            _factory = factory;
        }

        public IShopSection GetShopSection(LocationID id)
        {
            return _factory(GetNode(id), GetRequirement(id));
        }

        /// <summary>
        /// Returns the <see cref="INode"/> to which the section belongs for the specified <see cref="LocationID"/>.
        /// </summary>
        /// <param name="id">
        ///     The <see cref="LocationID"/>.
        /// </param>
        /// <returns>
        ///     The <see cref="INode"/> to which the section belongs.
        /// </returns>
        private INode GetNode(LocationID id)
        {
            switch (id)
            {
                case LocationID.KakarikoShop:
                case LocationID.LakeHyliaShop:
                    return _overworldNodes[OverworldNodeID.LightWorld];
                case LocationID.DeathMountainShop:
                    return _overworldNodes[OverworldNodeID.DeathMountainEastBottom];
                case LocationID.PotionShop:
                    return _overworldNodes[OverworldNodeID.LWWitchArea];
                case LocationID.DarkLumberjackShop:
                case LocationID.RedShieldShop:
                    return _overworldNodes[OverworldNodeID.DarkWorldWest];
                case LocationID.VillageOfOutcastsShop:
                    return _overworldNodes[OverworldNodeID.HammerHouse];
                case LocationID.DarkLakeHyliaShop:
                    return _overworldNodes[OverworldNodeID.DarkWorldSouth];
                case LocationID.DarkPotionShop:
                    return _overworldNodes[OverworldNodeID.DWWitchArea];
                case LocationID.DarkDeathMountainShop:
                    return _overworldNodes[OverworldNodeID.DarkDeathMountainEastBottom];
                default:
                    throw new ArgumentOutOfRangeException(nameof(id), id, null);
            }
        }

        /// <summary>
        /// Returns the <see cref="IRequirement"/> for the section to be active for the specified
        /// <see cref="LocationID"/>.
        /// </summary>
        /// <param name="id">
        ///     The <see cref="LocationID"/>.
        /// </param>
        /// <returns>
        ///     The <see cref="IRequirement"/> for the section to be active.
        /// </returns>
        private IRequirement GetRequirement(LocationID id)
        {
            if (id == LocationID.PotionShop)
            {
                return _aggregateRequirements[new HashSet<IRequirement>
                {
                    _alternativeRequirements[new HashSet<IRequirement>
                    {
                        _entranceShuffleRequirements[EntranceShuffle.None],
                        _entranceShuffleRequirements[EntranceShuffle.Dungeon]
                    }],
                    _shopShuffleRequirements[true]
                }];
            }

            return _aggregateRequirements[new HashSet<IRequirement>
            {
                _alternativeRequirements[new HashSet<IRequirement>
                {
                    _entranceShuffleRequirements[EntranceShuffle.None],
                    _entranceShuffleRequirements[EntranceShuffle.Dungeon]
                }],
                _alternativeRequirements[new HashSet<IRequirement>
                {
                    _shopShuffleRequirements[true],
                    _takeAnyLocationsRequirements[true]
                }]
            }];
        }
    }
}