using System;
using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Mode;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Mode;

public class EntranceShuffleRequirementTests
{
    private readonly IMode _mode = Substitute.For<IMode>();

    [Fact]
    public void ModeChanged_ShouldUpdateMetValue()
    {
        const EntranceShuffle entranceShuffle = EntranceShuffle.Dungeon;
        var sut = new EntranceShuffleRequirement(_mode, entranceShuffle);
        _mode.EntranceShuffle.Returns(entranceShuffle);

        _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _mode, new PropertyChangedEventArgs(nameof(IMode.EntranceShuffle)));
            
        Assert.True(sut.Met);
    }

    [Fact]
    public void Met_ShouldRaisePropertyChanged()
    {
        const EntranceShuffle entranceShuffle = EntranceShuffle.Dungeon;
        var sut = new EntranceShuffleRequirement(_mode, entranceShuffle);
        _mode.EntranceShuffle.Returns(entranceShuffle);

        Assert.PropertyChanged(sut, nameof(IRequirement.Met), 
            () => _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _mode, new PropertyChangedEventArgs(nameof(IMode.EntranceShuffle))));
    }

    [Fact]
    public void Met_ShouldRaiseChangePropagated()
    {
        const EntranceShuffle entranceShuffle = EntranceShuffle.Dungeon;
        var sut = new EntranceShuffleRequirement(_mode, entranceShuffle);
        _mode.EntranceShuffle.Returns(entranceShuffle);

        var eventRaised = false;

        void Handler(object? sender, EventArgs e)
        {
            eventRaised = true;
        }
            
        sut.ChangePropagated += Handler;
        _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _mode, new PropertyChangedEventArgs(nameof(IMode.EntranceShuffle)));
        sut.ChangePropagated -= Handler;
            
        Assert.True(eventRaised);
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
        _mode.EntranceShuffle.Returns(entranceShuffle);
        var sut = new EntranceShuffleRequirement(_mode, requirement);
            
        Assert.Equal(expected, sut.Met);
    }

    [Fact]
    public void Accessibility_ShouldRaisePropertyChanged()
    {
        const EntranceShuffle entranceShuffle = EntranceShuffle.Dungeon;
        var sut = new EntranceShuffleRequirement(_mode, entranceShuffle);
        _mode.EntranceShuffle.Returns(entranceShuffle);

        Assert.PropertyChanged(sut, nameof(IRequirement.Accessibility), 
            () => _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _mode, new PropertyChangedEventArgs(nameof(IMode.EntranceShuffle))));
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
        _mode.EntranceShuffle.Returns(entranceShuffle);
        var sut = new EntranceShuffleRequirement(_mode, requirement);
            
        Assert.Equal(expected, sut.Accessibility);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IEntranceShuffleRequirement.Factory>();
        var sut = factory(EntranceShuffle.None);
            
        Assert.NotNull(sut as EntranceShuffleRequirement);
    }
}