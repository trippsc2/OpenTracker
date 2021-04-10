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
//     public class TRDungeonNodeFactoryTests
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
//         private readonly TRDungeonNodeFactory _sut;
//
//         private static readonly Dictionary<DungeonNodeID, OverworldNodeID> ExpectedEntryValues = new();
//         private static readonly Dictionary<DungeonNodeID, List<
//             (DungeonNodeID fromNodeID, RequirementType requirementType)>> ExpectedConnectionValue = new();
//         private static readonly Dictionary<DungeonNodeID, List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>>
//             ExpectedKeyDoorValues = new();
//
//         public TRDungeonNodeFactoryTests()
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
//             _sut = new TRDungeonNodeFactory(_overworldNodes, EntryFactory, ConnectionFactory);
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
//                     case DungeonNodeID.TRFront:
//                         ExpectedEntryValues.Add(id, OverworldNodeID.TRFrontEntry);
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.TRF1SomariaTrack, RequirementType.NoRequirement)
//                             });
//                         break;
//                     case DungeonNodeID.TRF1SomariaTrack:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.TRFront, RequirementType.CaneOfSomaria),
//                                 (DungeonNodeID.TRF1CompassChestArea, RequirementType.CaneOfSomaria),
//                                 (DungeonNodeID.TRF1FourTorchRoom, RequirementType.CaneOfSomaria),
//                                 (DungeonNodeID.TRF1FirstKeyDoorArea, RequirementType.CaneOfSomaria)
//                             });
//                         break;
//                     case DungeonNodeID.TRF1CompassChestArea:
//                     case DungeonNodeID.TRF1FourTorchRoom:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.TRF1SomariaTrack, RequirementType.NoRequirement)
//                             });
//                         break;
//                     case DungeonNodeID.TRF1RollerRoom:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.TRF1FourTorchRoom, RequirementType.FireRod)
//                             });
//                         break;
//                     case DungeonNodeID.TRF1FirstKeyDoorArea:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.TRF1SomariaTrack, RequirementType.NoRequirement)
//                             });
//                         ExpectedKeyDoorValues.Add(id,
//                             new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
//                             {
//                                 (DungeonNodeID.TRF1PastFirstKeyDoor, KeyDoorID.TR1FFirstKeyDoor)
//                             });
//                         break;
//                     case DungeonNodeID.TRF1FirstKeyDoor:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.TRF1FirstKeyDoorArea, RequirementType.NoRequirement),
//                                 (DungeonNodeID.TRF1PastFirstKeyDoor, RequirementType.NoRequirement)
//                             });
//                         break;
//                     case DungeonNodeID.TRF1PastFirstKeyDoor:
//                         ExpectedKeyDoorValues.Add(id,
//                             new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
//                             {
//                                 (DungeonNodeID.TRF1FirstKeyDoorArea, KeyDoorID.TR1FFirstKeyDoor),
//                                 (DungeonNodeID.TRF1PastSecondKeyDoor, KeyDoorID.TR1FSecondKeyDoor)
//                             });
//                         break;
//                     case DungeonNodeID.TRF1SecondKeyDoor:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.TRF1PastFirstKeyDoor, RequirementType.NoRequirement),
//                                 (DungeonNodeID.TRF1PastSecondKeyDoor, RequirementType.NoRequirement)
//                             });
//                         break;
//                     case DungeonNodeID.TRF1PastSecondKeyDoor:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.TRB1, RequirementType.NoRequirement)
//                             });
//                         ExpectedKeyDoorValues.Add(id,
//                             new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
//                             {
//                                 (DungeonNodeID.TRF1PastFirstKeyDoor, KeyDoorID.TR1FSecondKeyDoor)
//                             });
//                         break;
//                     case DungeonNodeID.TRB1:
//                         ExpectedEntryValues.Add(id, OverworldNodeID.TRMiddleEntry);
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.TRB1BigChestArea, RequirementType.NoRequirement)
//                             });
//                         ExpectedKeyDoorValues.Add(id,
//                             new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
//                             {
//                                 (DungeonNodeID.TRF1PastSecondKeyDoor, KeyDoorID.TR1FThirdKeyDoor),
//                                 (DungeonNodeID.TRB1RightSide, KeyDoorID.TRB1BigKeyDoor),
//                                 (DungeonNodeID.TRB1PastBigKeyChestKeyDoor, KeyDoorID.TRB1BigKeyChestKeyDoor)
//                             });
//                         break;
//                     case DungeonNodeID.TRB1BigKeyChestKeyDoor:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.TRB1, RequirementType.NoRequirement),
//                                 (DungeonNodeID.TRB1PastBigKeyChestKeyDoor, RequirementType.NoRequirement)
//                             });
//                         break;
//                     case DungeonNodeID.TRB1PastBigKeyChestKeyDoor:
//                         ExpectedKeyDoorValues.Add(id,
//                             new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
//                             {
//                                 (DungeonNodeID.TRB1, KeyDoorID.TRB1BigKeyChestKeyDoor)
//                             });
//                         break;
//                     case DungeonNodeID.TRB1MiddleRightEntranceArea:
//                         ExpectedEntryValues.Add(id, OverworldNodeID.TRMiddleEntry);
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.TRB1, RequirementType.NoRequirement)
//                             });
//                         break;
//                     case DungeonNodeID.TRB1BigChestArea:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.TRB1MiddleRightEntranceArea, RequirementType.Hookshot),
//                                 (DungeonNodeID.TRB1MiddleRightEntranceArea, RequirementType.CaneOfSomaria),
//                                 (DungeonNodeID.TRB1MiddleRightEntranceArea, RequirementType.Hover)
//                             });
//                         break;
//                     case DungeonNodeID.TRBigChest:
//                         ExpectedKeyDoorValues.Add(id,
//                             new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
//                             {
//                                 (DungeonNodeID.TRB1BigChestArea, KeyDoorID.TRBigChest)
//                             });
//                         break;
//                     case DungeonNodeID.TRB1BigKeyDoor:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.TRB1, RequirementType.NoRequirement),
//                                 (DungeonNodeID.TRB1RightSide, RequirementType.NoRequirement)
//                             });
//                         break;
//                     case DungeonNodeID.TRB1RightSide:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.TRPastB1ToB2KeyDoor, RequirementType.NoRequirement)
//                             });
//                         ExpectedKeyDoorValues.Add(id,
//                             new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
//                             {
//                                 (DungeonNodeID.TRB1, KeyDoorID.TRB1BigKeyDoor)
//                             });
//                         break;
//                     case DungeonNodeID.TRPastB1ToB2KeyDoor:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.TRB2DarkRoomTop, RequirementType.NoRequirement)
//                             });
//                         ExpectedKeyDoorValues.Add(id,
//                             new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
//                             {
//                                 (DungeonNodeID.TRB1RightSide, KeyDoorID.TRB1ToB2KeyDoor)
//                             });
//                         break;
//                     case DungeonNodeID.TRB2DarkRoomTop:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.TRPastB1ToB2KeyDoor, RequirementType.DarkRoomTR),
//                                 (DungeonNodeID.TRB2DarkRoomBottom, RequirementType.CaneOfSomaria)
//                             });
//                         break;
//                     case DungeonNodeID.TRB2DarkRoomBottom:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.TRB2DarkRoomTop, RequirementType.CaneOfSomaria),
//                                 (DungeonNodeID.TRB2PastDarkMaze, RequirementType.DarkRoomTR)
//                             });
//                         break;
//                     case DungeonNodeID.TRB2PastDarkMaze:
//                         ExpectedEntryValues.Add(id, OverworldNodeID.TRBackEntry);
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.TRB2DarkRoomBottom, RequirementType.NoRequirement)
//                             });
//                         ExpectedKeyDoorValues.Add(id,
//                             new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
//                             {
//                                 (DungeonNodeID.TRB2PastKeyDoor, KeyDoorID.TRB2KeyDoor)
//                             });
//                         break;
//                     case DungeonNodeID.TRLaserBridgeChests:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.TRB2PastDarkMaze, RequirementType.LaserBridge)
//                             });
//                         break;
//                     case DungeonNodeID.TRB2KeyDoor:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.TRB2PastDarkMaze, RequirementType.NoRequirement),
//                                 (DungeonNodeID.TRB2PastKeyDoor, RequirementType.NoRequirement)
//                             });
//                         break;
//                     case DungeonNodeID.TRB2PastKeyDoor:
//                         ExpectedKeyDoorValues.Add(id,
//                             new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
//                             {
//                                 (DungeonNodeID.TRB2PastDarkMaze, KeyDoorID.TRB2KeyDoor)
//                             });
//                         break;
//                     case DungeonNodeID.TRB3:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.TRB2PastKeyDoor, RequirementType.NoRequirement)
//                             });
//                         break;
//                     case DungeonNodeID.TRB3BossRoomEntry:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.TRB3, RequirementType.CaneOfSomaria)
//                             });
//                         break;
//                     case DungeonNodeID.TRBossRoom:
//                         ExpectedKeyDoorValues.Add(id,
//                             new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
//                             {
//                                 (DungeonNodeID.TRB3BossRoomEntry, KeyDoorID.TRBossRoomBigKeyDoor)
//                             });
//                         break;
//                     case DungeonNodeID.TRBoss:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.TRBossRoom, RequirementType.TRBoss)
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
//             var sut = scope.Resolve<ITRDungeonNodeFactory>();
//             
//             Assert.NotNull(sut as TRDungeonNodeFactory);
//         }
//     }
// }