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
using OpenTracker.Models.NodeConnections;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Boss;
using OpenTracker.Models.Requirements.Complex;
using OpenTracker.Models.Requirements.Item;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.Nodes.Factories
{
    public class MMDungeonNodeFactoryTests
    {
        private readonly IBossRequirementDictionary _bossRequirements;
        private readonly IComplexRequirementDictionary _complexRequirements;
        private readonly IItemRequirementDictionary _itemRequirements;
        
        private readonly IOverworldNodeDictionary _overworldNodes;

        private readonly List<INode> _entryFactoryCalls = new();
        private readonly List<(INode fromNode, INode toNode, IRequirement? requirement)> _connectionFactoryCalls = new();

        private readonly MMDungeonNodeFactory _sut;

        private static readonly Dictionary<DungeonNodeID, OverworldNodeID> ExpectedEntryValues = new();
        private static readonly Dictionary<DungeonNodeID, List<DungeonNodeID>> ExpectedNoRequirementValues = new();
        private static readonly Dictionary<DungeonNodeID,
            List<(DungeonNodeID fromNodeID, BossPlacementID bossID)>> ExpectedBossValues = new();
        private static readonly Dictionary<DungeonNodeID,
            List<(DungeonNodeID fromNodeID, ComplexRequirementType type)>> ExpectedComplexValues = new();
        private static readonly Dictionary<DungeonNodeID,
            List<(DungeonNodeID fromNodeID, ItemType type, int count)>> ExpectedItemValues = new();
        private static readonly Dictionary<DungeonNodeID,
            List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>> ExpectedKeyDoorValues = new();

        public MMDungeonNodeFactoryTests()
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

            _sut = new MMDungeonNodeFactory(
                _bossRequirements, _complexRequirements, _itemRequirements, _overworldNodes, EntryFactory,
                ConnectionFactory);
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
                case DungeonNodeID.MM:
                    ExpectedEntryValues.Add(id, OverworldNodeID.MMEntry);
                    break;
                case DungeonNodeID.MMPastEntranceGap:
                    ExpectedComplexValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ComplexRequirementType type)>
                        {
                            (DungeonNodeID.MM, ComplexRequirementType.BonkOverLedge),
                            (DungeonNodeID.MM, ComplexRequirementType.Hover)
                        });
                    ExpectedItemValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ItemType type, int count)>
                        {
                            (DungeonNodeID.MM, ItemType.Hookshot, 1)
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
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.MMPastEntranceGap,
                        DungeonNodeID.MMB1TopSide
                    });
                    break;
                case DungeonNodeID.MMB1TopRightKeyDoor:
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.MMPastEntranceGap,
                        DungeonNodeID.MMB1TopSide
                    });
                    break;
                case DungeonNodeID.MMB1TopSide:
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.MMB1PastPortalBigKeyDoor
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
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.MMB1TopSide,
                        DungeonNodeID.MMB1LeftSidePastFirstKeyDoor
                    });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.MMB1RightSideBeyondBlueBlocks, KeyDoorID.MMB1RightSideKeyDoor)
                        });
                    break;
                case DungeonNodeID.MMB1RightSideKeyDoor:
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.MMB1LobbyBeyondBlueBlocks,
                        DungeonNodeID.MMB1RightSideBeyondBlueBlocks
                    });
                    break;
                case DungeonNodeID.MMB1RightSideBeyondBlueBlocks:
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.MMB1LeftSideFirstKeyDoor,
                        DungeonNodeID.MMB1TopSide
                    });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.MMB1LobbyBeyondBlueBlocks, KeyDoorID.MMB1RightSideKeyDoor)
                        });
                    break;
                case DungeonNodeID.MMB1LeftSideFirstKeyDoor:
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.MMPastEntranceGap,
                        DungeonNodeID.MMB1LeftSidePastFirstKeyDoor
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
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.MMB1LeftSidePastFirstKeyDoor,
                        DungeonNodeID.MMB1LeftSidePastSecondKeyDoor
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
                    ExpectedComplexValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ComplexRequirementType type)>
                        {
                            (DungeonNodeID.MMB1LeftSidePastSecondKeyDoor, ComplexRequirementType.FireSource)
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
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.MMB1TopSide,
                        DungeonNodeID.MMB1PastBridgeBigKeyDoor
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
                    ExpectedComplexValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ComplexRequirementType type)>
                        {
                            (DungeonNodeID.MMB1PastBridgeBigKeyDoor, ComplexRequirementType.DarkRoomMM)
                        });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.MMB2PastWorthlessKeyDoor, KeyDoorID.MMB2WorthlessKeyDoor)
                        });
                    break;
                case DungeonNodeID.MMB2WorthlessKeyDoor:
                    ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.MMDarkRoom,
                        DungeonNodeID.MMB2PastWorthlessKeyDoor
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
                    ExpectedItemValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ItemType type, int count)>
                        {
                            (DungeonNodeID.MMDarkRoom, ItemType.CaneOfSomaria, 1)
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
                    ExpectedBossValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, BossPlacementID bossID)>
                        {
                            (DungeonNodeID.MMBossRoom, BossPlacementID.MMBoss)
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
        [MemberData(nameof(PopulateNodeConnections_ShouldCreateExpectedComplexConnectionsData))]
        public void PopulateNodeConnections_ShouldCreateExpectedComplexConnections(
            DungeonNodeID id, DungeonNodeID fromNodeID, ComplexRequirementType type)
        {
            var dungeonData = Substitute.For<IMutableDungeon>();
            var node = Substitute.For<IDungeonNode>();
            var connections = new List<INodeConnection>();
            _sut.PopulateNodeConnections(dungeonData, id, node, connections);

            Assert.Contains(_connectionFactoryCalls, x =>
                x.fromNode == dungeonData.Nodes[fromNodeID] && x.requirement == _complexRequirements[type]);
        }

        public static IEnumerable<object[]> PopulateNodeConnections_ShouldCreateExpectedComplexConnectionsData()
        {
            PopulateExpectedValues();

            return (from id in ExpectedComplexValues.Keys from value in ExpectedComplexValues[id]
                select new object[] {id, value.fromNodeID, value.type}).ToList();
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
            var sut = scope.Resolve<IMMDungeonNodeFactory>();
            
            Assert.NotNull(sut as MMDungeonNodeFactory);
        }
    }
}