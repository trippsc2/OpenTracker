using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Items;
using OpenTracker.Models.Prizes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Item.Crystal;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Item.Crystal;

[ExcludeFromCodeCoverage]
public sealed class CrystalRequirementTests
{
    private readonly ICrystalRequirementItem _gtCrystal = Substitute.For<ICrystalRequirementItem>();
    private readonly IItem _crystal = Substitute.For<IItem>();
    private readonly IItem _redCrystal = Substitute.For<IItem>();

    private readonly CrystalRequirement _sut;

    public CrystalRequirementTests()
    {
        var items = Substitute.For<IItemDictionary>();
        var prizes = Substitute.For<IPrizeDictionary>();
            
        items[ItemType.TowerCrystals].Returns(_gtCrystal);
        prizes[PrizeType.Crystal].Returns(_crystal);
        prizes[PrizeType.RedCrystal].Returns(_redCrystal);

        _sut = new CrystalRequirement(items, prizes);
    }

    [Fact]
    public void ItemChanged_ShouldUpdateValue_WhenRequirementKnownChanged()
    {
        _gtCrystal.Known.Returns(true);

        _gtCrystal.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _gtCrystal, new PropertyChangedEventArgs(nameof(ICrystalRequirementItem.Known)));
            
        Assert.Equal(AccessibilityLevel.None, _sut.Accessibility);
    }
        
    [Fact]
    public void ItemChanged_ShouldUpdateValue_WhenRequirementCurrentChanged()
    {
        _gtCrystal.Known.Returns(true);

        _gtCrystal.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _gtCrystal, new PropertyChangedEventArgs(nameof(IItem.Current)));
            
        Assert.Equal(AccessibilityLevel.None, _sut.Accessibility);
    }
        
    [Fact]
    public void ItemChanged_ShouldUpdateValue_WhenCrystalCurrentChanged()
    {
        _gtCrystal.Known.Returns(true);

        _crystal.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _gtCrystal, new PropertyChangedEventArgs(nameof(IItem.Current)));
            
        Assert.Equal(AccessibilityLevel.None, _sut.Accessibility);
    }
        
    [Fact]
    public void ItemChanged_ShouldUpdateValue_WhenRedCrystalCurrentChanged()
    {
        _gtCrystal.Known.Returns(true);

        _redCrystal.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _gtCrystal, new PropertyChangedEventArgs(nameof(IItem.Current)));
            
        Assert.Equal(AccessibilityLevel.None, _sut.Accessibility);
    }

    [Fact]
    public void Accessibility_ShouldRaisePropertyChanged()
    {
        _gtCrystal.Known.Returns(true);

        Assert.PropertyChanged(_sut, nameof(IRequirement.Accessibility),
            () => _gtCrystal.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _gtCrystal, new PropertyChangedEventArgs(nameof(ICrystalRequirementItem.Known))));
    }

    [Fact]
    public void Accessibility_ShouldRaiseChangePropagated()
    {
        _gtCrystal.Known.Returns(true);

        var eventRaised = false;

        void Handler(object? sender, EventArgs e)
        {
            eventRaised = true;
        }

        _sut.ChangePropagated += Handler;
        _gtCrystal.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _gtCrystal, new PropertyChangedEventArgs(nameof(ICrystalRequirementItem.Known)));
        _sut.ChangePropagated -= Handler;
            
        Assert.True(eventRaised);
    }

    [Theory]
    [InlineData(AccessibilityLevel.SequenceBreak, false, 0, 0, 0)]
    [InlineData(AccessibilityLevel.SequenceBreak, false, 0, 4, 1)]
    [InlineData(AccessibilityLevel.SequenceBreak, false, 0, 4, 2)]
    [InlineData(AccessibilityLevel.SequenceBreak, false, 0, 5, 1)]
    [InlineData(AccessibilityLevel.Normal, false, 0, 5, 2)]
    [InlineData(AccessibilityLevel.SequenceBreak, false, 7, 0, 0)]
    [InlineData(AccessibilityLevel.SequenceBreak, false, 7, 4, 1)]
    [InlineData(AccessibilityLevel.SequenceBreak, false, 7, 4, 2)]
    [InlineData(AccessibilityLevel.SequenceBreak, false, 7, 5, 1)]
    [InlineData(AccessibilityLevel.Normal, false, 7, 5, 2)]
    [InlineData(AccessibilityLevel.None, true, 0, 0, 0)]
    [InlineData(AccessibilityLevel.None, true, 0, 4, 1)]
    [InlineData(AccessibilityLevel.None, true, 0, 4, 2)]
    [InlineData(AccessibilityLevel.None, true, 0, 5, 1)]
    [InlineData(AccessibilityLevel.Normal, true, 0, 5, 2)]
    [InlineData(AccessibilityLevel.None, true, 6, 0, 0)]
    [InlineData(AccessibilityLevel.Normal, true, 6, 1, 0)]
    [InlineData(AccessibilityLevel.Normal, true, 6, 0, 1)]
    [InlineData(AccessibilityLevel.Normal, true, 7, 0, 0)]
    public void Accessibility_ShouldEqualExpected(
        AccessibilityLevel expected, bool known, int requirement, int crystal, int redCrystal)
    {
        _gtCrystal.Known.Returns(known);
        _gtCrystal.Current.Returns(requirement);

        _crystal.Current.Returns(crystal);
        _redCrystal.Current.Returns(redCrystal);
            
        _gtCrystal.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _gtCrystal, new PropertyChangedEventArgs(nameof(ICrystalRequirementItem.Known)));
            
        Assert.Equal(expected, _sut.Accessibility);
    }

    [Fact]
    public void Met_ShouldRaisePropertyChanged()
    {
        _gtCrystal.Known.Returns(true);

        Assert.PropertyChanged(_sut, nameof(IRequirement.Met),
            () => _gtCrystal.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _gtCrystal, new PropertyChangedEventArgs(nameof(ICrystalRequirementItem.Known))));
    }

    [Theory]
    [InlineData(true, false, 0, 0, 0)]
    [InlineData(true, false, 0, 4, 1)]
    [InlineData(true, false, 0, 4, 2)]
    [InlineData(true, false, 0, 5, 1)]
    [InlineData(true, false, 0, 5, 2)]
    [InlineData(true, false, 7, 0, 0)]
    [InlineData(true, false, 7, 4, 1)]
    [InlineData(true, false, 7, 4, 2)]
    [InlineData(true, false, 7, 5, 1)]
    [InlineData(true, false, 7, 5, 2)]
    [InlineData(false, true, 0, 0, 0)]
    [InlineData(false, true, 0, 4, 1)]
    [InlineData(false, true, 0, 4, 2)]
    [InlineData(false, true, 0, 5, 1)]
    [InlineData(true, true, 0, 5, 2)]
    [InlineData(false, true, 6, 0, 0)]
    [InlineData(true, true, 6, 1, 0)]
    [InlineData(true, true, 6, 0, 1)]
    [InlineData(true, true, 7, 0, 0)]
    public void Met_ShouldEqualExpected(bool expected, bool known, int requirement, int crystal, int redCrystal)
    {
        _gtCrystal.Known.Returns(known);
        _gtCrystal.Current.Returns(requirement);

        _crystal.Current.Returns(crystal);
        _redCrystal.Current.Returns(redCrystal);
            
        _gtCrystal.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _gtCrystal, new PropertyChangedEventArgs(nameof(ICrystalRequirementItem.Known)));
            
        Assert.Equal(expected, _sut.Met);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<ICrystalRequirement.Factory>();
        var sut = factory();
            
        Assert.NotNull(sut as CrystalRequirement);
    }
}