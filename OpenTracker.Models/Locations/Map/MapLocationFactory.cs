using System;
using System.Collections.Generic;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Aggregate;
using OpenTracker.Models.Requirements.Alternative;
using OpenTracker.Models.Requirements.Mode;

namespace OpenTracker.Models.Locations.Map
{
    /// <summary>
    /// This class contains the creation logic for map location data.
    /// </summary>
    public class MapLocationFactory : IMapLocationFactory
    {
        private readonly IAlternativeRequirementDictionary _alternativeRequirements;
        private readonly IAggregateRequirementDictionary _aggregateRequirements;
        private readonly IEntranceShuffleRequirementDictionary _entranceShuffleRequirements;
        private readonly IWorldStateRequirementDictionary _worldStateRequirements;
        
        private readonly IMapLocation.Factory _factory;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="alternativeRequirements">
        ///     The alternative requirement dictionary.
        /// </param>
        /// <param name="aggregateRequirements">
        ///     The aggregate requirement dictionary.
        /// </param>
        /// <param name="entranceShuffleRequirements">
        ///     The entrance shuffle requirement dictionary.
        /// </param>
        /// <param name="worldStateRequirements">
        ///     The world state requirement dictionary.
        /// </param>
        /// <param name="factory">
        ///     An Autofac factory for creating map locations.
        /// </param>
        public MapLocationFactory(
            IAlternativeRequirementDictionary alternativeRequirements,
            IAggregateRequirementDictionary aggregateRequirements,
            IEntranceShuffleRequirementDictionary entranceShuffleRequirements,
            IWorldStateRequirementDictionary worldStateRequirements, IMapLocation.Factory factory)
        {
            _alternativeRequirements = alternativeRequirements;
            _aggregateRequirements = aggregateRequirements;
            _entranceShuffleRequirements = entranceShuffleRequirements;
            _worldStateRequirements = worldStateRequirements;

            _factory = factory;
        }

