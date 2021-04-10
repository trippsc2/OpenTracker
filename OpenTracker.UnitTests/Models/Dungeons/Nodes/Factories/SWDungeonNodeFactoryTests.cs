// using System;
// using System.Collections.Generic;
// using System.Linq;
// using Autofac;
// using NSubstitute;
// using OpenTracker.Models.Dungeons.KeyDoors;
// using OpenTracker.Models.Dungeons.Mutable;
// using OpenTracker.Models.Dungeons.Nodes;
// using OpenTracker.Models.Dungeons.Nodes.Factories;
// using OpenTracker.Models.NodeConnections;
// using OpenTracker.Models.Nodes;
// using OpenTracker.Models.Requirements;
// using Xunit;
//
// namespace OpenTracker.UnitTests.Models.Dungeons.Nodes.Factories
// {
//     public class SWDungeonNodeFactoryTests
//     {
//         private readonly IRequirementFactory _requirementFactory = Substitute.For<IRequirementFactory>();
//         private readonly IOverworldNodeFactory _overworldNodeFactory = Substitute.For<IOverworldNodeFactory>();
//         
//         private readonly IRequirementDictionary _requirements;
//         private readonly IOverworldNodeDictionary _overworldNodes;
//
//         private readonly List<INode> _entryFactoryCalls = new();
//         private readonly List<(INode fromNode, INode toNode, IRequirement requirement)> _connectionFactoryCalls = new();
//
//         private readonly SWDungeonNodeFactory _sut;
//
//         private static readonly Dictionary<DungeonNodeID, OverworldNodeID> ExpectedEntryValues = new();
//         private static readonly Dictionary<DungeonNodeID, List<
//             (DungeonNodeID fromNodeID, RequirementType requirementType)>> ExpectedConnectionValue = new();
//         private static readonly Dictionary<DungeonNodeID, List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>>
//             ExpectedKeyDoorValues = new();
//
//         public SWDungeonNodeFactoryTests()
//         {
//             _requirements = new RequirementDictionary(() => _requirementFactory);
//             _overworldNodes = new OverworldNodeDictionary(() => _overworldNodeFactory);
//
//             IEntryNodeConnection EntryFactory(INode fromNode)
//             {
//                 _entryFactoryCalls.Add(fromNode);
//                 return Substitute.For<IEntryNodeConnection>();
//             }
//
//             INodeConnection ConnectionFactory(INode fromNode, INode toNode, IRequirement requirement)
//             {
//                 _connectionFactoryCalls.Add((fromNode, toNode, requirement));
//                 return Substitute.For<INodeConnection>();
//             }
//
//             _sut = new SWDungeonNodeFactory(_overworldNodes, EntryFactory, ConnectionFactory);
//         }
//
//         private static void PopulateExpectedValues()
//         {
//             ExpectedEntryValues.Clear();
//             ExpectedConnectionValue.Clear();
//             ExpectedKeyDoorValues.Clear();
//             
//             foreach (DungeonNodeID id in Enum.GetValues(typeof(DungeonNodeID)))
//             {
//                 switch (id)
//                 {
//                     case DungeonNodeID.SWBigChestAreaBottom:
//                         ExpectedEntryValues.Add(id, OverworldNodeID.SWFrontEntry);
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.SWBigChestAreaTop, RequirementType.NoRequirement)
//                             });
//                         ExpectedKeyDoorValues.Add(id,
//                             new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
//                             {
//                                 (DungeonNodeID.SWFrontLeftSide, KeyDoorID.SWFrontLeftKeyDoor),
//                                 (DungeonNodeID.SWFrontRightSide, KeyDoorID.SWFrontRightKeyDoor)
//                             });
//                         break;
//                     case DungeonNodeID.SWBigChestAreaTop:
//                         ExpectedEntryValues.Add(id, OverworldNodeID.SWFrontEntry);
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.SWBigChestAreaBottom, RequirementType.Hookshot),
//                                 (DungeonNodeID.SWBigChestAreaBottom, RequirementType.BombJumpSWBigChest)
//                             });
//                         break;
//                     case DungeonNodeID.SWBigChest:
//                         ExpectedKeyDoorValues.Add(id,
//                             new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
//                             {
//                                 (DungeonNodeID.SWBigChestAreaTop, KeyDoorID.SWBigChest)
//                             });
//                         break;
//                     case DungeonNodeID.SWFrontLeftKeyDoor:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.SWFrontLeftSide, RequirementType.NoRequirement),
//                                 (DungeonNodeID.SWBigChestAreaBottom, RequirementType.NoRequirement)
//                             });
//                         break;
//                     case DungeonNodeID.SWFrontLeftSide:
//                         ExpectedEntryValues.Add(id, OverworldNodeID.SWFrontEntry);
//                         ExpectedKeyDoorValues.Add(id,
//                             new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
//                             {
//                                 (DungeonNodeID.SWBigChestAreaBottom, KeyDoorID.SWFrontLeftKeyDoor)
//                             });
//                        break;
//                     case DungeonNodeID.SWFrontRightKeyDoor:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.SWFrontRightSide, RequirementType.NoRequirement),
//                                 (DungeonNodeID.SWBigChestAreaBottom, RequirementType.NoRequirement)
//                             });
//                         break;
//                     case DungeonNodeID.SWFrontRightSide:
//                         ExpectedEntryValues.Add(id, OverworldNodeID.SWFrontEntry);
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.SWFrontLeftSide, RequirementType.NoRequirement)
//                             });
//                         ExpectedKeyDoorValues.Add(id,
//                             new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
//                             {
//                                 (DungeonNodeID.SWBigChestAreaBottom, KeyDoorID.SWFrontRightKeyDoor)
//                             });
//                         break;
//                     case DungeonNodeID.SWFrontBackConnector:
//                         ExpectedEntryValues.Add(id, OverworldNodeID.SWFrontEntry);
//                         ExpectedKeyDoorValues.Add(id,
//                             new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
//                             {
//                                 (DungeonNodeID.SWPastTheWorthlessKeyDoor, KeyDoorID.SWWorthlessKeyDoor)
//                             });
//                         break;
//                     case DungeonNodeID.SWWorthlessKeyDoor:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.SWFrontBackConnector, RequirementType.NoRequirement),
//                                 (DungeonNodeID.SWPastTheWorthlessKeyDoor, RequirementType.NoRequirement)
//                             });
//                         break;
//                     case DungeonNodeID.SWPastTheWorthlessKeyDoor:
//                         ExpectedKeyDoorValues.Add(id,
//                             new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
//                             {
//                                 (DungeonNodeID.SWFrontBackConnector, KeyDoorID.SWWorthlessKeyDoor)
//                             });
//                         break;
//                     case DungeonNodeID.SWBack:
//                         ExpectedEntryValues.Add(id, OverworldNodeID.SWBackEntry);
//                         ExpectedKeyDoorValues.Add(id,
//                             new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
//                             {
//                                 (DungeonNodeID.SWBackFirstKeyDoor, KeyDoorID.SWBackFirstKeyDoor)
//                             });
//                         break;
//                     case DungeonNodeID.SWBackFirstKeyDoor:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.SWBack, RequirementType.NoRequirement),
//                                 (DungeonNodeID.SWBackPastFirstKeyDoor, RequirementType.NoRequirement)
//                             });
//                         break;
//                     case DungeonNodeID.SWBackPastFirstKeyDoor:
//                         ExpectedKeyDoorValues.Add(id,
//                             new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
//                             {
//                                 (DungeonNodeID.SWBack, KeyDoorID.SWBackFirstKeyDoor)
//                             });
//                         break;
//                     case DungeonNodeID.SWBackPastFourTorchRoom:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.SWBackPastFirstKeyDoor, RequirementType.FireRod)
//                             });
//                         break;
//                     case DungeonNodeID.SWBackPastCurtains:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.SWBackPastFourTorchRoom, RequirementType.Curtains)
//                             });
//                         ExpectedKeyDoorValues.Add(id,
//                             new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
//                             {
//                                 (DungeonNodeID.SWBossRoom, KeyDoorID.SWBackSecondKeyDoor)
//                             });
//                         break;
//                     case DungeonNodeID.SWBackSecondKeyDoor:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.SWBackPastCurtains, RequirementType.NoRequirement),
//                                 (DungeonNodeID.SWBossRoom, RequirementType.NoRequirement)
//                             });
//                         break;
//                     case DungeonNodeID.SWBossRoom:
//                         ExpectedKeyDoorValues.Add(id,
//                             new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
//                             {
//                                 (DungeonNodeID.SWBackPastCurtains, KeyDoorID.SWBackSecondKeyDoor)
//                             });
//                         break;
//                     case DungeonNodeID.SWBoss:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.SWBossRoom, RequirementType.SWBoss)
//                             });
//                         break;
//                 }
//             }
//         }
//
//         [Fact]
//         public void PopulateNodeConnections_ShouldThrowException_WhenNodeIDIsUnexpected()
//         {
//             var dungeonData = Substitute.For<IMutableDungeon>();
//             var node = Substitute.For<IDungeonNode>();
//             var connections = new List<INodeConnection>();
//             const DungeonNodeID id = (DungeonNodeID)int.MaxValue;
//
//             Assert.Throws<ArgumentOutOfRangeException>(() =>
//                 _sut.PopulateNodeConnections(dungeonData, id, node, connections));
//         }
//         
//         [Theory]
//         [MemberData(nameof(PopulateNodeConnections_ShouldCreateExpectedEntryConnectionsData))]
//         public void PopulateNodeConnections_ShouldCreateExpectedEntryConnections(
//             DungeonNodeID id, OverworldNodeID fromNodeID)
//         {
//             var dungeonData = Substitute.For<IMutableDungeon>();
//             var node = Substitute.For<IDungeonNode>();
//             var connections = new List<INodeConnection>();
//             _sut.PopulateNodeConnections(dungeonData, id, node, connections);
//
//             Assert.Contains(_entryFactoryCalls, x => x == _overworldNodes[fromNodeID]);
//         }
//
//         public static IEnumerable<object[]> PopulateNodeConnections_ShouldCreateExpectedEntryConnectionsData()
//         {
//             PopulateExpectedValues();
//
//             return ExpectedEntryValues.Keys.Select(id => new object[] {id, ExpectedEntryValues[id]}).ToList();
//         }
//
//         [Theory]
//         [MemberData(nameof(PopulateNodeConnections_ShouldCreateExpectedConnectionsData))]
//         public void PopulateNodeConnections_ShouldCreateExpectedConnections(
//             DungeonNodeID id, DungeonNodeID fromNodeID, RequirementType requirementType)
//         {
//             var dungeonData = Substitute.For<IMutableDungeon>();
//             var node = Substitute.For<IDungeonNode>();
//             var connections = new List<INodeConnection>();
//             _sut.PopulateNodeConnections(dungeonData, id, node, connections);
//
//             Assert.Contains(_connectionFactoryCalls, x =>
//                 x.fromNode == dungeonData.Nodes[fromNodeID] && x.requirement == _requirements[requirementType]);
//         }
//
//         public static IEnumerable<object[]> PopulateNodeConnections_ShouldCreateExpectedConnectionsData()
//         {
//             PopulateExpectedValues();
//
//             return (from id in ExpectedConnectionValue.Keys from value in ExpectedConnectionValue[id]
//                 select new object[] {id, value.fromNodeID, value.requirementType}).ToList();
//         }
//
//         [Theory]
//         [MemberData(nameof(PopulateNodeConnections_ShouldCreateExpectedKeyDoorConnectionsData))]
//         public void PopulateNodeConnections_ShouldCreateExpectedKeyDoorConnections(
//             DungeonNodeID id, DungeonNodeID fromNodeID, KeyDoorID keyDoor)
//         {
//             var dungeonData = Substitute.For<IMutableDungeon>();
//             var node = Substitute.For<IDungeonNode>();
//             var connections = new List<INodeConnection>();
//             _sut.PopulateNodeConnections(dungeonData, id, node, connections);
//
//             Assert.Contains(_connectionFactoryCalls, x =>
//                 x.fromNode == dungeonData.Nodes[fromNodeID] &&
//                 x.requirement == dungeonData.KeyDoors[keyDoor].Requirement);
//         }
//         
//         public static IEnumerable<object[]> PopulateNodeConnections_ShouldCreateExpectedKeyDoorConnectionsData()
//         {
//             PopulateExpectedValues();
//
//             return (from id in ExpectedKeyDoorValues.Keys from value in ExpectedKeyDoorValues[id]
//                 select new object[] {id, value.fromNodeID, value.keyDoor}).ToList();
//         }
//
//         [Fact]
//         public void AutofacTest()
//         {
//             using var scope = ContainerConfig.Configure().BeginLifetimeScope();
//             var sut = scope.Resolve<ISWDungeonNodeFactory>();
//             
//             Assert.NotNull(sut as SWDungeonNodeFactory);
//         }
//     }
// }