using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models
{
    public class ItemSection : ISection
    {
        private readonly bool _mapCompass;
        private readonly int _smallKeys;
        private readonly bool _bigKey;
        private readonly int _baseTotal;

        public event EventHandler ItemRequirementChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get; }
        public bool HasVisibleItem { get; }

        private int _available;
        public int Available
        {
            get => _available;
            set
            {
                if (_available != value)
                {
                    _available = value;
                    OnPropertyChanged(nameof(Available));
                }
            }
        }

        private int _total;
        public int Total
        {
            get => _total;
            set
            {
                if (_total != value)
                {
                    _total = value;
                    OnPropertyChanged(nameof(Total));
                }
            }
        }

        private Item _visibleItem;
        public Item VisibleItem
        {
            get => _visibleItem;
            set
            {
                if (_visibleItem != value)
                {
                    _visibleItem = value;
                    OnPropertyChanged(nameof(VisibleItem));
                }
            }
        }

        public Func<Mode, ItemDictionary, Accessibility> GetAccessibility { get; }

        public ItemSection(Game game, LocationID iD, int index = 0)
        {
            game.Mode.PropertyChanged += OnGameModeChanged;

            List<Item> itemRequirements = new List<Item>();

            switch (iD)
            {
                case LocationID.Pedestal:

                    _baseTotal = 1;
                    Name = "Pedestal";
                    HasVisibleItem = true;

                    GetAccessibility = (mode, items) =>
                    {
                        if (items.Has(ItemType.GreenPendant) && items.Has(ItemType.Pendant, 2))
                        {
                            if ((mode.ItemPlacement.Value == ItemPlacement.Advanced || items.Has(ItemType.Book)) &&
                                (mode.WorldState.Value == WorldState.StandardOpen || items.Has(ItemType.MoonPearl)))
                                return Accessibility.Normal;

                            if (mode.WorldState.Value == WorldState.StandardOpen &&
                                mode.ItemPlacement.Value == ItemPlacement.Basic)
                                return Accessibility.SequenceBreak;
                        }

                        if (items.Has(ItemType.Book))
                            return Accessibility.Inspect;

                        return Accessibility.None;
                    };

                    itemRequirements.Add(game.Items[ItemType.GreenPendant]);
                    itemRequirements.Add(game.Items[ItemType.Pendant]);
                    itemRequirements.Add(game.Items[ItemType.Book]);
                    itemRequirements.Add(game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.LumberjackCave:

                    _baseTotal = 1;
                    Name = "Cave";
                    HasVisibleItem = true;

                    GetAccessibility = (mode, items) =>
                    {
                        if (items.Has(ItemType.Aga) && items.Has(ItemType.Boots) &&
                            (mode.WorldState == WorldState.StandardOpen || items.Has(ItemType.MoonPearl)))
                            return Accessibility.Normal;

                        return Accessibility.Inspect;
                    };

                    itemRequirements.Add(game.Items[ItemType.Aga]);
                    itemRequirements.Add(game.Items[ItemType.Boots]);
                    itemRequirements.Add(game.Items[ItemType.MoonPearl]);

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

            Total = _baseTotal;
            Available = _baseTotal;

            foreach (Item item in itemRequirements)
                item.PropertyChanged += OnItemRequirementChanged;
        }

        void SetTotal(Mode mode)
        {
            int newTotal = _baseTotal + ((mode.DungeonItemShuffle.Value >= DungeonItemShuffle.MapsCompasses && _mapCompass) ? 2 : 0) +
                ((mode.DungeonItemShuffle.Value >= DungeonItemShuffle.MapsCompassesSmallKeys) ? _smallKeys : 0) +
                ((mode.DungeonItemShuffle.Value == DungeonItemShuffle.Keysanity && _bigKey) ? 1 : 0);

            int delta = newTotal - Total;

            Total = newTotal;
            Available = Math.Max(0, Math.Min(Total, Available + delta));
        }
        
        private void OnItemRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            if (ItemRequirementChanged != null)
                ItemRequirementChanged.Invoke(this, new EventArgs());
        }

        private void OnGameModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DungeonItemShuffle")
                SetTotal((Mode)sender);
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Clear()
        {
            Available = 0;

            if (VisibleItem != null)
            {
                VisibleItem.Current = Math.Min(VisibleItem.Maximum, VisibleItem.Current + 1);
                VisibleItem = null;
            }
        }

        public bool IsAvailable()
        {
            return Available > 0;
        }
    }
}
