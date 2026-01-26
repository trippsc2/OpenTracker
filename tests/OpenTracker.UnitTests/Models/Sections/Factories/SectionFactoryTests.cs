using System;
using NSubstitute;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Sections.Factories;
using Xunit;

namespace OpenTracker.UnitTests.Models.Sections.Factories;

public class SectionFactoryTests
{
    private readonly IDropdownSectionFactory _dropdownSectionFactory = Substitute.For<IDropdownSectionFactory>();
    private readonly IDungeonSectionFactory _dungeonSectionFactory = Substitute.For<IDungeonSectionFactory>();
    private readonly IEntranceSectionFactory _entranceSectionFactory = Substitute.For<IEntranceSectionFactory>();
    private readonly IItemSectionFactory _itemSectionFactory = Substitute.For<IItemSectionFactory>();
    private readonly IShopSectionFactory _shopSectionFactory = Substitute.For<IShopSectionFactory>();
    private readonly ITakeAnySectionFactory _takeAnySectionFactory = Substitute.For<ITakeAnySectionFactory>();

    private readonly ISectionAutoTrackingFactory _autoTrackingFactory =
        Substitute.For<ISectionAutoTrackingFactory>();

    private readonly SectionFactory _sut;

    public SectionFactoryTests()
    {
        _sut = new SectionFactory(
            _dropdownSectionFactory, _dungeonSectionFactory, _entranceSectionFactory, _itemSectionFactory,
            _shopSectionFactory, _takeAnySectionFactory, _autoTrackingFactory);
    }

