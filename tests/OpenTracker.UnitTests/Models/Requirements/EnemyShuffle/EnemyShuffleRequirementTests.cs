using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using FluentAssertions;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.EnemyShuffle;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.EnemyShuffle;

[ExcludeFromCodeCoverage]
public sealed class EnemyShuffleRequirementTests
{
    private readonly IMode _mode = Substitute.For<IMode>();

    private void ChangeEnemyShuffle(bool newValue)
    {
        _mode.EnemyShuffle.Returns(newValue);
        _mode.PropertyChanged += Raise
            .Event<PropertyChangedEventHandler>(
                _mode,
                new PropertyChangedEventArgs(nameof(IMode.EnemyShuffle)));
    }

    [Fact]
    public void Met_ShouldRaisePropertyChanged()
    {
        var sut = new EnemyShuffleRequirement(_mode, true);
        using var monitor = sut.Monitor();
        
        ChangeEnemyShuffle(true);
        
        monitor.Should().RaisePropertyChangeFor(x => x.Met);
    }

    [Theory]
    [InlineData(true, false, false)]
    [InlineData(false, false, true)]
    [InlineData(true, true, true)]
    public void Met_ShouldReturnExpectedValue(bool expected, bool enemyShuffle, bool requirement)
    {
        var sut = new EnemyShuffleRequirement(_mode, requirement);
        ChangeEnemyShuffle(enemyShuffle);
            
        sut.Met.Should().Be(expected);
    }

    [Fact]
    public void Accessibility_ShouldRaisePropertyChanged()
    {
        var sut = new EnemyShuffleRequirement(_mode, true);
        using var monitor = sut.Monitor();
        
        ChangeEnemyShuffle(true);
        
        monitor.Should().RaisePropertyChangeFor(x => x.Accessibility);
    }

    [Fact]
    public void Accessibility_ShouldRaiseChangePropagated()
    {
        var sut = new EnemyShuffleRequirement(_mode, true);
        using var monitor = sut.Monitor();
        
        ChangeEnemyShuffle(true);
        
        monitor.Should().Raise(nameof(IRequirement.ChangePropagated));
    }

    [Theory]
    [InlineData(AccessibilityLevel.Normal, false, false)]
    [InlineData(AccessibilityLevel.None, false, true)]
    [InlineData(AccessibilityLevel.Normal, true, true)]
    public void Accessibility_ShouldReturnExpectedValue(
        AccessibilityLevel expected,
        bool enemyShuffle,
        bool requirement)
    {
        var sut = new EnemyShuffleRequirement(_mode, requirement);
        ChangeEnemyShuffle(enemyShuffle);

        sut.Accessibility.Should().Be(expected);
    }

    [Fact]
    public void AutofacResolve_ShouldResolveToTransientInstance()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<EnemyShuffleRequirement.Factory>();
        var sut1 = factory(false);
        var sut2 = factory(false);
            
        sut1.Should().NotBeSameAs(sut2);
    }
}