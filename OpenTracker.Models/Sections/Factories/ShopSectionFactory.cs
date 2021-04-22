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
    ///     This class contains the creation logic for shop sections.
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
        ///     Constructor
        /// </summary>
        /// <param name="aggregateRequirements">
        ///     The aggregate requirement dictionary.
        /// </param>
        /// <param name="alternativeRequirements">
        ///     The alternative requirement dictionary.
        /// </param>
        /// <param name="entranceShuffleRequirements">
        ///     The entrance shuffle requirement dictionary.
        /// </param>
        /// <param name="shopShuffleRequirements">
        ///     The shop shuffle requirement dictionary.
        /// </param>
        /// <param name="takeAnyLocationsRequirements">
        ///     The take any locations requirement dictionary.
        /// </param>
        /// <param name="overworldNodes">
        ///     The overworld node dictionary.
        /// </param>
        /// <param name="factory">
        ///     An Autofac factory for creating new shop sections.
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
        ///     Returns the node to which the section belongs.
        /// </summary>
        /// <param name="id">
        ///     The location ID.
        /// </param>
        /// <returns>
        ///     The node to which the section belongs.
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
        ///     Returns the requirement for the section to be active.
        /// </summary>
        /// <param name="id">
        ///     The location ID.
        /// </param>
        /// <returns>
        ///     The requirement for the section to be active.
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