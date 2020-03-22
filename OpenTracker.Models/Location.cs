using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models
{
    public class Location
    {

        public LocationID ID { get; }
        public string Name { get; }

        public BossSection BossSection { get; private set; }
        public List<MapLocation> MapLocations { get; }
        public List<ISection> Sections { get; }

        public event EventHandler ItemRequirementChanged;

        public Location(Game game, LocationID iD)
        {
            ID = iD;

            MapLocations = new List<MapLocation>();
            Sections = new List<ISection>();

            int itemCollections = 0;

            List<Item> itemRequirements = new List<Item>();

            switch (iD)
            {
                case LocationID.Pedestal:
                    Name = "Pedestal";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 83, 101, new Mode()));
                    itemCollections = 1;
                    break;
                case LocationID.LumberjackCave:
                    Name = "Lumberjacks";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 633, 117,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemCollections = 1;
                    break;
                case LocationID.BlindsHouse:
                    Name = "Blind's House";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 307, 840,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemCollections = 2;
                    break;
                case LocationID.TheWell:
                    Name = "The Well";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 47, 833,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemCollections = 2;
                    break;
                case LocationID.BottleVendor:
                    Name = "Bottle Vendor";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 190, 933, new Mode()));
                    itemCollections = 1;
                    break;
                case LocationID.ChickenHouse:
                    Name = "Chicken House";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 197, 1066,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemCollections = 1;
                    break;
                case LocationID.Tavern:
                    Name = "Tavern";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 320, 1145,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemCollections = 1;
                    break;
                case LocationID.SickKid:
                    Name = "Sick Kid";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 314, 1060,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemCollections = 1;
                    break;
                case LocationID.MagicBat:
                    Name = "Magic Bat";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 650, 1127,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemCollections = 1;
                    break;
                case LocationID.RaceGame:
                    Name = "Race Game";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 111, 1354, new Mode()));
                    itemCollections = 1;
                    break;
                case LocationID.Library:
                    Name = "Library";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 313, 1310,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemCollections = 1;
                    break;
                case LocationID.LostWoods:
                    Name = "Lost Woods";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 320, 260, new Mode()));
                    itemCollections = 2;
                    break;
                case LocationID.CastleSecretEntrance:
                    Name = "Uncle";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1196, 834,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemCollections = 1;
                    itemCollections = 1;
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
                            WorldState = WorldState.Inverted
                        }));
                    itemCollections = 1;
                    break;
                case LocationID.GroveDiggingSpot:
                    Name = "Dig Spot";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 600, 1350, new Mode()));
                    itemCollections = 1;
                    break;
                case LocationID.PyramidLedge:
                    Name = "Pyramid Ledge";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1164, 922, new Mode()));
                    itemCollections = 1;
                    break;
                case LocationID.FatFairy:
                    Name = "Fat Fairy";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 940, 976,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemCollections = 1;
                    break;
                case LocationID.HauntedGrove:
                    Name = "Haunted Grove";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 620, 1371, new Mode()));
                    itemCollections = 1;
                    break;
                case LocationID.HypeCave:
                    Name = "Hype Cave";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1200, 1560,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemCollections = 1;
                    break;
                case LocationID.BombosTablet:
                    Name = "Bombos Tablet";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 440, 1845, new Mode()));
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 440, 1845, new Mode()));
                    itemCollections = 1;
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
                            EntranceShuffle = false
                        }));
                    itemCollections = 1;
                    break;
                case LocationID.DiggingGame:
                    Name = "Digging Game";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 100, 1385, new Mode()));
                    itemCollections = 1;
                    break;
                case LocationID.WitchsHut:
                    Name = "Witch's Hut";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1607, 670,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemCollections = 1;
                    break;
                case LocationID.WaterfallFairy:
                    Name = "Waterfall Fairy";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1806, 286,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemCollections = 1;
                    break;
                case LocationID.ZoraArea:
                    Name = "Zora Area";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1920, 273, new Mode()));
                    itemCollections = 2;
                    break;
                case LocationID.Catfish:
                    Name = "Catfish";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1813, 347, new Mode()));
                    itemCollections = 1;
                    break;
                case LocationID.SahasrahlasHut:
                    Name = "Sahash's Hut";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1625, 900,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemCollections = 2;
                    break;
                case LocationID.BonkRocks:
                    Name = "Bonk Rocks";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 777, 590,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemCollections = 1;
                    break;
                case LocationID.KingsTomb:
                    Name = "King's Tomb";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1207, 598,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemCollections = 1;
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
                            EntranceShuffle = false
                        }));
                    itemCollections = 1;
                    break;
                case LocationID.DesertLedge:
                    Name = "Desert Ledge";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 40, 1835, new Mode()));
                    itemCollections = 1;
                    break;
                case LocationID.AginahsCave:
                    Name = "Aginah's Cave";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 400, 1655,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemCollections = 1;
                    break;
                case LocationID.CShapedHouse:
                    Name = "C-Shaped House";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 414, 969,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemCollections = 1;
                    break;
                case LocationID.TreasureGame:
                    Name = "Treasure Game";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 100, 936,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemCollections = 1;
                    break;
                case LocationID.BombableShack:
                    Name = "Bombable Shack";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 219, 1171,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemCollections = 1;
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
                    itemCollections = 1;
                    break;
                case LocationID.PurpleChest:
                    Name = "Purple Chest";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 680, 1805, new Mode()));
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 601, 1050, new Mode()));
                    itemCollections = 1;
                    break;
                case LocationID.HammerPegs:
                    Name = "Hammer Pegs";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 636, 1214, new Mode()));
                    itemCollections = 1;
                    break;
                case LocationID.BumperCave:
                    Name = "Bumper Cave";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 687, 340,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemCollections = 1;
                    break;
                case LocationID.Dam:
                    Name = "Dam";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 942, 1880,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemCollections = 2;
                    break;
                case LocationID.MiniMoldormCave:
                    Name = "Mini-Moldorm";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1309, 1887,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemCollections = 1;
                    break;
                case LocationID.IceRodCave:
                    Name = "Ice Rod Cave";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1795, 1547,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemCollections = 1;
                    break;
                case LocationID.LakeHyliaIsland:
                    Name = "Lake Hylia Island";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1450, 1666, new Mode()));
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1450, 1666, new Mode()));
                    itemCollections = 1;
                    break;
                case LocationID.Hobo:
                    Name = "Hobo";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1390, 1390, new Mode()));
                    itemCollections = 1;
                    break;
                case LocationID.MireShack:
                    Name = "Mire Shack";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 77, 1600,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemCollections = 1;
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
                    itemCollections = 1;
                    break;
                case LocationID.OldMan:
                    Name = "Old Man";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 816, 378,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemCollections = 1;
                    break;
                case LocationID.SpectacleRock:
                    Name = "Spectacle Rock";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 980, 178,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemCollections = 2;
                    break;
                case LocationID.EtherTablet:
                    Name = "Ether Tablet";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 844, 38, new Mode()));
                    itemCollections = 1;
                    break;
                case LocationID.SpikeCave:
                    Name = "Spike Cave";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1151, 294,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemCollections = 1;
                    break;
                case LocationID.SpiralCave:
                    Name = "Spiral Cave";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1598, 180,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemCollections = 1;
                    break;
                case LocationID.ParadoxCave:
                    Name = "Paradox Cave";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1731, 434,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1714, 293,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemCollections = 2;
                    break;
                case LocationID.SuperBunnyCave:
                    Name = "Super-Bunny Cave";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1695, 290,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemCollections = 1;
                    break;
                case LocationID.HookshotCave:
                    Name = "Hookshot Cave";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1670, 126,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemCollections = 2;
                    break;
                case LocationID.FloatingIsland:
                    Name = "Floating Island";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1627, 40,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 1627, 40,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    itemCollections = 1;
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
                    itemCollections = 1;
                    break;
                case LocationID.HyruleCastle:
                    Name = "Escape";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1003, 906,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 925, 536,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1000, 940,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    itemCollections = 1;
                    break;
                case LocationID.Agahnim:
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
                    itemCollections = 1;
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
                    itemCollections = 1;
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
                    itemCollections = 1;
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
                    itemCollections = 1;
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
                    itemCollections = 1;
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
                    itemCollections = 1;
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
                    itemCollections = 1;
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
                    itemCollections = 1;
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
                    itemCollections = 1;
                    BossSection = new BossSection(game, iD);
                    break;
                case LocationID.MiseryMire:
                    Name = "Misery Mire";
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 150, 1670,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));
                    MapLocations.Add(new MapLocation(this, MapID.DarkWorld, 150, 1600,
                        new Mode()
                        {
                            EntranceShuffle = true
                        }));
                    itemCollections = 1;
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
                    itemCollections = 1;
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
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 1003, 807,
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
                    itemCollections = 1;
                    break;
            }

            for (int i = 0; i < itemCollections; i++)
                Sections.Add(new ItemSection(game, this, i));

            if (BossSection != null)
                Sections.Add(BossSection);

            foreach (ISection section in Sections)
                section.PropertyChanged += OnItemRequirementChanged;
        }

        private void OnItemRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            if (ItemRequirementChanged != null)
                ItemRequirementChanged.Invoke(this, new EventArgs());
        }

        public AccessibilityLevel GetAccessibility(Mode mode, ItemDictionary items)
        {
            AccessibilityLevel? leastAccessible = null;
            AccessibilityLevel? mostAccessible = null;

            bool available = false;

            foreach (ISection section in Sections)
            {
                if (section.IsAvailable())
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
                return AccessibilityLevel.Cleared;

            return mostAccessible.Value switch
            {
                AccessibilityLevel.None => AccessibilityLevel.None,
                AccessibilityLevel.Inspect => AccessibilityLevel.Inspect,
                AccessibilityLevel.SequenceBreak when leastAccessible.Value <= AccessibilityLevel.Inspect => AccessibilityLevel.Partial,
                AccessibilityLevel.SequenceBreak => AccessibilityLevel.SequenceBreak,
                AccessibilityLevel.Normal when leastAccessible.Value <= AccessibilityLevel.Inspect => AccessibilityLevel.Partial,
                AccessibilityLevel.Normal when leastAccessible.Value == AccessibilityLevel.SequenceBreak => AccessibilityLevel.SequenceBreak,
                AccessibilityLevel.Normal => AccessibilityLevel.Normal,
                _ => throw new Exception(string.Format("Unknown availability state for location {0}", ID.ToString())),
            };
        }
    }
}
