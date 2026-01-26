using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using ExpectedObjects;
using NSubstitute;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.AccessibilityProvider;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Requirements.SmallKeyShuffle;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Sections.Factories;
using OpenTracker.Models.Sections.Item;
using OpenTracker.Models.UndoRedo.Sections;
using Xunit;

namespace OpenTracker.UnitTests.Models.Sections.Factories;

public class DungeonItemSectionFactoryTests
{
    private static readonly ISmallKeyShuffleRequirementDictionary SmallKeyShuffleRequirements =
        new SmallKeyShuffleRequirementDictionary(_ => Substitute.For<ISmallKeyShuffleRequirement>());

    private static readonly IMarking.Factory MarkingFactory = () => Substitute.For<IMarking>();

    private static readonly ISaveLoadManager SaveLoadManager = Substitute.For<ISaveLoadManager>();
        
    private static readonly ICollectSection.Factory CollectSectionFactory =
        (section, force) => new CollectSection(section, force);
    private static readonly IUncollectSection.Factory UncollectSectionFactory =
        section => new UncollectSection(section);

    private static readonly IDungeonItemSection.Factory Factory =
        (dungeon, accessibilityProvider, autoTrackValue, marking, requirement) => new DungeonItemSection(
            SaveLoadManager, CollectSectionFactory, UncollectSectionFactory, dungeon, accessibilityProvider,
            autoTrackValue, marking, requirement);

    private static readonly IDungeon Dungeon = Substitute.For<IDungeon>();
    private static readonly IDungeonAccessibilityProvider AccessibilityProvider =
        Substitute.For<IDungeonAccessibilityProvider>();
    private static readonly IAutoTrackValue AutoTrackValue = Substitute.For<IAutoTrackValue>();

    private static readonly Dictionary<LocationID, ExpectedObject> ExpectedValues = new();

    private readonly DungeonItemSectionFactory _sut = new(SmallKeyShuffleRequirements, MarkingFactory, Factory);

    private static void PopulateExpectedValues()
    {
        ExpectedValues.Clear();

        foreach (LocationID id in Enum.GetValues(typeof(LocationID)))
        {
            switch (id)
            {
                case LocationID.HyruleCastle:
                case LocationID.EasternPalace:
                case LocationID.TowerOfHera:
                case LocationID.PalaceOfDarkness:
                case LocationID.SwampPalace:
                case LocationID.SkullWoods:
                case LocationID.ThievesTown:
                case LocationID.IcePalace:
                case LocationID.MiseryMire:
                case LocationID.TurtleRock:
                    ExpectedValues.Add(id, Factory(
                        Dungeon, AccessibilityProvider, AutoTrackValue).ToExpectedObject());
                    break;
                case LocationID.AgahnimTower:
                    ExpectedValues.Add(id, Factory(
                        Dungeon, AccessibilityProvider, AutoTrackValue,
                        requirement: SmallKeyShuffleRequirements[true]).ToExpectedObject());
                    break;
                case LocationID.DesertPalace:
                case LocationID.GanonsTower:
                    ExpectedValues.Add(id, Factory(
                        Dungeon, AccessibilityProvider, AutoTrackValue, MarkingFactory()).ToExpectedObject());
                    break;
            }
        }
    }

    [Theory]
    [MemberData(nameof(GetDungeonItemSection_ShouldReturnExpectedData))]
    public void GetDungeonItemSection_ShouldReturnExpected(ExpectedObject expected, LocationID id)
    {
        var actual = _sut.GetDungeonItemSection(
            Dungeon, AccessibilityProvider, AutoTrackValue, id);
            
        expected.ShouldEqual(actual);
    }

    public static IEnumerable<object[]> GetDungeonItemSection_ShouldReturnExpectedData()
    {
        PopulateExpectedValues();

        return ExpectedValues.Keys.Select(
            id => new object[] {ExpectedValues[id], id}).ToList();
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut = scope.Resolve<IDungeonItemSectionFactory>();
            
        Assert.NotNull(sut as DungeonItemSectionFactory);
    }
}