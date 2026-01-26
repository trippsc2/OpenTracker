using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using ExpectedObjects;
using NSubstitute;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Nodes.Connections;
using OpenTracker.Models.Nodes.Factories;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Alternative;
using OpenTracker.Models.Requirements.Complex;
using OpenTracker.Models.Requirements.Item;
using OpenTracker.Models.Requirements.Mode;
using OpenTracker.Models.Requirements.Node;
using Xunit;

namespace OpenTracker.UnitTests.Models.Nodes.Factories;

public class NWDarkWorldConnectionFactoryTests
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
    private static readonly INodeRequirementDictionary NodeRequirements =
        Substitute.For<INodeRequirementDictionary>();
    private static readonly IWorldStateRequirementDictionary WorldStateRequirements =
        Substitute.For<IWorldStateRequirementDictionary>();
        
    private static readonly IOverworldNodeDictionary OverworldNodes = Substitute.For<IOverworldNodeDictionary>();

    private static readonly INodeConnection.Factory ConnectionFactory = (fromNode, toNode, requirement) =>
        new NodeConnection(fromNode, toNode, requirement);

    private static readonly Dictionary<OverworldNodeID, ExpectedObject> ExpectedValues = new();

    private readonly NWDarkWorldConnectionFactory _sut = new(
        AlternativeRequirements, ComplexRequirements, EntranceShuffleRequirements, ItemRequirements,
        NodeRequirements, WorldStateRequirements, OverworldNodes, ConnectionFactory);

    private static void PopulateExpectedValues()
    {
        ExpectedValues.Clear();

        foreach (OverworldNodeID id in Enum.GetValues(typeof(OverworldNodeID)))
        {
            var node = OverworldNodes[id];

            var connections = id switch
            {
                OverworldNodeID.DWKakarikoPortal => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.LWKakarikoPortalStandardOpen], node,
                        ItemRequirements[(ItemType.Gloves, 1)]),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.DarkWorldWestNotBunny], node)
                },
                OverworldNodeID.DWKakarikoPortalInverted => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DWKakarikoPortal], node,
                        WorldStateRequirements[WorldState.Inverted])
                },
                OverworldNodeID.DarkWorldWest => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.EntranceNoneInverted], node),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.FluteInverted], node),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.LightWorldMirror], node),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DWKakarikoPortal], node,
                        ComplexRequirements[ComplexRequirementType.NotBunnyDW]),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.SkullWoodsBackArea], node,
                        AlternativeRequirements[new HashSet<IRequirement>
                        {
                            EntranceShuffleRequirements[EntranceShuffle.None],
                            EntranceShuffleRequirements[EntranceShuffle.Dungeon],
                            EntranceShuffleRequirements[EntranceShuffle.All]
                        }]),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.BumperCaveEntry], node),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.BumperCaveTop], node),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.HammerHouseNotBunny], node,
                        ItemRequirements[(ItemType.Hammer, 1)]),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DarkWorldSouthNotBunny], node,
                        ItemRequirements[(ItemType.Gloves, 2)]),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DWWitchAreaNotBunny], node,
                        ItemRequirements[(ItemType.Hookshot, 1)])
                },
                OverworldNodeID.DarkWorldWestNotBunny => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DarkWorldWest], node,
                        ComplexRequirements[ComplexRequirementType.NotBunnyDW])
                },
                OverworldNodeID.DarkWorldWestNotBunnyOrSuperBunnyMirror => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.DarkWorldWestNotBunny], node),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DarkWorldWest], node,
                        ComplexRequirements[ComplexRequirementType.SuperBunnyMirror])
                },
                OverworldNodeID.DarkWorldWestLift2 => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DarkWorldWestNotBunny], node,
                        ItemRequirements[(ItemType.Gloves, 2)])
                },
                OverworldNodeID.SkullWoodsBackArea => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DarkWorldWest], node,
                        AlternativeRequirements[new HashSet<IRequirement>
                        {
                            EntranceShuffleRequirements[EntranceShuffle.None],
                            EntranceShuffleRequirements[EntranceShuffle.Dungeon],
                            EntranceShuffleRequirements[EntranceShuffle.All]
                        }])
                },
                OverworldNodeID.SkullWoodsBackAreaNotBunny => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.SkullWoodsBackArea], node,
                        ComplexRequirements[ComplexRequirementType.NotBunnyDW])
                },
                OverworldNodeID.SkullWoodsBack => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.SkullWoodsBackAreaNotBunny], node,
                        ItemRequirements[(ItemType.FireRod, 1)])
                },
                OverworldNodeID.BumperCaveEntry => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DarkWorldWestNotBunny], node,
                        ItemRequirements[(ItemType.Gloves, 1)]),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainEntry], node,
                        ComplexRequirements[ComplexRequirementType.LWMirror])
                },
                OverworldNodeID.BumperCaveEntryNonEntrance => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.BumperCaveEntry], node,
                        AlternativeRequirements[new HashSet<IRequirement>
                        {
                            EntranceShuffleRequirements[EntranceShuffle.None],
                            EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                        }])
                },
                OverworldNodeID.BumperCaveFront => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainEntryNonEntrance], node,
                        WorldStateRequirements[WorldState.Inverted]),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.BumperCaveEntryNonEntrance], node,
                        WorldStateRequirements[WorldState.StandardOpen])
                },
                OverworldNodeID.BumperCaveNotBunny => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.BumperCaveFront], node,
                        ItemRequirements[(ItemType.MoonPearl, 1)])
                },
                OverworldNodeID.BumperCavePastGap => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.BumperCaveNotBunny], node,
                        ComplexRequirements[ComplexRequirementType.BumperCaveGap])
                },
                OverworldNodeID.BumperCaveBack => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.BumperCavePastGap], node,
                        ItemRequirements[(ItemType.Cape, 1)])
                },
                OverworldNodeID.BumperCaveTop => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainExit], node,
                        ComplexRequirements[ComplexRequirementType.LWMirror]),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.BumperCaveBack], node,
                        WorldStateRequirements[WorldState.StandardOpen])
                },
                OverworldNodeID.HammerHouse => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DarkWorldWestNotBunny], node,
                        ItemRequirements[(ItemType.Hammer, 1)])
                },
                OverworldNodeID.HammerHouseNotBunny => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.HammerHouse], node,
                        ComplexRequirements[ComplexRequirementType.NotBunnyDW])
                },
                OverworldNodeID.HammerPegsArea => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.LightWorldMirror], node),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.DarkWorldWestLift2], node)
                },
                OverworldNodeID.HammerPegs => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.HammerPegsArea], node,
                        ItemRequirements[(ItemType.Hammer, 1)])
                },
                OverworldNodeID.PurpleChest => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.Blacksmith], node,
                        NodeRequirements[OverworldNodeID.HammerPegsArea])
                },
                OverworldNodeID.BlacksmithPrison => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.LightWorldMirror], node),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.DarkWorldWestLift2], node)
                },
                OverworldNodeID.Blacksmith => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.BlacksmithPrison], node,
                        NodeRequirements[OverworldNodeID.LightWorld])
                },
                OverworldNodeID.DWGraveyard => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.DarkWorldWestNotBunny], node)
                },
                OverworldNodeID.DWGraveyardMirror => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DWGraveyard], node,
                        ComplexRequirements[ComplexRequirementType.DWMirror])
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
        var sut = scope.Resolve<INWDarkWorldConnectionFactory>();
            
        Assert.NotNull(sut as NWDarkWorldConnectionFactory);
    }
}