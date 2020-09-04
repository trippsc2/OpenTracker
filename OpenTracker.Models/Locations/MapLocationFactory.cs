using OpenTracker.Models.Requirements;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.Locations
{
    /// <summary>
    /// This is the class for creating map locations.
    /// </summary>
    internal static class MapLocationFactory
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
        internal static List<MapLocation> GetMapLocations(LocationID id)
        {
            return id switch
            {
                LocationID.LinksHouse => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1097, 1366,
                        RequirementDictionary.Instance[RequirementType.WorldStateNonInverted]),
                    new MapLocation(
                        id, MapID.DarkWorld, 1097, 1366,
                        RequirementDictionary.Instance[RequirementType.WorldStateInvertedEntranceShuffleNotAll])
                },
                LocationID.Pedestal => new List<MapLocation>
                {
                    new MapLocation(id, MapID.LightWorld, 83, 101)
                },
                LocationID.LumberjackCave => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 633, 117,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll])
                },
                LocationID.BlindsHouse => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 307, 840,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll])
                },
                LocationID.TheWell => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 47, 833,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll])
                },
                LocationID.BottleVendor => new List<MapLocation>
                {
                    new MapLocation(id, MapID.LightWorld, 190, 933)
                },
                LocationID.ChickenHouse => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 197, 1066,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll])
                },
                LocationID.Tavern => new List<MapLocation>
                {
                    new MapLocation(id, MapID.LightWorld, 320, 1145)
                },
                LocationID.SickKid => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 314, 1060,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll])
                },
                LocationID.MagicBat => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 650, 1127,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll])
                },
                LocationID.RaceGame => new List<MapLocation>
                {
                    new MapLocation(id, MapID.LightWorld, 111, 1354)
                },
                LocationID.Library => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 313, 1310,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll])
                },
                LocationID.MushroomSpot => new List<MapLocation>
                {
                    new MapLocation(id, MapID.LightWorld, 244, 170)
                },
                LocationID.ForestHideout => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 380, 264,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll])
                },
                LocationID.CastleSecret => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1196, 834,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll])
                },
                LocationID.WitchsHut => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1607, 670,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll])
                },
                LocationID.SahasrahlasHut => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1625, 900,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll])
                },
                LocationID.BonkRocks => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 777, 590,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll])
                },
                LocationID.KingsTomb => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1207, 598,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll])
                },
                LocationID.AginahsCave => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 400, 1655,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll])
                },
                LocationID.GroveDiggingSpot => new List<MapLocation>
                {
                    new MapLocation(id, MapID.LightWorld, 600, 1350)
                },
                LocationID.Dam => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 942, 1880,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll]),
                    new MapLocation(
                        id, MapID.LightWorld, 900, 1860,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll]),
                },
                LocationID.MiniMoldormCave => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1309, 1887,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll])
                },
                LocationID.IceRodCave => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1795, 1547,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll])
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
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll])
                },
                LocationID.HauntedGrove => new List<MapLocation>
                {
                    new MapLocation(id, MapID.DarkWorld, 620, 1371)
                },
                LocationID.HypeCave => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1200, 1560,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll])
                },
                LocationID.BombosTablet => new List<MapLocation>
                {
                    new MapLocation(id, MapID.LightWorld, 440, 1845),
                    new MapLocation(
                        id, MapID.DarkWorld, 440, 1845,
                        RequirementDictionary.Instance[RequirementType.WorldStateNonInverted])
                },
                LocationID.SouthOfGrove => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 552, 1693,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll]),
                    new MapLocation(
                        id, MapID.DarkWorld, 552, 1693,
                        RequirementDictionary.Instance[RequirementType.WorldStateNonInvertedEntranceShuffleNotAll])
                },
                LocationID.DiggingGame => new List<MapLocation>
                {
                    new MapLocation(id, MapID.DarkWorld, 100, 1385)
                },
                LocationID.WaterfallFairy => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1806, 286,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll])
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
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll]),
                    new MapLocation(
                        id, MapID.DarkWorld, 1132, 530,
                        RequirementDictionary.Instance[RequirementType.WorldStateNonInvertedEntranceShuffleNotAll])
                },
                LocationID.DesertLedge => new List<MapLocation>
                {
                    new MapLocation(id, MapID.LightWorld, 40, 1835)
                },
                LocationID.CShapedHouse => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 414, 969,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll])
                },
                LocationID.TreasureGame => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 100, 936,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll])
                },
                LocationID.BombableShack => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 219, 1171,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll])
                },
                LocationID.Blacksmith => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 616, 1054,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll]),
                    new MapLocation(
                        id, MapID.DarkWorld, 295, 1325,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll])
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
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll])
                },
                LocationID.BumperCave => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 687, 340,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll]),
                    new MapLocation(
                        id, MapID.DarkWorld, 680, 315,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.LakeHyliaIsland => new List<MapLocation>
                {
                    new MapLocation(id, MapID.LightWorld, 1450, 1666),
                    new MapLocation(
                        id, MapID.DarkWorld, 1450, 1666,
                        RequirementDictionary.Instance[RequirementType.WorldStateNonInverted])
                },
                LocationID.MireShack => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 77, 1600,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll])
                },
                LocationID.CheckerboardCave => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 354, 1560,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll]),
                    new MapLocation(
                        id, MapID.DarkWorld, 334, 1557,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll])
                },
                LocationID.OldMan => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 816, 378,
                        RequirementDictionary.Instance[RequirementType.WorldStateInvertedEntranceShuffleNotAll]),
                    new MapLocation(
                        id, MapID.LightWorld, 900, 440,
                        RequirementDictionary.Instance[RequirementType.WorldStateNonInvertedEntranceShuffleNotAll]),
                    new MapLocation(
                        id, MapID.DarkWorld, 816, 378,
                        RequirementDictionary.Instance[RequirementType.WorldStateInvertedEntranceShuffleNotAll])
                },
                LocationID.SpectacleRock => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 980, 178,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll]),
                    new MapLocation(
                        id, MapID.LightWorld, 1036, 170,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.EtherTablet => new List<MapLocation>
                {
                    new MapLocation(id, MapID.LightWorld, 844, 38)
                },
                LocationID.SpikeCave => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1151, 294,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll])
                },
                LocationID.SpiralCave => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1598, 180,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll])
                },
                LocationID.ParadoxCave => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1731, 434,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll])
                },
                LocationID.SuperBunnyCave => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1695, 290,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll])
                },
                LocationID.HookshotCave => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1670, 126,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll])
                },
                LocationID.FloatingIsland => new List<MapLocation>
                {
                    new MapLocation(id, MapID.LightWorld, 1627, 40),
                    new MapLocation(
                        id, MapID.DarkWorld, 1627, 40,
                        RequirementDictionary.Instance[RequirementType.WorldStateNonInvertedEntranceShuffleNotAll])
                },
                LocationID.MimicCave => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1694, 190,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll]),
                    new MapLocation(
                        id, MapID.DarkWorld, 1693, 205,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNone])
                },
                LocationID.HyruleCastle => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1003, 906,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNone]),
                    new MapLocation(id, MapID.LightWorld, 925, 536)
                },
                LocationID.AgahnimTower => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1003, 807,
                        RequirementDictionary.Instance[RequirementType.WorldStateNonInvertedEntranceShuffleNone]),
                    new MapLocation(
                        id, MapID.DarkWorld, 1126, 68,
                        RequirementDictionary.Instance[RequirementType.WorldStateInvertedEntranceShuffleNone]),
                    new MapLocation(
                        id, MapID.LightWorld, 1000, 750,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn])
                },
                LocationID.EasternPalace => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1925, 791,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNone]),
                    new MapLocation(
                        id, MapID.LightWorld, 1925, 780,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn])
                },
                LocationID.DesertPalace => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 146, 1584,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNone]),
                    new MapLocation(
                        id, MapID.LightWorld, 150, 1700,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn])
                },
                LocationID.TowerOfHera => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1126, 68,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNone]),
                    new MapLocation(
                        id, MapID.LightWorld, 1125, 20,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn])
                },
                LocationID.PalaceOfDarkness => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1924, 800,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNone]),
                    new MapLocation(
                        id, MapID.DarkWorld, 1925, 785,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn])
                },
                LocationID.SwampPalace => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 940, 1880,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNone]),
                    new MapLocation(
                        id, MapID.DarkWorld, 940, 1840,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn])
                },
                LocationID.SkullWoods => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 79, 121,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNone]),
                    new MapLocation(
                        id, MapID.DarkWorld, 80, 50,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn])
                },
                LocationID.ThievesTown => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 251, 971,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNone]),
                    new MapLocation(
                        id, MapID.DarkWorld, 255, 935,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn])
                },
                LocationID.IcePalace => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1600, 1735,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNone]),
                    new MapLocation(
                        id, MapID.DarkWorld, 1600, 1695,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn])
                },
                LocationID.MiseryMire => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 150, 1710,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNone]),
                    new MapLocation(
                        id, MapID.DarkWorld, 150, 1600,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn])
                },
                LocationID.TurtleRock => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1890, 144,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleNone]),
                    new MapLocation(
                        id, MapID.DarkWorld, 1890, 125,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn])
                },
                LocationID.GanonsTower => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1126, 68,
                        RequirementDictionary.Instance[RequirementType.WorldStateNonInvertedEntranceShuffleNone]),
                    new MapLocation(
                        id, MapID.LightWorld, 1003, 797,
                        RequirementDictionary.Instance[RequirementType.WorldStateInvertedEntranceShuffleNone]),
                    new MapLocation(
                        id, MapID.DarkWorld, 1125, 30,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn])
                },
                LocationID.LumberjackHouseEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 675, 120,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.LumberjackCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 600, 145,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.DeathMountainEntryCave => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 715, 350,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.DeathMountainExitCave => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 720, 305,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.KakarikoFortuneTellerEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 375, 645,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.WomanLeftDoor => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 305, 840,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.WomanRightDoor => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 340, 840,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.LeftSnitchHouseEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 100, 940,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.RightSnitchHouseEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 415, 965,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.BlindsHouseEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 255, 840,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.TheWellEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 47, 833,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.ChickenHouseEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 197, 1066,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.GrassHouseEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 410, 1075,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.TavernFront => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 320, 1195,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.KakarikoShop => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 220, 1175,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.BombHutEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 55, 1195,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.SickKidEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 314, 1060,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.BlacksmithHouse => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 615, 1055,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.MagicBatEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 650, 1127,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.ChestGameEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 425, 1410,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.RaceHouseLeft => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 220, 1435,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.RaceHouseRight => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 280, 1435,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.LibraryEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 313, 1310,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.ForestHideoutEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 380, 264,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.ForestChestGameEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 370, 40,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.CastleSecretEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1196, 834,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.CastleMainEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1000, 895,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn])
                },
                LocationID.CastleLeftEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 900, 780,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn])
                },
                LocationID.CastleRightEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1105, 780,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn])
                },
                LocationID.CastleTowerEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1000, 800,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn])
                },
                LocationID.DamEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 942, 1880,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.CentralBonkRocksEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 945, 1310,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.WitchsHutEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1607, 670,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.WaterfallFairyEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1806, 286,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.SahasrahlasHutEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1625, 900,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.TreesFairyCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1650, 1295,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.PegsFairyCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1970, 1405,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.EasternPalaceEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1925, 820,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn])
                },
                LocationID.HoulihanHole => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1290, 625,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.SanctuaryGrave => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1040, 590,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.NorthBonkRocks => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 777, 590,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.KingsTombEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1207, 598,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.GraveyardLedgeEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1132, 549,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.DesertLeftEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 70, 1590,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn])
                },
                LocationID.DesertBackEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 150, 1540,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn])
                },
                LocationID.DesertRightEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 225, 1590,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn])
                },
                LocationID.DesertFrontEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 150, 1600,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn])
                },
                LocationID.AginahsCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 400, 1655,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.ThiefCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 555, 1790,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.RupeeCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 625, 1920,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.SkullWoodsBack => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 80, 100,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn])
                },
                LocationID.ThievesTownEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 255, 975,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn])
                },
                LocationID.CShapedHouseEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 414, 969,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.HammerHouse => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 408, 1069,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.DarkVillageFortuneTellerEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 377, 647,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.DarkChapelEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 924, 551,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.ShieldShop => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 665, 922,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.DarkLumberjack => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 675, 115,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.TreasureGameEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 100, 936,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.BombableShackEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 219, 1171,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.HammerPegsEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 636, 1214,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.BumperCaveExit => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 720, 310,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.BumperCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 715, 355,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.HypeCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1200, 1560,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.SwampPalaceEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 940, 1875,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn])
                },
                LocationID.DarkCentralBonkRocksEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 945, 1310,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.SouthOfGroveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 552, 1693,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.BombShop => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1098, 1382,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.ArrowGameEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 431, 1409,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.DarkHyliaFortuneTeller => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1300, 1615,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.DarkTreesFairyCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1656, 1296,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.DarkSahasrahlaEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1706, 1008,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.PalaceOfDarknessEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1925, 830,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn])
                },
                LocationID.DarkWitchsHut => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1616, 678,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.DarkFluteSpotFiveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1968, 1405,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.FatFairyEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 940, 976,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.GanonHole => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 902, 862,
                        new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted],
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleAll]
                        })),
                    new MapLocation(
                        id, MapID.DarkWorld, 1000, 820,
                        new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateNonInverted],
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleAll]
                        }))
                },
                LocationID.DarkIceRodCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1795, 1545,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.DarkFakeIceRodCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1835, 1545,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.DarkIceRodRockEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1810, 1585,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.HypeFairyCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1200, 1565,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.FortuneTellerEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1300, 1615,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.LakeShop => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1460, 1540,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.UpgradeFairy => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1590, 1710,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.MiniMoldormCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1309, 1887,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.IceRodCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1795, 1545,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.IceBeeCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1835, 1545,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.IceFairyCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1810, 1585,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.IcePalaceEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1600, 1735,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn])
                },
                LocationID.MiseryMireEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 150, 1650,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn])
                },
                LocationID.MireShackEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 77, 1600,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.MireRightShackEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 220, 1610,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.MireCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 400, 1655,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.CheckerboardCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 354, 1560,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.DeathMountainEntranceBack => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 816, 378,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.OldManResidence => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 900, 470,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.OldManBackResidence => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1075, 325,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.DeathMountainExitFront => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 790, 275,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.SpectacleRockLeft => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 920, 280,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.SpectacleRockRight => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 980, 290,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.SpectacleRockTop => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 980, 205,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.SpikeCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1151, 294,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.DarkMountainFairyEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 815, 376,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.TowerOfHeraEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1125, 65,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn])
                },
                LocationID.SpiralCaveBottom => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1605, 260,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.EDMFairyCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1695, 290,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.ParadoxCaveMiddle => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1735, 290,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.ParadoxCaveBottom => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1731, 434,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.EDMConnectorBottom => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1645, 275,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.SpiralCaveTop => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1600, 180,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.MimicCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1695, 180,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.EDMConnectorTop => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1645, 230,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.ParadoxCaveTop => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1725, 125,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.SuperBunnyCaveBottom => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1685, 295,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.DeathMountainShop => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1725, 295,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.SuperBunnyCaveTop => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1725, 128,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.HookshotCaveEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1670, 126,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.TurtleRockEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1890, 165,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn])
                },
                LocationID.GanonsTowerEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1125, 70,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn])
                },
                LocationID.TRLedgeLeft => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1598, 182,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn])
                },
                LocationID.TRLedgeRight => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1694, 182,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn])
                },
                LocationID.TRSafetyDoor => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1648, 229,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn])
                },
                LocationID.HookshotCaveTop => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1627, 40,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleAll])
                },
                LocationID.LinksHouseEntrance => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1098, 1382,
                        RequirementDictionary.Instance[RequirementType.WorldStateInvertedEntranceShuffleAll])
                },
                LocationID.TreesFairyCaveTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1650, 1295,
                        RequirementDictionary.Instance[RequirementType.WorldStateRetroEntranceShuffleNotAll])
                },
                LocationID.PegsFairyCaveTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1970, 1405,
                        RequirementDictionary.Instance[RequirementType.WorldStateRetroEntranceShuffleNotAll])
                },
                LocationID.KakarikoFortuneTellerTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 375, 645,
                        RequirementDictionary.Instance[RequirementType.WorldStateRetroEntranceShuffleNotAll])
                },
                LocationID.GrassHouseTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 410, 1075,
                        RequirementDictionary.Instance[RequirementType.WorldStateRetroEntranceShuffleNotAll])
                },
                LocationID.ForestChestGameTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 370, 40,
                        RequirementDictionary.Instance[RequirementType.WorldStateRetroEntranceShuffleNotAll])
                },
                LocationID.LumberjackHouseTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 688, 120,
                        RequirementDictionary.Instance[RequirementType.WorldStateRetroEntranceShuffleNotAll])
                },
                LocationID.LeftSnitchHouseTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 100, 940,
                        RequirementDictionary.Instance[RequirementType.WorldStateRetroEntranceShuffleNotAll])
                },
                LocationID.RightSnitchHouseTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 415, 965,
                        RequirementDictionary.Instance[RequirementType.WorldStateRetroEntranceShuffleNotAll])
                },
                LocationID.BombHutTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 55, 1195,
                        RequirementDictionary.Instance[RequirementType.WorldStateRetroEntranceShuffleNotAll])
                },
                LocationID.IceFairyCaveTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1810, 1602,
                        RequirementDictionary.Instance[RequirementType.WorldStateRetroEntranceShuffleNotAll])
                },
                LocationID.RupeeCaveTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 625, 1920,
                        RequirementDictionary.Instance[RequirementType.WorldStateRetroEntranceShuffleNotAll])
                },
                LocationID.CentralBonkRocksTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 945, 1310,
                        RequirementDictionary.Instance[RequirementType.WorldStateRetroEntranceShuffleNotAll])
                },
                LocationID.ThiefCaveTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 555, 1790,
                        RequirementDictionary.Instance[RequirementType.WorldStateRetroEntranceShuffleNotAll])
                },
                LocationID.IceBeeCaveTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1850, 1545,
                        RequirementDictionary.Instance[RequirementType.WorldStateRetroEntranceShuffleNotAll])
                },
                LocationID.FortuneTellerTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1300, 1615,
                        RequirementDictionary.Instance[RequirementType.WorldStateRetroEntranceShuffleNotAll])
                },
                LocationID.HypeFairyCaveTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1200, 1565,
                        RequirementDictionary.Instance[RequirementType.WorldStateRetroEntranceShuffleNotAll])
                },
                LocationID.ChestGameTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 425, 1410,
                        RequirementDictionary.Instance[RequirementType.WorldStateRetroEntranceShuffleNotAll])
                },
                LocationID.EDMFairyCaveTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.LightWorld, 1695, 290,
                        RequirementDictionary.Instance[RequirementType.WorldStateRetroEntranceShuffleNotAll])
                },
                LocationID.DarkChapelTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 924, 551,
                        RequirementDictionary.Instance[RequirementType.WorldStateRetroEntranceShuffleNotAll])
                },
                LocationID.DarkVillageFortuneTellerTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 377, 647,
                        RequirementDictionary.Instance[RequirementType.WorldStateRetroEntranceShuffleNotAll])
                },
                LocationID.DarkTreesFairyCaveTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1656, 1296,
                        RequirementDictionary.Instance[RequirementType.WorldStateRetroEntranceShuffleNotAll])
                },
                LocationID.DarkSahasrahlaTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1968, 1405,
                        RequirementDictionary.Instance[RequirementType.WorldStateRetroEntranceShuffleNotAll])
                },
                LocationID.DarkFluteSpotFiveTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 431, 1409,
                        RequirementDictionary.Instance[RequirementType.WorldStateRetroEntranceShuffleNotAll])
                },
                LocationID.ArrowGameTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 945, 1310,
                        RequirementDictionary.Instance[RequirementType.WorldStateRetroEntranceShuffleNotAll])
                },
                LocationID.DarkCentralBonkRocksTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1795, 1545,
                        RequirementDictionary.Instance[RequirementType.WorldStateRetroEntranceShuffleNotAll])
                },
                LocationID.DarkIceRodCaveTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1835, 1545,
                        RequirementDictionary.Instance[RequirementType.WorldStateRetroEntranceShuffleNotAll])
                },
                LocationID.DarkFakeIceRodCaveTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1835, 1545,
                        RequirementDictionary.Instance[RequirementType.WorldStateRetroEntranceShuffleNotAll])
                },
                LocationID.DarkIceRodRockTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 1810, 1585,
                        RequirementDictionary.Instance[RequirementType.WorldStateRetroEntranceShuffleNotAll])
                },
                LocationID.DarkMountainFairyTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 815, 376,
                        RequirementDictionary.Instance[RequirementType.WorldStateRetroEntranceShuffleNotAll])
                },
                LocationID.MireRightShackTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 220, 1610,
                        RequirementDictionary.Instance[RequirementType.WorldStateRetroEntranceShuffleNotAll])
                },
                LocationID.MireCaveTakeAny => new List<MapLocation>
                {
                    new MapLocation(
                        id, MapID.DarkWorld, 400, 1655,
                        RequirementDictionary.Instance[RequirementType.WorldStateRetroEntranceShuffleNotAll])
                },
                _ => throw new ArgumentOutOfRangeException(nameof(id))
            };
        }
    }
}
