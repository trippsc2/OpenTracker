using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.ShopShuffle;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.ShopShuffle;

[ExcludeFromCodeCoverage]
public sealed class ShopShuffleRequirementTests
{
    private readonly IMode _mode = Substitute.For<IMode>();

    [Fact]
    public void ModeChanged_ShouldUpdateMetValue()
    {
        var sut = new ShopShuffleRequirement(_mode, true);
        _mode.ShopShuffle.Returns(true);

        _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _mode, new PropertyChangedEventArgs(nameof(IMode.ShopShuffle)));
            
        Assert.True(sut.Met);
    }

    [Fact]
    public void Met_ShouldRaisePropertyChanged()
    {
        var sut = new ShopShuffleRequirement(_mode, true);
        _mode.ShopShuffle.Returns(true);

        Assert.PropertyChanged(sut, nameof(IRequirement.Met), 
            () => _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _mode, new PropertyChangedEventArgs(nameof(IMode.ShopShuffle))));
    }

    [Fact]
    public void Met_ShouldRaiseChangePropagated()
    {
        var sut = new ShopShuffleRequirement(_mode, true);
        _mode.ShopShuffle.Returns(true);

        var eventRaised = false;

        void Handler(object? sender, EventArgs e)
        {
            eventRaised = true;
        }
            
        sut.ChangePropagated += Handler;
        _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _mode, new PropertyChangedEventArgs(nameof(IMode.ShopShuffle)));
        sut.ChangePropagated -= Handler;
            
        Assert.True(eventRaised);
    }

    [Theory]
    [InlineData(true, false, false)]
    [InlineData(false, false, true)]
    [InlineData(false, true, false)]
    [InlineData(true, true, true)]
    public void Met_ShouldReturnExpectedValue(bool expected, bool shopShuffle, bool requirement)
    {
        _mode.ShopShuffle.Returns(shopShuffle);
        var sut = new ShopShuffleRequirement(_mode, requirement);
            
        Assert.Equal(expected, sut.Met);
    }

    [Fact]
    public void Accessibility_ShouldRaisePropertyChanged()
    {
        var sut = new ShopShuffleRequirement(_mode, true);
        _mode.ShopShuffle.Returns(true);

        Assert.PropertyChanged(sut, nameof(IRequirement.Accessibility), 
            () => _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _mode, new PropertyChangedEventArgs(nameof(IMode.ShopShuffle))));
    }

    [Theory]
    [InlineData(AccessibilityLevel.Normal, false, false)]
    [InlineData(AccessibilityLevel.None, false, true)]
    [InlineData(AccessibilityLevel.None, true, false)]
    [InlineData(AccessibilityLevel.Normal, true, true)]
    public void Accessibility_ShouldReturnExpectedValue(AccessibilityLevel expected, bool shopShuffle, bool requirement)
    {
        _mode.ShopShuffle.Returns(shopShuffle);
        var sut = new ShopShuffleRequirement(_mode, requirement);
            
        Assert.Equal(expected, sut.Accessibility);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IShopShuffleRequirement.Factory>();
        var sut = factory(false);
            
        Assert.NotNull(sut as ShopShuffleRequirement);
    }
}