        /// <summary>
        /// Returns the list of map locations for the specified location.
        /// </summary>
        /// <param name="location">
        ///     The location data.
        /// </param>
        /// <returns>
        ///     The list of map locations.
        /// </returns>
        public IList<IMapLocation> GetMapLocations(ILocation location)
        {
            return location.ID switch
            {
                LocationID.LinksHouse => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1097, 1366, location,
                        _worldStateRequirements[WorldState.StandardOpen]),
                    _factory(MapID.DarkWorld, 1097, 1366, location,
                        _aggregateRequirements[new HashSet<IRequirement>
                        {
                            _alternativeRequirements[new HashSet<IRequirement>
                            {
                                _entranceShuffleRequirements[EntranceShuffle.None],
                                _entranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }],
                            _worldStateRequirements[WorldState.Inverted]
                        }])
                },
                LocationID.Pedestal => new List<IMapLocation> {_factory(MapID.LightWorld, 83, 101, location)},
                LocationID.LumberjackCave => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 633, 117, location)
                },
                LocationID.BlindsHouse => new List<IMapLocation> {_factory(MapID.LightWorld, 307, 840, location)},
                LocationID.TheWell => new List<IMapLocation> {_factory(MapID.LightWorld, 47, 833, location)},
                LocationID.BottleVendor => new List<IMapLocation> {_factory(MapID.LightWorld, 190, 933, location)},
                LocationID.ChickenHouse => new List<IMapLocation> {_factory(MapID.LightWorld, 197, 1066, location)},
                LocationID.Tavern => new List<IMapLocation> {_factory(MapID.LightWorld, 320, 1145, location)},
                LocationID.SickKid => new List<IMapLocation> {_factory(MapID.LightWorld, 314, 1060, location)},
                LocationID.MagicBat => new List<IMapLocation> {_factory(MapID.LightWorld, 650, 1127, location)},
                LocationID.RaceGame => new List<IMapLocation> {_factory(MapID.LightWorld, 111, 1354, location)},
                LocationID.Library => new List<IMapLocation> {_factory(MapID.LightWorld, 313, 1310, location)},
                LocationID.MushroomSpot => new List<IMapLocation> {_factory(MapID.LightWorld, 244, 170, location)},
                LocationID.ForestHideout => new List<IMapLocation> {_factory(MapID.LightWorld, 380, 264, location)},
                LocationID.CastleSecret => new List<IMapLocation> {_factory(MapID.LightWorld, 1196, 834, location)},
                LocationID.WitchsHut => new List<IMapLocation> {_factory(MapID.LightWorld, 1607, 665, location)},
                LocationID.SahasrahlasHut => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1625, 900, location)
                },
                LocationID.BonkRocks => new List<IMapLocation> {_factory(MapID.LightWorld, 777, 590, location)},
                LocationID.KingsTomb => new List<IMapLocation> {_factory(MapID.LightWorld, 1207, 598, location)},
                LocationID.AginahsCave => new List<IMapLocation> {_factory(MapID.LightWorld, 400, 1655, location)},
                LocationID.GroveDiggingSpot => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 600, 1350, location)
                },
                LocationID.Dam => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 942, 1880, location,
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.None],
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon]
                        }]),
                    _factory(MapID.LightWorld, 900, 1860, location,
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.All],
                            _entranceShuffleRequirements[EntranceShuffle.Insanity]
                        }]),
                },
                LocationID.MiniMoldormCave => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1309, 1887, location)
                },
                LocationID.IceRodCave => new List<IMapLocation> {_factory(MapID.LightWorld, 1795, 1547, location)},
                LocationID.Hobo => new List<IMapLocation> {_factory(MapID.LightWorld, 1390, 1390, location)},
                LocationID.PyramidLedge => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1164, 922, location)
                },
                LocationID.FatFairy => new List<IMapLocation> {_factory(MapID.DarkWorld, 940, 976, location)},
                LocationID.HauntedGrove => new List<IMapLocation> {_factory(MapID.DarkWorld, 620, 1371, location)},
                LocationID.HypeCave => new List<IMapLocation> {_factory(MapID.DarkWorld, 1200, 1560, location)},
                LocationID.BombosTablet => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 440, 1845, location),
                    _factory(MapID.DarkWorld, 440, 1845, location,
                        _worldStateRequirements[WorldState.StandardOpen])
                },
                LocationID.SouthOfGrove => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 552, 1693, location),
                    _factory(MapID.DarkWorld, 552, 1693, location,
                        _worldStateRequirements[WorldState.StandardOpen])
                },
                LocationID.DiggingGame => new List<IMapLocation> {_factory(MapID.DarkWorld, 100, 1385, location)},
                LocationID.WaterfallFairy => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1806, 286, location)
                },
                LocationID.ZoraArea => new List<IMapLocation> {_factory(MapID.LightWorld, 1920, 273, location)},
                LocationID.Catfish => new List<IMapLocation> {_factory(MapID.DarkWorld, 1813, 347, location)},
                LocationID.GraveyardLedge => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1132, 549, location),
                    _factory(MapID.DarkWorld, 1132, 530, location,
                        _worldStateRequirements[WorldState.StandardOpen])
                },
                LocationID.DesertLedge => new List<IMapLocation> {_factory(MapID.LightWorld, 40, 1835, location)},
                LocationID.CShapedHouse => new List<IMapLocation> {_factory(MapID.DarkWorld, 414, 969, location)},
                LocationID.TreasureGame => new List<IMapLocation> {_factory(MapID.DarkWorld, 100, 936, location)},
                LocationID.BombableShack => new List<IMapLocation> {_factory(MapID.DarkWorld, 219, 1171, location)},
                LocationID.Blacksmith => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 616, 1054, location,
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.None],
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon]
                        }]),
                    _factory(MapID.DarkWorld, 295, 1325, location)
                },
                LocationID.PurpleChest => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 680, 1805, location),
                    _factory(MapID.DarkWorld, 601, 1050, location)
                },
                LocationID.HammerPegs => new List<IMapLocation> {_factory(MapID.DarkWorld, 636, 1214, location)},
                LocationID.BumperCave => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 687, 340, location,
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.None],
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon]
                        }]),
                    _factory(MapID.DarkWorld, 680, 315, location,
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.All],
                            _entranceShuffleRequirements[EntranceShuffle.Insanity]
                        }])
                },
                LocationID.LakeHyliaIsland => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1450, 1666, location),
                    _factory(MapID.DarkWorld, 1450, 1666, location,
                        _worldStateRequirements[WorldState.StandardOpen])
                },
                LocationID.MireShack => new List<IMapLocation> {_factory(MapID.DarkWorld, 77, 1600, location)},
                LocationID.CheckerboardCave => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 354, 1560, location),
                    _factory(MapID.DarkWorld, 334, 1557, location,
                        _worldStateRequirements[WorldState.StandardOpen])
                },
                LocationID.OldMan => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 816, 378, location,
                        _aggregateRequirements[new HashSet<IRequirement>
                        {
                            _alternativeRequirements[new HashSet<IRequirement>
                            {
                                _entranceShuffleRequirements[EntranceShuffle.None],
                                _entranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }],
                            _worldStateRequirements[WorldState.Inverted]
                        }]),
                    _factory(MapID.LightWorld, 900, 440, location,
                        _aggregateRequirements[new HashSet<IRequirement>
                        {
                            _alternativeRequirements[new HashSet<IRequirement>
                            {
                                _entranceShuffleRequirements[EntranceShuffle.All],
                                _entranceShuffleRequirements[EntranceShuffle.Insanity]
                            }],
                            _worldStateRequirements[WorldState.StandardOpen]
                        }]),
                    _factory(MapID.DarkWorld, 816, 378, location,
                        _aggregateRequirements[new HashSet<IRequirement>
                        {
                            _alternativeRequirements[new HashSet<IRequirement>
                            {
                                _entranceShuffleRequirements[EntranceShuffle.None],
                                _entranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }],
                            _worldStateRequirements[WorldState.Inverted]
                        }])
                },
                LocationID.SpectacleRock => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 980, 178, location,
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.None],
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon]
                        }]),
                    _factory(MapID.LightWorld, 1036, 170, location,
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.All],
                            _entranceShuffleRequirements[EntranceShuffle.Insanity]
                        }])
                },
                LocationID.EtherTablet => new List<IMapLocation> {_factory(MapID.LightWorld, 844, 38, location)},
                LocationID.SpikeCave => new List<IMapLocation> {_factory(MapID.DarkWorld, 1151, 294, location)},
                LocationID.SpiralCave => new List<IMapLocation> {_factory(MapID.LightWorld, 1598, 180, location)},
                LocationID.ParadoxCave => new List<IMapLocation> {_factory(MapID.LightWorld, 1731, 434, location)},
                LocationID.SuperBunnyCave => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1695, 290, location)
                },
                LocationID.HookshotCave => new List<IMapLocation> {_factory(MapID.DarkWorld, 1670, 126, location)},
                LocationID.FloatingIsland => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1627, 40, location),
                    _factory(MapID.DarkWorld, 1627, 40, location,
                        _aggregateRequirements[new HashSet<IRequirement>
                        {
                            _alternativeRequirements[new HashSet<IRequirement>
                            {
                                _entranceShuffleRequirements[EntranceShuffle.None],
                                _entranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }],
                            _worldStateRequirements[WorldState.StandardOpen]
                        }])
                },
                LocationID.MimicCave => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1694, 190, location),
                    _factory(MapID.DarkWorld, 1693, 205, location,
                        _aggregateRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.None],
                            _worldStateRequirements[WorldState.StandardOpen]  
                        }])
                },
                LocationID.HyruleCastle => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1003, 926, location,
                        _entranceShuffleRequirements[EntranceShuffle.None]),
                    _factory(MapID.LightWorld, 925, 540, location,
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.None],
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon],
                            _entranceShuffleRequirements[EntranceShuffle.All]
                        }]),
                    _factory(MapID.LightWorld, 925, 516, location,
                        _entranceShuffleRequirements[EntranceShuffle.Insanity])
                },
                LocationID.AgahnimTower => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1003, 797, location,
                        _aggregateRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.None],
                            _worldStateRequirements[WorldState.StandardOpen]
                        }]),
                    _factory(MapID.DarkWorld, 1126, 68, location,
                        _aggregateRequirements[new HashSet<IRequirement>
                        {
                            _worldStateRequirements[WorldState.Inverted],
                            _entranceShuffleRequirements[EntranceShuffle.None]
                        }]),
                    _factory(MapID.LightWorld, 1000, 750, location,
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon],
                            _entranceShuffleRequirements[EntranceShuffle.All],
                            _entranceShuffleRequirements[EntranceShuffle.Insanity]
                        }])
                },
                LocationID.EasternPalace => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1925, 791, location,
                        _entranceShuffleRequirements[EntranceShuffle.None]),
                    _factory(MapID.LightWorld, 1925, 780, location,
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon],
                            _entranceShuffleRequirements[EntranceShuffle.All],
                            _entranceShuffleRequirements[EntranceShuffle.Insanity]
                        }])
                },
                LocationID.DesertPalace => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 146, 1584, location,
                        _entranceShuffleRequirements[EntranceShuffle.None]),
                    _factory(MapID.LightWorld, 150, 1700, location,
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon],
                            _entranceShuffleRequirements[EntranceShuffle.All],
                            _entranceShuffleRequirements[EntranceShuffle.Insanity]
                        }])
                },
                LocationID.TowerOfHera => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1126, 68, location,
                        _entranceShuffleRequirements[EntranceShuffle.None]),
                    _factory(MapID.LightWorld, 1125, 20, location,
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon],
                            _entranceShuffleRequirements[EntranceShuffle.All],
                            _entranceShuffleRequirements[EntranceShuffle.Insanity]
                        }])
                },
                LocationID.PalaceOfDarkness => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1924, 800, location,
                        _entranceShuffleRequirements[EntranceShuffle.None]),
                    _factory(MapID.DarkWorld, 1925, 785, location,
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon],
                            _entranceShuffleRequirements[EntranceShuffle.All],
                            _entranceShuffleRequirements[EntranceShuffle.Insanity]
                        }])
                },
                LocationID.SwampPalace => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 940, 1880, location,
                        _entranceShuffleRequirements[EntranceShuffle.None]),
                    _factory(MapID.DarkWorld, 940, 1840, location,
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon],
                            _entranceShuffleRequirements[EntranceShuffle.All],
                            _entranceShuffleRequirements[EntranceShuffle.Insanity]
                        }])
                },
                LocationID.SkullWoods => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 79, 121, location,
                        _entranceShuffleRequirements[EntranceShuffle.None]),
                    _factory(MapID.DarkWorld, 80, 50, location,
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon],
                            _entranceShuffleRequirements[EntranceShuffle.All],
                            _entranceShuffleRequirements[EntranceShuffle.Insanity]
                        }])
                },
                LocationID.ThievesTown => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 251, 971, location,
                        _entranceShuffleRequirements[EntranceShuffle.None]),
                    _factory(MapID.DarkWorld, 255, 935, location,
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon],
                            _entranceShuffleRequirements[EntranceShuffle.All],
                            _entranceShuffleRequirements[EntranceShuffle.Insanity]
                        }])
                },
                LocationID.IcePalace => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1600, 1735, location,
                        _entranceShuffleRequirements[EntranceShuffle.None]),
                    _factory(MapID.DarkWorld, 1600, 1695, location,
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon],
                            _entranceShuffleRequirements[EntranceShuffle.All],
                            _entranceShuffleRequirements[EntranceShuffle.Insanity]
                        }])
                },
                LocationID.MiseryMire => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 150, 1710, location,
                        _entranceShuffleRequirements[EntranceShuffle.None]),
                    _factory(MapID.DarkWorld, 150, 1600, location,
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon],
                            _entranceShuffleRequirements[EntranceShuffle.All],
                            _entranceShuffleRequirements[EntranceShuffle.Insanity]
                        }])
                },
                LocationID.TurtleRock => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1890, 144, location,
                        _entranceShuffleRequirements[EntranceShuffle.None]),
                    _factory(MapID.DarkWorld, 1890, 125, location,
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon],
                            _entranceShuffleRequirements[EntranceShuffle.All],
                            _entranceShuffleRequirements[EntranceShuffle.Insanity]
                        }])
                },
                LocationID.GanonsTower => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1126, 68, location,
                        _aggregateRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.None],
                            _worldStateRequirements[WorldState.StandardOpen]
                        }]),
                    _factory(MapID.LightWorld, 1003, 797, location,
                        _aggregateRequirements[new HashSet<IRequirement>
                        {
                            _worldStateRequirements[WorldState.Inverted],
                            _entranceShuffleRequirements[EntranceShuffle.None]
                        }]),
                    _factory(MapID.DarkWorld, 1125, 30, location,
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon],
                            _entranceShuffleRequirements[EntranceShuffle.All],
                            _entranceShuffleRequirements[EntranceShuffle.Insanity]
                        }])
                },
                LocationID.LumberjackHouseEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 675, 120, location)
                },
                LocationID.LumberjackCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 600, 145, location)
                },
                LocationID.DeathMountainEntryCave => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 715, 350, location)
                },
                LocationID.DeathMountainExitCave => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 720, 305, location)
                },
                LocationID.KakarikoFortuneTellerEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 375, 645, location)
                },
                LocationID.WomanLeftDoor => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 305, 840, location)
                },
                LocationID.WomanRightDoor => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 340, 840, location)
                },
                LocationID.LeftSnitchHouseEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 100, 940, location)
                },
                LocationID.RightSnitchHouseEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 415, 965, location)
                },
                LocationID.BlindsHouseEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 255, 840, location)
                },
                LocationID.TheWellEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 47, 833, location)
                },
                LocationID.ChickenHouseEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 197, 1066, location)
                },
                LocationID.GrassHouseEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 410, 1075, location)
                },
                LocationID.TavernFront => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 320, 1195, location)
                },
                LocationID.KakarikoShopEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 220, 1175, location)
                },
                LocationID.BombHutEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 55, 1195, location)
                },
                LocationID.SickKidEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 314, 1060, location)
                },
                LocationID.BlacksmithHouse => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 615, 1055, location)
                },
                LocationID.MagicBatEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 650, 1127, location)
                },
                LocationID.ChestGameEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 425, 1410, location)
                },
                LocationID.RaceHouseLeft => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 220, 1435, location)
                },
                LocationID.RaceHouseRight => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 280, 1435, location)
                },
                LocationID.LibraryEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 313, 1310, location)
                },
                LocationID.ForestHideoutEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 380, 264, location)
                },
                LocationID.ForestChestGameEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 370, 40, location)
                },
                LocationID.CastleSecretEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1196, 834, location)
                },
                LocationID.CastleMainEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1000, 895, location)
                },
                LocationID.CastleLeftEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 900, 780, location)
                },
                LocationID.CastleRightEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1105, 780, location)
                },
                LocationID.CastleTowerEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1000, 800, location)
                },
                LocationID.DamEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 942, 1880, location)
                },
                LocationID.CentralBonkRocksEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 945, 1310, location)
                },
                LocationID.WitchsHutEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1607, 670, location)
                },
                LocationID.WaterfallFairyEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1806, 286, location)
                },
                LocationID.SahasrahlasHutEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1625, 900, location)
                },
                LocationID.TreesFairyCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1650, 1295, location)
                },
                LocationID.PegsFairyCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1970, 1405, location)
                },
                LocationID.EasternPalaceEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1925, 820, location)
                },
                LocationID.HoulihanHole => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1290, 625, location)
                },
                LocationID.SanctuaryGrave => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1040, 590, location)
                },
                LocationID.NorthBonkRocks => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 777, 590, location)
                },
                LocationID.KingsTombEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1207, 598, location)
                },
                LocationID.GraveyardLedgeEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1132, 549, location)
                },
                LocationID.DesertLeftEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 70, 1590, location)
                },
                LocationID.DesertBackEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 150, 1540, location)
                },
                LocationID.DesertRightEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 225, 1590, location)
                },
                LocationID.DesertFrontEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 150, 1600, location)
                },
                LocationID.AginahsCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 400, 1655, location)
                },
                LocationID.ThiefCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 555, 1790, location)
                },
                LocationID.RupeeCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 625, 1920, location)
                },
                LocationID.SkullWoodsBack => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 80, 100, location)
                },
                LocationID.ThievesTownEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 255, 975, location)
                },
                LocationID.CShapedHouseEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 414, 969, location)
                },
                LocationID.HammerHouse => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 408, 1069, location)
                },
                LocationID.DarkVillageFortuneTellerEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 377, 647, location)
                },
                LocationID.DarkChapelEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 924, 551, location)
                },
                LocationID.ShieldShop => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 665, 922, location)
                },
                LocationID.DarkLumberjack => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 675, 115, location)
                },
                LocationID.TreasureGameEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 100, 936, location)
                },
                LocationID.BombableShackEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 219, 1171, location)
                },
                LocationID.HammerPegsEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 636, 1214, location)
                },
                LocationID.BumperCaveExit => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 720, 310, location)
                },
                LocationID.BumperCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 715, 355, location)
                },
                LocationID.HypeCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1200, 1560, location)
                },
                LocationID.SwampPalaceEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 940, 1875, location)
                },
                LocationID.DarkCentralBonkRocksEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 945, 1310, location)
                },
                LocationID.SouthOfGroveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 552, 1693, location)
                },
                LocationID.BombShop => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1098, 1382, location)
                },
                LocationID.ArrowGameEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 431, 1409, location)
                },
                LocationID.DarkHyliaFortuneTeller => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1300, 1615, location)
                },
                LocationID.DarkTreesFairyCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1656, 1296, location)
                },
                LocationID.DarkSahasrahlaEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1706, 1008, location)
                },
                LocationID.PalaceOfDarknessEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1925, 830, location)
                },
                LocationID.DarkWitchsHut => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1616, 678, location)
                },
                LocationID.DarkFluteSpotFiveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1968, 1405, location)
                },
                LocationID.FatFairyEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 940, 976, location)
                },
                LocationID.GanonHole => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1003, 832, location,
                        _worldStateRequirements[WorldState.Inverted]),
                    _factory(MapID.DarkWorld, 1000, 820, location,
                        _worldStateRequirements[WorldState.StandardOpen])
                },
                LocationID.DarkIceRodCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1795, 1545, location)
                },
                LocationID.DarkFakeIceRodCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1835, 1545, location)
                },
                LocationID.DarkIceRodRockEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1810, 1585, location)
                },
                LocationID.HypeFairyCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1200, 1565, location)
                },
                LocationID.FortuneTellerEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1300, 1615, location)
                },
                LocationID.LakeShop => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1460, 1540, location)
                },
                LocationID.UpgradeFairy => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1590, 1710, location)
                },
                LocationID.MiniMoldormCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1309, 1887, location)
                },
                LocationID.IceRodCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1795, 1545, location)
                },
                LocationID.IceBeeCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1835, 1545, location)
                },
                LocationID.IceFairyCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1810, 1585, location)
                },
                LocationID.IcePalaceEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1600, 1735, location)
                },
                LocationID.MiseryMireEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 150, 1650, location)
                },
                LocationID.MireShackEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 77, 1600, location)
                },
                LocationID.MireRightShackEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 220, 1610, location)
                },
                LocationID.MireCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 400, 1655, location)
                },
                LocationID.CheckerboardCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 354, 1560, location)
                },
                LocationID.DeathMountainEntranceBack => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 816, 378, location)
                },
                LocationID.OldManResidence => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 900, 470, location)
                },
                LocationID.OldManBackResidence => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1075, 325, location)
                },
                LocationID.DeathMountainExitFront => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 790, 275, location)
                },
                LocationID.SpectacleRockLeft => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 920, 280, location)
                },
                LocationID.SpectacleRockRight => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 980, 290, location)
                },
                LocationID.SpectacleRockTop => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 980, 205, location)
                },
                LocationID.SpikeCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1151, 294, location)
                },
                LocationID.DarkMountainFairyEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 815, 376, location)
                },
                LocationID.TowerOfHeraEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1125, 65, location)
                },
                LocationID.SpiralCaveBottom => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1605, 260, location)
                },
                LocationID.EDMFairyCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1695, 290, location)
                },
                LocationID.ParadoxCaveMiddle => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1735, 290, location)
                },
                LocationID.ParadoxCaveBottom => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1731, 434, location)
                },
                LocationID.EDMConnectorBottom => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1645, 275, location)
                },
                LocationID.SpiralCaveTop => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1600, 180, location)
                },
                LocationID.MimicCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1695, 180, location)
                },
                LocationID.EDMConnectorTop => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1645, 230, location)
                },
                LocationID.ParadoxCaveTop => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1725, 125, location)
                },
                LocationID.SuperBunnyCaveBottom => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1685, 295, location)
                },
                LocationID.DeathMountainShopEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1725, 295, location)
                },
                LocationID.SuperBunnyCaveTop => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1725, 128, location)
                },
                LocationID.HookshotCaveEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1670, 126, location)
                },
                LocationID.TurtleRockEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1890, 165, location)
                },
                LocationID.GanonsTowerEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1125, 70, location)
                },
                LocationID.TRLedgeLeft => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1598, 182, location)
                },
                LocationID.TRLedgeRight => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1694, 182, location)
                },
                LocationID.TRSafetyDoor => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1648, 229, location)
                },
                LocationID.HookshotCaveTop => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1627, 40, location)
                },
                LocationID.LinksHouseEntrance => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1098, 1382, location)
                },
                LocationID.TreesFairyCaveTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1650, 1295, location)
                },
                LocationID.PegsFairyCaveTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1970, 1405, location)
                },
                LocationID.KakarikoFortuneTellerTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 375, 645, location)
                },
                LocationID.GrassHouseTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 410, 1075, location)
                },
                LocationID.ForestChestGameTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 370, 40, location)
                },
                LocationID.LumberjackHouseTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 688, 120, location)
                },
                LocationID.LeftSnitchHouseTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 100, 940, location)
                },
                LocationID.RightSnitchHouseTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 415, 965, location)
                },
                LocationID.BombHutTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 55, 1195, location)
                },
                LocationID.IceFairyCaveTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1810, 1602, location)
                },
                LocationID.RupeeCaveTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 625, 1920, location)
                },
                LocationID.CentralBonkRocksTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 945, 1310, location)
                },
                LocationID.ThiefCaveTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 555, 1790, location)
                },
                LocationID.IceBeeCaveTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1850, 1545, location)
                },
                LocationID.FortuneTellerTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1300, 1615, location)
                },
                LocationID.HypeFairyCaveTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1200, 1565, location)
                },
                LocationID.ChestGameTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 425, 1410, location)
                },
                LocationID.EDMFairyCaveTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1695, 290, location)
                },
                LocationID.DarkChapelTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 924, 551, location)
                },
                LocationID.DarkVillageFortuneTellerTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 377, 647, location)
                },
                LocationID.DarkTreesFairyCaveTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1656, 1296, location)
                },
                LocationID.DarkSahasrahlaTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1706, 1008, location)
                },
                LocationID.DarkFluteSpotFiveTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1968, 1405, location)
                },
                LocationID.ArrowGameTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 431, 1409, location)
                },
                LocationID.DarkCentralBonkRocksTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 945, 1310, location)
                },
                LocationID.DarkIceRodCaveTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1795, 1545, location)
                },
                LocationID.DarkFakeIceRodCaveTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1835, 1545, location)
                },
                LocationID.DarkIceRodRockTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1810, 1585, location)
                },
                LocationID.DarkMountainFairyTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 815, 376, location)
                },
                LocationID.MireRightShackTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 220, 1610, location)
                },
                LocationID.MireCaveTakeAny => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 400, 1655, location)
                },
                LocationID.LumberjackCaveExit => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 666, 64, location)
                },
                LocationID.TheWellExit => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 94, 854, location)
                },
                LocationID.MagicBatExit => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 634, 1112, location)
                },
                LocationID.ForestHideoutExit => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 368, 301, location)
                },
                LocationID.CastleSecretExit => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1105, 860, location)
                },
                LocationID.HoulihanHoleExit => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1340, 548, location)
                },
                LocationID.Sanctuary => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 924, 537, location)
                },
                LocationID.GanonHoleExit => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 902, 862, location)
                },
                LocationID.SkullWoodsWestEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 117, 260, location)
                },
                LocationID.SkullWoodsCenterEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 290, 291, location)
                },
                LocationID.SkullWoodsEastEntrance => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 368, 299, location)
                },
                LocationID.SkullWoodsNWHole => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 243, 180, location)
                },
                LocationID.SkullWoodsSWHole => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 313, 352, location)
                },
                LocationID.SkullWoodsSEHole => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 392, 337, location)
                },
                LocationID.SkullWoodsNEHole => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 380, 262, location)
                },
                LocationID.KakarikoShop => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 220, 1175, location)
                },
                LocationID.LakeHyliaShop => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1460, 1540, location)
                },
                LocationID.DeathMountainShop => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1735, 290, location)
                },
                LocationID.PotionShop => new List<IMapLocation>
                {
                    _factory(MapID.LightWorld, 1607, 715, location)
                },
                LocationID.DarkLumberjackShop => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 675, 115, location)
                },
                LocationID.RedShieldShop => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 665, 922, location)
                },
                LocationID.VillageOfOutcastsShop => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 408, 1069, location)
                },
                LocationID.DarkLakeHyliaShop => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1300, 1618, location)
                },
                LocationID.DarkPotionShop => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1607, 670, location)
                },
                LocationID.DarkDeathMountainShop => new List<IMapLocation>
                {
                    _factory(MapID.DarkWorld, 1725, 360, location)
                },
                _ => throw new ArgumentOutOfRangeException(nameof(location))
            };
        }
    }
}
