using System;
using System.Collections.Generic;
using System.ComponentModel;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Modes;
using OpenTracker.Models.NodeConnections;
using OpenTracker.Models.Nodes;
using Xunit;

namespace OpenTracker.UnitTests.Models.RequirementNodes
{
    public class RequirementNodeTests
    {
        private readonly IMode _mode = Substitute.For<IMode>();
        private readonly List<INodeConnection> _nodeConnections = new()
        {
            Substitute.For<INodeConnection>(),
            Substitute.For<INodeConnection>()
        };
        private readonly IOverworldNodeDictionary _requirementNodes = Substitute.For<IOverworldNodeDictionary>();
        private readonly IOverworldNodeFactory _factory = Substitute.For<IOverworldNodeFactory>();

        private readonly OverworldNode _sut;

        public RequirementNodeTests()
        {
            _sut = new OverworldNode(_mode, _requirementNodes, _factory);
            _factory.GetNodeConnections(
                Arg.Any<OverworldNodeID>(), Arg.Any<INode>()).Returns(_nodeConnections);
            _requirementNodes.ItemCreated +=
                Raise.Event<EventHandler<KeyValuePair<OverworldNodeID, INode>>>(
                    _requirementNodes, new KeyValuePair<OverworldNodeID, INode>(
                        OverworldNodeID.LightWorld, _sut));
        }

        [Theory]
        [InlineData(AccessibilityLevel.None, EntranceShuffle.None, 0)]
        [InlineData(AccessibilityLevel.None, EntranceShuffle.None, 1)]
        [InlineData(AccessibilityLevel.None, EntranceShuffle.None, 2)]
        [InlineData(AccessibilityLevel.None, EntranceShuffle.Dungeon, 0)]
        [InlineData(AccessibilityLevel.Normal, EntranceShuffle.Dungeon, 1)]
        [InlineData(AccessibilityLevel.Normal, EntranceShuffle.Dungeon, 2)]
        [InlineData(AccessibilityLevel.None, EntranceShuffle.All, 0)]
        [InlineData(AccessibilityLevel.Normal, EntranceShuffle.All, 1)]
        [InlineData(AccessibilityLevel.Normal, EntranceShuffle.All, 2)]
        [InlineData(AccessibilityLevel.None, EntranceShuffle.Insanity, 0)]
        [InlineData(AccessibilityLevel.Normal, EntranceShuffle.Insanity, 1)]
        [InlineData(AccessibilityLevel.Normal, EntranceShuffle.Insanity, 2)]
        public void DungeonExitsAccessible_AccessibilityShouldReturnNormal_WhenExitsAccessible(
            AccessibilityLevel expected, EntranceShuffle entranceShuffle, int dungeonExitsAccessible)
        {
            _mode.EntranceShuffle.Returns(entranceShuffle);
            _sut.DungeonExitsAccessible = dungeonExitsAccessible;
            
            Assert.Equal(expected, _sut.Accessibility);
        }

        [Theory]
        [InlineData(AccessibilityLevel.None, EntranceShuffle.None, 0)]
        [InlineData(AccessibilityLevel.None, EntranceShuffle.None, 1)]
        [InlineData(AccessibilityLevel.None, EntranceShuffle.None, 2)]
        [InlineData(AccessibilityLevel.None, EntranceShuffle.Dungeon, 0)]
        [InlineData(AccessibilityLevel.None, EntranceShuffle.Dungeon, 1)]
        [InlineData(AccessibilityLevel.None, EntranceShuffle.Dungeon, 2)]
        [InlineData(AccessibilityLevel.None, EntranceShuffle.All, 0)]
        [InlineData(AccessibilityLevel.Normal, EntranceShuffle.All, 1)]
        [InlineData(AccessibilityLevel.Normal, EntranceShuffle.All, 2)]
        [InlineData(AccessibilityLevel.None, EntranceShuffle.Insanity, 0)]
        [InlineData(AccessibilityLevel.Normal, EntranceShuffle.Insanity, 1)]
        [InlineData(AccessibilityLevel.Normal, EntranceShuffle.Insanity, 2)]
        public void ExitsAccessible_AccessibilityShouldReturnNormal_WhenExitsAccessible(
            AccessibilityLevel expected, EntranceShuffle entranceShuffle, int exitsAccessible)
        {
            _mode.EntranceShuffle.Returns(entranceShuffle);
            _sut.ExitsAccessible = exitsAccessible;
            
            Assert.Equal(expected, _sut.Accessibility);
        }

