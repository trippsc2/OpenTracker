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
using OpenTracker.Models.Nodes.Factories;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Complex;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.Nodes.Factories
{
    public class HCDungeonNodeFactoryTests
    {
        private readonly IComplexRequirementDictionary _complexRequirements;
        
        private readonly IOverworldNodeDictionary _overworldNodes;

        private readonly HCDungeonNodeFactory _sut;

        private readonly List<INode> _entryFactoryCalls = new();
        private readonly List<(INode fromNode, INode toNode, IRequirement? requirement)> _connectionFactoryCalls = new();

        private static readonly Dictionary<DungeonNodeID, List<OverworldNodeID>> ExpectedEntryValues = new();
        private static readonly Dictionary<DungeonNodeID, List<DungeonNodeID>> ExpectedNoRequirementValues = new();
        private static readonly Dictionary<DungeonNodeID,
            List<(DungeonNodeID fromNodeID, ComplexRequirementType type)>> ExpectedComplexValues = new();
        private static readonly Dictionary<DungeonNodeID,
            List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>> ExpectedKeyDoorValues = new();

        public HCDungeonNodeFactoryTests()
        {
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

            _sut = new HCDungeonNodeFactory(_complexRequirements, _overworldNodes, EntryFactory, ConnectionFactory);
        }

        private static void PopulateExpectedValues()
        {
            ExpectedEntryValues.Clear();
            ExpectedNoRequirementValues.Clear();
            ExpectedComplexValues.Clear();
            ExpectedKeyDoorValues.Clear();
            
            foreach (DungeonNodeID id in Enum.GetValues(typeof(DungeonNodeID)))
            {
                switch (id)
                {
                    case DungeonNodeID.HCSanctuary:
                        ExpectedEntryValues.Add(id, new List<OverworldNodeID> {OverworldNodeID.HCSanctuaryEntry});
                        ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                        {
                            DungeonNodeID.HCBack
                        });
                        break;
                    case DungeonNodeID.HCFront:
                        ExpectedEntryValues.Add(id, new List<OverworldNodeID> {OverworldNodeID.HCFrontEntry});
                        ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                        {
                            DungeonNodeID.HCDarkRoomFront
                        });
                        ExpectedKeyDoorValues.Add(id, new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.HCPastEscapeFirstKeyDoor, KeyDoorID.HCEscapeFirstKeyDoor)
                        });
                        break;
                    case DungeonNodeID.HCEscapeFirstKeyDoor:
                        ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID> 
                        {
                            DungeonNodeID.HCFront,
                            DungeonNodeID.HCPastEscapeFirstKeyDoor
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
                        ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID> 
                        {
                            DungeonNodeID.HCPastEscapeFirstKeyDoor,
                            DungeonNodeID.HCPastEscapeSecondKeyDoor
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
                        ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                        {
                            DungeonNodeID.HCPastEscapeSecondKeyDoor,
                            DungeonNodeID.HCZeldasCell
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
                        ExpectedComplexValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, ComplexRequirementType requirementType)>
                            {
                                (DungeonNodeID.HCFront, ComplexRequirementType.DarkRoomHC)
                            });
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.HCPastDarkCrossKeyDoor, KeyDoorID.HCDarkCrossKeyDoor)
                            });
                        break;
                    case DungeonNodeID.HCDarkCrossKeyDoor:
                        ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                        {
                            DungeonNodeID.HCDarkRoomFront,
                            DungeonNodeID.HCPastDarkCrossKeyDoor
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
                        ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                        {
                            DungeonNodeID.HCPastDarkCrossKeyDoor,
                            DungeonNodeID.HCPastSewerRatRoomKeyDoor
                        });
                        break;
                    case DungeonNodeID.HCPastSewerRatRoomKeyDoor:
                        ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID> 
                        {
                            DungeonNodeID.HCDarkRoomBack
                        });
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.HCPastDarkCrossKeyDoor, KeyDoorID.HCSewerRatRoomKeyDoor)
                            });
                        break;
                    case DungeonNodeID.HCDarkRoomBack:
                        ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID> 
                        {
                            DungeonNodeID.HCPastSewerRatRoomKeyDoor
                        });
                        ExpectedComplexValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, ComplexRequirementType requirementType)>
                            {
                                (DungeonNodeID.HCBack, ComplexRequirementType.DarkRoomHC)
                            });
                        break;
                    case DungeonNodeID.HCBack:
                        ExpectedEntryValues.Add(id, new List<OverworldNodeID> {OverworldNodeID.HCBackEntry});
                        ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                        {
                            DungeonNodeID.HCDarkRoomBack
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