using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Autofac;
using ExpectedObjects;
using NSubstitute;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Dungeons.AccessibilityProvider;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.Requirements.BossShuffle;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Sections.Boss;
using OpenTracker.Models.Sections.Factories;
using OpenTracker.Models.UndoRedo.Sections;
using Xunit;

namespace OpenTracker.UnitTests.Models.Sections.Factories;

[ExcludeFromCodeCoverage]
public sealed class BossSectionFactoryTests
{
    private static readonly IMode Mode = Substitute.For<IMode>();
    
    private static readonly IBossShuffleRequirementDictionary BossShuffleRequirements =
        new BossShuffleRequirementDictionary(expectedValue =>
            new BossShuffleRequirement(Mode, expectedValue));

    private static readonly IBossPlacementDictionary BossPlacements = new BossPlacementDictionary(
        () => Substitute.For<IBossPlacementFactory>());
    private static readonly IPrizePlacementDictionary PrizePlacements = new PrizePlacementDictionary(
        () => Substitute.For<IPrizePlacementFactory>());
        
    private static readonly ISaveLoadManager SaveLoadManager = Substitute.For<ISaveLoadManager>();

    private static readonly ICollectSection.Factory CollectSectionFactory =
        (section, force) => new CollectSection(section, force);
    private static readonly IUncollectSection.Factory UncollectSectionFactory =
        section => new UncollectSection(section);
    private static readonly ITogglePrizeSection.Factory TogglePrizeSectionFactory =
        (section, force) => new TogglePrizeSection(section, force);
        
    private static readonly IBossSection.Factory BossSectionFactory =
        (accessibilityProvider, name, bossPlacement, requirement) => new BossSection(
            SaveLoadManager, CollectSectionFactory, UncollectSectionFactory, accessibilityProvider, name,
            bossPlacement, null, requirement);
    private static readonly IPrizeSection.Factory PrizeSectionFactory = 
        (accessibilityProvider, name, bossPlacement, prizePlacement, autoTrackValue) => new PrizeSection(
            SaveLoadManager, CollectSectionFactory, UncollectSectionFactory, TogglePrizeSectionFactory,
            accessibilityProvider, name, bossPlacement, prizePlacement, autoTrackValue);

    private static readonly BossAccessibilityProvider AccessibilityProvider = new();
    private static readonly IAutoTrackValue AutoTrackValue = Substitute.For<IAutoTrackValue>();

    private static readonly Dictionary<(LocationID id, int index), ExpectedObject> ExpectedValues = new();

    private readonly BossSectionFactory _sut = new BossSectionFactory(
        BossShuffleRequirements, BossPlacements, PrizePlacements,
        BossSectionFactory, PrizeSectionFactory);

