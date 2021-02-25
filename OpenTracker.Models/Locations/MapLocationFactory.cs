using OpenTracker.Models.Requirements;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.Locations
{
    /// <summary>
    /// This class contains the creation logic for map location data.
    /// </summary>
    public class MapLocationFactory : IMapLocationFactory
    {
        private readonly IRequirementDictionary _requirements;
        private readonly IMapLocation.Factory _factory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requirements">
        /// The requirement dictionary.
        /// </param>
        /// <param name="factory">
        /// An Autofac factory for creating map locations.
        /// </param>
        public MapLocationFactory(
            IRequirementDictionary requirements, IMapLocation.Factory factory)
        {
            _requirements = requirements;
            _factory = factory;
        }

        /// <summary>
        /// Returns the list of map locations for the specified location.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <returns>
        /// The list of map locations.
        /// </returns>
        public List<IMapLocation> GetMapLocations(ILocation location)
        {
            return location.ID switch
            {
                LocationID.LinksHouse => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1097, 1366, location,
                        _requirements[RequirementType.WorldStateStandardOpen]),
                    _factory(MapID.DarkWorld, 1097, 1366, location,
                        _requirements[RequirementType.WorldStateInvertedEntranceShuffleNoneDungeon])
                },
                LocationID.Pedestal => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 83, 101, location,
                        _requirements[RequirementType.NoRequirement])
                },
                LocationID.LumberjackCave => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 633, 117, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.BlindsHouse => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 307, 840, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.TheWell => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 47, 833, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.BottleVendor => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 190, 933, location,
                        _requirements[RequirementType.NoRequirement])
                },
                LocationID.ChickenHouse => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 197, 1066, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.Tavern => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 320, 1145, location,
                        _requirements[RequirementType.NoRequirement])
                },
                LocationID.SickKid => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 314, 1060, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.MagicBat => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 650, 1127, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.RaceGame => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 111, 1354, location,
                        _requirements[RequirementType.NoRequirement])
                },
                LocationID.Library => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 313, 1310, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.MushroomSpot => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 244, 170, location,
                        _requirements[RequirementType.NoRequirement])
                },
                LocationID.ForestHideout => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 380, 264, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.CastleSecret => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1196, 834, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.WitchsHut => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1607, 665, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.SahasrahlasHut => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1625, 900, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.BonkRocks => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 777, 590, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.KingsTomb => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1207, 598, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.AginahsCave => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 400, 1655, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.GroveDiggingSpot => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 600, 1350, location,
                        _requirements[RequirementType.NoRequirement])
                },
                LocationID.Dam => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 942, 1880, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon]),
                    _factory(MapID.LightWorld, 900, 1860, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity]),
                },
                LocationID.MiniMoldormCave => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1309, 1887, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.IceRodCave => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1795, 1547, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.Hobo => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1390, 1390, location,
                        _requirements[RequirementType.NoRequirement])
                },
                LocationID.PyramidLedge => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1164, 922, location,
                        _requirements[RequirementType.NoRequirement])
                },
                LocationID.FatFairy => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 940, 976, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.HauntedGrove => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 620, 1371, location,
                        _requirements[RequirementType.NoRequirement])
                },
                LocationID.HypeCave => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1200, 1560, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.BombosTablet => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 440, 1845, location,
                        _requirements[RequirementType.NoRequirement]),
                    _factory(MapID.DarkWorld, 440, 1845, location,
                        _requirements[RequirementType.WorldStateStandardOpen])
                },
                LocationID.SouthOfGrove => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 552, 1693, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon]),
                    _factory(MapID.DarkWorld, 552, 1693, location,
                        _requirements[RequirementType.WorldStateStandardOpenEntranceShuffleNoneDungeon])
                },
                LocationID.DiggingGame => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 100, 1385, location,
                        _requirements[RequirementType.NoRequirement])
                },
                LocationID.WaterfallFairy => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1806, 286, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.ZoraArea => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1920, 273, location,
                        _requirements[RequirementType.NoRequirement])
                },
                LocationID.Catfish => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1813, 347, location,
                        _requirements[RequirementType.NoRequirement])
                },
                LocationID.GraveyardLedge => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1132, 549, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon]),
                    _factory(MapID.DarkWorld, 1132, 530, location,
                        _requirements[RequirementType.WorldStateStandardOpenEntranceShuffleNoneDungeon])
                },
                LocationID.DesertLedge => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 40, 1835, location,
                        _requirements[RequirementType.NoRequirement])
                },
                LocationID.CShapedHouse => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 414, 969, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.TreasureGame => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 100, 936, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.BombableShack => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 219, 1171, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.Blacksmith => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 616, 1054, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon]),
                    _factory(MapID.DarkWorld, 295, 1325, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.PurpleChest => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 680, 1805, location,
                        _requirements[RequirementType.NoRequirement]),
                    _factory(MapID.DarkWorld, 601, 1050, location,
                        _requirements[RequirementType.NoRequirement])
                },
                LocationID.HammerPegs => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 636, 1214, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.BumperCave => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 687, 340, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon]),
                    _factory(MapID.DarkWorld, 680, 315, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.LakeHyliaIsland => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1450, 1666, location,
                        _requirements[RequirementType.NoRequirement]),
                    _factory(MapID.DarkWorld, 1450, 1666, location,
                        _requirements[RequirementType.WorldStateStandardOpen])
                },
                LocationID.MireShack => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 77, 1600, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.CheckerboardCave => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 354, 1560, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon]),
                    _factory(MapID.DarkWorld, 334, 1557, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.OldMan => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 816, 378, location,
                        _requirements[RequirementType.WorldStateInvertedEntranceShuffleNoneDungeon]),
                    _factory(MapID.LightWorld, 900, 440, location,
                        _requirements[RequirementType.WorldStateStandardOpenEntranceShuffleNoneDungeon]),
                    _factory(MapID.DarkWorld, 816, 378, location,
                        _requirements[RequirementType.WorldStateInvertedEntranceShuffleNoneDungeon])
                },
                LocationID.SpectacleRock => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 980, 178, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon]),
                    _factory(MapID.LightWorld, 1036, 170, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.EtherTablet => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 844, 38, location,
                        _requirements[RequirementType.NoRequirement])
                },
                LocationID.SpikeCave => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1151, 294, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.SpiralCave => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1598, 180, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.ParadoxCave => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1731, 434, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.SuperBunnyCave => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1695, 290, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.HookshotCave => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1670, 126, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.FloatingIsland => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1627, 40, location,
                        _requirements[RequirementType.NoRequirement]),
                    _factory(MapID.DarkWorld, 1627, 40, location,
                        _requirements[RequirementType.WorldStateStandardOpenEntranceShuffleNoneDungeon])
                },
                LocationID.MimicCave => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1694, 190, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon]),
                    _factory(MapID.DarkWorld, 1693, 205, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeon])
                },
                LocationID.HyruleCastle => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1003, 926, location,
                        _requirements[RequirementType.EntranceShuffleNone]),
                    _factory(MapID.LightWorld, 925, 540, location,
                        _requirements[RequirementType.EntranceShuffleNoneDungeonAll]),
                    _factory(MapID.LightWorld, 925, 516, location,
                        _requirements[RequirementType.EntranceShuffleInsanity])
                },
                LocationID.AgahnimTower => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1003, 797, location,
                        _requirements[RequirementType.WorldStateStandardOpenEntranceShuffleNone]),
                    _factory(MapID.DarkWorld, 1126, 68, location,
                        _requirements[RequirementType.WorldStateInvertedEntranceShuffleNone]),
                    _factory(MapID.LightWorld, 1000, 750, location,
                        _requirements[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.EasternPalace => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1925, 791, location,
                        _requirements[RequirementType.EntranceShuffleNone]),
                    _factory(MapID.LightWorld, 1925, 780, location,
                        _requirements[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.DesertPalace => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 146, 1584, location,
                        _requirements[RequirementType.EntranceShuffleNone]),
                    _factory(MapID.LightWorld, 150, 1700, location,
                        _requirements[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.TowerOfHera => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1126, 68, location,
                        _requirements[RequirementType.EntranceShuffleNone]),
                    _factory(MapID.LightWorld, 1125, 20, location,
                        _requirements[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.PalaceOfDarkness => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1924, 800, location,
                        _requirements[RequirementType.EntranceShuffleNone]),
                    _factory(MapID.DarkWorld, 1925, 785, location,
                        _requirements[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.SwampPalace => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 940, 1880, location,
                        _requirements[RequirementType.EntranceShuffleNone]),
                    _factory(MapID.DarkWorld, 940, 1840, location,
                        _requirements[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.SkullWoods => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 79, 121, location,
                        _requirements[RequirementType.EntranceShuffleNone]),
                    _factory(MapID.DarkWorld, 80, 50, location,
                        _requirements[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.ThievesTown => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 251, 971, location,
                        _requirements[RequirementType.EntranceShuffleNone]),
                    _factory(MapID.DarkWorld, 255, 935, location,
                        _requirements[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.IcePalace => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1600, 1735, location,
                        _requirements[RequirementType.EntranceShuffleNone]),
                    _factory(MapID.DarkWorld, 1600, 1695, location,
                        _requirements[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.MiseryMire => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 150, 1710, location,
                        _requirements[RequirementType.EntranceShuffleNone]),
                    _factory(MapID.DarkWorld, 150, 1600, location,
                        _requirements[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.TurtleRock => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1890, 144, location,
                        _requirements[RequirementType.EntranceShuffleNone]),
                    _factory(MapID.DarkWorld, 1890, 125, location,
                        _requirements[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.GanonsTower => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1126, 68, location,
                        _requirements[RequirementType.WorldStateStandardOpenEntranceShuffleNone]),
                    _factory(MapID.LightWorld, 1003, 797, location,
                        _requirements[RequirementType.WorldStateInvertedEntranceShuffleNone]),
                    _factory(MapID.DarkWorld, 1125, 30, location,
                        _requirements[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.LumberjackHouseEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 675, 120, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.LumberjackCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 600, 145, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.DeathMountainEntryCave => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 715, 350, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.DeathMountainExitCave => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 720, 305, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.KakarikoFortuneTellerEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 375, 645, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.WomanLeftDoor => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 305, 840, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.WomanRightDoor => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 340, 840, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.LeftSnitchHouseEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 100, 940, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.RightSnitchHouseEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 415, 965, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.BlindsHouseEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 255, 840, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.TheWellEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 47, 833, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.ChickenHouseEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 197, 1066, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.GrassHouseEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 410, 1075, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.TavernFront => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 320, 1195, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.KakarikoShopEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 220, 1175, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.BombHutEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 55, 1195, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.SickKidEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 314, 1060, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.BlacksmithHouse => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 615, 1055, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.MagicBatEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 650, 1127, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.ChestGameEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 425, 1410, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.RaceHouseLeft => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 220, 1435, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.RaceHouseRight => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 280, 1435, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.LibraryEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 313, 1310, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.ForestHideoutEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 380, 264, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.ForestChestGameEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 370, 40, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.CastleSecretEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1196, 834, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.CastleMainEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1000, 895, location,
                        _requirements[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.CastleLeftEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 900, 780, location,
                        _requirements[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.CastleRightEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1105, 780, location,
                        _requirements[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.CastleTowerEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1000, 800, location,
                        _requirements[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.DamEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 942, 1880, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.CentralBonkRocksEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 945, 1310, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.WitchsHutEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1607, 670, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.WaterfallFairyEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1806, 286, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.SahasrahlasHutEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1625, 900, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.TreesFairyCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1650, 1295, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.PegsFairyCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1970, 1405, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.EasternPalaceEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1925, 820, location,
                        _requirements[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.HoulihanHole => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1290, 625, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.SanctuaryGrave => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1040, 590, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.NorthBonkRocks => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 777, 590, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.KingsTombEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1207, 598, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.GraveyardLedgeEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1132, 549, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.DesertLeftEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 70, 1590, location,
                        _requirements[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.DesertBackEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 150, 1540, location,
                        _requirements[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.DesertRightEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 225, 1590, location,
                        _requirements[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.DesertFrontEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 150, 1600, location,
                        _requirements[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.AginahsCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 400, 1655, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.ThiefCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 555, 1790, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.RupeeCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 625, 1920, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.SkullWoodsBack => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 80, 100, location,
                        _requirements[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.ThievesTownEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 255, 975, location,
                        _requirements[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.CShapedHouseEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 414, 969, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.HammerHouse => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 408, 1069, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.DarkVillageFortuneTellerEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 377, 647, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.DarkChapelEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 924, 551, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.ShieldShop => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 665, 922, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.DarkLumberjack => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 675, 115, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.TreasureGameEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 100, 936, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.BombableShackEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 219, 1171, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.HammerPegsEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 636, 1214, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.BumperCaveExit => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 720, 310, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.BumperCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 715, 355, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.HypeCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1200, 1560, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.SwampPalaceEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 940, 1875, location,
                        _requirements[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.DarkCentralBonkRocksEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 945, 1310, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.SouthOfGroveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 552, 1693, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.BombShop => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1098, 1382, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.ArrowGameEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 431, 1409, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.DarkHyliaFortuneTeller => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1300, 1615, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.DarkTreesFairyCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1656, 1296, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.DarkSahasrahlaEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1706, 1008, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.PalaceOfDarknessEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1925, 830, location,
                        _requirements[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.DarkWitchsHut => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1616, 678, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.DarkFluteSpotFiveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1968, 1405, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.FatFairyEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 940, 976, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.GanonHole => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1003, 832, location,
                        _requirements[RequirementType.WorldStateInvertedEntranceShuffleAllInsanity]),
                    _factory(MapID.DarkWorld, 1000, 820, location,
                        _requirements[RequirementType.WorldStateStandardOpenEntranceShuffleAllInsanity])
                },
                LocationID.DarkIceRodCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1795, 1545, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.DarkFakeIceRodCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1835, 1545, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.DarkIceRodRockEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1810, 1585, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.HypeFairyCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1200, 1565, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.FortuneTellerEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1300, 1615, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.LakeShop => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1460, 1540, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.UpgradeFairy => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1590, 1710, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.MiniMoldormCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1309, 1887, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.IceRodCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1795, 1545, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.IceBeeCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1835, 1545, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.IceFairyCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1810, 1585, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.IcePalaceEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1600, 1735, location,
                        _requirements[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.MiseryMireEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 150, 1650, location,
                        _requirements[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.MireShackEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 77, 1600, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.MireRightShackEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 220, 1610, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.MireCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 400, 1655, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.CheckerboardCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 354, 1560, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.DeathMountainEntranceBack => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 816, 378, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.OldManResidence => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 900, 470, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.OldManBackResidence => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1075, 325, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.DeathMountainExitFront => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 790, 275, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.SpectacleRockLeft => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 920, 280, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.SpectacleRockRight => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 980, 290, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.SpectacleRockTop => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 980, 205, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.SpikeCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1151, 294, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.DarkMountainFairyEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 815, 376, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.TowerOfHeraEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1125, 65, location,
                        _requirements[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.SpiralCaveBottom => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1605, 260, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.EDMFairyCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1695, 290, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.ParadoxCaveMiddle => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1735, 290, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.ParadoxCaveBottom => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1731, 434, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.EDMConnectorBottom => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1645, 275, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.SpiralCaveTop => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1600, 180, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.MimicCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1695, 180, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.EDMConnectorTop => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1645, 230, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.ParadoxCaveTop => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1725, 125, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.SuperBunnyCaveBottom => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1685, 295, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.DeathMountainShopEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1725, 295, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.SuperBunnyCaveTop => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1725, 128, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.HookshotCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1670, 126, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.TurtleRockEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1890, 165, location,
                        _requirements[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.GanonsTowerEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1125, 70, location,
                        _requirements[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.TRLedgeLeft => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1598, 182, location,
                        _requirements[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.TRLedgeRight => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1694, 182, location,
                        _requirements[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.TRSafetyDoor => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1648, 229, location,
                        _requirements[RequirementType.EntranceShuffleDungeonAllInsanity])
                },
                LocationID.HookshotCaveTop => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1627, 40, location,
                        _requirements[RequirementType.EntranceShuffleAllInsanity])
                },
                LocationID.LinksHouseEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1098, 1382, location,
                        _requirements[RequirementType.WorldStateInvertedEntranceShuffleAllInsanity])
                },
                LocationID.TreesFairyCaveTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1650, 1295, location,
                        _requirements[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.PegsFairyCaveTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1970, 1405, location,
                        _requirements[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.KakarikoFortuneTellerTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 375, 645, location,
                        _requirements[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.GrassHouseTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 410, 1075, location,
                        _requirements[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.ForestChestGameTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 370, 40, location,
                        _requirements[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.LumberjackHouseTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 688, 120, location,
                        _requirements[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.LeftSnitchHouseTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 100, 940, location,
                        _requirements[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.RightSnitchHouseTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 415, 965, location,
                        _requirements[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.BombHutTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 55, 1195, location,
                        _requirements[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.IceFairyCaveTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1810, 1602, location,
                        _requirements[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.RupeeCaveTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 625, 1920, location,
                        _requirements[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.CentralBonkRocksTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 945, 1310, location,
                        _requirements[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.ThiefCaveTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 555, 1790, location,
                        _requirements[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.IceBeeCaveTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1850, 1545, location,
                        _requirements[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.FortuneTellerTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1300, 1615, location,
                        _requirements[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.HypeFairyCaveTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1200, 1565, location,
                        _requirements[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.ChestGameTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 425, 1410, location,
                        _requirements[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.EDMFairyCaveTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1695, 290, location,
                        _requirements[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.DarkChapelTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 924, 551, location,
                        _requirements[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.DarkVillageFortuneTellerTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 377, 647, location,
                        _requirements[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.DarkTreesFairyCaveTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1656, 1296, location,
                        _requirements[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.DarkSahasrahlaTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1706, 1008, location,
                        _requirements[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.DarkFluteSpotFiveTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1968, 1405, location,
                        _requirements[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.ArrowGameTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 431, 1409, location,
                        _requirements[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.DarkCentralBonkRocksTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 945, 1310, location,
                        _requirements[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.DarkIceRodCaveTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1795, 1545, location,
                        _requirements[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.DarkFakeIceRodCaveTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1835, 1545, location,
                        _requirements[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.DarkIceRodRockTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1810, 1585, location,
                        _requirements[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.DarkMountainFairyTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 815, 376, location,
                        _requirements[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.MireRightShackTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 220, 1610, location,
                        _requirements[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.MireCaveTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 400, 1655, location,
                        _requirements[RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon])
                },
                LocationID.LumberjackCaveExit => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 666, 64, location,
                        _requirements[RequirementType.EntranceShuffleInsanity])
                },
                LocationID.TheWellExit => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 94, 854, location,
                        _requirements[RequirementType.EntranceShuffleInsanity])
                },
                LocationID.MagicBatExit => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 634, 1112, location,
                        _requirements[RequirementType.EntranceShuffleInsanity])
                },
                LocationID.ForestHideoutExit => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 368, 301, location,
                        _requirements[RequirementType.EntranceShuffleInsanity])
                },
                LocationID.CastleSecretExit => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1105, 860, location,
                        _requirements[RequirementType.EntranceShuffleInsanity])
                },
                LocationID.HoulihanHoleExit => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1340, 548, location,
                        _requirements[RequirementType.EntranceShuffleInsanity])
                },
                LocationID.Sanctuary => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 924, 537, location,
                        _requirements[RequirementType.EntranceShuffleInsanity])
                },
                LocationID.GanonHoleExit => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 902, 862, location,
                        _requirements[RequirementType.WorldStateInvertedEntranceShuffleInsanity])
                },
                LocationID.SkullWoodsWestEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 117, 260, location,
                        _requirements[RequirementType.EntranceShuffleInsanity])
                },
                LocationID.SkullWoodsCenterEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 290, 291, location,
                        _requirements[RequirementType.EntranceShuffleInsanity])
                },
                LocationID.SkullWoodsEastEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 368, 299, location,
                        _requirements[RequirementType.EntranceShuffleInsanity])
                },
                LocationID.SkullWoodsNWHole => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 243, 180, location,
                        _requirements[RequirementType.EntranceShuffleInsanity])
                },
                LocationID.SkullWoodsSWHole => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 313, 352, location,
                        _requirements[RequirementType.EntranceShuffleInsanity])
                },
                LocationID.SkullWoodsSEHole => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 392, 337, location,
                        _requirements[RequirementType.EntranceShuffleInsanity])
                },
                LocationID.SkullWoodsNEHole => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 380, 262, location,
                        _requirements[RequirementType.EntranceShuffleInsanity])
                },
                LocationID.KakarikoShop => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 220, 1175, location,
                        _requirements[RequirementType.TakeAnyLocationsOrShopShuffleEntranceShuffleNoneDungeon])
                },
                LocationID.LakeHyliaShop => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1460, 1540, location,
                        _requirements[RequirementType.TakeAnyLocationsOrShopShuffleEntranceShuffleNoneDungeon])
                },
                LocationID.DeathMountainShop => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1735, 290, location,
                        _requirements[RequirementType.TakeAnyLocationsOrShopShuffleEntranceShuffleNoneDungeon])
                },
                LocationID.PotionShop => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1607, 715, location,
                        _requirements[RequirementType.ShopShuffleEntranceShuffleNoneDungeon])
                },
                LocationID.DarkLumberjackShop => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 675, 115, location,
                        _requirements[RequirementType.TakeAnyLocationsOrShopShuffleEntranceShuffleNoneDungeon])
                },
                LocationID.RedShieldShop => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 665, 922, location,
                        _requirements[RequirementType.TakeAnyLocationsOrShopShuffleEntranceShuffleNoneDungeon])
                },
                LocationID.VillageOfOutcastsShop => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 408, 1069, location,
                        _requirements[RequirementType.TakeAnyLocationsOrShopShuffleEntranceShuffleNoneDungeon])
                },
                LocationID.DarkLakeHyliaShop => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1460, 1540, location,
                        _requirements[RequirementType.TakeAnyLocationsOrShopShuffleEntranceShuffleNoneDungeon])
                },
                LocationID.DarkPotionShop => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1607, 670, location,
                        _requirements[RequirementType.TakeAnyLocationsOrShopShuffleEntranceShuffleNoneDungeon])
                },
                LocationID.DarkDeathMountainShop => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1725, 360, location,
                        _requirements[RequirementType.TakeAnyLocationsOrShopShuffleEntranceShuffleNoneDungeon])
                },
                _ => throw new ArgumentOutOfRangeException(nameof(location))
            };
        }
    }
}
