using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Autofac;
using NSubstitute;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Dungeons.KeyDoors;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Dungeons.Nodes;
using OpenTracker.Models.Dungeons.Nodes.Factories;
using OpenTracker.Models.Items;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Nodes.Connections;
using OpenTracker.Models.Nodes.Factories;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Boss;
using OpenTracker.Models.Requirements.Complex;
using OpenTracker.Models.Requirements.Item;
using OpenTracker.Models.Requirements.SequenceBreak;
using OpenTracker.Models.SequenceBreaks;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.Nodes.Factories;

[ExcludeFromCodeCoverage]
public sealed class SWDungeonNodeFactoryTests
{
    private readonly IBossRequirementDictionary _bossRequirements;
    private readonly IComplexRequirementDictionary _complexRequirements;
    private readonly IItemRequirementDictionary _itemRequirements;
    private readonly ISequenceBreakRequirementDictionary _sequenceBreakRequirements;

    private readonly IOverworldNodeDictionary _overworldNodes;

    private readonly SWDungeonNodeFactory _sut;

    private readonly List<INode> _entryFactoryCalls = new();
    private readonly List<(INode fromNode, INode toNode, IRequirement? requirement)> _connectionFactoryCalls = new();

    private static readonly Dictionary<DungeonNodeID, List<OverworldNodeID>> ExpectedEntryValues = new();
    private static readonly Dictionary<DungeonNodeID, List<DungeonNodeID>> ExpectedNoRequirementValues = new();
    private static readonly Dictionary<DungeonNodeID,
        List<(DungeonNodeID fromNodeID, BossPlacementID bossID)>> ExpectedBossValues = new();
    private static readonly Dictionary<DungeonNodeID,
        List<(DungeonNodeID fromNodeID, ComplexRequirementType type)>> ExpectedComplexValues = new();
    private static readonly Dictionary<DungeonNodeID,
        List<(DungeonNodeID fromNodeID, ItemType type, int count)>> ExpectedItemValues = new();
    private static readonly Dictionary<DungeonNodeID,
        List<(DungeonNodeID fromNodeID, SequenceBreakType type)>> ExpectedSequenceBreakValues = new();
    private static readonly Dictionary<DungeonNodeID,
        List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>> ExpectedKeyDoorValues = new();

    public SWDungeonNodeFactoryTests()
    {
        _bossRequirements = new BossRequirementDictionary(
            Substitute.For<IBossPlacementDictionary>(),
            _ => Substitute.For<IBossRequirement>());
        _complexRequirements = new ComplexRequirementDictionary(
            () => Substitute.For<IComplexRequirementFactory>());
        _itemRequirements = new ItemRequirementDictionary(
            Substitute.For<IItemDictionary>(), (item, count) => new ItemRequirement(item, count));
        _sequenceBreakRequirements = new SequenceBreakRequirementDictionary(
            Substitute.For<ISequenceBreakDictionary>(),
            sequenceBreak => new SequenceBreakRequirement(sequenceBreak));

        _overworldNodes = new OverworldNodeDictionary(() => Substitute.For<IOverworldNodeFactory>());

        IEntryNodeConnection EntryFactory(INode fromNode)
        {
            _entryFactoryCalls.Add(fromNode);
            return Substitute.For<IEntryNodeConnection>();
        }

        INodeConnection ConnectionFactory(INode fromNode, INode toNode, IRequirement? requirement)
        {
            _connectionFactoryCalls.Add((fromNode, toNode, requirement));
            return Substitute.For<INodeConnection>();
        }


        _sut = new SWDungeonNodeFactory(
            _bossRequirements, _complexRequirements, _itemRequirements, _sequenceBreakRequirements, _overworldNodes,
            EntryFactory, ConnectionFactory);
    }

