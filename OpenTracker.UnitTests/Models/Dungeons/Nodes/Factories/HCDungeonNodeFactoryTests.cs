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
    public class HCDungeonNodeFactoryTests
    {
        private readonly IRequirementFactory _requirementFactory = Substitute.For<IRequirementFactory>();
        private readonly IOverworldNodeFactory _overworldNodeFactory = Substitute.For<IOverworldNodeFactory>();
        
        private readonly IRequirementDictionary _requirements;
        private readonly IOverworldNodeDictionary _overworldNodes;

        private readonly List<INode> _entryFactoryCalls = new();
        private readonly List<(INode fromNode, INode toNode, IRequirement requirement)> _connectionFactoryCalls = new();

        private readonly HCDungeonNodeFactory _sut;

        private static readonly Dictionary<DungeonNodeID, OverworldNodeID> ExpectedEntryValues = new();
        private static readonly Dictionary<DungeonNodeID, List<
            (DungeonNodeID fromNodeID, RequirementType requirementType)>> ExpectedConnectionValue = new();
        private static readonly Dictionary<DungeonNodeID, List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>>
            ExpectedKeyDoorValues = new();

        public HCDungeonNodeFactoryTests()
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

            _sut = new HCDungeonNodeFactory(_overworldNodes, EntryFactory, ConnectionFactory);
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
                    case DungeonNodeID.HCSanctuary:
                        ExpectedEntryValues.Add(id, OverworldNodeID.HCSanctuaryEntry);
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.HCBack, RequirementType.NoRequirement)
                            });
                        break;
                    case DungeonNodeID.HCFront:
                        ExpectedEntryValues.Add(id, OverworldNodeID.HCFrontEntry);
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.HCDarkRoomFront, RequirementType.NoRequirement)
                            });
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.HCPastEscapeFirstKeyDoor, KeyDoorID.HCEscapeFirstKeyDoor)
                            });
                        break;
                    case DungeonNodeID.HCEscapeFirstKeyDoor:
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.HCFront, RequirementType.NoRequirement),
                                (DungeonNodeID.HCPastEscapeFirstKeyDoor, RequirementType.NoRequirement)
                            });
                        break;
                    case DungeonNodeID.HCPastEscapeFirstKeyDoor:
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.HCFront, KeyDoorID.HCEscapeFirstKeyDoor),
                                (DungeonNodeID.HCPastEscapeSecondKeyDoor, KeyDoorID.HCEscapeSecondKeyDoor)
                            });
                        break;
                    case DungeonNodeID.HCEscapeSecondKeyDoor:
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.HCPastEscapeFirstKeyDoor, RequirementType.NoRequirement),
                                (DungeonNodeID.HCPastEscapeSecondKeyDoor, RequirementType.NoRequirement)
                            });
                        break;
                    case DungeonNodeID.HCPastEscapeSecondKeyDoor:
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.HCPastEscapeFirstKeyDoor, KeyDoorID.HCEscapeSecondKeyDoor),
                                (DungeonNodeID.HCZeldasCell, KeyDoorID.HCZeldasCellDoor)
                            });
                        break;
                    case DungeonNodeID.HCZeldasCellDoor:
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.HCPastEscapeSecondKeyDoor, RequirementType.NoRequirement),
                                (DungeonNodeID.HCZeldasCell, RequirementType.NoRequirement)
                            });
                        break;
                    case DungeonNodeID.HCZeldasCell:
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.HCPastEscapeSecondKeyDoor, KeyDoorID.HCZeldasCellDoor)
                            });
                        break;
                    case DungeonNodeID.HCDarkRoomFront:
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.HCFront, RequirementType.DarkRoomHC)
                            });
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.HCPastDarkCrossKeyDoor, KeyDoorID.HCDarkCrossKeyDoor)
                            });
                        break;
                    case DungeonNodeID.HCDarkCrossKeyDoor:
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.HCDarkRoomFront, RequirementType.NoRequirement),
                                (DungeonNodeID.HCPastDarkCrossKeyDoor, RequirementType.NoRequirement)
                            });
                        break;
                    case DungeonNodeID.HCPastDarkCrossKeyDoor:
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.HCDarkRoomFront, KeyDoorID.HCDarkCrossKeyDoor),
                                (DungeonNodeID.HCPastSewerRatRoomKeyDoor, KeyDoorID.HCSewerRatRoomKeyDoor)
                            });
                        break;
                    case DungeonNodeID.HCSewerRatRoomKeyDoor:
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.HCPastDarkCrossKeyDoor, RequirementType.NoRequirement),
                                (DungeonNodeID.HCPastSewerRatRoomKeyDoor, RequirementType.NoRequirement)
                            });
                        break;
                    case DungeonNodeID.HCPastSewerRatRoomKeyDoor:
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.HCDarkRoomBack, RequirementType.NoRequirement)
                            });
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.HCPastDarkCrossKeyDoor, KeyDoorID.HCSewerRatRoomKeyDoor)
                            });
                        break;
                    case DungeonNodeID.HCDarkRoomBack:
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.HCPastSewerRatRoomKeyDoor, RequirementType.NoRequirement),
                                (DungeonNodeID.HCBack, RequirementType.DarkRoomHC)
                            });
                        break;
                    case DungeonNodeID.HCBack:
                        ExpectedEntryValues.Add(id, OverworldNodeID.HCBackEntry);
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.HCDarkRoomBack, RequirementType.NoRequirement)
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
            var sut = scope.Resolve<IHCDungeonNodeFactory>();
            
            Assert.NotNull(sut as HCDungeonNodeFactory);
        }
    }
}