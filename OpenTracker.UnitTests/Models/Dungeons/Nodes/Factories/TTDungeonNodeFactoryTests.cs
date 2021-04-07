using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Dungeons.KeyDoors;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Dungeons.Nodes;
using OpenTracker.Models.Dungeons.Nodes.Factories;
using OpenTracker.Models.NodeConnections;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.Nodes.Factories
{
    public class TTDungeonNodeFactoryTests
    {
        private readonly IRequirementFactory _requirementFactory = Substitute.For<IRequirementFactory>();
        private readonly IOverworldNodeFactory _overworldNodeFactory = Substitute.For<IOverworldNodeFactory>();
        
        private readonly IRequirementDictionary _requirements;
        private readonly IOverworldNodeDictionary _overworldNodes;

        private readonly List<INode> _entryFactoryCalls = new();
        private readonly List<(INode fromNode, INode toNode, IRequirement requirement)> _connectionFactoryCalls = new();

        private readonly TTDungeonNodeFactory _sut;

        private static readonly Dictionary<DungeonNodeID, OverworldNodeID> ExpectedEntryValues = new();
        private static readonly Dictionary<DungeonNodeID, List<
            (DungeonNodeID fromNodeID, RequirementType requirementType)>> ExpectedConnectionValue = new();
        private static readonly Dictionary<DungeonNodeID, List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>>
            ExpectedKeyDoorValues = new();

        public TTDungeonNodeFactoryTests()
        {
            _requirements = new RequirementDictionary(() => _requirementFactory);
            _overworldNodes = new OverworldNodeDictionary(() => _overworldNodeFactory);

            IEntryNodeConnection EntryFactory(INode fromNode)
            {
                _entryFactoryCalls.Add(fromNode);
                return Substitute.For<IEntryNodeConnection>();
            }

            INodeConnection ConnectionFactory(INode fromNode, INode toNode, IRequirement requirement)
            {
                _connectionFactoryCalls.Add((fromNode, toNode, requirement));
                return Substitute.For<INodeConnection>();
            }

            _sut = new TTDungeonNodeFactory(_requirements, _overworldNodes, EntryFactory, ConnectionFactory);
        }

        private static void PopulateExpectedValues()
        {
            ExpectedEntryValues.Clear();
            ExpectedConnectionValue.Clear();
            ExpectedKeyDoorValues.Clear();
            
            foreach (DungeonNodeID id in Enum.GetValues(typeof(DungeonNodeID)))
            {
                switch (id)
                {
                case DungeonNodeID.TT:
                    ExpectedEntryValues.Add(id, OverworldNodeID.TTEntry);
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.TTPastBigKeyDoor, KeyDoorID.TTBigKeyDoor)
                        });
                    break;
                case DungeonNodeID.TTBigKeyDoor:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.TT, RequirementType.NoRequirement),
                            (DungeonNodeID.TTPastBigKeyDoor, RequirementType.NoRequirement)
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
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.TTPastBigKeyDoor, RequirementType.NoRequirement),
                            (DungeonNodeID.TTPastFirstKeyDoor, RequirementType.NoRequirement)
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
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.TTPastFirstKeyDoor, RequirementType.NoRequirement),
                            (DungeonNodeID.TTPastBigChestRoomKeyDoor, RequirementType.NoRequirement)
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
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.TTPastBigChestRoomKeyDoor, RequirementType.Hammer)
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
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.TTPastBigKeyDoor, RequirementType.BossShuffleOn),
                            (DungeonNodeID.TTPastSecondKeyDoor, RequirementType.BossShuffleOff)
                        });
                    break;
                case DungeonNodeID.TTBoss:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.TTBossRoom, RequirementType.TTBoss)
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

            return ExpectedEntryValues.Keys.Select(id => new object[] {id, ExpectedEntryValues[id]}).ToList();
        }

        [Theory]
        [MemberData(nameof(PopulateNodeConnections_ShouldCreateExpectedConnectionsData))]
        public void PopulateNodeConnections_ShouldCreateExpectedConnections(
            DungeonNodeID id, DungeonNodeID fromNodeID, RequirementType requirementType)
        {
            var dungeonData = Substitute.For<IMutableDungeon>();
            var node = Substitute.For<IDungeonNode>();
            var connections = new List<INodeConnection>();
            _sut.PopulateNodeConnections(dungeonData, id, node, connections);

            Assert.Contains(_connectionFactoryCalls, x =>
                x.fromNode == dungeonData.Nodes[fromNodeID] && x.requirement == _requirements[requirementType]);
        }

        public static IEnumerable<object[]> PopulateNodeConnections_ShouldCreateExpectedConnectionsData()
        {
            PopulateExpectedValues();

            return (from id in ExpectedConnectionValue.Keys from value in ExpectedConnectionValue[id]
                select new object[] {id, value.fromNodeID, value.requirementType}).ToList();
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