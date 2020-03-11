using OpenTracker.Enums;
using OpenTracker.Interfaces;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models
{
    public class ItemLocation : ILocation
    {
        private readonly Game _game;
        private readonly LocationID _iD;

        public string Name { get; }

        public List<LocationPlacement> Placements { get; }
        public List<ItemSection> ItemSections { get; }

        public ItemLocation(Game game, LocationID iD)
        {
            _game = game;
            _iD = iD;

            Placements = new List<LocationPlacement>();
            ItemSections = new List<ItemSection>();

            int itemCollections = 1;

            switch (iD)
            {
                case LocationID.Pedestal:
                    Name = "Master Sword Pedestal";
                    Placements.Add(new LocationPlacement(this, MapID.LightWorld, 83, 101, new Mode()));
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
                ItemSections.Add(new ItemSection(_game, _iD, i));
        }

        public Accessibility GetAccessibility()
        {
            Accessibility? leastAccessible = null;
            Accessibility? mostAccessible = null;

            int itemsAvailable = 0;

            foreach (ItemSection section in ItemSections)
            {
                itemsAvailable += section.Available;

                Accessibility sectionAccessibility = section.GetAccessibility();

                if (leastAccessible == null || leastAccessible > sectionAccessibility)
                    leastAccessible = sectionAccessibility;
                if (mostAccessible == null || mostAccessible < sectionAccessibility)
                    mostAccessible = sectionAccessibility;
            }

            if (itemsAvailable == 0)
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
