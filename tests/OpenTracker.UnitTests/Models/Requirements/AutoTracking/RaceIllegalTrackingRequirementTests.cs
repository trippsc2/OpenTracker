using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using FluentAssertions;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.AutoTracking;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.AutoTracking;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.AutoTracking;

[ExcludeFromCodeCoverage]
public sealed class RaceIllegalTrackingRequirementTests
{
    private readonly IAutoTracker _autoTracker = Substitute.For<IAutoTracker>();

    private readonly RaceIllegalTrackingRequirement _sut;

    public RaceIllegalTrackingRequirementTests()
    {
        _sut = new RaceIllegalTrackingRequirement(_autoTracker);
    }
    
    private void ChangeRaceIllegalTracking(bool value)
    {
        _autoTracker.RaceIllegalTracking.Returns(value);
        _autoTracker.PropertyChanged += Raise
            .Event<PropertyChangedEventHandler>(
                _autoTracker,
                new PropertyChangedEventArgs(nameof(IAutoTracker.RaceIllegalTracking)));
    }

    [Fact]
    public void Met_ShouldRaisePropertyChanged()
    {
        using var monitor = _sut.Monitor();
        
        ChangeRaceIllegalTracking(true);
        
        monitor.Should().RaisePropertyChangeFor(x => x.Met);
    }
    
    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void Met_ShouldReturnExpectedValue(bool expected, bool raceIllegalTracking)
    {
        ChangeRaceIllegalTracking(raceIllegalTracking);

        _sut.Met.Should().Be(expected);
    }

    [Fact]
    public void Accessibility_ShouldRaisePropertyChanged()
    {
        using var monitor = _sut.Monitor();
        
        ChangeRaceIllegalTracking(true);
        
        monitor.Should().RaisePropertyChangeFor(x => x.Accessibility);
    }

    [Fact]
    public void Accessibility_ShouldRaiseChangePropagated()
    {
        using var monitor = _sut.Monitor();
        
        ChangeRaceIllegalTracking(true);
        
        monitor.Should().Raise(nameof(IRequirement.ChangePropagated));
    }

    [Theory]
    [InlineData(AccessibilityLevel.None, false)]
    [InlineData(AccessibilityLevel.Normal, true)]
    public void Accessibility_ShouldReturnExpectedValue(AccessibilityLevel expected, bool raceIllegalTracking)
    {
        ChangeRaceIllegalTracking(raceIllegalTracking);

        _sut.Accessibility.Should().Be(expected);
    }

    [Fact]
    public void AutofacResolve_ShouldResolveToTransientInstance()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut1 = scope.Resolve<RaceIllegalTrackingRequirement>();
        var sut2 = scope.Resolve<RaceIllegalTrackingRequirement>();

        sut1.Should().NotBeSameAs(sut2);
    }
}