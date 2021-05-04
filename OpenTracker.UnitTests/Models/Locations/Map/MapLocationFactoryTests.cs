using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using ExpectedObjects;
using NSubstitute;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Locations.Map;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Aggregate;
using OpenTracker.Models.Requirements.Alternative;
using OpenTracker.Models.Requirements.Mode;
using OpenTracker.Models.Requirements.ShopShuffle;
using OpenTracker.Models.Requirements.TakeAnyLocations;
using Xunit;

namespace OpenTracker.UnitTests.Models.Locations.Map
{
    public class MapLocationFactoryTests
    {
        private static readonly IAlternativeRequirementDictionary AlternativeRequirements =
            new AlternativeRequirementDictionary(requirements =>
                new AlternativeRequirement(requirements));
        private static readonly IAggregateRequirementDictionary AggregateRequirements =
            new AggregateRequirementDictionary(requirements => 
                new AggregateRequirement(requirements));
        private static readonly IEntranceShuffleRequirementDictionary EntranceShuffleRequirements =
            Substitute.For<IEntranceShuffleRequirementDictionary>();
        private static readonly IShopShuffleRequirementDictionary ShopShuffleRequirements =
            Substitute.For<IShopShuffleRequirementDictionary>();
        private static readonly ITakeAnyLocationsRequirementDictionary TakeAnyLocationsRequirements =
            Substitute.For<ITakeAnyLocationsRequirementDictionary>();
        private static readonly IWorldStateRequirementDictionary WorldStateRequirements =
            Substitute.For<IWorldStateRequirementDictionary>();

        private static readonly IMapLocation.Factory Factory = (map, x, y, location, requirement) =>
            new MapLocation(map, x, y, location, requirement);

        private static readonly ILocation Location = Substitute.For<ILocation>();

        private static readonly Dictionary<LocationID, ExpectedObject> ExpectedValues = new();

        private readonly MapLocationFactory _sut = new MapLocationFactory(
            AlternativeRequirements, AggregateRequirements, EntranceShuffleRequirements, WorldStateRequirements, Factory);

