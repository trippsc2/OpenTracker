using System.Diagnostics.CodeAnalysis;
using Autofac;
using FluentAssertions;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.AlwaysDisplayDungeonItems;
using OpenTracker.Models.Settings;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.AlwaysDisplayDungeonItems;

[ExcludeFromCodeCoverage]
public sealed class AlwaysDisplayDungeonItemsRequirementTests
{
    private readonly LayoutSettings _layoutSettings = new();

    [Fact]
    public void Met_ShouldRaisePropertyChanged()
    {
        var sut = new AlwaysDisplayDungeonItemsRequirement(_layoutSettings, true);
        using var monitor = sut.Monitor();
        
        _layoutSettings.AlwaysDisplayDungeonItems = true;
        
        monitor.Should().RaisePropertyChangeFor(x => x.Met);
    }
    
    [Theory]
    [InlineData(true, false, false)]
    [InlineData(false, false, true)]
    [InlineData(true, true, true)]
    public void Met_ShouldReturnExpectedValue(bool expected, bool alwaysDisplayDungeonItems, bool requirement)
    {
        var sut = new AlwaysDisplayDungeonItemsRequirement(_layoutSettings, requirement);
        _layoutSettings.AlwaysDisplayDungeonItems = alwaysDisplayDungeonItems;

        sut.Met.Should().Be(expected);
    }

    [Fact]
    public void Accessibility_ShouldRaisePropertyChanged()
    {
        var sut = new AlwaysDisplayDungeonItemsRequirement(_layoutSettings, true);
        using var monitor = sut.Monitor();
        
        _layoutSettings.AlwaysDisplayDungeonItems = true;
        
        monitor.Should().RaisePropertyChangeFor(x => x.Accessibility);
    }

    [Fact]
    public void Accessibility_ShouldRaiseChangePropagated()
    {
        var sut = new AlwaysDisplayDungeonItemsRequirement(_layoutSettings, true);
        using var monitor = sut.Monitor();
        
        _layoutSettings.AlwaysDisplayDungeonItems = true;
        
        monitor.Should().Raise(nameof(IRequirement.ChangePropagated));
    }

    [Theory]
    [InlineData(AccessibilityLevel.Normal, false, false)]
    [InlineData(AccessibilityLevel.None, false, true)]
    [InlineData(AccessibilityLevel.Normal, true, true)]
    public void Accessibility_ShouldReturnExpectedValue(
        AccessibilityLevel expected,
        bool alwaysDisplayDungeonItems,
        bool requirement)
    {
        var sut = new AlwaysDisplayDungeonItemsRequirement(_layoutSettings, requirement);
        _layoutSettings.AlwaysDisplayDungeonItems = alwaysDisplayDungeonItems;

        sut.Accessibility.Should().Be(expected);
    }

    [Fact]
    public void AutofacResolve_ShouldResolveInterfaceToTransientInstance()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<AlwaysDisplayDungeonItemsRequirement.Factory>();
        var sut1 = factory(false);
        var sut2 = factory(false);
        
        sut1.Should().NotBeSameAs(sut2);
    }
}