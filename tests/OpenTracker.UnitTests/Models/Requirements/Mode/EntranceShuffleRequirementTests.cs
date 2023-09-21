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
public sealed class EntranceShuffleRequirementTests
{
    private readonly IMode _mode = Substitute.For<IMode>();
    
    private void ChangeEntranceShuffle(EntranceShuffle newValue)
    {
        _mode.EntranceShuffle.Returns(newValue);
        _mode.PropertyChanged += Raise
            .Event<PropertyChangedEventHandler>(
                _mode,
                new PropertyChangedEventArgs(nameof(IMode.EntranceShuffle)));
    }

    [Fact]
    public void Met_ShouldRaisePropertyChanged()
    {
        const EntranceShuffle entranceShuffle = EntranceShuffle.Dungeon;
        
        var sut = new EntranceShuffleRequirement(_mode, entranceShuffle);
        using var monitor = sut.Monitor();
        
        ChangeEntranceShuffle(entranceShuffle);
        
        monitor.Should().RaisePropertyChangeFor(x => x.Met);
    }

    [Theory]
    [InlineData(true, EntranceShuffle.None, EntranceShuffle.None)]
    [InlineData(false, EntranceShuffle.None, EntranceShuffle.Dungeon)]
    [InlineData(false, EntranceShuffle.None, EntranceShuffle.All)]
    [InlineData(false, EntranceShuffle.None, EntranceShuffle.Insanity)]
    [InlineData(false, EntranceShuffle.Dungeon, EntranceShuffle.None)]
    [InlineData(true, EntranceShuffle.Dungeon, EntranceShuffle.Dungeon)]
    [InlineData(false, EntranceShuffle.Dungeon, EntranceShuffle.All)]
    [InlineData(false, EntranceShuffle.Dungeon, EntranceShuffle.Insanity)]
    [InlineData(false, EntranceShuffle.All, EntranceShuffle.None)]
    [InlineData(false, EntranceShuffle.All, EntranceShuffle.Dungeon)]
    [InlineData(true, EntranceShuffle.All, EntranceShuffle.All)]
    [InlineData(false, EntranceShuffle.All, EntranceShuffle.Insanity)]
    [InlineData(false, EntranceShuffle.Insanity, EntranceShuffle.None)]
    [InlineData(false, EntranceShuffle.Insanity, EntranceShuffle.Dungeon)]
    [InlineData(false, EntranceShuffle.Insanity, EntranceShuffle.All)]
    [InlineData(true, EntranceShuffle.Insanity, EntranceShuffle.Insanity)]
    public void Met_ShouldReturnExpectedValue(
        bool expected, EntranceShuffle entranceShuffle, EntranceShuffle requirement)
    {
        var sut = new EntranceShuffleRequirement(_mode, requirement);
        ChangeEntranceShuffle(entranceShuffle);
            
        sut.Met.Should().Be(expected);
    }

    [Fact]
    public void Accessibility_ShouldRaisePropertyChanged()
    {
        const EntranceShuffle entranceShuffle = EntranceShuffle.Dungeon;
        
        var sut = new EntranceShuffleRequirement(_mode, entranceShuffle);
        using var monitor = sut.Monitor();
        
        ChangeEntranceShuffle(entranceShuffle);
        
        monitor.Should().RaisePropertyChangeFor(x => x.Accessibility);
    }

    [Fact]
    public void Accessibility_ShouldRaiseChangePropagated()
    {
        const EntranceShuffle entranceShuffle = EntranceShuffle.Dungeon;
        
        var sut = new EntranceShuffleRequirement(_mode, entranceShuffle);
        using var monitor = sut.Monitor();
        
        ChangeEntranceShuffle(entranceShuffle);

        monitor.Should().Raise(nameof(IRequirement.ChangePropagated));
    }

    [Theory]
    [InlineData(AccessibilityLevel.Normal, EntranceShuffle.None, EntranceShuffle.None)]
    [InlineData(AccessibilityLevel.None, EntranceShuffle.None, EntranceShuffle.Dungeon)]
    [InlineData(AccessibilityLevel.None, EntranceShuffle.None, EntranceShuffle.All)]
    [InlineData(AccessibilityLevel.None, EntranceShuffle.None, EntranceShuffle.Insanity)]
    [InlineData(AccessibilityLevel.None, EntranceShuffle.Dungeon, EntranceShuffle.None)]
    [InlineData(AccessibilityLevel.Normal, EntranceShuffle.Dungeon, EntranceShuffle.Dungeon)]
    [InlineData(AccessibilityLevel.None, EntranceShuffle.Dungeon, EntranceShuffle.All)]
    [InlineData(AccessibilityLevel.None, EntranceShuffle.Dungeon, EntranceShuffle.Insanity)]
    [InlineData(AccessibilityLevel.None, EntranceShuffle.All, EntranceShuffle.None)]
    [InlineData(AccessibilityLevel.None, EntranceShuffle.All, EntranceShuffle.Dungeon)]
    [InlineData(AccessibilityLevel.Normal, EntranceShuffle.All, EntranceShuffle.All)]
    [InlineData(AccessibilityLevel.None, EntranceShuffle.All, EntranceShuffle.Insanity)]
    [InlineData(AccessibilityLevel.None, EntranceShuffle.Insanity, EntranceShuffle.None)]
    [InlineData(AccessibilityLevel.None, EntranceShuffle.Insanity, EntranceShuffle.Dungeon)]
    [InlineData(AccessibilityLevel.None, EntranceShuffle.Insanity, EntranceShuffle.All)]
    [InlineData(AccessibilityLevel.Normal, EntranceShuffle.Insanity, EntranceShuffle.Insanity)]
    public void Accessibility_ShouldReturnExpectedValue(
        AccessibilityLevel expected, EntranceShuffle entranceShuffle, EntranceShuffle requirement)
    {
        var sut = new EntranceShuffleRequirement(_mode, requirement);
        ChangeEntranceShuffle(entranceShuffle);
            
        sut.Accessibility.Should().Be(expected);
    }

    [Fact]
    public void AutofacResolve_ShouldResolveToTransientInstance()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<EntranceShuffleRequirement.Factory>();
        var sut1 = factory(EntranceShuffle.None);
        var sut2 = factory(EntranceShuffle.None);
            
        sut1.Should().NotBeSameAs(sut2);
    }
}