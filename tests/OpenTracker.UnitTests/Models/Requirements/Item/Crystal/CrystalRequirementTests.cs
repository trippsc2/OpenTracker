using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using FluentAssertions;
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
    
    private void ChangeGTCrystalKnown(bool newValue)
    {
        _gtCrystal.Known.Returns(newValue);
        _gtCrystal.PropertyChanged += Raise
            .Event<PropertyChangedEventHandler>(
                _gtCrystal,
                new PropertyChangedEventArgs(nameof(ICrystalRequirementItem.Known)));
    }

    private void ChangeGTCrystalCurrent(int newValue)
    {
        _gtCrystal.Current.Returns(newValue);
        _gtCrystal.PropertyChanged += Raise
            .Event<PropertyChangedEventHandler>(
                _gtCrystal,
                new PropertyChangedEventArgs(nameof(ICrystalRequirementItem.Current)));
    }
    
    private void ChangeCrystalCurrent(int newValue)
    {
        _crystal.Current.Returns(newValue);
        _crystal.PropertyChanged += Raise
            .Event<PropertyChangedEventHandler>(
                _crystal,
                new PropertyChangedEventArgs(nameof(IItem.Current)));
    }
    
    private void ChangeRedCrystalCurrent(int newValue)
    {
        _redCrystal.Current.Returns(newValue);
        _redCrystal.PropertyChanged += Raise
            .Event<PropertyChangedEventHandler>(
                _redCrystal,
                new PropertyChangedEventArgs(nameof(IItem.Current)));
    }

    [Fact]
    public void Met_ShouldRaisePropertyChanged()
    {
        using var monitor = _sut.Monitor();
        
        ChangeGTCrystalKnown(true);
        
        monitor.Should().RaisePropertyChangeFor(x => x.Met);
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
        ChangeGTCrystalKnown(known);
        ChangeGTCrystalCurrent(requirement);
        ChangeCrystalCurrent(crystal);
        ChangeRedCrystalCurrent(redCrystal);

        _sut.Met.Should().Be(expected);
    }

    [Fact]
    public void Accessibility_ShouldRaisePropertyChanged()
    {
        using var monitor = _sut.Monitor();
        
        ChangeGTCrystalKnown(true);
        
        monitor.Should().RaisePropertyChangeFor(x => x.Accessibility);
    }

    [Fact]
    public void Accessibility_ShouldRaiseChangePropagated()
    {
        using var monitor = _sut.Monitor();
        
        ChangeGTCrystalKnown(true);
        
        monitor.Should().Raise(nameof(IRequirement.ChangePropagated));
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
        ChangeGTCrystalKnown(known);
        ChangeGTCrystalCurrent(requirement);
        ChangeCrystalCurrent(crystal);
        ChangeRedCrystalCurrent(redCrystal);
        
        _sut.Accessibility.Should().Be(expected);
    }

    [Fact]
    public void AutofacResolve_ShouldResolveToTransientInstance()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut1 = scope.Resolve<ICrystalRequirement>();
        
        sut1.Should().BeOfType<CrystalRequirement>();

        var sut2 = scope.Resolve<ICrystalRequirement>();

        sut1.Should().NotBeSameAs(sut2);
    }
}