        private static void PopulateExpectedValues()
        {
            ExpectedValues.Clear();

            foreach (LocationID id in Enum.GetValues(typeof(LocationID)))
            {
                var mapLocations = id switch
                {
                    LocationID.LinksHouse => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1097, 1366, Location,
                            WorldStateRequirements[WorldState.StandardOpen]),
                        Factory(MapID.DarkWorld, 1097, 1366, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                WorldStateRequirements[WorldState.Inverted]
                            }])
                    },
                    LocationID.Pedestal => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 83, 101, Location)
                    },
                    LocationID.LumberjackCave => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 633, 117, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }])
                    },
                    LocationID.BlindsHouse => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 307, 840, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }])
                    },
                    LocationID.TheWell => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 47, 833, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }])
                    },
                    LocationID.BottleVendor => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 190, 933, Location)
                    },
                    LocationID.ChickenHouse => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 197, 1066, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }])
                    },
                    LocationID.Tavern => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 320, 1145, Location)
                    },
                    LocationID.SickKid => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 314, 1060, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }])
                    },
                    LocationID.MagicBat => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 650, 1127, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }])
                    },
                    LocationID.RaceGame => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 111, 1354, Location)
                    },
                    LocationID.Library => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 313, 1310, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }])
                    },
                    LocationID.MushroomSpot => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 244, 170, Location)
                    },
                    LocationID.ForestHideout => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 380, 264, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }])
                    },
                    LocationID.CastleSecret => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1196, 834, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }])
                    },
                    LocationID.WitchsHut => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1607, 665, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }])
                    },
                    LocationID.SahasrahlasHut => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1625, 900, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }])
                    },
                    LocationID.BonkRocks => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 777, 590, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }])
                    },
                    LocationID.KingsTomb => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1207, 598, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }])
                    },
                    LocationID.AginahsCave => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 400, 1655, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }])
                    },
                    LocationID.GroveDiggingSpot => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 600, 1350, Location)
                    },
                    LocationID.Dam => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 942, 1880, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }]),
                        Factory(MapID.LightWorld, 900, 1860, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }]),
                    },
                    LocationID.MiniMoldormCave => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1309, 1887, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }])
                    },
                    LocationID.IceRodCave => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1795, 1547, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }])
                    },
                    LocationID.Hobo => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1390, 1390, Location)
                    },
                    LocationID.PyramidLedge => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1164, 922, Location)
                    },
                    LocationID.FatFairy => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 940, 976, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }])
                    },
                    LocationID.HauntedGrove => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 620, 1371, Location)
                    },
                    LocationID.HypeCave => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1200, 1560, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }])
                    },
                    LocationID.BombosTablet => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 440, 1845, Location),
                        Factory(MapID.DarkWorld, 440, 1845, Location,
                            WorldStateRequirements[WorldState.StandardOpen])
                    },
                    LocationID.SouthOfGrove => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 552, 1693, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }]),
                        Factory(MapID.DarkWorld, 552, 1693, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.All],
                                    EntranceShuffleRequirements[EntranceShuffle.Insanity]
                                }],
                                WorldStateRequirements[WorldState.StandardOpen]
                            }])
                    },
                    LocationID.DiggingGame => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 100, 1385, Location)
                    },
                    LocationID.WaterfallFairy => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1806, 286, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }])
                    },
                    LocationID.ZoraArea => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1920, 273, Location)
                    },
                    LocationID.Catfish => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1813, 347, Location)
                    },
                    LocationID.GraveyardLedge => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1132, 549, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }]),
                        Factory(MapID.DarkWorld, 1132, 530, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.All],
                                    EntranceShuffleRequirements[EntranceShuffle.Insanity]
                                }],
                                WorldStateRequirements[WorldState.StandardOpen]
                            }])
                    },
                    LocationID.DesertLedge => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 40, 1835, Location)
                    },
                    LocationID.CShapedHouse => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 414, 969, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }])
                    },
                    LocationID.TreasureGame => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 100, 936, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }])
                    },
                    LocationID.BombableShack => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 219, 1171, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }])
                    },
                    LocationID.Blacksmith => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 616, 1054, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }]),
                        Factory(MapID.DarkWorld, 295, 1325, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }])
                    },
                    LocationID.PurpleChest => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 680, 1805, Location),
                        Factory(MapID.DarkWorld, 601, 1050, Location)
                    },
                    LocationID.HammerPegs => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 636, 1214, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }])
                    },
                    LocationID.BumperCave => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 687, 340, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }]),
                        Factory(MapID.DarkWorld, 680, 315, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.LakeHyliaIsland => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1450, 1666, Location),
                        Factory(MapID.DarkWorld, 1450, 1666, Location,
                            WorldStateRequirements[WorldState.StandardOpen])
                    },
                    LocationID.MireShack => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 77, 1600, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }])
                    },
                    LocationID.CheckerboardCave => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 354, 1560, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }]),
                        Factory(MapID.DarkWorld, 334, 1557, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }])
                    },
                    LocationID.OldMan => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 816, 378, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                WorldStateRequirements[WorldState.Inverted]
                            }]),
                        Factory(MapID.LightWorld, 900, 440, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.All],
                                    EntranceShuffleRequirements[EntranceShuffle.Insanity]
                                }],
                                WorldStateRequirements[WorldState.StandardOpen]
                            }]),
                        Factory(MapID.DarkWorld, 816, 378, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                WorldStateRequirements[WorldState.Inverted]
                            }])
                    },
                    LocationID.SpectacleRock => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 980, 178, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }]),
                        Factory(MapID.LightWorld, 1036, 170, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.EtherTablet => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 844, 38, Location)
                    },
                    LocationID.SpikeCave => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1151, 294, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }])
                    },
                    LocationID.SpiralCave => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1598, 180, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }])
                    },
                    LocationID.ParadoxCave => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1731, 434, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }])
                    },
                    LocationID.SuperBunnyCave => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1695, 290, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }])
                    },
                    LocationID.HookshotCave => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1670, 126, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }])
                    },
                    LocationID.FloatingIsland => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1627, 40, Location),
                        Factory(MapID.DarkWorld, 1627, 40, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.All],
                                    EntranceShuffleRequirements[EntranceShuffle.Insanity]
                                }],
                                WorldStateRequirements[WorldState.StandardOpen]
                            }])
                    },
                    LocationID.MimicCave => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1694, 190, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }]),
                        Factory(MapID.DarkWorld, 1693, 205, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }])
                    },
                    LocationID.HyruleCastle => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1003, 926, Location,
                            EntranceShuffleRequirements[EntranceShuffle.None]),
                        Factory(MapID.LightWorld, 925, 540, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon],
                                EntranceShuffleRequirements[EntranceShuffle.All]
                            }]),
                        Factory(MapID.LightWorld, 925, 516, Location,
                            EntranceShuffleRequirements[EntranceShuffle.Insanity])
                    },
                    LocationID.AgahnimTower => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1003, 797, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                WorldStateRequirements[WorldState.StandardOpen]
                            }]),
                        Factory(MapID.DarkWorld, 1126, 68, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                WorldStateRequirements[WorldState.Inverted],
                                EntranceShuffleRequirements[EntranceShuffle.None]
                            }]),
                        Factory(MapID.LightWorld, 1000, 750, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.EasternPalace => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1925, 791, Location,
                            EntranceShuffleRequirements[EntranceShuffle.None]),
                        Factory(MapID.LightWorld, 1925, 780, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.DesertPalace => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 146, 1584, Location,
                            EntranceShuffleRequirements[EntranceShuffle.None]),
                        Factory(MapID.LightWorld, 150, 1700, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.TowerOfHera => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1126, 68, Location,
                            EntranceShuffleRequirements[EntranceShuffle.None]),
                        Factory(MapID.LightWorld, 1125, 20, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.PalaceOfDarkness => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1924, 800, Location,
                            EntranceShuffleRequirements[EntranceShuffle.None]),
                        Factory(MapID.DarkWorld, 1925, 785, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.SwampPalace => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 940, 1880, Location,
                            EntranceShuffleRequirements[EntranceShuffle.None]),
                        Factory(MapID.DarkWorld, 940, 1840, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.SkullWoods => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 79, 121, Location,
                            EntranceShuffleRequirements[EntranceShuffle.None]),
                        Factory(MapID.DarkWorld, 80, 50, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.ThievesTown => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 251, 971, Location,
                            EntranceShuffleRequirements[EntranceShuffle.None]),
                        Factory(MapID.DarkWorld, 255, 935, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.IcePalace => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1600, 1735, Location,
                            EntranceShuffleRequirements[EntranceShuffle.None]),
                        Factory(MapID.DarkWorld, 1600, 1695, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.MiseryMire => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 150, 1710, Location,
                            EntranceShuffleRequirements[EntranceShuffle.None]),
                        Factory(MapID.DarkWorld, 150, 1600, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.TurtleRock => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1890, 144, Location,
                            EntranceShuffleRequirements[EntranceShuffle.None]),
                        Factory(MapID.DarkWorld, 1890, 125, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.GanonsTower => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1126, 68, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                WorldStateRequirements[WorldState.StandardOpen]
                            }]),
                        Factory(MapID.LightWorld, 1003, 797, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                WorldStateRequirements[WorldState.Inverted],
                                EntranceShuffleRequirements[EntranceShuffle.None]
                            }]),
                        Factory(MapID.DarkWorld, 1125, 30, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.LumberjackHouseEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 675, 120, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.LumberjackCaveEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 600, 145, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.DeathMountainEntryCave => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 715, 350, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.DeathMountainExitCave => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 720, 305, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.KakarikoFortuneTellerEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 375, 645, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.WomanLeftDoor => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 305, 840, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.WomanRightDoor => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 340, 840, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.LeftSnitchHouseEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 100, 940, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.RightSnitchHouseEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 415, 965, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.BlindsHouseEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 255, 840, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.TheWellEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 47, 833, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.ChickenHouseEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 197, 1066, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.GrassHouseEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 410, 1075, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.TavernFront => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 320, 1195, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.KakarikoShopEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 220, 1175, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.BombHutEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 55, 1195, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.SickKidEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 314, 1060, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.BlacksmithHouse => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 615, 1055, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.MagicBatEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 650, 1127, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.ChestGameEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 425, 1410, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.RaceHouseLeft => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 220, 1435, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.RaceHouseRight => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 280, 1435, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.LibraryEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 313, 1310, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.ForestHideoutEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 380, 264, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.ForestChestGameEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 370, 40, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.CastleSecretEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1196, 834, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.CastleMainEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1000, 895, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.CastleLeftEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 900, 780, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.CastleRightEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1105, 780, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.CastleTowerEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1000, 800, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.DamEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 942, 1880, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.CentralBonkRocksEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 945, 1310, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.WitchsHutEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1607, 670, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.WaterfallFairyEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1806, 286, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.SahasrahlasHutEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1625, 900, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.TreesFairyCaveEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1650, 1295, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.PegsFairyCaveEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1970, 1405, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.EasternPalaceEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1925, 820, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.HoulihanHole => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1290, 625, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.SanctuaryGrave => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1040, 590, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.NorthBonkRocks => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 777, 590, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.KingsTombEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1207, 598, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.GraveyardLedgeEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1132, 549, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.DesertLeftEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 70, 1590, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.DesertBackEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 150, 1540, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.DesertRightEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 225, 1590, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.DesertFrontEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 150, 1600, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.AginahsCaveEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 400, 1655, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.ThiefCaveEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 555, 1790, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.RupeeCaveEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 625, 1920, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.SkullWoodsBack => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 80, 100, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.ThievesTownEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 255, 975, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.CShapedHouseEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 414, 969, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.HammerHouse => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 408, 1069, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.DarkVillageFortuneTellerEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 377, 647, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.DarkChapelEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 924, 551, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.ShieldShop => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 665, 922, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.DarkLumberjack => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 675, 115, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.TreasureGameEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 100, 936, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.BombableShackEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 219, 1171, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.HammerPegsEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 636, 1214, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.BumperCaveExit => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 720, 310, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.BumperCaveEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 715, 355, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.HypeCaveEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1200, 1560, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.SwampPalaceEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 940, 1875, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.DarkCentralBonkRocksEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 945, 1310, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.SouthOfGroveEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 552, 1693, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.BombShop => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1098, 1382, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.ArrowGameEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 431, 1409, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.DarkHyliaFortuneTeller => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1300, 1615, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.DarkTreesFairyCaveEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1656, 1296, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.DarkSahasrahlaEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1706, 1008, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.PalaceOfDarknessEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1925, 830, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.DarkWitchsHut => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1616, 678, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.DarkFluteSpotFiveEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1968, 1405, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.FatFairyEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 940, 976, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.GanonHole => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1003, 832, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.All],
                                    EntranceShuffleRequirements[EntranceShuffle.Insanity]
                                }],
                                WorldStateRequirements[WorldState.Inverted]
                            }]),
                        Factory(MapID.DarkWorld, 1000, 820, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.All],
                                    EntranceShuffleRequirements[EntranceShuffle.Insanity]
                                }],
                                WorldStateRequirements[WorldState.StandardOpen]
                            }])
                    },
                    LocationID.DarkIceRodCaveEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1795, 1545, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.DarkFakeIceRodCaveEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1835, 1545, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.DarkIceRodRockEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1810, 1585, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.HypeFairyCaveEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1200, 1565, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.FortuneTellerEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1300, 1615, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.LakeShop => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1460, 1540, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.UpgradeFairy => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1590, 1710, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.MiniMoldormCaveEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1309, 1887, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.IceRodCaveEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1795, 1545, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.IceBeeCaveEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1835, 1545, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.IceFairyCaveEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1810, 1585, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.IcePalaceEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1600, 1735, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.MiseryMireEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 150, 1650, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.MireShackEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 77, 1600, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.MireRightShackEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 220, 1610, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.MireCaveEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 400, 1655, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.CheckerboardCaveEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 354, 1560, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.DeathMountainEntranceBack => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 816, 378, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.OldManResidence => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 900, 470, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.OldManBackResidence => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1075, 325, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.DeathMountainExitFront => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 790, 275, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.SpectacleRockLeft => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 920, 280, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.SpectacleRockRight => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 980, 290, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.SpectacleRockTop => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 980, 205, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.SpikeCaveEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1151, 294, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.DarkMountainFairyEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 815, 376, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.TowerOfHeraEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1125, 65, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.SpiralCaveBottom => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1605, 260, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.EDMFairyCaveEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1695, 290, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.ParadoxCaveMiddle => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1735, 290, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.ParadoxCaveBottom => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1731, 434, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.EDMConnectorBottom => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1645, 275, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.SpiralCaveTop => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1600, 180, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.MimicCaveEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1695, 180, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.EDMConnectorTop => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1645, 230, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.ParadoxCaveTop => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1725, 125, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.SuperBunnyCaveBottom => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1685, 295, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.DeathMountainShopEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1725, 295, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.SuperBunnyCaveTop => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1725, 128, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.HookshotCaveEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1670, 126, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.TurtleRockEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1890, 165, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.GanonsTowerEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1125, 70, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.TRLedgeLeft => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1598, 182, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.TRLedgeRight => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1694, 182, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.TRSafetyDoor => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1648, 229, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.HookshotCaveTop => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1627, 40, Location,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    LocationID.LinksHouseEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1098, 1382, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.All],
                                    EntranceShuffleRequirements[EntranceShuffle.Insanity]
                                }],
                                WorldStateRequirements[WorldState.Inverted]
                            }])
                    },
                    LocationID.TreesFairyCaveTakeAny => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1650, 1295, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }])
                    },
                    LocationID.PegsFairyCaveTakeAny => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1970, 1405, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }])
                    },
                    LocationID.KakarikoFortuneTellerTakeAny => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 375, 645, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }])
                    },
                    LocationID.GrassHouseTakeAny => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 410, 1075, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }])
                    },
                    LocationID.ForestChestGameTakeAny => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 370, 40, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }])
                    },
                    LocationID.LumberjackHouseTakeAny => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 688, 120, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }])
                    },
                    LocationID.LeftSnitchHouseTakeAny => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 100, 940, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }])
                    },
                    LocationID.RightSnitchHouseTakeAny => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 415, 965, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }])
                    },
                    LocationID.BombHutTakeAny => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 55, 1195, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }])
                    },
                    LocationID.IceFairyCaveTakeAny => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1810, 1602, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }])
                    },
                    LocationID.RupeeCaveTakeAny => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 625, 1920, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }])
                    },
                    LocationID.CentralBonkRocksTakeAny => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 945, 1310, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }])
                    },
                    LocationID.ThiefCaveTakeAny => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 555, 1790, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }])
                    },
                    LocationID.IceBeeCaveTakeAny => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1850, 1545, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }])
                    },
                    LocationID.FortuneTellerTakeAny => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1300, 1615, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }])
                    },
                    LocationID.HypeFairyCaveTakeAny => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1200, 1565, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }])
                    },
                    LocationID.ChestGameTakeAny => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 425, 1410, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }])
                    },
                    LocationID.EDMFairyCaveTakeAny => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1695, 290, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }])
                    },
                    LocationID.DarkChapelTakeAny => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 924, 551, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }])
                    },
                    LocationID.DarkVillageFortuneTellerTakeAny => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 377, 647, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }])
                    },
                    LocationID.DarkTreesFairyCaveTakeAny => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1656, 1296, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }])
                    },
                    LocationID.DarkSahasrahlaTakeAny => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1706, 1008, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }])
                    },
                    LocationID.DarkFluteSpotFiveTakeAny => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1968, 1405, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }])
                    },
                    LocationID.ArrowGameTakeAny => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 431, 1409, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }])
                    },
                    LocationID.DarkCentralBonkRocksTakeAny => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 945, 1310, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }])
                    },
                    LocationID.DarkIceRodCaveTakeAny => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1795, 1545, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }])
                    },
                    LocationID.DarkFakeIceRodCaveTakeAny => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1835, 1545, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }])
                    },
                    LocationID.DarkIceRodRockTakeAny => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1810, 1585, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }])
                    },
                    LocationID.DarkMountainFairyTakeAny => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 815, 376, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }])
                    },
                    LocationID.MireRightShackTakeAny => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 220, 1610, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }])
                    },
                    LocationID.MireCaveTakeAny => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 400, 1655, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }])
                    },
                    LocationID.LumberjackCaveExit => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 666, 64, Location,
                            EntranceShuffleRequirements[EntranceShuffle.Insanity])
                    },
                    LocationID.TheWellExit => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 94, 854, Location,
                            EntranceShuffleRequirements[EntranceShuffle.Insanity])
                    },
                    LocationID.MagicBatExit => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 634, 1112, Location,
                            EntranceShuffleRequirements[EntranceShuffle.Insanity])
                    },
                    LocationID.ForestHideoutExit => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 368, 301, Location,
                            EntranceShuffleRequirements[EntranceShuffle.Insanity])
                    },
                    LocationID.CastleSecretExit => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1105, 860, Location,
                            EntranceShuffleRequirements[EntranceShuffle.Insanity])
                    },
                    LocationID.HoulihanHoleExit => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1340, 548, Location,
                            EntranceShuffleRequirements[EntranceShuffle.Insanity])
                    },
                    LocationID.Sanctuary => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 924, 537, Location,
                            EntranceShuffleRequirements[EntranceShuffle.Insanity])
                    },
                    LocationID.GanonHoleExit => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 902, 862, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.Insanity],
                                WorldStateRequirements[WorldState.Inverted]
                            }])
                    },
                    LocationID.SkullWoodsWestEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 117, 260, Location,
                            EntranceShuffleRequirements[EntranceShuffle.Insanity])
                    },
                    LocationID.SkullWoodsCenterEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 290, 291, Location,
                            EntranceShuffleRequirements[EntranceShuffle.Insanity])
                    },
                    LocationID.SkullWoodsEastEntrance => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 368, 299, Location,
                            EntranceShuffleRequirements[EntranceShuffle.Insanity])
                    },
                    LocationID.SkullWoodsNWHole => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 243, 180, Location,
                            EntranceShuffleRequirements[EntranceShuffle.Insanity])
                    },
                    LocationID.SkullWoodsSWHole => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 313, 352, Location,
                            EntranceShuffleRequirements[EntranceShuffle.Insanity])
                    },
                    LocationID.SkullWoodsSEHole => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 392, 337, Location,
                            EntranceShuffleRequirements[EntranceShuffle.Insanity])
                    },
                    LocationID.SkullWoodsNEHole => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 380, 262, Location,
                            EntranceShuffleRequirements[EntranceShuffle.Insanity])
                    },
                    LocationID.KakarikoShop => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 220, 1175, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    ShopShuffleRequirements[true],
                                    TakeAnyLocationsRequirements[true]
                                }]
                            }])
                    },
                    LocationID.LakeHyliaShop => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1460, 1540, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    ShopShuffleRequirements[true],
                                    TakeAnyLocationsRequirements[true]
                                }]
                            }])
                    },
                    LocationID.DeathMountainShop => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1735, 290, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    ShopShuffleRequirements[true],
                                    TakeAnyLocationsRequirements[true]
                                }]
                            }])
                    },
                    LocationID.PotionShop => new List<IMapLocation>
                    {
                        Factory(MapID.LightWorld, 1607, 715, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                ShopShuffleRequirements[true]
                            }])
                    },
                    LocationID.DarkLumberjackShop => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 675, 115, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    ShopShuffleRequirements[true],
                                    TakeAnyLocationsRequirements[true]
                                }]
                            }])
                    },
                    LocationID.RedShieldShop => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 665, 922, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    ShopShuffleRequirements[true],
                                    TakeAnyLocationsRequirements[true]
                                }]
                            }])
                    },
                    LocationID.VillageOfOutcastsShop => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 408, 1069, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    ShopShuffleRequirements[true],
                                    TakeAnyLocationsRequirements[true]
                                }]
                            }])
                    },
                    LocationID.DarkLakeHyliaShop => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1300, 1618, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    ShopShuffleRequirements[true],
                                    TakeAnyLocationsRequirements[true]
                                }]
                            }])
                    },
                    LocationID.DarkPotionShop => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1607, 670, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    ShopShuffleRequirements[true],
                                    TakeAnyLocationsRequirements[true]
                                }]
                            }])
                    },
                    LocationID.DarkDeathMountainShop => new List<IMapLocation>
                    {
                        Factory(MapID.DarkWorld, 1725, 360, Location,
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    ShopShuffleRequirements[true],
                                    TakeAnyLocationsRequirements[true]
                                }]
                            }])
                    },
                    _ => null
                };

                if (mapLocations is not null)
                {
                    ExpectedValues.Add(id, mapLocations.ToExpectedObject());
                }
            }
        }

        [Fact]
        public void GetMapLocations_ShouldThrowException_WhenIDIsUnexpected()
        {
            Location.ID.Returns((LocationID) int.MaxValue);

            Assert.Throws<ArgumentOutOfRangeException>(() => _ = _sut.GetMapLocations(Location));
        }
        
        [Theory]
        [MemberData(nameof(GetMapLocations_ShouldReturnExpectedData))]
        public void GetMapLocations_ShouldReturnExpected(ExpectedObject expected, LocationID id)
        {
            Location.ID.Returns(id);
            
            expected.ShouldEqual(_sut.GetMapLocations(Location));
        }

        public static IEnumerable<object[]> GetMapLocations_ShouldReturnExpectedData()
        {
            PopulateExpectedValues();

            return ExpectedValues.Keys.Select(id => new object[] {ExpectedValues[id], id}).ToList();
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var sut = scope.Resolve<IMapLocationFactory>();
            
            Assert.NotNull(sut as MapLocationFactory);
        }
    }
}