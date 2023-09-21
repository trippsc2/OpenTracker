using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using FluentAssertions;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Mode;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Mode;

[ExcludeFromCodeCoverage]
public sealed class WorldStateRequirementTests
{
    private readonly IMode _mode = Substitute.For<IMode>();

    private void ChangeWorldState(WorldState newValue)
    {
        _mode.WorldState.Returns(newValue);
        _mode.PropertyChanged += Raise
            .Event<PropertyChangedEventHandler>(
                _mode,
                new PropertyChangedEventArgs(nameof(IMode.WorldState)));
    }

    [Fact]
    public void Met_ShouldRaisePropertyChanged()
    {
        const WorldState worldState = WorldState.Inverted;
        
        var sut = new WorldStateRequirement(_mode, worldState);
        using var monitor = sut.Monitor();
        
        ChangeWorldState(worldState);
        
        monitor.Should().RaisePropertyChangeFor(x => x.Met);
    }

    [Theory]
    [InlineData(true, WorldState.StandardOpen, WorldState.StandardOpen)]
    [InlineData(false, WorldState.StandardOpen, WorldState.Inverted)]
    [InlineData(false, WorldState.Inverted, WorldState.StandardOpen)]
    [InlineData(true, WorldState.Inverted, WorldState.Inverted)]
    public void Met_ShouldReturnExpectedValue(bool expected, WorldState worldState, WorldState requirement)
    {
        var sut = new WorldStateRequirement(_mode, requirement);
        ChangeWorldState(worldState);
            
        sut.Met.Should().Be(expected);
    }

    [Fact]
    public void Accessibility_ShouldRaisePropertyChanged()
    {
        const WorldState worldState = WorldState.Inverted;
        
        var sut = new WorldStateRequirement(_mode, worldState);
        using var monitor = sut.Monitor();
        
        ChangeWorldState(worldState);
        
        monitor.Should().RaisePropertyChangeFor(x => x.Accessibility);
    }

    [Fact]
    public void Accessibility_ShouldRaiseChangePropagated()
    {
        const WorldState worldState = WorldState.Inverted;
        
        var sut = new WorldStateRequirement(_mode, worldState);
        using var monitor = sut.Monitor();
        
        ChangeWorldState(worldState);

        monitor.Should().Raise(nameof(IRequirement.ChangePropagated));
    }

    [Theory]
    [InlineData(AccessibilityLevel.Normal, WorldState.StandardOpen, WorldState.StandardOpen)]
    [InlineData(AccessibilityLevel.None, WorldState.StandardOpen, WorldState.Inverted)]
    [InlineData(AccessibilityLevel.None, WorldState.Inverted, WorldState.StandardOpen)]
    [InlineData(AccessibilityLevel.Normal, WorldState.Inverted, WorldState.Inverted)]
    public void Accessibility_ShouldReturnExpectedValue(AccessibilityLevel expected, WorldState worldState, WorldState requirement)
    {
        var sut = new WorldStateRequirement(_mode, requirement);
        ChangeWorldState(worldState);
            
        sut.Accessibility.Should().Be(expected);
    }

    [Fact]
    public void AutofacResolve_ShouldResolveToTransientInstance()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<WorldStateRequirement.Factory>();
        var sut1 = factory(WorldState.StandardOpen);
        var sut2 = factory(WorldState.StandardOpen);
        
        sut1.Should().NotBeSameAs(sut2);
    }
}