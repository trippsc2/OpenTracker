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

public class ItemPlacementRequirementTests
{
    private readonly IMode _mode = Substitute.For<IMode>();

    [Fact]
    public void ModeChanged_ShouldUpdateMetValue()
    {
        const ItemPlacement itemPlacement = ItemPlacement.Advanced;
        var sut = new ItemPlacementRequirement(_mode, itemPlacement);
        _mode.ItemPlacement.Returns(itemPlacement);

        _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _mode, new PropertyChangedEventArgs(nameof(IMode.ItemPlacement)));
            
        Assert.True(sut.Met);
    }

    [Fact]
    public void Met_ShouldRaisePropertyChanged()
    {
        const ItemPlacement itemPlacement = ItemPlacement.Advanced;
        var sut = new ItemPlacementRequirement(_mode, itemPlacement);
        _mode.ItemPlacement.Returns(itemPlacement);

        Assert.PropertyChanged(sut, nameof(IRequirement.Met), 
            () => _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _mode, new PropertyChangedEventArgs(nameof(IMode.ItemPlacement))));
    }

    [Fact]
    public void Met_ShouldRaiseChangePropagated()
    {
        const ItemPlacement itemPlacement = ItemPlacement.Advanced;
        var sut = new ItemPlacementRequirement(_mode, itemPlacement);
        _mode.ItemPlacement.Returns(itemPlacement);

        var eventRaised = false;

        void Handler(object? sender, EventArgs e)
        {
            eventRaised = true;
        }
            
        sut.ChangePropagated += Handler;
        _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _mode, new PropertyChangedEventArgs(nameof(IMode.ItemPlacement)));
        sut.ChangePropagated -= Handler;
            
        Assert.True(eventRaised);
    }

    [Theory]
    [InlineData(true, ItemPlacement.Basic, ItemPlacement.Basic)]
    [InlineData(false, ItemPlacement.Basic, ItemPlacement.Advanced)]
    [InlineData(false, ItemPlacement.Advanced, ItemPlacement.Basic)]
    [InlineData(true, ItemPlacement.Advanced, ItemPlacement.Advanced)]
    public void Met_ShouldReturnExpectedValue(
        bool expected, ItemPlacement itemPlacement, ItemPlacement requirement)
    {
        _mode.ItemPlacement.Returns(itemPlacement);
        var sut = new ItemPlacementRequirement(_mode, requirement);
            
        Assert.Equal(expected, sut.Met);
    }

    [Fact]
    public void Accessibility_ShouldRaisePropertyChanged()
    {
        const ItemPlacement itemPlacement = ItemPlacement.Advanced;
        var sut = new ItemPlacementRequirement(_mode, itemPlacement);
        _mode.ItemPlacement.Returns(itemPlacement);

        Assert.PropertyChanged(sut, nameof(IRequirement.Accessibility), 
            () => _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _mode, new PropertyChangedEventArgs(nameof(IMode.ItemPlacement))));
    }

    [Theory]
    [InlineData(AccessibilityLevel.Normal, ItemPlacement.Basic, ItemPlacement.Basic)]
    [InlineData(AccessibilityLevel.None, ItemPlacement.Basic, ItemPlacement.Advanced)]
    [InlineData(AccessibilityLevel.None, ItemPlacement.Advanced, ItemPlacement.Basic)]
    [InlineData(AccessibilityLevel.Normal, ItemPlacement.Advanced, ItemPlacement.Advanced)]
    public void Accessibility_ShouldReturnExpectedValue(
        AccessibilityLevel expected, ItemPlacement itemPlacement, ItemPlacement requirement)
    {
        _mode.ItemPlacement.Returns(itemPlacement);
        var sut = new ItemPlacementRequirement(_mode, requirement);
            
        Assert.Equal(expected, sut.Accessibility);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IItemPlacementRequirement.Factory>();
        var sut = factory(ItemPlacement.Basic);
            
        Assert.NotNull(sut as ItemPlacementRequirement);
    }
}