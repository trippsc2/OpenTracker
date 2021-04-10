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
    public class DPDungeonNodeFactoryTests
    {
        private readonly IRequirementFactory _requirementFactory = Substitute.For<IRequirementFactory>();
        private readonly IOverworldNodeFactory _overworldNodeFactory = Substitute.For<IOverworldNodeFactory>();
        
        private readonly IRequirementDictionary _requirements;
        private readonly IOverworldNodeDictionary _overworldNodes;

        private readonly List<INode> _entryFactoryCalls = new();
        private readonly List<(INode fromNode, INode toNode, IRequirement requirement)> _connectionFactoryCalls = new();

        private readonly DPDungeonNodeFactory _sut;

        private static readonly Dictionary<DungeonNodeID, List<OverworldNodeID>> ExpectedEntryValues = new();
        private static readonly Dictionary<DungeonNodeID, List<
            (DungeonNodeID fromNodeID, RequirementType requirementType)>> ExpectedConnectionValue = new();
        private static readonly Dictionary<DungeonNodeID, List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>>
            ExpectedKeyDoorValues = new();

        public DPDungeonNodeFactoryTests()
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

            _sut = new DPDungeonNodeFactory(_overworldNodes, EntryFactory, ConnectionFactory);
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
                    case DungeonNodeID.DPFront:
                        ExpectedEntryValues.Add(id,
                            new List<OverworldNodeID> {OverworldNodeID.DPFrontEntry, OverworldNodeID.DPLeftEntry});
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.DPPastRightKeyDoor, KeyDoorID.DPRightKeyDoor)
                            });
                        break;
                    case DungeonNodeID.DPTorch:
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.DPFront, RequirementType.Torch)
                            });
                        break;
                    case DungeonNodeID.DPBigChest:
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.DPFront, KeyDoorID.DPBigChest)
                            });
                        break;
                    case DungeonNodeID.DPRightKeyDoor:
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.DPFront, RequirementType.NoRequirement),
                                (DungeonNodeID.DPPastRightKeyDoor, RequirementType.NoRequirement)
                            });
                        break;
                    case DungeonNodeID.DPPastRightKeyDoor:
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.DPFront, KeyDoorID.DPRightKeyDoor)
                            });
                        break;
                    case DungeonNodeID.DPBack:
                        ExpectedEntryValues.Add(id, new List<OverworldNodeID> {OverworldNodeID.DPBackEntry});
                        break;
                    case DungeonNodeID.DP2F:
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.DPBack, KeyDoorID.DP1FKeyDoor),
                                (DungeonNodeID.DP2FPastFirstKeyDoor, KeyDoorID.DP2FFirstKeyDoor)
                            });
                        break;
                    case DungeonNodeID.DP2FFirstKeyDoor:
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.DP2F, RequirementType.NoRequirement),
                                (DungeonNodeID.DP2FPastFirstKeyDoor, RequirementType.NoRequirement)
                            });
                        break;
                    case DungeonNodeID.DP2FPastFirstKeyDoor:
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.DP2F, KeyDoorID.DP2FFirstKeyDoor),
                                (DungeonNodeID.DP2FPastSecondKeyDoor, KeyDoorID.DP2FSecondKeyDoor)
                            });
                        break;
                    case DungeonNodeID.DP2FSecondKeyDoor:
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.DP2FPastFirstKeyDoor, RequirementType.NoRequirement),
                                (DungeonNodeID.DP2FPastSecondKeyDoor, RequirementType.NoRequirement)
                            });
                        break;
                    case DungeonNodeID.DP2FPastSecondKeyDoor:
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.DP2FPastFirstKeyDoor, KeyDoorID.DP2FSecondKeyDoor)
                            });
                        break;
                    case DungeonNodeID.DPPastFourTorchWall:
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.DP2FPastSecondKeyDoor, RequirementType.FireSource)
                            });
                        break;
                    case DungeonNodeID.DPBossRoom:
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.DPPastFourTorchWall, KeyDoorID.DPBigKeyDoor)
                            });
                        break;
                    case DungeonNodeID.DPBoss:
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.DPBossRoom, RequirementType.DPBoss)
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

            return (from id in ExpectedEntryValues.Keys from value in ExpectedEntryValues[id]
                select new object[] {id, value}).ToList();
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
            var sut = scope.Resolve<IDPDungeonNodeFactory>();
            
            Assert.NotNull(sut as DPDungeonNodeFactory);
        }
    }
}