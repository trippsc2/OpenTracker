using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Autofac;
using ExpectedObjects;
using NSubstitute;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Nodes.Factories;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Aggregate;
using OpenTracker.Models.Requirements.Alternative;
using OpenTracker.Models.Requirements.Mode;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Sections.Entrance;
using OpenTracker.Models.Sections.Factories;
using OpenTracker.Models.UndoRedo.Sections;
using Xunit;

namespace OpenTracker.UnitTests.Models.Sections.Factories;

[ExcludeFromCodeCoverage]
public sealed class EntranceSectionFactoryTests
{
    private static readonly IMode Mode = Substitute.For<IMode>();
    
    private static readonly IAggregateRequirementDictionary AggregateRequirements = 
        new AggregateRequirementDictionary(requirements => 
            new AggregateRequirement(requirements));
    private static readonly IAlternativeRequirementDictionary AlternativeRequirements =
        new AlternativeRequirementDictionary(requirements => 
            new AlternativeRequirement(requirements));
    private static readonly IEntranceShuffleRequirementDictionary EntranceShuffleRequirements =
        new EntranceShuffleRequirementDictionary(expectedValue =>
            new EntranceShuffleRequirement(Mode, expectedValue));
    private static readonly IWorldStateRequirementDictionary WorldStateRequirements =
        new WorldStateRequirementDictionary(expectedValue =>
            new WorldStateRequirement(Mode, expectedValue));

    private static readonly IOverworldNodeFactory OverworldNodeFactory = Substitute.For<IOverworldNodeFactory>();
    private static readonly IOverworldNodeDictionary OverworldNodes = new OverworldNodeDictionary(() =>
        OverworldNodeFactory);

    private static readonly ISaveLoadManager SaveLoadManager = Substitute.For<ISaveLoadManager>();

    private static readonly ICollectSection.Factory CollectSectionFactory =
        (_, _) => Substitute.For<ICollectSection>();
    private static readonly IUncollectSection.Factory UncollectSectionFactory =
        _ => Substitute.For<IUncollectSection>();

    private static readonly IMarking.Factory MarkingFactory = () => Substitute.For<IMarking>();
        
    private static readonly IEntranceSection.Factory Factory =
        (name, entranceShuffleLevel, requirement, node, exitProvided) => new EntranceSection(
            SaveLoadManager, CollectSectionFactory, UncollectSectionFactory, MarkingFactory, name,
            entranceShuffleLevel, requirement, node, exitProvided);

    private static readonly Dictionary<LocationID, ExpectedObject> ExpectedValues = new();

    private readonly EntranceSectionFactory _sut = new EntranceSectionFactory(
        AggregateRequirements, AlternativeRequirements, EntranceShuffleRequirements, WorldStateRequirements,
        OverworldNodes, Factory);

    static EntranceSectionFactoryTests()
    {
        OverworldNodeFactory.GetOverworldNode(Arg.Any<OverworldNodeID>())
            .Returns(_ => Substitute.For<IOverworldNode>());
    }
        
