using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using FluentAssertions;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Boss;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Boss;

[ExcludeFromCodeCoverage]
public sealed class BossRequirementTests
{
    private readonly IBossTypeRequirementDictionary _bossTypeRequirements;
    private readonly IBossPlacement _bossPlacement = Substitute.For<IBossPlacement>();

    private readonly BossRequirement _sut;

    public BossRequirementTests()
    {
        _bossTypeRequirements = new BossTypeRequirementDictionary(
            () => Substitute.For<IBossTypeRequirementFactory>());

        _sut = new BossRequirement(_bossTypeRequirements, _bossPlacement);
    }

    private static void SetRequirementAccessibility(IRequirement requirement, AccessibilityLevel accessibility)
    {
        requirement.Accessibility.Returns(accessibility);
        requirement.PropertyChanged += Raise
            .Event<PropertyChangedEventHandler>(
                requirement,
                new PropertyChangedEventArgs(nameof(IRequirement.Accessibility)));
    }

    private void SetCurrentBoss(BossType? bossType)
    {
        _bossPlacement.CurrentBoss.Returns(bossType);
        _bossPlacement.PropertyChanged += Raise
            .Event<PropertyChangedEventHandler>(
                _bossPlacement,
                new PropertyChangedEventArgs(nameof(IBossPlacement.CurrentBoss)));
    }

    [Fact]
    public void Accessibility_ShouldRaisePropertyChanged()
    {
        using var monitor = _sut.Monitor();
        
        const BossType bossType = BossType.Armos;
        SetRequirementAccessibility(_bossTypeRequirements[bossType], AccessibilityLevel.Normal);
        SetCurrentBoss(bossType);

        monitor.Should().RaisePropertyChangeFor(x => x.Accessibility);
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
    public void Accessibility_ShouldReturnExpected(AccessibilityLevel accessibility, BossType? bossType)
    {
        var requirement = bossType is null ? _bossTypeRequirements.NoBoss.Value
            : _bossTypeRequirements[bossType.Value];
            
        SetRequirementAccessibility(requirement, accessibility);
        SetCurrentBoss(bossType);

        _sut.Accessibility.Should().Be(accessibility);
    }

    [Fact]
    public void Met_ShouldRaisePropertyChanged()
    {
        using var monitor = _sut.Monitor();
        
        const BossType bossType = BossType.Armos;
        SetRequirementAccessibility(_bossTypeRequirements[bossType], AccessibilityLevel.Normal);
        SetCurrentBoss(bossType);
        
        monitor.Should().RaisePropertyChangeFor(x => x.Met);
    }

    [Fact]
    public void Met_ShouldRaiseChangePropagated()
    {
        using var monitor = _sut.Monitor();
        
        const BossType bossType = BossType.Armos;
        SetRequirementAccessibility(_bossTypeRequirements[bossType], AccessibilityLevel.Normal);
        SetCurrentBoss(bossType);

        monitor.Should().Raise(nameof(IRequirement.ChangePropagated));
    }

    [Theory]
    [InlineData(false, AccessibilityLevel.None, null)]
    [InlineData(true, AccessibilityLevel.Normal, null)]
    [InlineData(false, AccessibilityLevel.None, BossType.Armos)]
    [InlineData(true, AccessibilityLevel.Normal, BossType.Armos)]
    [InlineData(false, AccessibilityLevel.None, BossType.Lanmolas)]
    [InlineData(true, AccessibilityLevel.Normal, BossType.Lanmolas)]
    [InlineData(false, AccessibilityLevel.None, BossType.Moldorm)]
    [InlineData(true, AccessibilityLevel.Normal, BossType.Moldorm)]
    [InlineData(false, AccessibilityLevel.None, BossType.HelmasaurKing)]
    [InlineData(true, AccessibilityLevel.Normal, BossType.HelmasaurKing)]
    [InlineData(false, AccessibilityLevel.None, BossType.Arrghus)]
    [InlineData(true, AccessibilityLevel.Normal, BossType.Arrghus)]
    [InlineData(false, AccessibilityLevel.None, BossType.Mothula)]
    [InlineData(true, AccessibilityLevel.Normal, BossType.Mothula)]
    [InlineData(false, AccessibilityLevel.None, BossType.Blind)]
    [InlineData(true, AccessibilityLevel.Normal, BossType.Blind)]
    [InlineData(false, AccessibilityLevel.None, BossType.Kholdstare)]
    [InlineData(true, AccessibilityLevel.Normal, BossType.Kholdstare)]
    [InlineData(false, AccessibilityLevel.None, BossType.Vitreous)]
    [InlineData(true, AccessibilityLevel.Normal, BossType.Vitreous)]
    [InlineData(false, AccessibilityLevel.None, BossType.Trinexx)]
    [InlineData(true, AccessibilityLevel.Normal, BossType.Trinexx)]
    [InlineData(false, AccessibilityLevel.None, BossType.Aga)]
    [InlineData(true, AccessibilityLevel.Normal, BossType.Aga)]
    public void Met_ShouldReturnExpected(bool expected, AccessibilityLevel accessibility, BossType? bossType)
    {
        var requirement = bossType is null ? _bossTypeRequirements.NoBoss.Value
            : _bossTypeRequirements[bossType.Value];
            
        SetRequirementAccessibility(requirement, accessibility);
        SetCurrentBoss(bossType);
        
        _sut.Met.Should().Be(expected);
    }

    [Fact]
    public void AutofacResolve_ShouldResolveInterfaceToTransientInstance()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IBossRequirement.Factory>();
        var sut1 = factory(_bossPlacement);

        sut1.Should().BeOfType<BossRequirement>();
        
        var sut2 = factory(_bossPlacement);

        sut1.Should().NotBeSameAs(sut2);
    }
}