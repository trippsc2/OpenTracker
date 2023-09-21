using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using FluentAssertions;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.BossShuffle;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.BossShuffle;

[ExcludeFromCodeCoverage]
public sealed class BossShuffleRequirementTests
{
    private readonly IMode _mode = Substitute.For<IMode>();

    private void ChangeBossShuffle(bool newValue)
    {
        _mode.BossShuffle.Returns(newValue);
        _mode.PropertyChanged += Raise
            .Event<PropertyChangedEventHandler>(
                _mode,
                new PropertyChangedEventArgs(nameof(IMode.BossShuffle)));
    }
    
    [Fact]
    public void Met_ShouldRaisePropertyChanged()
    {
        var sut = new BossShuffleRequirement(_mode, true);
        using var monitor = sut.Monitor();
        
        ChangeBossShuffle(true);
        
        monitor.Should().RaisePropertyChangeFor(x => x.Met);
    }

    [Theory]
    [InlineData(true, false, false)]
    [InlineData(false, false, true)]
    [InlineData(true, true, true)]
    public void Met_ShouldReturnExpectedValue(bool expected, bool bossShuffle, bool requirement)
    {
        var sut = new BossShuffleRequirement(_mode, requirement);
        
        ChangeBossShuffle(bossShuffle);

        sut.Met.Should().Be(expected);
    }

    [Fact]
    public void Accessibility_ShouldRaisePropertyChanged()
    {
        var sut = new BossShuffleRequirement(_mode, true);
        using var monitor = sut.Monitor();
        
        ChangeBossShuffle(true);
        
        monitor.Should().RaisePropertyChangeFor(x => x.Accessibility);
    }
    
    [Fact]
    public void Accessibility_ShouldRaiseChangePropagated()
    {
        var sut = new BossShuffleRequirement(_mode, true);
        using var monitor = sut.Monitor();
        
        ChangeBossShuffle(true);

        monitor.Should().Raise(nameof(IRequirement.ChangePropagated));
    }

    [Theory]
    [InlineData(AccessibilityLevel.Normal, false, false)]
    [InlineData(AccessibilityLevel.None, false, true)]
    [InlineData(AccessibilityLevel.Normal, true, true)]
    public void Accessibility_ShouldReturnExpectedValue(
        AccessibilityLevel expected, bool bossShuffle, bool requirement)
    {
        var sut = new BossShuffleRequirement(_mode, requirement);
        ChangeBossShuffle(bossShuffle);

        sut.Accessibility.Should().Be(expected);
    }
        
    [Fact]
    public void AutofacResolve_ShouldResolveToTransientInstance()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<BossShuffleRequirement.Factory>();
        var sut1 = factory(false);
        var sut2 = factory(false);

        sut1.Should().NotBeSameAs(sut2);
    }
}