    // TODO - Switch this method to using explicit expected objects.
    private static void PopulateExpectedValues()
    {
        ExpectedValues.Clear();

        foreach (LocationID id in Enum.GetValues(typeof(LocationID)))
        {
            var entranceShuffleLevel = GetEntranceShuffleLevel(id);
                
            switch (id)
            {
                case LocationID.LumberjackHouseEntrance:
                case LocationID.DeathMountainEntryCave:
                case LocationID.DeathMountainExitCave:
                case LocationID.KakarikoFortuneTellerEntrance:
                case LocationID.WomanLeftDoor:
                case LocationID.WomanRightDoor:
                case LocationID.LeftSnitchHouseEntrance:
                case LocationID.RightSnitchHouseEntrance:
                case LocationID.BlindsHouseEntrance:
                case LocationID.ChickenHouseEntrance:
                case LocationID.GrassHouseEntrance:
                case LocationID.TavernFront:
                case LocationID.KakarikoShopEntrance:
                case LocationID.BombHutEntrance:
                case LocationID.SickKidEntrance:
                case LocationID.BlacksmithHouse:
                case LocationID.ChestGameEntrance:
                case LocationID.RaceHouseLeft:
                case LocationID.RaceHouseRight:
                case LocationID.LibraryEntrance:
                case LocationID.ForestChestGameEntrance:
                case LocationID.CastleMainEntrance:
                case LocationID.CastleLeftEntrance:
                case LocationID.CastleRightEntrance:
                case LocationID.CastleTowerEntrance:
                case LocationID.DamEntrance:
                case LocationID.CentralBonkRocksEntrance:
                case LocationID.WitchsHutEntrance:
                case LocationID.WaterfallFairyEntrance:
                case LocationID.SahasrahlasHutEntrance:
                case LocationID.TreesFairyCaveEntrance:
                case LocationID.PegsFairyCaveEntrance:
                case LocationID.EasternPalaceEntrance:
                case LocationID.NorthBonkRocks:
                case LocationID.KingsTombEntrance:
                case LocationID.GraveyardLedgeEntrance:
                case LocationID.DesertLeftEntrance:
                case LocationID.DesertBackEntrance:
                case LocationID.DesertRightEntrance:
                case LocationID.DesertFrontEntrance:
                case LocationID.AginahsCaveEntrance:
                case LocationID.ThiefCaveEntrance:
                case LocationID.RupeeCaveEntrance:
                case LocationID.SkullWoodsBack:
                case LocationID.ThievesTownEntrance:
                case LocationID.CShapedHouseEntrance:
                case LocationID.HammerHouse:
                case LocationID.DarkVillageFortuneTellerEntrance:
                case LocationID.DarkChapelEntrance:
                case LocationID.ShieldShop:
                case LocationID.DarkLumberjack:
                case LocationID.TreasureGameEntrance:
                case LocationID.BombableShackEntrance:
                case LocationID.HammerPegsEntrance:
                case LocationID.BumperCaveExit:
                case LocationID.BumperCaveEntrance:
                case LocationID.HypeCaveEntrance:
                case LocationID.SwampPalaceEntrance:
                case LocationID.DarkCentralBonkRocksEntrance:
                case LocationID.SouthOfGroveEntrance:
                case LocationID.BombShop:
                case LocationID.ArrowGameEntrance:
                case LocationID.DarkHyliaFortuneTeller:
                case LocationID.DarkTreesFairyCaveEntrance:
                case LocationID.DarkSahasrahlaEntrance:
                case LocationID.PalaceOfDarknessEntrance:
                case LocationID.DarkWitchsHut:
                case LocationID.DarkFluteSpotFiveEntrance:
                case LocationID.FatFairyEntrance:
                case LocationID.DarkIceRodCaveEntrance:
                case LocationID.DarkFakeIceRodCaveEntrance:
                case LocationID.DarkIceRodRockEntrance:
                case LocationID.HypeFairyCaveEntrance:
                case LocationID.FortuneTellerEntrance:
                case LocationID.LakeShop:
                case LocationID.UpgradeFairy:
                case LocationID.MiniMoldormCaveEntrance:
                case LocationID.IceRodCaveEntrance:
                case LocationID.IceBeeCaveEntrance:
                case LocationID.IceFairyCaveEntrance:
                case LocationID.IcePalaceEntrance:
                case LocationID.MiseryMireEntrance:
                case LocationID.MireRightShackEntrance:
                case LocationID.MireCaveEntrance:
                case LocationID.CheckerboardCaveEntrance:
                case LocationID.DeathMountainEntranceBack:
                case LocationID.OldManResidence:
                case LocationID.OldManBackResidence:
                case LocationID.DeathMountainExitFront:
                case LocationID.SpectacleRockLeft:
                case LocationID.SpectacleRockRight:
                case LocationID.SpectacleRockTop:
                case LocationID.SpikeCaveEntrance:
                case LocationID.DarkMountainFairyEntrance:
                case LocationID.TowerOfHeraEntrance:
                case LocationID.SpiralCaveBottom:
                case LocationID.EDMFairyCaveEntrance:
                case LocationID.ParadoxCaveMiddle:
                case LocationID.ParadoxCaveBottom:
                case LocationID.EDMConnectorBottom:
                case LocationID.SpiralCaveTop:
                case LocationID.MimicCaveEntrance:
                case LocationID.EDMConnectorTop:
                case LocationID.ParadoxCaveTop:
                case LocationID.SuperBunnyCaveBottom:
                case LocationID.DeathMountainShopEntrance:
                case LocationID.SuperBunnyCaveTop:
                case LocationID.HookshotCaveEntrance:
                case LocationID.TurtleRockEntrance:
                case LocationID.GanonsTowerEntrance:
                case LocationID.TRLedgeLeft:
                case LocationID.TRLedgeRight:
                case LocationID.TRSafetyDoor:
                case LocationID.HookshotCaveTop:
                case LocationID.LinksHouseEntrance:
                case LocationID.LumberjackCaveExit:
                case LocationID.TheWellExit:
                case LocationID.MagicBatExit:
                case LocationID.ForestHideoutExit:
                case LocationID.CastleSecretExit:
                case LocationID.HoulihanHoleExit:
                case LocationID.Sanctuary:
                case LocationID.GanonHoleExit:
                case LocationID.SkullWoodsWestEntrance:
                case LocationID.SkullWoodsCenterEntrance:
                case LocationID.SkullWoodsEastEntrance:
                case LocationID.SkullWoodsNWHole:
                case LocationID.SkullWoodsSWHole:
                case LocationID.SkullWoodsSEHole:
                case LocationID.SkullWoodsNEHole:
                    ExpectedValues.Add(id, Factory(
                        GetName(id), entranceShuffleLevel, GetRequirement(id, entranceShuffleLevel), GetNode(id),
                        GetExitProvided(id)).ToExpectedObject());
                    break;
            }
        }
    }
        
