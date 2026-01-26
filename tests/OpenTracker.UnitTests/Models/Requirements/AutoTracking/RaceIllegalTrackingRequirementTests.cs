using System;
using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.AutoTracking;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.AutoTracking;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.AutoTracking;

public class RaceIllegalTrackingRequirementTests
{
    private readonly IAutoTracker _autoTracker = Substitute.For<IAutoTracker>();

    private readonly RaceIllegalTrackingRequirement _sut;

    public RaceIllegalTrackingRequirementTests()
    {
        _sut = new RaceIllegalTrackingRequirement(_autoTracker);
    }
        
    [Fact]
    public void AutoTrackerChanged_ShouldUpdateMetValue()
    {
        _autoTracker.RaceIllegalTracking.Returns(true);

        _autoTracker.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _autoTracker, new PropertyChangedEventArgs(nameof(IAutoTracker.RaceIllegalTracking)));
            
        Assert.True(_sut.Met);
    }

    [Fact]
    public void Met_ShouldRaisePropertyChanged()
    {
        _autoTracker.RaceIllegalTracking.Returns(true);

        Assert.PropertyChanged(_sut, nameof(IRequirement.Met), 
            () => _autoTracker.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _autoTracker, new PropertyChangedEventArgs(nameof(IAutoTracker.RaceIllegalTracking))));
    }

    [Fact]
    public void Met_ShouldRaiseChangePropagated()
    {
        _autoTracker.RaceIllegalTracking.Returns(true);
            
        var eventRaised = false;

        void Handler(object? sender, EventArgs e)
        {
            eventRaised = true;
        }
            
        _sut.ChangePropagated += Handler;
        _autoTracker.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _autoTracker, new PropertyChangedEventArgs(nameof(IAutoTracker.RaceIllegalTracking)));
        _sut.ChangePropagated -= Handler;
            
        Assert.True(eventRaised);
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void Met_ShouldReturnExpectedValue(bool expected, bool raceIllegalTracking)
    {
        _autoTracker.RaceIllegalTracking.Returns(raceIllegalTracking);
            
        _autoTracker.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _autoTracker, new PropertyChangedEventArgs(nameof(IAutoTracker.RaceIllegalTracking)));

        Assert.Equal(expected, _sut.Met);
    }

    [Fact]
    public void Accessibility_ShouldRaisePropertyChanged()
    {
        _autoTracker.RaceIllegalTracking.Returns(true);

        Assert.PropertyChanged(_sut, nameof(IRequirement.Accessibility), 
            () => _autoTracker.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _autoTracker, new PropertyChangedEventArgs(nameof(IAutoTracker.RaceIllegalTracking))));
    }

    [Theory]
    [InlineData(AccessibilityLevel.None, false)]
    [InlineData(AccessibilityLevel.Normal, true)]
    public void Accessibility_ShouldReturnExpectedValue(AccessibilityLevel expected, bool raceIllegalTracking)
    {
        _autoTracker.RaceIllegalTracking.Returns(raceIllegalTracking);
            
        _autoTracker.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _autoTracker, new PropertyChangedEventArgs(nameof(IAutoTracker.RaceIllegalTracking)));

        Assert.Equal(expected, _sut.Accessibility);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut = scope.Resolve<IRaceIllegalTrackingRequirement>();
            
        Assert.NotNull(sut as RaceIllegalTrackingRequirement);
    }
}