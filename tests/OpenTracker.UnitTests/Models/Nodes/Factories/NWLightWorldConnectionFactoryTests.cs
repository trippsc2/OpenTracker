using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Autofac;
using ExpectedObjects;
using NSubstitute;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Nodes.Connections;
using OpenTracker.Models.Nodes.Factories;
using OpenTracker.Models.Prizes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Alternative;
using OpenTracker.Models.Requirements.Complex;
using OpenTracker.Models.Requirements.Item;
using OpenTracker.Models.Requirements.Item.Prize;
using OpenTracker.Models.Requirements.Mode;
using Xunit;

namespace OpenTracker.UnitTests.Models.Nodes.Factories;

[ExcludeFromCodeCoverage]
public sealed class NWLightWorldConnectionFactoryTests
{
    private static readonly IAlternativeRequirementDictionary AlternativeRequirements =
        new AlternativeRequirementDictionary(requirements => 
            new AlternativeRequirement(requirements));
    private static readonly IComplexRequirementDictionary ComplexRequirements =
        Substitute.For<IComplexRequirementDictionary>();
    private static readonly IEntranceShuffleRequirementDictionary EntranceShuffleRequirements =
        Substitute.For<IEntranceShuffleRequirementDictionary>();
    private static readonly IItemRequirementDictionary ItemRequirements =
        Substitute.For<IItemRequirementDictionary>();
    private static readonly IPrizeRequirementDictionary PrizeRequirements =
        Substitute.For<IPrizeRequirementDictionary>();
    private static readonly IWorldStateRequirementDictionary WorldStateRequirements =
        Substitute.For<IWorldStateRequirementDictionary>();

    private static readonly IOverworldNodeDictionary OverworldNodes =
        Substitute.For<IOverworldNodeDictionary>();

    private static readonly INodeConnection.Factory ConnectionFactory = 
        (fromNode, toNode, requirement) => new NodeConnection(fromNode, toNode, requirement);

    private static readonly Dictionary<OverworldNodeID, ExpectedObject> ExpectedValues = new();

    private readonly NWLightWorldConnectionFactory _sut = new(
        AlternativeRequirements, ComplexRequirements, EntranceShuffleRequirements, ItemRequirements,
        PrizeRequirements, WorldStateRequirements, OverworldNodes, ConnectionFactory);