    private static string GetName(LocationID id)
    {
        switch (id)
        {
            case LocationID.LumberjackHouseEntrance:
            case LocationID.KakarikoFortuneTellerEntrance:
            case LocationID.WomanLeftDoor:
            case LocationID.WomanRightDoor:
            case LocationID.LeftSnitchHouseEntrance:
            case LocationID.RightSnitchHouseEntrance:
            case LocationID.BlindsHouseEntrance:
            case LocationID.ChickenHouseEntrance:
            case LocationID.GrassHouseEntrance:
            case LocationID.TavernFront:
            case LocationID.KakarikoShopEntrance:
            case LocationID.BombHutEntrance:
            case LocationID.SickKidEntrance:
            case LocationID.BlacksmithHouse:
            case LocationID.ChestGameEntrance:
            case LocationID.RaceHouseLeft:
            case LocationID.RaceHouseRight:
            case LocationID.LibraryEntrance:
            case LocationID.WitchsHutEntrance:
            case LocationID.SahasrahlasHutEntrance:
            case LocationID.CShapedHouseEntrance:
            case LocationID.HammerHouse:
            case LocationID.DarkVillageFortuneTellerEntrance:
            case LocationID.ShieldShop:
            case LocationID.DarkLumberjack:
            case LocationID.TreasureGameEntrance:
            case LocationID.BombableShackEntrance:
            case LocationID.HammerPegsEntrance:
            case LocationID.BombShop:
            case LocationID.ArrowGameEntrance:
            case LocationID.DarkHyliaFortuneTeller:
            case LocationID.DarkSahasrahlaEntrance:
            case LocationID.DarkWitchsHut:
            case LocationID.FortuneTellerEntrance:
            case LocationID.LakeShop:
            case LocationID.MireShackEntrance:
            case LocationID.MireRightShackEntrance:
            case LocationID.LinksHouseEntrance:
                return "House";
            case LocationID.ForestChestGameEntrance:
                return "Log";
            case LocationID.CastleMainEntrance:
            case LocationID.CastleLeftEntrance:
            case LocationID.CastleRightEntrance:
            case LocationID.CastleTowerEntrance:
            case LocationID.EasternPalaceEntrance:
            case LocationID.SanctuaryGrave:
            case LocationID.DesertLeftEntrance:
            case LocationID.DesertBackEntrance:
            case LocationID.DesertRightEntrance:
            case LocationID.DesertFrontEntrance:
            case LocationID.SkullWoodsBack:
            case LocationID.ThievesTownEntrance:
            case LocationID.SwampPalaceEntrance:
            case LocationID.PalaceOfDarknessEntrance:
            case LocationID.IcePalaceEntrance:
            case LocationID.MiseryMireEntrance:
            case LocationID.TowerOfHeraEntrance:
            case LocationID.TurtleRockEntrance:
            case LocationID.GanonsTowerEntrance:
                return "Dungeon";
            case LocationID.DamEntrance:
                return "Dam";
            case LocationID.WaterfallFairyEntrance:
                return "Waterfall Cave";
            case LocationID.CentralBonkRocksEntrance:
            case LocationID.NorthBonkRocks:
            case LocationID.DarkCentralBonkRocksEntrance:
                return "Cave Under Rocks";
            case LocationID.KingsTombEntrance:
                return "Crypt";
            case LocationID.FatFairyEntrance:
                return "Big Bomb";
            case LocationID.ForestHideoutExit:
                return "Stump";
            case LocationID.CastleSecretExit:
                return "Secret Passage";
            case LocationID.Sanctuary:
                return "Sanc";
            case LocationID.LumberjackCaveExit:
            case LocationID.TheWellExit:
            case LocationID.MagicBatExit:
            case LocationID.HoulihanHoleExit:
            case LocationID.GanonHoleExit:
            case LocationID.SkullWoodsWestEntrance:
            case LocationID.SkullWoodsCenterEntrance:
            case LocationID.SkullWoodsEastEntrance:
                return "Skull";
            case LocationID.SkullWoodsNWHole:
            case LocationID.SkullWoodsSWHole:
            case LocationID.SkullWoodsSEHole:
            case LocationID.SkullWoodsNEHole:
                return "Dropdown";
            default:
                return "Cave";
        }
    }