    [Fact]
    public void GetSections_ShouldThrowException_WhenIDIsUnexpected()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _ = _sut.GetSections((LocationID) int.MaxValue));
    }

    [Theory]
    [InlineData(LocationID.LinksHouse)]
    [InlineData(LocationID.Pedestal)]
    [InlineData(LocationID.LumberjackCave)]
    [InlineData(LocationID.BottleVendor)]
    [InlineData(LocationID.ChickenHouse)]
    [InlineData(LocationID.Tavern)]
    [InlineData(LocationID.SickKid)]
    [InlineData(LocationID.MagicBat)]
    [InlineData(LocationID.RaceGame)]
    [InlineData(LocationID.Library)]
    [InlineData(LocationID.MushroomSpot)]
    [InlineData(LocationID.ForestHideout)]
    [InlineData(LocationID.WitchsHut)]
    [InlineData(LocationID.BonkRocks)]
    [InlineData(LocationID.KingsTomb)]
    [InlineData(LocationID.AginahsCave)]
    [InlineData(LocationID.GroveDiggingSpot)]
    [InlineData(LocationID.MiniMoldormCave)]
    [InlineData(LocationID.IceRodCave)]
    [InlineData(LocationID.Hobo)]
    [InlineData(LocationID.PyramidLedge)]
    [InlineData(LocationID.FatFairy)]
    [InlineData(LocationID.HauntedGrove)]
    [InlineData(LocationID.HypeCave)]
    [InlineData(LocationID.BombosTablet)]
    [InlineData(LocationID.SouthOfGrove)]
    [InlineData(LocationID.DiggingGame)]
    [InlineData(LocationID.WaterfallFairy)]
    [InlineData(LocationID.Catfish)]
    [InlineData(LocationID.GraveyardLedge)]
    [InlineData(LocationID.DesertLedge)]
    [InlineData(LocationID.CShapedHouse)]
    [InlineData(LocationID.TreasureGame)]
    [InlineData(LocationID.BombableShack)]
    [InlineData(LocationID.Blacksmith)]
    [InlineData(LocationID.PurpleChest)]
    [InlineData(LocationID.HammerPegs)]
    [InlineData(LocationID.BumperCave)]
    [InlineData(LocationID.LakeHyliaIsland)]
    [InlineData(LocationID.MireShack)]
    [InlineData(LocationID.CheckerboardCave)]
    [InlineData(LocationID.OldMan)]
    [InlineData(LocationID.EtherTablet)]
    [InlineData(LocationID.SpikeCave)]
    [InlineData(LocationID.SpiralCave)]
    [InlineData(LocationID.SuperBunnyCave)]
    [InlineData(LocationID.FloatingIsland)]
    [InlineData(LocationID.MimicCave)]
    public void GetSections_ShouldCallGetAutoTrackValueOnce(LocationID id)
    {
        _ = _sut.GetSections(id);
            
        _autoTrackingFactory.Received(1).GetAutoTrackValue(id, Arg.Any<int>());
    }

    [Theory]
    [InlineData(LocationID.LinksHouse)]
    [InlineData(LocationID.Pedestal)]
    [InlineData(LocationID.LumberjackCave)]
    [InlineData(LocationID.BottleVendor)]
    [InlineData(LocationID.ChickenHouse)]
    [InlineData(LocationID.Tavern)]
    [InlineData(LocationID.SickKid)]
    [InlineData(LocationID.MagicBat)]
    [InlineData(LocationID.RaceGame)]
    [InlineData(LocationID.Library)]
    [InlineData(LocationID.MushroomSpot)]
    [InlineData(LocationID.ForestHideout)]
    [InlineData(LocationID.WitchsHut)]
    [InlineData(LocationID.BonkRocks)]
    [InlineData(LocationID.KingsTomb)]
    [InlineData(LocationID.AginahsCave)]
    [InlineData(LocationID.GroveDiggingSpot)]
    [InlineData(LocationID.MiniMoldormCave)]
    [InlineData(LocationID.IceRodCave)]
    [InlineData(LocationID.Hobo)]
    [InlineData(LocationID.PyramidLedge)]
    [InlineData(LocationID.FatFairy)]
    [InlineData(LocationID.HauntedGrove)]
    [InlineData(LocationID.HypeCave)]
    [InlineData(LocationID.BombosTablet)]
    [InlineData(LocationID.SouthOfGrove)]
    [InlineData(LocationID.DiggingGame)]
    [InlineData(LocationID.WaterfallFairy)]
    [InlineData(LocationID.Catfish)]
    [InlineData(LocationID.GraveyardLedge)]
    [InlineData(LocationID.DesertLedge)]
    [InlineData(LocationID.CShapedHouse)]
    [InlineData(LocationID.TreasureGame)]
    [InlineData(LocationID.BombableShack)]
    [InlineData(LocationID.Blacksmith)]
    [InlineData(LocationID.PurpleChest)]
    [InlineData(LocationID.HammerPegs)]
    [InlineData(LocationID.BumperCave)]
    [InlineData(LocationID.LakeHyliaIsland)]
    [InlineData(LocationID.MireShack)]
    [InlineData(LocationID.CheckerboardCave)]
    [InlineData(LocationID.OldMan)]
    [InlineData(LocationID.EtherTablet)]
    [InlineData(LocationID.SpikeCave)]
    [InlineData(LocationID.SpiralCave)]
    [InlineData(LocationID.SuperBunnyCave)]
    [InlineData(LocationID.FloatingIsland)]
    [InlineData(LocationID.MimicCave)]
    public void GetSections_ShouldCallGetItemSectionOnce(LocationID id)
    {
        _ = _sut.GetSections(id);
            
        _itemSectionFactory.Received(1).GetItemSection(
            Arg.Any<IAutoTrackValue?>(), id, Arg.Any<int>());
    }
        
    [Theory]
    [InlineData(LocationID.BlindsHouse)]
    [InlineData(LocationID.TheWell)]
    [InlineData(LocationID.CastleSecret)]
    [InlineData(LocationID.SahasrahlasHut)]
    [InlineData(LocationID.Dam)]
    [InlineData(LocationID.ZoraArea)]
    [InlineData(LocationID.SpectacleRock)]
    [InlineData(LocationID.ParadoxCave)]
    [InlineData(LocationID.HookshotCave)]
    public void GetSections_ShouldCallGetAutoTrackValueTwice(LocationID id)
    {
        _ = _sut.GetSections(id);
            
        _autoTrackingFactory.Received(2).GetAutoTrackValue(id, Arg.Any<int>());
    }
        
    [Theory]
    [InlineData(LocationID.BlindsHouse)]
    [InlineData(LocationID.TheWell)]
    [InlineData(LocationID.CastleSecret)]
    [InlineData(LocationID.SahasrahlasHut)]
    [InlineData(LocationID.Dam)]
    [InlineData(LocationID.ZoraArea)]
    [InlineData(LocationID.SpectacleRock)]
    [InlineData(LocationID.ParadoxCave)]
    [InlineData(LocationID.HookshotCave)]
    public void GetSections_ShouldCallGetItemSectionTwice(LocationID id)
    {
        _ = _sut.GetSections(id);
            
        _itemSectionFactory.Received(2).GetItemSection(
            Arg.Any<IAutoTrackValue?>(), id, Arg.Any<int>());
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
    public void GetSections_ShouldCallGetDungeonSections(LocationID id)
    {
        _ = _sut.GetSections(id);
            
        _dungeonSectionFactory.Received(1).GetDungeonSections(id);
    }
        
    [Theory]
    [InlineData(LocationID.LumberjackHouseEntrance)]
    [InlineData(LocationID.DeathMountainEntryCave)]
    [InlineData(LocationID.DeathMountainExitCave)]
    [InlineData(LocationID.KakarikoFortuneTellerEntrance)]
    [InlineData(LocationID.WomanLeftDoor)]
    [InlineData(LocationID.WomanRightDoor)]
    [InlineData(LocationID.LeftSnitchHouseEntrance)]
    [InlineData(LocationID.RightSnitchHouseEntrance)]
    [InlineData(LocationID.BlindsHouseEntrance)]
    [InlineData(LocationID.ChickenHouseEntrance)]
    [InlineData(LocationID.GrassHouseEntrance)]
    [InlineData(LocationID.TavernFront)]
    [InlineData(LocationID.KakarikoShopEntrance)]
    [InlineData(LocationID.BombHutEntrance)]
    [InlineData(LocationID.SickKidEntrance)]
    [InlineData(LocationID.BlacksmithHouse)]
    [InlineData(LocationID.ChestGameEntrance)]
    [InlineData(LocationID.RaceHouseLeft)]
    [InlineData(LocationID.RaceHouseRight)]
    [InlineData(LocationID.LibraryEntrance)]
    [InlineData(LocationID.ForestChestGameEntrance)]
    [InlineData(LocationID.CastleMainEntrance)]
    [InlineData(LocationID.CastleLeftEntrance)]
    [InlineData(LocationID.CastleRightEntrance)]
    [InlineData(LocationID.CastleTowerEntrance)]
    [InlineData(LocationID.DamEntrance)]
    [InlineData(LocationID.CentralBonkRocksEntrance)]
    [InlineData(LocationID.WitchsHutEntrance)]
    [InlineData(LocationID.WaterfallFairyEntrance)]
    [InlineData(LocationID.SahasrahlasHutEntrance)]
    [InlineData(LocationID.TreesFairyCaveEntrance)]
    [InlineData(LocationID.PegsFairyCaveEntrance)]
    [InlineData(LocationID.EasternPalaceEntrance)]
    [InlineData(LocationID.NorthBonkRocks)]
    [InlineData(LocationID.KingsTombEntrance)]
    [InlineData(LocationID.GraveyardLedgeEntrance)]
    [InlineData(LocationID.DesertLeftEntrance)]
    [InlineData(LocationID.DesertBackEntrance)]
    [InlineData(LocationID.DesertRightEntrance)]
    [InlineData(LocationID.DesertFrontEntrance)]
    [InlineData(LocationID.AginahsCaveEntrance)]
    [InlineData(LocationID.ThiefCaveEntrance)]
    [InlineData(LocationID.RupeeCaveEntrance)]
    [InlineData(LocationID.SkullWoodsBack)]
    [InlineData(LocationID.ThievesTownEntrance)]
    [InlineData(LocationID.CShapedHouseEntrance)]
    [InlineData(LocationID.HammerHouse)]
    [InlineData(LocationID.DarkVillageFortuneTellerEntrance)]
    [InlineData(LocationID.DarkChapelEntrance)]
    [InlineData(LocationID.ShieldShop)]
    [InlineData(LocationID.DarkLumberjack)]
    [InlineData(LocationID.TreasureGameEntrance)]
    [InlineData(LocationID.BombableShackEntrance)]
    [InlineData(LocationID.HammerPegsEntrance)]
    [InlineData(LocationID.BumperCaveExit)]
    [InlineData(LocationID.BumperCaveEntrance)]
    [InlineData(LocationID.HypeCaveEntrance)]
    [InlineData(LocationID.SwampPalaceEntrance)]
    [InlineData(LocationID.DarkCentralBonkRocksEntrance)]
    [InlineData(LocationID.SouthOfGroveEntrance)]
    [InlineData(LocationID.BombShop)]
    [InlineData(LocationID.ArrowGameEntrance)]
    [InlineData(LocationID.DarkHyliaFortuneTeller)]
    [InlineData(LocationID.DarkTreesFairyCaveEntrance)]
    [InlineData(LocationID.DarkSahasrahlaEntrance)]
    [InlineData(LocationID.PalaceOfDarknessEntrance)]
    [InlineData(LocationID.DarkWitchsHut)]
    [InlineData(LocationID.DarkFluteSpotFiveEntrance)]
    [InlineData(LocationID.FatFairyEntrance)]
    [InlineData(LocationID.DarkIceRodCaveEntrance)]
    [InlineData(LocationID.DarkFakeIceRodCaveEntrance)]
    [InlineData(LocationID.DarkIceRodRockEntrance)]
    [InlineData(LocationID.HypeFairyCaveEntrance)]
    [InlineData(LocationID.FortuneTellerEntrance)]
    [InlineData(LocationID.LakeShop)]
    [InlineData(LocationID.UpgradeFairy)]
    [InlineData(LocationID.MiniMoldormCaveEntrance)]
    [InlineData(LocationID.IceRodCaveEntrance)]
    [InlineData(LocationID.IceBeeCaveEntrance)]
    [InlineData(LocationID.IceFairyCaveEntrance)]
    [InlineData(LocationID.IcePalaceEntrance)]
    [InlineData(LocationID.MiseryMireEntrance)]
    [InlineData(LocationID.MireShackEntrance)]
    [InlineData(LocationID.MireRightShackEntrance)]
    [InlineData(LocationID.MireCaveEntrance)]
    [InlineData(LocationID.CheckerboardCaveEntrance)]
    [InlineData(LocationID.DeathMountainEntranceBack)]
    [InlineData(LocationID.OldManResidence)]
    [InlineData(LocationID.OldManBackResidence)]
    [InlineData(LocationID.DeathMountainExitFront)]
    [InlineData(LocationID.SpectacleRockLeft)]
    [InlineData(LocationID.SpectacleRockRight)]
    [InlineData(LocationID.SpectacleRockTop)]
    [InlineData(LocationID.SpikeCaveEntrance)]
    [InlineData(LocationID.DarkMountainFairyEntrance)]
    [InlineData(LocationID.TowerOfHeraEntrance)]
    [InlineData(LocationID.SpiralCaveBottom)]
    [InlineData(LocationID.EDMFairyCaveEntrance)]
    [InlineData(LocationID.ParadoxCaveMiddle)]
    [InlineData(LocationID.ParadoxCaveBottom)]
    [InlineData(LocationID.EDMConnectorBottom)]
    [InlineData(LocationID.SpiralCaveTop)]
    [InlineData(LocationID.MimicCaveEntrance)]
    [InlineData(LocationID.EDMConnectorTop)]
    [InlineData(LocationID.ParadoxCaveTop)]
    [InlineData(LocationID.SuperBunnyCaveBottom)]
    [InlineData(LocationID.DeathMountainShopEntrance)]
    [InlineData(LocationID.SuperBunnyCaveTop)]
    [InlineData(LocationID.HookshotCaveEntrance)]
    [InlineData(LocationID.TurtleRockEntrance)]
    [InlineData(LocationID.GanonsTowerEntrance)]
    [InlineData(LocationID.TRLedgeLeft)]
    [InlineData(LocationID.TRLedgeRight)]
    [InlineData(LocationID.TRSafetyDoor)]
    [InlineData(LocationID.HookshotCaveTop)]
    [InlineData(LocationID.LinksHouseEntrance)] 
    [InlineData(LocationID.LumberjackCaveExit)]
    [InlineData(LocationID.TheWellExit)]
    [InlineData(LocationID.MagicBatExit)]
    [InlineData(LocationID.ForestHideoutExit)]
    [InlineData(LocationID.CastleSecretExit)]
    [InlineData(LocationID.HoulihanHoleExit)]
    [InlineData(LocationID.Sanctuary)]
    [InlineData(LocationID.GanonHoleExit)]
    [InlineData(LocationID.SkullWoodsWestEntrance)]
    [InlineData(LocationID.SkullWoodsCenterEntrance)]
    [InlineData(LocationID.SkullWoodsEastEntrance)]
    [InlineData(LocationID.SkullWoodsNWHole)]
    [InlineData(LocationID.SkullWoodsSWHole)]
    [InlineData(LocationID.SkullWoodsSEHole)]
    [InlineData(LocationID.SkullWoodsNEHole)]
    public void GetSections_ShouldCallGetEntranceSection(LocationID id)
    {
        _ = _sut.GetSections(id);

        _entranceSectionFactory.Received(1).GetEntranceSection(id);
    }
        
    [Theory]
    [InlineData(LocationID.LumberjackCaveEntrance)]
    [InlineData(LocationID.TheWellEntrance)]
    [InlineData(LocationID.MagicBatEntrance)]
    [InlineData(LocationID.ForestHideoutEntrance)]
    [InlineData(LocationID.CastleSecretEntrance)]
    [InlineData(LocationID.HoulihanHole)]
    [InlineData(LocationID.SanctuaryGrave)]
    [InlineData(LocationID.GanonHole)]
    public void GetSections_ShouldCallGetDropdownSection(LocationID id)
    {
        _ = _sut.GetSections(id);

        _dropdownSectionFactory.Received(1).GetDropdownSection(id);
    }
        
    [Theory]
    [InlineData(LocationID.TreesFairyCaveTakeAny)]
    [InlineData(LocationID.PegsFairyCaveTakeAny)]
    [InlineData(LocationID.KakarikoFortuneTellerTakeAny)]
    [InlineData(LocationID.GrassHouseTakeAny)]
    [InlineData(LocationID.ForestChestGameTakeAny)]
    [InlineData(LocationID.LumberjackHouseTakeAny)]
    [InlineData(LocationID.LeftSnitchHouseTakeAny)]
    [InlineData(LocationID.RightSnitchHouseTakeAny)]
    [InlineData(LocationID.BombHutTakeAny)]
    [InlineData(LocationID.IceFairyCaveTakeAny)]
    [InlineData(LocationID.RupeeCaveTakeAny)]
    [InlineData(LocationID.CentralBonkRocksTakeAny)]
    [InlineData(LocationID.ThiefCaveTakeAny)]
    [InlineData(LocationID.IceBeeCaveTakeAny)]
    [InlineData(LocationID.FortuneTellerTakeAny)]
    [InlineData(LocationID.HypeFairyCaveTakeAny)]
    [InlineData(LocationID.ChestGameTakeAny)]
    [InlineData(LocationID.EDMFairyCaveTakeAny)]
    [InlineData(LocationID.DarkChapelTakeAny)]
    [InlineData(LocationID.DarkVillageFortuneTellerTakeAny)]
    [InlineData(LocationID.DarkTreesFairyCaveTakeAny)]
    [InlineData(LocationID.DarkSahasrahlaTakeAny)]
    [InlineData(LocationID.DarkFluteSpotFiveTakeAny)]
    [InlineData(LocationID.ArrowGameTakeAny)]
    [InlineData(LocationID.DarkCentralBonkRocksTakeAny)]
    [InlineData(LocationID.DarkIceRodCaveTakeAny)]
    [InlineData(LocationID.DarkFakeIceRodCaveTakeAny)]
    [InlineData(LocationID.DarkIceRodRockTakeAny)]
    [InlineData(LocationID.DarkMountainFairyTakeAny)]
    [InlineData(LocationID.MireRightShackTakeAny)]
    [InlineData(LocationID.MireCaveTakeAny)]
    public void GetSections_ShouldCallGetTakeAnySection(LocationID id)
    {
        _ = _sut.GetSections(id);

        _takeAnySectionFactory.Received(1).GetTakeAnySection(id);
    }
        
    [Theory]
    [InlineData(LocationID.KakarikoShop)]
    [InlineData(LocationID.DeathMountainShop)]
    [InlineData(LocationID.LakeHyliaShop)]
    [InlineData(LocationID.PotionShop)]
    [InlineData(LocationID.DarkLumberjackShop)]
    [InlineData(LocationID.RedShieldShop)]
    [InlineData(LocationID.VillageOfOutcastsShop)]
    [InlineData(LocationID.DarkLakeHyliaShop)]
    [InlineData(LocationID.DarkPotionShop)]
    [InlineData(LocationID.DarkDeathMountainShop)]
    public void GetSections_ShouldCallGetShopSection(LocationID id)
    {
        _ = _sut.GetSections(id);

        _shopSectionFactory.Received(1).GetShopSection(id);
    }
}