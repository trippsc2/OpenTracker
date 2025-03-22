using System;
using System.Collections.Generic;
using OpenTracker.Models.Locations;

namespace OpenTracker.Models.Sections.Factories
{
    /// <summary>
    /// This class contains creation logic for section data.
    /// </summary>
    public class SectionFactory : ISectionFactory
    {
        private readonly IDropdownSectionFactory _dropdownSectionFactory;
        private readonly IDungeonSectionFactory _dungeonSectionFactory;
        private readonly IEntranceSectionFactory _entranceSectionFactory;
        private readonly IItemSectionFactory _itemSectionFactory;
        private readonly IShopSectionFactory _shopSectionFactory;
        private readonly ITakeAnySectionFactory _takeAnySectionFactory;

        private readonly ISectionAutoTrackingFactory _autoTrackingFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dropdownSectionFactory">
        ///     The <see cref="IDropdownSectionFactory"/>.
        /// </param>
        /// <param name="dungeonSectionFactory">
        ///     The <see cref="IDungeonSectionFactory"/>.
        /// </param>
        /// <param name="entranceSectionFactory">
        ///     The <see cref="IEntranceSectionFactory"/>.
        /// </param>
        /// <param name="itemSectionFactory">
        ///     The <see cref="IItemSectionFactory"/>.
        /// </param>
        /// <param name="shopSectionFactory">
        ///     The <see cref="IShopSectionFactory"/>.
        /// </param>
        /// <param name="takeAnySectionFactory">
        ///     The <see cref="ITakeAnySectionFactory"/>.
        /// </param>
        /// <param name="autoTrackingFactory">
        ///     The <see cref="ISectionAutoTrackingFactory"/>.
        /// </param>
        public SectionFactory(
            IDropdownSectionFactory dropdownSectionFactory, IDungeonSectionFactory dungeonSectionFactory,
            IEntranceSectionFactory entranceSectionFactory, IItemSectionFactory itemSectionFactory,
            IShopSectionFactory shopSectionFactory, ITakeAnySectionFactory takeAnySectionFactory,
            ISectionAutoTrackingFactory autoTrackingFactory)
        {
            _dropdownSectionFactory = dropdownSectionFactory;
            _dungeonSectionFactory = dungeonSectionFactory;
            _entranceSectionFactory = entranceSectionFactory;
            _itemSectionFactory = itemSectionFactory;
            _shopSectionFactory = shopSectionFactory;
            _takeAnySectionFactory = takeAnySectionFactory;
            
            _autoTrackingFactory = autoTrackingFactory;
        }

