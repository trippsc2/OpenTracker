using OpenTracker.Models.Enums;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.Sections
{
    internal static class SectionFactory
    {
        private static string GetSectionName(LocationID id, int index = 0)
        {
            switch (id)
            {
                case LocationID.LinksHouse when index > 0:
                case LocationID.Pedestal when index > 0:
                case LocationID.LumberjackCave when index > 0:
                case LocationID.BlindsHouse when index > 1:
                case LocationID.TheWell when index > 1:
                case LocationID.BottleVendor when index > 0:
                case LocationID.ChickenHouse when index > 0:
                case LocationID.Tavern when index > 0:
                case LocationID.SickKid when index > 0:
                case LocationID.MagicBat when index > 0:
                case LocationID.RaceGame when index > 0:
                case LocationID.Library when index > 0:
                case LocationID.MushroomSpot when index > 0:
                case LocationID.ForestHideout when index > 0:
                case LocationID.CastleSecret when index > 1:
                case LocationID.WitchsHut when index > 0:
                case LocationID.SahasrahlasHut when index > 1:
                case LocationID.BonkRocks when index > 0:
                case LocationID.KingsTomb when index > 0:
                case LocationID.AginahsCave when index > 0:
                case LocationID.GroveDiggingSpot when index > 0:
                case LocationID.Dam when index > 1:
                case LocationID.MiniMoldormCave when index > 0:
                case LocationID.IceRodCave when index > 0:
                case LocationID.Hobo when index > 0:
                case LocationID.PyramidLedge when index > 0:
                case LocationID.FatFairy when index > 0:
                case LocationID.HauntedGrove when index > 0:
                case LocationID.HypeCave when index > 0:
                case LocationID.BombosTablet when index > 0:
                case LocationID.SouthOfGrove when index > 0:
                case LocationID.DiggingGame when index > 0:
                case LocationID.WaterfallFairy when index > 0:
                case LocationID.ZoraArea when index > 1:
                case LocationID.Catfish when index > 0:
                case LocationID.GraveyardLedge when index > 0:
                case LocationID.DesertLedge when index > 0:
                case LocationID.CShapedHouse when index > 0:
                case LocationID.TreasureGame when index > 0:
                case LocationID.BombableShack when index > 0:
                case LocationID.Blacksmith when index > 0:
                case LocationID.PurpleChest when index > 0:
                case LocationID.HammerPegs when index > 0:
                case LocationID.BumperCave when index > 0:
                case LocationID.LakeHyliaIsland when index > 0:
                case LocationID.MireShack when index > 0:
                case LocationID.CheckerboardCave when index > 0:
                case LocationID.OldMan when index > 0:
                case LocationID.SpectacleRock when index > 1:
                case LocationID.EtherTablet when index > 0:
                case LocationID.SpikeCave when index > 0:
                case LocationID.SpiralCave when index > 0:
                case LocationID.ParadoxCave when index > 1:
                case LocationID.SuperBunnyCave when index > 0:
                case LocationID.HookshotCave when index > 1:
                case LocationID.FloatingIsland when index > 0:
                case LocationID.MimicCave when index > 0:
                case LocationID.HyruleCastle when index > 0:
                case LocationID.AgahnimTower when index > 1:
                case LocationID.EasternPalace when index > 1:
                case LocationID.DesertPalace when index > 1:
                case LocationID.TowerOfHera when index > 1:
                case LocationID.PalaceOfDarkness when index > 1:
                case LocationID.SwampPalace when index > 1:
                case LocationID.SkullWoods when index > 1:
                case LocationID.ThievesTown when index > 1:
                case LocationID.IcePalace when index > 1:
                case LocationID.MiseryMire when index > 1:
                case LocationID.TurtleRock when index > 1:
                case LocationID.GanonsTower when index > 4:
                case LocationID.LumberjackHouseEntrance when index > 0:
                case LocationID.LumberjackCaveEntrance when index > 0:
                case LocationID.DeathMountainEntryCave when index > 0:
                case LocationID.DeathMountainExitCave when index > 0:
                case LocationID.KakarikoFortuneTellerEntrance when index > 0:
                case LocationID.WomanLeftDoor when index > 0:
                case LocationID.WomanRightDoor when index > 0:
                case LocationID.LeftSnitchHouseEntrance when index > 0:
                case LocationID.RightSnitchHouseEntrance when index > 0:
                case LocationID.BlindsHouseEntrance when index > 0:
                case LocationID.TheWellEntrance when index > 0:
                case LocationID.ChickenHouseEntrance when index > 0:
                case LocationID.GrassHouseEntrance when index > 0:
                case LocationID.TavernFront when index > 0:
                case LocationID.KakarikoShop when index > 0:
                case LocationID.BombHutEntrance when index > 0:
                case LocationID.SickKidEntrance when index > 0:
                case LocationID.BlacksmithHouse when index > 0:
                case LocationID.MagicBatEntrance when index > 0:
                case LocationID.ChestGameEntrance when index > 0:
                case LocationID.RaceHouseLeft when index > 0:
                case LocationID.RaceHouseRight when index > 0:
                case LocationID.LibraryEntrance when index > 0:
                case LocationID.ForestHideoutEntrance when index > 0:
                case LocationID.ForestChestGameEntrance when index > 0:
                case LocationID.CastleSecretEntrance when index > 0:
                case LocationID.CastleMainEntrance when index > 0:
                case LocationID.CastleLeftEntrance when index > 0:
                case LocationID.CastleRightEntrance when index > 0:
                case LocationID.CastleTowerEntrance when index > 0:
                case LocationID.DamEntrance when index > 0:
                case LocationID.CentralBonkRocksEntrance when index > 0:
                case LocationID.WitchsHutEntrance when index > 0:
                case LocationID.WaterfallFairyEntrance when index > 0:
                case LocationID.SahasrahlasHutEntrance when index > 0:
                case LocationID.TreesFairyCaveEntrance when index > 0:
                case LocationID.PegsFairyCaveEntrance when index > 0:
                case LocationID.EasternPalaceEntrance when index > 0:
                case LocationID.HoulihanHole when index > 0:
                case LocationID.SanctuaryGrave when index > 0:
                case LocationID.NorthBonkRocks when index > 0:
                case LocationID.KingsTombEntrance when index > 0:
                case LocationID.GraveyardLedgeEntrance when index > 0:
                case LocationID.DesertLeftEntrance when index > 0:
                case LocationID.DesertBackEntrance when index > 0:
                case LocationID.DesertRightEntrance when index > 0:
                case LocationID.DesertFrontEntrance when index > 0:
                case LocationID.AginahsCaveEntrance when index > 0:
                case LocationID.ThiefCaveEntrance when index > 0:
                case LocationID.RupeeCaveEntrance when index > 0:
                case LocationID.SkullWoodsBack when index > 0:
                case LocationID.ThievesTownEntrance when index > 0:
                case LocationID.CShapedHouseEntrance when index > 0:
                case LocationID.HammerHouse when index > 0:
                case LocationID.DarkVillageFortuneTellerEntrance when index > 0:
                case LocationID.DarkChapelEntrance when index > 0:
                case LocationID.ShieldShop when index > 0:
                case LocationID.DarkLumberjack when index > 0:
                case LocationID.TreasureGameEntrance when index > 0:
                case LocationID.BombableShackEntrance when index > 0:
                case LocationID.HammerPegsEntrance when index > 0:
                case LocationID.BumperCaveExit when index > 0:
                case LocationID.BumperCaveEntrance when index > 0:
                case LocationID.HypeCaveEntrance when index > 0:
                case LocationID.SwampPalaceEntrance when index > 0:
                case LocationID.DarkCentralBonkRocksEntrance when index > 0:
                case LocationID.SouthOfGroveEntrance when index > 0:
                case LocationID.BombShop when index > 0:
                case LocationID.ArrowGameEntrance when index > 0:
                case LocationID.DarkHyliaFortuneTeller when index > 0:
                case LocationID.DarkTreesFairyCaveEntrance when index > 0:
                case LocationID.DarkSahasrahlaEntrance when index > 0:
                case LocationID.PalaceOfDarknessEntrance when index > 0:
                case LocationID.DarkWitchsHut when index > 0:
                case LocationID.DarkFluteSpotFiveEntrance when index > 0:
                case LocationID.FatFairyEntrance when index > 0:
                case LocationID.GanonHole when index > 0:
                case LocationID.DarkIceRodCaveEntrance when index > 0:
                case LocationID.DarkFakeIceRodCaveEntrance when index > 0:
                case LocationID.DarkIceRodRockEntrance when index > 0:
                case LocationID.HypeFairyCaveEntrance when index > 0:
                case LocationID.FortuneTellerEntrance when index > 0:
                case LocationID.LakeShop when index > 0:
                case LocationID.UpgradeFairy when index > 0:
                case LocationID.MiniMoldormCaveEntrance when index > 0:
                case LocationID.IceRodCaveEntrance when index > 0:
                case LocationID.IceBeeCaveEntrance when index > 0:
                case LocationID.IceFairyCaveEntrance when index > 0:
                case LocationID.IcePalaceEntrance when index > 0:
                case LocationID.MiseryMireEntrance when index > 0:
                case LocationID.MireShackEntrance when index > 0:
                case LocationID.MireRightShackEntrance when index > 0:
                case LocationID.MireCaveEntrance when index > 0:
                case LocationID.CheckerboardCaveEntrance when index > 0:
                case LocationID.DeathMountainEntranceBack when index > 0:
                case LocationID.OldManResidence when index > 0:
                case LocationID.OldManBackResidence when index > 0:
                case LocationID.DeathMountainExitFront when index > 0:
                case LocationID.SpectacleRockLeft when index > 0:
                case LocationID.SpectacleRockRight when index > 0:
                case LocationID.SpectacleRockTop when index > 0:
                case LocationID.SpikeCaveEntrance when index > 0:
                case LocationID.DarkMountainFairyEntrance when index > 0:
                case LocationID.TowerOfHeraEntrance when index > 0:
                case LocationID.SpiralCaveBottom when index > 0:
                case LocationID.EDMFairyCaveEntrance when index > 0:
                case LocationID.ParadoxCaveMiddle when index > 0:
                case LocationID.ParadoxCaveBottom when index > 0:
                case LocationID.EDMConnectorBottom when index > 0:
                case LocationID.SpiralCaveTop when index > 0:
                case LocationID.MimicCaveEntrance when index > 0:
                case LocationID.EDMConnectorTop when index > 0:
                case LocationID.ParadoxCaveTop when index > 0:
                case LocationID.SuperBunnyCaveBottom when index > 0:
                case LocationID.DeathMountainShop when index > 0:
                case LocationID.SuperBunnyCaveTop when index > 0:
                case LocationID.HookshotCaveEntrance when index > 0:
                case LocationID.TurtleRockEntrance when index > 0:
                case LocationID.GanonsTowerEntrance when index > 0:
                case LocationID.TRLedgeLeft when index > 0:
                case LocationID.TRLedgeRight when index > 0:
                case LocationID.TRSafetyDoor when index > 0:
                case LocationID.HookshotCaveTop when index > 0:
                case LocationID.LinksHouseEntrance when index > 0:
                case LocationID.TreesFairyCaveTakeAny when index > 0:
                case LocationID.PegsFairyCaveTakeAny when index > 0:
                case LocationID.KakarikoFortuneTellerTakeAny when index > 0:
                case LocationID.GrassHouseTakeAny when index > 0:
                case LocationID.ForestChestGameTakeAny when index > 0:
                case LocationID.LumberjackHouseTakeAny when index > 0:
                case LocationID.LeftSnitchHouseTakeAny when index > 0:
                case LocationID.RightSnitchHouseTakeAny when index > 0:
                case LocationID.BombHutTakeAny when index > 0:
                case LocationID.IceFairyCaveTakeAny when index > 0:
                case LocationID.RupeeCaveTakeAny when index > 0:
                case LocationID.CentralBonkRocksTakeAny when index > 0:
                case LocationID.ThiefCaveTakeAny when index > 0:
                case LocationID.IceBeeCaveTakeAny when index > 0:
                case LocationID.FortuneTellerTakeAny when index > 0:
                case LocationID.HypeFairyCaveTakeAny when index > 0:
                case LocationID.ChestGameTakeAny when index > 0:
                case LocationID.EDMFairyCaveTakeAny when index > 0:
                case LocationID.DarkChapelTakeAny when index > 0:
                case LocationID.DarkVillageFortuneTellerTakeAny when index > 0:
                case LocationID.DarkTreesFairyCaveTakeAny when index > 0:
                case LocationID.DarkSahasrahlaTakeAny when index > 0:
                case LocationID.DarkFluteSpotFiveTakeAny when index > 0:
                case LocationID.ArrowGameTakeAny when index > 0:
                case LocationID.DarkCentralBonkRocksTakeAny when index > 0:
                case LocationID.DarkIceRodCaveTakeAny when index > 0:
                case LocationID.DarkFakeIceRodCaveTakeAny when index > 0:
                case LocationID.DarkIceRodRockTakeAny when index > 0:
                case LocationID.DarkMountainFairyTakeAny when index > 0:
                case LocationID.MireRightShackTakeAny when index > 0:
                case LocationID.MireCaveTakeAny when index > 0:
                    {
                        throw new ArgumentOutOfRangeException(nameof(index));
                    }
                case LocationID.LinksHouse:
                    {
                        return "By The Door";
                    }
                case LocationID.Pedestal:
                    {
                        return "Pedestal";
                    }
                case LocationID.LumberjackCave:
                case LocationID.TheWell when index == 0:
                case LocationID.BonkRocks:
                case LocationID.AginahsCave:
                case LocationID.MiniMoldormCave:
                case LocationID.IceRodCave:
                case LocationID.HypeCave:
                case LocationID.GraveyardLedge:
                case LocationID.HammerPegs:
                case LocationID.CheckerboardCave:
                case LocationID.SpectacleRock when index == 1:
                case LocationID.SpikeCave:
                case LocationID.SpiralCave:
                case LocationID.SuperBunnyCave:
                case LocationID.MimicCave:
                case LocationID.DeathMountainEntryCave:
                case LocationID.DeathMountainExitCave:
                    {
                        return "Cave";
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
                case LocationID.ParadoxCave when index == 0:
                    {
                        return "Top";
                    }
                case LocationID.ParadoxCave when index == 1:
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
                    {
                        return "Dropdown";
                    }
                case LocationID.ChestGameEntrance:
                case LocationID.RaceHouseLeft:
                case LocationID.RaceHouseRight:
                case LocationID.LibraryEntrance:
                case LocationID.ForestHideoutEntrance:
                    break;
                case LocationID.ForestChestGameEntrance:
                    break;
                case LocationID.CastleSecretEntrance:
                    break;
                case LocationID.CastleMainEntrance:
                    break;
                case LocationID.CastleLeftEntrance:
                    break;
                case LocationID.CastleRightEntrance:
                    break;
                case LocationID.CastleTowerEntrance:
                    break;
                case LocationID.DamEntrance:
                    break;
                case LocationID.CentralBonkRocksEntrance:
                    break;
                case LocationID.WitchsHutEntrance:
                    break;
                case LocationID.WaterfallFairyEntrance:
                    break;
                case LocationID.SahasrahlasHutEntrance:
                    break;
                case LocationID.TreesFairyCaveEntrance:
                    break;
                case LocationID.PegsFairyCaveEntrance:
                    break;
                case LocationID.EasternPalaceEntrance:
                    break;
                case LocationID.HoulihanHole:
                    break;
                case LocationID.SanctuaryGrave:
                    break;
                case LocationID.NorthBonkRocks:
                    break;
                case LocationID.KingsTombEntrance:
                    break;
                case LocationID.GraveyardLedgeEntrance:
                    break;
                case LocationID.DesertLeftEntrance:
                    break;
                case LocationID.DesertBackEntrance:
                    break;
                case LocationID.DesertRightEntrance:
                    break;
                case LocationID.DesertFrontEntrance:
                    break;
                case LocationID.AginahsCaveEntrance:
                    break;
                case LocationID.ThiefCaveEntrance:
                    break;
                case LocationID.RupeeCaveEntrance:
                    break;
                case LocationID.SkullWoodsBack:
                    break;
                case LocationID.ThievesTownEntrance:
                    break;
                case LocationID.CShapedHouseEntrance:
                    break;
                case LocationID.HammerHouse:
                    break;
                case LocationID.DarkVillageFortuneTellerEntrance:
                    break;
                case LocationID.DarkChapelEntrance:
                    break;
                case LocationID.ShieldShop:
                    break;
                case LocationID.DarkLumberjack:
                    break;
                case LocationID.TreasureGameEntrance:
                    break;
                case LocationID.BombableShackEntrance:
                    break;
                case LocationID.HammerPegsEntrance:
                    break;
                case LocationID.BumperCaveExit:
                    break;
                case LocationID.BumperCaveEntrance:
                    break;
                case LocationID.HypeCaveEntrance:
                    break;
                case LocationID.SwampPalaceEntrance:
                    break;
                case LocationID.DarkCentralBonkRocksEntrance:
                    break;
                case LocationID.SouthOfGroveEntrance:
                    break;
                case LocationID.BombShop:
                    break;
                case LocationID.ArrowGameEntrance:
                    break;
                case LocationID.DarkHyliaFortuneTeller:
                    break;
                case LocationID.DarkTreesFairyCaveEntrance:
                    break;
                case LocationID.DarkSahasrahlaEntrance:
                    break;
                case LocationID.PalaceOfDarknessEntrance:
                    break;
                case LocationID.DarkWitchsHut:
                    break;
                case LocationID.DarkFluteSpotFiveEntrance:
                    break;
                case LocationID.FatFairyEntrance:
                    break;
                case LocationID.GanonHole:
                    break;
                case LocationID.DarkIceRodCaveEntrance:
                    break;
                case LocationID.DarkFakeIceRodCaveEntrance:
                    break;
                case LocationID.DarkIceRodRockEntrance:
                    break;
                case LocationID.HypeFairyCaveEntrance:
                    break;
                case LocationID.FortuneTellerEntrance:
                    break;
                case LocationID.LakeShop:
                    break;
                case LocationID.UpgradeFairy:
                    break;
                case LocationID.MiniMoldormCaveEntrance:
                    break;
                case LocationID.IceRodCaveEntrance:
                    break;
                case LocationID.IceBeeCaveEntrance:
                    break;
                case LocationID.IceFairyCaveEntrance:
                    break;
                case LocationID.IcePalaceEntrance:
                    break;
                case LocationID.MiseryMireEntrance:
                    break;
                case LocationID.MireShackEntrance:
                    break;
                case LocationID.MireRightShackEntrance:
                    break;
                case LocationID.MireCaveEntrance:
                    break;
                case LocationID.CheckerboardCaveEntrance:
                    break;
                case LocationID.DeathMountainEntranceBack:
                    break;
                case LocationID.OldManResidence:
                    break;
                case LocationID.OldManBackResidence:
                    break;
                case LocationID.DeathMountainExitFront:
                    break;
                case LocationID.SpectacleRockLeft:
                    break;
                case LocationID.SpectacleRockRight:
                    break;
                case LocationID.SpectacleRockTop:
                    break;
                case LocationID.SpikeCaveEntrance:
                    break;
                case LocationID.DarkMountainFairyEntrance:
                    break;
                case LocationID.TowerOfHeraEntrance:
                    break;
                case LocationID.SpiralCaveBottom:
                    break;
                case LocationID.EDMFairyCaveEntrance:
                    break;
                case LocationID.ParadoxCaveMiddle:
                    break;
                case LocationID.ParadoxCaveBottom:
                    break;
                case LocationID.EDMConnectorBottom:
                    break;
                case LocationID.SpiralCaveTop:
                    break;
                case LocationID.MimicCaveEntrance:
                    break;
                case LocationID.EDMConnectorTop:
                    break;
                case LocationID.ParadoxCaveTop:
                    break;
                case LocationID.SuperBunnyCaveBottom:
                    break;
                case LocationID.DeathMountainShop:
                    break;
                case LocationID.SuperBunnyCaveTop:
                    break;
                case LocationID.HookshotCaveEntrance:
                    break;
                case LocationID.TurtleRockEntrance:
                    break;
                case LocationID.GanonsTowerEntrance:
                    break;
                case LocationID.TRLedgeLeft:
                    break;
                case LocationID.TRLedgeRight:
                    break;
                case LocationID.TRSafetyDoor:
                    break;
                case LocationID.HookshotCaveTop:
                    break;
                case LocationID.LinksHouseEntrance:
                    break;
                case LocationID.TreesFairyCaveTakeAny:
                    break;
                case LocationID.PegsFairyCaveTakeAny:
                    break;
                case LocationID.KakarikoFortuneTellerTakeAny:
                    break;
                case LocationID.GrassHouseTakeAny:
                    break;
                case LocationID.ForestChestGameTakeAny:
                    break;
                case LocationID.LumberjackHouseTakeAny:
                    break;
                case LocationID.LeftSnitchHouseTakeAny:
                    break;
                case LocationID.RightSnitchHouseTakeAny:
                    break;
                case LocationID.BombHutTakeAny:
                    break;
                case LocationID.IceFairyCaveTakeAny:
                    break;
                case LocationID.RupeeCaveTakeAny:
                    break;
                case LocationID.CentralBonkRocksTakeAny:
                    break;
                case LocationID.ThiefCaveTakeAny:
                    break;
                case LocationID.IceBeeCaveTakeAny:
                    break;
                case LocationID.FortuneTellerTakeAny:
                    break;
                case LocationID.HypeFairyCaveTakeAny:
                    break;
                case LocationID.ChestGameTakeAny:
                    break;
                case LocationID.EDMFairyCaveTakeAny:
                    break;
                case LocationID.DarkChapelTakeAny:
                    break;
                case LocationID.DarkVillageFortuneTellerTakeAny:
                    break;
                case LocationID.DarkTreesFairyCaveTakeAny:
                    break;
                case LocationID.DarkSahasrahlaTakeAny:
                    break;
                case LocationID.DarkFluteSpotFiveTakeAny:
                    break;
                case LocationID.ArrowGameTakeAny:
                    break;
                case LocationID.DarkCentralBonkRocksTakeAny:
                    break;
                case LocationID.DarkIceRodCaveTakeAny:
                    break;
                case LocationID.DarkFakeIceRodCaveTakeAny:
                    break;
                case LocationID.DarkIceRodRockTakeAny:
                    break;
                case LocationID.DarkMountainFairyTakeAny:
                    break;
                case LocationID.MireRightShackTakeAny:
                    break;
                case LocationID.MireCaveTakeAny:
                    break;
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }

        internal static List<ISection> GetSections(LocationID id)
        {
            switch (id)
            {
                case LocationID.LinksHouse:
                    break;
                case LocationID.Pedestal:
                    break;
                case LocationID.LumberjackCave:
                    break;
                case LocationID.BlindsHouse:
                    break;
                case LocationID.TheWell:
                    break;
                case LocationID.BottleVendor:
                    break;
                case LocationID.ChickenHouse:
                    break;
                case LocationID.Tavern:
                    break;
                case LocationID.SickKid:
                    break;
                case LocationID.MagicBat:
                    break;
                case LocationID.RaceGame:
                    break;
                case LocationID.Library:
                    break;
                case LocationID.MushroomSpot:
                    break;
                case LocationID.ForestHideout:
                    break;
                case LocationID.CastleSecret:
                    break;
                case LocationID.WitchsHut:
                    break;
                case LocationID.SahasrahlasHut:
                    break;
                case LocationID.BonkRocks:
                    break;
                case LocationID.KingsTomb:
                    break;
                case LocationID.AginahsCave:
                    break;
                case LocationID.GroveDiggingSpot:
                    break;
                case LocationID.Dam:
                    break;
                case LocationID.MiniMoldormCave:
                    break;
                case LocationID.IceRodCave:
                    break;
                case LocationID.Hobo:
                    break;
                case LocationID.PyramidLedge:
                    break;
                case LocationID.FatFairy:
                    break;
                case LocationID.HauntedGrove:
                    break;
                case LocationID.HypeCave:
                    break;
                case LocationID.BombosTablet:
                    break;
                case LocationID.SouthOfGrove:
                    break;
                case LocationID.DiggingGame:
                    break;
                case LocationID.WaterfallFairy:
                    break;
                case LocationID.ZoraArea:
                    break;
                case LocationID.Catfish:
                    break;
                case LocationID.GraveyardLedge:
                    break;
                case LocationID.DesertLedge:
                    break;
                case LocationID.CShapedHouse:
                    break;
                case LocationID.TreasureGame:
                    break;
                case LocationID.BombableShack:
                    break;
                case LocationID.Blacksmith:
                    break;
                case LocationID.PurpleChest:
                    break;
                case LocationID.HammerPegs:
                    break;
                case LocationID.BumperCave:
                    break;
                case LocationID.LakeHyliaIsland:
                    break;
                case LocationID.MireShack:
                    break;
                case LocationID.CheckerboardCave:
                    break;
                case LocationID.OldMan:
                    break;
                case LocationID.SpectacleRock:
                    break;
                case LocationID.EtherTablet:
                    break;
                case LocationID.SpikeCave:
                    break;
                case LocationID.SpiralCave:
                    break;
                case LocationID.ParadoxCave:
                    break;
                case LocationID.SuperBunnyCave:
                    break;
                case LocationID.HookshotCave:
                    break;
                case LocationID.FloatingIsland:
                    break;
                case LocationID.MimicCave:
                    break;
                case LocationID.HyruleCastle:
                    break;
                case LocationID.AgahnimTower:
                    break;
                case LocationID.EasternPalace:
                    break;
                case LocationID.DesertPalace:
                    break;
                case LocationID.TowerOfHera:
                    break;
                case LocationID.PalaceOfDarkness:
                    break;
                case LocationID.SwampPalace:
                    break;
                case LocationID.SkullWoods:
                    break;
                case LocationID.ThievesTown:
                    break;
                case LocationID.IcePalace:
                    break;
                case LocationID.MiseryMire:
                    break;
                case LocationID.TurtleRock:
                    break;
                case LocationID.GanonsTower:
                    break;
                case LocationID.LumberjackHouseEntrance:
                    break;
                case LocationID.LumberjackCaveEntrance:
                    break;
                case LocationID.DeathMountainEntryCave:
                    break;
                case LocationID.DeathMountainExitCave:
                    break;
                case LocationID.KakarikoFortuneTellerEntrance:
                    break;
                case LocationID.WomanLeftDoor:
                    break;
                case LocationID.WomanRightDoor:
                    break;
                case LocationID.LeftSnitchHouseEntrance:
                    break;
                case LocationID.RightSnitchHouseEntrance:
                    break;
                case LocationID.BlindsHouseEntrance:
                    break;
                case LocationID.TheWellEntrance:
                    break;
                case LocationID.ChickenHouseEntrance:
                    break;
                case LocationID.GrassHouseEntrance:
                    break;
                case LocationID.TavernFront:
                    break;
                case LocationID.KakarikoShop:
                    break;
                case LocationID.BombHutEntrance:
                    break;
                case LocationID.SickKidEntrance:
                    break;
                case LocationID.BlacksmithHouse:
                    break;
                case LocationID.MagicBatEntrance:
                    break;
                case LocationID.ChestGameEntrance:
                    break;
                case LocationID.RaceHouseLeft:
                    break;
                case LocationID.RaceHouseRight:
                    break;
                case LocationID.LibraryEntrance:
                    break;
                case LocationID.ForestHideoutEntrance:
                    break;
                case LocationID.ForestChestGameEntrance:
                    break;
                case LocationID.CastleSecretEntrance:
                    break;
                case LocationID.CastleMainEntrance:
                    break;
                case LocationID.CastleLeftEntrance:
                    break;
                case LocationID.CastleRightEntrance:
                    break;
                case LocationID.CastleTowerEntrance:
                    break;
                case LocationID.DamEntrance:
                    break;
                case LocationID.CentralBonkRocksEntrance:
                    break;
                case LocationID.WitchsHutEntrance:
                    break;
                case LocationID.WaterfallFairyEntrance:
                    break;
                case LocationID.SahasrahlasHutEntrance:
                    break;
                case LocationID.TreesFairyCaveEntrance:
                    break;
                case LocationID.PegsFairyCaveEntrance:
                    break;
                case LocationID.EasternPalaceEntrance:
                    break;
                case LocationID.HoulihanHole:
                    break;
                case LocationID.SanctuaryGrave:
                    break;
                case LocationID.NorthBonkRocks:
                    break;
                case LocationID.KingsTombEntrance:
                    break;
                case LocationID.GraveyardLedgeEntrance:
                    break;
                case LocationID.DesertLeftEntrance:
                    break;
                case LocationID.DesertBackEntrance:
                    break;
                case LocationID.DesertRightEntrance:
                    break;
                case LocationID.DesertFrontEntrance:
                    break;
                case LocationID.AginahsCaveEntrance:
                    break;
                case LocationID.ThiefCaveEntrance:
                    break;
                case LocationID.RupeeCaveEntrance:
                    break;
                case LocationID.SkullWoodsBack:
                    break;
                case LocationID.ThievesTownEntrance:
                    break;
                case LocationID.CShapedHouseEntrance:
                    break;
                case LocationID.HammerHouse:
                    break;
                case LocationID.DarkVillageFortuneTellerEntrance:
                    break;
                case LocationID.DarkChapelEntrance:
                    break;
                case LocationID.ShieldShop:
                    break;
                case LocationID.DarkLumberjack:
                    break;
                case LocationID.TreasureGameEntrance:
                    break;
                case LocationID.BombableShackEntrance:
                    break;
                case LocationID.HammerPegsEntrance:
                    break;
                case LocationID.BumperCaveExit:
                    break;
                case LocationID.BumperCaveEntrance:
                    break;
                case LocationID.HypeCaveEntrance:
                    break;
                case LocationID.SwampPalaceEntrance:
                    break;
                case LocationID.DarkCentralBonkRocksEntrance:
                    break;
                case LocationID.SouthOfGroveEntrance:
                    break;
                case LocationID.BombShop:
                    break;
                case LocationID.ArrowGameEntrance:
                    break;
                case LocationID.DarkHyliaFortuneTeller:
                    break;
                case LocationID.DarkTreesFairyCaveEntrance:
                    break;
                case LocationID.DarkSahasrahlaEntrance:
                    break;
                case LocationID.PalaceOfDarknessEntrance:
                    break;
                case LocationID.DarkWitchsHut:
                    break;
                case LocationID.DarkFluteSpotFiveEntrance:
                    break;
                case LocationID.FatFairyEntrance:
                    break;
                case LocationID.GanonHole:
                    break;
                case LocationID.DarkIceRodCaveEntrance:
                    break;
                case LocationID.DarkFakeIceRodCaveEntrance:
                    break;
                case LocationID.DarkIceRodRockEntrance:
                    break;
                case LocationID.HypeFairyCaveEntrance:
                    break;
                case LocationID.FortuneTellerEntrance:
                    break;
                case LocationID.LakeShop:
                    break;
                case LocationID.UpgradeFairy:
                    break;
                case LocationID.MiniMoldormCaveEntrance:
                    break;
                case LocationID.IceRodCaveEntrance:
                    break;
                case LocationID.IceBeeCaveEntrance:
                    break;
                case LocationID.IceFairyCaveEntrance:
                    break;
                case LocationID.IcePalaceEntrance:
                    break;
                case LocationID.MiseryMireEntrance:
                    break;
                case LocationID.MireShackEntrance:
                    break;
                case LocationID.MireRightShackEntrance:
                    break;
                case LocationID.MireCaveEntrance:
                    break;
                case LocationID.CheckerboardCaveEntrance:
                    break;
                case LocationID.DeathMountainEntranceBack:
                    break;
                case LocationID.OldManResidence:
                    break;
                case LocationID.OldManBackResidence:
                    break;
                case LocationID.DeathMountainExitFront:
                    break;
                case LocationID.SpectacleRockLeft:
                    break;
                case LocationID.SpectacleRockRight:
                    break;
                case LocationID.SpectacleRockTop:
                    break;
                case LocationID.SpikeCaveEntrance:
                    break;
                case LocationID.DarkMountainFairyEntrance:
                    break;
                case LocationID.TowerOfHeraEntrance:
                    break;
                case LocationID.SpiralCaveBottom:
                    break;
                case LocationID.EDMFairyCaveEntrance:
                    break;
                case LocationID.ParadoxCaveMiddle:
                    break;
                case LocationID.ParadoxCaveBottom:
                    break;
                case LocationID.EDMConnectorBottom:
                    break;
                case LocationID.SpiralCaveTop:
                    break;
                case LocationID.MimicCaveEntrance:
                    break;
                case LocationID.EDMConnectorTop:
                    break;
                case LocationID.ParadoxCaveTop:
                    break;
                case LocationID.SuperBunnyCaveBottom:
                    break;
                case LocationID.DeathMountainShop:
                    break;
                case LocationID.SuperBunnyCaveTop:
                    break;
                case LocationID.HookshotCaveEntrance:
                    break;
                case LocationID.TurtleRockEntrance:
                    break;
                case LocationID.GanonsTowerEntrance:
                    break;
                case LocationID.TRLedgeLeft:
                    break;
                case LocationID.TRLedgeRight:
                    break;
                case LocationID.TRSafetyDoor:
                    break;
                case LocationID.HookshotCaveTop:
                    break;
                case LocationID.LinksHouseEntrance:
                    break;
                case LocationID.TreesFairyCaveTakeAny:
                    break;
                case LocationID.PegsFairyCaveTakeAny:
                    break;
                case LocationID.KakarikoFortuneTellerTakeAny:
                    break;
                case LocationID.GrassHouseTakeAny:
                    break;
                case LocationID.ForestChestGameTakeAny:
                    break;
                case LocationID.LumberjackHouseTakeAny:
                    break;
                case LocationID.LeftSnitchHouseTakeAny:
                    break;
                case LocationID.RightSnitchHouseTakeAny:
                    break;
                case LocationID.BombHutTakeAny:
                    break;
                case LocationID.IceFairyCaveTakeAny:
                    break;
                case LocationID.RupeeCaveTakeAny:
                    break;
                case LocationID.CentralBonkRocksTakeAny:
                    break;
                case LocationID.ThiefCaveTakeAny:
                    break;
                case LocationID.IceBeeCaveTakeAny:
                    break;
                case LocationID.FortuneTellerTakeAny:
                    break;
                case LocationID.HypeFairyCaveTakeAny:
                    break;
                case LocationID.ChestGameTakeAny:
                    break;
                case LocationID.EDMFairyCaveTakeAny:
                    break;
                case LocationID.DarkChapelTakeAny:
                    break;
                case LocationID.DarkVillageFortuneTellerTakeAny:
                    break;
                case LocationID.DarkTreesFairyCaveTakeAny:
                    break;
                case LocationID.DarkSahasrahlaTakeAny:
                    break;
                case LocationID.DarkFluteSpotFiveTakeAny:
                    break;
                case LocationID.ArrowGameTakeAny:
                    break;
                case LocationID.DarkCentralBonkRocksTakeAny:
                    break;
                case LocationID.DarkIceRodCaveTakeAny:
                    break;
                case LocationID.DarkFakeIceRodCaveTakeAny:
                    break;
                case LocationID.DarkIceRodRockTakeAny:
                    break;
                case LocationID.DarkMountainFairyTakeAny:
                    break;
                case LocationID.MireRightShackTakeAny:
                    break;
                case LocationID.MireCaveTakeAny:
                    break;
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }
    }
}
