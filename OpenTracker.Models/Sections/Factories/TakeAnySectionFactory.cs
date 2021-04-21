using System;
using System.Collections.Generic;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Aggregate;
using OpenTracker.Models.Requirements.Alternative;
using OpenTracker.Models.Requirements.Mode;
using OpenTracker.Models.Requirements.TakeAnyLocations;
using OpenTracker.Models.Sections.Boolean;

namespace OpenTracker.Models.Sections.Factories
{
    /// <summary>
    ///     This class contains the creation logic for take any sections.
    /// </summary>
    public class TakeAnySectionFactory : ITakeAnySectionFactory
    {
        private readonly IAggregateRequirementDictionary _aggregateRequirements;
        private readonly IAlternativeRequirementDictionary _alternativeRequirements;
        private readonly IEntranceShuffleRequirementDictionary _entranceShuffleRequirements;
        private readonly ITakeAnyLocationsRequirementDictionary _takeAnyLocationsRequirements;

        private readonly IOverworldNodeDictionary _overworldNodes;
        
        private readonly ITakeAnySection.Factory _factory;

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
        /// <param name="takeAnyLocationsRequirements">
        ///     The take any locations requirement dictionary.
        /// </param>
        /// <param name="overworldNodes">
        ///     The overworld node dictionary.
        /// </param>
        /// <param name="factory">
        ///     An Autofac factory for creating new take any sections.
        /// </param>
        public TakeAnySectionFactory(
            IAggregateRequirementDictionary aggregateRequirements,
            IAlternativeRequirementDictionary alternativeRequirements,
            IEntranceShuffleRequirementDictionary entranceShuffleRequirements,
            ITakeAnyLocationsRequirementDictionary takeAnyLocationsRequirements,
            IOverworldNodeDictionary overworldNodes, ITakeAnySection.Factory factory)
        {
            _aggregateRequirements = aggregateRequirements;
            _alternativeRequirements = alternativeRequirements;
            _entranceShuffleRequirements = entranceShuffleRequirements;
            _takeAnyLocationsRequirements = takeAnyLocationsRequirements;
            
            _overworldNodes = overworldNodes;
            
            _factory = factory;
        }

        public ITakeAnySection GetTakeAnySection(LocationID id)
        {
            return _factory(GetNode(id), _aggregateRequirements[new HashSet<IRequirement>
            {
                _alternativeRequirements[new HashSet<IRequirement>
                {
                    _entranceShuffleRequirements[EntranceShuffle.None],
                    _entranceShuffleRequirements[EntranceShuffle.Dungeon]
                }],
                _takeAnyLocationsRequirements[true]
            }]);
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
                case LocationID.TreesFairyCaveTakeAny:
                case LocationID.PegsFairyCaveTakeAny:
                case LocationID.KakarikoFortuneTellerTakeAny:
                case LocationID.ForestChestGameTakeAny:
                case LocationID.LumberjackHouseTakeAny:
                case LocationID.LeftSnitchHouseTakeAny:
                case LocationID.RightSnitchHouseTakeAny:
                case LocationID.ThiefCaveTakeAny:
                case LocationID.IceBeeCaveTakeAny:
                case LocationID.FortuneTellerTakeAny:
                case LocationID.ChestGameTakeAny:
                    return _overworldNodes[OverworldNodeID.LightWorld];
                case LocationID.GrassHouseTakeAny:
                    return _overworldNodes[OverworldNodeID.GrassHouse];
                case LocationID.BombHutTakeAny:
                    return _overworldNodes[OverworldNodeID.BombHut];
                case LocationID.IceFairyCaveTakeAny:
                case LocationID.RupeeCaveTakeAny:
                    return _overworldNodes[OverworldNodeID.LightWorldLift1];
                case LocationID.CentralBonkRocksTakeAny:
                    return _overworldNodes[OverworldNodeID.LightWorldDash];
                case LocationID.HypeFairyCaveTakeAny:
                    return _overworldNodes[OverworldNodeID.LightWorldNotBunny];
                case LocationID.EDMFairyCaveTakeAny:
                    return _overworldNodes[OverworldNodeID.DeathMountainEastBottom];
                case LocationID.DarkChapelTakeAny:
                case LocationID.DarkVillageFortuneTellerTakeAny:
                    return _overworldNodes[OverworldNodeID.DarkWorldWest];
                case LocationID.DarkTreesFairyCaveTakeAny:
                case LocationID.DarkSahasrahlaTakeAny:
                case LocationID.DarkFluteSpotFiveTakeAny:
                case LocationID.ArrowGameTakeAny:
                    return _overworldNodes[OverworldNodeID.DarkWorldEast];
                case LocationID.DarkCentralBonkRocksTakeAny:
                    return _overworldNodes[OverworldNodeID.DarkWorldSouthDash];
                case LocationID.DarkIceRodCaveTakeAny:
                    return _overworldNodes[OverworldNodeID.DarkWorldSouthEastNotBunny];
                case LocationID.DarkFakeIceRodCaveTakeAny:
                    return _overworldNodes[OverworldNodeID.DarkWorldSouthEast];
                case LocationID.DarkIceRodRockTakeAny:
                    return _overworldNodes[OverworldNodeID.DarkWorldSouthEastLift1];
                case LocationID.DarkMountainFairyTakeAny:
                    return _overworldNodes[OverworldNodeID.DarkDeathMountainWestBottom];
                case LocationID.MireRightShackTakeAny:
                case LocationID.MireCaveTakeAny:
                    return _overworldNodes[OverworldNodeID.MireArea];
                default:
                    throw new ArgumentOutOfRangeException(nameof(id), id, null);
            }
        }
    }
}