    private static void PopulateExpectedValues()
    {
        ExpectedValues.Clear();

        foreach (LocationID id in Enum.GetValues(typeof(LocationID)))
        {
            switch (id)
            {
                case LocationID.AgahnimTower:
                    ExpectedValues.Add((id, 1), PrizeSectionFactory(
                        AccessibilityProvider, "Boss", BossPlacements[BossPlacementID.ATBoss],
                        PrizePlacements[PrizePlacementID.ATPrize], AutoTrackValue).ToExpectedObject());
                    break;
                case LocationID.EasternPalace:
                    ExpectedValues.Add((id, 1), PrizeSectionFactory(
                        AccessibilityProvider, "Boss", BossPlacements[BossPlacementID.EPBoss],
                        PrizePlacements[PrizePlacementID.EPPrize], AutoTrackValue).ToExpectedObject());
                    break;
                case LocationID.DesertPalace:
                    ExpectedValues.Add((id, 1), PrizeSectionFactory(
                        AccessibilityProvider, "Boss", BossPlacements[BossPlacementID.DPBoss],
                        PrizePlacements[PrizePlacementID.DPPrize], AutoTrackValue).ToExpectedObject());
                    break;
                case LocationID.TowerOfHera:
                    ExpectedValues.Add((id, 1), PrizeSectionFactory(
                        AccessibilityProvider, "Boss", BossPlacements[BossPlacementID.ToHBoss],
                        PrizePlacements[PrizePlacementID.ToHPrize], AutoTrackValue).ToExpectedObject());
                    break;
                case LocationID.PalaceOfDarkness:
                    ExpectedValues.Add((id, 1), PrizeSectionFactory(
                        AccessibilityProvider, "Boss", BossPlacements[BossPlacementID.PoDBoss],
                        PrizePlacements[PrizePlacementID.PoDPrize], AutoTrackValue).ToExpectedObject());
                    break;
                case LocationID.SwampPalace:
                    ExpectedValues.Add((id, 1), PrizeSectionFactory(
                        AccessibilityProvider, "Boss", BossPlacements[BossPlacementID.SPBoss],
                        PrizePlacements[PrizePlacementID.SPPrize], AutoTrackValue).ToExpectedObject());
                    break;
                case LocationID.SkullWoods:
                    ExpectedValues.Add((id, 1), PrizeSectionFactory(
                        AccessibilityProvider, "Boss", BossPlacements[BossPlacementID.SWBoss],
                        PrizePlacements[PrizePlacementID.SWPrize], AutoTrackValue).ToExpectedObject());
                    break;
                case LocationID.ThievesTown:
                    ExpectedValues.Add((id, 1), PrizeSectionFactory(
                        AccessibilityProvider, "Boss", BossPlacements[BossPlacementID.TTBoss],
                        PrizePlacements[PrizePlacementID.TTPrize], AutoTrackValue).ToExpectedObject());
                    break;
                case LocationID.IcePalace:
                    ExpectedValues.Add((id, 1), PrizeSectionFactory(
                        AccessibilityProvider, "Boss", BossPlacements[BossPlacementID.IPBoss],
                        PrizePlacements[PrizePlacementID.IPPrize], AutoTrackValue).ToExpectedObject());
                    break;
                case LocationID.MiseryMire:
                    ExpectedValues.Add((id, 1), PrizeSectionFactory(
                        AccessibilityProvider, "Boss", BossPlacements[BossPlacementID.MMBoss],
                        PrizePlacements[PrizePlacementID.MMPrize], AutoTrackValue).ToExpectedObject());
                    break;
                case LocationID.TurtleRock:
                    ExpectedValues.Add((id, 1), PrizeSectionFactory(
                        AccessibilityProvider, "Boss", BossPlacements[BossPlacementID.TRBoss],
                        PrizePlacements[PrizePlacementID.TRPrize], AutoTrackValue).ToExpectedObject());
                    break;
                case LocationID.GanonsTower:
                    ExpectedValues.Add((id, 1), BossSectionFactory(
                        AccessibilityProvider, "Boss 1", BossPlacements[BossPlacementID.GTBoss1],
                        BossShuffleRequirements[true]).ToExpectedObject());
                    ExpectedValues.Add((id, 2), BossSectionFactory(
                        AccessibilityProvider, "Boss 2", BossPlacements[BossPlacementID.GTBoss2],
                        BossShuffleRequirements[true]).ToExpectedObject());
                    ExpectedValues.Add((id, 3), BossSectionFactory(
                        AccessibilityProvider, "Boss 3", BossPlacements[BossPlacementID.GTBoss3],
                        BossShuffleRequirements[true]).ToExpectedObject());
                    ExpectedValues.Add((id, 4), PrizeSectionFactory(
                        AccessibilityProvider, "Final Boss", BossPlacements[BossPlacementID.GTFinalBoss],
                        PrizePlacements[PrizePlacementID.GTPrize], AutoTrackValue).ToExpectedObject());
                    break;
            }
        }
    }

    [Fact]
    public void GetBossSection_ShouldThrowException_WhenIDIsUnexpected()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.GetBossSection(
            AccessibilityProvider, AutoTrackValue, (LocationID)int.MaxValue));
    }

    [Theory]
    [MemberData(nameof(GetBossSection_ShouldReturnExpectedData))]
    public void GetBossSection_ShouldReturnExpected(ExpectedObject expected, LocationID id, int index)
    {
        var actual = _sut.GetBossSection(AccessibilityProvider, AutoTrackValue, id, index);
            
        expected.ShouldEqual(actual);
    }

    public static IEnumerable<object[]> GetBossSection_ShouldReturnExpectedData()
    {
        PopulateExpectedValues();

        return ExpectedValues.Keys.Select(
            key => new object[] {ExpectedValues[key], key.id, key.index}).ToList();
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut = scope.Resolve<IBossSectionFactory>();
            
        Assert.NotNull(sut as BossSectionFactory);
    }
}