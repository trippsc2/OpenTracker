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
using OpenTracker.Models.Requirements.SequenceBreak;
using OpenTracker.Models.SequenceBreaks;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.Nodes.Factories
{
    public class GTDungeonNodeFactoryTests
    {
        private readonly IOverworldNodeFactory _overworldNodeFactory = Substitute.For<IOverworldNodeFactory>();
        
        private readonly IBossRequirementDictionary _bossRequirements;
        private readonly IComplexRequirementDictionary _complexRequirements;
        private readonly IItemRequirementDictionary _itemRequirements;
        private readonly ISequenceBreakRequirementDictionary _sequenceBreakRequirements;
        
        private readonly IOverworldNodeDictionary _overworldNodes;

        private readonly List<INode> _entryFactoryCalls = new();
        private readonly List<(INode fromNode, INode toNode, IRequirement? requirement)> _connectionFactoryCalls = new();

        private readonly GTDungeonNodeFactory _sut;

        private static readonly Dictionary<DungeonNodeID, OverworldNodeID> ExpectedEntryValues = new();
        private static readonly Dictionary<DungeonNodeID, List<DungeonNodeID>>
            ExpectedNoRequirementConnectionValues = new();
        private static readonly Dictionary<DungeonNodeID, List<
                (DungeonNodeID fromNodeID, BossPlacementID bossID)>> ExpectedBossConnectionValues = new();
        private static readonly Dictionary<DungeonNodeID, List<
            (DungeonNodeID fromNodeID, ComplexRequirementType requirementType)>>
            ExpectedComplexConnectionValues = new();
        private static readonly Dictionary<DungeonNodeID, List<
            (DungeonNodeID fromNodeID, ItemType type, int count)>> ExpectedItemConnectionValues = new();
        private static readonly Dictionary<DungeonNodeID, List<
            (DungeonNodeID fromNodeID, SequenceBreakType type)>> ExpectedSequenceBreakConnectionValues = new();
        private static readonly Dictionary<DungeonNodeID, List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>>
            ExpectedKeyDoorValues = new();

        public GTDungeonNodeFactoryTests()
        {
            _bossRequirements = new BossRequirementDictionary(
                Substitute.For<IBossPlacementDictionary>(),
                _ => Substitute.For<IBossRequirement>());
            _complexRequirements = new ComplexRequirementDictionary(
                () => Substitute.For<IComplexRequirementFactory>());
            _itemRequirements = new ItemRequirementDictionary(
                Substitute.For<IItemDictionary>(), (_, _) => Substitute.For<IItemRequirement>());
            _sequenceBreakRequirements = new SequenceBreakRequirementDictionary(
                Substitute.For<ISequenceBreakDictionary>(),
                _ => Substitute.For<ISequenceBreakRequirement>());
            
            _overworldNodes = new OverworldNodeDictionary(() => _overworldNodeFactory);

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

            _sut = new GTDungeonNodeFactory(
                _bossRequirements, _complexRequirements, _itemRequirements, _sequenceBreakRequirements, _overworldNodes,
                EntryFactory, ConnectionFactory);
        }

        private static void PopulateExpectedValues()
        {
            ExpectedEntryValues.Clear();
            ExpectedNoRequirementConnectionValues.Clear();
            ExpectedBossConnectionValues.Clear();
            ExpectedComplexConnectionValues.Clear();
            ExpectedItemConnectionValues.Clear();
            ExpectedSequenceBreakConnectionValues.Clear();
            ExpectedKeyDoorValues.Clear();
            
            foreach (DungeonNodeID id in Enum.GetValues(typeof(DungeonNodeID)))
            {
                switch (id)
                {
                case DungeonNodeID.GT:
                    ExpectedEntryValues.Add(id, OverworldNodeID.GTEntry);
                    break;
                case DungeonNodeID.GTBobsTorch:
                    ExpectedComplexConnectionValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ComplexRequirementType requirementType)>
                        {
                            (DungeonNodeID.GT1FLeft, ComplexRequirementType.Torch)
                        });
                    break;
                case DungeonNodeID.GT1FLeft:
                    ExpectedNoRequirementConnectionValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.GT
                    });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.GT1FRight, KeyDoorID.GT1FLeftToRightKeyDoor)
                        });
                    break;
                case DungeonNodeID.GT1FLeftToRightKeyDoor:
                    ExpectedNoRequirementConnectionValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.GT1FLeft,
                        DungeonNodeID.GT1FRight
                    });
                    break;
                case DungeonNodeID.GT1FLeftPastHammerBlocks:
                    ExpectedItemConnectionValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ItemType type, int count)>
                        {
                            (DungeonNodeID.GT1FLeft, ItemType.Hammer, 1)
                        });
                    break;
                case DungeonNodeID.GT1FLeftDMsRoom:
                    ExpectedItemConnectionValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ItemType type, int count)>
                        {
                            (DungeonNodeID.GT1FLeftPastHammerBlocks, ItemType.Hookshot, 1)
                        });
                    ExpectedComplexConnectionValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ComplexRequirementType requirementType)>
                        {
                            (DungeonNodeID.GT1FLeftPastHammerBlocks, ComplexRequirementType.Hover)
                        });
                    break;
                case DungeonNodeID.GT1FLeftPastBonkableGaps:
                    ExpectedItemConnectionValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ItemType type, int count)>
                        {
                            (DungeonNodeID.GT1FLeftPastHammerBlocks, ItemType.Hookshot, 1)
                        });
                    ExpectedComplexConnectionValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ComplexRequirementType requirementType)>
                        {
                            (DungeonNodeID.GT1FLeftPastHammerBlocks, ComplexRequirementType.BonkOverLedge),
                            (DungeonNodeID.GT1FLeftPastHammerBlocks, ComplexRequirementType.Hover)
                        });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.GT1FLeftMapChestRoom, KeyDoorID.GT1FMapChestRoomKeyDoor),
                            (DungeonNodeID.GT1FLeftSpikeTrapPortalRoom, KeyDoorID.GT1FSpikeTrapPortalRoomKeyDoor)
                        });
                    break;
                case DungeonNodeID.GT1FMapChestRoomKeyDoor:
                    ExpectedNoRequirementConnectionValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.GT1FLeftPastBonkableGaps,
                        DungeonNodeID.GT1FLeftMapChestRoom
                    });
                    break;
                case DungeonNodeID.GT1FLeftMapChestRoom:
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.GT1FLeftPastBonkableGaps, KeyDoorID.GT1FMapChestRoomKeyDoor)
                        });
                    break;
                case DungeonNodeID.GT1FSpikeTrapPortalRoomKeyDoor:
                    ExpectedNoRequirementConnectionValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.GT1FLeftPastBonkableGaps,
                        DungeonNodeID.GT1FLeftSpikeTrapPortalRoom
                    });
                    break;
                case DungeonNodeID.GT1FLeftSpikeTrapPortalRoom:
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.GT1FLeftPastBonkableGaps, KeyDoorID.GT1FSpikeTrapPortalRoomKeyDoor)
                        });
                    break;
                case DungeonNodeID.GT1FLeftFiresnakeRoom:
                    ExpectedNoRequirementConnectionValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.GT1FLeftSpikeTrapPortalRoom
                    });
                    break;
                case DungeonNodeID.GT1FLeftPastFiresnakeRoomGap:
                    ExpectedItemConnectionValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ItemType type, int count)>
                        {
                            (DungeonNodeID.GT1FLeftFiresnakeRoom, ItemType.Hookshot, 1)
                        });
                    ExpectedComplexConnectionValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ComplexRequirementType requirementType)>
                        {
                            (DungeonNodeID.GT1FLeftFiresnakeRoom, ComplexRequirementType.Hover)
                        });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.GT1FLeftPastFiresnakeRoomKeyDoor, KeyDoorID.GT1FFiresnakeRoomKeyDoor)
                        });
                    break;
                case DungeonNodeID.GT1FFiresnakeRoomKeyDoor:
                    ExpectedNoRequirementConnectionValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.GT1FLeftPastFiresnakeRoomGap,
                        DungeonNodeID.GT1FLeftPastFiresnakeRoomKeyDoor
                    });
                    break;
                case DungeonNodeID.GT1FLeftPastFiresnakeRoomKeyDoor:
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.GT1FLeftPastFiresnakeRoomGap, KeyDoorID.GT1FFiresnakeRoomKeyDoor)
                        });
                    break;
                case DungeonNodeID.GT1FLeftRandomizerRoom:
                    ExpectedNoRequirementConnectionValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.GT1FLeftPastFiresnakeRoomKeyDoor
                    });
                    break;
                case DungeonNodeID.GT1FRight:
                    ExpectedNoRequirementConnectionValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.GT
                    });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.GT1FLeft, KeyDoorID.GT1FLeftToRightKeyDoor)
                        });
                    break;
                case DungeonNodeID.GT1FRightTileRoom:
                    ExpectedItemConnectionValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ItemType type, int count)>
                        {
                            (DungeonNodeID.GT1FRight, ItemType.CaneOfSomaria, 1)
                        });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.GT1FRightFourTorchRoom, KeyDoorID.GT1FTileRoomKeyDoor)
                        });
                    break;
                case DungeonNodeID.GT1FTileRoomKeyDoor:
                    ExpectedNoRequirementConnectionValues.Add(id, new List<DungeonNodeID>
                        {
                            DungeonNodeID.GT1FRightTileRoom,
                            DungeonNodeID.GT1FRightFourTorchRoom
                        });
                    break;
                case DungeonNodeID.GT1FRightFourTorchRoom:
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.GT1FRightTileRoom, KeyDoorID.GT1FTileRoomKeyDoor)
                        });
                    break;
                case DungeonNodeID.GT1FRightCompassRoom:
                    ExpectedItemConnectionValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ItemType type, int count)>
                        {
                            (DungeonNodeID.GT1FRightFourTorchRoom, ItemType.FireRod, 1)
                        });
                    break;
                case DungeonNodeID.GT1FRightPastCompassRoomPortal:
                    ExpectedNoRequirementConnectionValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.GT1FRightCompassRoom
                    });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.GT1FRightCollapsingWalkway, KeyDoorID.GT1FCollapsingWalkwayKeyDoor)
                        });
                    break;
                case DungeonNodeID.GT1FCollapsingWalkwayKeyDoor:
                    ExpectedNoRequirementConnectionValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.GT1FRightPastCompassRoomPortal,
                        DungeonNodeID.GT1FRightCollapsingWalkway
                    });
                    break;
                case DungeonNodeID.GT1FRightCollapsingWalkway:
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.GT1FRightPastCompassRoomPortal, KeyDoorID.GT1FCollapsingWalkwayKeyDoor)
                        });
                    break;
                case DungeonNodeID.GT1FBottomRoom:
                    ExpectedNoRequirementConnectionValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.GT1FLeftRandomizerRoom,
                        DungeonNodeID.GT1FRightCollapsingWalkway
                    });
                    break;
                case DungeonNodeID.GTBoss1:
                    ExpectedBossConnectionValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, BossPlacementID bossID)>
                        {
                            (DungeonNodeID.GT1FBottomRoom, BossPlacementID.GTBoss1)
                        });
                    break;
                case DungeonNodeID.GTB1BossChests:
                    ExpectedNoRequirementConnectionValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.GTBoss1
                    });
                    break;
                case DungeonNodeID.GTBigChest:
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.GT1FBottomRoom, KeyDoorID.GTBigChest)
                        });
                    break;
                case DungeonNodeID.GT3FPastRedGoriyaRooms:
                    ExpectedSequenceBreakConnectionValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, SequenceBreakType type)>
                        {
                            (DungeonNodeID.GT, SequenceBreakType.MimicClip)
                        });
                    ExpectedComplexConnectionValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ComplexRequirementType requirementType)>
                        {
                            (DungeonNodeID.GT, ComplexRequirementType.RedEyegoreGoriya)
                        });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.GT3FPastBigKeyDoor, KeyDoorID.GT3FBigKeyDoor)
                        });
                    break;
                case DungeonNodeID.GT3FBigKeyDoor:
                    ExpectedNoRequirementConnectionValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.GT3FPastRedGoriyaRooms,
                        DungeonNodeID.GT3FPastBigKeyDoor
                    });
                    break;
                case DungeonNodeID.GT3FPastBigKeyDoor:
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.GT3FPastRedGoriyaRooms, KeyDoorID.GT3FBigKeyDoor)
                        });
                    break;
                case DungeonNodeID.GTBoss2:
                    ExpectedBossConnectionValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, BossPlacementID bossID)>
                        {
                            (DungeonNodeID.GT3FPastBigKeyDoor, BossPlacementID.GTBoss2)
                        });
                    break;
                case DungeonNodeID.GT4FPastBoss2:
                    ExpectedNoRequirementConnectionValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.GTBoss2
                    });
                    break;
                case DungeonNodeID.GT5FPastFourTorchRooms:
                    ExpectedComplexConnectionValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ComplexRequirementType requirementType)>
                        {
                            (DungeonNodeID.GT4FPastBoss2, ComplexRequirementType.FireSource)
                        });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.GT6FPastFirstKeyDoor, KeyDoorID.GT6FFirstKeyDoor)
                        });
                    break;
                case DungeonNodeID.GT6FFirstKeyDoor:
                    ExpectedNoRequirementConnectionValues.Add(id, new List<DungeonNodeID> 
                    {
                        DungeonNodeID.GT5FPastFourTorchRooms,
                        DungeonNodeID.GT6FPastFirstKeyDoor
                    });
                    break;
                case DungeonNodeID.GT6FPastFirstKeyDoor:
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.GT5FPastFourTorchRooms, KeyDoorID.GT6FFirstKeyDoor),
                            (DungeonNodeID.GT6FBossRoom, KeyDoorID.GT6FSecondKeyDoor)
                        });
                    break;
                case DungeonNodeID.GT6FSecondKeyDoor:
                    ExpectedNoRequirementConnectionValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.GT6FPastFirstKeyDoor,
                        DungeonNodeID.GT6FBossRoom
                    });
                    break;
                case DungeonNodeID.GT6FBossRoom:
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.GT6FPastFirstKeyDoor, KeyDoorID.GT6FSecondKeyDoor)
                        });
                    break;
                case DungeonNodeID.GTBoss3:
                    ExpectedBossConnectionValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, BossPlacementID bossID)>
                        {
                            (DungeonNodeID.GT6FBossRoom, BossPlacementID.GTBoss3)
                        });
                    break;
                case DungeonNodeID.GTBoss3Item:
                    ExpectedItemConnectionValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ItemType type, int count)>
                        {
                            (DungeonNodeID.GTBoss3, ItemType.Hookshot, 1)
                        });
                    ExpectedComplexConnectionValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ComplexRequirementType requirementType)>
                        {
                            (DungeonNodeID.GTBoss3, ComplexRequirementType.Hover)
                        });
                    break;
                case DungeonNodeID.GT6FPastBossRoomGap:
                    ExpectedNoRequirementConnectionValues.Add(id, new List<DungeonNodeID>
                    {
                        DungeonNodeID.GTBoss3Item
                    });
                    ExpectedComplexConnectionValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, ComplexRequirementType requirementType)>
                        {
                            (DungeonNodeID.GT6FBossRoom, ComplexRequirementType.Hover)
                        });
                    break;
                case DungeonNodeID.GTFinalBossRoom:
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.GT6FPastBossRoomGap, KeyDoorID.GT7FBigKeyDoor)
                        });
                    break;
                case DungeonNodeID.GTFinalBoss:
                    ExpectedBossConnectionValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, BossPlacementID bossID)>
                        {
                            (DungeonNodeID.GTFinalBossRoom, BossPlacementID.GTFinalBoss)
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

            return (from id in ExpectedNoRequirementConnectionValues.Keys from
                    value in ExpectedNoRequirementConnectionValues[id]
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

            return (from id in ExpectedBossConnectionValues.Keys from value in ExpectedBossConnectionValues[id]
                select new object[] {id, value.fromNodeID, value.bossID}).ToList();
        }

        [Theory]
        [MemberData(nameof(PopulateNodeConnections_ShouldCreateExpectedComplexConnectionsData))]
        public void PopulateNodeConnections_ShouldCreateExpectedComplexConnections(
            DungeonNodeID id, DungeonNodeID fromNodeID, ComplexRequirementType requirementType)
        {
            var dungeonData = Substitute.For<IMutableDungeon>();
            var node = Substitute.For<IDungeonNode>();
            var connections = new List<INodeConnection>();
            _sut.PopulateNodeConnections(dungeonData, id, node, connections);

            Assert.Contains(_connectionFactoryCalls, x =>
                x.fromNode == dungeonData.Nodes[fromNodeID] && x.requirement == _complexRequirements[requirementType]);
        }

        public static IEnumerable<object[]> PopulateNodeConnections_ShouldCreateExpectedComplexConnectionsData()
        {
            PopulateExpectedValues();

            return (from id in ExpectedComplexConnectionValues.Keys from value in ExpectedComplexConnectionValues[id]
                select new object[] {id, value.fromNodeID, value.requirementType}).ToList();
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

            return (from id in ExpectedItemConnectionValues.Keys from value in ExpectedItemConnectionValues[id]
                select new object[] {id, value.fromNodeID, value.type, value.count}).ToList();
        }

        [Theory]
        [MemberData(nameof(PopulateNodeConnections_ShouldCreateExpectedSequenceBreakConnectionsData))]
        public void PopulateNodeConnections_ShouldCreateExpectedSequenceBreakConnections(
            DungeonNodeID id, DungeonNodeID fromNodeID, SequenceBreakType type)
        {
            var dungeonData = Substitute.For<IMutableDungeon>();
            var node = Substitute.For<IDungeonNode>();
            var connections = new List<INodeConnection>();
            _sut.PopulateNodeConnections(dungeonData, id, node, connections);

            Assert.Contains(_connectionFactoryCalls, x =>
                x.fromNode == dungeonData.Nodes[fromNodeID] && x.requirement == _sequenceBreakRequirements[type]);
        }

        public static IEnumerable<object[]> PopulateNodeConnections_ShouldCreateExpectedSequenceBreakConnectionsData()
        {
            PopulateExpectedValues();

            return (from id in ExpectedSequenceBreakConnectionValues.Keys from value in
                ExpectedSequenceBreakConnectionValues[id] select new
                object[] {id, value.fromNodeID, value.type}).ToList();
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
            var sut = scope.Resolve<IGTDungeonNodeFactory>();
            
            Assert.NotNull(sut as GTDungeonNodeFactory);
        }
    }
}