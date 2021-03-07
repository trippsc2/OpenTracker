using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Locations;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.Requirements;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This class contains creation logic for section data.
    /// </summary>
    public class SectionFactory : ISectionFactory
    {
        private readonly IBossPlacementDictionary _bossPlacements;
        private readonly IPrizePlacementDictionary _prizePlacements;
        private readonly IRequirementNodeDictionary _requirementNodes;
        private readonly IRequirementDictionary _requirements;
        private readonly ISectionAutoTrackingFactory _autoTrackingFactory;
        private readonly BossSection.Factory _bossFactory;
        private readonly DropdownSection.Factory _dropdownFactory;
        private readonly DungeonEntranceSection.Factory _dungeonEntranceFactory;
        private readonly DungeonItemSection.Factory _dungeonItemFactory;
        private readonly EntranceSection.Factory _entranceFactory;
        private readonly InsanityEntranceSection.Factory _insanityEntranceFactory;
        private readonly ItemSection.Factory _itemFactory;
        private readonly MarkableDungeonItemSection.Factory _markableDungeonItemFactory;
        private readonly PrizeSection.Factory _prizeFactory;
        private readonly ShopSection.Factory _shopFactory;
        private readonly TakeAnySection.Factory _takeAnyFactory;
        private readonly VisibleItemSection.Factory _visibleItemFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bossPlacements">
        /// The boss placement dictionary.
        /// </param>
        /// <param name="prizePlacements">
        /// The prize placement dictionary.
        /// </param>
        /// <param name="requirementNodes">
        /// The requirement node dictionary.
        /// </param>
        /// <param name="requirements">
        /// The requirement dictionary.
        /// </param>
        /// <param name="autoTrackingFactory">
        /// The item autotracking factory.
        /// </param>
        /// <param name="bossFactory">
        /// An Autofac factory for creating boss sections.
        /// </param>
        /// <param name="dropdownFactory">
        /// An Autofac factory for creating dropdown sections.
        /// </param>
        /// <param name="dungeonEntranceFactory">
        /// An Autofac factory for creating dungeon entrance sections.
        /// </param>
        /// <param name="dungeonItemFactory">
        /// An Autofac factory for creating dungeon item sections.
        /// </param>
        /// <param name="entranceFactory">
        /// An Autofac factory for creating entrance sections.
        /// </param>
        /// <param name="insanityEntranceFactory">
        /// An Autofac factory for creating insanity entrance sections.
        /// </param>
        /// <param name="itemFactory">
        /// An Autofac factory for creating item sections.
        /// </param>
        /// <param name="markableDungeonItemFactory">
        /// An Autofac factory for creating markable dungeon item sections.
        /// </param>
        /// <param name="prizeFactory">
        /// An Autofac factory for creating prize sections.
        /// </param>
        /// <param name="shopFactory">
        /// An Autofac factory for creating shop sections.
        /// </param>
        /// <param name="takeAnyFactory">
        /// An Autofac factory for creating take any sections.
        /// </param>
        /// <param name="visibleItemFactory">
        /// An Autofac factory for creating visible item sections.
        /// </param>
        public SectionFactory(
            IBossPlacementDictionary bossPlacements, IPrizePlacementDictionary prizePlacements,
            IRequirementNodeDictionary requirementNodes, IRequirementDictionary requirements,
            ISectionAutoTrackingFactory autoTrackingFactory, BossSection.Factory bossFactory,
            DropdownSection.Factory dropdownFactory,
            DungeonEntranceSection.Factory dungeonEntranceFactory,
            DungeonItemSection.Factory dungeonItemFactory, EntranceSection.Factory entranceFactory,
            InsanityEntranceSection.Factory insanityEntranceFactory,
            ItemSection.Factory itemFactory,
            MarkableDungeonItemSection.Factory markableDungeonItemFactory,
            PrizeSection.Factory prizeFactory, ShopSection.Factory shopFactory,
            TakeAnySection.Factory takeAnyFactory, VisibleItemSection.Factory visibleItemFactory)
        {
            _bossPlacements = bossPlacements;
            _prizePlacements = prizePlacements;
            _requirementNodes = requirementNodes;
            _requirements = requirements;
            _autoTrackingFactory = autoTrackingFactory;
            _bossFactory = bossFactory;
            _dropdownFactory = dropdownFactory;
            _dungeonEntranceFactory = dungeonEntranceFactory;
            _dungeonItemFactory = dungeonItemFactory;
            _entranceFactory = entranceFactory;
            _insanityEntranceFactory = insanityEntranceFactory;
            _itemFactory = itemFactory;
            _markableDungeonItemFactory = markableDungeonItemFactory;
            _prizeFactory = prizeFactory;
            _shopFactory = shopFactory;
            _takeAnyFactory = takeAnyFactory;
            _visibleItemFactory = visibleItemFactory;
        }

        /// <summary>
        /// Returns the name of the specified section by location ID and section index.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <param name="index">
        /// The section index.
        /// </param>
        /// <returns>
        /// A string representing the section name.
        /// </returns>
        private static string GetSectionName(LocationID id, int index = 0)
        {
            switch (id)
            {
                case LocationID.LinksHouse:
                    {
                        return "By The Door";
                    }
                case LocationID.Pedestal:
                    {
                        return "Pedestal";
                    }
                case LocationID.BlindsHouse when index == 0:
                    {
                        return "Main";
                    }
                case LocationID.BlindsHouse when index == 1:
                case LocationID.TheWell when index == 1:
                    {
                        return "Bomb";
                    }
                case LocationID.BottleVendor:
                    {
                        return "Man";
                    }
                case LocationID.ChickenHouse:
                    {
                        return "Bombable Wall";
                    }
                case LocationID.Tavern:
                    {
                        return "Back Room";
                    }
                case LocationID.SickKid:
                    {
                        return "By The Bed";
                    }
                case LocationID.MagicBat:
                    {
                        return "Magic Bowl";
                    }
                case LocationID.RaceGame:
                    {
                        return "Take This Trash";
                    }
                case LocationID.Library:
                    {
                        return "On The Shelf";
                    }
                case LocationID.MushroomSpot:
                    {
                        return "Shroom";
                    }
                case LocationID.ForestHideout:
                    {
                        return "Hideout";
                    }
                case LocationID.CastleSecret when index == 0:
                    {
                        return "Uncle";
                    }
                case LocationID.CastleSecret when index == 1:
                    {
                        return "Hallway";
                    }
                case LocationID.WitchsHut:
                    {
                        return "Assistant";
                    }
                case LocationID.SahasrahlasHut when index == 0:
                    {
                        return "Back Room";
                    }
                case LocationID.SahasrahlasHut when index == 1:
                    {
                        return "Saha";
                    }
                case LocationID.KingsTomb:
                case LocationID.KingsTombEntrance:
                    {
                        return "The Crypt";
                    }
                case LocationID.GroveDiggingSpot:
                    {
                        return "Hidden Treasure";
                    }
                case LocationID.Dam when index == 0:
                    {
                        return "Inside";
                    }
                case LocationID.Dam when index == 1:
                    {
                        return "Outside";
                    }
                case LocationID.Hobo:
                    {
                        return "Under The Bridge";
                    }
                case LocationID.PyramidLedge:
                case LocationID.ZoraArea when index == 0:
                case LocationID.DesertLedge:
                case LocationID.BumperCave:
                    {
                        return "Ledge";
                    }
                case LocationID.FatFairy:
                    {
                        return "Big Bomb Spot";
                    }
                case LocationID.HauntedGrove:
                    {
                        return "Stumpy";
                    }
                case LocationID.BombosTablet:
                case LocationID.EtherTablet:
                    {
                        return "Tablet";
                    }
                case LocationID.SouthOfGrove:
                    {
                        return "Circle of Bushes";
                    }
                case LocationID.DiggingGame:
                    {
                        return "Dig For Treasure";
                    }
                case LocationID.WaterfallFairy:
                case LocationID.WaterfallFairyEntrance:
                    {
                        return "Waterfall Cave";
                    }
                case LocationID.ZoraArea when index == 1:
                    {
                        return "King Zora";
                    }
                case LocationID.Catfish:
                    {
                        return "Ring of Stones";
                    }
                case LocationID.CShapedHouse:
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
                case LocationID.KakarikoShop:
                case LocationID.BombHutEntrance:
                case LocationID.SickKidEntrance:
                case LocationID.BlacksmithHouse:
                case LocationID.ChestGameEntrance:
                case LocationID.RaceHouseLeft:
                case LocationID.RaceHouseRight:
                case LocationID.LibraryEntrance:
                case LocationID.ForestChestGameEntrance:
                case LocationID.WitchsHutEntrance:
                case LocationID.SahasrahlasHutEntrance:
                case LocationID.CShapedHouseEntrance:
                case LocationID.HammerHouse:
                case LocationID.DarkVillageFortuneTellerEntrance:
                case LocationID.ShieldShop:
                case LocationID.DarkLumberjack:
                case LocationID.TreasureGameEntrance:
                case LocationID.BombableShackEntrance:
                case LocationID.BombShop:
                case LocationID.ArrowGameEntrance:
                case LocationID.DarkHyliaFortuneTeller:
                case LocationID.DarkSahasrahlaEntrance:
                case LocationID.DarkWitchsHut:
                case LocationID.LinksHouseEntrance:
                    {
                        return "House";
                    }
                case LocationID.TreasureGame:
                    {
                        return "Prize";
                    }
                case LocationID.BombableShack:
                    {
                        return "Downstairs";
                    }
                case LocationID.Blacksmith:
                    {
                        return "Bring Frog Home";
                    }
                case LocationID.PurpleChest:
                    {
                        return "Gary";
                    }
                case LocationID.LakeHyliaIsland:
                case LocationID.FloatingIsland:
                    {
                        return "Island";
                    }
                case LocationID.MireShack:
                    {
                        return "Shack";
                    }
                case LocationID.OldMan:
                    {
                        return "Old Man";
                    }
                case LocationID.SpectacleRock when index == 0:
                case LocationID.ParadoxCave when index == 1:
                    {
                        return "Top";
                    }
                case LocationID.ParadoxCave when index == 0:
                    {
                        return "Bottom";
                    }
                case LocationID.HookshotCave when index == 0:
                    {
                        return "Bonkable Chest";
                    }
                case LocationID.HookshotCave when index == 1:
                    {
                        return "Back";
                    }
                case LocationID.HyruleCastle:
                case LocationID.AgahnimTower when index == 0:
                case LocationID.EasternPalace when index == 0:
                case LocationID.DesertPalace when index == 0:
                case LocationID.TowerOfHera when index == 0:
                case LocationID.PalaceOfDarkness when index == 0:
                case LocationID.SwampPalace when index == 0:
                case LocationID.SkullWoods when index == 0:
                case LocationID.ThievesTown when index == 0:
                case LocationID.IcePalace when index == 0:
                case LocationID.MiseryMire when index == 0:
                case LocationID.TurtleRock when index == 0:
                case LocationID.GanonsTower when index == 0:
                case LocationID.CastleMainEntrance:
                case LocationID.CastleLeftEntrance:
                case LocationID.CastleRightEntrance:
                case LocationID.CastleTowerEntrance:
                case LocationID.EasternPalaceEntrance:
                case LocationID.SkullWoodsBack:
                case LocationID.ThievesTownEntrance:
                case LocationID.PalaceOfDarknessEntrance:
                case LocationID.IcePalaceEntrance:
                case LocationID.MiseryMireEntrance:
                case LocationID.TowerOfHeraEntrance:
                case LocationID.TurtleRockEntrance:
                case LocationID.GanonsTowerEntrance:
                case LocationID.SkullWoodsWestEntrance:
                case LocationID.SkullWoodsCenterEntrance:
                case LocationID.SkullWoodsEastEntrance:
                    {
                        return "Dungeon";
                    }
                case LocationID.AgahnimTower when index == 1:
                case LocationID.EasternPalace when index == 1:
                case LocationID.DesertPalace when index == 1:
                case LocationID.TowerOfHera when index == 1:
                case LocationID.PalaceOfDarkness when index == 1:
                case LocationID.SwampPalace when index == 1:
                case LocationID.SkullWoods when index == 1:
                case LocationID.ThievesTown when index == 1:
                case LocationID.IcePalace when index == 1:
                case LocationID.MiseryMire when index == 1:
                case LocationID.TurtleRock when index == 1:
                    {
                        return "Boss";
                    }
                case LocationID.GanonsTower when index == 1:
                    {
                        return "Boss 1";
                    }
                case LocationID.GanonsTower when index == 2:
                    {
                        return "Boss 2";
                    }
                case LocationID.GanonsTower when index == 3:
                    {
                        return "Boss 3";
                    }
                case LocationID.GanonsTower when index == 4:
                    {
                        return "Aga 2";
                    }
                case LocationID.LumberjackCaveEntrance:
                case LocationID.TheWellEntrance:
                case LocationID.MagicBatEntrance:
                case LocationID.ForestHideoutEntrance:
                case LocationID.CastleSecretEntrance:
                case LocationID.HoulihanHole:
                case LocationID.SanctuaryGrave:
                case LocationID.GanonHole:
                case LocationID.SkullWoodsNWHole:
                case LocationID.SkullWoodsSWHole:
                case LocationID.SkullWoodsSEHole:
                case LocationID.SkullWoodsNEHole:
                    {
                        return "Dropdown";
                    }
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
                    {
                        return "Take Any";
                    }
                case LocationID.ForestHideoutExit:
                case LocationID.CastleSecretExit:
                case LocationID.GanonHoleExit:
                    {
                        return "Exit";
                    }
                case LocationID.Sanctuary:
                    {
                        return "Sanc";
                    }
                default:
                    {
                        return "Cave";
                    }
            }
        }

        /// <summary>
        /// Returns item section total.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <param name="index">
        /// The section index.
        /// </param>
        /// <returns>
        /// A 32-bit signed integer representing the total items in the section.
        /// </returns>
        private static int GetItemSectionTotal(LocationID id, int index = 0)
        {
            switch (id)
            {
                case LocationID.BlindsHouse when index == 0:
                case LocationID.TheWell when index == 0:
                    {
                        return 4;
                    }
                case LocationID.SahasrahlasHut when index == 0:
                case LocationID.HookshotCave when index == 1:
                    {
                        return 3;
                    }
                case LocationID.MiniMoldormCave:
                case LocationID.HypeCave:
                case LocationID.ParadoxCave when index == 1:
                    {
                        return 5;
                    }
                case LocationID.FatFairy:
                case LocationID.WaterfallFairy:
                case LocationID.ParadoxCave when index == 0:
                case LocationID.SuperBunnyCave:
                case LocationID.MireShack:
                    {
                        return 2;
                    }
                default:
                    {
                        return 1;
                    }
            }
        }

        /// <summary>
        /// Returns the requirement node to which the specified section belongs.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <param name="index">
        /// The section index.
        /// </param>
        /// <returns>
        /// The requirement node to which the section belongs.
        /// </returns>
        private IRequirementNode GetSectionNode(LocationID id, int index = 0)
        {
            switch (id)
            {
                case LocationID.LinksHouse:
                    {
                        return _requirementNodes[RequirementNodeID.Start];
                    }
                case LocationID.Pedestal:
                    {
                        return _requirementNodes[RequirementNodeID.Pedestal];
                    }
                case LocationID.LumberjackCave:
                case LocationID.LumberjackCaveEntrance:
                    {
                        return _requirementNodes[
                            RequirementNodeID.LumberjackCaveHole];
                    }
                case LocationID.BlindsHouse when index == 0:
                case LocationID.Tavern:
                case LocationID.Dam when index == 0:
                    {
                        return _requirementNodes[
                            RequirementNodeID.LightWorldNotBunnyOrSuperBunnyMirror];
                    }
                case LocationID.BlindsHouse when index == 1:
                case LocationID.TheWell when index == 1:
                case LocationID.ChickenHouse:
                case LocationID.MushroomSpot:
                case LocationID.ForestHideout:
                case LocationID.CastleSecret:
                case LocationID.SahasrahlasHut when index == 0:
                case LocationID.AginahsCave:
                case LocationID.Dam when index == 1:
                case LocationID.MiniMoldormCave:
                case LocationID.IceRodCave:
                case LocationID.ForestHideoutEntrance:
                case LocationID.CastleSecretEntrance:
                case LocationID.HoulihanHole:
                case LocationID.HypeFairyCaveEntrance:
                case LocationID.MiniMoldormCaveEntrance:
                case LocationID.IceRodCaveEntrance:
                case LocationID.HypeFairyCaveTakeAny:
                    {
                        return _requirementNodes[
                            RequirementNodeID.LightWorldNotBunny];
                    }
                case LocationID.TheWell when index == 0:
                    {
                        return _requirementNodes[
                            RequirementNodeID.LightWorldNotBunnyOrSuperBunnyFallInHole];
                    }
                case LocationID.BottleVendor:
                case LocationID.LumberjackHouseEntrance:
                case LocationID.KakarikoFortuneTellerEntrance:
                case LocationID.WomanLeftDoor:
                case LocationID.WomanRightDoor:
                case LocationID.LeftSnitchHouseEntrance:
                case LocationID.RightSnitchHouseEntrance:
                case LocationID.BlindsHouseEntrance:
                case LocationID.TheWellEntrance:
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
                case LocationID.SahasrahlasHutEntrance:
                case LocationID.TreesFairyCaveEntrance:
                case LocationID.PegsFairyCaveEntrance:
                case LocationID.EasternPalaceEntrance:
                case LocationID.AginahsCaveEntrance:
                case LocationID.ThiefCaveEntrance:
                case LocationID.FortuneTellerEntrance:
                case LocationID.LakeShop:
                case LocationID.IceBeeCaveEntrance:
                case LocationID.LinksHouseEntrance:
                case LocationID.TreesFairyCaveTakeAny:
                case LocationID.PegsFairyCaveTakeAny:
                case LocationID.KakarikoFortuneTellerTakeAny:
                case LocationID.ForestChestGameTakeAny:
                case LocationID.LumberjackHouseTakeAny:
                case LocationID.LeftSnitchHouseTakeAny:
                case LocationID.RightSnitchHouseTakeAny:
                case LocationID.ThiefCaveTakeAny:
                case LocationID.IceBeeCaveTakeAny:
                case LocationID.FortuneTellerTakeAny:
                case LocationID.ChestGameTakeAny:
                case LocationID.LumberjackCaveExit:
                case LocationID.TheWellExit:
                case LocationID.MagicBatExit:
                case LocationID.ForestHideoutExit:
                case LocationID.HoulihanHoleExit:
                case LocationID.Sanctuary:
                case LocationID.KakarikoShop:
                case LocationID.LakeHyliaShop:
                    {
                        return _requirementNodes[RequirementNodeID.LightWorld];
                    }
                case LocationID.SickKid:
                    {
                        return _requirementNodes[RequirementNodeID.SickKid];
                    }
                case LocationID.MagicBat:
                    {
                        return _requirementNodes[RequirementNodeID.MagicBat];
                    }
                case LocationID.RaceGame:
                    {
                        return _requirementNodes[
                            RequirementNodeID.RaceGameLegdeNotBunny];
                    }
                case LocationID.Library:
                case LocationID.BonkRocks:
                case LocationID.CentralBonkRocksEntrance:
                case LocationID.NorthBonkRocks:
                case LocationID.CentralBonkRocksTakeAny:
                    {
                        return _requirementNodes[
                            RequirementNodeID.LightWorldDash];
                    }
                case LocationID.WitchsHut:
                    {
                        return _requirementNodes[RequirementNodeID.WitchsHut];
                    }
                case LocationID.SahasrahlasHut when index == 1:
                    {
                        return _requirementNodes[RequirementNodeID.Sahasrahla];
                    }
                case LocationID.KingsTomb:
                case LocationID.KingsTombEntrance:
                    {
                        return _requirementNodes[
                            RequirementNodeID.KingsTombGrave];
                    }
                case LocationID.GroveDiggingSpot:
                    {
                        return _requirementNodes[
                            RequirementNodeID.GroveDiggingSpot];
                    }
                case LocationID.Hobo:
                    {
                        return _requirementNodes[RequirementNodeID.Hobo];
                    }
                case LocationID.PyramidLedge:
                    {
                        return _requirementNodes[RequirementNodeID.DarkWorldEast];
                    }
                case LocationID.FatFairy:
                    {
                        return _requirementNodes[RequirementNodeID.BigBombToWall];
                    }
                case LocationID.HauntedGrove:
                case LocationID.HypeCaveEntrance:
                    {
                        return _requirementNodes[
                            RequirementNodeID.DarkWorldSouthNotBunny];
                    }
                case LocationID.HypeCave:
                case LocationID.DiggingGame:
                    {
                        return _requirementNodes[
                            RequirementNodeID.DarkWorldSouthNotBunny];
                    }
                case LocationID.BombosTablet:
                    {
                        return _requirementNodes[RequirementNodeID.BombosTablet];
                    }
                case LocationID.SouthOfGrove:
                    {
                        return _requirementNodes[RequirementNodeID.SouthOfGrove];
                    }
                case LocationID.WaterfallFairy:
                    {
                        return _requirementNodes[RequirementNodeID.WaterfallFairy];
                    }
                case LocationID.ZoraArea when index == 0:
                    {
                        return _requirementNodes[RequirementNodeID.ZoraLedge];
                    }
                case LocationID.ZoraArea when index == 1:
                    {
                        return _requirementNodes[RequirementNodeID.ZoraArea];
                    }
                case LocationID.Catfish:
                    {
                        return _requirementNodes[RequirementNodeID.CatfishArea];
                    }
                case LocationID.GraveyardLedge:
                    {
                        return _requirementNodes[RequirementNodeID.LWGraveyardLedge];
                    }
                case LocationID.DesertLedge:
                    {
                        return _requirementNodes[RequirementNodeID.DesertLedge];
                    }
                case LocationID.CShapedHouse:
                case LocationID.TreasureGame:
                    {
                        return _requirementNodes[
                            RequirementNodeID.DarkWorldWestNotBunnyOrSuperBunnyMirror];
                    }
                case LocationID.BombableShack:
                case LocationID.BombableShackEntrance:
                case LocationID.SkullWoodsNEHole:
                    {
                        return _requirementNodes[
                            RequirementNodeID.DarkWorldWestNotBunny];
                    }
                case LocationID.Blacksmith:
                    {
                        return _requirementNodes[RequirementNodeID.Blacksmith];
                    }
                case LocationID.PurpleChest:
                    {
                        return _requirementNodes[RequirementNodeID.PurpleChest];
                    }
                case LocationID.HammerPegs:
                case LocationID.HammerPegsEntrance:
                    {
                        return _requirementNodes[RequirementNodeID.HammerPegs];
                    }
                case LocationID.BumperCave:
                    {
                        return _requirementNodes[
                            RequirementNodeID.BumperCaveTop];
                    }
                case LocationID.LakeHyliaIsland:
                    {
                        return _requirementNodes[
                            RequirementNodeID.LakeHyliaIsland];
                    }
                case LocationID.MireShack:
                    {
                        return _requirementNodes[
                            RequirementNodeID.MireAreaNotBunnyOrSuperBunnyMirror];
                    }
                case LocationID.CheckerboardCave:
                case LocationID.CheckerboardCaveEntrance:
                    {
                        return _requirementNodes[
                            RequirementNodeID.CheckerboardCave];
                    }
                case LocationID.OldMan:
                    {
                        return _requirementNodes[
                            RequirementNodeID.DeathMountainEntryCaveDark];
                    }
                case LocationID.SpectacleRock when index == 0:
                    {
                        return _requirementNodes[
                            RequirementNodeID.SpectacleRockTop];
                    }
                case LocationID.SpectacleRock when index == 1:
                    {
                        return _requirementNodes[
                            RequirementNodeID.DeathMountainWestBottom];
                    }
                case LocationID.EtherTablet:
                    {
                        return _requirementNodes[RequirementNodeID.EtherTablet];
                    }
                case LocationID.SpikeCave:
                    {
                        return _requirementNodes[
                            RequirementNodeID.SpikeCaveChest];
                    }
                case LocationID.SpiralCave:
                    {
                        return _requirementNodes[RequirementNodeID.SpiralCave];
                    }
                case LocationID.ParadoxCave when index == 0:
                    {
                        return _requirementNodes[
                            RequirementNodeID.ParadoxCaveNotBunny];
                    }
                case LocationID.ParadoxCave:
                    {
                        return _requirementNodes[
                            RequirementNodeID.ParadoxCaveTop];
                    }
                case LocationID.SuperBunnyCave:
                    {
                        return _requirementNodes[
                            RequirementNodeID.SuperBunnyCaveChests];
                    }
                case LocationID.HookshotCave when index == 0:
                    {
                        return _requirementNodes[
                            RequirementNodeID.HookshotCaveBonkableChest];
                    }
                case LocationID.HookshotCave when index == 1:
                    {
                        return _requirementNodes[
                            RequirementNodeID.HookshotCaveBack];
                    }
                case LocationID.FloatingIsland:
                    {
                        return _requirementNodes[
                            RequirementNodeID.LWFloatingIsland];
                    }
                case LocationID.MimicCave:
                    {
                        return _requirementNodes[RequirementNodeID.MimicCave];
                    }
                case LocationID.DeathMountainEntryCave:
                    {
                        return _requirementNodes[
                            RequirementNodeID.DeathMountainEntry];
                    }
                case LocationID.DeathMountainExitCave:
                    {
                        return _requirementNodes[
                            RequirementNodeID.DeathMountainExit];
                    }
                case LocationID.GrassHouseEntrance:
                case LocationID.GrassHouseTakeAny:
                    {
                        return _requirementNodes[RequirementNodeID.GrassHouse];
                    }
                case LocationID.BombHutEntrance:
                case LocationID.BombHutTakeAny:
                    {
                        return _requirementNodes[RequirementNodeID.BombHut];
                    }
                case LocationID.MagicBatEntrance:
                    {
                        return _requirementNodes[
                            RequirementNodeID.MagicBatLedge];
                    }
                case LocationID.RaceHouseLeft:
                    {
                        return _requirementNodes[RequirementNodeID.RaceGameLedge];
                    }
                case LocationID.CastleLeftEntrance:
                case LocationID.CastleRightEntrance:
                    {
                        return _requirementNodes[
                            RequirementNodeID.HyruleCastleTop];
                    }
                case LocationID.CastleTowerEntrance:
                    {
                        return _requirementNodes[
                            RequirementNodeID.AgahnimTowerEntrance];
                    }
                case LocationID.WitchsHutEntrance:
                case LocationID.PotionShop:
                    {
                        return _requirementNodes[RequirementNodeID.LWWitchArea];
                    }
                case LocationID.WaterfallFairyEntrance:
                    {
                        return _requirementNodes[
                            RequirementNodeID.WaterfallFairy];
                    }
                case LocationID.SanctuaryGrave:
                    {
                        return _requirementNodes[
                            RequirementNodeID.EscapeGrave];
                    }
                case LocationID.GraveyardLedgeEntrance:
                    {
                        return _requirementNodes[
                            RequirementNodeID.LWGraveyardLedge];
                    }
                case LocationID.DesertLeftEntrance:
                    {
                        return _requirementNodes[RequirementNodeID.DesertLedge];
                    }
                case LocationID.DesertBackEntrance:
                    {
                        return _requirementNodes[RequirementNodeID.DesertBack];
                    }
                case LocationID.DesertRightEntrance:
                    {
                        return _requirementNodes[RequirementNodeID.Inaccessible];
                    }
                case LocationID.DesertFrontEntrance:
                    {
                        return _requirementNodes[
                            RequirementNodeID.DesertPalaceFrontEntrance];
                    }
                case LocationID.RupeeCaveEntrance:
                case LocationID.IceFairyCaveEntrance:
                case LocationID.IceFairyCaveTakeAny:
                case LocationID.RupeeCaveTakeAny:
                    {
                        return _requirementNodes[
                            RequirementNodeID.LightWorldLift1];
                    }
                case LocationID.SkullWoodsBack:
                    {
                        return _requirementNodes[
                            RequirementNodeID.SkullWoodsBack];
                    }
                case LocationID.ThievesTownEntrance:
                    {
                        return _requirementNodes[
                            RequirementNodeID.DarkWorldWestNotBunny];
                    }
                case LocationID.CShapedHouseEntrance:
                case LocationID.DarkVillageFortuneTellerEntrance:
                case LocationID.DarkChapelEntrance:
                case LocationID.ShieldShop:
                case LocationID.DarkLumberjack:
                case LocationID.TreasureGameEntrance:
                case LocationID.DarkChapelTakeAny:
                case LocationID.DarkVillageFortuneTellerTakeAny:
                case LocationID.SkullWoodsCenterEntrance:
                case LocationID.SkullWoodsEastEntrance:
                case LocationID.SkullWoodsSWHole:
                case LocationID.SkullWoodsSEHole:
                case LocationID.DarkLumberjackShop:
                case LocationID.RedShieldShop:
                    {
                        return _requirementNodes[RequirementNodeID.DarkWorldWest];
                    }
                case LocationID.HammerHouse:
                case LocationID.VillageOfOutcastsShop:
                    {
                        return _requirementNodes[RequirementNodeID.HammerHouse];
                    }
                case LocationID.BumperCaveExit:
                    {
                        return _requirementNodes[RequirementNodeID.BumperCaveTop];
                    }
                case LocationID.BumperCaveEntrance:
                    {
                        return _requirementNodes[
                            RequirementNodeID.BumperCaveEntry];
                    }
                case LocationID.SwampPalaceEntrance:
                case LocationID.BombShop:
                case LocationID.ArrowGameEntrance:
                case LocationID.DarkHyliaFortuneTeller:
                case LocationID.ArrowGameTakeAny:
                case LocationID.DarkLakeHyliaShop:
                    {
                        return _requirementNodes[RequirementNodeID.DarkWorldSouth];
                    }
                case LocationID.DarkCentralBonkRocksEntrance:
                case LocationID.DarkCentralBonkRocksTakeAny:
                    {
                        return _requirementNodes[
                            RequirementNodeID.DarkWorldSouthDash];
                    }
                case LocationID.SouthOfGroveEntrance:
                    {
                        return _requirementNodes[
                            RequirementNodeID.SouthOfGroveLedge];
                    }
                case LocationID.DarkTreesFairyCaveEntrance:
                case LocationID.DarkSahasrahlaEntrance:
                case LocationID.DarkFluteSpotFiveEntrance:
                case LocationID.DarkTreesFairyCaveTakeAny:
                case LocationID.DarkSahasrahlaTakeAny:
                case LocationID.DarkFluteSpotFiveTakeAny:
                    {
                        return _requirementNodes[RequirementNodeID.DarkWorldEast];
                    }
                case LocationID.PalaceOfDarknessEntrance:
                    {
                        return _requirementNodes[
                            RequirementNodeID.DarkWorldEastNotBunny];
                    }
                case LocationID.DarkWitchsHut:
                case LocationID.DarkPotionShop:
                    {
                        return _requirementNodes[RequirementNodeID.DWWitchArea];
                    }
                case LocationID.FatFairyEntrance:
                    {
                        return _requirementNodes[RequirementNodeID.FatFairyEntrance];
                    }
                case LocationID.GanonHole:
                    {
                        return _requirementNodes[RequirementNodeID.GanonHole];
                    }
                case LocationID.DarkIceRodCaveEntrance:
                case LocationID.DarkIceRodCaveTakeAny:
                    {
                        return _requirementNodes[
                            RequirementNodeID.DarkWorldSouthEastNotBunny];
                    }
                case LocationID.DarkFakeIceRodCaveEntrance:
                case LocationID.DarkFakeIceRodCaveTakeAny:
                    {
                        return _requirementNodes[
                            RequirementNodeID.DarkWorldSouthEast];
                    }
                case LocationID.DarkIceRodRockEntrance:
                case LocationID.DarkIceRodRockTakeAny:
                    {
                        return _requirementNodes[
                            RequirementNodeID.DarkWorldSouthEastLift1];
                    }
                case LocationID.UpgradeFairy:
                    {
                        return _requirementNodes[
                            RequirementNodeID.LakeHyliaFairyIsland];
                    }
                case LocationID.IcePalaceEntrance:
                    {
                        return _requirementNodes[
                            RequirementNodeID.IcePalaceIsland];
                    }
                case LocationID.MiseryMireEntrance:
                    {
                        return _requirementNodes[
                            RequirementNodeID.MiseryMireEntrance];
                    }
                case LocationID.MireShackEntrance:
                case LocationID.MireRightShackEntrance:
                case LocationID.MireCaveEntrance:
                case LocationID.MireRightShackTakeAny:
                case LocationID.MireCaveTakeAny:
                    {
                        return _requirementNodes[RequirementNodeID.MireArea];
                    }
                case LocationID.DeathMountainEntranceBack:
                case LocationID.OldManResidence:
                case LocationID.OldManBackResidence:
                case LocationID.DeathMountainExitFront:
                case LocationID.SpectacleRockLeft:
                case LocationID.SpectacleRockRight:
                case LocationID.SpectacleRockTop:
                    {
                        return _requirementNodes[
                            RequirementNodeID.DeathMountainWestBottom];
                    }
                case LocationID.SpikeCaveEntrance:
                case LocationID.DarkMountainFairyEntrance:
                case LocationID.DarkMountainFairyTakeAny:
                    {
                        return _requirementNodes[
                            RequirementNodeID.DarkDeathMountainWestBottom];
                    }
                case LocationID.TowerOfHeraEntrance:
                    {
                        return _requirementNodes[
                            RequirementNodeID.DeathMountainWestTop];
                    }
                case LocationID.SpiralCaveBottom:
                case LocationID.EDMFairyCaveEntrance:
                case LocationID.ParadoxCaveMiddle:
                case LocationID.ParadoxCaveBottom:
                case LocationID.EDMFairyCaveTakeAny:
                case LocationID.DeathMountainShop:
                    {
                        return _requirementNodes[
                            RequirementNodeID.DeathMountainEastBottom];
                    }
                case LocationID.EDMConnectorBottom:
                    {
                        return _requirementNodes[
                            RequirementNodeID.DeathMountainEastBottomConnector];
                    }
                case LocationID.SpiralCaveTop:
                    {
                        {
                            return _requirementNodes[
                                RequirementNodeID.SpiralCaveLedge];
                        }
                    }
                case LocationID.MimicCaveEntrance:
                    {
                        return _requirementNodes[
                            RequirementNodeID.MimicCaveLedge];
                    }
                case LocationID.EDMConnectorTop:
                    {
                        return _requirementNodes[
                            RequirementNodeID.DeathMountainEastTopConnector];
                    }
                case LocationID.ParadoxCaveTop:
                    {
                        return _requirementNodes[
                            RequirementNodeID.DeathMountainEastTop];
                    }
                case LocationID.SuperBunnyCaveBottom:
                case LocationID.DeathMountainShopEntrance:
                case LocationID.DarkDeathMountainShop:
                    {
                        return _requirementNodes[
                            RequirementNodeID.DarkDeathMountainEastBottom];
                    }
                case LocationID.SuperBunnyCaveTop:
                    {
                        return _requirementNodes[
                            RequirementNodeID.DarkDeathMountainTop];
                    }
                case LocationID.HookshotCaveEntrance:
                    {
                        return _requirementNodes[
                            RequirementNodeID.HookshotCaveEntrance];
                    }
                case LocationID.TurtleRockEntrance:
                    {
                        return _requirementNodes[
                            RequirementNodeID.TurtleRockFrontEntrance];
                    }
                case LocationID.GanonsTowerEntrance:
                    {
                        return _requirementNodes[
                            RequirementNodeID.GanonsTowerEntrance];
                    }
                case LocationID.TRLedgeLeft:
                case LocationID.TRLedgeRight:
                    {
                        return _requirementNodes[
                            RequirementNodeID.TurtleRockTunnel];
                    }
                case LocationID.TRSafetyDoor:
                    {
                        return _requirementNodes[
                            RequirementNodeID.TurtleRockSafetyDoor];
                    }
                case LocationID.HookshotCaveTop:
                    {
                        return _requirementNodes[
                            RequirementNodeID.DWFloatingIsland];
                    }
                case LocationID.CastleSecretExit:
                    {
                        return _requirementNodes[RequirementNodeID.CastleSecretExitArea];
                    }
                case LocationID.GanonHoleExit:
                    {
                        return _requirementNodes[RequirementNodeID.LightWorldInverted];
                    }
                case LocationID.SkullWoodsWestEntrance:
                case LocationID.SkullWoodsNWHole:
                    {
                        return _requirementNodes[RequirementNodeID.SkullWoodsBackArea];
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }

        /// <summary>
        /// Returns the requirement node from which the specified section is visible.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <param name="index">
        /// The section index.
        /// </param>
        /// <returns>
        /// The requirement node from which the section is visible.
        /// </returns>
        private IRequirementNode? GetVisibleNode(LocationID id, int index = 0)
        {
            switch (id)
            {
                case LocationID.Pedestal:
                case LocationID.BombosTablet:
                case LocationID.EtherTablet:
                    {
                        return null;
                    }
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
                    {
                        return _requirementNodes[RequirementNodeID.LightWorld];
                    }
                case LocationID.ZoraArea when index == 0:
                    {
                        return _requirementNodes[RequirementNodeID.ZoraArea];
                    }
                case LocationID.BumperCave:
                    {
                        return _requirementNodes[RequirementNodeID.DarkWorldWest];
                    }
                case LocationID.SpectacleRock when index == 0:
                    {
                        return _requirementNodes[
                            RequirementNodeID.DeathMountainWestBottom];
                    }
                case LocationID.FloatingIsland:
                    {
                        return _requirementNodes[
                            RequirementNodeID.DeathMountainEastTop];
                    }
                case LocationID.CastleSecretEntrance:
                    {
                        return _requirementNodes[
                            RequirementNodeID.CastleSecretExitArea];
                    }
                case LocationID.GanonHole:
                    {
                        return _requirementNodes[
                            RequirementNodeID.LightWorldInverted];
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }

        /// <summary>
        /// Returns the requirement for the specified section.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <param name="index">
        /// The section index.
        /// </param>
        /// <returns>
        /// The requirement.
        /// </returns>
        private IRequirement GetSectionRequirement(LocationID id, int index = 0)
        {
            switch (id)
            {
                case LocationID.Dam when index == 0:
                case LocationID.SpectacleRock when index == 1:
                    {
                        return _requirements[RequirementType.EntranceShuffleNoneDungeon];
                    }
                case LocationID.AgahnimTower when index == 0:
                    {
                        return _requirements[RequirementType.SmallKeyShuffleOn];
                    }
                case LocationID.GanonsTower when index > 0 && index < 4:
                    {
                        return _requirements[RequirementType.BossShuffleOn];
                    }
                default:
                    {
                        return _requirements[RequirementType.NoRequirement];
                    }
            }
        }

        /// <summary>
        /// Returns a new item section instance.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <param name="index">
        /// The section index.
        /// </param>
        /// <returns>
        /// A new item section instance.
        /// </returns>
        private IItemSection GetItemSection(LocationID id, int index = 0)
        {
            return _itemFactory(
                GetSectionName(id, index), GetItemSectionTotal(id, index),
                GetSectionNode(id, index),
                _autoTrackingFactory.GetAutoTrackValue(id, index),
                GetSectionRequirement(id, index));
        }

        /// <summary>
        /// Returns a new markable item section.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <param name="index">
        /// The section index.
        /// </param>
        /// <returns>
        /// A new markable item section.
        /// </returns>
        private ISection GetVisibleItemSection(LocationID id, int index = 0)
        {
            return _visibleItemFactory(
                GetSectionName(id, index), GetItemSectionTotal(id, index),
                GetSectionNode(id, index),
                _autoTrackingFactory.GetAutoTrackValue(id, index),
                GetSectionRequirement(id, index), GetVisibleNode(id, index));
        }

        /// <summary>
        /// Returns a new dungeon item section.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <param name="index">
        /// The section index.
        /// </param>
        /// <returns>
        /// A new dungeon item section.
        /// </returns>
        private IDungeonItemSection GetDungeonItemSection(LocationID id, int index = 0)
        {
            return _dungeonItemFactory(
                id, _autoTrackingFactory.GetAutoTrackValue(id, index),
                GetSectionRequirement(id, index));
        }

        /// <summary>
        /// Returns a new markable dungeon item section.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <param name="index">
        /// The section index.
        /// </param>
        /// <returns>
        /// A new markable dungeon item section.
        /// </returns>
        private ISection GetMarkableDungeonItemSection(LocationID id, int index = 0)
        {
            return _markableDungeonItemFactory(
                GetDungeonItemSection(id, index));
        }

        /// <summary>
        /// Returns the boss placement for the specified section.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <param name="index">
        /// The section index.
        /// </param>
        /// <returns>
        /// The boss placement.
        /// </returns>
        private IBossPlacement GetSectionBossPlacement(LocationID id, int index = 0)
        {
            switch (id)
            {
                case LocationID.AgahnimTower:
                    {
                        return _bossPlacements[BossPlacementID.ATBoss];
                    }
                case LocationID.EasternPalace:
                    {
                        return _bossPlacements[BossPlacementID.EPBoss];
                    }
                case LocationID.DesertPalace:
                    {
                        return _bossPlacements[BossPlacementID.DPBoss];
                    }
                case LocationID.TowerOfHera:
                    {
                        return _bossPlacements[BossPlacementID.ToHBoss];
                    }
                case LocationID.PalaceOfDarkness:
                    {
                        return _bossPlacements[BossPlacementID.PoDBoss];
                    }
                case LocationID.SwampPalace:
                    {
                        return _bossPlacements[BossPlacementID.SPBoss];
                    }
                case LocationID.SkullWoods:
                    {
                        return _bossPlacements[BossPlacementID.SWBoss];
                    }
                case LocationID.ThievesTown:
                    {
                        return _bossPlacements[BossPlacementID.TTBoss];
                    }
                case LocationID.IcePalace:
                    {
                        return _bossPlacements[BossPlacementID.IPBoss];
                    }
                case LocationID.MiseryMire:
                    {
                        return _bossPlacements[BossPlacementID.MMBoss];
                    }
                case LocationID.TurtleRock:
                    {
                        return _bossPlacements[BossPlacementID.TRBoss];
                    }
                case LocationID.GanonsTower when index == 1:
                    {
                        return _bossPlacements[BossPlacementID.GTBoss1];
                    }
                case LocationID.GanonsTower when index == 2:
                    {
                        return _bossPlacements[BossPlacementID.GTBoss2];
                    }
                case LocationID.GanonsTower when index == 3:
                    {
                        return _bossPlacements[BossPlacementID.GTBoss3];
                    }
                case LocationID.GanonsTower when index == 4:
                    {
                        return _bossPlacements[BossPlacementID.GTFinalBoss];
                    }
                case LocationID.GanonsTower:
                    {
                        throw new ArgumentOutOfRangeException(nameof(index));
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }

        /// <summary>
        /// Returns the prize placement for the specified section.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <returns>
        /// The prize placement.
        /// </returns>
        private IPrizePlacement GetSectionPrizePlacement(LocationID id)
        {
            switch (id)
            {
                case LocationID.AgahnimTower:
                    {
                        return _prizePlacements[PrizePlacementID.ATPrize];
                    }
                case LocationID.EasternPalace:
                    {
                        return _prizePlacements[PrizePlacementID.EPPrize];
                    }
                case LocationID.DesertPalace:
                    {
                        return _prizePlacements[PrizePlacementID.DPPrize];
                    }
                case LocationID.TowerOfHera:
                    {
                        return _prizePlacements[PrizePlacementID.ToHPrize];
                    }
                case LocationID.PalaceOfDarkness:
                    {
                        return _prizePlacements[PrizePlacementID.PoDPrize];
                    }
                case LocationID.SwampPalace:
                    {
                        return _prizePlacements[PrizePlacementID.SPPrize];
                    }
                case LocationID.SkullWoods:
                    {
                        return _prizePlacements[PrizePlacementID.SWPrize];
                    }
                case LocationID.ThievesTown:
                    {
                        return _prizePlacements[PrizePlacementID.TTPrize];
                    }
                case LocationID.IcePalace:
                    {
                        return _prizePlacements[PrizePlacementID.IPPrize];
                    }
                case LocationID.MiseryMire:
                    {
                        return _prizePlacements[PrizePlacementID.MMPrize];
                    }
                case LocationID.TurtleRock:
                    {
                        return _prizePlacements[PrizePlacementID.TRPrize];
                    }
                case LocationID.GanonsTower:
                    {
                        return _prizePlacements[PrizePlacementID.GTPrize];
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }

        /// <summary>
        /// Returns a new boss section instance for the specified section.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <param name="index">
        /// The section index.
        /// </param>
        /// <returns>
        /// A new boss section instance.
        /// </returns>
        private ISection GetBossSection(LocationID id, int index = 0)
        {
            return _bossFactory(
                GetSectionName(id, index), GetSectionBossPlacement(id, index),
                GetSectionRequirement(id, index));
        }

        /// <summary>
        /// Returns whether the prize section can always be cleared.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <param name="index">
        /// The section index.
        /// </param>
        /// <returns>
        /// A boolean representing whether the prize section can always be cleared.
        /// </returns>
        private static bool GetPrizeSectionAlwaysClearable(LocationID id, int index)
        {
            return id == LocationID.GanonsTower && index == 4;
        }

        /// <summary>
        /// Returns a new prize section instance for the specified section.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <param name="index">
        /// The section index.
        /// </param>
        /// <returns>
        /// A new prize section instance.
        /// </returns>
        private ISection GetPrizeSection(LocationID id, int index = 0)
        {
            return _prizeFactory(
                GetSectionName(id, index), GetSectionBossPlacement(id, index),
                GetSectionPrizePlacement(id),
                _autoTrackingFactory.GetAutoTrackValue(id, index),
                GetSectionRequirement(id, index), GetPrizeSectionAlwaysClearable(id, index));
        }

        /// <summary>
        /// Returns the item provided for the specified entrance section.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <returns>
        /// The item provided.
        /// </returns>
        private IRequirementNode? GetEntranceSectionExitProvided(LocationID id)
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
                case LocationID.SouthOfGroveEntrance:
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
                    {
                        return _requirementNodes[RequirementNodeID.LightWorld];
                    }
                case LocationID.LumberjackCaveEntrance:
                case LocationID.ForestHideoutEntrance:
                case LocationID.TheWellEntrance:
                case LocationID.MagicBatEntrance:
                case LocationID.CastleSecretEntrance:
                case LocationID.HoulihanHole:
                case LocationID.SanctuaryGrave:
                case LocationID.GanonHole:
                case LocationID.SkullWoodsNWHole:
                case LocationID.SkullWoodsSWHole:
                case LocationID.SkullWoodsSEHole:
                case LocationID.SkullWoodsNEHole:
                    {
                        return null;
                    }
                case LocationID.DeathMountainEntryCave:
                    {
                        return _requirementNodes[RequirementNodeID.DeathMountainEntry];
                    }
                case LocationID.DeathMountainExitCave:
                    {
                        return _requirementNodes[RequirementNodeID.DeathMountainExit];
                    }
                case LocationID.BombHutEntrance:
                    {
                        return _requirementNodes[RequirementNodeID.BombHut];
                    }
                case LocationID.RaceHouseLeft:
                    {
                        return _requirementNodes[RequirementNodeID.RaceGameLedge];
                    }
                case LocationID.CastleLeftEntrance:
                case LocationID.CastleRightEntrance:
                case LocationID.CastleTowerEntrance:
                    {
                        return _requirementNodes[RequirementNodeID.HyruleCastleTop];
                    }
                case LocationID.WitchsHutEntrance:
                    {
                        return _requirementNodes[RequirementNodeID.LWWitchArea];
                    }
                case LocationID.WaterfallFairyEntrance:
                    {
                        return _requirementNodes[RequirementNodeID.WaterfallFairy];
                    }
                case LocationID.KingsTombEntrance:
                    {
                        return _requirementNodes[RequirementNodeID.KingsTomb];
                    }
                case LocationID.GraveyardLedgeEntrance:
                    {
                        return _requirementNodes[RequirementNodeID.LWGraveyardLedge];
                    }
                case LocationID.DesertLeftEntrance:
                    {
                        return _requirementNodes[RequirementNodeID.DesertLedge];
                    }
                case LocationID.DesertBackEntrance:
                    {
                        return _requirementNodes[RequirementNodeID.DesertBack];
                    }
                case LocationID.DesertFrontEntrance:
                    {
                        return null;
                    }
                case LocationID.SkullWoodsBack:
                case LocationID.SkullWoodsWestEntrance:
                    {
                        return _requirementNodes[RequirementNodeID.SkullWoodsBackArea];
                    }
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
                    {
                        return _requirementNodes[RequirementNodeID.DarkWorldWest];
                    }
                case LocationID.HammerHouse:
                    {
                        return _requirementNodes[RequirementNodeID.HammerHouse];
                    }
                case LocationID.HammerPegsEntrance:
                    {
                        return _requirementNodes[RequirementNodeID.HammerPegsArea];
                    }
                case LocationID.BumperCaveExit:
                    {
                        return _requirementNodes[RequirementNodeID.BumperCaveTop];
                    }
                case LocationID.BumperCaveEntrance:
                    {
                        return _requirementNodes[RequirementNodeID.BumperCaveEntry];
                    }
                case LocationID.HypeCaveEntrance:
                case LocationID.SwampPalaceEntrance:
                case LocationID.DarkCentralBonkRocksEntrance:
                case LocationID.BombShop:
                case LocationID.ArrowGameEntrance:
                case LocationID.DarkHyliaFortuneTeller:
                    {
                        return _requirementNodes[RequirementNodeID.DarkWorldSouth];
                    }
                case LocationID.DarkTreesFairyCaveEntrance:
                case LocationID.DarkSahasrahlaEntrance:
                case LocationID.PalaceOfDarknessEntrance:
                case LocationID.DarkFluteSpotFiveEntrance:
                case LocationID.FatFairyEntrance:
                    {
                        return _requirementNodes[RequirementNodeID.DarkWorldEast];
                    }
                case LocationID.DarkWitchsHut:
                    {
                        return _requirementNodes[RequirementNodeID.DWWitchArea];
                    }
                case LocationID.DarkIceRodCaveEntrance:
                case LocationID.DarkFakeIceRodCaveEntrance:
                case LocationID.DarkIceRodRockEntrance:
                    {
                        return _requirementNodes[RequirementNodeID.DarkWorldSouthEast];
                    }
                case LocationID.UpgradeFairy:
                    {
                        return _requirementNodes[RequirementNodeID.LakeHyliaFairyIsland];
                    }
                case LocationID.IcePalaceEntrance:
                    {
                        return _requirementNodes[RequirementNodeID.IcePalaceIsland];
                    }
                case LocationID.MiseryMireEntrance:
                case LocationID.MireShackEntrance:
                case LocationID.MireRightShackEntrance:
                case LocationID.MireCaveEntrance:
                    {
                        return _requirementNodes[RequirementNodeID.MireArea];
                    }
                case LocationID.CheckerboardCaveEntrance:
                    {
                        return _requirementNodes[RequirementNodeID.CheckerboardLedge];
                    }
                case LocationID.DeathMountainEntranceBack:
                case LocationID.OldManResidence:
                case LocationID.OldManBackResidence:
                case LocationID.DeathMountainExitFront:
                case LocationID.SpectacleRockLeft:
                case LocationID.SpectacleRockRight:
                case LocationID.SpectacleRockTop:
                    {
                        return _requirementNodes[RequirementNodeID.DeathMountainWestBottom];
                    }
                case LocationID.SpikeCaveEntrance:
                case LocationID.DarkMountainFairyEntrance:
                    {
                        return _requirementNodes[RequirementNodeID.DarkDeathMountainWestBottom];
                    }
                case LocationID.TowerOfHeraEntrance:
                    {
                        return _requirementNodes[RequirementNodeID.DeathMountainWestTop];
                    }
                case LocationID.SpiralCaveBottom:
                case LocationID.EDMFairyCaveEntrance:
                case LocationID.ParadoxCaveMiddle:
                case LocationID.ParadoxCaveBottom:
                    {
                        return _requirementNodes[RequirementNodeID.DeathMountainEastBottom];
                    }
                case LocationID.EDMConnectorBottom:
                    {
                        return _requirementNodes[RequirementNodeID.DeathMountainEastBottomConnector];
                    }
                case LocationID.SpiralCaveTop:
                    {
                        return _requirementNodes[RequirementNodeID.SpiralCaveLedge];
                    }
                case LocationID.MimicCaveEntrance:
                    {
                        return _requirementNodes[RequirementNodeID.MimicCaveLedge];
                    }
                case LocationID.EDMConnectorTop:
                    {
                        return _requirementNodes[RequirementNodeID.DeathMountainEastTopConnector];
                    }
                case LocationID.ParadoxCaveTop:
                    {
                        return _requirementNodes[RequirementNodeID.DeathMountainEastTop];
                    }
                case LocationID.SuperBunnyCaveBottom:
                case LocationID.DeathMountainShopEntrance:
                    {
                        return _requirementNodes[RequirementNodeID.DarkDeathMountainEastBottom];
                    }
                case LocationID.SuperBunnyCaveTop:
                case LocationID.HookshotCaveEntrance:
                case LocationID.TurtleRockEntrance:
                case LocationID.GanonsTowerEntrance:
                    {
                        return _requirementNodes[RequirementNodeID.DarkDeathMountainTop];
                    }
                case LocationID.TRLedgeLeft:
                case LocationID.TRLedgeRight:
                    {
                        return _requirementNodes[RequirementNodeID.TurtleRockTunnel];
                    }
                case LocationID.TRSafetyDoor:
                    {
                        return _requirementNodes[RequirementNodeID.TurtleRockSafetyDoor];
                    }
                case LocationID.HookshotCaveTop:
                    {
                        return _requirementNodes[RequirementNodeID.DWFloatingIsland];
                    }
                case LocationID.CastleSecretExit:
                    {
                        return _requirementNodes[RequirementNodeID.CastleSecretExitArea];
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }

        /// <summary>
        /// Returns a new entrance section instance for the specified section.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <returns>
        /// A new entrance section instance.
        /// </returns>
        private ISection GetEntranceSection(LocationID id)
        {
            return _entranceFactory(
                GetSectionName(id), GetEntranceSectionExitProvided(id), GetSectionNode(id),
                GetSectionRequirement(id));
        }

        /// <summary>
        /// Returns a new dropdown section instance for the specified section.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <returns>
        /// A new dropdown section instance.
        /// </returns>
        private ISection GetDropdownSection(LocationID id)
        {
            return _dropdownFactory(
                GetSectionName(id), GetVisibleNode(id), GetSectionNode(id),
                GetSectionRequirement(id));
        }

        /// <summary>
        /// Returns a new insanity entrance section instance for the specified section.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <returns>
        /// A new insanity entrance section instance.
        /// </returns>
        private ISection GetInsanityEntranceSection(LocationID id)
        {
            return _insanityEntranceFactory(
                GetSectionName(id), GetEntranceSectionExitProvided(id), GetSectionNode(id),
                GetSectionRequirement(id));
        }

        /// <summary>
        /// Returns a new dungeon entrance section instance for the specified section.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <returns>
        /// A new dungeon entrance section instance.
        /// </returns>
        private ISection GetDungeonEntranceSection(LocationID id)
        {
            return _dungeonEntranceFactory(
                GetSectionName(id), GetEntranceSectionExitProvided(id), GetSectionNode(id),
                GetSectionRequirement(id));
        }

        /// <summary>
        /// Returns a new shop section instance for the specified section.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <returns>
        /// A new shop section instance.
        /// </returns>
        private ISection GetShopSection(LocationID id)
        {
            return _shopFactory(GetSectionNode(id), GetSectionRequirement(id));
        }

        /// <summary>
        /// Returns a new take any section instance for the specified section.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <returns>
        /// A new take any section instance.
        /// </returns>
        private ISection GetTakeAnySection(LocationID id)
        {
            return _takeAnyFactory(GetSectionNode(id), GetSectionRequirement(id));
        }

        /// <summary>
        /// Returns a list of sections for the specified location.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <param name="location">
        /// The location parent class.
        /// </param>
        /// <returns>
        /// A list of sections.
        /// </returns>
        public List<ISection> GetSections(LocationID id)
        {
            switch (id)
            {
                case LocationID.LinksHouse:
                case LocationID.BottleVendor:
                case LocationID.ChickenHouse:
                case LocationID.Tavern:
                case LocationID.SickKid:
                case LocationID.MagicBat:
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
                case LocationID.SouthOfGrove:
                case LocationID.DiggingGame:
                case LocationID.WaterfallFairy:
                case LocationID.Catfish:
                case LocationID.GraveyardLedge:
                case LocationID.CShapedHouse:
                case LocationID.TreasureGame:
                case LocationID.BombableShack:
                case LocationID.Blacksmith:
                case LocationID.PurpleChest:
                case LocationID.HammerPegs:
                case LocationID.MireShack:
                case LocationID.CheckerboardCave:
                case LocationID.OldMan:
                case LocationID.SpikeCave:
                case LocationID.SpiralCave:
                case LocationID.SuperBunnyCave:
                case LocationID.MimicCave:
                    {
                        return new List<ISection>
                        {
                            GetItemSection(id)
                        };
                    }
                case LocationID.Pedestal:
                case LocationID.LumberjackCave:
                case LocationID.RaceGame:
                case LocationID.Library:
                case LocationID.MushroomSpot:
                case LocationID.ForestHideout:
                case LocationID.BombosTablet:
                case LocationID.DesertLedge:
                case LocationID.BumperCave:
                case LocationID.LakeHyliaIsland:
                case LocationID.EtherTablet:
                case LocationID.FloatingIsland:
                    {
                        return new List<ISection>
                        {
                            GetVisibleItemSection(id)
                        };
                    }
                case LocationID.BlindsHouse:
                case LocationID.TheWell:
                case LocationID.CastleSecret:
                case LocationID.SahasrahlasHut:
                case LocationID.Dam:
                case LocationID.ParadoxCave:
                case LocationID.HookshotCave:
                    {
                        return new List<ISection>
                        {
                            GetItemSection(id, 0),
                            GetItemSection(id, 1)
                        };
                    }
                case LocationID.ZoraArea:
                case LocationID.SpectacleRock:
                    {
                        return new List<ISection>
                        {
                            GetVisibleItemSection(id, 0),
                            GetItemSection(id, 1)
                        };
                    }
                case LocationID.HyruleCastle:
                    {
                        return new List<ISection>
                        {
                            GetDungeonItemSection(id, 0)
                        };
                    }
                case LocationID.AgahnimTower:
                case LocationID.EasternPalace:
                case LocationID.TowerOfHera:
                case LocationID.PalaceOfDarkness:
                case LocationID.SwampPalace:
                case LocationID.SkullWoods:
                case LocationID.ThievesTown:
                case LocationID.IcePalace:
                case LocationID.MiseryMire:
                case LocationID.TurtleRock:
                    {
                        return new List<ISection>
                        {
                            GetDungeonItemSection(id, 0),
                            GetPrizeSection(id, 1)
                        };
                    }
                case LocationID.DesertPalace:
                    {
                        return new List<ISection>
                        {
                            GetMarkableDungeonItemSection(id, 0),
                            GetPrizeSection(id, 1)
                        };
                    }
                case LocationID.GanonsTower:
                    {
                        return new List<ISection>
                        {
                            GetMarkableDungeonItemSection(id, 0),
                            GetBossSection(id, 1),
                            GetBossSection(id, 2),
                            GetBossSection(id, 3),
                            GetPrizeSection(id, 4)
                        };
                    }
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
                case LocationID.DarkWitchsHut:
                case LocationID.DarkFluteSpotFiveEntrance:
                case LocationID.FatFairyEntrance:
                case LocationID.PegsFairyCaveEntrance:
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
                case LocationID.HookshotCaveTop:
                case LocationID.LinksHouseEntrance:
                    {
                        return new List<ISection>
                        {
                            GetEntranceSection(id)
                        };
                    }
                case LocationID.LumberjackCaveEntrance:
                case LocationID.TheWellEntrance:
                case LocationID.MagicBatEntrance:
                case LocationID.ForestHideoutEntrance:
                case LocationID.CastleSecretEntrance:
                case LocationID.HoulihanHole:
                case LocationID.SanctuaryGrave:
                case LocationID.GanonHole:
                    {
                        return new List<ISection>
                        {
                            GetDropdownSection(id)
                        };
                    }
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
                case LocationID.PalaceOfDarknessEntrance:
                case LocationID.IcePalaceEntrance:
                case LocationID.MiseryMireEntrance:
                case LocationID.TowerOfHeraEntrance:
                case LocationID.TurtleRockEntrance:
                case LocationID.GanonsTowerEntrance:
                case LocationID.TRLedgeLeft:
                case LocationID.TRLedgeRight:
                case LocationID.TRSafetyDoor:
                    {
                        return new List<ISection>
                        {
                            GetDungeonEntranceSection(id)
                        };
                    }
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
                    {
                        return new List<ISection>
                        {
                            GetTakeAnySection(id)
                        };
                    }
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
                    {
                        return new List<ISection>
                        {
                            GetInsanityEntranceSection(id)
                        };
                    }
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
                    {
                        return new List<ISection>
                        {
                            GetShopSection(id)
                        };
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }
    }
}
