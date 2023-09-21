using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using FluentAssertions;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.TakeAnyLocations;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.TakeAnyLocations;

[ExcludeFromCodeCoverage]
public sealed class TakeAnyLocationsRequirementTests
{
    private readonly IMode _mode = Substitute.For<IMode>();
    
    private void ChangeTakeAnyLocations(bool newValue)
    {
        _mode.TakeAnyLocations.Returns(newValue);
        _mode.PropertyChanged += Raise
            .Event<PropertyChangedEventHandler>(
                _mode,
                new PropertyChangedEventArgs(nameof(IMode.TakeAnyLocations)));
    }

    [Fact]
    public void Met_ShouldRaisePropertyChanged()
    {
        var sut = new TakeAnyLocationsRequirement(_mode, true);
        using var monitor = sut.Monitor();
        
        ChangeTakeAnyLocations(true);
        
        monitor.Should().RaisePropertyChangeFor(x => x.Met);
    }

    [Theory]
    [InlineData(true, false, false)]
    [InlineData(false, false, true)]
    [InlineData(false, true, false)]
    [InlineData(true, true, true)]
    public void Met_ShouldReturnExpectedValue(bool expected, bool takeAnyLocations, bool requirement)
    {
        var sut = new TakeAnyLocationsRequirement(_mode, requirement);
        ChangeTakeAnyLocations(takeAnyLocations);
            
        sut.Met.Should().Be(expected);
    }

    [Fact]
    public void Accessibility_ShouldRaisePropertyChanged()
    {
        var sut = new TakeAnyLocationsRequirement(_mode, true);
        using var monitor = sut.Monitor();
        
        ChangeTakeAnyLocations(true);
        
        monitor.Should().RaisePropertyChangeFor(x => x.Accessibility);
    }

    [Fact]
    public void Accessibility_ShouldRaiseChangePropagated()
    {
        var sut = new TakeAnyLocationsRequirement(_mode, true);
        using var monitor = sut.Monitor();
        
        ChangeTakeAnyLocations(true);

        monitor.Should().Raise(nameof(IRequirement.ChangePropagated));
    }

    [Theory]
    [InlineData(AccessibilityLevel.Normal, false, false)]
    [InlineData(AccessibilityLevel.None, false, true)]
    [InlineData(AccessibilityLevel.None, true, false)]
    [InlineData(AccessibilityLevel.Normal, true, true)]
    public void Accessibility_ShouldReturnExpectedValue(
        AccessibilityLevel expected,
        bool takeAnyLocations,
        bool requirement)
    {
        var sut = new TakeAnyLocationsRequirement(_mode, requirement);
        ChangeTakeAnyLocations(takeAnyLocations);
            
        sut.Accessibility.Should().Be(expected);
    }

    [Fact]
    public void AutofacResolve_ShouldResolveToTransientInstance()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<TakeAnyLocationsRequirement.Factory>();
        var sut1 = factory(false);
        var sut2 = factory(false);
            
        sut1.Should().NotBeSameAs(sut2);
    }
}