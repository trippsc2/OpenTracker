using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using FluentAssertions;
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

    private void ChangeNodeAccessibility(AccessibilityLevel newValue)
    {
        _node.Accessibility.Returns(newValue);
        _node.PropertyChanged += Raise
            .Event<PropertyChangedEventHandler>(
                _node,
                new PropertyChangedEventArgs(nameof(INode.Accessibility)));
    }

    public NodeRequirementTests()
    {
        _sut = new NodeRequirement(_node);
    }

    [Fact]
    public void Accessibility_ShouldRaisePropertyChanged()
    {
        using var monitor = _sut.Monitor();
        
        ChangeNodeAccessibility(AccessibilityLevel.Normal);
        
        monitor.Should().RaisePropertyChangeFor(x => x.Accessibility);
    }

    [Theory]
    [InlineData(AccessibilityLevel.None, AccessibilityLevel.None)]
    [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.Inspect)]
    [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak)]
    [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Normal)]
    public void Accessibility_ShouldMatchExpected(AccessibilityLevel expected, AccessibilityLevel nodeAccessibility)
    {
        ChangeNodeAccessibility(nodeAccessibility);
            
        _sut.Accessibility.Should().Be(expected);
    }

    [Fact]
    public void Met_ShouldRaisePropertyChanged()
    {
        using var monitor = _sut.Monitor();
        
        ChangeNodeAccessibility(AccessibilityLevel.Normal);
        
        monitor.Should().RaisePropertyChangeFor(x => x.Met);
    }

    [Fact]
    public void Met_ShouldRaiseChangePropagated()
    {
        using var monitor = _sut.Monitor();

        ChangeNodeAccessibility(AccessibilityLevel.Normal);

        monitor.Should().Raise(nameof(IRequirement.ChangePropagated));
    }

    [Theory]
    [InlineData(false, AccessibilityLevel.None)]
    [InlineData(true, AccessibilityLevel.Inspect)]
    [InlineData(true, AccessibilityLevel.SequenceBreak)]
    [InlineData(true, AccessibilityLevel.Normal)]
    public void Met_ShouldMatchExpected(bool expected, AccessibilityLevel nodeAccessibility)
    {
        ChangeNodeAccessibility(nodeAccessibility);
            
        _sut.Met.Should().Be(expected);
    }

    [Fact]
    public void AutofacResolve_ShouldResolveToTransientInstance()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<NodeRequirement.Factory>();
        var sut1 = factory(_node);
        var sut2 = factory(_node);
            
        sut1.Should().NotBeSameAs(sut2);
    }
}