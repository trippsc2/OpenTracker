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
public sealed class IPDungeonNodeFactoryTests
{
    private readonly IBossRequirementDictionary _bossRequirements;
    private readonly IComplexRequirementDictionary _complexRequirements;
    private readonly IItemRequirementDictionary _itemRequirements;
    private readonly ISequenceBreakRequirementDictionary _sequenceBreakRequirements;

    private readonly IOverworldNodeDictionary _overworldNodes;

    private readonly IPDungeonNodeFactory _sut;

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

    public IPDungeonNodeFactoryTests()
    {
        _bossRequirements = new BossRequirementDictionary(
            Substitute.For<IBossPlacementDictionary>(),
            _ => Substitute.For<IBossRequirement>());
        _complexRequirements = new ComplexRequirementDictionary(
            () => Substitute.For<IComplexRequirementFactory>());
        _itemRequirements = new ItemRequirementDictionary(
            Substitute.For<IItemDictionary>(), (_, _) => Substitute.For<IItemRequirement>());
        _sequenceBreakRequirements = new SequenceBreakRequirementDictionary(
            Substitute.For<ISequenceBreakDictionary>(),
            _ => Substitute.For<ISequenceBreakRequirement>());
            
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

        _sut = new IPDungeonNodeFactory(
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
                case DungeonNodeID.IP:
                    ExpectedEntryValues.Add(id, new List<OverworldNodeID> {OverworldNodeID.IPEntry});
                    break;
                case DungeonNodeID.IPPastEntranceFreezorRoom:
                    ExpectedComplexValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ComplexRequirementType type)>
                        {
                            (DungeonNodeID.IP, ComplexRequirementType.MeltThings)
                        });
                    break;
                case DungeonNodeID.IPB1LeftSide:
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.IPPastEntranceFreezorRoom, KeyDoorID.IP1FKeyDoor)
                        });
                    break;
                case DungeonNodeID.IPB1RightSide:
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.IPB2PastLiftBlock
                    });
                    ExpectedComplexValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ComplexRequirementType type)>
                        {
                            (DungeonNodeID.IPB1LeftSide, ComplexRequirementType.IPIceBreaker)
                        });
                    break;
                case DungeonNodeID.IPB2LeftSide:
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.IPB1LeftSide
                    });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.IPB2PastKeyDoor, KeyDoorID.IPB2KeyDoor)
                        });
                    break;
                case DungeonNodeID.IPB2KeyDoor:
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.IPB2LeftSide,
                        DungeonNodeID.IPB2PastKeyDoor
                    });
                    break;
                case DungeonNodeID.IPB2PastKeyDoor:
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.IPB4FreezorRoom
                    });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.IPB2LeftSide, KeyDoorID.IPB2KeyDoor),
                            (DungeonNodeID.IPSpikeRoom, KeyDoorID.IPB3KeyDoor)
                        });
                    break;
                case DungeonNodeID.IPB2PastHammerBlocks:
                    ExpectedItemValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ItemType type, int count)>
                        {
                            (DungeonNodeID.IPSpikeRoom, ItemType.Hammer, 1)
                        });
                    break;
                case DungeonNodeID.IPB2PastLiftBlock:
                    ExpectedItemValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ItemType type, int count)>
                        {
                            (DungeonNodeID.IPB2PastHammerBlocks, ItemType.Gloves, 1)
                        });
                    break;
                case DungeonNodeID.IPB3KeyDoor:
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.IPB2PastKeyDoor,
                        DungeonNodeID.IPSpikeRoom
                    });
                    break;
                case DungeonNodeID.IPSpikeRoom:
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.IPB1RightSide,
                        DungeonNodeID.IPB4RightSide
                    });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.IPB2PastKeyDoor, KeyDoorID.IPB3KeyDoor)
                        });
                    break;
                case DungeonNodeID.IPB4RightSide:
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.IPSpikeRoom
                    });
                    ExpectedComplexValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ComplexRequirementType type)>
                        {
                            (DungeonNodeID.IPB4IceRoom, ComplexRequirementType.Hover)
                        });
                    ExpectedItemValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ItemType type, int count)>
                        {
                            (DungeonNodeID.IPB4IceRoom, ItemType.Hookshot, 1)
                        });
                    ExpectedSequenceBreakValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, SequenceBreakType type)>
                        {
                            (DungeonNodeID.IPB4IceRoom, SequenceBreakType.BombJumpIPHookshotGap)
                        });
                    break;
                case DungeonNodeID.IPB4IceRoom:
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.IPB2PastKeyDoor
                    });
                    ExpectedComplexValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ComplexRequirementType type)>
                        {
                            (DungeonNodeID.IPB4FreezorRoom, ComplexRequirementType.Hover)
                        });
                    ExpectedSequenceBreakValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, SequenceBreakType type)>
                        {
                            (DungeonNodeID.IPB4FreezorRoom, SequenceBreakType.BombJumpIPFreezorRoomGap)
                        });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.IPB4PastKeyDoor, KeyDoorID.IPB4KeyDoor)
                        });
                    break;
                case DungeonNodeID.IPB4FreezorRoom:
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.IPB2PastKeyDoor
                    });
                    ExpectedComplexValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ComplexRequirementType type)>
                        {
                            (DungeonNodeID.IPB4IceRoom, ComplexRequirementType.Hover)
                        });
                    ExpectedSequenceBreakValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, SequenceBreakType type)>
                        {
                            (DungeonNodeID.IPB4IceRoom, SequenceBreakType.BombJumpIPFreezorRoomGap)
                        });
                    break;
                case DungeonNodeID.IPFreezorChest:
                    ExpectedComplexValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ComplexRequirementType type)>
                        {
                            (DungeonNodeID.IPB4FreezorRoom, ComplexRequirementType.MeltThings)
                        });
                    break;
                case DungeonNodeID.IPB4KeyDoor:
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.IPB4IceRoom,
                        DungeonNodeID.IPB4PastKeyDoor
                    });
                    break;
                case DungeonNodeID.IPB4PastKeyDoor:
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.IPB5
                    });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.IPB4IceRoom, KeyDoorID.IPB4KeyDoor)
                        });
                    break;
                case DungeonNodeID.IPBigChestArea:
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.IPB4FreezorRoom
                    });
                    break;
                case DungeonNodeID.IPBigChest:
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.IPBigChestArea, KeyDoorID.IPBigChest)
                        });
                    break;
                case DungeonNodeID.IPB5:
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.IPB4FreezorRoom,
                        DungeonNodeID.IPB4PastKeyDoor
                    });
                    break;
                case DungeonNodeID.IPB5PastBigKeyDoor:
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.IPB5, KeyDoorID.IPBigKeyDoor)
                        });
                    break;
                case DungeonNodeID.IPB6:
                    ExpectedComplexValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ComplexRequirementType type)>
                        {
                            (DungeonNodeID.IPB5, ComplexRequirementType.IPIceBreaker)
                        });
                    ExpectedSequenceBreakValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, SequenceBreakType type)>
                        {
                            (DungeonNodeID.IPB5, SequenceBreakType.BombJumpIPBJ)
                        });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.IPB5PastBigKeyDoor, KeyDoorID.IPB5KeyDoor),
                            (DungeonNodeID.IPB6PastKeyDoor, KeyDoorID.IPB6KeyDoor)
                        });
                    break;
                case DungeonNodeID.IPB6KeyDoor:
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.IPB6,
                        DungeonNodeID.IPB6PastKeyDoor
                    });
                    break;
                case DungeonNodeID.IPB6PastKeyDoor:
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.IPB6, KeyDoorID.IPB6KeyDoor)
                        });
                    break;
                case DungeonNodeID.IPB6PreBossRoom:
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.IPB6PastKeyDoor
                    });
                    ExpectedComplexValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ComplexRequirementType type)>
                        {
                            (DungeonNodeID.IPB6, ComplexRequirementType.IPIceBreaker)
                        });
                    ExpectedItemValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ItemType type, int count)>
                        {
                            (DungeonNodeID.IPB6, ItemType.CaneOfSomaria, 1)
                        });
                    ExpectedSequenceBreakValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, SequenceBreakType type)>
                        {
                            (DungeonNodeID.IPB6, SequenceBreakType.BombJumpIPBJ)
                        });
                    break;
                case DungeonNodeID.IPB6PastHammerBlocks:
                    ExpectedItemValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ItemType type, int count)>
                        {
                            (DungeonNodeID.IPB6PreBossRoom, ItemType.Hammer, 1)
                        });
                    break;
                case DungeonNodeID.IPB6PastLiftBlock:
                    ExpectedItemValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ItemType type, int count)>
                        {
                            (DungeonNodeID.IPB6PastHammerBlocks, ItemType.Gloves, 1)
                        });
                    break;
                case DungeonNodeID.IPBoss:
                    ExpectedBossValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, BossPlacementID bossID)>
                        {
                            (DungeonNodeID.IPB6PastLiftBlock, BossPlacementID.IPBoss)
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
        var sut = scope.Resolve<IIPDungeonNodeFactory>();
            
        Assert.NotNull(sut as IPDungeonNodeFactory);
    }
}