using System;
using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.AlwaysDisplayDungeonItems;
using OpenTracker.Models.Settings;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.AlwaysDisplayDungeonItems;

public class AlwaysDisplayDungeonItemsRequirementTests
{
    private readonly ILayoutSettings _layoutSettings = Substitute.For<ILayoutSettings>();

    [Fact]
    public void LayoutSettingsChanged_ShouldUpdateMetValue()
    {
        var sut = new AlwaysDisplayDungeonItemsRequirement(_layoutSettings, true);
        _layoutSettings.AlwaysDisplayDungeonItems.Returns(true);

        _layoutSettings.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _layoutSettings,
            new PropertyChangedEventArgs(nameof(ILayoutSettings.AlwaysDisplayDungeonItems)));
            
        Assert.True(sut.Met);
    }

    [Fact]
    public void LayoutSettings_ShouldRaisePropertyChanged()
    {
        var sut = new AlwaysDisplayDungeonItemsRequirement(_layoutSettings, true);
        _layoutSettings.AlwaysDisplayDungeonItems.Returns(true);

        Assert.PropertyChanged(sut, nameof(IRequirement.Met), 
            () => _layoutSettings.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _layoutSettings,
                new PropertyChangedEventArgs(nameof(ILayoutSettings.AlwaysDisplayDungeonItems))));
    }

    [Fact]
    public void Met_ShouldRaiseChangePropagated()
    {
        var sut = new AlwaysDisplayDungeonItemsRequirement(_layoutSettings, true);
        _layoutSettings.AlwaysDisplayDungeonItems.Returns(true);

        var eventRaised = false;

        void Handler(object? sender, EventArgs e)
        {
            eventRaised = true;
        }
            
        sut.ChangePropagated += Handler;
        _layoutSettings.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _layoutSettings,
            new PropertyChangedEventArgs(nameof(ILayoutSettings.AlwaysDisplayDungeonItems)));
        sut.ChangePropagated -= Handler;
            
        Assert.True(eventRaised);
    }

    [Theory]
    [InlineData(true, false, false)]
    [InlineData(false, false, true)]
    [InlineData(true, true, true)]
    public void Met_ShouldReturnExpectedValue(bool expected, bool bossShuffle, bool requirement)
    {
        _layoutSettings.AlwaysDisplayDungeonItems.Returns(bossShuffle);
        var sut = new AlwaysDisplayDungeonItemsRequirement(_layoutSettings, requirement);
            
        Assert.Equal(expected, sut.Met);
    }

    [Fact]
    public void Accessibility_ShouldRaisePropertyChanged()
    {
        var sut = new AlwaysDisplayDungeonItemsRequirement(_layoutSettings, true);
        _layoutSettings.AlwaysDisplayDungeonItems.Returns(true);

        Assert.PropertyChanged(sut, nameof(IRequirement.Accessibility),
            () => _layoutSettings.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _layoutSettings,
                new PropertyChangedEventArgs(nameof(ILayoutSettings.AlwaysDisplayDungeonItems))));
    }

    [Theory]
    [InlineData(AccessibilityLevel.Normal, false, false)]
    [InlineData(AccessibilityLevel.None, false, true)]
    [InlineData(AccessibilityLevel.Normal, true, true)]
    public void Accessibility_ShouldReturnExpectedValue(
        AccessibilityLevel expected, bool bigKeyShuffle, bool requirement)
    {
        _layoutSettings.AlwaysDisplayDungeonItems.Returns(bigKeyShuffle);
        var sut = new AlwaysDisplayDungeonItemsRequirement(_layoutSettings, requirement);
            
        Assert.Equal(expected, sut.Accessibility);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IAlwaysDisplayDungeonItemsRequirement.Factory>();
        var sut = factory(false);
            
        Assert.NotNull(sut as AlwaysDisplayDungeonItemsRequirement);
    }
}