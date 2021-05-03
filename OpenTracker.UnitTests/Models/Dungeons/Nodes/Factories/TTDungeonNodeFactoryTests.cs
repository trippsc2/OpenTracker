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
using OpenTracker.Models.Requirements.BossShuffle;
using OpenTracker.Models.Requirements.Item;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.Nodes.Factories
{
    public class TTDungeonNodeFactoryTests
    {
        private readonly IBossRequirementDictionary _bossRequirements;
        private readonly IBossShuffleRequirementDictionary _bossShuffleRequirements;
        private readonly IItemRequirementDictionary _itemRequirements;

        private readonly IOverworldNodeDictionary _overworldNodes;

        private readonly TTDungeonNodeFactory _sut;

        private readonly List<INode> _entryFactoryCalls = new();
        private readonly List<(INode fromNode, INode toNode, IRequirement? requirement)> _connectionFactoryCalls = new();

        private static readonly Dictionary<DungeonNodeID, List<OverworldNodeID>> ExpectedEntryValues = new();
        private static readonly Dictionary<DungeonNodeID, List<DungeonNodeID>> ExpectedNoRequirementValues = new();
        private static readonly Dictionary<DungeonNodeID,
            List<(DungeonNodeID fromNodeID, BossPlacementID bossID)>> ExpectedBossValues = new();
        private static readonly Dictionary<DungeonNodeID,
            List<(DungeonNodeID fromNodeID, bool bossShuffle)>> ExpectedBossShuffleValues = new();
        private static readonly Dictionary<DungeonNodeID,
            List<(DungeonNodeID fromNodeID, ItemType type, int count)>> ExpectedItemValues = new();
        private static readonly Dictionary<DungeonNodeID,
            List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>> ExpectedKeyDoorValues = new();

        public TTDungeonNodeFactoryTests()
        {
            _bossRequirements = new BossRequirementDictionary(
                Substitute.For<IBossPlacementDictionary>(),
                _ => Substitute.For<IBossRequirement>());
            _bossShuffleRequirements = new BossShuffleRequirementDictionary(
                _ => Substitute.For<IBossShuffleRequirement>());
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

            _sut = new TTDungeonNodeFactory(
                _bossRequirements, _bossShuffleRequirements, _itemRequirements, _overworldNodes, EntryFactory,
                ConnectionFactory);
        }

        private static void PopulateExpectedValues()
        {
            ExpectedEntryValues.Clear();
            ExpectedNoRequirementValues.Clear();
            ExpectedBossValues.Clear();
            ExpectedBossShuffleValues.Clear();
            ExpectedItemValues.Clear();
            ExpectedKeyDoorValues.Clear();
            
            foreach (DungeonNodeID id in Enum.GetValues(typeof(DungeonNodeID)))
            {
                switch (id)
                {
                case DungeonNodeID.TT:
                    ExpectedEntryValues.Add(id, new List<OverworldNodeID> {OverworldNodeID.TTEntry});
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.TTPastBigKeyDoor, KeyDoorID.TTBigKeyDoor)
                        });
                    break;
                case DungeonNodeID.TTBigKeyDoor:
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.TT,
                        DungeonNodeID.TTPastBigKeyDoor
                    });
                    break;
                case DungeonNodeID.TTPastBigKeyDoor:
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.TT, KeyDoorID.TTBigKeyDoor),
                            (DungeonNodeID.TTPastFirstKeyDoor, KeyDoorID.TTFirstKeyDoor)
                        });
                    break;
                case DungeonNodeID.TTFirstKeyDoor:
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                        {
                            DungeonNodeID.TTPastBigKeyDoor,
                            DungeonNodeID.TTPastFirstKeyDoor
                        });
                    break;
                case DungeonNodeID.TTPastFirstKeyDoor:
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.TTPastBigKeyDoor, KeyDoorID.TTFirstKeyDoor),
                            (DungeonNodeID.TTPastBigChestRoomKeyDoor, KeyDoorID.TTBigChestKeyDoor)
                        });
                    break;
                case DungeonNodeID.TTPastSecondKeyDoor:
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.TTPastFirstKeyDoor, KeyDoorID.TTSecondKeyDoor)
                        });
                    break;
                case DungeonNodeID.TTBigChestKeyDoor:
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.TTPastFirstKeyDoor,
                        DungeonNodeID.TTPastBigChestRoomKeyDoor
                    });
                    break;
                case DungeonNodeID.TTPastBigChestRoomKeyDoor:
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.TTPastFirstKeyDoor, KeyDoorID.TTBigChestKeyDoor)
                        });
                    break;
                case DungeonNodeID.TTPastHammerBlocks:
                    ExpectedItemValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ItemType type, int count)>
                        {
                            (DungeonNodeID.TTPastBigChestRoomKeyDoor, ItemType.Hammer, 1)
                        });
                    break;
                case DungeonNodeID.TTBigChest:
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.TTPastHammerBlocks, KeyDoorID.TTBigChest)
                        });
                    break;
                case DungeonNodeID.TTBossRoom:
                    ExpectedBossShuffleValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, bool bossShuffle)>
                        {
                            (DungeonNodeID.TTPastBigKeyDoor, true),
                            (DungeonNodeID.TTPastSecondKeyDoor, false)
                        });
                    break;
                case DungeonNodeID.TTBoss:
                    ExpectedBossValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, BossPlacementID bossID)>
                        {
                            (DungeonNodeID.TTBossRoom, BossPlacementID.TTBoss)
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
        [MemberData(nameof(PopulateNodeConnections_ShouldCreateExpectedBossShuffleConnectionsData))]
        public void PopulateNodeConnections_ShouldCreateExpectedBossShuffleConnections(
            DungeonNodeID id, DungeonNodeID fromNodeID, bool bossShuffle)
        {
            var dungeonData = Substitute.For<IMutableDungeon>();
            var node = Substitute.For<IDungeonNode>();
            var connections = new List<INodeConnection>();
            _sut.PopulateNodeConnections(dungeonData, id, node, connections);

            Assert.Contains(_connectionFactoryCalls, x =>
                x.fromNode == dungeonData.Nodes[fromNodeID] && x.requirement == _bossShuffleRequirements[bossShuffle]);
        }

        public static IEnumerable<object[]> PopulateNodeConnections_ShouldCreateExpectedBossShuffleConnectionsData()
        {
            PopulateExpectedValues();

            return (from id in ExpectedBossShuffleValues.Keys from value in ExpectedBossShuffleValues[id]
                select new object[] {id, value.fromNodeID, value.bossShuffle}).ToList();
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
            var sut = scope.Resolve<ITTDungeonNodeFactory>();
            
            Assert.NotNull(sut as TTDungeonNodeFactory);
        }
    }
}