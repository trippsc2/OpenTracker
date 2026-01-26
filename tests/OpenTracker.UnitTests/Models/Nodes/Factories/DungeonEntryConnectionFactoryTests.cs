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
using OpenTracker.Models.Prizes;
using OpenTracker.Models.Requirements.Complex;
using OpenTracker.Models.Requirements.Item;
using OpenTracker.Models.Requirements.Item.Prize;
using OpenTracker.Models.Requirements.Mode;
using OpenTracker.Models.Requirements.SequenceBreak;
using OpenTracker.Models.SequenceBreaks;
using Xunit;

namespace OpenTracker.UnitTests.Models.Nodes.Factories;

public class DungeonEntryConnectionFactoryTests
{
    private static readonly IComplexRequirementDictionary ComplexRequirements =
        Substitute.For<IComplexRequirementDictionary>();
    private static readonly IEntranceShuffleRequirementDictionary EntranceShuffleRequirements =
        Substitute.For<IEntranceShuffleRequirementDictionary>();
    private static readonly IItemRequirementDictionary ItemRequirements =
        Substitute.For<IItemRequirementDictionary>();
    private static readonly IPrizeRequirementDictionary PrizeRequirements =
        Substitute.For<IPrizeRequirementDictionary>();
    private static readonly ISequenceBreakRequirementDictionary SequenceBreakRequirements =
        Substitute.For<ISequenceBreakRequirementDictionary>();
    private static readonly IWorldStateRequirementDictionary WorldStateRequirements =
        Substitute.For<IWorldStateRequirementDictionary>();

    private static readonly IOverworldNodeDictionary OverworldNodes = Substitute.For<IOverworldNodeDictionary>();

    private static readonly INodeConnection.Factory ConnectionFactory =
        (fromNode, toNode, requirement) => new NodeConnection(fromNode, toNode, requirement);

    private static readonly Dictionary<OverworldNodeID, ExpectedObject> ExpectedValues = new();

    private readonly DungeonEntryConnectionFactory _sut = new(
        ComplexRequirements, EntranceShuffleRequirements, ItemRequirements, PrizeRequirements,
        SequenceBreakRequirements, WorldStateRequirements, OverworldNodes, ConnectionFactory);

    private static void PopulateExpectedValues()
    {
        ExpectedValues.Clear();

        foreach (OverworldNodeID id in Enum.GetValues(typeof(OverworldNodeID)))
        {
            var node = OverworldNodes[id];

            var connections = id switch
            {
                OverworldNodeID.GanonHole => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.HyruleCastleTopInverted], node,
                        PrizeRequirements[(PrizeType.Aga2, 1)]),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DarkWorldEastStandardOpen], node,
                        PrizeRequirements[(PrizeType.Aga2, 1)])
                },
                OverworldNodeID.HCSanctuaryEntry => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.LightWorldNotBunnyOrSuperBunnyMirror], node)
                },
                OverworldNodeID.HCFrontEntry => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.LightWorldNotBunnyOrDungeonRevive], node)
                },
                OverworldNodeID.HCBackEntry => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.EscapeGrave], node)
                },
                OverworldNodeID.ATEntry => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.AgahnimTowerEntrance], node,
                        WorldStateRequirements[WorldState.StandardOpen]),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.GanonsTowerEntrance], node,
                        WorldStateRequirements[WorldState.Inverted])
                },
                OverworldNodeID.EPEntry => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.LightWorldNotBunnyOrDungeonRevive], node)
                },
                OverworldNodeID.DPFrontEntry => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DesertPalaceFrontEntrance], node,
                        ComplexRequirements[ComplexRequirementType.NotBunnyLW]),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DesertPalaceFrontEntrance], node,
                        SequenceBreakRequirements[SequenceBreakType.DungeonRevive])
                },
                OverworldNodeID.DPLeftEntry => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.DesertLedge], node)
                },
                OverworldNodeID.DPBackEntry => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.DesertBack], node)
                },
                OverworldNodeID.ToHEntry => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.DeathMountainWestTopNotBunny], node),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DeathMountainWestTop], node,
                        SequenceBreakRequirements[SequenceBreakType.DungeonRevive])
                },
                OverworldNodeID.PoDEntry => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.DarkWorldEastNotBunny], node)
                },
                OverworldNodeID.SPEntry => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.LightWorldInvertedNotBunny], node,
                        ItemRequirements[(ItemType.Mirror, 1)]),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DarkWorldSouthStandardOpenNotBunny], node,
                        ItemRequirements[(ItemType.Mirror, 1)])
                },
                OverworldNodeID.SWFrontEntry => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.DarkWorldWestNotBunny], node),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.DarkWorldWest], node,
                        SequenceBreakRequirements[SequenceBreakType.DungeonRevive]),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.Start], node,
                        EntranceShuffleRequirements[EntranceShuffle.Insanity])
                },
                OverworldNodeID.SWBackEntry => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.SkullWoodsBack], node)
                },
                OverworldNodeID.TTEntry => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.DarkWorldWestNotBunny], node)
                },
                OverworldNodeID.IPEntry => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.IcePalaceIsland], node,
                        ComplexRequirements[ComplexRequirementType.NotBunnyDW]),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.IcePalaceIsland], node,
                        SequenceBreakRequirements[SequenceBreakType.DungeonRevive])
                },
                OverworldNodeID.MMEntry => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.MiseryMireEntrance], node)
                },
                OverworldNodeID.TRFrontEntry => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.TurtleRockFrontEntrance], node)
                },
                OverworldNodeID.TRFrontEntryStandardOpen => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.TRFrontEntry], node,
                        WorldStateRequirements[WorldState.StandardOpen])
                },
                OverworldNodeID.TRFrontEntryStandardOpenEntranceNone => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.TRFrontEntryStandardOpen], node,
                        EntranceShuffleRequirements[EntranceShuffle.None])
                },
                OverworldNodeID.TRFrontToKeyDoors => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.TRFrontEntryStandardOpenEntranceNone], node,
                        ItemRequirements[(ItemType.CaneOfSomaria, 1)])
                },
                OverworldNodeID.TRKeyDoorsToMiddleExit => new List<INodeConnection>
                {
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.TRFrontToKeyDoors], node,
                        ComplexRequirements[ComplexRequirementType.TRKeyDoorsToMiddleExit])
                },
                OverworldNodeID.TRMiddleEntry => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.TurtleRockTunnel], node)
                },
                OverworldNodeID.TRBackEntry => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    ConnectionFactory(OverworldNodes[OverworldNodeID.TurtleRockSafetyDoor], node)
                },
                OverworldNodeID.GTEntry => new List<INodeConnection>
                {
                    ConnectionFactory(OverworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.AgahnimTowerEntrance], node,
                        WorldStateRequirements[WorldState.Inverted]),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.GanonsTowerEntranceStandardOpen], node,
                        ComplexRequirements[ComplexRequirementType.NotBunnyDW]),
                    ConnectionFactory(
                        OverworldNodes[OverworldNodeID.GanonsTowerEntranceStandardOpen], node,
                        SequenceBreakRequirements[SequenceBreakType.DungeonRevive])
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
        var sut = scope.Resolve<IDungeonEntryConnectionFactory>();
            
        Assert.NotNull(sut as DungeonEntryConnectionFactory);
    }
}