    private static void PopulateExpectedValues()
    {
        ExpectedValues.Clear();

        foreach (OverworldNodeID id in Enum.GetValues(typeof(OverworldNodeID)))
        {
            var node = OverworldNodes[id];

            var connections = id switch
            {
                OverworldNodeID.Pedestal => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.LightWorld], node,
                        ComplexRequirements[ComplexRequirementType.Pedestal])
                },
                OverworldNodeID.LumberjackCaveHole => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.LightWorldDash], node,
                        PrizeRequirements[(PrizeType.Aga1, 1)])
                },
                OverworldNodeID.DeathMountainEntry => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.LightWorldLift1], node),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.BumperCaveEntry], node,
                        ComplexRequirements[ComplexRequirementType.DWMirror])
                },
                OverworldNodeID.DeathMountainEntryNonEntrance => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainEntry], node,
                        AlternativeRequirements[new HashSet<IRequirement>
                        {
                            EntranceShuffleRequirements[EntranceShuffle.None],
                            EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                        }])
                },
                OverworldNodeID.DeathMountainEntryCave => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainEntryNonEntrance], node,
                        WorldStateRequirements[WorldState.StandardOpen]),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainWestBottomNonEntrance], node,
                        WorldStateRequirements[WorldState.StandardOpen]),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.BumperCaveEntryNonEntrance], node,
                        WorldStateRequirements[WorldState.Inverted]),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DarkDeathMountainWestBottomNonEntrance], node,
                        WorldStateRequirements[WorldState.Inverted])
                },
                OverworldNodeID.DeathMountainEntryCaveDark => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainEntryCave], node,
                        ComplexRequirements[ComplexRequirementType.DarkRoomDeathMountainEntry])
                },
                OverworldNodeID.DeathMountainExit => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainExitCaveDark], node,
                        WorldStateRequirements[WorldState.StandardOpen]),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.BumperCaveBack], node,
                        WorldStateRequirements[WorldState.Inverted]),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.BumperCaveTop], node,
                        ComplexRequirements[ComplexRequirementType.DWMirror])
                },
                OverworldNodeID.DeathMountainExitNonEntrance => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainExit], node,
                        AlternativeRequirements[new HashSet<IRequirement>
                        {
                            EntranceShuffleRequirements[EntranceShuffle.None],
                            EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                        }]),
                },
                OverworldNodeID.DeathMountainExitCave => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainExitNonEntrance], node,
                        WorldStateRequirements[WorldState.StandardOpen]),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainWestBottomNonEntrance], node,
                        WorldStateRequirements[WorldState.StandardOpen])
                },
                OverworldNodeID.DeathMountainExitCaveDark => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainExitCave], node,
                        ComplexRequirements[ComplexRequirementType.DarkRoomDeathMountainExit])
                },
                OverworldNodeID.LWKakarikoPortal => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.LightWorldNotBunny], node,
                        ItemRequirements[(ItemType.Gloves, 2)]),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.LightWorldHammer], node),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DWKakarikoPortalInverted], node,
                        ItemRequirements[(ItemType.Gloves, 1)])
                },
                OverworldNodeID.LWKakarikoPortalStandardOpen => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.LWKakarikoPortal], node,
                        WorldStateRequirements[WorldState.StandardOpen])
                },
                OverworldNodeID.LWKakarikoPortalNotBunny => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.LWKakarikoPortal], node,
                        ComplexRequirements[ComplexRequirementType.NotBunnyLW])
                },
                OverworldNodeID.SickKid => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.LightWorld], node,
                        ItemRequirements[(ItemType.Bottle, 1)])
                },
                OverworldNodeID.GrassHouse => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.LightWorldNotBunny], node)
                },
                OverworldNodeID.BombHut => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.LightWorldNotBunny], node)
                },
                OverworldNodeID.MagicBatLedge => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.LightWorldHammer], node),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.HammerPegsArea], node,
                        ComplexRequirements[ComplexRequirementType.DWMirror])
                },
                OverworldNodeID.MagicBat => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.MagicBatLedge], node,
                        ComplexRequirements[ComplexRequirementType.MagicBat])
                },
                OverworldNodeID.LWGraveyard => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.LightWorldNotBunny], node),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.LWGraveyardLedge], node),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.KingsTombNotBunny], node,
                        ItemRequirements[(ItemType.Gloves, 2)])
                },
                OverworldNodeID.LWGraveyardNotBunny => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.LWGraveyard], node,
                        ComplexRequirements[ComplexRequirementType.NotBunnyLW])
                },
                OverworldNodeID.LWGraveyardLedge => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.LWGraveyardNotBunny], node,
                        WorldStateRequirements[WorldState.Inverted]),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.DWGraveyardMirror], node)
                },
                OverworldNodeID.EscapeGrave => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.LWGraveyardNotBunny], node,
                        ItemRequirements[(ItemType.Gloves, 1)])
                },
                OverworldNodeID.KingsTomb => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.LWGraveyardNotBunny], node,
                        ItemRequirements[(ItemType.Gloves, 2)]),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.DWGraveyardMirror], node)
                },
                OverworldNodeID.KingsTombNotBunny => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.KingsTomb], node,
                        ComplexRequirements[ComplexRequirementType.NotBunnyLW])
                },
                OverworldNodeID.KingsTombGrave => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.KingsTombNotBunny], node,
                        ItemRequirements[(ItemType.Boots, 1)])
                },
                _ => null
            };

            if (connections is null)
            {
                continue;
            }

            ExpectedValues.Add(id, connections.ToExpectedObject());
        }
    }

    [Fact]
    public void GetNodeConnections_ShouldThrowException_WhenIDIsUnexpected()
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => _sut.GetNodeConnections((OverworldNodeID)int.MaxValue, Substitute.For<INode>()));
    }

    [Theory]
    [MemberData(nameof(GetNodeConnections_ShouldReturnExpectedValueData))]
    public void GetNodeConnections_ShouldReturnExpectedValue(ExpectedObject expected, OverworldNodeID id)
    {
        expected.ShouldEqual(_sut.GetNodeConnections(id, OverworldNodes[id]));
    }

    public static IEnumerable<object[]> GetNodeConnections_ShouldReturnExpectedValueData()
    {
        PopulateExpectedValues();

        return ExpectedValues.Keys.Select(id => new object[] {ExpectedValues[id], id}).ToList();
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut = scope.Resolve<INWLightWorldConnectionFactory>();
            
        Assert.NotNull(sut as NWLightWorldConnectionFactory);
    }
}