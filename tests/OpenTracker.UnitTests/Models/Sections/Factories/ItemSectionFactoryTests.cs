using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Autofac;
using ExpectedObjects;
using NSubstitute;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Nodes.Factories;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Alternative;
using OpenTracker.Models.Requirements.Mode;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Sections.Factories;
using OpenTracker.Models.Sections.Item;
using OpenTracker.Models.UndoRedo.Sections;
using Xunit;

namespace OpenTracker.UnitTests.Models.Sections.Factories;

[ExcludeFromCodeCoverage]
public sealed class ItemSectionFactoryTests
{
    private static readonly IMode Mode = Substitute.For<IMode>();
    
    private static readonly IOverworldNodeDictionary OverworldNodes = new OverworldNodeDictionary(
        () => Substitute.For<IOverworldNodeFactory>());

    private static readonly IAlternativeRequirementDictionary AlternativeRequirements =
        new AlternativeRequirementDictionary(requirements =>
            new AlternativeRequirement(requirements));

    private static readonly IEntranceShuffleRequirementDictionary EntranceShuffleRequirements =
        new EntranceShuffleRequirementDictionary(expectedValue => 
            new EntranceShuffleRequirement(Mode, expectedValue));

    private static readonly IWorldStateRequirementDictionary WorldStateRequirements =
        new WorldStateRequirementDictionary(expectedValue =>
            new WorldStateRequirement(Mode, expectedValue));

    private static readonly ISaveLoadManager SaveLoadManager = Substitute.For<ISaveLoadManager>();

    private static readonly ICollectSection.Factory CollectSectionFactory = (section, force) =>
        new CollectSection(section, force);

    private static readonly IUncollectSection.Factory UncollectSectionFactory = section =>
        new UncollectSection(section);

    private static readonly IItemSection.Factory Factory =
        (name, node, total, autoTrackValue, marking, requirement, visibleNode) => new ItemSection(
            SaveLoadManager, CollectSectionFactory, UncollectSectionFactory, name, node, total, autoTrackValue,
            marking, requirement, visibleNode);

    private static readonly IMarking.Factory MarkingFactory = () => Substitute.For<IMarking>();

    private static readonly IAutoTrackValue AutoTrackValue = Substitute.For<IAutoTrackValue>();

    private static readonly Dictionary<(LocationID id, int index), ExpectedObject> ExpectedValues = new();

    private readonly ItemSectionFactory _sut = new(
        OverworldNodes, AlternativeRequirements, EntranceShuffleRequirements, WorldStateRequirements,
        Factory, MarkingFactory);

