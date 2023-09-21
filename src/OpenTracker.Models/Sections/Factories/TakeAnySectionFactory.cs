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
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Sections.Factories;

/// <summary>
/// This class contains the creation logic for <see cref="ITakeAnySection"/> objects.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class TakeAnySectionFactory : ITakeAnySectionFactory
{
    private readonly IAggregateRequirementDictionary _aggregateRequirements;
    private readonly IAlternativeRequirementDictionary _alternativeRequirements;
    private readonly IEntranceShuffleRequirementDictionary _entranceShuffleRequirements;
    private readonly ITakeAnyLocationsRequirementDictionary _takeAnyLocationsRequirements;

    private readonly IOverworldNodeDictionary _overworldNodes;
        
    private readonly ITakeAnySection.Factory _factory;

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
    /// <param name="takeAnyLocationsRequirements">
    ///     The <see cref="ITakeAnyLocationsRequirementDictionary"/>.
    /// </param>
    /// <param name="overworldNodes">
    ///     The <see cref="IOverworldNodeDictionary"/>.
    /// </param>
    /// <param name="factory">
    ///     An Autofac factory for creating new <see cref="ITakeAnySection"/> objects.
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