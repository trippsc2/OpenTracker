using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace OpenTracker.Models
{
    public class Location : INotifyPropertyChanged
    {
        private readonly Game _game;

        public LocationID ID { get; }
        public string Name { get; }

        public BossSection BossSection { get; private set; }
        public List<MapLocation> MapLocations { get; }
        public List<ISection> Sections { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private AccessibilityLevel _accessibility;
        public AccessibilityLevel Accessibility
        {
            get => _accessibility;
            set
            {
                if (_accessibility != value)
                {
                    _accessibility = value;
                    OnPropertyChanged(nameof(Accessibility));
                }
            }
        }

        private int _available;
        public int Available
        {
            get => _available;
            private set
            {
                if (_available != value)
                {
                    _available = value;
                    OnPropertyChanged(nameof(Available));
                }
            }
        }

        private int _accessible;
        public int Accessible
        {
            get => _accessible;
            private set
            {
                if (_accessible != value)
                {
                    _accessible = value;
                    OnPropertyChanged(nameof(Accessible));
                }
            }
        }

        private int _total;
        public int Total
        {
            get => _total;
            private set
            {
                if (_total != value)
                {
                    _total = value;
                    OnPropertyChanged(nameof(Total));
                }
            }
        }

        public Location(Game game, LocationID iD)
        {
            _game = game;

            ID = iD;

            MapLocations = new List<MapLocation>();
            Sections = new List<ISection>();

            int itemSections = 0;
            bool entranceSection = false;
            bool takeAnySection = false;

            switch (iD)
            {
                case LocationID.Pedestal:
                    Name = "Pedestal";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 83, 101, new Mode()));
                    itemSections = 1;
                    break;
                case LocationID.LumberjackCave:
                    Name = "Lumberjacks";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 633, 117,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemSections = 1;
                    break;
                case LocationID.BlindsHouse:
                    Name = "Blind's House";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 307, 840,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemSections = 2;
                    break;
                case LocationID.TheWell:
                    Name = "The Well";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 47, 833,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemSections = 2;
                    break;
                case LocationID.BottleVendor:
                    Name = "Bottle Vendor";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 190, 933, new Mode()));
                    itemSections = 1;
                    break;
                case LocationID.ChickenHouse:
                    Name = "Chicken House";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 197, 1066,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemSections = 1;
                    break;
                case LocationID.Tavern:
                    Name = "Tavern";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 320, 1145, new Mode()));
                    itemSections = 1;
                    break;
                case LocationID.SickKid:
                    Name = "Sick Kid";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 314, 1060,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemSections = 1;
                    break;
                case LocationID.MagicBat:
                    Name = "Magic Bat";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 650, 1127,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemSections = 1;
                    break;
                case LocationID.RaceGame:
                    Name = "Race Game";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 111, 1354, new Mode()));
                    itemSections = 1;
                    break;
                case LocationID.Library:
                    Name = "Library";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 313, 1310,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemSections = 1;
                    break;
                case LocationID.MushroomSpot:
                    Name = "Mushroom Spot";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 244, 170, new Mode()));
                    itemSections = 1;
                    break;
                case LocationID.ForestHideout:
                    Name = "Forest Hideout";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 380, 264, new Mode()
                    {
                        EntranceShuffle = false
                    }));
                    itemSections = 1;
                    break;
                case LocationID.CastleSecret:
                    Name = "Uncle";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1196, 834,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemSections = 2;
                    break;
                case LocationID.LinksHouse:
                    Name = "Link's House";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1097, 1366,
                        new Mode()
                        {
                            WorldState = WorldState.StandardOpen
                        }));
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1097, 1366,
                        new Mode()
                        {
                            WorldState = WorldState.Inverted,
                            EntranceShuffle = false
                        }));
                    itemSections = 1;
                    break;
                case LocationID.GroveDiggingSpot:
                    Name = "Dig Spot";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 600, 1350, new Mode()));
                    itemSections = 1;
                    break;
                case LocationID.PyramidLedge:
                    Name = "Pyramid Ledge";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1164, 922, new Mode()));
                    itemSections = 1;
                    break;
                case LocationID.FatFairy:
                    Name = "Fat Fairy";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 940, 976,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemSections = 1;
                    break;
                case LocationID.HauntedGrove:
                    Name = "Haunted Grove";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 620, 1371, new Mode()));
                    itemSections = 1;
                    break;
                case LocationID.HypeCave:
                    Name = "Hype Cave";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1200, 1560,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemSections = 1;
                    break;
                case LocationID.BombosTablet:
                    Name = "Bombos Tablet";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 440, 1845, new Mode()));
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 440, 1845, new Mode()
                    {
                        WorldState = WorldState.StandardOpen
                    }));
                    itemSections = 1;
                    break;
                case LocationID.SouthOfGrove:
                    Name = "South of Grove";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 552, 1693,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 552, 1693,
                        new Mode()
                        {
                            WorldState = WorldState.StandardOpen,
                            EntranceShuffle = false
                        }));
                    itemSections = 1;
                    break;
                case LocationID.DiggingGame:
                    Name = "Digging Game";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 100, 1385, new Mode()));
                    itemSections = 1;
                    break;
                case LocationID.WitchsHut:
                    Name = "Witch's Hut";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1607, 670,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemSections = 1;
                    break;
                case LocationID.WaterfallFairy:
                    Name = "Waterfall Fairy";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1806, 286,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemSections = 1;
                    break;
                case LocationID.ZoraArea:
                    Name = "Zora Area";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1920, 273, new Mode()));
                    itemSections = 2;
                    break;
                case LocationID.Catfish:
                    Name = "Catfish";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1813, 347, new Mode()));
                    itemSections = 1;
                    break;
                case LocationID.SahasrahlasHut:
                    Name = "Sahash's Hut";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1625, 900,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemSections = 2;
                    break;
                case LocationID.BonkRocks:
                    Name = "Bonk Rocks";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 777, 590,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemSections = 1;
                    break;
                case LocationID.KingsTomb:
                    Name = "King's Tomb";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1207, 598,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemSections = 1;
                    break;
                case LocationID.GraveyardLedge:
                    Name = "Graveyard Ledge";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1132, 549,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1132, 530,
                        new Mode()
                        {
                            WorldState = WorldState.StandardOpen,
                            EntranceShuffle = false
                        }));
                    itemSections = 1;
                    break;
                case LocationID.DesertLedge:
                    Name = "Desert Ledge";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 40, 1835, new Mode()));
                    itemSections = 1;
                    break;
                case LocationID.AginahsCave:
                    Name = "Aginah's Cave";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 400, 1655,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemSections = 1;
                    break;
                case LocationID.CShapedHouse:
                    Name = "C-Shaped House";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 414, 969,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemSections = 1;
                    break;
                case LocationID.TreasureGame:
                    Name = "Treasure Game";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 100, 936,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemSections = 1;
                    break;
                case LocationID.BombableShack:
                    Name = "Bombable Shack";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 219, 1171,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemSections = 1;
                    break;
                case LocationID.Blacksmith:
                    Name = "Smiths";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 616, 1054,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 295, 1325,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemSections = 1;
                    break;
                case LocationID.PurpleChest:
                    Name = "Purple Chest";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 680, 1805, new Mode()));
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 601, 1050, new Mode()));
                    itemSections = 1;
                    break;
                case LocationID.HammerPegs:
                    Name = "Hammer Pegs";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 636, 1214,
                        new Mode()
                    {
                        EntranceShuffle = false
                    }));
                    itemSections = 1;
                    break;
                case LocationID.BumperCave:
                    Name = "Bumper Cave";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 687, 340,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 680, 315,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    itemSections = 1;
                    break;
                case LocationID.Dam:
                    Name = "Dam";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 942, 1880,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 900, 1860,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    itemSections = 2;
                    break;
                case LocationID.MiniMoldormCave:
                    Name = "Mini-Moldorm";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1309, 1887,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemSections = 1;
                    break;
                case LocationID.IceRodCave:
                    Name = "Ice Rod Cave";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1795, 1547,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemSections = 1;
                    break;
                case LocationID.LakeHyliaIsland:
                    Name = "Lake Hylia Island";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1450, 1666, new Mode()));
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1450, 1666,
                        new Mode()
                        {
                            WorldState = WorldState.StandardOpen
                        }));
                    itemSections = 1;
                    break;
                case LocationID.Hobo:
                    Name = "Hobo";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1390, 1390, new Mode()));
                    itemSections = 1;
                    break;
                case LocationID.MireShack:
                    Name = "Mire Shack";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 77, 1600,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemSections = 1;
                    break;
                case LocationID.CheckerboardCave:
                    Name = "Checkerboard";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 354, 1560,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 334, 1557,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemSections = 1;
                    break;
                case LocationID.OldMan:
                    Name = "Old Man";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 816, 378,
                        new Mode()
                        {
                            WorldState = WorldState.StandardOpen,
                            EntranceShuffle = false
                        }));
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 900, 440,
                        new Mode()
                        {
                            WorldState = WorldState.Inverted,
                            EntranceShuffle = false
                        }));
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 816, 378,
                        new Mode()
                        {
                            WorldState = WorldState.Inverted,
                            EntranceShuffle = false
                        }));
                    itemSections = 1;
                    break;
                case LocationID.SpectacleRock:
                    Name = "Spectacle Rock";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 980, 178,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1036, 170,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    itemSections = 2;
                    break;
                case LocationID.EtherTablet:
                    Name = "Ether Tablet";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 844, 38, new Mode()));
                    itemSections = 1;
                    break;
                case LocationID.SpikeCave:
                    Name = "Spike Cave";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1151, 294,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemSections = 1;
                    break;
                case LocationID.SpiralCave:
                    Name = "Spiral Cave";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1598, 180,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemSections = 1;
                    break;
                case LocationID.ParadoxCave:
                    Name = "Paradox Cave";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1731, 434,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemSections = 2;
                    break;
                case LocationID.SuperBunnyCave:
                    Name = "Super-Bunny Cave";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1695, 290,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemSections = 1;
                    break;
                case LocationID.HookshotCave:
                    Name = "Hookshot Cave";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1670, 126,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemSections = 2;
                    break;
                case LocationID.FloatingIsland:
                    Name = "Floating Island";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1627, 40, new Mode()));
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1627, 40,
                        new Mode()
                        {
                            WorldState = WorldState.StandardOpen,
                            EntranceShuffle = false
                        }));
                    itemSections = 1;
                    break;
                case LocationID.MimicCave:
                    Name = "Mimic Cave";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1694, 190,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1693, 205,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemSections = 1;
                    break;
                case LocationID.HyruleCastle:
                    Name = "Escape";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1003, 906,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 925, 536, new Mode()));
                    itemSections = 1;
                    break;
                case LocationID.AgahnimTower:
                    Name = "Agahnim";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1003, 807,
                        new Mode()
                        {
                            WorldState = WorldState.StandardOpen,
                            EntranceShuffle = false
                        }));
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1126, 68,
                        new Mode()
                        {
                            WorldState = WorldState.Inverted,
                            EntranceShuffle = false
                        }));
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1000, 750,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    itemSections = 1;
                    BossSection = new BossSection(game, iD);
                    break;
                case LocationID.EasternPalace:
                    Name = "Eastern Palace";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1925, 791,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1925, 780,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    itemSections = 1;
                    BossSection = new BossSection(game, iD);
                    break;
                case LocationID.DesertPalace:
                    Name = "Desert Palace";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 146, 1584,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 150, 1700,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    itemSections = 1;
                    BossSection = new BossSection(game, iD);
                    break;
                case LocationID.TowerOfHera:
                    Name = "Tower of Hera";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1126, 68,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1125, 20,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    itemSections = 1;
                    BossSection = new BossSection(game, iD);
                    break;
                case LocationID.PalaceOfDarkness:
                    Name = "Palace of Darkness";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1924, 800,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1925, 785,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    itemSections = 1;
                    BossSection = new BossSection(game, iD);
                    break;
                case LocationID.SwampPalace:
                    Name = "Swamp Palace";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 940, 1880,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 940, 1840,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    itemSections = 1;
                    BossSection = new BossSection(game, iD);
                    break;
                case LocationID.SkullWoods:
                    Name = "Skull Woods";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 79, 121,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 80, 50,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    itemSections = 1;
                    BossSection = new BossSection(game, iD);
                    break;
                case LocationID.ThievesTown:
                    Name = "Thieves' Town";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 251, 971,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 255, 935,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    itemSections = 1;
                    BossSection = new BossSection(game, iD);
                    break;
                case LocationID.IcePalace:
                    Name = "Ice Palace";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1600, 1735,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1600, 1695,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    itemSections = 1;
                    BossSection = new BossSection(game, iD);
                    break;
                case LocationID.MiseryMire:
                    Name = "Misery Mire";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 150, 1710,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 150, 1600,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    itemSections = 1;
                    BossSection = new BossSection(game, iD);
                    break;
                case LocationID.TurtleRock:
                    Name = "Turtle Rock";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1890, 144,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1890, 125,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    itemSections = 1;
                    BossSection = new BossSection(game, iD);
                    break;
                case LocationID.GanonsTower:
                    Name = "Ganon's Tower";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1126, 68,
                        new Mode()
                        {
                            WorldState = WorldState.StandardOpen,
                            EntranceShuffle = false
                        }));
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1003, 797,
                        new Mode()
                        {
                            WorldState = WorldState.Inverted,
                            EntranceShuffle = false
                        }));
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1125, 30,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    itemSections = 1;
                    BossSection = new BossSection(game, iD);
                    break;
                case LocationID.LumberjackHouseEntrance:
                    Name = "Lumber House";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 675, 120,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.LumberjackCaveEntrance:
                    Name = "Lumberjacks";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 600, 145,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.DeathMountainEntryCave:
                    Name = "DM Entry";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 715, 350,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.DeathMountainExitCave:
                    Name = "DM Exit";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 720, 305,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.KakarikoFortuneTellerEntrance:
                    Name = "Kak Fortune";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 375, 645,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.WomanLeftDoor:
                    Name = "Woman Left";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 305, 840,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.WomanRightDoor:
                    Name = "Woman Right";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 340, 840,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.LeftSnitchHouseEntrance:
                    Name = "Left Snitch";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 100, 940,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.RightSnitchHouseEntrance:
                    Name = "Right Snitch";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 415, 965,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.BlindsHouseEntrance:
                    Name = "Blind's House";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 255, 840,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.TheWellEntrance:
                    Name = "The Well";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 47, 833,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.ChickenHouseEntrance:
                    Name = "Chicken House";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 197, 1066,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.GrassHouseEntrance:
                    Name = "Grass House";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 410, 1075,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.TavernFront:
                    Name = "Front Tavern";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 320, 1195,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.KakarikoShop:
                    Name = "Kakariko Shop";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 220, 1175,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.BombHutEntrance:
                    Name = "Bomb Hut";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 55, 1195,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.SickKidEntrance:
                    Name = "Sick Kid";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 314, 1060,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.BlacksmithHouse:
                    Name = "Smiths House";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 615, 1055,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.MagicBatEntrance:
                    Name = "Magic Bat";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 650, 1127,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.ChestGameEntrance:
                    Name = "Chest Game";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 425, 1410,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.RaceHouseLeft:
                    Name = "Race House Left";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 220, 1435,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.RaceHouseRight:
                    Name = "Race House Right";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 280, 1435,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.LibraryEntrance:
                    Name = "Library";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 313, 1310,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.ForestHideoutEntrance:
                    Name = "Forest Hideout";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 380, 264,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.ForestChestGameEntrance:
                    Name = "Forest Chest Game";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 370, 40,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.CastleSecretEntrance:
                    Name = "Castle Secret Entrance";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1196, 834,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.CastleMainEntrance:
                    Name = "Castle Main Entrance";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1000, 895,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.CastleLeftEntrance:
                    Name = "Castle Left Entrance";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 900, 780,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.CastleRightEntrance:
                    Name = "Castle Right Entrance";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1105, 780,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.CastleTowerEntrance:
                    Name = "Castle Tower Entrance";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1000, 800,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.DamEntrance:
                    Name = "Dam";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 942, 1880,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.CentralBonkRocksEntrance:
                    Name = "Central Bonk Rocks";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 945, 1310,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.WitchsHutEntrance:
                    Name = "Witch's Hut";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1607, 670,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.WaterfallFairyEntrance:
                    Name = "Waterfall Fairy";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1806, 286,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.SahasrahlasHutEntrance:
                    Name = "Sahasrahla's Hut";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1625, 900,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.TreesFairyCaveEntrance:
                    Name = "Trees Fairy Cave";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1650, 1295,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.PegsFairyCaveEntrance:
                    Name = "Pegs Fairy Cave";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1970, 1405,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.EasternPalaceEntrance:
                    Name = "Eastern Palace";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1925, 820,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.HoulihanHole:
                    Name = "Houlihan Hole";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1290, 625,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.SanctuaryGrave:
                    Name = "Sanctuary Grave";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1040, 590,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.NorthBonkRocks:
                    Name = "North Bonk Rocks";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 777, 590,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.KingsTombEntrance:
                    Name = "King's Tomb";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1207, 598,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.GraveyardLedgeEntrance:
                    Name = "Graveyard Ledge";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1132, 549,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.DesertLeftEntrance:
                    Name = "Desert Left Entrance";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 70, 1590,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.DesertBackEntrance:
                    Name = "Desert Back Entrance";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 150, 1540,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.DesertRightEntrance:
                    Name = "Desert Right Entrance";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 225, 1590,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.DesertFrontEntrance:
                    Name = "Desert Front Entrance";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 150, 1600,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.AginahsCaveEntrance:
                    Name = "Aginah's Cave";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 400, 1655,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.ThiefCaveEntrance:
                    Name = "Thief Cave";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 555, 1790,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.RupeeCaveEntrance:
                    Name = "Rupee Cave";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 625, 1920,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.SkullWoodsBack:
                    Name = "Skull Woods Back";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 80, 100,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.ThievesTownEntrance:
                    Name = "Thieves Town";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 255, 975,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.CShapedHouseEntrance:
                    Name = "C-Shaped House";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 414, 969,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.HammerHouse:
                    Name = "Hammer House";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 408, 1069,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.DarkVillageFortuneTellerEntrance:
                    Name = "Dark Village Fortune Teller";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 377, 647,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.DarkChapelEntrance:
                    Name = "Dark Chapel";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 924, 551,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.ShieldShop:
                    Name = "Shield Shop";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 665, 922,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.DarkLumberjack:
                    Name = "Dark Lumberjack";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 675, 115,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.TreasureGameEntrance:
                    Name = "Treasure Game";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 100, 936,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.BombableShackEntrance:
                    Name = "Bombable Shack";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 219, 1171,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.HammerPegsEntrance:
                    Name = "Hammer Pegs";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 636, 1214,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.BumperCaveExit:
                    Name = "Bumper Cave Exit";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 720, 310,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.BumperCaveEntrance:
                    Name = "Bumper Cave Entry";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 715, 355,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.HypeCaveEntrance:
                    Name = "Hype Cave";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1200, 1560,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.SwampPalaceEntrance:
                    Name = "Swamp Palace";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 940, 1875,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.DarkCentralBonkRocksEntrance:
                    Name = "Bonk Rocks";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 945, 1310,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.SouthOfGroveEntrance:
                    Name = "Lumberjacks";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 552, 1693,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.BombShop:
                    Name = "Bomb Shop";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1098, 1382,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.ArrowGameEntrance:
                    Name = "Arrow Game";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 431, 1409,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.DarkHyliaFortuneTeller:
                    Name = "Dark Hylia Fortune Teller";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1300, 1615,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.DarkTreesFairyCaveEntrance:
                    Name = "Dark Trees Fairy Cave";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1656, 1296,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.DarkSahasrahlaEntrance:
                    Name = "Dark Saha";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1706, 1008,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.PalaceOfDarknessEntrance:
                    Name = "Palace of Darkness";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1925, 830,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.DarkWitchsHut:
                    Name = "Dark Witch's Hut";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1616, 678,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.DarkFluteSpotFiveEntrance:
                    Name = "Dark Flute Spot 5";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1968, 1405,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.FatFairyEntrance:
                    Name = "Fat Fairy";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 940, 976,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.GanonHole:
                    Name = "Ganon Hole";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 902, 862,
                        new Mode()
                        {
                            WorldState = WorldState.Inverted,
                            EntranceShuffle = true
                        }));
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1000, 820,
                        new Mode()
                        {
                            WorldState = WorldState.StandardOpen,
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.DarkIceRodCaveEntrance:
                    Name = "Dark Ice Rod Cave";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1795, 1545,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.DarkFakeIceRodCaveEntrance:
                    Name = "Dark Fake Ice Rod Cave";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1835, 1545,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.DarkIceRodRockEntrance:
                    Name = "Dark Ice Rod Rock";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1810, 1585,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.HypeFairyCaveEntrance:
                    Name = "Hype Fairy Cave";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1200, 1565,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.FortuneTellerEntrance:
                    Name = "Fortune Teller";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1300, 1615,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.LakeShop:
                    Name = "Lake Shop";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1460, 1540,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.UpgradeFairy:
                    Name = "Upgrade Fairy";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1590, 1710,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.MiniMoldormCaveEntrance:
                    Name = "Mini Moldorm Cave";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1309, 1887,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.IceRodCaveEntrance:
                    Name = "Ice Rod Cave";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1795, 1545,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.IceBeeCaveEntrance:
                    Name = "Ice Bee Cave";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1835, 1545,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.IceFairyCaveEntrance:
                    Name = "Ice Fairy Cave";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1810, 1585,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.IcePalaceEntrance:
                    Name = "Ice Palace";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1600, 1735,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.MiseryMireEntrance:
                    Name = "Misery Mire";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 150, 1650,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.MireShackEntrance:
                    Name = "Mire Shack";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 77, 1600,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.MireRightShackEntrance:
                    Name = "Mire Right Shack";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 220, 1610,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.MireCaveEntrance:
                    Name = "Mire Cave";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 400, 1655,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.CheckerboardCaveEntrance:
                    Name = "Checkerboard";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 354, 1560,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.DeathMountainEntranceBack:
                    Name = "DM Entry Back";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 816, 378,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.OldManResidence:
                    Name = "Old Man Residence";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 900, 470,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.OldManBackResidence:
                    Name = "Old Man Back Residence";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1075, 325,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.DeathMountainExitFront:
                    Name = "Death Mountain Exit Front";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 790, 275,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.SpectacleRockLeft:
                    Name = "Spectacle Rock Left";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 920, 280,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.SpectacleRockRight:
                    Name = "Spectacle Rock Right";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 980, 290,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.SpectacleRockTop:
                    Name = "Spectacle Rock Top";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 980, 205,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.SpikeCaveEntrance:
                    Name = "Spike Cave";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1151, 294,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.DarkMountainFairyEntrance:
                    Name = "Dark Mountain Fairy";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 815, 376,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.TowerOfHeraEntrance:
                    Name = "Tower Of Hera";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1125, 65,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.SpiralCaveBottom:
                    Name = "Spiral Cave Bottom";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1605, 260,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.EDMFairyCaveEntrance:
                    Name = "EDM Fairy Cave";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1695, 290,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.ParadoxCaveMiddle:
                    Name = "Paradox Cave Middle";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1735, 290,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.ParadoxCaveBottom:
                    Name = "Paradox Cave Bottom";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1731, 434,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.EDMConnectorBottom:
                    Name = "EDM Connector Bottom";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1645, 275,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.SpiralCaveTop:
                    Name = "Spiral Cave Top";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1600, 180,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.MimicCaveEntrance:
                    Name = "Mimic Cave";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1695, 180,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.EDMConnectorTop:
                    Name = "EDM Connector Top";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1645, 230,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.ParadoxCaveTop:
                    Name = "Paradox Cave Top";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1725, 125,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.SuperBunnyCaveBottom:
                    Name = "Super-Bunny Cave Bottom";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1685, 295,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.DeathMountainShop:
                    Name = "Death Mountain Shop";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1725, 295,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.SuperBunnyCaveTop:
                    Name = "Super-Bunny Cave Top";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1725, 128,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.HookshotCaveEntrance:
                    Name = "Hookshot Cave";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1670, 126,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.TurtleRockEntrance:
                    Name = "Turtle Rock";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1890, 165,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.GanonsTowerEntrance:
                    Name = "Ganon's Tower";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1125, 70,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.TRLedgeLeft:
                    Name = "TR Ledge Left";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1598, 182,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.TRLedgeRight:
                    Name = "TR Ledge Right";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1694, 182,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.TRSafetyDoor:
                    Name = "TR Safety Door";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1648, 229,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.HookshotCaveTop:
                    Name = "Hookshot Cave Top";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1627, 40,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.LinksHouseEntrance:
                    Name = "Link's House";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1098, 1382,
                        new Mode()
                        {
                            WorldState = WorldState.Inverted,
                            EntranceShuffle = true
                        }));
                    entranceSection = true;
                    break;
                case LocationID.TreesFairyCaveTakeAny:
                    Name = "Trees Fairy Cave";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1650, 1295,
                        new Mode()
                        {
                            WorldState = WorldState.Retro,
                            EntranceShuffle = false
                        }));
                    takeAnySection = true;
                    break;
                case LocationID.PegsFairyCaveTakeAny:
                    Name = "Pegs Fairy Cave";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1970, 1405,
                        new Mode()
                        {
                            WorldState = WorldState.Retro,
                            EntranceShuffle = false
                        }));
                    takeAnySection = true;
                    break;
                case LocationID.KakarikoFortuneTellerTakeAny:
                    Name = "Kak Fortune";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 375, 645,
                        new Mode()
                        {
                            WorldState = WorldState.Retro,
                            EntranceShuffle = false
                        }));
                    takeAnySection = true;
                    break;
                case LocationID.GrassHouseTakeAny:
                    Name = "Grass House";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 410, 1075,
                        new Mode()
                        {
                            WorldState = WorldState.Retro,
                            EntranceShuffle = false
                        }));
                    takeAnySection = true;
                    break;
                case LocationID.ForestChestGameTakeAny:
                    Name = "Forest Chest Game";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 370, 40,
                        new Mode()
                        {
                            WorldState = WorldState.Retro,
                            EntranceShuffle = false
                        }));
                    takeAnySection = true;
                    break;
                case LocationID.LumberjackHouseTakeAny:
                    Name = "Lumber House";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 688, 120,
                        new Mode()
                        {
                            WorldState = WorldState.Retro,
                            EntranceShuffle = false
                        }));
                    takeAnySection = true;
                    break;
                case LocationID.LeftSnitchHouseTakeAny:
                    Name = "Left Snitch";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 100, 940,
                        new Mode()
                        {
                            WorldState = WorldState.Retro,
                            EntranceShuffle = false
                        }));
                    takeAnySection = true;
                    break;
                case LocationID.RightSnitchHouseTakeAny:
                    Name = "Right Snitch";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 415, 965,
                        new Mode()
                        {
                            WorldState = WorldState.Retro,
                            EntranceShuffle = false
                        }));
                    takeAnySection = true;
                    break;
                case LocationID.BombHutTakeAny:
                    Name = "Bomb Hut";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 55, 1195,
                        new Mode()
                        {
                            WorldState = WorldState.Retro,
                            EntranceShuffle = false
                        }));
                    takeAnySection = true;
                    break;
                case LocationID.IceFairyCaveTakeAny:
                    Name = "Ice Fairy Cave";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1810, 1602,
                        new Mode()
                        {
                            WorldState = WorldState.Retro,
                            EntranceShuffle = false
                        }));
                    takeAnySection = true;
                    break;
                case LocationID.RupeeCaveTakeAny:
                    Name = "Rupee Cave";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 625, 1920,
                        new Mode()
                        {
                            WorldState = WorldState.Retro,
                            EntranceShuffle = false
                        }));
                    takeAnySection = true;
                    break;
                case LocationID.CentralBonkRocksTakeAny:
                    Name = "Central Bonk Rocks";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 945, 1310,
                        new Mode()
                        {
                            WorldState = WorldState.Retro,
                            EntranceShuffle = false
                        }));
                    takeAnySection = true;
                    break;
                case LocationID.ThiefCaveTakeAny:
                    Name = "Thief Cave";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 555, 1790,
                        new Mode()
                        {
                            WorldState = WorldState.Retro,
                            EntranceShuffle = false
                        }));
                    takeAnySection = true;
                    break;
                case LocationID.IceBeeCaveTakeAny:
                    Name = "Ice Bee Cave";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1850, 1545,
                        new Mode()
                        {
                            WorldState = WorldState.Retro,
                            EntranceShuffle = false
                        }));
                    takeAnySection = true;
                    break;
                case LocationID.FortuneTellerTakeAny:
                    Name = "Fortune Teller";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1300, 1615,
                        new Mode()
                        {
                            WorldState = WorldState.Retro,
                            EntranceShuffle = false
                        }));
                    takeAnySection = true;
                    break;
                case LocationID.HypeFairyCaveTakeAny:
                    Name = "Hype Fairy Cave";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1200, 1565,
                        new Mode()
                        {
                            WorldState = WorldState.Retro,
                            EntranceShuffle = false
                        }));
                    takeAnySection = true;
                    break;
                case LocationID.ChestGameTakeAny:
                    Name = "Chest Game";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 425, 1410,
                        new Mode()
                        {
                            WorldState = WorldState.Retro,
                            EntranceShuffle = false
                        }));
                    takeAnySection = true;
                    break;
                case LocationID.EDMFairyCaveTakeAny:
                    Name = "EDM Fairy Cave";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1695, 290,
                        new Mode()
                        {
                            WorldState = WorldState.Retro,
                            EntranceShuffle = false
                        }));
                    takeAnySection = true;
                    break;
                case LocationID.DarkChapelTakeAny:
                    Name = "Dark Chapel";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 924, 551,
                        new Mode()
                        {
                            WorldState = WorldState.Retro,
                            EntranceShuffle = false
                        }));
                    takeAnySection = true;
                    break;
                case LocationID.DarkVillageFortuneTellerTakeAny:
                    Name = "Dark Village Fortune Teller";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 377, 647,
                        new Mode()
                        {
                            WorldState = WorldState.Retro,
                            EntranceShuffle = false
                        }));
                    takeAnySection = true;
                    break;
                case LocationID.DarkTreesFairyCaveTakeAny:
                    Name = "Dark Trees Fairy Cave";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1656, 1296,
                        new Mode()
                        {
                            WorldState = WorldState.Retro,
                            EntranceShuffle = false
                        }));
                    takeAnySection = true;
                    break;
                case LocationID.DarkSahasrahlaTakeAny:
                    Name = "Dark Saha";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1706, 1008,
                        new Mode()
                        {
                            WorldState = WorldState.Retro,
                            EntranceShuffle = false
                        }));
                    takeAnySection = true;
                    break;
                case LocationID.DarkFluteSpotFiveTakeAny:
                    Name = "Dark Flute Spot 5";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1968, 1405,
                        new Mode()
                        {
                            WorldState = WorldState.Retro,
                            EntranceShuffle = false
                        }));
                    takeAnySection = true;
                    break;
                case LocationID.ArrowGameTakeAny:
                    Name = "Arrow Game";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 431, 1409,
                        new Mode()
                        {
                            WorldState = WorldState.Retro,
                            EntranceShuffle = false
                        }));
                    takeAnySection = true;
                    break;
                case LocationID.DarkCentralBonkRocksTakeAny:
                    Name = "Bonk Rocks";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 945, 1310,
                        new Mode()
                        {
                            WorldState = WorldState.Retro,
                            EntranceShuffle = false
                        }));
                    takeAnySection = true;
                    break;
                case LocationID.DarkIceRodCaveTakeAny:
                    Name = "Dark Ice Rod Cave";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1795, 1545,
                        new Mode()
                        {
                            WorldState = WorldState.Retro,
                            EntranceShuffle = false
                        }));
                    takeAnySection = true;
                    break;
                case LocationID.DarkFakeIceRodCaveTakeAny:
                    Name = "Dark Fake Ice Rod Cave";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1835, 1545,
                        new Mode()
                        {
                            WorldState = WorldState.Retro,
                            EntranceShuffle = false
                        }));
                    takeAnySection = true;
                    break;
                case LocationID.DarkIceRodRockTakeAny:
                    Name = "Dark Ice Rod Rock";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1810, 1585,
                        new Mode()
                        {
                            WorldState = WorldState.Retro,
                            EntranceShuffle = false
                        }));
                    takeAnySection = true;
                    break;
                case LocationID.DarkMountainFairyTakeAny:
                    Name = "Dark Mountain Fairy";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 815, 376,
                        new Mode()
                        {
                            WorldState = WorldState.Retro,
                            EntranceShuffle = false
                        }));
                    takeAnySection = true;
                    break;
                case LocationID.MireRightShackTakeAny:
                    Name = "Mire Right Shack";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 220, 1610,
                        new Mode()
                        {
                            WorldState = WorldState.Retro,
                            EntranceShuffle = false
                        }));
                    takeAnySection = true;
                    break;
                case LocationID.MireCaveTakeAny:
                    Name = "Mire Cave";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 400, 1655,
                        new Mode()
                        {
                            WorldState = WorldState.Retro,
                            EntranceShuffle = false
                        }));
                    takeAnySection = true;
                    break;
            }

            for (int i = 0; i < itemSections; i++)
                Sections.Add(new ItemSection(game, this, i));

            if (entranceSection)
                Sections.Add(new EntranceSection(game, ID));

            if (takeAnySection)
                Sections.Add(new TakeAnySection(game, ID));

            if (BossSection != null)
                Sections.Add(BossSection);

            foreach (ISection section in Sections)
            {
                if (section is ItemSection itemSection)
                    Total += itemSection.Total;

                section.PropertyChanged += OnSectionChanged;
            }

            UpdateAccessibility();
            UpdateAccessible();
            UpdateAvailable();
            UpdateTotal();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnSectionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ISection.Available))
            {
                UpdateAccessibility();
                UpdateAvailable();
            }

            if (e.PropertyName == nameof(ISection.Accessibility))
                UpdateAccessibility();

            if (e.PropertyName == nameof(ItemSection.Accessible))
                UpdateAccessible();

            if (e.PropertyName == nameof(ItemSection.Total))
                UpdateTotal();
        }

        private void UpdateAccessibility()
        {
            AccessibilityLevel? leastAccessible = null;
            AccessibilityLevel? mostAccessible = null;

            bool available = false;

            foreach (ISection section in Sections)
            {
                if (section.IsAvailable() && _game.Mode.Validate(section.RequiredMode))
                {
                    available = true;
                    AccessibilityLevel sectionAccessibility = section.Accessibility;

                    if (leastAccessible == null || leastAccessible > sectionAccessibility)
                        leastAccessible = sectionAccessibility;
                    if (mostAccessible == null || mostAccessible < sectionAccessibility)
                        mostAccessible = sectionAccessibility;
                }
            }

            if (!available)
                Accessibility = AccessibilityLevel.Cleared;
            else
            {
                Accessibility = mostAccessible.Value switch
                {
                    AccessibilityLevel.None => AccessibilityLevel.None,
                    AccessibilityLevel.Inspect => AccessibilityLevel.Inspect,
                    AccessibilityLevel.Partial => AccessibilityLevel.Partial,
                    AccessibilityLevel.SequenceBreak when leastAccessible.Value <= AccessibilityLevel.Partial => AccessibilityLevel.Partial,
                    AccessibilityLevel.SequenceBreak => AccessibilityLevel.SequenceBreak,
                    AccessibilityLevel.Normal when leastAccessible.Value <= AccessibilityLevel.Partial => AccessibilityLevel.Partial,
                    AccessibilityLevel.Normal when leastAccessible.Value == AccessibilityLevel.SequenceBreak => AccessibilityLevel.SequenceBreak,
                    AccessibilityLevel.Normal => AccessibilityLevel.Normal,
                    _ => throw new Exception(string.Format(CultureInfo.InvariantCulture, "Unknown availability state for location {0}", ID.ToString())),
                };
            }
        }

        private void UpdateAvailable()
        {
            int available = 0;

            foreach (ISection section in Sections)
            {
                if (section is ItemSection itemSection)
                    available += itemSection.Available;
            }

            Available = available;
        }

        private void UpdateAccessible()
        {
            int accessible = 0;

            foreach (ISection section in Sections)
            {
                if (section is ItemSection itemSection)
                    accessible += itemSection.Accessible;
            }

            Accessible = accessible;
        }

        private void UpdateTotal()
        {
            int total = 0;

            foreach (ISection section in Sections)
            {
                if (section is ItemSection itemSection)
                    total += itemSection.Total;
            }

            Total = total;
        }

        public void Reset()
        {
            foreach (ISection section in Sections)
                section.Reset();
        }
    }
}