    private static EntranceShuffle GetEntranceShuffleLevel(LocationID id)
    {
        switch (id)
        {
            case LocationID.LumberjackHouseEntrance:
            case LocationID.DeathMountainEntryCave:
            case LocationID.DeathMountainExitCave:
            case LocationID.KakarikoFortuneTellerEntrance:
            case LocationID.WomanLeftDoor:
            case LocationID.WomanRightDoor:
            case LocationID.LeftSnitchHouseEntrance:
            case LocationID.RightSnitchHouseEntrance:
            case LocationID.BlindsHouseEntrance:
            case LocationID.ChickenHouseEntrance:
            case LocationID.GrassHouseEntrance:
            case LocationID.TavernFront:
            case LocationID.KakarikoShopEntrance:
            case LocationID.BombHutEntrance:
            case LocationID.SickKidEntrance:
            case LocationID.BlacksmithHouse:
            case LocationID.ChestGameEntrance:
            case LocationID.RaceHouseLeft:
            case LocationID.RaceHouseRight:
            case LocationID.LibraryEntrance:
            case LocationID.ForestChestGameEntrance:
            case LocationID.DamEntrance:
            case LocationID.CentralBonkRocksEntrance:
            case LocationID.WitchsHutEntrance:
            case LocationID.WaterfallFairyEntrance:
            case LocationID.SahasrahlasHutEntrance:
            case LocationID.TreesFairyCaveEntrance:
            case LocationID.PegsFairyCaveEntrance:
            case LocationID.NorthBonkRocks:
            case LocationID.KingsTombEntrance:
            case LocationID.GraveyardLedgeEntrance:
            case LocationID.AginahsCaveEntrance:
            case LocationID.ThiefCaveEntrance:
            case LocationID.RupeeCaveEntrance:
            case LocationID.CShapedHouseEntrance:
            case LocationID.HammerHouse:
            case LocationID.DarkVillageFortuneTellerEntrance:
            case LocationID.DarkChapelEntrance:
            case LocationID.ShieldShop:
            case LocationID.DarkLumberjack:
            case LocationID.TreasureGameEntrance:
            case LocationID.BombableShackEntrance:
            case LocationID.HammerPegsEntrance:
            case LocationID.BumperCaveExit:
            case LocationID.BumperCaveEntrance:
            case LocationID.HypeCaveEntrance:
            case LocationID.DarkCentralBonkRocksEntrance:
            case LocationID.SouthOfGroveEntrance:
            case LocationID.BombShop:
            case LocationID.ArrowGameEntrance:
            case LocationID.DarkHyliaFortuneTeller:
            case LocationID.DarkTreesFairyCaveEntrance:
            case LocationID.DarkSahasrahlaEntrance:
            case LocationID.PalaceOfDarknessEntrance:
            case LocationID.DarkWitchsHut:
            case LocationID.DarkFluteSpotFiveEntrance:
            case LocationID.FatFairyEntrance:
            case LocationID.DarkIceRodCaveEntrance:
            case LocationID.DarkFakeIceRodCaveEntrance:
            case LocationID.DarkIceRodRockEntrance:
            case LocationID.HypeFairyCaveEntrance:
            case LocationID.FortuneTellerEntrance:
            case LocationID.LakeShop:
            case LocationID.UpgradeFairy:
            case LocationID.MiniMoldormCaveEntrance:
            case LocationID.IceRodCaveEntrance:
            case LocationID.IceBeeCaveEntrance:
            case LocationID.IceFairyCaveEntrance:
            case LocationID.MireShackEntrance:
            case LocationID.MireRightShackEntrance:
            case LocationID.MireCaveEntrance:
            case LocationID.CheckerboardCaveEntrance:
            case LocationID.DeathMountainEntranceBack:
            case LocationID.OldManResidence:
            case LocationID.OldManBackResidence:
            case LocationID.DeathMountainExitFront:
            case LocationID.SpectacleRockLeft:
            case LocationID.SpectacleRockRight:
            case LocationID.SpectacleRockTop:
            case LocationID.SpikeCaveEntrance:
            case LocationID.DarkMountainFairyEntrance:
            case LocationID.SpiralCaveBottom:
            case LocationID.EDMFairyCaveEntrance:
            case LocationID.ParadoxCaveMiddle:
            case LocationID.ParadoxCaveBottom:
            case LocationID.EDMConnectorBottom:
            case LocationID.SpiralCaveTop:
            case LocationID.MimicCaveEntrance:
            case LocationID.EDMConnectorTop:
            case LocationID.ParadoxCaveTop:
            case LocationID.SuperBunnyCaveBottom:
            case LocationID.DeathMountainShopEntrance:
            case LocationID.SuperBunnyCaveTop:
            case LocationID.HookshotCaveEntrance:
            case LocationID.LinksHouseEntrance:
            case LocationID.HookshotCaveTop:
                return EntranceShuffle.All;
            case LocationID.CastleMainEntrance:
            case LocationID.CastleLeftEntrance:
            case LocationID.CastleRightEntrance:
            case LocationID.CastleTowerEntrance:
            case LocationID.EasternPalaceEntrance:
            case LocationID.DesertLeftEntrance:
            case LocationID.DesertBackEntrance:
            case LocationID.DesertRightEntrance:
            case LocationID.DesertFrontEntrance:
            case LocationID.SkullWoodsBack:
            case LocationID.ThievesTownEntrance:
            case LocationID.SwampPalaceEntrance:
            case LocationID.IcePalaceEntrance:
            case LocationID.MiseryMireEntrance:
            case LocationID.TowerOfHeraEntrance:
            case LocationID.TurtleRockEntrance:
            case LocationID.GanonsTowerEntrance:
            case LocationID.TRLedgeLeft:
            case LocationID.TRLedgeRight:
            case LocationID.TRSafetyDoor:
                return EntranceShuffle.Dungeon;
            case LocationID.LumberjackCaveExit:
            case LocationID.TheWellExit:
            case LocationID.MagicBatExit:
            case LocationID.ForestHideoutExit:
            case LocationID.CastleSecretExit:
            case LocationID.HoulihanHoleExit:
            case LocationID.Sanctuary:
            case LocationID.GanonHoleExit:
            case LocationID.SkullWoodsWestEntrance:
            case LocationID.SkullWoodsCenterEntrance:
            case LocationID.SkullWoodsEastEntrance:
            case LocationID.SkullWoodsNWHole:
            case LocationID.SkullWoodsSWHole:
            case LocationID.SkullWoodsSEHole:
            case LocationID.SkullWoodsNEHole:
                return EntranceShuffle.Insanity;
            default:
                return EntranceShuffle.None;
        }
    }

