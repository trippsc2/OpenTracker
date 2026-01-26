using System;
using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.SmallKeyShuffle;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.SmallKeyShuffle;

public class SmallKeyShuffleRequirementTests
{
    private readonly IMode _mode = Substitute.For<IMode>();

    [Fact]
    public void ModeChanged_ShouldUpdateMetValue()
    {
        var sut = new SmallKeyShuffleRequirement(_mode, true);
        _mode.SmallKeyShuffle.Returns(true);

        _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _mode, new PropertyChangedEventArgs(nameof(IMode.SmallKeyShuffle)));
            
        Assert.True(sut.Met);
    }

    [Fact]
    public void Met_ShouldRaisePropertyChanged()
    {
        var sut = new SmallKeyShuffleRequirement(_mode, true);
        _mode.SmallKeyShuffle.Returns(true);

        Assert.PropertyChanged(sut, nameof(IRequirement.Met), 
            () => _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _mode, new PropertyChangedEventArgs(nameof(IMode.SmallKeyShuffle))));
    }

    [Fact]
    public void Met_ShouldRaiseChangePropagated()
    {
        var sut = new SmallKeyShuffleRequirement(_mode, true);
        _mode.SmallKeyShuffle.Returns(true);

        var eventRaised = false;

        void Handler(object? sender, EventArgs e)
        {
            eventRaised = true;
        }
            
        sut.ChangePropagated += Handler;
        _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _mode, new PropertyChangedEventArgs(nameof(IMode.SmallKeyShuffle)));
        sut.ChangePropagated -= Handler;
            
        Assert.True(eventRaised);
    }

    [Theory]
    [InlineData(true, false, false)]
    [InlineData(false, false, true)]
    [InlineData(false, true, false)]
    [InlineData(true, true, true)]
    public void Met_ShouldReturnExpectedValue(bool expected, bool smallKeyShuffle, bool requirement)
    {
        _mode.SmallKeyShuffle.Returns(smallKeyShuffle);
        var sut = new SmallKeyShuffleRequirement(_mode, requirement);
            
        Assert.Equal(expected, sut.Met);
    }

    [Fact]
    public void Accessibility_ShouldRaisePropertyChanged()
    {
        var sut = new SmallKeyShuffleRequirement(_mode, true);
        _mode.SmallKeyShuffle.Returns(true);

        Assert.PropertyChanged(sut, nameof(IRequirement.Accessibility), 
            () => _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _mode, new PropertyChangedEventArgs(nameof(IMode.SmallKeyShuffle))));
    }

    [Theory]
    [InlineData(AccessibilityLevel.Normal, false, false)]
    [InlineData(AccessibilityLevel.None, false, true)]
    [InlineData(AccessibilityLevel.None, true, false)]
    [InlineData(AccessibilityLevel.Normal, true, true)]
    public void Accessibility_ShouldReturnExpectedValue(
        AccessibilityLevel expected, bool smallKeyShuffle, bool requirement)
    {
        _mode.SmallKeyShuffle.Returns(smallKeyShuffle);
        var sut = new SmallKeyShuffleRequirement(_mode, requirement);
            
        Assert.Equal(expected, sut.Accessibility);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<ISmallKeyShuffleRequirement.Factory>();
        var sut = factory(false);
            
        Assert.NotNull(sut as SmallKeyShuffleRequirement);
    }
}