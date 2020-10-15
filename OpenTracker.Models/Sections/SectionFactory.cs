using OpenTracker.Models.AutoTracking.AutotrackValues;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Locations;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.Requirements;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This is the class for creating section classes.
    /// </summary>
    public static class SectionFactory
    {
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
        /// Returns the connections to the specified section.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <param name="index">
        /// The section index.
        /// </param>
        /// <returns>
        /// A list of connections to the section.
        /// </returns>
        private static IRequirementNode GetSectionConnections(LocationID id, int index = 0)
        {
            switch (id)
            {
                case LocationID.LinksHouse:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.Start];
                    }
                case LocationID.Pedestal:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.Pedestal];
                    }
                case LocationID.LumberjackCave:
                case LocationID.LumberjackCaveEntrance:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.LumberjackCave];
                    }
                case LocationID.BlindsHouse when index == 0:
                case LocationID.Tavern:
                case LocationID.Dam when index == 0:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.LightWorldNotBunnyOrSuperBunnyMirror];
                    }
                case LocationID.BlindsHouse when index == 1:
                case LocationID.TheWell when index == 1:
                case LocationID.ChickenHouse:
                case LocationID.SahasrahlasHut when index == 0:
                case LocationID.AginahsCave:
                case LocationID.Dam when index == 1:
                case LocationID.MiniMoldormCave:
                case LocationID.IceRodCave:
                case LocationID.CastleSecretEntrance:
                case LocationID.HypeFairyCaveEntrance:
                case LocationID.MiniMoldormCaveEntrance:
                case LocationID.IceRodCaveEntrance:
                case LocationID.HypeFairyCaveTakeAny:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.LightWorldNotBunny];
                    }
                case LocationID.TheWell when index == 0:
                    {
                        return RequirementNodeDictionary.Instance[
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
                case LocationID.KakarikoShop:
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
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.LightWorld];
                    }
                case LocationID.SickKid:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.SickKid];
                    }
                case LocationID.MagicBat:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.MagicBat];
                    }
                case LocationID.RaceGame:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.RaceGame];
                    }
                case LocationID.Library:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.Library];
                    }
                case LocationID.MushroomSpot:
                case LocationID.ForestHideout:
                case LocationID.HoulihanHole:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldNotBunnyOrInspect];
                    }
                case LocationID.CastleSecret:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.LightWorldNotBunny];
                    }
                case LocationID.WitchsHut:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.WitchsHut];
                    }
                case LocationID.SahasrahlasHut when index == 1:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.Sahasrahla];
                    }
                case LocationID.BonkRocks:
                case LocationID.CentralBonkRocksEntrance:
                case LocationID.NorthBonkRocks:
                case LocationID.CentralBonkRocksTakeAny:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.LightWorldDash];
                    }
                case LocationID.KingsTomb:
                case LocationID.KingsTombEntrance:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.KingsTombGrave];
                    }
                case LocationID.GroveDiggingSpot:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.GroveDiggingSpot];
                    }
                case LocationID.Hobo:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.Hobo];
                    }
                case LocationID.PyramidLedge:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldEast];
                    }
                case LocationID.FatFairy:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.BigBombToWall];
                    }
                case LocationID.HauntedGrove:
                case LocationID.HypeCaveEntrance:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldSouthNotBunny];
                    }
                case LocationID.HypeCave:
                case LocationID.DiggingGame:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.DarkWorldSouthNotBunny];
                    }
                case LocationID.BombosTablet:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.BombosTablet];
                    }
                case LocationID.SouthOfGrove:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.SouthOfGrove];
                    }
                case LocationID.WaterfallFairy:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.WaterfallFairy];
                    }
                case LocationID.ZoraArea when index == 0:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.ZoraLedge];
                    }
                case LocationID.ZoraArea when index == 1:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.ZoraArea];
                    }
                case LocationID.Catfish:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.CatfishArea];
                    }
                case LocationID.GraveyardLedge:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.LWGraveyardLedge];
                    }
                case LocationID.DesertLedge:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.DesertLedgeItem];
                    }
                case LocationID.CShapedHouse:
                case LocationID.TreasureGame:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.DarkWorldWestNotBunnyOrSuperBunnyMirror];
                    }
                case LocationID.BombableShack:
                case LocationID.BombableShackEntrance:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.DarkWorldWestNotBunny];
                    }
                case LocationID.Blacksmith:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.Blacksmith];
                    }
                case LocationID.PurpleChest:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.PurpleChest];
                    }
                case LocationID.HammerPegs:
                case LocationID.HammerPegsEntrance:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.HammerPegs];
                    }
                case LocationID.BumperCave:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.BumperCaveItem];
                    }
                case LocationID.LakeHyliaIsland:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.LakeHyliaIslandItem];
                    }
                case LocationID.MireShack:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.MireAreaNotBunnyOrSuperBunnyMirror];
                    }
                case LocationID.CheckerboardCave:
                case LocationID.CheckerboardCaveEntrance:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.CheckerboardCave];
                    }
                case LocationID.OldMan:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.DeathMountainEntryCaveDark];
                    }
                case LocationID.SpectacleRock when index == 0:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.SpectacleRockTopItem];
                    }
                case LocationID.SpectacleRock when index == 1:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.DeathMountainWestBottom];
                    }
                case LocationID.EtherTablet:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.EtherTablet];
                    }
                case LocationID.SpikeCave:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.SpikeCaveChest];
                    }
                case LocationID.SpiralCave:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.SpiralCave];
                    }
                case LocationID.ParadoxCave when index == 0:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.ParadoxCaveNotBunny];
                    }
                case LocationID.ParadoxCave:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.ParadoxCaveTop];
                    }
                case LocationID.SuperBunnyCave:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.SuperBunnyCaveChests];
                    }
                case LocationID.HookshotCave when index == 0:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.HookshotCaveBonkableChest];
                    }
                case LocationID.HookshotCave when index == 1:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.HookshotCaveBack];
                    }
                case LocationID.FloatingIsland:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.FloatingIsland];
                    }
                case LocationID.MimicCave:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.MimicCave];
                    }
                case LocationID.DeathMountainEntryCave:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.DeathMountainEntry];
                    }
                case LocationID.DeathMountainExitCave:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.DeathMountainExit];
                    }
                case LocationID.GrassHouseEntrance:
                case LocationID.GrassHouseTakeAny:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.GrassHouse];
                    }
                case LocationID.BombHutEntrance:
                case LocationID.BombHutTakeAny:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.BombHut];
                    }
                case LocationID.MagicBatEntrance:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.MagicBatEntrance];
                    }
                case LocationID.RaceHouseLeft:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.RaceGameLedge];
                    }
                case LocationID.ForestHideoutEntrance:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldNotBunnyOrInspect];
                    }
                case LocationID.CastleLeftEntrance:
                case LocationID.CastleRightEntrance:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.HyruleCastleTop];
                    }
                case LocationID.CastleTowerEntrance:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.AgahnimTowerEntrance];
                    }
                case LocationID.WitchsHutEntrance:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.LWWitchArea];
                    }
                case LocationID.WaterfallFairyEntrance:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.WaterfallFairy];
                    }
                case LocationID.SanctuaryGrave:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.SanctuaryGraveEntrance];
                    }
                case LocationID.GraveyardLedgeEntrance:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.LWGraveyardLedge];
                    }
                case LocationID.DesertLeftEntrance:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.DesertLedge];
                    }
                case LocationID.DesertBackEntrance:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.DesertBack];
                    }
                case LocationID.DesertRightEntrance:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.Inaccessible];
                    }
                case LocationID.DesertFrontEntrance:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.DesertPalaceFrontEntrance];
                    }
                case LocationID.RupeeCaveEntrance:
                case LocationID.IceFairyCaveEntrance:
                case LocationID.IceFairyCaveTakeAny:
                case LocationID.RupeeCaveTakeAny:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.LightWorldLift1];
                    }
                case LocationID.SkullWoodsBack:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.SkullWoodsBack];
                    }
                case LocationID.ThievesTownEntrance:
                    {
                        return RequirementNodeDictionary.Instance[
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
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldWest];
                    }
                case LocationID.HammerHouse:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.HammerHouse];
                    }
                case LocationID.BumperCaveExit:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.BumperCaveTop];
                    }
                case LocationID.BumperCaveEntrance:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.BumperCaveEntry];
                    }
                case LocationID.SwampPalaceEntrance:
                case LocationID.BombShop:
                case LocationID.ArrowGameEntrance:
                case LocationID.DarkHyliaFortuneTeller:
                case LocationID.ArrowGameTakeAny:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldSouth];
                    }
                case LocationID.DarkCentralBonkRocksEntrance:
                case LocationID.DarkCentralBonkRocksTakeAny:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.DarkWorldSouthDash];
                    }
                case LocationID.SouthOfGroveEntrance:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.SouthOfGroveLedge];
                    }
                case LocationID.DarkTreesFairyCaveEntrance:
                case LocationID.DarkSahasrahlaEntrance:
                case LocationID.DarkFluteSpotFiveEntrance:
                case LocationID.DarkTreesFairyCaveTakeAny:
                case LocationID.DarkSahasrahlaTakeAny:
                case LocationID.DarkFluteSpotFiveTakeAny:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldEast];
                    }
                case LocationID.PalaceOfDarknessEntrance:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.DarkWorldEastNotBunny];
                    }
                case LocationID.DarkWitchsHut:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.DWWitchArea];
                    }
                case LocationID.FatFairyEntrance:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.FatFairyEntrance];
                    }
                case LocationID.GanonHole:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.GanonHole];
                    }
                case LocationID.DarkIceRodCaveEntrance:
                case LocationID.DarkIceRodCaveTakeAny:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.DarkWorldSouthEastNotBunny];
                    }
                case LocationID.DarkFakeIceRodCaveEntrance:
                case LocationID.DarkFakeIceRodCaveTakeAny:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.DarkWorldSouthEast];
                    }
                case LocationID.DarkIceRodRockEntrance:
                case LocationID.DarkIceRodRockTakeAny:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.DarkWorldSouthEastLift1];
                    }
                case LocationID.UpgradeFairy:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.LakeHyliaFairyIsland];
                    }
                case LocationID.IcePalaceEntrance:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.IcePalaceIsland];
                    }
                case LocationID.MiseryMireEntrance:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.MiseryMireEntrance];
                    }
                case LocationID.MireShackEntrance:
                case LocationID.MireRightShackEntrance:
                case LocationID.MireCaveEntrance:
                case LocationID.MireRightShackTakeAny:
                case LocationID.MireCaveTakeAny:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.MireArea];
                    }
                case LocationID.DeathMountainEntranceBack:
                case LocationID.OldManResidence:
                case LocationID.OldManBackResidence:
                case LocationID.DeathMountainExitFront:
                case LocationID.SpectacleRockLeft:
                case LocationID.SpectacleRockRight:
                case LocationID.SpectacleRockTop:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.DeathMountainWestBottom];
                    }
                case LocationID.SpikeCaveEntrance:
                case LocationID.DarkMountainFairyEntrance:
                case LocationID.DarkMountainFairyTakeAny:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.DarkDeathMountainWestBottom];
                    }
                case LocationID.TowerOfHeraEntrance:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.DeathMountainWestTop];
                    }
                case LocationID.SpiralCaveBottom:
                case LocationID.EDMFairyCaveEntrance:
                case LocationID.ParadoxCaveMiddle:
                case LocationID.ParadoxCaveBottom:
                case LocationID.EDMFairyCaveTakeAny:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.DeathMountainEastBottom];
                    }
                case LocationID.EDMConnectorBottom:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.DeathMountainEastBottomConnector];
                    }
                case LocationID.SpiralCaveTop:
                    {
                        {
                            return RequirementNodeDictionary.Instance[
                                RequirementNodeID.SpiralCaveLedge];
                        }
                    }
                case LocationID.MimicCaveEntrance:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.MimicCaveLedge];
                    }
                case LocationID.EDMConnectorTop:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.DeathMountainEastTopConnector];
                    }
                case LocationID.ParadoxCaveTop:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.DeathMountainEastTop];
                    }
                case LocationID.SuperBunnyCaveBottom:
                case LocationID.DeathMountainShop:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.DarkDeathMountainEastBottom];
                    }
                case LocationID.SuperBunnyCaveTop:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.DarkDeathMountainTop];
                    }
                case LocationID.HookshotCaveEntrance:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.HookshotCaveEntrance];
                    }
                case LocationID.TurtleRockEntrance:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.TurtleRockFrontEntrance];
                    }
                case LocationID.GanonsTowerEntrance:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.GanonsTowerEntrance];
                    }
                case LocationID.TRLedgeLeft:
                case LocationID.TRLedgeRight:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.TurtleRockTunnel];
                    }
                case LocationID.TRSafetyDoor:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.TurtleRockSafetyDoor];
                    }
                case LocationID.HookshotCaveTop:
                    {
                        return RequirementNodeDictionary.Instance[
                            RequirementNodeID.DWFloatingIsland];
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
        private static IRequirement GetSectionRequirement(LocationID id, int index = 0)
        {
            switch (id)
            {
                case LocationID.Dam when index == 0:
                case LocationID.SpectacleRock when index == 1:
                    {
                        return RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll];
                    }
                case LocationID.AgahnimTower when index == 0:
                    {
                        return RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOn];
                    }
                case LocationID.GanonsTower when index > 0 && index < 4:
                    {
                        return RequirementDictionary.Instance[RequirementType.BossShuffleOn];
                    }
                default:
                    {
                        return null;
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
        private static IItemSection GetItemSection(LocationID id, int index = 0)
        {
            return new ItemSection(
                GetSectionName(id, index), GetItemSectionTotal(id, index),
                GetSectionConnections(id, index),
                SectionAutoTrackingFactory.GetAutoTrackValue(id, index),
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
        private static ISection GetMarkableItemSection(LocationID id, int index = 0)
        {
            return new MarkableItemSection(GetItemSection(id, index));
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
        private static IDungeonItemSection GetDungeonItemSection(
            IDungeon dungeon, LocationID id, int index = 0)
        {
            return new DungeonItemSection(
                dungeon, SectionAutoTrackingFactory.GetAutoTrackValue(id, index),
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
        private static ISection GetMarkableDungeonItemSection(
            IDungeon dungeon, LocationID id, int index = 0)
        {
            return new MarkableDungeonItemSection(
                GetDungeonItemSection(dungeon, id, index));
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
        private static IBossPlacement GetSectionBossPlacement(LocationID id, int index = 0)
        {
            switch (id)
            {
                case LocationID.AgahnimTower:
                    {
                        return BossPlacementDictionary.Instance[BossPlacementID.ATBoss];
                    }
                case LocationID.EasternPalace:
                    {
                        return BossPlacementDictionary.Instance[BossPlacementID.EPBoss];
                    }
                case LocationID.DesertPalace:
                    {
                        return BossPlacementDictionary.Instance[BossPlacementID.DPBoss];
                    }
                case LocationID.TowerOfHera:
                    {
                        return BossPlacementDictionary.Instance[BossPlacementID.ToHBoss];
                    }
                case LocationID.PalaceOfDarkness:
                    {
                        return BossPlacementDictionary.Instance[BossPlacementID.PoDBoss];
                    }
                case LocationID.SwampPalace:
                    {
                        return BossPlacementDictionary.Instance[BossPlacementID.SPBoss];
                    }
                case LocationID.SkullWoods:
                    {
                        return BossPlacementDictionary.Instance[BossPlacementID.SWBoss];
                    }
                case LocationID.ThievesTown:
                    {
                        return BossPlacementDictionary.Instance[BossPlacementID.TTBoss];
                    }
                case LocationID.IcePalace:
                    {
                        return BossPlacementDictionary.Instance[BossPlacementID.IPBoss];
                    }
                case LocationID.MiseryMire:
                    {
                        return BossPlacementDictionary.Instance[BossPlacementID.MMBoss];
                    }
                case LocationID.TurtleRock:
                    {
                        return BossPlacementDictionary.Instance[BossPlacementID.TRBoss];
                    }
                case LocationID.GanonsTower when index == 1:
                    {
                        return BossPlacementDictionary.Instance[BossPlacementID.GTBoss1];
                    }
                case LocationID.GanonsTower when index == 2:
                    {
                        return BossPlacementDictionary.Instance[BossPlacementID.GTBoss2];
                    }
                case LocationID.GanonsTower when index == 3:
                    {
                        return BossPlacementDictionary.Instance[BossPlacementID.GTBoss3];
                    }
                case LocationID.GanonsTower when index == 4:
                    {
                        return BossPlacementDictionary.Instance[BossPlacementID.GTFinalBoss];
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
        private static IPrizePlacement GetSectionPrizePlacement(LocationID id)
        {
            switch (id)
            {
                case LocationID.AgahnimTower:
                    {
                        return PrizePlacementDictionary.Instance[PrizePlacementID.ATPrize];
                    }
                case LocationID.EasternPalace:
                    {
                        return PrizePlacementDictionary.Instance[PrizePlacementID.EPPrize];
                    }
                case LocationID.DesertPalace:
                    {
                        return PrizePlacementDictionary.Instance[PrizePlacementID.DPPrize];
                    }
                case LocationID.TowerOfHera:
                    {
                        return PrizePlacementDictionary.Instance[PrizePlacementID.ToHPrize];
                    }
                case LocationID.PalaceOfDarkness:
                    {
                        return PrizePlacementDictionary.Instance[PrizePlacementID.PoDPrize];
                    }
                case LocationID.SwampPalace:
                    {
                        return PrizePlacementDictionary.Instance[PrizePlacementID.SPPrize];
                    }
                case LocationID.SkullWoods:
                    {
                        return PrizePlacementDictionary.Instance[PrizePlacementID.SWPrize];
                    }
                case LocationID.ThievesTown:
                    {
                        return PrizePlacementDictionary.Instance[PrizePlacementID.TTPrize];
                    }
                case LocationID.IcePalace:
                    {
                        return PrizePlacementDictionary.Instance[PrizePlacementID.IPPrize];
                    }
                case LocationID.MiseryMire:
                    {
                        return PrizePlacementDictionary.Instance[PrizePlacementID.MMPrize];
                    }
                case LocationID.TurtleRock:
                    {
                        return PrizePlacementDictionary.Instance[PrizePlacementID.TRPrize];
                    }
                case LocationID.GanonsTower:
                    {
                        return PrizePlacementDictionary.Instance[PrizePlacementID.GTPrize];
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
        private static ISection GetBossSection(LocationID id, int index = 0)
        {
            return new BossSection(
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
        private static ISection GetPrizeSection(LocationID id, int index = 0)
        {
            return new PrizeSection(
                GetSectionName(id, index), GetSectionBossPlacement(id, index),
                GetSectionPrizePlacement(id), 
                SectionAutoTrackingFactory.GetAutoTrackValue(id, index),
                GetPrizeSectionAlwaysClearable(id, index), GetSectionRequirement(id, index));
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
        private static IRequirementNode GetEntranceSectionExitProvided(LocationID id)
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
                case LocationID.KakarikoShop:
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
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.LightWorld];
                    }
                case LocationID.LumberjackCaveEntrance:
                case LocationID.ForestHideoutEntrance:
                case LocationID.TheWellEntrance:
                case LocationID.MagicBatEntrance:
                case LocationID.CastleSecretEntrance:
                case LocationID.HoulihanHole:
                case LocationID.SanctuaryGrave:
                case LocationID.GanonHole:
                    {
                        return null;
                    }
                case LocationID.DeathMountainEntryCave:
                    {
                        return RequirementNodeDictionary
                            .Instance[RequirementNodeID.DeathMountainEntry];
                    }
                case LocationID.DeathMountainExitCave:
                    {
                        return RequirementNodeDictionary
                            .Instance[RequirementNodeID.DeathMountainExit];
                    }
                case LocationID.BombHutEntrance:
                    {
                        return RequirementNodeDictionary
                            .Instance[RequirementNodeID.BombHut];
                    }
                case LocationID.RaceHouseLeft:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.RaceGameLedge];
                    }
                case LocationID.CastleLeftEntrance:
                case LocationID.CastleRightEntrance:
                case LocationID.CastleTowerEntrance:
                    {
                        return RequirementNodeDictionary
                            .Instance[RequirementNodeID.HyruleCastleTop];
                    }
                case LocationID.WitchsHutEntrance:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.LWWitchArea];
                    }
                case LocationID.WaterfallFairyEntrance:
                    {
                        return RequirementNodeDictionary
                            .Instance[RequirementNodeID.WaterfallFairy];
                    }
                case LocationID.KingsTombEntrance:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.KingsTomb];
                    }
                case LocationID.GraveyardLedgeEntrance:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.LWGraveyardLedge];
                    }
                case LocationID.DesertLeftEntrance:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.DesertLedge];
                    }
                case LocationID.DesertBackEntrance:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.DesertBack];
                    }
                case LocationID.DesertFrontEntrance:
                    {
                        return null;
                    }
                case LocationID.SkullWoodsBack:
                case LocationID.ThievesTownEntrance:
                case LocationID.CShapedHouseEntrance:
                case LocationID.DarkVillageFortuneTellerEntrance:
                case LocationID.DarkChapelEntrance:
                case LocationID.ShieldShop:
                case LocationID.DarkLumberjack:
                case LocationID.TreasureGameEntrance:
                case LocationID.BombableShackEntrance:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldWest];
                    }
                case LocationID.HammerHouse:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.HammerHouse];
                    }
                case LocationID.HammerPegsEntrance:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.HammerPegsArea];
                    }
                case LocationID.BumperCaveExit:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.BumperCaveTop];
                    }
                case LocationID.BumperCaveEntrance:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.BumperCaveEntry];
                    }
                case LocationID.HypeCaveEntrance:
                case LocationID.SwampPalaceEntrance:
                case LocationID.DarkCentralBonkRocksEntrance:
                case LocationID.BombShop:
                case LocationID.ArrowGameEntrance:
                case LocationID.DarkHyliaFortuneTeller:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldSouth];
                    }
                case LocationID.DarkTreesFairyCaveEntrance:
                case LocationID.DarkSahasrahlaEntrance:
                case LocationID.PalaceOfDarknessEntrance:
                case LocationID.DarkFluteSpotFiveEntrance:
                case LocationID.FatFairyEntrance:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldEast];
                    }
                case LocationID.DarkWitchsHut:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.DWWitchArea];
                    }
                case LocationID.DarkIceRodCaveEntrance:
                case LocationID.DarkFakeIceRodCaveEntrance:
                case LocationID.DarkIceRodRockEntrance:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldSouthEast];
                    }
                case LocationID.UpgradeFairy:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.LakeHyliaFairyIsland];
                    }
                case LocationID.IcePalaceEntrance:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.IcePalaceIsland];
                    }
                case LocationID.MiseryMireEntrance:
                case LocationID.MireShackEntrance:
                case LocationID.MireRightShackEntrance:
                case LocationID.MireCaveEntrance:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.MireArea];
                    }
                case LocationID.CheckerboardCaveEntrance:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.CheckerboardLedge];
                    }
                case LocationID.DeathMountainEntranceBack:
                case LocationID.OldManResidence:
                case LocationID.OldManBackResidence:
                case LocationID.DeathMountainExitFront:
                case LocationID.SpectacleRockLeft:
                case LocationID.SpectacleRockRight:
                case LocationID.SpectacleRockTop:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainWestBottom];
                    }
                case LocationID.SpikeCaveEntrance:
                case LocationID.DarkMountainFairyEntrance:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.DarkDeathMountainWestBottom];
                    }
                case LocationID.TowerOfHeraEntrance:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainWestTop];
                    }
                case LocationID.SpiralCaveBottom:
                case LocationID.EDMFairyCaveEntrance:
                case LocationID.ParadoxCaveMiddle:
                case LocationID.ParadoxCaveBottom:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainEastBottom];
                    }
                case LocationID.EDMConnectorBottom:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainEastBottomConnector];
                    }
                case LocationID.SpiralCaveTop:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.SpiralCaveLedge];
                    }
                case LocationID.MimicCaveEntrance:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.MimicCaveLedge];
                    }
                case LocationID.EDMConnectorTop:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainEastTopConnector];
                    }
                case LocationID.ParadoxCaveTop:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainEastTop];
                    }
                case LocationID.SuperBunnyCaveBottom:
                case LocationID.DeathMountainShop:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.DarkDeathMountainEastBottom];
                    }
                case LocationID.SuperBunnyCaveTop:
                case LocationID.HookshotCaveEntrance:
                case LocationID.TurtleRockEntrance:
                case LocationID.GanonsTowerEntrance:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.DarkDeathMountainTop];
                    }
                case LocationID.TRLedgeLeft:
                case LocationID.TRLedgeRight:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.TurtleRockTunnel];
                    }
                case LocationID.TRSafetyDoor:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.TurtleRockSafetyDoor];
                    }
                case LocationID.HookshotCaveTop:
                    {
                        return RequirementNodeDictionary.Instance[RequirementNodeID.DWFloatingIsland];
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
        private static ISection GetEntranceSection(LocationID id)
        {
            return new EntranceSection(
                GetSectionName(id), GetEntranceSectionExitProvided(id), GetSectionConnections(id),
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
        private static ISection GetDungeonEntranceSection(LocationID id)
        {
            return new DungeonEntranceSection(
                GetSectionName(id), GetEntranceSectionExitProvided(id), GetSectionConnections(id),
                GetSectionRequirement(id));
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
        private static ISection GetTakeAnySection(LocationID id)
        {
            return new TakeAnySection(GetSectionConnections(id), GetSectionRequirement(id));
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
        public static List<ISection> GetSections(LocationID id, ILocation location)
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
                            GetMarkableItemSection(id)
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
                            GetMarkableItemSection(id, 0),
                            GetItemSection(id, 1)
                        };
                    }
                case LocationID.HyruleCastle:
                    {
                        if (location is IDungeon dungeon)
                        {
                            return new List<ISection>
                            {
                                GetDungeonItemSection(dungeon, id, 0)
                            };
                        }
                    }
                    break;
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
                        if (location is IDungeon dungeon)
                        {
                            return new List<ISection>
                            {
                                GetDungeonItemSection(dungeon, id, 0),
                                GetPrizeSection(id, 1)
                            };
                        }
                    }
                    break;
                case LocationID.DesertPalace:
                    {
                        if (location is IDungeon dungeon)
                        {
                            return new List<ISection>
                            {
                                GetMarkableDungeonItemSection(dungeon, id, 0),
                                GetPrizeSection(id, 1)
                            };
                        }
                    }
                    break;
                case LocationID.GanonsTower:
                    {
                        if (location is IDungeon dungeon)
                        {
                            return new List<ISection>
                            {
                                GetMarkableDungeonItemSection(dungeon, id, 0),
                                GetBossSection(id, 1),
                                GetBossSection(id, 2),
                                GetBossSection(id, 3),
                                GetPrizeSection(id, 4)
                            };
                        }
                    }
                    break;
                case LocationID.LumberjackHouseEntrance:
                case LocationID.LumberjackCaveEntrance:
                case LocationID.DeathMountainEntryCave:
                case LocationID.DeathMountainExitCave:
                case LocationID.KakarikoFortuneTellerEntrance:
                case LocationID.WomanLeftDoor:
                case LocationID.WomanRightDoor:
                case LocationID.LeftSnitchHouseEntrance:
                case LocationID.RightSnitchHouseEntrance:
                case LocationID.BlindsHouseEntrance:
                case LocationID.TheWellEntrance:
                case LocationID.ChickenHouseEntrance:
                case LocationID.GrassHouseEntrance:
                case LocationID.TavernFront:
                case LocationID.KakarikoShop:
                case LocationID.BombHutEntrance:
                case LocationID.SickKidEntrance:
                case LocationID.BlacksmithHouse:
                case LocationID.MagicBatEntrance:
                case LocationID.ChestGameEntrance:
                case LocationID.RaceHouseLeft:
                case LocationID.RaceHouseRight:
                case LocationID.LibraryEntrance:
                case LocationID.ForestHideoutEntrance:
                case LocationID.ForestChestGameEntrance:
                case LocationID.CastleSecretEntrance:
                case LocationID.DamEntrance:
                case LocationID.CentralBonkRocksEntrance:
                case LocationID.WitchsHutEntrance:
                case LocationID.WaterfallFairyEntrance:
                case LocationID.SahasrahlasHutEntrance:
                case LocationID.TreesFairyCaveEntrance:
                case LocationID.PegsFairyCaveEntrance:
                case LocationID.HoulihanHole:
                case LocationID.SanctuaryGrave:
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
                case LocationID.GanonHole:
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
                case LocationID.DeathMountainShop:
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
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }
    }
}
