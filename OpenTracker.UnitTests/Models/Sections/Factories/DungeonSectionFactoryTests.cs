using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.AccessibilityProvider;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Sections.Factories;
using Xunit;

namespace OpenTracker.UnitTests.Models.Sections.Factories;

[ExcludeFromCodeCoverage]
public sealed class DungeonSectionFactoryTests
{
    private readonly IDungeonDictionary _dungeons = new DungeonDictionary(
        () => Substitute.For<IDungeonFactory>());

    private readonly IDungeonAccessibilityProvider.Factory _accessibilityProviderFactory = _ =>
    {
        var substitute = Substitute.For<IDungeonAccessibilityProvider>();

        var bossAccessibilityProviders = new List<BossAccessibilityProvider>
        {
            new(),
            new(),
            new(),
            new()
        };

        substitute.BossAccessibilityProviders.Returns(bossAccessibilityProviders);

        return substitute;
    };

    private readonly ISectionAutoTrackingFactory _autoTrackingFactory =
        Substitute.For<ISectionAutoTrackingFactory>();
    private readonly IBossSectionFactory _bossSectionFactory = Substitute.For<IBossSectionFactory>();
    private readonly IDungeonItemSectionFactory _dungeonItemSectionFactory =
        Substitute.For<IDungeonItemSectionFactory>();

    private readonly DungeonSectionFactory _sut;

    public DungeonSectionFactoryTests()
    {
        _sut = new DungeonSectionFactory(
            _dungeons, _accessibilityProviderFactory, _autoTrackingFactory, _bossSectionFactory,
            _dungeonItemSectionFactory);
    }

    [Fact]
    public void GetDungeonSections_ShouldThrowException_WhenIDIsUnexpected()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            _ = _sut.GetDungeonSections((LocationID) int.MaxValue));
    }

    [Theory]
    [InlineData(LocationID.HyruleCastle)]
    [InlineData(LocationID.AgahnimTower)]
    [InlineData(LocationID.EasternPalace)]
    [InlineData(LocationID.DesertPalace)]
    [InlineData(LocationID.TowerOfHera)]
    [InlineData(LocationID.PalaceOfDarkness)]
    [InlineData(LocationID.SwampPalace)]
    [InlineData(LocationID.SkullWoods)]
    [InlineData(LocationID.ThievesTown)]
    [InlineData(LocationID.IcePalace)]
    [InlineData(LocationID.MiseryMire)]
    [InlineData(LocationID.TurtleRock)]
    [InlineData(LocationID.GanonsTower)]
    public void GetDungeonSections_ShouldCallGetDungeonItemSection(LocationID id)
    {
        _ = _sut.GetDungeonSections(id);

        var dungeonID = Enum.Parse<DungeonID>(id.ToString());

        _dungeonItemSectionFactory.Received(1).GetDungeonItemSection(
            _dungeons[dungeonID], Arg.Any<IDungeonAccessibilityProvider>(),
            Arg.Any<IAutoTrackValue>(), id);
    }

    [Theory]
    [InlineData(LocationID.AgahnimTower, 1)]
    [InlineData(LocationID.EasternPalace, 1)]
    [InlineData(LocationID.DesertPalace, 1)]
    [InlineData(LocationID.TowerOfHera, 1)]
    [InlineData(LocationID.PalaceOfDarkness, 1)]
    [InlineData(LocationID.SwampPalace, 1)]
    [InlineData(LocationID.SkullWoods, 1)]
    [InlineData(LocationID.ThievesTown, 1)]
    [InlineData(LocationID.IcePalace, 1)]
    [InlineData(LocationID.MiseryMire, 1)]
    [InlineData(LocationID.TurtleRock, 1)]
    [InlineData(LocationID.GanonsTower, 4)]
    public void GetDungeonSections_ShouldCallGetBossSection(LocationID id, int numberOfCalls)
    {
        _ = _sut.GetDungeonSections(id);

        _bossSectionFactory.Received(numberOfCalls).GetBossSection(
            Arg.Any<BossAccessibilityProvider>(), Arg.Any<IAutoTrackValue>(),
            id, Arg.Any<int>());
    }

    [Fact]
    public void GetDungeonSections_ShouldNotCallGetBossSection_WhenIDIsHyruleCastle()
    {
        _ = _sut.GetDungeonSections(LocationID.HyruleCastle);

        _bossSectionFactory.DidNotReceive().GetBossSection(
            Arg.Any<BossAccessibilityProvider>(), Arg.Any<IAutoTrackValue>(),
            Arg.Any<LocationID>(), Arg.Any<int>());
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut = scope.Resolve<IDungeonSectionFactory>();
            
        Assert.NotNull(sut as DungeonSectionFactory);
    }
}