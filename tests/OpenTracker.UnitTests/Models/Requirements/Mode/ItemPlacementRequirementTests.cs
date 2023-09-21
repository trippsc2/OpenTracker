using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using FluentAssertions;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Mode;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Mode;

[ExcludeFromCodeCoverage]
public sealed class ItemPlacementRequirementTests
{
    private readonly IMode _mode = Substitute.For<IMode>();
    
    private void ChangeItemPlacement(ItemPlacement newValue)
    {
        _mode.ItemPlacement.Returns(newValue);
        _mode.PropertyChanged += Raise
            .Event<PropertyChangedEventHandler>(
                _mode,
                new PropertyChangedEventArgs(nameof(IMode.ItemPlacement)));
    }

    [Fact]
    public void Met_ShouldRaisePropertyChanged()
    {
        const ItemPlacement itemPlacement = ItemPlacement.Advanced;
        
        var sut = new ItemPlacementRequirement(_mode, itemPlacement);
        using var monitor = sut.Monitor();
        
        ChangeItemPlacement(itemPlacement);
        
        monitor.Should().RaisePropertyChangeFor(x => x.Met);
    }

    [Theory]
    [InlineData(true, ItemPlacement.Basic, ItemPlacement.Basic)]
    [InlineData(false, ItemPlacement.Basic, ItemPlacement.Advanced)]
    [InlineData(false, ItemPlacement.Advanced, ItemPlacement.Basic)]
    [InlineData(true, ItemPlacement.Advanced, ItemPlacement.Advanced)]
    public void Met_ShouldReturnExpectedValue(
        bool expected,
        ItemPlacement itemPlacement,
        ItemPlacement requirement)
    {
        var sut = new ItemPlacementRequirement(_mode, requirement);
        ChangeItemPlacement(itemPlacement);

        sut.Met.Should().Be(expected);
    }

    [Fact]
    public void Accessibility_ShouldRaisePropertyChanged()
    {
        const ItemPlacement itemPlacement = ItemPlacement.Advanced;
        
        var sut = new ItemPlacementRequirement(_mode, itemPlacement);
        using var monitor = sut.Monitor();
        
        ChangeItemPlacement(itemPlacement);
        
        monitor.Should().RaisePropertyChangeFor(x => x.Accessibility);
    }

    [Fact]
    public void Accessibility_ShouldRaiseChangePropagated()
    {
        const ItemPlacement itemPlacement = ItemPlacement.Advanced;
        
        var sut = new ItemPlacementRequirement(_mode, itemPlacement);
        using var monitor = sut.Monitor();
        
        ChangeItemPlacement(itemPlacement);

        monitor.Should().Raise(nameof(IRequirement.ChangePropagated));
    }

    [Theory]
    [InlineData(AccessibilityLevel.Normal, ItemPlacement.Basic, ItemPlacement.Basic)]
    [InlineData(AccessibilityLevel.None, ItemPlacement.Basic, ItemPlacement.Advanced)]
    [InlineData(AccessibilityLevel.None, ItemPlacement.Advanced, ItemPlacement.Basic)]
    [InlineData(AccessibilityLevel.Normal, ItemPlacement.Advanced, ItemPlacement.Advanced)]
    public void Accessibility_ShouldReturnExpectedValue(
        AccessibilityLevel expected,
        ItemPlacement itemPlacement,
        ItemPlacement requirement)
    {
        var sut = new ItemPlacementRequirement(_mode, requirement);
        ChangeItemPlacement(itemPlacement);
            
        sut.Accessibility.Should().Be(expected);
    }

    [Fact]
    public void AutofacResolve_ShouldResolveToTransientInstance()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<ItemPlacementRequirement.Factory>();
        var sut1 = factory(ItemPlacement.Basic);
        var sut2 = factory(ItemPlacement.Basic);
            
        sut1.Should().NotBeSameAs(sut2);
    }
}