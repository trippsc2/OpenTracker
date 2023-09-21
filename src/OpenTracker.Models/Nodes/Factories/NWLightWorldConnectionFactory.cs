using System;
using System.Collections.Generic;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Nodes.Connections;
using OpenTracker.Models.Prizes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Alternative;
using OpenTracker.Models.Requirements.Complex;
using OpenTracker.Models.Requirements.Item;
using OpenTracker.Models.Requirements.Item.Prize;
using OpenTracker.Models.Requirements.Mode;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Nodes.Factories;

/// <summary>
/// This class contains the creation logic for northwest light world <see cref="INodeConnection"/> objects. 
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class NWLightWorldConnectionFactory : INWLightWorldConnectionFactory
{
    private readonly IAlternativeRequirementDictionary _alternativeRequirements;
    private readonly IComplexRequirementDictionary _complexRequirements;
    private readonly IEntranceShuffleRequirementDictionary _entranceShuffleRequirements;
    private readonly IItemRequirementDictionary _itemRequirements;
    private readonly IPrizeRequirementDictionary _prizeRequirements;
    private readonly IWorldStateRequirementDictionary _worldStateRequirements;

    private readonly IOverworldNodeDictionary _overworldNodes;

    private readonly INodeConnection.Factory _connectionFactory;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="alternativeRequirements">
    ///     The <see cref="IAlternativeRequirementDictionary"/>.
    /// </param>
    /// <param name="complexRequirements">
    ///     The <see cref="IComplexRequirementDictionary"/>.
    /// </param>
    /// <param name="entranceShuffleRequirements">
    ///     The <see cref="IEntranceShuffleRequirementDictionary"/>.
    /// </param>
    /// <param name="itemRequirements">
    ///     The <see cref="IItemRequirementDictionary"/>.
    /// </param>
    /// <param name="prizeRequirements">
    ///     The <see cref="IPrizeRequirementDictionary"/>.
    /// </param>
    /// <param name="worldStateRequirements">
    ///     The <see cref="IWorldStateRequirementDictionary"/>.
    /// </param>
    /// <param name="overworldNodes">
    ///     The <see cref="IOverworldNodeDictionary"/>.
    /// </param>
    /// <param name="connectionFactory">
    ///     An Autofac factory for creating new <see cref="INodeConnection"/> objects.
    /// </param>
    public NWLightWorldConnectionFactory(
        IAlternativeRequirementDictionary alternativeRequirements,
        IComplexRequirementDictionary complexRequirements,
        IEntranceShuffleRequirementDictionary entranceShuffleRequirements,
        IItemRequirementDictionary itemRequirements, IPrizeRequirementDictionary prizeRequirements,
        IWorldStateRequirementDictionary worldStateRequirements, IOverworldNodeDictionary overworldNodes,
        INodeConnection.Factory connectionFactory)
    {
        _alternativeRequirements = alternativeRequirements;
        _complexRequirements = complexRequirements;
        _entranceShuffleRequirements = entranceShuffleRequirements;
        _itemRequirements = itemRequirements;
        _prizeRequirements = prizeRequirements;
        _worldStateRequirements = worldStateRequirements;
        _overworldNodes = overworldNodes;
        _connectionFactory = connectionFactory;
    }

    public IEnumerable<INodeConnection> GetNodeConnections(OverworldNodeID id, INode node)
    {
        return id switch
        {
            OverworldNodeID.Pedestal => new List<INodeConnection>
            {
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.LightWorld], node,
                    _complexRequirements[ComplexRequirementType.Pedestal])
            },
            OverworldNodeID.LumberjackCaveHole => new List<INodeConnection>
            {
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.LightWorldDash], node,
                    _prizeRequirements[(PrizeType.Aga1, 1)])
            },
            OverworldNodeID.DeathMountainEntry => new List<INodeConnection>
            {
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.LightWorldLift1], node),
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.BumperCaveEntry], node,
                    _complexRequirements[ComplexRequirementType.DWMirror])
            },
            OverworldNodeID.DeathMountainEntryNonEntrance => new List<INodeConnection>
            {
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.DeathMountainEntry], node,
                    _alternativeRequirements[new HashSet<IRequirement>
                    {
                        _entranceShuffleRequirements[EntranceShuffle.None],
                        _entranceShuffleRequirements[EntranceShuffle.Dungeon]
                    }])
            },
            OverworldNodeID.DeathMountainEntryCave => new List<INodeConnection>
            {
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.DeathMountainEntryNonEntrance], node,
                    _worldStateRequirements[WorldState.StandardOpen]),
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.DeathMountainWestBottomNonEntrance], node,
                    _worldStateRequirements[WorldState.StandardOpen]),
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.BumperCaveEntryNonEntrance], node,
                    _worldStateRequirements[WorldState.Inverted]),
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.DarkDeathMountainWestBottomNonEntrance], node,
                    _worldStateRequirements[WorldState.Inverted])
            },
            OverworldNodeID.DeathMountainEntryCaveDark => new List<INodeConnection>
            {
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.DeathMountainEntryCave], node,
                    _complexRequirements[ComplexRequirementType.DarkRoomDeathMountainEntry])
            },
            OverworldNodeID.DeathMountainExit => new List<INodeConnection>
            {
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.DeathMountainExitCaveDark], node,
                    _worldStateRequirements[WorldState.StandardOpen]),
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.BumperCaveBack], node,
                    _worldStateRequirements[WorldState.Inverted]),
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.BumperCaveTop], node,
                    _complexRequirements[ComplexRequirementType.DWMirror])
            },
            OverworldNodeID.DeathMountainExitNonEntrance => new List<INodeConnection>
            {
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.DeathMountainExit], node,
                    _alternativeRequirements[new HashSet<IRequirement>
                    {
                        _entranceShuffleRequirements[EntranceShuffle.None],
                        _entranceShuffleRequirements[EntranceShuffle.Dungeon]
                    }]),
            },
            OverworldNodeID.DeathMountainExitCave => new List<INodeConnection>
            {
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.DeathMountainExitNonEntrance], node,
                    _worldStateRequirements[WorldState.StandardOpen]),
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.DeathMountainWestBottomNonEntrance], node,
                    _worldStateRequirements[WorldState.StandardOpen])
            },
            OverworldNodeID.DeathMountainExitCaveDark => new List<INodeConnection>
            {
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.DeathMountainExitCave], node,
                    _complexRequirements[ComplexRequirementType.DarkRoomDeathMountainExit])
            },
            OverworldNodeID.LWKakarikoPortal => new List<INodeConnection>
            {
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.LightWorldNotBunny], node,
                    _itemRequirements[(ItemType.Gloves, 2)]),
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.LightWorldHammer], node),
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.DWKakarikoPortalInverted], node,
                    _itemRequirements[(ItemType.Gloves, 1)])
            },
            OverworldNodeID.LWKakarikoPortalStandardOpen => new List<INodeConnection>
            {
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.LWKakarikoPortal], node,
                    _worldStateRequirements[WorldState.StandardOpen])
            },
            OverworldNodeID.LWKakarikoPortalNotBunny => new List<INodeConnection>
            {
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.LWKakarikoPortal], node,
                    _complexRequirements[ComplexRequirementType.NotBunnyLW])
            },
            OverworldNodeID.SickKid => new List<INodeConnection>
            {
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.LightWorld], node,
                    _itemRequirements[(ItemType.Bottle, 1)])
            },
            OverworldNodeID.GrassHouse => new List<INodeConnection>
            {
                _connectionFactory(_overworldNodes[OverworldNodeID.LightWorldNotBunny], node)
            },
            OverworldNodeID.BombHut => new List<INodeConnection>
            {
                _connectionFactory(_overworldNodes[OverworldNodeID.LightWorldNotBunny], node)
            },
            OverworldNodeID.MagicBatLedge => new List<INodeConnection>
            {
                _connectionFactory(_overworldNodes[OverworldNodeID.LightWorldHammer], node),
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.HammerPegsArea], node,
                    _complexRequirements[ComplexRequirementType.DWMirror])
            },
            OverworldNodeID.MagicBat => new List<INodeConnection>
            {
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.MagicBatLedge], node,
                    _complexRequirements[ComplexRequirementType.MagicBat])
            },
            OverworldNodeID.LWGraveyard => new List<INodeConnection>
            {
                _connectionFactory(_overworldNodes[OverworldNodeID.LightWorldNotBunny], node),
                _connectionFactory(_overworldNodes[OverworldNodeID.LWGraveyardLedge], node),
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.KingsTombNotBunny], node,
                    _itemRequirements[(ItemType.Gloves, 2)])
            },
            OverworldNodeID.LWGraveyardNotBunny => new List<INodeConnection>
            {
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.LWGraveyard], node,
                    _complexRequirements[ComplexRequirementType.NotBunnyLW])
            },
            OverworldNodeID.LWGraveyardLedge => new List<INodeConnection>
            {
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.LWGraveyardNotBunny], node,
                    _worldStateRequirements[WorldState.Inverted]),
                _connectionFactory(_overworldNodes[OverworldNodeID.DWGraveyardMirror], node)
            },
            OverworldNodeID.EscapeGrave => new List<INodeConnection>
            {
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.LWGraveyardNotBunny], node,
                    _itemRequirements[(ItemType.Gloves, 1)])
            },
            OverworldNodeID.KingsTomb => new List<INodeConnection>
            {
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.LWGraveyardNotBunny], node,
                    _itemRequirements[(ItemType.Gloves, 2)]),
                _connectionFactory(_overworldNodes[OverworldNodeID.DWGraveyardMirror], node)
            },
            OverworldNodeID.KingsTombNotBunny => new List<INodeConnection>
            {
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.KingsTomb], node,
                    _complexRequirements[ComplexRequirementType.NotBunnyLW])
            },
            OverworldNodeID.KingsTombGrave => new List<INodeConnection>
            {
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.KingsTombNotBunny], node,
                    _itemRequirements[(ItemType.Boots, 1)])
            },
            _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
        };
    }
}