        [Theory]
        [InlineData(AccessibilityLevel.None, EntranceShuffle.None, 0)]
        [InlineData(AccessibilityLevel.None, EntranceShuffle.None, 1)]
        [InlineData(AccessibilityLevel.None, EntranceShuffle.None, 2)]
        [InlineData(AccessibilityLevel.None, EntranceShuffle.Dungeon, 0)]
        [InlineData(AccessibilityLevel.None, EntranceShuffle.Dungeon, 1)]
        [InlineData(AccessibilityLevel.None, EntranceShuffle.Dungeon, 2)]
        [InlineData(AccessibilityLevel.None, EntranceShuffle.All, 0)]
        [InlineData(AccessibilityLevel.None, EntranceShuffle.All, 1)]
        [InlineData(AccessibilityLevel.None, EntranceShuffle.All, 2)]
        [InlineData(AccessibilityLevel.None, EntranceShuffle.Insanity, 0)]
        [InlineData(AccessibilityLevel.Normal, EntranceShuffle.Insanity, 1)]
        [InlineData(AccessibilityLevel.Normal, EntranceShuffle.Insanity, 2)]
        public void InsanityExitsAccessible_AccessibilityShouldReturnNormal_WhenExitsAccessible(
            AccessibilityLevel expected, EntranceShuffle entranceShuffle, int insanityExitsAccessible)
        {
            _mode.EntranceShuffle.Returns(entranceShuffle);
            _sut.InsanityExitsAccessible = insanityExitsAccessible;
            
            Assert.Equal(expected, _sut.Accessibility);
        }

        [Theory]
        [InlineData(AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.None)]
        [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.Inspect, AccessibilityLevel.None)]
        [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.None, AccessibilityLevel.Inspect)]
        [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Normal, AccessibilityLevel.SequenceBreak)]
         public void Accessibility_ShouldReturnGreatestConnectionAccessibility(
            AccessibilityLevel expected, AccessibilityLevel connection1Accessibility,
            AccessibilityLevel connection2Accessibility)
        {
            _nodeConnections[0].Accessibility.Returns(connection1Accessibility);
            _nodeConnections[0].GetConnectionAccessibility(Arg.Any<List<INode>>())
                .Returns(connection1Accessibility);

            _nodeConnections[1].Accessibility.Returns(connection2Accessibility);
            _nodeConnections[1].GetConnectionAccessibility(Arg.Any<List<INode>>())
                .Returns(connection2Accessibility);

            _nodeConnections[0].PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _nodeConnections[0],
                new PropertyChangedEventArgs(nameof(INodeConnection.Accessibility)));
            
            Assert.Equal(expected, _sut.Accessibility);
        }

         [Fact]
         public void Accessibility_ShouldRaisePropertyChanged_WhenEntranceShuffleIsChanged()
         {
             _sut.ExitsAccessible = 1;
             
             Assert.PropertyChanged(_sut, nameof(IOverworldNode.Accessibility),
                 () =>
                 {
                     _mode.EntranceShuffle.Returns(EntranceShuffle.All);
                     _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                         _mode, new PropertyChangedEventArgs(nameof(IMode.EntranceShuffle)));
                 });
         }

         [Fact]
         public void Accessibility_ShouldRaisePropertyChanged_WhenConnection1IsChanged()
         {
             _nodeConnections[0].Accessibility.Returns(AccessibilityLevel.Normal);
             _nodeConnections[0].GetConnectionAccessibility(Arg.Any<List<INode>>())
                 .Returns(AccessibilityLevel.Normal);
             
             Assert.PropertyChanged(_sut, nameof(IOverworldNode.Accessibility),
                 () => _nodeConnections[0].PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                     _nodeConnections[0],
                     new PropertyChangedEventArgs(nameof(INodeConnection.Accessibility))));
         }

         [Fact]
         public void Accessibility_ShouldRaisePropertyChanged_WhenConnection2IsChanged()
         {
             _nodeConnections[1].Accessibility.Returns(AccessibilityLevel.Normal);
             _nodeConnections[1].GetConnectionAccessibility(Arg.Any<List<INode>>())
                 .Returns(AccessibilityLevel.Normal);
             
             Assert.PropertyChanged(_sut, nameof(IOverworldNode.Accessibility),
                 () => _nodeConnections[1].PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                     _nodeConnections[1],
                     new PropertyChangedEventArgs(nameof(INodeConnection.Accessibility))));
         }
    }
}