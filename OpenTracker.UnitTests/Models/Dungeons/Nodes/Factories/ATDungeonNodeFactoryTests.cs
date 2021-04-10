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
using OpenTracker.Models.NodeConnections;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Boss;
using OpenTracker.Models.Requirements.Complex;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.Nodes.Factories
{
    public class ATDungeonNodeFactoryTests
    {
        // private readonly IRequirementFactory _requirementFactory = Substitute.For<IRequirementFactory>();
        // private readonly IOverworldNodeFactory _overworldNodeFactory = Substitute.For<IOverworldNodeFactory>();
        //
        // private readonly IOverworldNodeDictionary _overworldNodes;
        //
        // private readonly List<INode> _entryFactoryCalls = new();
        // private readonly List<(INode fromNode, INode toNode, IRequirement? requirement)> _connectionFactoryCalls = new();
        //
        // private readonly IBossRequirementDictionary _bossRequirements;
        // private readonly IComplexRequirementDictionary _complexRequirements;
        //
        // private readonly ATDungeonNodeFactory _sut;
        //
        // private static readonly Dictionary<DungeonNodeID, OverworldNodeID> ExpectedEntryValues = new();
        // private static readonly Dictionary<DungeonNodeID, List<
        //     (DungeonNodeID fromNodeID, RequirementType? requirementType)>> ExpectedConnectionValue = new();
        // private static readonly Dictionary<DungeonNodeID, List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>>
        //     ExpectedKeyDoorValues = new();
        //
        // public ATDungeonNodeFactoryTests()
        // {
        //     _bossRequirements = new BossRequirementDictionary(
        //         Substitute.For<IBossPlacementDictionary>(),
        //         _ => Substitute.For<IBossRequirement>());
        //     
        //     var factory = Substitute.For<IComplexRequirementFactory>();
        //     factory.GetComplexRequirement(Arg.Any<ComplexRequirementType>()).Returns(
        //         _ => Substitute.For<IRequirement>());
        //
        //     _complexRequirements = new ComplexRequirementDictionary(() => factory);
        //     
        //     _overworldNodes = new OverworldNodeDictionary(() => _overworldNodeFactory);
        //
        //     IEntryNodeConnection EntryFactory(INode fromNode)
        //     {
        //         _entryFactoryCalls.Add(fromNode);
        //         return Substitute.For<IEntryNodeConnection>();
        //     }
        //
        //     INodeConnection ConnectionFactory(INode fromNode, INode toNode, IRequirement? requirement)
        //     {
        //         _connectionFactoryCalls.Add((fromNode, toNode, requirement));
        //         return Substitute.For<INodeConnection>();
        //     }
        //
        //     _sut = new ATDungeonNodeFactory(
        //         _bossRequirements, _complexRequirements, _overworldNodes, EntryFactory, ConnectionFactory);
        // }
        //
        // private static void PopulateExpectedValues()
        // {
        //     ExpectedEntryValues.Clear();
        //     ExpectedConnectionValue.Clear();
        //     ExpectedKeyDoorValues.Clear();
        //     
        //     foreach (DungeonNodeID id in Enum.GetValues(typeof(DungeonNodeID)))
        //     {
        //         switch (id)
        //         {
        //             case DungeonNodeID.AT:
        //                 ExpectedEntryValues.Add(id, OverworldNodeID.ATEntry);
        //                 break;
        //             case DungeonNodeID.ATDarkMaze:
        //                 ExpectedConnectionValue.Add(id,
        //                     new List<(DungeonNodeID fromNodeID, RequirementType? requirementType)>
        //                     {
        //                         (DungeonNodeID.AT, RequirementType.DarkRoomAT)
        //                     });
        //                 break;
        //             case DungeonNodeID.ATPastFirstKeyDoor:
        //                 ExpectedKeyDoorValues.Add(id,
        //                     new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
        //                     {
        //                         (DungeonNodeID.ATDarkMaze, KeyDoorID.ATFirstKeyDoor),
        //                         (DungeonNodeID.ATPastSecondKeyDoor, KeyDoorID.ATSecondKeyDoor)
        //                     });
        //                 break;
        //             case DungeonNodeID.ATSecondKeyDoor:
        //                 ExpectedConnectionValue.Add(id,
        //                     new List<(DungeonNodeID fromNodeID, RequirementType? requirementType)>
        //                     {
        //                         (DungeonNodeID.ATPastFirstKeyDoor, null),
        //                         (DungeonNodeID.ATPastSecondKeyDoor, null)
        //                     });
        //                 break;
        //             case DungeonNodeID.ATPastSecondKeyDoor:
        //                 ExpectedKeyDoorValues.Add(id,
        //                     new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
        //                     {
        //                         (DungeonNodeID.ATPastFirstKeyDoor, KeyDoorID.ATSecondKeyDoor)
        //                     });
        //                 break;
        //             case DungeonNodeID.ATPastThirdKeyDoor:
        //                 ExpectedKeyDoorValues.Add(id,
        //                     new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
        //                     {
        //                         (DungeonNodeID.ATPastSecondKeyDoor, KeyDoorID.ATThirdKeyDoor),
        //                         (DungeonNodeID.ATPastFourthKeyDoor, KeyDoorID.ATFourthKeyDoor)
        //                     });
        //                 break;
        //             case DungeonNodeID.ATFourthKeyDoor:
        //                 ExpectedConnectionValue.Add(id,
        //                     new List<(DungeonNodeID fromNodeID, RequirementType? requirementType)>
        //                     {
        //                         (DungeonNodeID.ATPastThirdKeyDoor, null),
        //                         (DungeonNodeID.ATPastFourthKeyDoor, null)
        //                     });
        //                 break;
        //             case DungeonNodeID.ATPastFourthKeyDoor:
        //                 ExpectedKeyDoorValues.Add(id,
        //                     new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
        //                     {
        //                         (DungeonNodeID.ATPastThirdKeyDoor, KeyDoorID.ATFourthKeyDoor)
        //                     });
        //                 break;
        //             case DungeonNodeID.ATBossRoom:
        //                 ExpectedConnectionValue.Add(id,
        //                     new List<(DungeonNodeID fromNodeID, RequirementType? requirementType)>
        //                     {
        //                         (DungeonNodeID.ATPastFourthKeyDoor, RequirementType.Curtains)
        //                     });
        //                 break;
        //             case DungeonNodeID.ATBoss:
        //                 ExpectedConnectionValue.Add(id,
        //                     new List<(DungeonNodeID fromNodeID, RequirementType? requirementType)>
        //                     {
        //                         (DungeonNodeID.ATBossRoom, RequirementType.ATBoss)
        //                     });
        //                 break;
        //         }
        //     }
        // }
        //
        // [Fact]
        // public void PopulateNodeConnections_ShouldThrowException_WhenNodeIDIsUnexpected()
        // {
        //     var dungeonData = Substitute.For<IMutableDungeon>();
        //     var node = Substitute.For<IDungeonNode>();
        //     var connections = new List<INodeConnection>();
        //     const DungeonNodeID id = (DungeonNodeID)int.MaxValue;
        //
        //     Assert.Throws<ArgumentOutOfRangeException>(() =>
        //         _sut.PopulateNodeConnections(dungeonData, id, node, connections));
        // }
        //
        // [Theory]
        // [MemberData(nameof(PopulateNodeConnections_ShouldCreateExpectedEntryConnectionsData))]
        // public void PopulateNodeConnections_ShouldCreateExpectedEntryConnections(
        //     DungeonNodeID id, OverworldNodeID fromNodeID)
        // {
        //     var dungeonData = Substitute.For<IMutableDungeon>();
        //     var node = Substitute.For<IDungeonNode>();
        //     var connections = new List<INodeConnection>();
        //     _sut.PopulateNodeConnections(dungeonData, id, node, connections);
        //
        //     Assert.Contains(_entryFactoryCalls, x => x == _overworldNodes[fromNodeID]);
        // }
        //
        // public static IEnumerable<object[]> PopulateNodeConnections_ShouldCreateExpectedEntryConnectionsData()
        // {
        //     PopulateExpectedValues();
        //
        //     return ExpectedEntryValues.Keys.Select(id => new object[] {id, ExpectedEntryValues[id]}).ToList();
        // }
        //
        // [Theory]
        // [MemberData(nameof(PopulateNodeConnections_ShouldCreateExpectedConnectionsData))]
        // public void PopulateNodeConnections_ShouldCreateExpectedConnections(
        //     DungeonNodeID id, DungeonNodeID fromNodeID, RequirementType? requirementType)
        // {
        //     var dungeonData = Substitute.For<IMutableDungeon>();
        //     var node = Substitute.For<IDungeonNode>();
        //     var connections = new List<INodeConnection>();
        //     _sut.PopulateNodeConnections(dungeonData, id, node, connections);
        //
        //     Assert.Contains(_connectionFactoryCalls, x =>
        //         x.fromNode == dungeonData.Nodes[fromNodeID] &&
        //         x.requirement is null ? requirementType is null :
        //         x.requirement == _requirements[requirementType!.Value]);
        // }
        //
        // public static IEnumerable<object[]> PopulateNodeConnections_ShouldCreateExpectedConnectionsData()
        // {
        //     PopulateExpectedValues();
        //
        //     return (from id in ExpectedConnectionValue.Keys from value in ExpectedConnectionValue[id]
        //         select new object[] {id, value.fromNodeID, value.requirementType}).ToList();
        // }
        //
        // [Theory]
        // [MemberData(nameof(PopulateNodeConnections_ShouldCreateExpectedKeyDoorConnectionsData))]
        // public void PopulateNodeConnections_ShouldCreateExpectedKeyDoorConnections(
        //     DungeonNodeID id, DungeonNodeID fromNodeID, KeyDoorID keyDoor)
        // {
        //     var dungeonData = Substitute.For<IMutableDungeon>();
        //     var node = Substitute.For<IDungeonNode>();
        //     var connections = new List<INodeConnection>();
        //     _sut.PopulateNodeConnections(dungeonData, id, node, connections);
        //
        //     Assert.Contains(_connectionFactoryCalls, x =>
        //         x.fromNode == dungeonData.Nodes[fromNodeID] &&
        //         x.requirement == dungeonData.KeyDoors[keyDoor].Requirement);
        // }
        //
        // public static IEnumerable<object[]> PopulateNodeConnections_ShouldCreateExpectedKeyDoorConnectionsData()
        // {
        //     PopulateExpectedValues();
        //
        //     return (from id in ExpectedKeyDoorValues.Keys from value in ExpectedKeyDoorValues[id]
        //         select new object[] {id, value.fromNodeID, value.keyDoor}).ToList();
        // }
        
        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var sut = scope.Resolve<IATDungeonNodeFactory>();
            
            Assert.NotNull(sut as ATDungeonNodeFactory);
        }
    }
}