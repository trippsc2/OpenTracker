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
    public class MMDungeonNodeFactoryTests
    {
        private readonly IRequirementFactory _requirementFactory = Substitute.For<IRequirementFactory>();
        private readonly IOverworldNodeFactory _overworldNodeFactory = Substitute.For<IOverworldNodeFactory>();
        
        private readonly IRequirementDictionary _requirements;
        private readonly IOverworldNodeDictionary _overworldNodes;

        private readonly List<INode> _entryFactoryCalls = new();
        private readonly List<(INode fromNode, INode toNode, IRequirement requirement)> _connectionFactoryCalls = new();

        private readonly MMDungeonNodeFactory _sut;

        private static readonly Dictionary<DungeonNodeID, OverworldNodeID> ExpectedEntryValues = new();
        private static readonly Dictionary<DungeonNodeID, List<
            (DungeonNodeID fromNodeID, RequirementType requirementType)>> ExpectedConnectionValue = new();
        private static readonly Dictionary<DungeonNodeID, List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>>
            ExpectedKeyDoorValues = new();

        public MMDungeonNodeFactoryTests()
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

            _sut = new MMDungeonNodeFactory(_requirements, _overworldNodes, EntryFactory, ConnectionFactory);
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
                case DungeonNodeID.MM:
                    ExpectedEntryValues.Add(id, OverworldNodeID.MMEntry);
                    break;
                case DungeonNodeID.MMPastEntranceGap:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.MM, RequirementType.Hookshot),
                            (DungeonNodeID.MM, RequirementType.BonkOverLedge),
                            (DungeonNodeID.MM, RequirementType.Hover)
                        });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.MMB1TopSide, KeyDoorID.MMB1TopLeftKeyDoor),
                            (DungeonNodeID.MMB1TopSide, KeyDoorID.MMB1TopRightKeyDoor),
                            (DungeonNodeID.MMB1LeftSidePastFirstKeyDoor, KeyDoorID.MMB1LeftSideFirstKeyDoor)
                        });
                    break;
                case DungeonNodeID.MMBigChest:
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.MMPastEntranceGap, KeyDoorID.MMBigChest)
                        });
                    break;
                case DungeonNodeID.MMB1TopLeftKeyDoor:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.MMPastEntranceGap, RequirementType.NoRequirement),
                            (DungeonNodeID.MMB1TopSide, RequirementType.NoRequirement)
                        });
                    break;
                case DungeonNodeID.MMB1TopRightKeyDoor:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.MMPastEntranceGap, RequirementType.NoRequirement),
                            (DungeonNodeID.MMB1TopSide, RequirementType.NoRequirement)
                        });
                    break;
                case DungeonNodeID.MMB1TopSide:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.MMB1PastPortalBigKeyDoor, RequirementType.NoRequirement)
                        });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.MMPastEntranceGap, KeyDoorID.MMB1TopLeftKeyDoor),
                            (DungeonNodeID.MMPastEntranceGap, KeyDoorID.MMB1TopRightKeyDoor),
                            (DungeonNodeID.MMB1PastBridgeBigKeyDoor, KeyDoorID.MMBridgeBigKeyDoor)
                        });
                    break;
                case DungeonNodeID.MMB1LobbyBeyondBlueBlocks:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.MMB1TopSide, RequirementType.NoRequirement),
                            (DungeonNodeID.MMB1LeftSidePastFirstKeyDoor, RequirementType.NoRequirement)
                        });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.MMB1RightSideBeyondBlueBlocks, KeyDoorID.MMB1RightSideKeyDoor)
                        });
                    break;
                case DungeonNodeID.MMB1RightSideKeyDoor:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.MMB1LobbyBeyondBlueBlocks, RequirementType.NoRequirement),
                            (DungeonNodeID.MMB1RightSideBeyondBlueBlocks, RequirementType.NoRequirement)
                        });
                    break;
                case DungeonNodeID.MMB1RightSideBeyondBlueBlocks:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.MMB1LeftSideFirstKeyDoor, RequirementType.NoRequirement),
                            (DungeonNodeID.MMB1TopSide, RequirementType.NoRequirement)
                        });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.MMB1LobbyBeyondBlueBlocks, KeyDoorID.MMB1RightSideKeyDoor)
                        });
                    break;
                case DungeonNodeID.MMB1LeftSideFirstKeyDoor:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.MMPastEntranceGap, RequirementType.NoRequirement),
                            (DungeonNodeID.MMB1LeftSidePastFirstKeyDoor, RequirementType.NoRequirement)
                        });
                    break;
                case DungeonNodeID.MMB1LeftSidePastFirstKeyDoor:
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.MMPastEntranceGap, KeyDoorID.MMB1LeftSideFirstKeyDoor),
                            (DungeonNodeID.MMB1LeftSidePastSecondKeyDoor, KeyDoorID.MMB1LeftSideSecondKeyDoor)
                        });
                    break;
                case DungeonNodeID.MMB1LeftSideSecondKeyDoor:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.MMB1LeftSidePastFirstKeyDoor, RequirementType.NoRequirement),
                            (DungeonNodeID.MMB1LeftSidePastSecondKeyDoor, RequirementType.NoRequirement)
                        });
                    break;
                case DungeonNodeID.MMB1LeftSidePastSecondKeyDoor:
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.MMB1LeftSidePastFirstKeyDoor, KeyDoorID.MMB1LeftSideSecondKeyDoor)
                        });
                    break;
                case DungeonNodeID.MMB1PastFourTorchRoom:
                case DungeonNodeID.MMF1PastFourTorchRoom:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.MMB1LeftSidePastSecondKeyDoor, RequirementType.FireSource)
                        });
                    break;
                case DungeonNodeID.MMB1PastPortalBigKeyDoor:
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.MMPastEntranceGap, KeyDoorID.MMPortalBigKeyDoor)
                        });
                    break;
                case DungeonNodeID.MMBridgeBigKeyDoor:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.MMB1TopSide, RequirementType.NoRequirement),
                            (DungeonNodeID.MMB1PastBridgeBigKeyDoor, RequirementType.NoRequirement)
                        });
                    break;
                case DungeonNodeID.MMB1PastBridgeBigKeyDoor:
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.MMB1TopSide, KeyDoorID.MMBridgeBigKeyDoor)
                        });
                    break;
                case DungeonNodeID.MMDarkRoom:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.MMB1PastBridgeBigKeyDoor, RequirementType.DarkRoomMM)
                        });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.MMB2PastWorthlessKeyDoor, KeyDoorID.MMB2WorthlessKeyDoor)
                        });
                    break;
                case DungeonNodeID.MMB2WorthlessKeyDoor:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.MMDarkRoom, RequirementType.NoRequirement),
                            (DungeonNodeID.MMB2PastWorthlessKeyDoor, RequirementType.NoRequirement)
                        });
                    break;
                case DungeonNodeID.MMB2PastWorthlessKeyDoor:
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.MMDarkRoom, KeyDoorID.MMB2WorthlessKeyDoor)
                        });
                    break;
                case DungeonNodeID.MMB2PastCaneOfSomariaSwitch:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.MMDarkRoom, RequirementType.CaneOfSomaria)
                        });
                    break;
                case DungeonNodeID.MMBossRoom:
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.MMB2PastCaneOfSomariaSwitch, KeyDoorID.MMBossRoomBigKeyDoor)
                        });
                    break;
                case DungeonNodeID.MMBoss:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.MMBossRoom, RequirementType.MMBoss)
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
            var sut = scope.Resolve<IMMDungeonNodeFactory>();
            
            Assert.NotNull(sut as MMDungeonNodeFactory);
        }
    }
}