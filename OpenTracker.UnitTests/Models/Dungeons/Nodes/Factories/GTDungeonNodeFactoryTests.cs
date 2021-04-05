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
    public class GTDungeonNodeFactoryTests
    {
        private readonly IRequirementFactory _requirementFactory = Substitute.For<IRequirementFactory>();
        private readonly IOverworldNodeFactory _overworldNodeFactory = Substitute.For<IOverworldNodeFactory>();
        
        private readonly IRequirementDictionary _requirements;
        private readonly IOverworldNodeDictionary _overworldNodes;

        private readonly List<INode> _entryFactoryCalls = new();
        private readonly List<(INode fromNode, INode toNode, IRequirement requirement)> _connectionFactoryCalls = new();

        private readonly GTDungeonNodeFactory _sut;

        private static readonly Dictionary<DungeonNodeID, OverworldNodeID> ExpectedEntryValues = new();
        private static readonly Dictionary<DungeonNodeID, List<
            (DungeonNodeID fromNodeID, RequirementType requirementType)>> ExpectedConnectionValue = new();
        private static readonly Dictionary<DungeonNodeID, List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>>
            ExpectedKeyDoorValues = new();

        public GTDungeonNodeFactoryTests()
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

            _sut = new GTDungeonNodeFactory(_requirements, _overworldNodes, EntryFactory, ConnectionFactory);
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
                case DungeonNodeID.GT:
                    ExpectedEntryValues.Add(id, OverworldNodeID.GTEntry);
                    break;
                case DungeonNodeID.GTBobsTorch:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.GT1FLeft, RequirementType.Torch)
                        });
                    break;
                case DungeonNodeID.GT1FLeft:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.GT, RequirementType.NoRequirement)
                        });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.GT1FRight, KeyDoorID.GT1FLeftToRightKeyDoor)
                        });
                    break;
                case DungeonNodeID.GT1FLeftToRightKeyDoor:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.GT1FLeft, RequirementType.NoRequirement),
                            (DungeonNodeID.GT1FRight, RequirementType.NoRequirement)
                        });
                    break;
                case DungeonNodeID.GT1FLeftPastHammerBlocks:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.GT1FLeft, RequirementType.Hammer)
                        });
                    break;
                case DungeonNodeID.GT1FLeftDMsRoom:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.GT1FLeftPastHammerBlocks, RequirementType.Hookshot),
                            (DungeonNodeID.GT1FLeftPastHammerBlocks, RequirementType.Hover)
                        });
                    break;
                case DungeonNodeID.GT1FLeftPastBonkableGaps:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.GT1FLeftPastHammerBlocks, RequirementType.Hookshot),
                            (DungeonNodeID.GT1FLeftPastHammerBlocks, RequirementType.BonkOverLedge),
                            (DungeonNodeID.GT1FLeftPastHammerBlocks, RequirementType.Hover)
                        });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.GT1FLeftMapChestRoom, KeyDoorID.GT1FMapChestRoomKeyDoor),
                            (DungeonNodeID.GT1FLeftSpikeTrapPortalRoom, KeyDoorID.GT1FSpikeTrapPortalRoomKeyDoor)
                        });
                    break;
                case DungeonNodeID.GT1FMapChestRoomKeyDoor:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.GT1FLeftPastBonkableGaps, RequirementType.NoRequirement),
                            (DungeonNodeID.GT1FLeftMapChestRoom, RequirementType.NoRequirement)
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
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.GT1FLeftPastBonkableGaps, RequirementType.NoRequirement),
                            (DungeonNodeID.GT1FLeftSpikeTrapPortalRoom, RequirementType.NoRequirement)
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
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.GT1FLeftSpikeTrapPortalRoom, RequirementType.NoRequirement)
                        });
                    break;
                case DungeonNodeID.GT1FLeftPastFiresnakeRoomGap:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.GT1FLeftFiresnakeRoom, RequirementType.Hookshot),
                            (DungeonNodeID.GT1FLeftFiresnakeRoom, RequirementType.Hover)
                        });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.GT1FLeftPastFiresnakeRoomKeyDoor, KeyDoorID.GT1FFiresnakeRoomKeyDoor)
                        });
                    break;
                case DungeonNodeID.GT1FFiresnakeRoomKeyDoor:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.GT1FLeftPastFiresnakeRoomGap, RequirementType.NoRequirement),
                            (DungeonNodeID.GT1FLeftPastFiresnakeRoomKeyDoor, RequirementType.NoRequirement)
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
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.GT1FLeftPastFiresnakeRoomKeyDoor, RequirementType.NoRequirement)
                        });
                    break;
                case DungeonNodeID.GT1FRight:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.GT, RequirementType.NoRequirement)
                        });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.GT1FLeft, KeyDoorID.GT1FLeftToRightKeyDoor)
                        });
                    break;
                case DungeonNodeID.GT1FRightTileRoom:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.GT1FRight, RequirementType.CaneOfSomaria)
                        });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.GT1FRightFourTorchRoom, KeyDoorID.GT1FTileRoomKeyDoor)
                        });
                    break;
                case DungeonNodeID.GT1FTileRoomKeyDoor:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.GT1FRightTileRoom, RequirementType.NoRequirement),
                            (DungeonNodeID.GT1FRightFourTorchRoom, RequirementType.NoRequirement)
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
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.GT1FRightFourTorchRoom, RequirementType.FireRod)
                        });
                    break;
                case DungeonNodeID.GT1FRightPastCompassRoomPortal:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.GT1FRightCompassRoom, RequirementType.NoRequirement)
                        });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.GT1FRightCollapsingWalkway, KeyDoorID.GT1FCollapsingWalkwayKeyDoor)
                        });
                    break;
                case DungeonNodeID.GT1FCollapsingWalkwayKeyDoor:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.GT1FRightPastCompassRoomPortal, RequirementType.NoRequirement),
                            (DungeonNodeID.GT1FRightCollapsingWalkway, RequirementType.NoRequirement)
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
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.GT1FLeftRandomizerRoom, RequirementType.NoRequirement),
                            (DungeonNodeID.GT1FRightCollapsingWalkway, RequirementType.NoRequirement)
                        });
                    break;
                case DungeonNodeID.GTBoss1:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.GT1FBottomRoom, RequirementType.GTBoss1)
                        });
                    break;
                case DungeonNodeID.GTB1BossChests:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.GTBoss1, RequirementType.NoRequirement)
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
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.GT, RequirementType.RedEyegoreGoriya),
                            (DungeonNodeID.GT, RequirementType.MimicClip)
                        });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.GT3FPastBigKeyDoor, KeyDoorID.GT3FBigKeyDoor)
                        });
                    break;
                case DungeonNodeID.GT3FBigKeyDoor:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.GT3FPastRedGoriyaRooms, RequirementType.NoRequirement),
                            (DungeonNodeID.GT3FPastBigKeyDoor, RequirementType.NoRequirement)
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
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.GT3FPastBigKeyDoor, RequirementType.GTBoss2)
                        });
                    break;
                case DungeonNodeID.GT4FPastBoss2:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.GTBoss2, RequirementType.NoRequirement)
                        });
                    break;
                case DungeonNodeID.GT5FPastFourTorchRooms:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.GT4FPastBoss2, RequirementType.FireSource)
                        });
                    ExpectedKeyDoorValues.Add(id,
                        new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                        {
                            (DungeonNodeID.GT6FPastFirstKeyDoor, KeyDoorID.GT6FFirstKeyDoor)
                        });
                    break;
                case DungeonNodeID.GT6FFirstKeyDoor:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.GT5FPastFourTorchRooms, RequirementType.NoRequirement),
                            (DungeonNodeID.GT6FPastFirstKeyDoor, RequirementType.NoRequirement)
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
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.GT6FPastFirstKeyDoor, RequirementType.NoRequirement),
                            (DungeonNodeID.GT6FBossRoom, RequirementType.NoRequirement)
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
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.GT6FBossRoom, RequirementType.GTBoss3)
                        });
                    break;
                case DungeonNodeID.GTBoss3Item:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.GTBoss3, RequirementType.Hookshot),
                            (DungeonNodeID.GTBoss3, RequirementType.Hover)
                        });
                    break;
                case DungeonNodeID.GT6FPastBossRoomGap:
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.GTBoss3Item, RequirementType.NoRequirement),
                            (DungeonNodeID.GT6FBossRoom, RequirementType.Hover)
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
                    ExpectedConnectionValue.Add(id,
                        new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
                        {
                            (DungeonNodeID.GTFinalBossRoom, RequirementType.GTFinalBoss)
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
            var sut = scope.Resolve<IGTDungeonNodeFactory>();
            
            Assert.NotNull(sut as GTDungeonNodeFactory);
        }
    }
}