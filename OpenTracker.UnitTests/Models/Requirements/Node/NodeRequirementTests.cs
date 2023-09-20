using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Node;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Node;

[ExcludeFromCodeCoverage]
public sealed class NodeRequirementTests
{
    private readonly INode _node = Substitute.For<INode>();

    private readonly NodeRequirement _sut;

    public NodeRequirementTests()
    {
        _sut = new NodeRequirement(_node);
    }

    [Fact]
    public void NodeChanged_ShouldUpdateValue()
    {
        _node.Accessibility.Returns(AccessibilityLevel.Normal);

        _node.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _node, new PropertyChangedEventArgs(nameof(IOverworldNode.Accessibility)));
            
        Assert.Equal(AccessibilityLevel.Normal, _sut.Accessibility);
    }

    [Fact]
    public void Accessibility_ShouldRaisePropertyChanged()
    {
        _node.Accessibility.Returns(AccessibilityLevel.Normal);

        Assert.PropertyChanged(_sut, nameof(IRequirement.Accessibility), 
            () => _node.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _node, new PropertyChangedEventArgs(nameof(IOverworldNode.Accessibility))));
    }

    [Fact]
    public void Accessibility_ShouldRaiseChangePropagated()
    {
        _node.Accessibility.Returns(AccessibilityLevel.Normal);

        var eventRaised = false;

        void Handler(object? sender, EventArgs e)
        {
            eventRaised = true;
        }
            
        _sut.ChangePropagated += Handler;
        _node.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _node, new PropertyChangedEventArgs(nameof(IOverworldNode.Accessibility)));
        _sut.ChangePropagated -= Handler;
            
        Assert.True(eventRaised);
    }

    [Theory]
    [InlineData(AccessibilityLevel.None, AccessibilityLevel.None)]
    [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.Inspect)]
    [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak)]
    [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Normal)]
    public void Accessibility_ShouldMatchExpected(AccessibilityLevel expected, AccessibilityLevel nodeAccessibility)
    {
        _node.Accessibility.Returns(nodeAccessibility);
            
        _node.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _node, new PropertyChangedEventArgs(nameof(IOverworldNode.Accessibility)));
            
        Assert.Equal(expected, _sut.Accessibility);
    }

    [Fact]
    public void Met_ShouldRaisePropertyChanged()
    {
        _node.Accessibility.Returns(AccessibilityLevel.Normal);

        Assert.PropertyChanged(_sut, nameof(IRequirement.Met), 
            () => _node.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _node, new PropertyChangedEventArgs(nameof(IOverworldNode.Accessibility))));
    }

    [Theory]
    [InlineData(false, AccessibilityLevel.None)]
    [InlineData(true, AccessibilityLevel.Inspect)]
    [InlineData(true, AccessibilityLevel.SequenceBreak)]
    [InlineData(true, AccessibilityLevel.Normal)]
    public void Met_ShouldMatchExpected(bool expected, AccessibilityLevel nodeAccessibility)
    {
        _node.Accessibility.Returns(nodeAccessibility);
            
        _node.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _node, new PropertyChangedEventArgs(nameof(IOverworldNode.Accessibility)));
            
        Assert.Equal(expected, _sut.Met);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<INodeRequirement.Factory>();
        var sut = factory(_node);
            
        Assert.NotNull(sut as NodeRequirement);
    }
}