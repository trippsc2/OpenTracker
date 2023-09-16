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
using OpenTracker.Models.Requirements.Item.Crystal;
using OpenTracker.Models.Requirements.Mode;
using OpenTracker.Models.Requirements.SequenceBreak;
using OpenTracker.Models.SequenceBreaks;
using Xunit;

namespace OpenTracker.UnitTests.Models.Nodes.Factories;

public class DWDeathMountainConnectionFactoryTests
{
    private static readonly IAlternativeRequirementDictionary AlternativeRequirements =
        Substitute.For<IAlternativeRequirementDictionary>();
    private static readonly IComplexRequirementDictionary ComplexRequirements =
        Substitute.For<IComplexRequirementDictionary>();
    private static readonly ICrystalRequirement CrystalRequirement = Substitute.For<ICrystalRequirement>();
    private static readonly IEntranceShuffleRequirementDictionary EntranceShuffleRequirements =
        Substitute.For<IEntranceShuffleRequirementDictionary>();
    private static readonly IItemRequirementDictionary ItemRequirements =
        Substitute.For<IItemRequirementDictionary>();
    private static readonly ISequenceBreakRequirementDictionary SequenceBreakRequirements =
        Substitute.For<ISequenceBreakRequirementDictionary>();
    private static readonly IWorldStateRequirementDictionary WorldStateRequirements =
        Substitute.For<IWorldStateRequirementDictionary>();
        
    private static readonly IOverworldNodeDictionary OverworldNodes = Substitute.For<IOverworldNodeDictionary>();

    private static readonly INodeConnection.Factory ConnectionFactory =
        (fromNode, toNode, requirement) => new NodeConnection(fromNode, toNode, requirement);

    private static readonly Dictionary<OverworldNodeID, ExpectedObject> ExpectedValues = new();

    private readonly DWDeathMountainConnectionFactory _sut = new(
        AlternativeRequirements, ComplexRequirements, CrystalRequirement, EntranceShuffleRequirements,
        ItemRequirements, SequenceBreakRequirements, WorldStateRequirements, OverworldNodes,
        ConnectionFactory);