    private static void PopulateExpectedValues()
    {
        ExpectedValues.Clear();

        foreach (var id in Enum.GetValues<LocationID>())
        {
            for (var i = 0; i < 2; i++)
            {
                var visibleNode = GetVisibleNode(id, i);
                var marking = visibleNode is null ? null : MarkingFactory();

                try
                {
                    ExpectedValues.Add((id, i), Factory(
                        GetName(id, i), GetNode(id, i), GetTotal(id, i), AutoTrackValue, marking,
                        GetRequirement(id, i), visibleNode).ToExpectedObject());
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }
    }

    private static string GetName(LocationID id, int index)
    {
        switch (id)
        {
            case LocationID.LinksHouse:
                return "By The Door";
            case LocationID.Pedestal:
                return "Pedestal";
            case LocationID.BlindsHouse when index == 0:
                return "Main";
            case LocationID.BlindsHouse:
            case LocationID.TheWell when index == 1:
                return "Bomb";
            case LocationID.BottleVendor:
                return "Man";
            case LocationID.ChickenHouse:
                return "Bombable Wall";
            case LocationID.Tavern:
            case LocationID.SahasrahlasHut when index == 0:
                return "Back Room";
            case LocationID.SickKid:
                return "By The Bed";
            case LocationID.MagicBat:
                return "Magic Bowl";
            case LocationID.RaceGame:
                return "Take This Trash";
            case LocationID.Library:
                return "On The Shelf";
            case LocationID.MushroomSpot:
                return "Shroom";
            case LocationID.ForestHideout:
                return "Hideout";
            case LocationID.CastleSecret when index == 0:
                return "Uncle";
            case LocationID.CastleSecret:
                return "Hallway";
            case LocationID.WitchsHut:
                return "Assistant";
            case LocationID.SahasrahlasHut:
                return "Saha";
            case LocationID.KingsTomb:
                return "The Crypt";
            case LocationID.GroveDiggingSpot:
                return "Hidden Treasure";
            case LocationID.Dam when index == 0:
                return "Inside";
            case LocationID.Dam:
                return "Outside";
            case LocationID.Hobo:
                return "Under The Bridge";
            case LocationID.PyramidLedge:
            case LocationID.ZoraArea when index == 0:
            case LocationID.DesertLedge:
            case LocationID.BumperCave:
                return "Ledge";
            case LocationID.FatFairy:
                return "Big Bomb Spot";
            case LocationID.HauntedGrove:
                return "Stumpy";
            case LocationID.BombosTablet:
            case LocationID.EtherTablet:
                return "Tablet";
            case LocationID.SouthOfGrove:
                return "Circle of Bushes";
            case LocationID.DiggingGame:
                return "Dig For Treasure";
            case LocationID.WaterfallFairy:
                return "Waterfall Cave";
            case LocationID.ZoraArea:
                return "King Zora";
            case LocationID.Catfish:
                return "Ring of Stones";
            case LocationID.CShapedHouse:
                return "House";
            case LocationID.TreasureGame:
                return "Prize";
            case LocationID.BombableShack:
                return "Downstairs";
            case LocationID.Blacksmith:
                return "Bring Frog Home";
            case LocationID.PurpleChest:
                return "Gary";
            case LocationID.LakeHyliaIsland:
            case LocationID.FloatingIsland:
                return "Island";
            case LocationID.MireShack:
                return "Shack";
            case LocationID.OldMan:
                return "Old Man";
            case LocationID.SpectacleRock when index == 0:
            case LocationID.ParadoxCave when index == 1:
                return "Top";
            case LocationID.ParadoxCave:
                return "Bottom";
            case LocationID.HookshotCave when index == 0:
                return "Bonkable Chest";
            case LocationID.HookshotCave:
                return "Back";
            default:
                return "Cave";
        }
    }

    private static INode GetNode(LocationID id, int index)
    {
        switch (id)
        {
            case LocationID.LinksHouse:
                return OverworldNodes[OverworldNodeID.Start];
            case LocationID.Pedestal:
                return OverworldNodes[OverworldNodeID.Pedestal];
            case LocationID.LumberjackCave:
                return OverworldNodes[OverworldNodeID.LumberjackCaveHole];
            case LocationID.BlindsHouse when index == 0:
            case LocationID.Tavern:
            case LocationID.Dam when index == 0:
                return OverworldNodes[OverworldNodeID.LightWorldNotBunnyOrSuperBunnyMirror];
            case LocationID.BlindsHouse:
            case LocationID.TheWell when index == 1:
            case LocationID.ChickenHouse:
            case LocationID.MushroomSpot:
            case LocationID.ForestHideout:
            case LocationID.CastleSecret:
            case LocationID.SahasrahlasHut when index == 0:
            case LocationID.AginahsCave:
            case LocationID.Dam:
            case LocationID.MiniMoldormCave:
            case LocationID.IceRodCave:
                return OverworldNodes[OverworldNodeID.LightWorldNotBunny];
            case LocationID.TheWell when index == 0:
                return OverworldNodes[OverworldNodeID.LightWorldNotBunnyOrSuperBunnyFallInHole];
            case LocationID.BottleVendor:
                return OverworldNodes[OverworldNodeID.LightWorld];
            case LocationID.SickKid:
                return OverworldNodes[OverworldNodeID.SickKid];
            case LocationID.MagicBat:
                return OverworldNodes[OverworldNodeID.MagicBat];
            case LocationID.RaceGame:
                return OverworldNodes[OverworldNodeID.RaceGameLedgeNotBunny];
            case LocationID.Library:
            case LocationID.BonkRocks:
                return OverworldNodes[OverworldNodeID.LightWorldDash];
            case LocationID.WitchsHut:
                return OverworldNodes[OverworldNodeID.WitchsHut];
            case LocationID.SahasrahlasHut:
                return OverworldNodes[OverworldNodeID.Sahasrahla];
            case LocationID.KingsTomb:
                return OverworldNodes[OverworldNodeID.KingsTombGrave];
            case LocationID.GroveDiggingSpot:
                return OverworldNodes[OverworldNodeID.GroveDiggingSpot];
            case LocationID.Hobo:
                return OverworldNodes[OverworldNodeID.Hobo];
            case LocationID.PyramidLedge:
                return OverworldNodes[OverworldNodeID.DarkWorldEast];
            case LocationID.FatFairy:
                return OverworldNodes[OverworldNodeID.BigBombToWall];
            case LocationID.HauntedGrove:
                return OverworldNodes[OverworldNodeID.DarkWorldSouthNotBunny];
            case LocationID.HypeCave:
            case LocationID.DiggingGame:
                return OverworldNodes[OverworldNodeID.DarkWorldSouthNotBunny];
            case LocationID.BombosTablet:
                return OverworldNodes[OverworldNodeID.BombosTablet];
            case LocationID.SouthOfGrove:
                return OverworldNodes[OverworldNodeID.SouthOfGrove];
            case LocationID.WaterfallFairy:
                return OverworldNodes[OverworldNodeID.WaterfallFairy];
            case LocationID.ZoraArea when index == 0:
                return OverworldNodes[OverworldNodeID.ZoraLedge];
            case LocationID.ZoraArea:
                return OverworldNodes[OverworldNodeID.ZoraArea];
            case LocationID.Catfish:
                return OverworldNodes[OverworldNodeID.CatfishArea];
            case LocationID.GraveyardLedge:
                return OverworldNodes[OverworldNodeID.LWGraveyardLedge];
            case LocationID.DesertLedge:
                return OverworldNodes[OverworldNodeID.DesertLedge];
            case LocationID.CShapedHouse:
            case LocationID.TreasureGame:
                return OverworldNodes[OverworldNodeID.DarkWorldWestNotBunnyOrSuperBunnyMirror];
            case LocationID.BombableShack:
                return OverworldNodes[OverworldNodeID.DarkWorldWestNotBunny];
            case LocationID.Blacksmith:
                return OverworldNodes[OverworldNodeID.Blacksmith];
            case LocationID.PurpleChest:
                return OverworldNodes[OverworldNodeID.PurpleChest];
            case LocationID.HammerPegs:
                return OverworldNodes[OverworldNodeID.HammerPegs];
            case LocationID.BumperCave:
                return OverworldNodes[OverworldNodeID.BumperCaveTop];
            case LocationID.LakeHyliaIsland:
                return OverworldNodes[OverworldNodeID.LakeHyliaIsland];
            case LocationID.MireShack:
                return OverworldNodes[OverworldNodeID.MireAreaNotBunnyOrSuperBunnyMirror];
            case LocationID.CheckerboardCave:
                return OverworldNodes[OverworldNodeID.CheckerboardCave];
            case LocationID.OldMan:
                return OverworldNodes[OverworldNodeID.DeathMountainEntryCaveDark];
            case LocationID.SpectacleRock when index == 0:
                return OverworldNodes[OverworldNodeID.SpectacleRockTop];
            case LocationID.SpectacleRock:
                return OverworldNodes[OverworldNodeID.DeathMountainWestBottom];
            case LocationID.EtherTablet:
                return OverworldNodes[OverworldNodeID.EtherTablet];
            case LocationID.SpikeCave:
                return OverworldNodes[OverworldNodeID.SpikeCaveChest];
            case LocationID.SpiralCave:
                return OverworldNodes[OverworldNodeID.SpiralCave];
            case LocationID.ParadoxCave when index == 0:
                return OverworldNodes[OverworldNodeID.ParadoxCaveNotBunny];
            case LocationID.ParadoxCave:
                return OverworldNodes[OverworldNodeID.ParadoxCaveTop];
            case LocationID.SuperBunnyCave:
                return OverworldNodes[OverworldNodeID.SuperBunnyCaveChests];
            case LocationID.HookshotCave when index == 0:
                return OverworldNodes[OverworldNodeID.HookshotCaveBonkableChest];
            case LocationID.HookshotCave:
                return OverworldNodes[OverworldNodeID.HookshotCaveBack];
            case LocationID.FloatingIsland:
                return OverworldNodes[OverworldNodeID.LWFloatingIsland];
            case LocationID.MimicCave:
                return OverworldNodes[OverworldNodeID.MimicCave];
            default:
                return OverworldNodes[OverworldNodeID.Inaccessible];
        }
    }

    private static int GetTotal(LocationID id, int index)
    {
        switch (id)
        {
            case LocationID.BlindsHouse when index == 0:
            case LocationID.TheWell when index == 0:
                return 4;
            case LocationID.SahasrahlasHut when index == 0:
            case LocationID.HookshotCave when index == 1:
                return 3;
            case LocationID.MiniMoldormCave:
            case LocationID.HypeCave:
            case LocationID.ParadoxCave when index == 1:
                return 5;
            case LocationID.FatFairy:
            case LocationID.WaterfallFairy:
            case LocationID.ParadoxCave:
            case LocationID.SuperBunnyCave:
            case LocationID.MireShack:
                return 2;
            default:
                return 1;
        }
    }

    private static INode? GetVisibleNode(LocationID id, int index)
    {
        switch (id)
        {
            case LocationID.Pedestal:
                return OverworldNodes[OverworldNodeID.LightWorldBook];
            case LocationID.LumberjackCave:
            case LocationID.RaceGame:
            case LocationID.Library:
            case LocationID.MushroomSpot:
            case LocationID.ForestHideout:
            case LocationID.DesertLedge:
            case LocationID.LakeHyliaIsland:
            case LocationID.LumberjackCaveEntrance:
            case LocationID.TheWellEntrance:
            case LocationID.MagicBatEntrance:
            case LocationID.ForestHideoutEntrance:
            case LocationID.HoulihanHole:
            case LocationID.SanctuaryGrave:
                return OverworldNodes[OverworldNodeID.LightWorld];
            case LocationID.BombosTablet:
                return OverworldNodes[OverworldNodeID.BombosTabletLedgeBook];
            case LocationID.ZoraArea when index == 0:
                return OverworldNodes[OverworldNodeID.ZoraArea];
            case LocationID.BumperCave:
                return OverworldNodes[OverworldNodeID.DarkWorldWest];
            case LocationID.SpectacleRock when index == 0:
                return OverworldNodes[OverworldNodeID.DeathMountainWestBottom];
            case LocationID.EtherTablet:
                return OverworldNodes[OverworldNodeID.DeathMountainWestTopBook];
            case LocationID.FloatingIsland:
                return OverworldNodes[OverworldNodeID.DeathMountainEastTop];
            case LocationID.CastleSecretEntrance:
                return OverworldNodes[OverworldNodeID.CastleSecretExitArea];
            case LocationID.GanonHole:
                return OverworldNodes[OverworldNodeID.LightWorldInverted];
            default:
                return null;
        }
    }

    private static IRequirement? GetRequirement(LocationID id, int index)
    {
        switch (id)
        {
            case LocationID.LinksHouse:
                return AlternativeRequirements[new HashSet<IRequirement>
                {
                    WorldStateRequirements[WorldState.StandardOpen],
                    AlternativeRequirements[new HashSet<IRequirement>
                    {
                        EntranceShuffleRequirements[EntranceShuffle.None],
                        EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                    }]
                }];
            case LocationID.Pedestal:
            case LocationID.BottleVendor:
            case LocationID.Tavern:
            case LocationID.RaceGame:
            case LocationID.MushroomSpot:
            case LocationID.GroveDiggingSpot:
            case LocationID.Dam when index == 1:
            case LocationID.Hobo:
            case LocationID.PyramidLedge:
            case LocationID.HauntedGrove:
            case LocationID.BombosTablet:
            case LocationID.DiggingGame:
            case LocationID.ZoraArea:
            case LocationID.Catfish:
            case LocationID.DesertLedge:
            case LocationID.Blacksmith:
            case LocationID.PurpleChest:
            case LocationID.BumperCave:
            case LocationID.LakeHyliaIsland:
            case LocationID.OldMan:
            case LocationID.SpectacleRock when index == 0:
            case LocationID.EtherTablet:
            case LocationID.FloatingIsland:
                return null;
            case LocationID.LumberjackCave:
            case LocationID.BlindsHouse:
            case LocationID.TheWell:
            case LocationID.ChickenHouse:
            case LocationID.SickKid:
            case LocationID.MagicBat:
            case LocationID.Library:
            case LocationID.ForestHideout:
            case LocationID.CastleSecret:
            case LocationID.WitchsHut:
            case LocationID.SahasrahlasHut:
            case LocationID.BonkRocks:
            case LocationID.KingsTomb:
            case LocationID.AginahsCave:
            case LocationID.Dam:
            case LocationID.MiniMoldormCave:
            case LocationID.IceRodCave:
            case LocationID.FatFairy:
            case LocationID.HypeCave:
            case LocationID.SouthOfGrove:
            case LocationID.WaterfallFairy:
            case LocationID.GraveyardLedge:
            case LocationID.CShapedHouse:
            case LocationID.TreasureGame:
            case LocationID.BombableShack:
            case LocationID.HammerPegs:
            case LocationID.MireShack:
            case LocationID.CheckerboardCave:
            case LocationID.SpectacleRock:
            case LocationID.SpikeCave:
            case LocationID.SpiralCave:
            case LocationID.ParadoxCave:
            case LocationID.SuperBunnyCave:
            case LocationID.HookshotCave:
            case LocationID.MimicCave:
                return AlternativeRequirements[new HashSet<IRequirement>
                {
                    EntranceShuffleRequirements[EntranceShuffle.None],
                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                }];
            default:
                throw new ArgumentOutOfRangeException(nameof(id), id, null);
        }
    }

    [Fact]
    public void GetItemSection_ShouldThrowException_WhenIDIsUnexpected()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            _ = _sut.GetItemSection(AutoTrackValue, (LocationID) int.MaxValue));
    }

    [Theory]
    [MemberData(nameof(GetItemSection_ShouldReturnExpectedData))]
    public void GetItemSection_ShouldReturnExpected(ExpectedObject expected, LocationID id, int index)
    {
        expected.ShouldEqual(_sut.GetItemSection(AutoTrackValue, id, index));
    }

    public static IEnumerable<object[]> GetItemSection_ShouldReturnExpectedData()
    {
        PopulateExpectedValues();

        return ExpectedValues.Keys.Select(key => new object[] {ExpectedValues[key], key.id, key.index}).ToList();
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut = scope.Resolve<IItemSectionFactory>();
            
        Assert.NotNull(sut as ItemSectionFactory);
    }
}