using System.Collections.Generic;
using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Nodes.Connections;
using OpenTracker.Models.Requirements;
using Xunit;

namespace OpenTracker.UnitTests.Models.Nodes.Connections;

public class NodeConnectionTests
{
    private readonly INode _fromNode = Substitute.For<INode>();
    private readonly INode _toNode = Substitute.For<INode>();

    private readonly IRequirement _requirement = Substitute.For<IRequirement>();

    [Fact]
    public void Accessibility_ShouldRaisePropertyChanged()
    {
        const AccessibilityLevel accessibility = AccessibilityLevel.Normal;

        var sut = new NodeConnection(_fromNode, _toNode);

        _fromNode.Accessibility.Returns(accessibility);
        _fromNode.GetNodeAccessibility(Arg.Any<IList<INode>>()).Returns(accessibility);
            
        Assert.PropertyChanged(sut, nameof(INodeConnection.Accessibility), () =>
            _fromNode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _fromNode, new PropertyChangedEventArgs(nameof(INode.Accessibility))));
    }

    [Fact]
    public void GetConnectionAccessibility_ShouldCallGetNodeAccessibility()
    {
        var sut = new NodeConnection(_fromNode, _toNode);
            
        _fromNode.Accessibility.Returns(AccessibilityLevel.Normal);
        _ = sut.GetConnectionAccessibility(new List<INode>());

        _fromNode.Received().GetNodeAccessibility(Arg.Any<IList<INode>>());
    }

    [Theory]
    [InlineData(AccessibilityLevel.None, AccessibilityLevel.None)]
    [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.Inspect)]
    [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak)]
    [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Normal)]
    public void GetConnectionAccessibility_ShouldReturnExpected_WhenRequirementIsNull(
        AccessibilityLevel expected, AccessibilityLevel nodeAccessibility)
    {
        var sut = new NodeConnection(_fromNode, _toNode);

        _fromNode.Accessibility.Returns(nodeAccessibility);
        _fromNode.GetNodeAccessibility(Arg.Any<IList<INode>>()).Returns(nodeAccessibility);
            
        Assert.Equal(expected, sut.GetConnectionAccessibility(new List<INode>()));
    }

    [Theory]
    [InlineData(AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.None)]
    [InlineData(AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.Inspect)]
    [InlineData(AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.SequenceBreak)]
    [InlineData(AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.Normal)]
    [InlineData(AccessibilityLevel.None, AccessibilityLevel.Inspect, AccessibilityLevel.None)]
    [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.Inspect, AccessibilityLevel.Inspect)]
    [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.Inspect, AccessibilityLevel.SequenceBreak)]
    [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.Inspect, AccessibilityLevel.Normal)]
    [InlineData(AccessibilityLevel.None, AccessibilityLevel.SequenceBreak, AccessibilityLevel.None)]
    [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.SequenceBreak, AccessibilityLevel.Inspect)]
    [InlineData(
        AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak)]
    [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak, AccessibilityLevel.Normal)]
    [InlineData(AccessibilityLevel.None, AccessibilityLevel.Normal, AccessibilityLevel.None)]
    [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.Normal, AccessibilityLevel.Inspect)]
    [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.Normal, AccessibilityLevel.SequenceBreak)]
    [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Normal, AccessibilityLevel.Normal)]
    public void GetConnectionAccessibility_ShouldReturnExpected_WhenRequirementIsNotNull(
        AccessibilityLevel expected, AccessibilityLevel requirementAccessibility,
        AccessibilityLevel nodeAccessibility)
    {
        var sut = new NodeConnection(_fromNode, _toNode, _requirement);

        _requirement.Accessibility.Returns(requirementAccessibility);
        _fromNode.Accessibility.Returns(nodeAccessibility);
        _fromNode.GetNodeAccessibility(Arg.Any<IList<INode>>()).Returns(nodeAccessibility);
            
        Assert.Equal(expected, sut.GetConnectionAccessibility(new List<INode>()));
    }

    [Fact]
    public void NodeChanged_ShouldRaisePropertyChanged()
    {
        const AccessibilityLevel accessibility = AccessibilityLevel.Normal;
            
        var sut = new NodeConnection(_fromNode, _toNode);

        _fromNode.Accessibility.Returns(accessibility);
        _fromNode.GetNodeAccessibility(Arg.Any<IList<INode>>()).Returns(accessibility);

        _fromNode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _fromNode, new PropertyChangedEventArgs(nameof(INode.Accessibility)));
            
        Assert.Equal(accessibility, sut.Accessibility);
    }

    [Fact]
    public void RequirementChanged_ShouldRaisePropertyChanged()
    {
        const AccessibilityLevel accessibility = AccessibilityLevel.Normal;
            
        var sut = new NodeConnection(_fromNode, _toNode, _requirement);

        _fromNode.Accessibility.Returns(accessibility);
        _fromNode.GetNodeAccessibility(Arg.Any<IList<INode>>()).Returns(accessibility);
        _requirement.Accessibility.Returns(accessibility);

        _requirement.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _requirement, new PropertyChangedEventArgs(nameof(IRequirement.Accessibility)));
            
        Assert.Equal(accessibility, sut.Accessibility);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<INodeConnection.Factory>();
        var sut = factory(_fromNode, _toNode, _requirement);
            
        Assert.NotNull(sut as NodeConnection);
    }
}