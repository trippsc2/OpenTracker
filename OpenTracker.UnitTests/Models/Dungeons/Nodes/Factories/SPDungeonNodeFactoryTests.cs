using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Dungeons.Nodes;
using OpenTracker.Models.Dungeons.Nodes.Factories;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.NodeConnections;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.Nodes.Factories
{
    public class SPDungeonNodeFactoryTests
    {
        private readonly IRequirementFactory _requirementFactory = Substitute.For<IRequirementFactory>();
        private readonly IOverworldNodeFactory _overworldNodeFactory = Substitute.For<IOverworldNodeFactory>();
        
        private readonly IRequirementDictionary _requirements;
        private readonly IOverworldNodeDictionary _overworldNodes;

        private readonly List<INode> _entryFactoryCalls = new();
        private readonly List<(INode fromNode, INode toNode, IRequirement requirement)> _connectionFactoryCalls = new();

        private readonly SPDungeonNodeFactory _sut;

        private static readonly Dictionary<DungeonNodeID, OverworldNodeID> ExpectedEntryValues = new();
        private static readonly Dictionary<DungeonNodeID, List<
            (DungeonNodeID fromNodeID, RequirementType requirementType)>> ExpectedConnectionValue = new();
        private static readonly Dictionary<DungeonNodeID, List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>>
            ExpectedKeyDoorValues = new();

        public SPDungeonNodeFactoryTests()
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

            _sut = new SPDungeonNodeFactory(_requirements, _overworldNodes, EntryFactory, ConnectionFactory);
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
                case DungeonNodeID.SP:
                    ExpectedEntryValues.Add(id, OverworldNodeID.SPEntry);
                    break;
                case DungeonNodeID.SPAfterRiver:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.SP, RequirementType.Flippers)
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
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.SPB1, RequirementType.NoRequirement),
                            (DungeonNodeID.SPB1PastFirstRightKeyDoor, RequirementType.NoRequirement)
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
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.SPB1PastFirstRightKeyDoor, RequirementType.NoRequirement),
                            (DungeonNodeID.SPB1PastSecondRightKeyDoor, RequirementType.NoRequirement)
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
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.SPB1PastSecondRightKeyDoor, RequirementType.Hammer)
                        });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.SPB1PastLeftKeyDoor, KeyDoorID.SPB1LeftKeyDoor)
                        });
                    break;
                case DungeonNodeID.SPB1KeyLedge:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.SPB1PastRightHammerBlocks, RequirementType.Hookshot)
                        });
                    break;
                case DungeonNodeID.SPB1LeftKeyDoor:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.SPB1PastRightHammerBlocks, RequirementType.NoRequirement),
                            (DungeonNodeID.SPB1PastLeftKeyDoor, RequirementType.NoRequirement)
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
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.SPB1PastRightHammerBlocks, RequirementType.Hookshot)
                        });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.SPB1PastBackFirstKeyDoor, KeyDoorID.SPB1BackFirstKeyDoor)
                        });
                    break;
                case DungeonNodeID.SPB1BackFirstKeyDoor:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.SPB1Back, RequirementType.NoRequirement),
                            (DungeonNodeID.SPB1PastBackFirstKeyDoor, RequirementType.NoRequirement)
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
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.SPB1PastBackFirstKeyDoor, RequirementType.NoRequirement),
                            (DungeonNodeID.SPBossRoom, RequirementType.NoRequirement)
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
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.SPBossRoom, RequirementType.SPBoss)
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
    }
}