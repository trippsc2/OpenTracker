using OpenTracker.Enums;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models
{
    public class ItemSection
    {
        private Game _game;
        private readonly bool _mapCompass;
        private readonly int _smallKeys;
        private readonly bool _bigKeys;

        public string Name { get; }
        public bool HasVisibleItem { get; }
        public int Total { get; }
        public int Available { get; set; }
        public Item VisibleItem { get; set; }

        public Func<Accessibility> GetAccessibility { get; }

        public ItemSection(Game game, LocationID iD, int index = 0)
        {
            _game = game;

            switch (iD)
            {
                case LocationID.Pedestal:

                    Name = "Pedestal";
                    HasVisibleItem = true;
                    Total = 1;
                    Available = 1;

                    GetAccessibility = () =>
                    {
                        if (_game.Items.Has(ItemType.GreenPendant) && _game.Items.Has(ItemType.Pendant, 2))
                        {
                            if ((_game.Mode.ItemPlacement.Value == ItemPlacement.Advanced || _game.Items.Has(ItemType.Book)) &&
                                (_game.Mode.WorldState.Value == WorldState.StandardOpen || _game.Items.Has(ItemType.MoonPearl)))
                                return Accessibility.Normal;

                            if (_game.Mode.WorldState.Value == WorldState.StandardOpen &&
                                _game.Mode.ItemPlacement.Value == ItemPlacement.Basic)
                                return Accessibility.SequenceBreak;
                        }

                        if (_game.Items.Has(ItemType.Book))
                            return Accessibility.Inspect;

                        return Accessibility.None;
                    };

                    break;
                case LocationID.LumberjackCave:
                    break;
                case LocationID.BlindsHouse:
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
        }
    }
}
