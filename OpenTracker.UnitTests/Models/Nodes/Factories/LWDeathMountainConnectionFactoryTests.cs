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
using OpenTracker.Models.Requirements.SequenceBreak;
using OpenTracker.Models.SequenceBreaks;
using Xunit;

namespace OpenTracker.UnitTests.Models.Nodes.Factories;

public class LWDeathMountainConnectionFactoryTests
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
    private static readonly ISequenceBreakRequirementDictionary SequenceBreakRequirements =
        Substitute.For<ISequenceBreakRequirementDictionary>();
    private static readonly IWorldStateRequirementDictionary WorldStateRequirements =
        Substitute.For<IWorldStateRequirementDictionary>();
        
    private static readonly IOverworldNodeDictionary OverworldNodes =
        Substitute.For<IOverworldNodeDictionary>();

    private static readonly INodeConnection.Factory ConnectionFactory =
        (fromNode, toNode, requirement) => new NodeConnection(fromNode, toNode, requirement);

    private static readonly Dictionary<OverworldNodeID, ExpectedObject> ExpectedValues = new();

    private readonly LWDeathMountainConnectionFactory _sut = new(
        AlternativeRequirements, ComplexRequirements, EntranceShuffleRequirements, ItemRequirements,
        SequenceBreakRequirements, WorldStateRequirements, OverworldNodes, ConnectionFactory);
        
    private static void PopulateExpectedValues()
    {
        ExpectedValues.Clear();

        foreach (OverworldNodeID id in Enum.GetValues(typeof(OverworldNodeID)))
        {
            var node = OverworldNodes[id];

            var connections = id switch
            {
                OverworldNodeID.DeathMountainWestBottom => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.FluteStandardOpen], node),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainEntryCaveDark], node,
                        WorldStateRequirements[WorldState.StandardOpen]),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainExitCaveDark], node,
                        WorldStateRequirements[WorldState.StandardOpen]),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.DeathMountainWestTop], node),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainEastBottomNotBunny], node,
                        ItemRequirements[(ItemType.Hookshot, 1)]),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.DarkDeathMountainWestBottomInverted], node),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.DarkDeathMountainWestBottomMirror], node)
                },
                OverworldNodeID.DeathMountainWestBottomNonEntrance => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainWestBottom], node,
                        AlternativeRequirements[new HashSet<IRequirement>
                        {
                            EntranceShuffleRequirements[EntranceShuffle.None],
                            EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                        }])
                },
                OverworldNodeID.DeathMountainWestBottomNotBunny => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainWestBottom], node,
                        ComplexRequirements[ComplexRequirementType.NotBunnyLW])
                },
                OverworldNodeID.SpectacleRockTop => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainWestTop], node,
                        WorldStateRequirements[WorldState.Inverted]),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.DarkDeathMountainWestBottomMirror], node)
                },
                OverworldNodeID.DeathMountainWestTop => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.SpectacleRockTop], node),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainEastTopNotBunny], node,
                        ItemRequirements[(ItemType.Hammer, 1)]),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.DarkDeathMountainTopMirror], node)
                },
                OverworldNodeID.DeathMountainWestTopNotBunny => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainWestTop], node,
                        ComplexRequirements[ComplexRequirementType.NotBunnyLW])
                },
                OverworldNodeID.EtherTablet => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainWestTop], node,
                        ComplexRequirements[ComplexRequirementType.Tablet])
                },
                OverworldNodeID.DeathMountainEastBottom => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainWestBottomNotBunny], node,
                        ItemRequirements[(ItemType.Hookshot, 1)]),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.DeathMountainEastBottomConnector], node),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.ParadoxCave], node),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.DeathMountainEastTop], node),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.SpiralCaveLedge], node),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.MimicCaveLedge], node,
                        WorldStateRequirements[WorldState.Inverted]),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DarkDeathMountainEastBottom], node,
                        ComplexRequirements[ComplexRequirementType.DWMirror]),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DarkDeathMountainEastBottomInverted], node,
                        ItemRequirements[(ItemType.Gloves, 2)])
                },
                OverworldNodeID.DeathMountainEastBottomNotBunny => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainEastBottom], node,
                        ComplexRequirements[ComplexRequirementType.NotBunnyLW])
                },
                OverworldNodeID.DeathMountainEastBottomLift2 => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainEastBottomNotBunny], node,
                        ItemRequirements[(ItemType.Gloves, 2)])
                },
                OverworldNodeID.DeathMountainEastBottomConnector => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.DeathMountainEastBottomLift2], node),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.DeathMountainEastTopConnector], node),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DarkDeathMountainEastBottomConnector], node,
                        ComplexRequirements[ComplexRequirementType.DWMirror])
                },
                OverworldNodeID.ParadoxCave => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainEastBottom], node,
                        AlternativeRequirements[new HashSet<IRequirement>
                        {
                            EntranceShuffleRequirements[EntranceShuffle.None],
                            EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                        }]),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainEastTop], node,
                        AlternativeRequirements[new HashSet<IRequirement>
                        {
                            EntranceShuffleRequirements[EntranceShuffle.None],
                            EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                        }])
                },
                OverworldNodeID.ParadoxCaveSuperBunnyFallInHole => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.ParadoxCave], node,
                        SequenceBreakRequirements[SequenceBreakType.SuperBunnyFallInHole])
                },
                OverworldNodeID.ParadoxCaveNotBunny => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.ParadoxCave], node,
                        ComplexRequirements[ComplexRequirementType.NotBunnyLW])
                },
                OverworldNodeID.ParadoxCaveTop => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.ParadoxCaveNotBunny], node),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.ParadoxCaveSuperBunnyFallInHole], node,
                        ItemRequirements[(ItemType.Sword, 3)])
                },
                OverworldNodeID.DeathMountainEastTop => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainWestTopNotBunny], node,
                        ItemRequirements[(ItemType.Hammer, 1)]),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.ParadoxCave], node),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.LWTurtleRockTopInvertedNotBunny], node,
                        ItemRequirements[(ItemType.Hammer, 1)]),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.DarkDeathMountainTopMirror], node)
                },
                OverworldNodeID.DeathMountainEastTopInverted => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainEastTop], node,
                        WorldStateRequirements[WorldState.Inverted])
                },
                OverworldNodeID.DeathMountainEastTopNotBunny => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainEastTop], node,
                        ComplexRequirements[ComplexRequirementType.NotBunnyLW])
                },
                OverworldNodeID.DeathMountainEastTopConnector => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.DeathMountainEastTop], node),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.TurtleRockSafetyDoor], node,
                        ComplexRequirements[ComplexRequirementType.DWMirror])
                },
                OverworldNodeID.SpiralCaveLedge => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.DeathMountainEastTop], node),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.TurtleRockTunnelMirror], node)
                },
                OverworldNodeID.SpiralCave => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.SpiralCaveLedge], node,
                        ComplexRequirements[ComplexRequirementType.NotBunnyLW]),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.SpiralCaveLedge], node,
                        SequenceBreakRequirements[SequenceBreakType.SuperBunnyFallInHole])
                },
                OverworldNodeID.MimicCaveLedge => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.DeathMountainEastTopInverted], node),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.TurtleRockTunnelMirror], node)
                },
                OverworldNodeID.MimicCaveLedgeNotBunny => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.MimicCaveLedge], node,
                        ComplexRequirements[ComplexRequirementType.NotBunnyLW])
                },
                OverworldNodeID.MimicCave => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.MimicCaveLedgeNotBunny], node,
                        ItemRequirements[(ItemType.Hammer, 1)])
                },
                OverworldNodeID.LWFloatingIsland => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.DeathMountainEastTopInverted], node),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DWFloatingIsland], node,
                        ComplexRequirements[ComplexRequirementType.DWMirror])
                },
                OverworldNodeID.LWTurtleRockTop => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainEastTopNotBunny], node,
                        ItemRequirements[(ItemType.Gloves, 2)]),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DWTurtleRockTopInverted], node,
                        ItemRequirements[(ItemType.Gloves, 2)])
                },
                OverworldNodeID.LWTurtleRockTopInverted => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.LWTurtleRockTop], node,
                        WorldStateRequirements[WorldState.Inverted])
                },
                OverworldNodeID.LWTurtleRockTopInvertedNotBunny => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.LWTurtleRockTopInverted], node,
                        ComplexRequirements[ComplexRequirementType.NotBunnyLW])
                },
                OverworldNodeID.LWTurtleRockTopStandardOpen => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.LWTurtleRockTop], node,
                        WorldStateRequirements[WorldState.StandardOpen])
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
        var sut = scope.Resolve<ILWDeathMountainConnectionFactory>();
            
        Assert.NotNull(sut as LWDeathMountainConnectionFactory);
    }
}