    private static IRequirement GetRequirement(LocationID id, EntranceShuffle entranceShuffleLevel)
    {
        switch (id)
        {
            case LocationID.LinksHouseEntrance:
                return AggregateRequirements[new HashSet<IRequirement>
                {
                    AlternativeRequirements[new HashSet<IRequirement>
                    {
                        EntranceShuffleRequirements[EntranceShuffle.All],
                        EntranceShuffleRequirements[EntranceShuffle.Insanity]
                    }],
                    WorldStateRequirements[WorldState.Inverted]
                }];
            case LocationID.GanonHoleExit:
                return AggregateRequirements[new HashSet<IRequirement>
                {
                    EntranceShuffleRequirements[EntranceShuffle.Insanity],
                    WorldStateRequirements[WorldState.Inverted]
                }];
        }

        switch (entranceShuffleLevel)
        {
            case EntranceShuffle.Dungeon:
                return AlternativeRequirements[new HashSet<IRequirement>
                {
                    EntranceShuffleRequirements[EntranceShuffle.Dungeon],
                    EntranceShuffleRequirements[EntranceShuffle.All],
                    EntranceShuffleRequirements[EntranceShuffle.Insanity]
                }];
            case EntranceShuffle.All:
                return AlternativeRequirements[new HashSet<IRequirement>
                {
                    EntranceShuffleRequirements[EntranceShuffle.All],
                    EntranceShuffleRequirements[EntranceShuffle.Insanity]
                }];
            case EntranceShuffle.Insanity:
                return EntranceShuffleRequirements[EntranceShuffle.Insanity];
            default:
                throw new ArgumentOutOfRangeException(nameof(entranceShuffleLevel), entranceShuffleLevel, null);
        }
    }

