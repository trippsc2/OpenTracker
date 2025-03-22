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
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Nodes.Connections;
using OpenTracker.Models.Nodes.Factories;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Boss;
using OpenTracker.Models.Requirements.Complex;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.Nodes.Factories
{
    public class DPDungeonNodeFactoryTests
    {
        private readonly IBossRequirementDictionary _bossRequirements;
        private readonly IComplexRequirementDictionary _complexRequirements;

        private readonly IOverworldNodeDictionary _overworldNodes;

        private readonly DPDungeonNodeFactory _sut;

        private readonly List<INode> _entryFactoryCalls = new();
        private readonly List<(INode fromNode, INode toNode, IRequirement? requirement)> _connectionFactoryCalls = new();

        private static readonly Dictionary<DungeonNodeID, List<OverworldNodeID>> ExpectedEntryValues = new();
        private static readonly Dictionary<DungeonNodeID, List<DungeonNodeID>> ExpectedNoRequirementValues = new();
        private static readonly Dictionary<DungeonNodeID,
            List<(DungeonNodeID fromNodeID, BossPlacementID bossID)>> ExpectedBossValues = new();
        private static readonly Dictionary<DungeonNodeID,
            List<(DungeonNodeID fromNodeID, ComplexRequirementType type)>> ExpectedComplexValues = new();
        private static readonly Dictionary<DungeonNodeID,
            List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>> ExpectedKeyDoorValues = new();

        public DPDungeonNodeFactoryTests()
        {
            _bossRequirements = new BossRequirementDictionary(
                Substitute.For<IBossPlacementDictionary>(),
                _ => Substitute.For<IBossRequirement>());
            _complexRequirements = new ComplexRequirementDictionary(
                () => Substitute.For<IComplexRequirementFactory>());
            
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

            _sut = new DPDungeonNodeFactory(
                _bossRequirements, _complexRequirements, _overworldNodes, EntryFactory, ConnectionFactory);
        }

        private static void PopulateExpectedValues()
        {
            ExpectedEntryValues.Clear();
            ExpectedNoRequirementValues.Clear();
            ExpectedComplexValues.Clear();
            ExpectedBossValues.Clear();
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
                        ExpectedComplexValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, ComplexRequirementType requirementType)>
                            {
                                (DungeonNodeID.DPFront, ComplexRequirementType.Torch)
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
                        ExpectedNoRequirementValues.Add(id,
                            new List<DungeonNodeID>
                            {
                                DungeonNodeID.DPFront,
                                DungeonNodeID.DPPastRightKeyDoor
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
                        ExpectedNoRequirementValues.Add(id,
                            new List<DungeonNodeID>
                            {
                                DungeonNodeID.DP2F,
                                DungeonNodeID.DP2FPastFirstKeyDoor
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
                        ExpectedNoRequirementValues.Add(id,
                            new List<DungeonNodeID>
                            {
                                DungeonNodeID.DP2FPastFirstKeyDoor,
                                DungeonNodeID.DP2FPastSecondKeyDoor
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
                        ExpectedComplexValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, ComplexRequirementType requirementType)>
                            {
                                (DungeonNodeID.DP2FPastSecondKeyDoor, ComplexRequirementType.FireSource)
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
                        ExpectedBossValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, BossPlacementID bossID)>
                            {
                                (DungeonNodeID.DPBossRoom, BossPlacementID.DPBoss)
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

            return (from id in ExpectedNoRequirementValues.Keys from value in ExpectedNoRequirementValues[id]
                select new object[] {id, value}).ToList();
        }

        [Theory]
        [MemberData(nameof(PopulateNodeConnections_ShouldCreateExpectedConnectionsData))]
        public void PopulateNodeConnections_ShouldCreateExpectedConnections(
            DungeonNodeID id, DungeonNodeID fromNodeID, ComplexRequirementType requirementType)
        {
            var dungeonData = Substitute.For<IMutableDungeon>();
            var node = Substitute.For<IDungeonNode>();
            var connections = new List<INodeConnection>();
            _sut.PopulateNodeConnections(dungeonData, id, node, connections);

            Assert.Contains(_connectionFactoryCalls, x =>
                x.fromNode == dungeonData.Nodes[fromNodeID] && x.requirement == _complexRequirements[requirementType]);
        }

        public static IEnumerable<object[]> PopulateNodeConnections_ShouldCreateExpectedConnectionsData()
        {
            PopulateExpectedValues();

            return (from id in ExpectedComplexValues.Keys from value in ExpectedComplexValues[id]
                select new object[] {id, value.fromNodeID, value.type}).ToList();
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