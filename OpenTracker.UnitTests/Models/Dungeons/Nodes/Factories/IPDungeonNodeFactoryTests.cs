using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
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
    public class IPDungeonNodeFactoryTests
    {
        private readonly IRequirementFactory _requirementFactory = Substitute.For<IRequirementFactory>();
        private readonly IOverworldNodeFactory _overworldNodeFactory = Substitute.For<IOverworldNodeFactory>();
        
        private readonly IRequirementDictionary _requirements;
        private readonly IOverworldNodeDictionary _overworldNodes;

        private readonly List<INode> _entryFactoryCalls = new();
        private readonly List<(INode fromNode, INode toNode, IRequirement requirement)> _connectionFactoryCalls = new();

        private readonly IPDungeonNodeFactory _sut;

        private static readonly Dictionary<DungeonNodeID, OverworldNodeID> ExpectedEntryValues = new();
        private static readonly Dictionary<DungeonNodeID, List<
            (DungeonNodeID fromNodeID, RequirementType requirementType)>> ExpectedConnectionValue = new();
        private static readonly Dictionary<DungeonNodeID, List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>>
            ExpectedKeyDoorValues = new();

        public IPDungeonNodeFactoryTests()
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

            _sut = new IPDungeonNodeFactory(_requirements, _overworldNodes, EntryFactory, ConnectionFactory);
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
                    case DungeonNodeID.IP:
                        ExpectedEntryValues.Add(id, OverworldNodeID.IPEntry);
                        break;
                    case DungeonNodeID.IPPastEntranceFreezorRoom:
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.IP, RequirementType.MeltThings)
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
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.IPB2PastLiftBlock, RequirementType.NoRequirement),
                                (DungeonNodeID.IPB1LeftSide, RequirementType.IPIceBreaker)
                            });
                        break;
                    case DungeonNodeID.IPB2LeftSide:
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.IPB1LeftSide, RequirementType.NoRequirement)
                            });
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.IPB2PastKeyDoor, KeyDoorID.IPB2KeyDoor)
                            });
                        break;
                    case DungeonNodeID.IPB2KeyDoor:
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.IPB2LeftSide, RequirementType.NoRequirement),
                                (DungeonNodeID.IPB2PastKeyDoor, RequirementType.NoRequirement)
                            });
                        break;
                    case DungeonNodeID.IPB2PastKeyDoor:
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.IPB4FreezorRoom, RequirementType.NoRequirement)
                            });
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.IPB2LeftSide, KeyDoorID.IPB2KeyDoor),
                                (DungeonNodeID.IPSpikeRoom, KeyDoorID.IPB3KeyDoor)
                            });
                        break;
                    case DungeonNodeID.IPB2PastHammerBlocks:
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.IPSpikeRoom, RequirementType.Hammer)
                            });
                        break;
                    case DungeonNodeID.IPB2PastLiftBlock:
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.IPB2PastHammerBlocks, RequirementType.Gloves1)
                            });
                        break;
                    case DungeonNodeID.IPB3KeyDoor:
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.IPB2PastKeyDoor, RequirementType.NoRequirement),
                                (DungeonNodeID.IPSpikeRoom, RequirementType.NoRequirement)
                            });
                        break;
                    case DungeonNodeID.IPSpikeRoom:
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.IPB1RightSide, RequirementType.NoRequirement),
                                (DungeonNodeID.IPB4RightSide, RequirementType.NoRequirement)
                            });
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.IPB2PastKeyDoor, KeyDoorID.IPB3KeyDoor)
                            });
                        break;
                    case DungeonNodeID.IPB4RightSide:
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.IPSpikeRoom, RequirementType.NoRequirement),
                                (DungeonNodeID.IPB4IceRoom, RequirementType.Hookshot),
                                (DungeonNodeID.IPB4IceRoom, RequirementType.BombJumpIPHookshotGap),
                                (DungeonNodeID.IPB4IceRoom, RequirementType.Hover)
                            });
                        break;
                    case DungeonNodeID.IPB4IceRoom:
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.IPB2PastKeyDoor, RequirementType.NoRequirement),
                                (DungeonNodeID.IPB4FreezorRoom, RequirementType.BombJumpIPFreezorRoomGap),
                                (DungeonNodeID.IPB4FreezorRoom, RequirementType.Hover)
                            });
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.IPB4PastKeyDoor, KeyDoorID.IPB4KeyDoor)
                            });
                        break;
                    case DungeonNodeID.IPB4FreezorRoom:
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.IPB2PastKeyDoor, RequirementType.NoRequirement),
                                (DungeonNodeID.IPB4IceRoom, RequirementType.BombJumpIPFreezorRoomGap),
                                (DungeonNodeID.IPB4IceRoom, RequirementType.Hover)
                            });
                        break;
                    case DungeonNodeID.IPFreezorChest:
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.IPB4FreezorRoom, RequirementType.MeltThings)
                            });
                        break;
                    case DungeonNodeID.IPB4KeyDoor:
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.IPB4IceRoom, RequirementType.NoRequirement),
                                (DungeonNodeID.IPB4PastKeyDoor, RequirementType.NoRequirement)
                            });
                        break;
                    case DungeonNodeID.IPB4PastKeyDoor:
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.IPB5, RequirementType.NoRequirement)
                            });
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.IPB4IceRoom, KeyDoorID.IPB4KeyDoor)
                            });
                        break;
                    case DungeonNodeID.IPBigChestArea:
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.IPB4FreezorRoom, RequirementType.NoRequirement)
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
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.IPB4FreezorRoom, RequirementType.NoRequirement),
                                (DungeonNodeID.IPB4PastKeyDoor, RequirementType.NoRequirement)
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
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.IPB5, RequirementType.BombJumpIPBJ),
                                (DungeonNodeID.IPB5, RequirementType.IPIceBreaker)
                            });
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.IPB5PastBigKeyDoor, KeyDoorID.IPB5KeyDoor),
                                (DungeonNodeID.IPB6PastKeyDoor, KeyDoorID.IPB6KeyDoor)
                            });
                        break;
                    case DungeonNodeID.IPB6KeyDoor:
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.IPB6, RequirementType.NoRequirement),
                                (DungeonNodeID.IPB6PastKeyDoor, RequirementType.NoRequirement)
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
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.IPB6, RequirementType.CaneOfSomaria),
                                (DungeonNodeID.IPB6PastKeyDoor, RequirementType.NoRequirement),
                                (DungeonNodeID.IPB6, RequirementType.BombJumpIPBJ),
                                (DungeonNodeID.IPB6, RequirementType.IPIceBreaker)
                            });
                        break;
                    case DungeonNodeID.IPB6PastHammerBlocks:
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.IPB6PreBossRoom, RequirementType.Hammer)
                            });
                        break;
                    case DungeonNodeID.IPB6PastLiftBlock:
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.IPB6PastHammerBlocks, RequirementType.Gloves1)
                            });
                        break;
                    case DungeonNodeID.IPBoss:
                        ExpectedConnectionValue.Add(id,
                            new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                            {
                                (DungeonNodeID.IPB6PastLiftBlock, RequirementType.IPBoss)
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
            var sut = scope.Resolve<IIPDungeonNodeFactory>();
            
            Assert.NotNull(sut as IPDungeonNodeFactory);
        }
    }
}