    private static void PopulateExpectedValues()
    {
        ExpectedValues.Clear();

        foreach (OverworldNodeID id in Enum.GetValues(typeof(OverworldNodeID)))
        {
            var node = OverworldNodes[id];

            var connections = id switch
            {
                OverworldNodeID.DarkDeathMountainWestBottom => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.FluteInverted], node),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainEntryCaveDark], node,
                        WorldStateRequirements[WorldState.Inverted]),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainWestBottom], node,
                        WorldStateRequirements[WorldState.StandardOpen]),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainWestBottom], node,
                        ComplexRequirements[ComplexRequirementType.LWMirror]),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.DarkDeathMountainTop], node)
                },
                OverworldNodeID.DarkDeathMountainWestBottomInverted => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DarkDeathMountainWestBottom], node,
                        WorldStateRequirements[WorldState.Inverted])
                },
                OverworldNodeID.DarkDeathMountainWestBottomNonEntrance => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DarkDeathMountainWestBottom], node,
                        AlternativeRequirements[new HashSet<IRequirement>
                        {
                            EntranceShuffleRequirements[EntranceShuffle.None],
                            EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                        }])
                },
                OverworldNodeID.DarkDeathMountainWestBottomMirror => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DarkDeathMountainWestBottom], node,
                        ComplexRequirements[ComplexRequirementType.DWMirror])
                },
                OverworldNodeID.DarkDeathMountainWestBottomNotBunny => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DarkDeathMountainWestBottom], node,
                        ComplexRequirements[ComplexRequirementType.NotBunnyDW])
                },
                OverworldNodeID.SpikeCavePastHammerBlocks => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DarkDeathMountainWestBottomNotBunny], node,
                        ItemRequirements[(ItemType.Hammer, 1)])
                },
                OverworldNodeID.SpikeCavePastSpikes => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.SpikeCavePastHammerBlocks], node,
                        ComplexRequirements[ComplexRequirementType.SpikeCave])
                },
                OverworldNodeID.SpikeCaveChest => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.SpikeCavePastSpikes], node,
                        ItemRequirements[(ItemType.Gloves, 1)])
                },
                OverworldNodeID.DarkDeathMountainTop => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainWestTop], node,
                        ComplexRequirements[ComplexRequirementType.LWMirror]),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainEastTop], node,
                        ComplexRequirements[ComplexRequirementType.LWMirror]),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.DarkDeathMountainWestBottomInverted], node),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.SuperBunnyCave], node),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.DWFloatingIsland], node),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.DWTurtleRockTop], node)
                },
                OverworldNodeID.DarkDeathMountainTopInverted => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DarkDeathMountainTop], node,
                        WorldStateRequirements[WorldState.Inverted])
                },
                OverworldNodeID.DarkDeathMountainTopStandardOpen => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DarkDeathMountainTop], node,
                        WorldStateRequirements[WorldState.StandardOpen])
                },
                OverworldNodeID.DarkDeathMountainTopMirror => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DarkDeathMountainTop], node,
                        ComplexRequirements[ComplexRequirementType.DWMirror])
                },
                OverworldNodeID.DarkDeathMountainTopNotBunny => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DarkDeathMountainTop], node,
                        ComplexRequirements[ComplexRequirementType.NotBunnyDW])
                },
                OverworldNodeID.SuperBunnyCave => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DarkDeathMountainTop], node,
                        AlternativeRequirements[new HashSet<IRequirement>
                        {
                            EntranceShuffleRequirements[EntranceShuffle.None],
                            EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                        }]),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DarkDeathMountainEastBottom], node,
                        AlternativeRequirements[new HashSet<IRequirement>
                        {
                            EntranceShuffleRequirements[EntranceShuffle.None],
                            EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                        }])
                },
                OverworldNodeID.SuperBunnyCaveChests => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.SuperBunnyCave], node,
                        ComplexRequirements[ComplexRequirementType.NotBunnyDW]),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.SuperBunnyCave], node,
                        SequenceBreakRequirements[SequenceBreakType.SuperBunnyFallInHole])
                },
                OverworldNodeID.GanonsTowerEntrance => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.DarkDeathMountainTopInverted], node),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DarkDeathMountainTopStandardOpen], node,
                        CrystalRequirement)
                },
                OverworldNodeID.GanonsTowerEntranceStandardOpen => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.GanonsTowerEntrance], node,
                        WorldStateRequirements[WorldState.StandardOpen])
                },
                OverworldNodeID.DWFloatingIsland => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.LWFloatingIsland], node,
                        ComplexRequirements[ComplexRequirementType.LWMirror]),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.HookshotCaveEntrance], node,
                        AlternativeRequirements[new HashSet<IRequirement>
                        {
                            EntranceShuffleRequirements[EntranceShuffle.None],
                            EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                        }])
                },
                OverworldNodeID.HookshotCaveEntrance => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DarkDeathMountainTopNotBunny], node,
                        ItemRequirements[(ItemType.Gloves, 1)])
                },
                OverworldNodeID.HookshotCaveEntranceHookshot => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.HookshotCaveEntrance], node,
                        ItemRequirements[(ItemType.Hookshot, 1)])
                },
                OverworldNodeID.HookshotCaveEntranceHover => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.HookshotCaveEntrance], node,
                        ComplexRequirements[ComplexRequirementType.Hover])
                },
                OverworldNodeID.HookshotCaveBonkableChest => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.HookshotCaveEntranceHookshot], node),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.HookshotCaveEntrance], node,
                        ComplexRequirements[ComplexRequirementType.BonkOverLedge]),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.HookshotCaveEntranceHover], node)
                },
                OverworldNodeID.HookshotCaveBack => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.HookshotCaveEntranceHookshot], node),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.HookshotCaveEntranceHover], node)
                },
                OverworldNodeID.DWTurtleRockTop => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.LWTurtleRockTopStandardOpen], node,
                        ItemRequirements[(ItemType.Hammer, 1)]),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.DarkDeathMountainTopInverted], node)
                },
                OverworldNodeID.DWTurtleRockTopInverted => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DWTurtleRockTop], node,
                        WorldStateRequirements[WorldState.Inverted])
                },
                OverworldNodeID.DWTurtleRockTopNotBunny => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DWTurtleRockTop], node,
                        ComplexRequirements[ComplexRequirementType.NotBunnyDW])
                },
                OverworldNodeID.TurtleRockFrontEntrance => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DWTurtleRockTopNotBunny], node,
                        ComplexRequirements[ComplexRequirementType.TRMedallion])
                },
                OverworldNodeID.DarkDeathMountainEastBottom => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainEastBottom], node,
                        ComplexRequirements[ComplexRequirementType.LWMirror]),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainEastBottomLift2], node,
                        WorldStateRequirements[WorldState.StandardOpen]),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.DarkDeathMountainTop], node)
                },
                OverworldNodeID.DarkDeathMountainEastBottomInverted => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DarkDeathMountainEastBottom], node,
                        WorldStateRequirements[WorldState.Inverted])
                },
                OverworldNodeID.DarkDeathMountainEastBottomConnector => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DarkDeathMountainEastBottom], node,
                        ComplexRequirements[ComplexRequirementType.NotBunnyDW])
                },
                OverworldNodeID.TurtleRockTunnel => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.SpiralCaveLedge], node,
                        ComplexRequirements[ComplexRequirementType.LWMirror]),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.MimicCaveLedge], node,
                        ComplexRequirements[ComplexRequirementType.LWMirror])
                },
                OverworldNodeID.TurtleRockTunnelMirror => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.TurtleRockTunnel], node,
                        ComplexRequirements[ComplexRequirementType.DWMirror]),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.TRKeyDoorsToMiddleExit], node,
                        ComplexRequirements[ComplexRequirementType.DWMirror])
                },
                OverworldNodeID.TurtleRockSafetyDoor => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainEastTopConnector], node,
                        ComplexRequirements[ComplexRequirementType.LWMirror])
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
        var sut = scope.Resolve<IDWDeathMountainConnectionFactory>();
            
        Assert.NotNull(sut as DWDeathMountainConnectionFactory);
    }
}