    private static void PopulateExpectedValues()
    {
        ExpectedEntryValues.Clear();
        ExpectedNoRequirementValues.Clear();
        ExpectedBossValues.Clear();
        ExpectedComplexValues.Clear();
        ExpectedItemValues.Clear();
        ExpectedSequenceBreakValues.Clear();
        ExpectedKeyDoorValues.Clear();
            
        foreach (DungeonNodeID id in Enum.GetValues(typeof(DungeonNodeID)))
        {
            switch (id)
            {
                case DungeonNodeID.SWBigChestAreaBottom:
                    ExpectedEntryValues.Add(id, new List<OverworldNodeID> {OverworldNodeID.SWFrontEntry});
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.SWBigChestAreaTop
                    });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.SWFrontLeftSide, KeyDoorID.SWFrontLeftKeyDoor),
                            (DungeonNodeID.SWFrontRightSide, KeyDoorID.SWFrontRightKeyDoor)
                        });
                    break;
                case DungeonNodeID.SWBigChestAreaTop:
                    ExpectedEntryValues.Add(id, new List<OverworldNodeID> {OverworldNodeID.SWFrontEntry});
                    ExpectedItemValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ItemType type, int count)>
                        {
                            (DungeonNodeID.SWBigChestAreaBottom, ItemType.Hookshot, 1)
                        });
                    ExpectedSequenceBreakValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, SequenceBreakType type)>
                        {
                            (DungeonNodeID.SWBigChestAreaBottom, SequenceBreakType.BombJumpSWBigChest)
                        });
                    break;
                case DungeonNodeID.SWBigChest:
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.SWBigChestAreaTop, KeyDoorID.SWBigChest)
                        });
                    break;
                case DungeonNodeID.SWFrontLeftKeyDoor:
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.SWFrontLeftSide,
                        DungeonNodeID.SWBigChestAreaBottom
                    });
                    break;
                case DungeonNodeID.SWFrontLeftSide:
                    ExpectedEntryValues.Add(id, new List<OverworldNodeID> {OverworldNodeID.SWFrontEntry});
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.SWBigChestAreaBottom, KeyDoorID.SWFrontLeftKeyDoor)
                        });
                    break;
                case DungeonNodeID.SWFrontRightKeyDoor:
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.SWFrontRightSide,
                        DungeonNodeID.SWBigChestAreaBottom
                    });
                    break;
                case DungeonNodeID.SWFrontRightSide:
                    ExpectedEntryValues.Add(id, new List<OverworldNodeID> {OverworldNodeID.SWFrontEntry});
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.SWFrontLeftSide
                    });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.SWBigChestAreaBottom, KeyDoorID.SWFrontRightKeyDoor)
                        });
                    break;
                case DungeonNodeID.SWFrontBackConnector:
                    ExpectedEntryValues.Add(id, new List<OverworldNodeID> {OverworldNodeID.SWFrontEntry});
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.SWPastTheWorthlessKeyDoor, KeyDoorID.SWWorthlessKeyDoor)
                        });
                    break;
                case DungeonNodeID.SWWorthlessKeyDoor:
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.SWFrontBackConnector,
                        DungeonNodeID.SWPastTheWorthlessKeyDoor
                    });
                    break;
                case DungeonNodeID.SWPastTheWorthlessKeyDoor:
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.SWFrontBackConnector, KeyDoorID.SWWorthlessKeyDoor)
                        });
                    break;
                case DungeonNodeID.SWBack:
                    ExpectedEntryValues.Add(id, new List<OverworldNodeID> {OverworldNodeID.SWBackEntry});
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.SWBackFirstKeyDoor, KeyDoorID.SWBackFirstKeyDoor)
                        });
                    break;
                case DungeonNodeID.SWBackFirstKeyDoor:
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.SWBack,
                        DungeonNodeID.SWBackPastFirstKeyDoor
                    });
                    break;
                case DungeonNodeID.SWBackPastFirstKeyDoor:
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.SWBack, KeyDoorID.SWBackFirstKeyDoor)
                        });
                    break;
                case DungeonNodeID.SWBackPastFourTorchRoom:
                    ExpectedItemValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ItemType type, int count)>
                        {
                            (DungeonNodeID.SWBackPastFirstKeyDoor, ItemType.FireRod, 1)
                        });
                    break;
                case DungeonNodeID.SWBackPastCurtains:
                    ExpectedComplexValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ComplexRequirementType type)>
                        {
                            (DungeonNodeID.SWBackPastFourTorchRoom, ComplexRequirementType.Curtains)
                        });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.SWBossRoom, KeyDoorID.SWBackSecondKeyDoor)
                        });
                    break;
                case DungeonNodeID.SWBackSecondKeyDoor:
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.SWBackPastCurtains,
                        DungeonNodeID.SWBossRoom
                    });
                    break;
                case DungeonNodeID.SWBossRoom:
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.SWBackPastCurtains, KeyDoorID.SWBackSecondKeyDoor)
                        });
                    break;
                case DungeonNodeID.SWBoss:
                    ExpectedBossValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, BossPlacementID bossID)>
                        {
                            (DungeonNodeID.SWBossRoom, BossPlacementID.SWBoss)
                        });
                    break;
            }
        }
    }

    [Fact]
    public void PopulateNodeConnections_ShouldThrowException_WhenNodeIDIsUnexpected()
    {
        var dungeonData = Substitute.For<IMutableDungeon>();
        var node = Substitute.For<IDungeonNode>();
        var connections = new List<INodeConnection>();
        const DungeonNodeID id = (DungeonNodeID)int.MaxValue;

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            _sut.PopulateNodeConnections(dungeonData, id, node, connections));
    }
        
    [Theory]
    [MemberData(nameof(PopulateNodeConnections_ShouldCreateExpectedEntryConnectionsData))]
    public void PopulateNodeConnections_ShouldCreateExpectedEntryConnections(
        DungeonNodeID id, OverworldNodeID fromNodeID)
    {
        var dungeonData = Substitute.For<IMutableDungeon>();
        var node = Substitute.For<IDungeonNode>();
        var connections = new List<INodeConnection>();
        _sut.PopulateNodeConnections(dungeonData, id, node, connections);

        Assert.Contains(_entryFactoryCalls, x => x == _overworldNodes[fromNodeID]);
    }

    public static IEnumerable<object[]> PopulateNodeConnections_ShouldCreateExpectedEntryConnectionsData()
    {
        PopulateExpectedValues();

        return (from id in ExpectedEntryValues.Keys
            from value in ExpectedEntryValues[id]
            select new object[] {id, value}).ToList();
    }
        
    [Theory]
    [MemberData(nameof(PopulateNodeConnections_ShouldCreateExpectedNoRequirementConnectionsData))]
    public void PopulateNodeConnections_ShouldCreateExpectedNoRequirementConnections(
        DungeonNodeID id, DungeonNodeID fromNodeID)
    {
        var dungeonData = Substitute.For<IMutableDungeon>();
        var node = Substitute.For<IDungeonNode>();
        var connections = new List<INodeConnection>();
        _sut.PopulateNodeConnections(dungeonData, id, node, connections);

        Assert.Contains(_connectionFactoryCalls, x =>
            x.fromNode == dungeonData.Nodes[fromNodeID] && x.requirement == null);
    }

    public static IEnumerable<object[]> PopulateNodeConnections_ShouldCreateExpectedNoRequirementConnectionsData()
    {
        PopulateExpectedValues();

        return (from id in ExpectedNoRequirementValues.Keys
            from value in ExpectedNoRequirementValues[id]
            select new object[] {id, value}).ToList();
    }

    [Theory]
    [MemberData(nameof(PopulateNodeConnections_ShouldCreateExpectedBossConnectionsData))]
    public void PopulateNodeConnections_ShouldCreateExpectedBossConnections(
        DungeonNodeID id, DungeonNodeID fromNodeID, BossPlacementID bossID)
    {
        var dungeonData = Substitute.For<IMutableDungeon>();
        var node = Substitute.For<IDungeonNode>();
        var connections = new List<INodeConnection>();
        _sut.PopulateNodeConnections(dungeonData, id, node, connections);

        Assert.Contains(_connectionFactoryCalls, x =>
            x.fromNode == dungeonData.Nodes[fromNodeID] && x.requirement == _bossRequirements[bossID]);
    }

    public static IEnumerable<object[]> PopulateNodeConnections_ShouldCreateExpectedBossConnectionsData()
    {
        PopulateExpectedValues();

        return (from id in ExpectedBossValues.Keys from value in ExpectedBossValues[id]
            select new object[] {id, value.fromNodeID, value.bossID}).ToList();
    }

    [Theory]
    [MemberData(nameof(PopulateNodeConnections_ShouldCreateExpectedComplexConnectionsData))]
    public void PopulateNodeConnections_ShouldCreateExpectedComplexConnections(
        DungeonNodeID id, DungeonNodeID fromNodeID, ComplexRequirementType type)
    {
        var dungeonData = Substitute.For<IMutableDungeon>();
        var node = Substitute.For<IDungeonNode>();
        var connections = new List<INodeConnection>();
        _sut.PopulateNodeConnections(dungeonData, id, node, connections);

        Assert.Contains(_connectionFactoryCalls, x =>
            x.fromNode == dungeonData.Nodes[fromNodeID] && x.requirement == _complexRequirements[type]);
    }

    public static IEnumerable<object[]> PopulateNodeConnections_ShouldCreateExpectedComplexConnectionsData()
    {
        PopulateExpectedValues();

        return (from id in ExpectedComplexValues.Keys from value in ExpectedComplexValues[id]
            select new object[] {id, value.fromNodeID, value.type}).ToList();
    }

    [Theory]
    [MemberData(nameof(PopulateNodeConnections_ShouldCreateExpectedItemConnectionsData))]
    public void PopulateNodeConnections_ShouldCreateExpectedItemConnections(
        DungeonNodeID id, DungeonNodeID fromNodeID, ItemType type, int count)
    {
        var dungeonData = Substitute.For<IMutableDungeon>();
        var node = Substitute.For<IDungeonNode>();
        var connections = new List<INodeConnection>();
        _sut.PopulateNodeConnections(dungeonData, id, node, connections);

        Assert.Contains(_connectionFactoryCalls, x =>
            x.fromNode == dungeonData.Nodes[fromNodeID] && x.requirement == _itemRequirements[(type, count)]);
    }

    public static IEnumerable<object[]> PopulateNodeConnections_ShouldCreateExpectedItemConnectionsData()
    {
        PopulateExpectedValues();

        return (from id in ExpectedItemValues.Keys from value in ExpectedItemValues[id]
            select new object[] {id, value.fromNodeID, value.type, value.count}).ToList();
    }

    [Theory]
    [MemberData(nameof(PopulateNodeConnections_ShouldCreateExpectedSequenceBreakConnectionsData))]
    public void PopulateNodeConnections_ShouldCreateExpectedSequenceBreakConnections(
        DungeonNodeID id, DungeonNodeID fromNodeID, SequenceBreakType type)
    {
        var dungeonData = Substitute.For<IMutableDungeon>();
        var node = Substitute.For<IDungeonNode>();
        var connections = new List<INodeConnection>();
        _sut.PopulateNodeConnections(dungeonData, id, node, connections);

        Assert.Contains(_connectionFactoryCalls, x =>
            x.fromNode == dungeonData.Nodes[fromNodeID] && x.requirement == _sequenceBreakRequirements[type]);
    }

    public static IEnumerable<object[]> PopulateNodeConnections_ShouldCreateExpectedSequenceBreakConnectionsData()
    {
        PopulateExpectedValues();

        return (from id in ExpectedSequenceBreakValues.Keys from value in ExpectedSequenceBreakValues[id]
            select new object[] {id, value.fromNodeID, value.type}).ToList();
    }

    [Theory]
    [MemberData(nameof(PopulateNodeConnections_ShouldCreateExpectedKeyDoorConnectionsData))]
    public void PopulateNodeConnections_ShouldCreateExpectedKeyDoorConnections(
        DungeonNodeID id, DungeonNodeID fromNodeID, KeyDoorID keyDoor)
    {
        var dungeonData = Substitute.For<IMutableDungeon>();
        var node = Substitute.For<IDungeonNode>();
        var connections = new List<INodeConnection>();
        _sut.PopulateNodeConnections(dungeonData, id, node, connections);

        Assert.Contains(_connectionFactoryCalls, x =>
            x.fromNode == dungeonData.Nodes[fromNodeID] &&
            x.requirement == dungeonData.KeyDoors[keyDoor].Requirement);
    }
        
    public static IEnumerable<object[]> PopulateNodeConnections_ShouldCreateExpectedKeyDoorConnectionsData()
    {
        PopulateExpectedValues();

        return (from id in ExpectedKeyDoorValues.Keys from value in ExpectedKeyDoorValues[id]
            select new object[] {id, value.fromNodeID, value.keyDoor}).ToList();
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut = scope.Resolve<ISWDungeonNodeFactory>();
            
        Assert.NotNull(sut as SWDungeonNodeFactory);
    }
}