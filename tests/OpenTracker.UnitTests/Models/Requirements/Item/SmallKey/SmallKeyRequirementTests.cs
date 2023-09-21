using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using FluentAssertions;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Items.Keys;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Item.SmallKey;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Item.SmallKey;

[ExcludeFromCodeCoverage]
public sealed class SmallKeyRequirementTests
{
    private readonly ISmallKeyItem _item = Substitute.For<ISmallKeyItem>();

    private void ChangeItemEffectiveCurrent(int newValue)
    {
        _item.EffectiveCurrent.Returns(newValue);
        _item.PropertyChanged += Raise
            .Event<PropertyChangedEventHandler>(
                _item,
                new PropertyChangedEventArgs(nameof(ISmallKeyItem.EffectiveCurrent)));
    }

    [Fact]
    public void Met_ShouldRaisePropertyChanged()
    {
        var sut = new SmallKeyRequirement(_item);
        using var monitor = sut.Monitor();

        ChangeItemEffectiveCurrent(1);
        
        monitor.Should().RaisePropertyChangeFor(x => x.Met);
    }

    [Theory]
    [InlineData(false, 0, 1)]
    [InlineData(true, 1, 1)]
    [InlineData(true, 2, 1)]
    [InlineData(true, 3, 1)]
    [InlineData(false, 0, 2)]
    [InlineData(false, 1, 2)]
    [InlineData(true, 2, 2)]
    [InlineData(true, 3, 2)]
    [InlineData(false, 0, 3)]
    [InlineData(false, 1, 3)]
    [InlineData(false, 2, 3)]
    [InlineData(true, 3, 3)]
    public void Met_ShouldMatchExpected(bool expected, int current, int count)
    {
        var sut = new SmallKeyRequirement(_item, count);
        ChangeItemEffectiveCurrent(current);

        sut.Met.Should().Be(expected);
    }

    [Fact]
    public void Accessibility_ShouldRaisePropertyChanged()
    {
        var sut = new SmallKeyRequirement(_item);
        using var monitor = sut.Monitor();
        
        ChangeItemEffectiveCurrent(1);
        
        monitor.Should().RaisePropertyChangeFor(x => x.Accessibility);
    }

    [Fact]
    public void Accessibility_ShouldRaiseChangePropagated()
    {
        var sut = new SmallKeyRequirement(_item);
        using var monitor = sut.Monitor();
        
        ChangeItemEffectiveCurrent(1);
        
        monitor.Should().Raise(nameof(IRequirement.ChangePropagated));
    }

    [Theory]
    [InlineData(AccessibilityLevel.None, 0, 1)]
    [InlineData(AccessibilityLevel.Normal, 1, 1)]
    [InlineData(AccessibilityLevel.Normal, 2, 1)]
    [InlineData(AccessibilityLevel.Normal, 3, 1)]
    [InlineData(AccessibilityLevel.None, 0, 2)]
    [InlineData(AccessibilityLevel.None, 1, 2)]
    [InlineData(AccessibilityLevel.Normal, 2, 2)]
    [InlineData(AccessibilityLevel.Normal, 3, 2)]
    [InlineData(AccessibilityLevel.None, 0, 3)]
    [InlineData(AccessibilityLevel.None, 1, 3)]
    [InlineData(AccessibilityLevel.None, 2, 3)]
    [InlineData(AccessibilityLevel.Normal, 3, 3)]
    public void Accessibility_ShouldMatchExpected(AccessibilityLevel expected, int current, int count)
    {
        var sut = new SmallKeyRequirement(_item, count);
        ChangeItemEffectiveCurrent(current);

        sut.Accessibility.Should().Be(expected);
    }

    [Fact]
    public void AutofacResolve_ShouldResolveToTransientInstance()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<SmallKeyRequirement.Factory>();
        var sut1 = factory(_item);
        var sut2 = factory(_item);
            
        sut1.Should().NotBeSameAs(sut2);
    }
}