using System;
using System.Collections.Generic;
using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Dungeons.Nodes;
using OpenTracker.Models.Dungeons.Nodes.Factories;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Nodes.Connections;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.Nodes
{
    public class DungeonNodeTests
    {
        private readonly IDungeonNodeFactory _factory = Substitute.For<IDungeonNodeFactory>();
        private readonly IMutableDungeon _dungeonData = Substitute.For<IMutableDungeon>();

        private readonly List<INodeConnection> _connections = new()
        {
            Substitute.For<INodeConnection>(),
            Substitute.For<INodeConnection>()
        };

        private readonly DungeonNode _sut;

        public DungeonNodeTests()
        {
            _sut = new DungeonNode(_factory, _dungeonData);
            _factory.When(x => x.PopulateNodeConnections(
                Arg.Any<IMutableDungeon>(), Arg.Any<DungeonNodeID>(), _sut,
                Arg.Any<IList<INodeConnection>>())).Do(x =>
                {
                    var connections = (IList<INodeConnection>)x[3];
                    foreach (var connection in _connections)
                    {
                        connections.Add(connection);
                    }
                });
            _dungeonData.Nodes.ItemCreated += Raise.Event<EventHandler<KeyValuePair<DungeonNodeID, IDungeonNode>>>(
                _dungeonData.Nodes, new KeyValuePair<DungeonNodeID, IDungeonNode>(
                    DungeonNodeID.AT, _sut));
        }

        [Fact]
        public void ConnectionChanged_ShouldCallGetConnectionAccessibility_WhenConnection1Changed()
        {
            _connections[0].PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _connections[0], new PropertyChangedEventArgs(nameof(INodeConnection.Accessibility)));

            _connections[0].Received().GetConnectionAccessibility(Arg.Any<IList<INode>>());
        }

        [Fact]
        public void ConnectionChanged_ShouldCallGetConnectionAccessibility_WhenConnection2Changed()
        {
            _connections[1].PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _connections[0], new PropertyChangedEventArgs(nameof(INodeConnection.Accessibility)));

            _connections[0].Received().GetConnectionAccessibility(Arg.Any<IList<INode>>());
        }

        [Theory]
        [InlineData(AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.None)]
        [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.None, AccessibilityLevel.Inspect)]
        [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.None, AccessibilityLevel.SequenceBreak)]
        [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.None, AccessibilityLevel.Normal)]
        [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.Inspect, AccessibilityLevel.None)]
        [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.Inspect, AccessibilityLevel.Inspect)]
        [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.Inspect, AccessibilityLevel.SequenceBreak)]
        [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Inspect, AccessibilityLevel.Normal)]
        [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak, AccessibilityLevel.None)]
        [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak, AccessibilityLevel.Inspect)]
        [InlineData(
            AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak)]
        [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.SequenceBreak, AccessibilityLevel.Normal)]
        [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Normal, AccessibilityLevel.None)]
        [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Normal, AccessibilityLevel.Inspect)]
        [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Normal, AccessibilityLevel.SequenceBreak)]
        [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Normal, AccessibilityLevel.Normal)]
        public void ConnectionChanged_ShouldUpdateAccessibilityToExpected(
            AccessibilityLevel expected, AccessibilityLevel connection1Accessibility,
            AccessibilityLevel connection2Accessibility)
        {
            _connections[0].Accessibility.Returns(connection1Accessibility);
            _connections[0].GetConnectionAccessibility(Arg.Any<IList<INode>>())
                .Returns(connection1Accessibility);
            _connections[1].Accessibility.Returns(connection2Accessibility);
            _connections[1].GetConnectionAccessibility(Arg.Any<IList<INode>>())
                .Returns(connection2Accessibility);

            _connections[0].PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _connections[0], new PropertyChangedEventArgs(nameof(INodeConnection.Accessibility)));
            
            Assert.Equal(expected, _sut.Accessibility);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IDungeonNode.Factory>();
            var sut = factory(_dungeonData);
            
            Assert.NotNull(sut as DungeonNode);
        }
    }
}