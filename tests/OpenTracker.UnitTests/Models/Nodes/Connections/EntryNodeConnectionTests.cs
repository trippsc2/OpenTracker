using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Nodes.Connections;
using Xunit;

namespace OpenTracker.UnitTests.Models.Nodes.Connections;

[ExcludeFromCodeCoverage]
public sealed class EntryNodeConnectionTests
{
    private readonly INode _fromNode = Substitute.For<INode>();

    private readonly EntryNodeConnection _sut;

    public EntryNodeConnectionTests()
    {
        _sut = new EntryNodeConnection(_fromNode);
    }

    [Fact]
    public void Requirement_ShouldAlwaysReturnNull()
    {
        Assert.Null(_sut.Requirement);
    }

    [Theory]
    [InlineData(AccessibilityLevel.None, AccessibilityLevel.None)]
    [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak)]
    [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Normal)]
    public void Accessibility_ShouldReturnFromNodeAccessibility(
        AccessibilityLevel expected, AccessibilityLevel nodeAccessibility)
    {
        _fromNode.Accessibility.Returns(nodeAccessibility);
            
        Assert.Equal(expected, _sut.Accessibility);
    }

    [Theory]
    [InlineData(AccessibilityLevel.None, AccessibilityLevel.None)]
    [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak)]
    [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Normal)]
    public void GetConnectionAccessibility_ShouldAlwaysReturnNodeAccessibility(
        AccessibilityLevel expected, AccessibilityLevel nodeAccessibility)
    {
        _fromNode.Accessibility.Returns(nodeAccessibility);
            
        Assert.Equal(expected, _sut.GetConnectionAccessibility(new List<INode>()));
    }

    [Fact]
    public void NodeChanged_ShouldRaiseAccessibilityChanged()
    {
        Assert.PropertyChanged(_sut, nameof(IEntryNodeConnection.Accessibility), () =>
            _fromNode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _fromNode, new PropertyChangedEventArgs(nameof(INode.Accessibility))));
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IEntryNodeConnection.Factory>();
        var sut = factory(_fromNode);
            
        Assert.NotNull(sut as EntryNodeConnection);
    }
}