    private static INode GetNode(LocationID id)
    {
        switch (id)
        {
            case LocationID.DeathMountainEntryCave:
                return OverworldNodes[OverworldNodeID.DeathMountainEntry];
            case LocationID.DeathMountainExitCave:
                return OverworldNodes[OverworldNodeID.DeathMountainExit];
            case LocationID.GrassHouseEntrance:
                return OverworldNodes[OverworldNodeID.GrassHouse];
            case LocationID.BombHutEntrance:
                return OverworldNodes[OverworldNodeID.BombHut];
            case LocationID.RaceHouseLeft:
                return OverworldNodes[OverworldNodeID.RaceGameLedge];
            case LocationID.CastleLeftEntrance:
            case LocationID.CastleRightEntrance:
                return OverworldNodes[OverworldNodeID.HyruleCastleTop];
            case LocationID.CastleTowerEntrance:
                return OverworldNodes[OverworldNodeID.AgahnimTowerEntrance];
            case LocationID.CentralBonkRocksEntrance:
            case LocationID.NorthBonkRocks:
                return OverworldNodes[OverworldNodeID.LightWorldDash];
            case LocationID.WitchsHutEntrance:
                return OverworldNodes[OverworldNodeID.LWWitchArea];
            case LocationID.WaterfallFairyEntrance:
                return OverworldNodes[OverworldNodeID.WaterfallFairy];
            case LocationID.KingsTombEntrance:
                return OverworldNodes[OverworldNodeID.KingsTombGrave];
            case LocationID.GraveyardLedgeEntrance:
                return OverworldNodes[OverworldNodeID.LWGraveyardLedge];
            case LocationID.DesertLeftEntrance:
                return OverworldNodes[OverworldNodeID.DesertLedge];
            case LocationID.DesertBackEntrance:
                return OverworldNodes[OverworldNodeID.DesertBack];
            case LocationID.DesertRightEntrance:
                return OverworldNodes[OverworldNodeID.Inaccessible];
            case LocationID.DesertFrontEntrance:
                return OverworldNodes[OverworldNodeID.DesertPalaceFrontEntrance];
            case LocationID.RupeeCaveEntrance:
            case LocationID.IceFairyCaveEntrance:
                return OverworldNodes[OverworldNodeID.LightWorldLift1];
            case LocationID.SkullWoodsBack:
                return OverworldNodes[OverworldNodeID.SkullWoodsBack];
            case LocationID.ThievesTownEntrance:
            case LocationID.BombableShackEntrance:
            case LocationID.SkullWoodsNEHole:
                return OverworldNodes[OverworldNodeID.DarkWorldWestNotBunny];
            case LocationID.CShapedHouseEntrance:
            case LocationID.DarkVillageFortuneTellerEntrance:
            case LocationID.DarkChapelEntrance:
            case LocationID.ShieldShop:
            case LocationID.DarkLumberjack:
            case LocationID.TreasureGameEntrance:
            case LocationID.SkullWoodsCenterEntrance:
            case LocationID.SkullWoodsEastEntrance:
            case LocationID.SkullWoodsSWHole:
            case LocationID.SkullWoodsSEHole:
                return OverworldNodes[OverworldNodeID.DarkWorldWest];
            case LocationID.HammerHouse:
                return OverworldNodes[OverworldNodeID.HammerHouse];
            case LocationID.HammerPegsEntrance:
                return OverworldNodes[OverworldNodeID.HammerPegs];
            case LocationID.BumperCaveExit:
                return OverworldNodes[OverworldNodeID.BumperCaveTop];
            case LocationID.BumperCaveEntrance:
                return OverworldNodes[OverworldNodeID.BumperCaveEntry];
            case LocationID.HypeCaveEntrance:
                return OverworldNodes[OverworldNodeID.DarkWorldSouthNotBunny];
            case LocationID.SwampPalaceEntrance:
            case LocationID.BombShop:
            case LocationID.ArrowGameEntrance:
            case LocationID.DarkHyliaFortuneTeller:
                return OverworldNodes[OverworldNodeID.DarkWorldSouth];
            case LocationID.DarkCentralBonkRocksEntrance:
                return OverworldNodes[OverworldNodeID.DarkWorldSouthDash];
            case LocationID.SouthOfGroveEntrance:
                return OverworldNodes[OverworldNodeID.SouthOfGroveLedge];
            case LocationID.DarkTreesFairyCaveEntrance:
            case LocationID.DarkSahasrahlaEntrance:
            case LocationID.DarkFluteSpotFiveEntrance:
                return OverworldNodes[OverworldNodeID.DarkWorldEast];
            case LocationID.PalaceOfDarknessEntrance:
                return OverworldNodes[OverworldNodeID.DarkWorldEastNotBunny];
            case LocationID.DarkWitchsHut:
                return OverworldNodes[OverworldNodeID.DWWitchArea];
            case LocationID.FatFairyEntrance:
                return OverworldNodes[OverworldNodeID.FatFairyEntrance];
            case LocationID.GanonHole:
                return OverworldNodes[OverworldNodeID.GanonHole];
            case LocationID.DarkIceRodCaveEntrance:
                return OverworldNodes[OverworldNodeID.DarkWorldSouthEastNotBunny];
            case LocationID.DarkFakeIceRodCaveEntrance:
                return OverworldNodes[OverworldNodeID.DarkWorldSouthEast];
            case LocationID.DarkIceRodRockEntrance:
                return OverworldNodes[OverworldNodeID.DarkWorldSouthEastLift1];
            case LocationID.HypeFairyCaveEntrance:
            case LocationID.MiniMoldormCaveEntrance:
            case LocationID.IceRodCaveEntrance:
                return OverworldNodes[OverworldNodeID.LightWorldNotBunny];
            case LocationID.UpgradeFairy:
                return OverworldNodes[OverworldNodeID.LakeHyliaFairyIsland];
            case LocationID.IcePalaceEntrance:
                return OverworldNodes[OverworldNodeID.IcePalaceIsland];
            case LocationID.MiseryMireEntrance:
                return OverworldNodes[OverworldNodeID.MiseryMireEntrance];
            case LocationID.MireShackEntrance:
            case LocationID.MireRightShackEntrance:
            case LocationID.MireCaveEntrance:
                return OverworldNodes[OverworldNodeID.MireArea];
            case LocationID.CheckerboardCaveEntrance:
                return OverworldNodes[OverworldNodeID.CheckerboardCave];
            case LocationID.DeathMountainEntranceBack:
            case LocationID.OldManResidence:
            case LocationID.OldManBackResidence:
            case LocationID.DeathMountainExitFront:
            case LocationID.SpectacleRockLeft:
            case LocationID.SpectacleRockRight:
            case LocationID.SpectacleRockTop:
                return OverworldNodes[OverworldNodeID.DeathMountainWestBottom];
            case LocationID.SpikeCaveEntrance:
            case LocationID.DarkMountainFairyEntrance:
                return OverworldNodes[OverworldNodeID.DarkDeathMountainWestBottom];
            case LocationID.TowerOfHeraEntrance:
                return OverworldNodes[OverworldNodeID.DeathMountainWestTop];
            case LocationID.SpiralCaveBottom:
            case LocationID.EDMFairyCaveEntrance:
            case LocationID.ParadoxCaveMiddle:
            case LocationID.ParadoxCaveBottom:
                return OverworldNodes[OverworldNodeID.DeathMountainEastBottom];
            case LocationID.EDMConnectorBottom:
                return OverworldNodes[OverworldNodeID.DeathMountainEastBottomConnector];
            case LocationID.SpiralCaveTop:
                return OverworldNodes[OverworldNodeID.SpiralCaveLedge];
            case LocationID.MimicCaveEntrance:
                return OverworldNodes[OverworldNodeID.MimicCaveLedge];
            case LocationID.EDMConnectorTop:
                return OverworldNodes[OverworldNodeID.DeathMountainEastTopConnector];
            case LocationID.ParadoxCaveTop:
                return OverworldNodes[OverworldNodeID.DeathMountainEastTop];
            case LocationID.SuperBunnyCaveBottom:
            case LocationID.DeathMountainShopEntrance:
                return OverworldNodes[OverworldNodeID.DarkDeathMountainEastBottom];
            case LocationID.SuperBunnyCaveTop:
                return OverworldNodes[OverworldNodeID.DarkDeathMountainTop];
            case LocationID.HookshotCaveEntrance:
                return OverworldNodes[OverworldNodeID.HookshotCaveEntrance];
            case LocationID.TurtleRockEntrance:
                return OverworldNodes[OverworldNodeID.TurtleRockFrontEntrance];
            case LocationID.GanonsTowerEntrance:
                return OverworldNodes[OverworldNodeID.GanonsTowerEntrance];
            case LocationID.TRLedgeLeft:
            case LocationID.TRLedgeRight:
                return OverworldNodes[OverworldNodeID.TurtleRockTunnel];
            case LocationID.TRSafetyDoor:
                return OverworldNodes[OverworldNodeID.TurtleRockSafetyDoor];
            case LocationID.HookshotCaveTop:
                return OverworldNodes[OverworldNodeID.DWFloatingIsland];
            case LocationID.GanonHoleExit:
                return OverworldNodes[OverworldNodeID.LightWorldInverted];
            case LocationID.SkullWoodsWestEntrance:
            case LocationID.SkullWoodsNWHole:
                return OverworldNodes[OverworldNodeID.SkullWoodsBackArea];
            default:
                return OverworldNodes[OverworldNodeID.LightWorld];
        }
    }

