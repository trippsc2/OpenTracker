using OpenTracker.Models.Requirements;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.Locations
{
    /// <summary>
    /// This is the class for creating map locations.
    /// </summary>
    public static class MapLocationFactory
    {
        /// <summary>
        /// Returns the list of map locations for the specified location.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <returns>
        /// The list of map locations.
        /// </returns>
        public static List<MapLocation> GetMapLocations(LocationID id)
        {
            return id switch
            {
                LocationID.LinksHouse => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1097, 1366,
                        RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen]),
                    new MapLocation(
                        id, MapID.DarkWorld, 1097, 1366,
                        RequirementDictionary.Instance[RequirementType.WorldStateInvertedEntranceShuffleNoneDungeon])
                },
                LocationID.Pedestal => new List<MapLocation>
                {
                    new MapLocation(id, MapID.LightWorld, 83, 101)
                },
                LocationID.LumberjackCave => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 633, 117,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.BlindsHouse => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 307, 840,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.TheWell => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 47, 833,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.BottleVendor => new List<MapLocation>
                {
                    new MapLocation(id, MapID.LightWorld, 190, 933)
                },
                LocationID.ChickenHouse => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 197, 1066,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.Tavern => new List<MapLocation>
                {
                    new MapLocation(id, MapID.LightWorld, 320, 1145)
                },
                LocationID.SickKid => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 314, 1060,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.MagicBat => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 650, 1127,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.RaceGame => new List<MapLocation>
                {
                    new MapLocation(id, MapID.LightWorld, 111, 1354)
                },
                LocationID.Library => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 313, 1310,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.MushroomSpot => new List<MapLocation>
                {
                    new MapLocation(id, MapID.LightWorld, 244, 170)
                },
                LocationID.ForestHideout => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 380, 264,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.CastleSecret => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1196, 834,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.WitchsHut => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1607, 670,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.SahasrahlasHut => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1625, 900,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.BonkRocks => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 777, 590,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.KingsTomb => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1207, 598,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.AginahsCave => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 400, 1655,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.GroveDiggingSpot => new List<MapLocation>
                {
                    new MapLocation(id, MapID.LightWorld, 600, 1350)
                },
                LocationID.Dam => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 942, 1880,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon]),
                    new MapLocation(
                        id, MapID.LightWorld, 900, 1860,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity]),
                },
                LocationID.MiniMoldormCave => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1309, 1887,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.IceRodCave => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1795, 1547,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.Hobo => new List<MapLocation>
                {
                    new MapLocation(id, MapID.LightWorld, 1390, 1390)
                },
                LocationID.PyramidLedge => new List<MapLocation>
                {
                    new MapLocation(id, MapID.DarkWorld, 1164, 922)
                },
                LocationID.FatFairy => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 940, 976,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.HauntedGrove => new List<MapLocation>
                {
                    new MapLocation(id, MapID.DarkWorld, 620, 1371)
                },
                LocationID.HypeCave => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1200, 1560,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.BombosTablet => new List<MapLocation>
                {
                    new MapLocation(id, MapID.LightWorld, 440, 1845),
                    new MapLocation(
                        id, MapID.DarkWorld, 440, 1845,
                        RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen])
                },
                LocationID.SouthOfGrove => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 552, 1693,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon]),
                    new MapLocation(
                        id, MapID.DarkWorld, 552, 1693,
                        RequirementDictionary.Instance[RequirementType.WorldStateStandardOpenEntranceShuffleNoneDungeon])
                },
                LocationID.DiggingGame => new List<MapLocation>
                {
                    new MapLocation(id, MapID.DarkWorld, 100, 1385)
                },
                LocationID.WaterfallFairy => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1806, 286,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.ZoraArea => new List<MapLocation>
                {
                    new MapLocation(id, MapID.LightWorld, 1920, 273)
                },
                LocationID.Catfish => new List<MapLocation>
                {
                    new MapLocation(id, MapID.DarkWorld, 1813, 347)
                },
                LocationID.GraveyardLedge => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1132, 549,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon]),
                    new MapLocation(
                        id, MapID.DarkWorld, 1132, 530,
                        RequirementDictionary.Instance[RequirementType.WorldStateStandardOpenEntranceShuffleNoneDungeon])
                },
                LocationID.DesertLedge => new List<MapLocation>
                {
                    new MapLocation(id, MapID.LightWorld, 40, 1835)
                },
                LocationID.CShapedHouse => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 414, 969,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.TreasureGame => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 100, 936,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.BombableShack => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 219, 1171,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.Blacksmith => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 616, 1054,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon]),
                    new MapLocation(
                        id, MapID.DarkWorld, 295, 1325,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.PurpleChest => new List<MapLocation>
                {
                    new MapLocation(id, MapID.LightWorld, 680, 1805),
                    new MapLocation(id, MapID.DarkWorld, 601, 1050)
                },
                LocationID.HammerPegs => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 636, 1214,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.BumperCave => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 687, 340,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon]),
                    new MapLocation(
                        id, MapID.DarkWorld, 680, 315,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.LakeHyliaIsland => new List<MapLocation>
                {
                    new MapLocation(id, MapID.LightWorld, 1450, 1666),
                    new MapLocation(
                        id, MapID.DarkWorld, 1450, 1666,
                        RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen])
                },
                LocationID.MireShack => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 77, 1600,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.CheckerboardCave => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 354, 1560,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon]),
                    new MapLocation(
                        id, MapID.DarkWorld, 334, 1557,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.OldMan => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 816, 378,
                        RequirementDictionary.Instance[RequirementType.WorldStateInvertedEntranceShuffleNoneDungeon]),
                    new MapLocation(
                        id, MapID.LightWorld, 900, 440,
                        RequirementDictionary.Instance[RequirementType.WorldStateStandardOpenEntranceShuffleNoneDungeon]),
                    new MapLocation(
                        id, MapID.DarkWorld, 816, 378,
                        RequirementDictionary.Instance[RequirementType.WorldStateInvertedEntranceShuffleNoneDungeon])
                },
                LocationID.SpectacleRock => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 980, 178,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon]),
                    new MapLocation(
                        id, MapID.LightWorld, 1036, 170,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.EtherTablet => new List<MapLocation>
                {
                    new MapLocation(id, MapID.LightWorld, 844, 38)
                },
                LocationID.SpikeCave => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1151, 294,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.SpiralCave => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1598, 180,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.ParadoxCave => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1731, 434,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.SuperBunnyCave => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1695, 290,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.HookshotCave => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1670, 126,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.FloatingIsland => new List<MapLocation>
                {
                    new MapLocation(id, MapID.LightWorld, 1627, 40),
                    new MapLocation(
                        id, MapID.DarkWorld, 1627, 40,
                        RequirementDictionary.Instance[RequirementType.WorldStateStandardOpenEntranceShuffleNoneDungeon])
                },
                LocationID.MimicCave => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1694, 190,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon]),
                    new MapLocation(
                        id, MapID.DarkWorld, 1693, 205,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.HyruleCastle => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1003, 906,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNone]),
                    new MapLocation(
                        id, MapID.LightWorld, 925, 536,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNoneDungeonAll]),
                    new MapLocation(
                        id, MapID.LightWorld, 925, 516,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleInsanity])
                },
                LocationID.AgahnimTower => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1003, 807,
                        RequirementDictionary.Instance[RequirementType.WorldStateStandardOpenEntranceShuffleNone]),
                    new MapLocation(
                        id, MapID.DarkWorld, 1126, 68,
                        RequirementDictionary.Instance[RequirementType.WorldStateInvertedEntranceShuffleNone]),
                    new MapLocation(
                        id, MapID.LightWorld, 1000, 750,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.EasternPalace => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1925, 791,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNone]),
                    new MapLocation(
                        id, MapID.LightWorld, 1925, 780,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.DesertPalace => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 146, 1584,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNone]),
                    new MapLocation(
                        id, MapID.LightWorld, 150, 1700,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.TowerOfHera => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1126, 68,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNone]),
                    new MapLocation(
                        id, MapID.LightWorld, 1125, 20,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.PalaceOfDarkness => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1924, 800,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNone]),
                    new MapLocation(
                        id, MapID.DarkWorld, 1925, 785,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.SwampPalace => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 940, 1880,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNone]),
                    new MapLocation(
                        id, MapID.DarkWorld, 940, 1840,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.SkullWoods => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 79, 121,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNone]),
                    new MapLocation(
                        id, MapID.DarkWorld, 80, 50,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.ThievesTown => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 251, 971,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNone]),
                    new MapLocation(
                        id, MapID.DarkWorld, 255, 935,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.IcePalace => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1600, 1735,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNone]),
                    new MapLocation(
                        id, MapID.DarkWorld, 1600, 1695,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.MiseryMire => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 150, 1710,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNone]),
                    new MapLocation(
                        id, MapID.DarkWorld, 150, 1600,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.TurtleRock => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1890, 144,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNone]),
                    new MapLocation(
                        id, MapID.DarkWorld, 1890, 125,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.GanonsTower => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1126, 68,
                        RequirementDictionary.Instance[RequirementType.WorldStateStandardOpenEntranceShuffleNone]),
                    new MapLocation(
                        id, MapID.LightWorld, 1003, 797,
                        RequirementDictionary.Instance[RequirementType.WorldStateInvertedEntranceShuffleNone]),
                    new MapLocation(
                        id, MapID.DarkWorld, 1125, 30,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.LumberjackHouseEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 675, 120,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.LumberjackCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 600, 145,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.DeathMountainEntryCave => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 715, 350,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.DeathMountainExitCave => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 720, 305,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.KakarikoFortuneTellerEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 375, 645,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.WomanLeftDoor => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 305, 840,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.WomanRightDoor => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 340, 840,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.LeftSnitchHouseEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 100, 940,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.RightSnitchHouseEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 415, 965,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.BlindsHouseEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 255, 840,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.TheWellEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 47, 833,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.ChickenHouseEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 197, 1066,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.GrassHouseEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 410, 1075,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.TavernFront => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 320, 1195,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.KakarikoShop => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 220, 1175,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.BombHutEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 55, 1195,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.SickKidEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 314, 1060,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.BlacksmithHouse => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 615, 1055,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.MagicBatEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 650, 1127,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.ChestGameEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 425, 1410,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.RaceHouseLeft => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 220, 1435,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.RaceHouseRight => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 280, 1435,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.LibraryEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 313, 1310,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.ForestHideoutEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 380, 264,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.ForestChestGameEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 370, 40,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.CastleSecretEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1196, 834,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.CastleMainEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1000, 895,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.CastleLeftEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 900, 780,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.CastleRightEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1105, 780,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.CastleTowerEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1000, 800,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.DamEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 942, 1880,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.CentralBonkRocksEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 945, 1310,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.WitchsHutEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1607, 670,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.WaterfallFairyEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1806, 286,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.SahasrahlasHutEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1625, 900,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.TreesFairyCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1650, 1295,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.PegsFairyCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1970, 1405,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.EasternPalaceEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1925, 820,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.HoulihanHole => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1290, 625,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.SanctuaryGrave => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1040, 590,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.NorthBonkRocks => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 777, 590,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.KingsTombEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1207, 598,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.GraveyardLedgeEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1132, 549,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.DesertLeftEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 70, 1590,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.DesertBackEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 150, 1540,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.DesertRightEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 225, 1590,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.DesertFrontEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 150, 1600,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.AginahsCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 400, 1655,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.ThiefCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 555, 1790,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.RupeeCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 625, 1920,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.SkullWoodsBack => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 80, 100,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.ThievesTownEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 255, 975,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.CShapedHouseEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 414, 969,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.HammerHouse => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 408, 1069,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.DarkVillageFortuneTellerEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 377, 647,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.DarkChapelEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 924, 551,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.ShieldShop => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 665, 922,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.DarkLumberjack => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 675, 115,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.TreasureGameEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 100, 936,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.BombableShackEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 219, 1171,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.HammerPegsEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 636, 1214,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.BumperCaveExit => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 720, 310,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.BumperCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 715, 355,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.HypeCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1200, 1560,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.SwampPalaceEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 940, 1875,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.DarkCentralBonkRocksEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 945, 1310,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.SouthOfGroveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 552, 1693,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.BombShop => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1098, 1382,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.ArrowGameEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 431, 1409,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.DarkHyliaFortuneTeller => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1300, 1615,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.DarkTreesFairyCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1656, 1296,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.DarkSahasrahlaEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1706, 1008,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.PalaceOfDarknessEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1925, 830,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.DarkWitchsHut => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1616, 678,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.DarkFluteSpotFiveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1968, 1405,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.FatFairyEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 940, 976,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.GanonHole => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1003, 832,
                        RequirementDictionary.Instance[RequirementType.WorldStateInvertedEntranceShuffleAllInsanity]),
                    new MapLocation(
                        id, MapID.DarkWorld, 1000, 820,
                        RequirementDictionary.Instance[RequirementType.WorldStateStandardOpenEntranceShuffleAllInsanity])
                },
                LocationID.DarkIceRodCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1795, 1545,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.DarkFakeIceRodCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1835, 1545,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.DarkIceRodRockEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1810, 1585,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.HypeFairyCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1200, 1565,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.FortuneTellerEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1300, 1615,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.LakeShop => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1460, 1540,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.UpgradeFairy => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1590, 1710,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.MiniMoldormCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1309, 1887,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.IceRodCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1795, 1545,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.IceBeeCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1835, 1545,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.IceFairyCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1810, 1585,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.IcePalaceEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1600, 1735,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.MiseryMireEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 150, 1650,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.MireShackEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 77, 1600,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.MireRightShackEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 220, 1610,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.MireCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 400, 1655,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.CheckerboardCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 354, 1560,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.DeathMountainEntranceBack => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 816, 378,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.OldManResidence => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 900, 470,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.OldManBackResidence => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1075, 325,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.DeathMountainExitFront => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 790, 275,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.SpectacleRockLeft => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 920, 280,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.SpectacleRockRight => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 980, 290,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.SpectacleRockTop => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 980, 205,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.SpikeCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1151, 294,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.DarkMountainFairyEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 815, 376,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.TowerOfHeraEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1125, 65,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.SpiralCaveBottom => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1605, 260,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.EDMFairyCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1695, 290,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.ParadoxCaveMiddle => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1735, 290,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.ParadoxCaveBottom => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1731, 434,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.EDMConnectorBottom => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1645, 275,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.SpiralCaveTop => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1600, 180,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.MimicCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1695, 180,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.EDMConnectorTop => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1645, 230,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.ParadoxCaveTop => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1725, 125,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.SuperBunnyCaveBottom => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1685, 295,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.DeathMountainShop => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1725, 295,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.SuperBunnyCaveTop => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1725, 128,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.HookshotCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1670, 126,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.TurtleRockEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1890, 165,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.GanonsTowerEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1125, 70,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.TRLedgeLeft => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1598, 182,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.TRLedgeRight => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1694, 182,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.TRSafetyDoor => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1648, 229,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.HookshotCaveTop => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1627, 40,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.LinksHouseEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1098, 1382,
                        RequirementDictionary.Instance[RequirementType.WorldStateInvertedEntranceShuffleAllInsanity])
                },
                LocationID.TreesFairyCaveTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1650, 1295,
                        RequirementDictionary.Instance[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.PegsFairyCaveTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1970, 1405,
                        RequirementDictionary.Instance[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.KakarikoFortuneTellerTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 375, 645,
                        RequirementDictionary.Instance[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.GrassHouseTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 410, 1075,
                        RequirementDictionary.Instance[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.ForestChestGameTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 370, 40,
                        RequirementDictionary.Instance[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.LumberjackHouseTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 688, 120,
                        RequirementDictionary.Instance[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.LeftSnitchHouseTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 100, 940,
                        RequirementDictionary.Instance[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.RightSnitchHouseTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 415, 965,
                        RequirementDictionary.Instance[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.BombHutTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 55, 1195,
                        RequirementDictionary.Instance[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.IceFairyCaveTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1810, 1602,
                        RequirementDictionary.Instance[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.RupeeCaveTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 625, 1920,
                        RequirementDictionary.Instance[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.CentralBonkRocksTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 945, 1310,
                        RequirementDictionary.Instance[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.ThiefCaveTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 555, 1790,
                        RequirementDictionary.Instance[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.IceBeeCaveTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1850, 1545,
                        RequirementDictionary.Instance[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.FortuneTellerTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1300, 1615,
                        RequirementDictionary.Instance[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.HypeFairyCaveTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1200, 1565,
                        RequirementDictionary.Instance[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.ChestGameTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 425, 1410,
                        RequirementDictionary.Instance[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.EDMFairyCaveTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1695, 290,
                        RequirementDictionary.Instance[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.DarkChapelTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 924, 551,
                        RequirementDictionary.Instance[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.DarkVillageFortuneTellerTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 377, 647,
                        RequirementDictionary.Instance[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.DarkTreesFairyCaveTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1656, 1296,
                        RequirementDictionary.Instance[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.DarkSahasrahlaTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1706, 1008,
                        RequirementDictionary.Instance[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.DarkFluteSpotFiveTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1968, 1405,
                        RequirementDictionary.Instance[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.ArrowGameTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 431, 1409,
                        RequirementDictionary.Instance[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.DarkCentralBonkRocksTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 945, 1310,
                        RequirementDictionary.Instance[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.DarkIceRodCaveTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1795, 1545,
                        RequirementDictionary.Instance[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.DarkFakeIceRodCaveTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1835, 1545,
                        RequirementDictionary.Instance[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.DarkIceRodRockTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1810, 1585,
                        RequirementDictionary.Instance[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.DarkMountainFairyTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 815, 376,
                        RequirementDictionary.Instance[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.MireRightShackTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 220, 1610,
                        RequirementDictionary.Instance[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.MireCaveTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 400, 1655,
                        RequirementDictionary.Instance[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.LumberjackCaveExit => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 666, 64,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleInsanity])
                },
                LocationID.TheWellExit => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 94, 854,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleInsanity])
                },
                LocationID.MagicBatExit => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 634, 1112,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleInsanity])
                },
                LocationID.ForestHideoutExit => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 368, 301,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleInsanity])
                },
                LocationID.CastleSecretExit => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1105, 860,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleInsanity])
                },
                LocationID.HoulihanHoleExit => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1340, 548,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleInsanity])
                },
                LocationID.Sanctuary => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 924, 537,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleInsanity])
                },
                LocationID.GanonHoleExit => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 902, 862,
                        RequirementDictionary.Instance[RequirementType.WorldStateInvertedEntranceShuffleInsanity])
                },
                LocationID.SkullWoodsWestEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 117, 260,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleInsanity])
                },
                LocationID.SkullWoodsCenterEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 290, 291,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleInsanity])
                },
                LocationID.SkullWoodsEastEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 368, 299,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleInsanity])
                },
                LocationID.SkullWoodsNWHole => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 243, 180,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleInsanity])
                },
                LocationID.SkullWoodsSWHole => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 313, 352,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleInsanity])
                },
                LocationID.SkullWoodsSEHole => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 392, 337,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleInsanity])
                },
                LocationID.SkullWoodsNEHole => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 380, 262,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleInsanity])
                },
                _ => throw new ArgumentOutOfRangeException(nameof(id))
            };
        }
    }
}
