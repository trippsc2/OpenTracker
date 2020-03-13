using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models
{
    public class Location
    {
        private readonly LocationID _iD;

        public event EventHandler ItemRequirementChanged;

        public string Name { get; }

        public List<MapLocation> MapLocations { get; }
        public List<ISection> Sections { get; }

        public Location(Game game, LocationID iD)
        {
            _iD = iD;

            MapLocations = new List<MapLocation>();
            Sections = new List<ISection>();

            int itemCollections = 0;

            List<Item> itemRequirements = new List<Item>();

            switch (iD)
            {
                case LocationID.Pedestal:
                    Name = "Master Sword Pedestal";
                    MapLocations.Add(new MapLocation(this, MapID.LightWorld, 83, 101, new Mode()));
                    itemCollections = 1;
                    break;
                case LocationID.LumberjackCave:
                    /*Placements.Add(new LocationPlacement(this, MapID.LightWorld, 633, 117,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));*/
                    break;
                case LocationID.BlindsHouse:
                    /*Placements.Add(new LocationPlacement(this, MapID.LightWorld, 307, 840,
                        new Mode()
                        {
                            EntranceShuffle = false
                        }));*/
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
                case LocationID.CastleSecretEntrance:
                    break;
                case LocationID.LinksHouse:
                    break;
                case LocationID.GroveDiggingSpot:
                    break;
                case LocationID.PyramidLedge:
                    break;
                case LocationID.PyramidFairy:
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
                case LocationID.WitchsHut:
                    break;
                case LocationID.WaterfallFairy:
                    break;
                case LocationID.ZoraArea:
                    break;
                case LocationID.Catfish:
                    break;
                case LocationID.SahasrahlasHut:
                    break;
                case LocationID.BonkRocks:
                    break;
                case LocationID.KingsTomb:
                    break;
                case LocationID.GraveyardLedge:
                    break;
                case LocationID.DesertLedge:
                    break;
                case LocationID.AginahsCave:
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
                case LocationID.Dam:
                    break;
                case LocationID.MiniMoldormCave:
                    break;
                case LocationID.IceRodCave:
                    break;
                case LocationID.Hobo:
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
                case LocationID.HyruleCastle:
                    break;
                case LocationID.Agahnim:
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
            }

            for (int i = 0; i < itemCollections; i++)
                Sections.Add(new ItemSection(game, _iD, i));

            foreach (ISection section in Sections)
                section.ItemRequirementChanged += OnItemRequirementChanged;
        }

        private void OnItemRequirementChanged(object sender, EventArgs e)
        {
            if (ItemRequirementChanged != null)
                ItemRequirementChanged.Invoke(this, new EventArgs());
        }

        public Accessibility GetAccessibility(Mode mode, ItemDictionary items)
        {
            Accessibility? leastAccessible = null;
            Accessibility? mostAccessible = null;

            bool available = false;

            foreach (ISection section in Sections)
            {
                if (section.IsAvailable())
                {
                    available = true;
                    Accessibility sectionAccessibility = section.GetAccessibility(mode, items);

                    if (leastAccessible == null || leastAccessible > sectionAccessibility)
                        leastAccessible = sectionAccessibility;
                    if (mostAccessible == null || mostAccessible < sectionAccessibility)
                        mostAccessible = sectionAccessibility;
                }
            }

            if (!available)
                return Accessibility.Cleared;

            return mostAccessible.Value switch
            {
                Accessibility.None => Accessibility.None,
                Accessibility.Inspect => Accessibility.Inspect,
                Accessibility.SequenceBreak when leastAccessible.Value <= Accessibility.Inspect => Accessibility.Partial,
                Accessibility.SequenceBreak => Accessibility.SequenceBreak,
                Accessibility.Normal when leastAccessible.Value <= Accessibility.Inspect => Accessibility.Partial,
                Accessibility.Normal when leastAccessible.Value == Accessibility.SequenceBreak => Accessibility.SequenceBreak,
                Accessibility.Normal => Accessibility.Normal,
                _ => throw new Exception(string.Format("Unknown availability state for location {0}", _iD.ToString())),
            };
        }
    }
}