    private static IOverworldNode? GetExitProvided(LocationID id)
    {
        switch (id)
        {
            case LocationID.LumberjackHouseEntrance:
            case LocationID.KakarikoFortuneTellerEntrance:
            case LocationID.WomanLeftDoor:
            case LocationID.WomanRightDoor:
            case LocationID.LeftSnitchHouseEntrance:
            case LocationID.RightSnitchHouseEntrance:
            case LocationID.BlindsHouseEntrance:
            case LocationID.ChickenHouseEntrance:
            case LocationID.TavernFront:
            case LocationID.KakarikoShopEntrance:
            case LocationID.SickKidEntrance:
            case LocationID.BlacksmithHouse:
            case LocationID.ChestGameEntrance:
            case LocationID.RaceHouseRight:
            case LocationID.LibraryEntrance:
            case LocationID.ForestChestGameEntrance:
            case LocationID.CastleMainEntrance:
            case LocationID.DamEntrance:
            case LocationID.CentralBonkRocksEntrance:
            case LocationID.SahasrahlasHutEntrance:
            case LocationID.TreesFairyCaveEntrance:
            case LocationID.PegsFairyCaveEntrance:
            case LocationID.EasternPalaceEntrance:
            case LocationID.NorthBonkRocks:
            case LocationID.DesertRightEntrance:
            case LocationID.AginahsCaveEntrance:
            case LocationID.ThiefCaveEntrance:
            case LocationID.RupeeCaveEntrance:
            case LocationID.HypeFairyCaveEntrance:
            case LocationID.FortuneTellerEntrance:
            case LocationID.LakeShop:
            case LocationID.MiniMoldormCaveEntrance:
            case LocationID.IceRodCaveEntrance:
            case LocationID.IceBeeCaveEntrance:
            case LocationID.IceFairyCaveEntrance:
            case LocationID.LinksHouseEntrance:
            case LocationID.LumberjackCaveExit:
            case LocationID.TheWellExit:
            case LocationID.MagicBatExit:
            case LocationID.ForestHideoutExit:
            case LocationID.HoulihanHoleExit:
            case LocationID.Sanctuary:
            case LocationID.GanonHoleExit:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.LightWorld];
            case LocationID.DeathMountainEntryCave:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.DeathMountainEntry];
            case LocationID.DeathMountainExitCave:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.DeathMountainExit];
            case LocationID.GrassHouseEntrance:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.GrassHouse];
            case LocationID.BombHutEntrance:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.BombHut];
            case LocationID.RaceHouseLeft:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.RaceGameLedge];
            case LocationID.CastleLeftEntrance:
            case LocationID.CastleRightEntrance:
            case LocationID.CastleTowerEntrance:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.HyruleCastleTop];
            case LocationID.WitchsHutEntrance:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.LWWitchArea];
            case LocationID.WaterfallFairyEntrance:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.WaterfallFairy];
            case LocationID.KingsTombEntrance:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.KingsTomb];
            case LocationID.GraveyardLedgeEntrance:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.LWGraveyardLedge];
            case LocationID.DesertLeftEntrance:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.DesertLedge];
            case LocationID.DesertBackEntrance:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.DesertBack];
            case LocationID.SkullWoodsBack:
            case LocationID.SkullWoodsWestEntrance:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.SkullWoodsBackArea];
            case LocationID.HammerHouse:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.HammerHouse];
            case LocationID.ThievesTownEntrance:
            case LocationID.CShapedHouseEntrance:
            case LocationID.DarkVillageFortuneTellerEntrance:
            case LocationID.DarkChapelEntrance:
            case LocationID.ShieldShop:
            case LocationID.DarkLumberjack:
            case LocationID.TreasureGameEntrance:
            case LocationID.BombableShackEntrance:
            case LocationID.SkullWoodsCenterEntrance:
            case LocationID.SkullWoodsEastEntrance:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.DarkWorldWest];
            case LocationID.HammerPegsEntrance:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.HammerPegsArea];
            case LocationID.BumperCaveExit:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.BumperCaveTop];
            case LocationID.BumperCaveEntrance:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.BumperCaveEntry];
            case LocationID.HypeCaveEntrance:
            case LocationID.SwampPalaceEntrance:
            case LocationID.DarkCentralBonkRocksEntrance:
            case LocationID.BombShop:
            case LocationID.ArrowGameEntrance:
            case LocationID.DarkHyliaFortuneTeller:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.DarkWorldSouth];
            case LocationID.SouthOfGroveEntrance:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.SouthOfGroveLedge];
            case LocationID.DarkTreesFairyCaveEntrance:
            case LocationID.DarkSahasrahlaEntrance:
            case LocationID.PalaceOfDarknessEntrance:
            case LocationID.DarkFluteSpotFiveEntrance:
            case LocationID.FatFairyEntrance:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.DarkWorldEast];
            case LocationID.DarkWitchsHut:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.DWWitchArea];
            case LocationID.DarkIceRodCaveEntrance:
            case LocationID.DarkFakeIceRodCaveEntrance:
            case LocationID.DarkIceRodRockEntrance:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.DarkWorldSouthEast];
            case LocationID.UpgradeFairy:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.LakeHyliaFairyIsland];
            case LocationID.IcePalaceEntrance:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.IcePalaceIsland];
            case LocationID.MiseryMireEntrance:
            case LocationID.MireShackEntrance:
            case LocationID.MireRightShackEntrance:
            case LocationID.MireCaveEntrance:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.MireArea];
            case LocationID.CheckerboardCaveEntrance:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.CheckerboardLedge];
            case LocationID.DeathMountainEntranceBack:
            case LocationID.OldManResidence:
            case LocationID.OldManBackResidence:
            case LocationID.DeathMountainExitFront:
            case LocationID.SpectacleRockLeft:
            case LocationID.SpectacleRockRight:
            case LocationID.SpectacleRockTop:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.DeathMountainWestBottom];
            case LocationID.SpikeCaveEntrance:
            case LocationID.DarkMountainFairyEntrance:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.DarkDeathMountainWestBottom];
            case LocationID.TowerOfHeraEntrance:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.DeathMountainWestTop];
            case LocationID.SpiralCaveBottom:
            case LocationID.EDMFairyCaveEntrance:
            case LocationID.ParadoxCaveMiddle:
            case LocationID.ParadoxCaveBottom:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.DeathMountainEastBottom];
            case LocationID.EDMConnectorBottom:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.DeathMountainEastBottomConnector];
            case LocationID.SpiralCaveTop:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.SpiralCaveLedge];
            case LocationID.MimicCaveEntrance:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.MimicCaveLedge];
            case LocationID.EDMConnectorTop:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.DeathMountainEastTopConnector];
            case LocationID.ParadoxCaveTop:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.DeathMountainEastTop];
            case LocationID.SuperBunnyCaveBottom:
            case LocationID.DeathMountainShopEntrance:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.DarkDeathMountainEastBottom];
            case LocationID.SuperBunnyCaveTop:
            case LocationID.HookshotCaveEntrance:
            case LocationID.TurtleRockEntrance:
            case LocationID.GanonsTowerEntrance:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.DarkDeathMountainTop];
            case LocationID.TRLedgeLeft:
            case LocationID.TRLedgeRight:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.TurtleRockTunnel];
            case LocationID.TRSafetyDoor:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.TurtleRockSafetyDoor];
            case LocationID.HookshotCaveTop:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.DWFloatingIsland];
            case LocationID.CastleSecretExit:
                return (IOverworldNode)OverworldNodes[OverworldNodeID.CastleSecretExitArea];
            default:
                return null;
        }
    }

    [Fact]
    public void GetEntranceSection_ShouldThrowException_WhenIDIsUnexpected()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            _ = _sut.GetEntranceSection((LocationID) int.MaxValue));
    }

    [Theory]
    [MemberData(nameof(GetEntranceSection_ShouldReturnExpectedData))]
    public void GetEntranceSection_ShouldReturnExpected(ExpectedObject expected, LocationID id)
    {
        expected.ShouldEqual(_sut.GetEntranceSection(id));
    }

    public static IEnumerable<object[]> GetEntranceSection_ShouldReturnExpectedData()
    {
        PopulateExpectedValues();

        return ExpectedValues.Keys.Select(id => new object[] {ExpectedValues[id], id}).ToList();
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut = scope.Resolve<IEntranceSectionFactory>();
            
        Assert.NotNull(sut as EntranceSectionFactory);
    }
}