        public List<ISection> GetSections(LocationID id)
        {
            switch (id)
            {
                case LocationID.LinksHouse:
                case LocationID.Pedestal:
                case LocationID.LumberjackCave:
                case LocationID.BottleVendor:
                case LocationID.ChickenHouse:
                case LocationID.Tavern:
                case LocationID.SickKid:
                case LocationID.MagicBat:
                case LocationID.RaceGame:
                case LocationID.Library:
                case LocationID.MushroomSpot:
                case LocationID.ForestHideout:
                case LocationID.WitchsHut:
                case LocationID.BonkRocks:
                case LocationID.KingsTomb:
                case LocationID.AginahsCave:
                case LocationID.GroveDiggingSpot:
                case LocationID.MiniMoldormCave:
                case LocationID.IceRodCave:
                case LocationID.Hobo:
                case LocationID.PyramidLedge:
                case LocationID.FatFairy:
                case LocationID.HauntedGrove:
                case LocationID.HypeCave:
                case LocationID.BombosTablet:
                case LocationID.SouthOfGrove:
                case LocationID.DiggingGame:
                case LocationID.WaterfallFairy:
                case LocationID.Catfish:
                case LocationID.GraveyardLedge:
                case LocationID.DesertLedge:
                case LocationID.CShapedHouse:
                case LocationID.TreasureGame:
                case LocationID.BombableShack:
                case LocationID.Blacksmith:
                case LocationID.PurpleChest:
                case LocationID.HammerPegs:
                case LocationID.BumperCave:
                case LocationID.LakeHyliaIsland:
                case LocationID.MireShack:
                case LocationID.CheckerboardCave:
                case LocationID.OldMan:
                case LocationID.EtherTablet:
                case LocationID.SpikeCave:
                case LocationID.SpiralCave:
                case LocationID.SuperBunnyCave:
                case LocationID.FloatingIsland:
                case LocationID.MimicCave:
                    return new List<ISection>
                    {
                        _itemSectionFactory.GetItemSection(_autoTrackingFactory.GetAutoTrackValue(id), id)
                    };
                case LocationID.BlindsHouse:
                case LocationID.TheWell:
                case LocationID.CastleSecret:
                case LocationID.SahasrahlasHut:
                case LocationID.Dam:
                case LocationID.ZoraArea:
                case LocationID.SpectacleRock:
                case LocationID.ParadoxCave:
                case LocationID.HookshotCave:
                    return new List<ISection>
                    {
                        _itemSectionFactory.GetItemSection(_autoTrackingFactory.GetAutoTrackValue(id), id),
                        _itemSectionFactory.GetItemSection(
                            _autoTrackingFactory.GetAutoTrackValue(id, 1), id, 1)
                    };
                case LocationID.HyruleCastle:
                case LocationID.AgahnimTower:
                case LocationID.EasternPalace:
                case LocationID.DesertPalace:
                case LocationID.TowerOfHera:
                case LocationID.PalaceOfDarkness:
                case LocationID.SwampPalace:
                case LocationID.SkullWoods:
                case LocationID.ThievesTown:
                case LocationID.IcePalace:
                case LocationID.MiseryMire:
                case LocationID.TurtleRock:
                case LocationID.GanonsTower:
                    return _dungeonSectionFactory.GetDungeonSections(id);
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
                    return new List<ISection> {_entranceSectionFactory.GetEntranceSection(id)};
                case LocationID.LumberjackCaveEntrance:
                case LocationID.TheWellEntrance:
                case LocationID.MagicBatEntrance:
                case LocationID.ForestHideoutEntrance:
                case LocationID.CastleSecretEntrance:
                case LocationID.HoulihanHole:
                case LocationID.SanctuaryGrave:
                case LocationID.GanonHole:
                    return new List<ISection> {_dropdownSectionFactory.GetDropdownSection(id)};
                case LocationID.TreesFairyCaveTakeAny:
                case LocationID.PegsFairyCaveTakeAny:
                case LocationID.KakarikoFortuneTellerTakeAny:
                case LocationID.GrassHouseTakeAny:
                case LocationID.ForestChestGameTakeAny:
                case LocationID.LumberjackHouseTakeAny:
                case LocationID.LeftSnitchHouseTakeAny:
                case LocationID.RightSnitchHouseTakeAny:
                case LocationID.BombHutTakeAny:
                case LocationID.IceFairyCaveTakeAny:
                case LocationID.RupeeCaveTakeAny:
                case LocationID.CentralBonkRocksTakeAny:
                case LocationID.ThiefCaveTakeAny:
                case LocationID.IceBeeCaveTakeAny:
                case LocationID.FortuneTellerTakeAny:
                case LocationID.HypeFairyCaveTakeAny:
                case LocationID.ChestGameTakeAny:
                case LocationID.EDMFairyCaveTakeAny:
                case LocationID.DarkChapelTakeAny:
                case LocationID.DarkVillageFortuneTellerTakeAny:
                case LocationID.DarkTreesFairyCaveTakeAny:
                case LocationID.DarkSahasrahlaTakeAny:
                case LocationID.DarkFluteSpotFiveTakeAny:
                case LocationID.ArrowGameTakeAny:
                case LocationID.DarkCentralBonkRocksTakeAny:
                case LocationID.DarkIceRodCaveTakeAny:
                case LocationID.DarkFakeIceRodCaveTakeAny:
                case LocationID.DarkIceRodRockTakeAny:
                case LocationID.DarkMountainFairyTakeAny:
                case LocationID.MireRightShackTakeAny:
                case LocationID.MireCaveTakeAny:
                    return new List<ISection> {_takeAnySectionFactory.GetTakeAnySection(id)};
                case LocationID.KakarikoShop:
                case LocationID.DeathMountainShop:
                case LocationID.LakeHyliaShop:
                case LocationID.PotionShop:
                case LocationID.DarkLumberjackShop:
                case LocationID.RedShieldShop:
                case LocationID.VillageOfOutcastsShop:
                case LocationID.DarkLakeHyliaShop:
                case LocationID.DarkPotionShop:
                case LocationID.DarkDeathMountainShop:
                    return new List<ISection> {_shopSectionFactory.GetShopSection(id)};
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }
    }
}
