using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Boss;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Boss;

[ExcludeFromCodeCoverage]
public sealed class BossRequirementTests
{
    private readonly IMode _mode = Substitute.For<IMode>();
        
    private readonly IBossTypeRequirementDictionary _bossTypeRequirements;
    private readonly IBossPlacement _bossPlacement = Substitute.For<IBossPlacement>();

    private readonly BossRequirement _sut;

    public BossRequirementTests()
    {
        _bossTypeRequirements = new BossTypeRequirementDictionary(
            () => Substitute.For<IBossTypeRequirementFactory>());

        _sut = new BossRequirement(_mode, _bossTypeRequirements, _bossPlacement);
    }

    [Fact]
    public void ModeChanged_ShouldUpdateAccessibility()
    {
        const BossType bossType = BossType.Armos;
        _bossTypeRequirements[bossType].Accessibility.Returns(AccessibilityLevel.Normal);
        _bossPlacement.GetCurrentBoss().Returns(bossType);

        _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _mode, new PropertyChangedEventArgs(nameof(IMode.BossShuffle)));
            
        Assert.Equal(_bossTypeRequirements[bossType].Accessibility, _sut.Accessibility);
    }

    [Fact]
    public void BossPlacementChanged_ShouldUpdateAccessibility()
    {
        const BossType bossType = BossType.Armos;
        _bossTypeRequirements[bossType].Accessibility.Returns(AccessibilityLevel.Normal);
        _bossPlacement.GetCurrentBoss().Returns(bossType);

        _bossPlacement.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _bossPlacement, new PropertyChangedEventArgs(nameof(IBossPlacement.Boss)));
            
        Assert.Equal(_bossTypeRequirements[bossType].Accessibility, _sut.Accessibility);
    }

    [Fact]
    public void RequirementChanged_ShouldUpdateAccessibility()
    {
        var requirement = _bossTypeRequirements.NoBoss.Value;
        requirement.Accessibility.Returns(AccessibilityLevel.Normal);

        requirement.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            requirement, new PropertyChangedEventArgs(nameof(IRequirement.Accessibility)));
            
        Assert.Equal(requirement.Accessibility, _sut.Accessibility);
    }

    [Fact]
    public void Accessibility_ShouldRaisePropertyChanged()
    {
        const BossType bossType = BossType.Armos;
        _bossTypeRequirements[bossType].Accessibility.Returns(AccessibilityLevel.Normal);
        _bossPlacement.GetCurrentBoss().Returns(bossType);

        Assert.PropertyChanged(_sut, nameof(IRequirement.Accessibility),
            () => _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _mode, new PropertyChangedEventArgs(nameof(IMode.BossShuffle))));
    }

    [Fact]
    public void Accessibility_ShouldRaiseChangePropagated()
    {
        const BossType bossType = BossType.Armos;
        _bossTypeRequirements[bossType].Accessibility.Returns(AccessibilityLevel.Normal);
        _bossPlacement.GetCurrentBoss().Returns(bossType);

        var eventRaised = false;

        void Handler(object? sender, EventArgs e)
        {
            eventRaised = true;
        }
            
        _sut.ChangePropagated += Handler;
        _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _mode, new PropertyChangedEventArgs(nameof(IMode.BossShuffle)));
        _sut.ChangePropagated -= Handler;
            
        Assert.True(eventRaised);
    }

    [Theory]
    [InlineData(AccessibilityLevel.None, null)]
    [InlineData(AccessibilityLevel.Normal, null)]
    [InlineData(AccessibilityLevel.None, BossType.Armos)]
    [InlineData(AccessibilityLevel.Normal, BossType.Armos)]
    [InlineData(AccessibilityLevel.None, BossType.Lanmolas)]
    [InlineData(AccessibilityLevel.Normal, BossType.Lanmolas)]
    [InlineData(AccessibilityLevel.None, BossType.Moldorm)]
    [InlineData(AccessibilityLevel.Normal, BossType.Moldorm)]
    [InlineData(AccessibilityLevel.None, BossType.HelmasaurKing)]
    [InlineData(AccessibilityLevel.Normal, BossType.HelmasaurKing)]
    [InlineData(AccessibilityLevel.None, BossType.Arrghus)]
    [InlineData(AccessibilityLevel.Normal, BossType.Arrghus)]
    [InlineData(AccessibilityLevel.None, BossType.Mothula)]
    [InlineData(AccessibilityLevel.Normal, BossType.Mothula)]
    [InlineData(AccessibilityLevel.None, BossType.Blind)]
    [InlineData(AccessibilityLevel.Normal, BossType.Blind)]
    [InlineData(AccessibilityLevel.None, BossType.Kholdstare)]
    [InlineData(AccessibilityLevel.Normal, BossType.Kholdstare)]
    [InlineData(AccessibilityLevel.None, BossType.Vitreous)]
    [InlineData(AccessibilityLevel.Normal, BossType.Vitreous)]
    [InlineData(AccessibilityLevel.None, BossType.Trinexx)]
    [InlineData(AccessibilityLevel.Normal, BossType.Trinexx)]
    [InlineData(AccessibilityLevel.None, BossType.Aga)]
    [InlineData(AccessibilityLevel.Normal, BossType.Aga)]
    public void Accessibility_ShouldEqualExpected(AccessibilityLevel accessibility, BossType? bossType)
    {
        var requirement = bossType is null ? _bossTypeRequirements.NoBoss.Value
            : _bossTypeRequirements[bossType.Value];
            
        requirement.Accessibility.Returns(accessibility);
        _bossPlacement.GetCurrentBoss().Returns(bossType);

        _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _mode, new PropertyChangedEventArgs(nameof(IMode.BossShuffle)));
        requirement.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            requirement, new PropertyChangedEventArgs(nameof(IRequirement.Accessibility)));
            
        Assert.Equal(requirement.Accessibility, _sut.Accessibility);
    }

    [Fact]
    public void Met_ShouldRaisePropertyChanged()
    {
        const BossType bossType = BossType.Armos;
        _bossTypeRequirements[bossType].Accessibility.Returns(AccessibilityLevel.Normal);
        _bossPlacement.GetCurrentBoss().Returns(bossType);

        Assert.PropertyChanged(_sut, nameof(IRequirement.Met),
            () => _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _mode, new PropertyChangedEventArgs(nameof(IMode.BossShuffle))));
    }

    [Theory]
    [InlineData(false, AccessibilityLevel.None)]
    [InlineData(true, AccessibilityLevel.Inspect)]
    [InlineData(true, AccessibilityLevel.SequenceBreak)]
    [InlineData(true, AccessibilityLevel.Normal)]
    public void Met_ShouldEqualExpected(bool expected, AccessibilityLevel accessibility)
    {
        var requirement = _bossTypeRequirements.NoBoss.Value;
            
        requirement.Accessibility.Returns(accessibility);

        _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _mode, new PropertyChangedEventArgs(nameof(IMode.BossShuffle)));
        requirement.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            requirement, new PropertyChangedEventArgs(nameof(IRequirement.Accessibility)));
            
        Assert.Equal(expected, _sut.Met);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IBossRequirement.Factory>();
        var sut = factory(_bossPlacement);
            
        Assert.NotNull(sut as BossRequirement);
    }
}