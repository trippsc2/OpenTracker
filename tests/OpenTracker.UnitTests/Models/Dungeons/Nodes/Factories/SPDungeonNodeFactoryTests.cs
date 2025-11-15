using System;
using System.Collections.Generic;
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
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.Nodes.Factories
{
    public class SPDungeonNodeFactoryTests
    {
        private readonly IBossRequirementDictionary _bossRequirements;
        private readonly IComplexRequirementDictionary _complexRequirements;
        private readonly IItemRequirementDictionary _itemRequirements;

        private readonly IOverworldNodeDictionary _overworldNodes;
        
        private readonly SPDungeonNodeFactory _sut;

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
            List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>> ExpectedKeyDoorValues = new();

        public SPDungeonNodeFactoryTests()
        {
            _bossRequirements = new BossRequirementDictionary(
                Substitute.For<IBossPlacementDictionary>(),
                _ => Substitute.For<IBossRequirement>());
            _complexRequirements = new ComplexRequirementDictionary(
                () => Substitute.For<IComplexRequirementFactory>());
            _itemRequirements = new ItemRequirementDictionary(
                Substitute.For<IItemDictionary>(), (_, _) => Substitute.For<IItemRequirement>());

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

            _sut = new SPDungeonNodeFactory(
                _bossRequirements, _complexRequirements, _itemRequirements, _overworldNodes, EntryFactory, ConnectionFactory);
        }

        private static void PopulateExpectedValues()
        {
            ExpectedEntryValues.Clear();
            ExpectedNoRequirementValues.Clear();
            ExpectedBossValues.Clear();
            ExpectedComplexValues.Clear();
            ExpectedItemValues.Clear();
            ExpectedKeyDoorValues.Clear();
            
            foreach (DungeonNodeID id in Enum.GetValues(typeof(DungeonNodeID)))
            {
                switch (id)
                {
                    case DungeonNodeID.SP:
                        ExpectedEntryValues.Add(id, new List<OverworldNodeID> {OverworldNodeID.SPEntry});
                        break;
                    case DungeonNodeID.SPAfterRiver:
                        ExpectedItemValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, ItemType type, int count)>
                            {
                                (DungeonNodeID.SP, ItemType.Flippers, 1)
                            });
                        break;
                    case DungeonNodeID.SPB1:
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.SPAfterRiver, KeyDoorID.SP1FKeyDoor),
                                (DungeonNodeID.SPB1PastFirstRightKeyDoor, KeyDoorID.SPB1FirstRightKeyDoor)
                            });
                        break;
                    case DungeonNodeID.SPB1FirstRightKeyDoor:
                        ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                        {
                            DungeonNodeID.SPB1,
                            DungeonNodeID.SPB1PastFirstRightKeyDoor
                        });
                        break;
                    case DungeonNodeID.SPB1PastFirstRightKeyDoor:
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.SPB1, KeyDoorID.SPB1FirstRightKeyDoor),
                                (DungeonNodeID.SPB1PastSecondRightKeyDoor, KeyDoorID.SPB1SecondRightKeyDoor)
                            });
                        break;
                    case DungeonNodeID.SPB1SecondRightKeyDoor:
                        ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                        {
                            DungeonNodeID.SPB1PastFirstRightKeyDoor,
                            DungeonNodeID.SPB1PastSecondRightKeyDoor
                        });
                        break;
                    case DungeonNodeID.SPB1PastSecondRightKeyDoor:
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.SPB1PastFirstRightKeyDoor, KeyDoorID.SPB1SecondRightKeyDoor)
                            });
                        break;
                    case DungeonNodeID.SPB1PastRightHammerBlocks:
                        ExpectedItemValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, ItemType type, int count)>
                            {
                                (DungeonNodeID.SPB1PastSecondRightKeyDoor, ItemType.Hammer, 1)
                            });
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.SPB1PastLeftKeyDoor, KeyDoorID.SPB1LeftKeyDoor)
                            });
                        ExpectedComplexValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, ComplexRequirementType type)>
                            {
                                (DungeonNodeID.SPB1PastFirstRightKeyDoor, ComplexRequirementType.SPSpeckyClip)
                            });
                        break;
                    case DungeonNodeID.SPB1KeyLedge:
                        ExpectedItemValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, ItemType type, int count)>
                            {
                                (DungeonNodeID.SPB1PastRightHammerBlocks, ItemType.Hookshot, 1)
                            });
                        break;
                    case DungeonNodeID.SPB1LeftKeyDoor:
                        ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                        {
                            DungeonNodeID.SPB1PastRightHammerBlocks,
                            DungeonNodeID.SPB1PastLeftKeyDoor
                        });
                        break;
                    case DungeonNodeID.SPB1PastLeftKeyDoor:
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.SPB1PastRightHammerBlocks, KeyDoorID.SPB1LeftKeyDoor)
                            });
                        break;
                    case DungeonNodeID.SPBigChest:
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.SPB1PastRightHammerBlocks, KeyDoorID.SPBigChest)
                            });
                        break;
                    case DungeonNodeID.SPB1Back:
                        ExpectedItemValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, ItemType type, int count)>
                            {
                                (DungeonNodeID.SPB1PastRightHammerBlocks, ItemType.Hookshot, 1)
                            });
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.SPB1PastBackFirstKeyDoor, KeyDoorID.SPB1BackFirstKeyDoor)
                            });
                        break;
                    case DungeonNodeID.SPB1BackFirstKeyDoor:
                        ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                        {
                            DungeonNodeID.SPB1Back,
                            DungeonNodeID.SPB1PastBackFirstKeyDoor
                        });
                        break;
                    case DungeonNodeID.SPB1PastBackFirstKeyDoor:
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.SPB1Back, KeyDoorID.SPB1BackFirstKeyDoor),
                                (DungeonNodeID.SPBossRoom, KeyDoorID.SPBossRoomKeyDoor)
                            });
                        break;
                    case DungeonNodeID.SPBossRoomKeyDoor:
                        ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                        {
                            DungeonNodeID.SPB1PastBackFirstKeyDoor,
                            DungeonNodeID.SPBossRoom
                        });
                        break;
                    case DungeonNodeID.SPBossRoom:
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.SPB1PastBackFirstKeyDoor, KeyDoorID.SPBossRoomKeyDoor)
                            });
                        break;
                    case DungeonNodeID.SPBoss:
                        ExpectedBossValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, BossPlacementID bossID)>
                            {
                                (DungeonNodeID.SPBossRoom, BossPlacementID.SPBoss)
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

            Assert.True(_entryFactoryCalls.Contains(_overworldNodes[fromNodeID]),
                $"Expected entry factory to be called with overworld node {fromNodeID}");
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
            
            // Configure the substitute to return consistent node instances
            var expectedNode = Substitute.For<INode>();
            dungeonData.Nodes[fromNodeID].Returns(expectedNode);
            
            _sut.PopulateNodeConnections(dungeonData, id, node, connections);

            Assert.True(_connectionFactoryCalls.Any(x =>
                x.fromNode == expectedNode && x.requirement == null),
                $"Expected connection from {fromNodeID} with no requirement for node {id}");
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
            
            // Configure the substitute to return consistent node instances
            var expectedNode = Substitute.For<INode>();
            dungeonData.Nodes[fromNodeID].Returns(expectedNode);
            
            _sut.PopulateNodeConnections(dungeonData, id, node, connections);

            Assert.True(_connectionFactoryCalls.Any(x =>
                x.fromNode == expectedNode && 
                ReferenceEquals(x.requirement, _bossRequirements[bossID])),
                $"Expected boss connection from {fromNodeID} with boss {bossID} for node {id}");
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
            DungeonNodeID id, DungeonNodeID fromNodeID, ComplexRequirementType requirementType)
        {
            var dungeonData = Substitute.For<IMutableDungeon>();
            var node = Substitute.For<IDungeonNode>();
            var connections = new List<INodeConnection>();
            
            // Configure the substitute to return consistent node instances
            var expectedNode = Substitute.For<INode>();
            dungeonData.Nodes[fromNodeID].Returns(expectedNode);
            
            _sut.PopulateNodeConnections(dungeonData, id, node, connections);

            Assert.True(_connectionFactoryCalls.Any(x =>
                x.fromNode == expectedNode && 
                ReferenceEquals(x.requirement, _complexRequirements[requirementType])),
                $"Expected complex connection from {fromNodeID} with type {requirementType} for node {id}");
        }

        public static IEnumerable<object[]> PopulateNodeConnections_ShouldCreateExpectedComplexConnectionsData()
        {
            PopulateExpectedValues();

            return (from id in ExpectedComplexValues.Keys
                    from value in ExpectedComplexValues[id]
                    select new object[] { id, value.fromNodeID, value.type }).ToList();
        }

        [Theory]
        [MemberData(nameof(PopulateNodeConnections_ShouldCreateExpectedItemConnectionsData))]
        public void PopulateNodeConnections_ShouldCreateExpectedItemConnections(
            DungeonNodeID id, DungeonNodeID fromNodeID, ItemType type, int count)
        {
            var dungeonData = Substitute.For<IMutableDungeon>();
            var node = Substitute.For<IDungeonNode>();
            var connections = new List<INodeConnection>();
            
            // Configure the substitute to return consistent node instances
            var expectedNode = Substitute.For<INode>();
            dungeonData.Nodes[fromNodeID].Returns(expectedNode);
            
            _sut.PopulateNodeConnections(dungeonData, id, node, connections);

            Assert.True(_connectionFactoryCalls.Any(x =>
                x.fromNode == expectedNode && 
                ReferenceEquals(x.requirement, _itemRequirements[(type, count)])),
                $"Expected item connection from {fromNodeID} requiring {type}x{count} for node {id}");
        }

        public static IEnumerable<object[]> PopulateNodeConnections_ShouldCreateExpectedItemConnectionsData()
        {
            PopulateExpectedValues();

            return (from id in ExpectedItemValues.Keys from value in ExpectedItemValues[id]
                select new object[] {id, value.fromNodeID, value.type, value.count}).ToList();
        }

        [Theory]
        [MemberData(nameof(PopulateNodeConnections_ShouldCreateExpectedKeyDoorConnectionsData))]
        public void PopulateNodeConnections_ShouldCreateExpectedKeyDoorConnections(
            DungeonNodeID id, DungeonNodeID fromNodeID, KeyDoorID keyDoor)
        {
            var dungeonData = Substitute.For<IMutableDungeon>();
            var node = Substitute.For<IDungeonNode>();
            var connections = new List<INodeConnection>();
            
            // Configure the substitute to return consistent node instances
            var expectedNode = Substitute.For<INode>();
            dungeonData.Nodes[fromNodeID].Returns(expectedNode);
            
            _sut.PopulateNodeConnections(dungeonData, id, node, connections);

            Assert.True(_connectionFactoryCalls.Any(x =>
                x.fromNode == expectedNode &&
                ReferenceEquals(x.requirement, dungeonData.KeyDoors[keyDoor].Requirement)),
                $"Expected key door connection from {fromNodeID} with door {keyDoor} for node {id}");
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
            var sut = scope.Resolve<ISPDungeonNodeFactory>();
            
            Assert.NotNull(sut as SPDungeonNodeFactory);
        }
    }
}