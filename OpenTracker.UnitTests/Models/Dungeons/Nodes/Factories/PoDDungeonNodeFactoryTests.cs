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
public sealed class PoDDungeonNodeFactoryTests
{
    private readonly IBossRequirementDictionary _bossRequirements;
    private readonly IComplexRequirementDictionary _complexRequirements;
    private readonly IItemRequirementDictionary _itemRequirements;
    private readonly ISequenceBreakRequirementDictionary _sequenceBreakRequirements;

    private readonly IOverworldNodeDictionary _overworldNodes;

    private readonly PoDDungeonNodeFactory _sut;

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

    public PoDDungeonNodeFactoryTests()
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

        _sut = new PoDDungeonNodeFactory(
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
                case DungeonNodeID.PoD:
                    ExpectedEntryValues.Add(id, new List<OverworldNodeID> {OverworldNodeID.PoDEntry});
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.PoDLobbyArena, KeyDoorID.PoDFrontKeyDoor)
                        });
                    break;
                case DungeonNodeID.PoDPastFirstRedGoriyaRoom:
                    ExpectedComplexValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ComplexRequirementType type)>
                        {
                            (DungeonNodeID.PoD, ComplexRequirementType.RedEyegoreGoriya),
                            (DungeonNodeID.PoD, ComplexRequirementType.CameraUnlock),
                        });
                    ExpectedSequenceBreakValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, SequenceBreakType type)>
                        {
                            (DungeonNodeID.PoD, SequenceBreakType.MimicClip)
                        });
                    break;
                case DungeonNodeID.PoDFrontKeyDoor:
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.PoD,
                        DungeonNodeID.PoDLobbyArena
                    });
                    break;
                case DungeonNodeID.PoDLobbyArena:
                    ExpectedItemValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ItemType type, int count)>
                        {
                            (DungeonNodeID.PoDPastFirstRedGoriyaRoom, ItemType.Hammer, 1)
                        });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.PoD, KeyDoorID.PoDFrontKeyDoor),
                            (DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor, KeyDoorID.PoDCollapsingWalkwayKeyDoor)
                        });
                    break;
                case DungeonNodeID.PoDBigKeyChestArea:
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.PoDLobbyArena, KeyDoorID.PoDBigKeyChestKeyDoor)
                        });
                    break;
                case DungeonNodeID.PoDCollapsingWalkwayKeyDoor:
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.PoDLobbyArena,
                        DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor
                    });
                    break;
                case DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor:
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.PoDLobbyArena, KeyDoorID.PoDCollapsingWalkwayKeyDoor),
                            (DungeonNodeID.PoDPastDarkMazeKeyDoor, KeyDoorID.PoDDarkMazeKeyDoor),
                            (DungeonNodeID.PoDHarmlessHellwayRoom, KeyDoorID.PoDHarmlessHellwayKeyDoor)
                        });
                    break;
                case DungeonNodeID.PoDDarkBasement:
                    ExpectedComplexValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ComplexRequirementType type)>
                        {
                            (DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor, ComplexRequirementType.DarkRoomPoDDarkBasement)
                        });
                    break;
                case DungeonNodeID.PoDHarmlessHellwayKeyDoor:
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor,
                        DungeonNodeID.PoDHarmlessHellwayRoom
                    });
                    break;
                case DungeonNodeID.PoDHarmlessHellwayRoom:
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor, KeyDoorID.PoDHarmlessHellwayKeyDoor)
                        });
                    break;
                case DungeonNodeID.PoDDarkMazeKeyDoor:
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor,
                        DungeonNodeID.PoDPastDarkMazeKeyDoor
                    });
                    break;
                case DungeonNodeID.PoDPastDarkMazeKeyDoor:
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.PoDDarkMaze
                    });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor, KeyDoorID.PoDDarkMazeKeyDoor)
                        });
                    break;
                case DungeonNodeID.PoDDarkMaze:
                    ExpectedComplexValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ComplexRequirementType type)>
                        {
                            (DungeonNodeID.PoDPastDarkMazeKeyDoor, ComplexRequirementType.DarkRoomPoDDarkMaze),
                            (DungeonNodeID.PoDBigChestLedge, ComplexRequirementType.DarkRoomPoDDarkMaze)
                        });
                    break;
                case DungeonNodeID.PoDBigChestLedge:
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.PoDDarkMaze
                    });
                    ExpectedSequenceBreakValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, SequenceBreakType type)>
                        {
                            (DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor, SequenceBreakType.BombJumpPoDHammerJump)
                        });
                    break;
                case DungeonNodeID.PoDBigChest:
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.PoDBigChestLedge, KeyDoorID.PoDBigChest)
                        });
                    break;
                case DungeonNodeID.PoDPastSecondRedGoriyaRoom:
                    ExpectedComplexValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ComplexRequirementType type)>
                        {
                            (DungeonNodeID.PoDLobbyArena, ComplexRequirementType.RedEyegoreGoriya)
                        });
                    ExpectedSequenceBreakValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, SequenceBreakType type)>
                        {
                            (DungeonNodeID.PoDLobbyArena, SequenceBreakType.MimicClip)
                        });
                    break;
                case DungeonNodeID.PoDPastBowStatue:
                    ExpectedItemValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ItemType type, int count)>
                        {
                            (DungeonNodeID.PoDPastSecondRedGoriyaRoom, ItemType.Bow, 1)
                        });
                    break;
                case DungeonNodeID.PoDBossAreaDarkRooms:
                    ExpectedComplexValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ComplexRequirementType type)>
                        {
                            (DungeonNodeID.PoDPastBowStatue, ComplexRequirementType.DarkRoomPoDBossArea)
                        });
                    break;
                case DungeonNodeID.PoDPastHammerBlocks:
                    ExpectedItemValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ItemType type, int count)>
                        {
                            (DungeonNodeID.PoDBossAreaDarkRooms, ItemType.Hammer, 1)
                        });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.PoDPastBossAreaKeyDoor, KeyDoorID.PoDBossAreaKeyDoor)
                        });
                    break;
                case DungeonNodeID.PoDBossAreaKeyDoor:
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.PoDPastHammerBlocks,
                        DungeonNodeID.PoDPastBossAreaKeyDoor
                    });
                    break;
                case DungeonNodeID.PoDPastBossAreaKeyDoor:
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.PoDPastHammerBlocks, KeyDoorID.PoDBossAreaKeyDoor)
                        });
                    break;
                case DungeonNodeID.PoDBossRoom:
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.PoDPastBossAreaKeyDoor, KeyDoorID.PoDBigKeyDoor)
                        });
                    break;
                case DungeonNodeID.PoDBoss:
                    ExpectedBossValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, BossPlacementID bossID)>
                        {
                            (DungeonNodeID.PoDBossRoom, BossPlacementID.PoDBoss)
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
        var sut = scope.Resolve<IPoDDungeonNodeFactory>();
            
        Assert.NotNull(sut as PoDDungeonNodeFactory);
    }
}