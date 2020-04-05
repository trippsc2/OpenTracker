using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models
{
    public class EntranceSection : ISection
    {
        private readonly Game _game;
        private readonly Item _standardItemProvided;
        private readonly Item _invertedItemProvided;
        private readonly Dictionary<RegionID, Mode> _regionSubscriptions;
        private readonly Dictionary<RegionID, bool> _regionIsSubscribed;
        private readonly Dictionary<ItemType, Mode> _itemSubscriptions;
        private readonly Dictionary<ItemType, bool> _itemIsSubscribed;

        public string Name { get; }
        public bool HasMarking { get => true; }
        public Mode RequiredMode { get; }

        public Func<AccessibilityLevel> GetAccessibility { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private AccessibilityLevel _accessibility;
        public AccessibilityLevel Accessibility
        {
            get => _accessibility;
            private set
            {
                if (_accessibility != value)
                {
                    _accessibility = value;
                    OnPropertyChanged(nameof(Accessibility));
                }
            }
        }

        private bool _available;
        public bool Available
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

        private MarkingType? _marking;
        public MarkingType? Marking
        {
            get => _marking;
            set
            {
                if (_marking != value)
                {
                    _marking = value;
                    OnPropertyChanged(nameof(Marking));
                }
            }
        }

        public EntranceSection(Game game, LocationID iD)
        {
            _game = game;

            _regionSubscriptions = new Dictionary<RegionID, Mode>();
            _regionIsSubscribed = new Dictionary<RegionID, bool>();
            _itemSubscriptions = new Dictionary<ItemType, Mode>();
            _itemIsSubscribed = new Dictionary<ItemType, bool>();

            RequiredMode = new Mode();
            Available = true;

            List<Item> itemReqs = new List<Item>();

            switch (iD)
            {
                case LocationID.LumberjackHouse:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
                case LocationID.LumberjackCaveEntrance:

                    Name = "Dropdown";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Aga) && _game.Items.Has(ItemType.Boots))
                                return _game.Regions[RegionID.LightWorld].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Aga) && _game.Items.Has(ItemType.Boots) &&
                                _game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.LightWorld].Accessibility;
                        }

                        return AccessibilityLevel.Inspect;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.Aga, new Mode());
                    _itemSubscriptions.Add(ItemType.Boots, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.DeathMountainEntryCave:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Gloves))
                                return _game.Regions[RegionID.LightWorld].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.LightWorld].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.Gloves, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.DeathMountainExitCave:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DeathMountainExitAccess];
                    _invertedItemProvided = _game.Items[ItemType.DeathMountainExitAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.BumperCaveAccess) && _game.Items.Has(ItemType.Mirror))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    _itemSubscriptions.Add(ItemType.BumperCaveAccess, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.KakarikoFortuneTeller:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
                case LocationID.WomanLeftDoor:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
                case LocationID.WomanRightDoor:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
                case LocationID.LeftSnitchHouse:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
                case LocationID.RightSnitchHouse:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
                case LocationID.BlindsHouseEntrance:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
                case LocationID.TheWellEntrance:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
                case LocationID.ChickenHouseEntrance:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
                case LocationID.GrassHouse:

                    Name = "Move your lawn";
                    _standardItemProvided = _game.Items[ItemType.GrassHouseAccess];
                    _invertedItemProvided = _game.Items[ItemType.GrassHouseAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                            return _game.Regions[RegionID.LightWorld].Accessibility;

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.LightWorld].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.TavernFront:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
                case LocationID.KakarikoShop:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
                case LocationID.BombHut:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                            return _game.Regions[RegionID.LightWorld].Accessibility;

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.LightWorld].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.SickKidEntrance:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
                case LocationID.BlacksmithHouse:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
                case LocationID.MagicBatEntrance:

                    Name = "Dropdown";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            AccessibilityLevel direct = AccessibilityLevel.None;
                            AccessibilityLevel mirror = AccessibilityLevel.None;

                            if (_game.Items.Has(ItemType.Hammer))
                                direct = _game.Regions[RegionID.LightWorld].Accessibility;
                            else
                            {
                                direct = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Inspect,
                                    (byte)_game.Regions[RegionID.LightWorld].Accessibility);
                            }

                            if (_game.Items.Has(ItemType.Mirror) && _game.Items.Has(ItemType.Gloves, 2) &&
                                _game.Items.Has(ItemType.MoonPearl))
                                mirror = AccessibilityLevel.Normal;

                            return (AccessibilityLevel)Math.Max((byte)direct, (byte)mirror);
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                            {
                                if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.MoonPearl))
                                    return _game.Regions[RegionID.LightWorld].Accessibility;

                                return AccessibilityLevel.Inspect;
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Gloves, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode());

                    break;
                case LocationID.ChestGame:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
                case LocationID.RaceHouseLeft:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.RaceGameAccess];
                    _invertedItemProvided = _game.Items[ItemType.RaceGameAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Mirror))
                                return _game.Regions[RegionID.DarkWorldSouth].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DarkWorldSouth, new Mode() { WorldState = WorldState.StandardOpen });

                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.RaceHouseRight:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
                case LocationID.LibraryEntrance:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
                case LocationID.ForestHideout:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                            return _game.Regions[RegionID.LightWorld].Accessibility;

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.LightWorld].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.ForestChestGame:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
                case LocationID.CastleSecretEntrance:

                    Name = "Dropdown";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                            return _game.Regions[RegionID.LightWorld].Accessibility;

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                            {
                                if (_game.Items.Has(ItemType.MoonPearl))
                                    return _game.Regions[RegionID.LightWorld].Accessibility;

                                return AccessibilityLevel.Inspect;
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.CastleMainEntrance:

                    Name = "Dungeon";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
                case LocationID.CastleLeftEntrance:

                    Name = "Dungeon";
                    _standardItemProvided = _game.Items[ItemType.HyruleCastleSecondFloorAccess];
                    _invertedItemProvided = _game.Items[ItemType.HyruleCastleSecondFloorAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.HyruleCastleSecondFloor].Accessibility; };

                    _regionSubscriptions.Add(RegionID.HyruleCastleSecondFloor, new Mode());

                    break;
                case LocationID.CastleRightEntrance:

                    Name = "Dungeon";
                    _standardItemProvided = _game.Items[ItemType.HyruleCastleSecondFloorAccess];
                    _invertedItemProvided = _game.Items[ItemType.HyruleCastleSecondFloorAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.HyruleCastleSecondFloor].Accessibility; };

                    _regionSubscriptions.Add(RegionID.HyruleCastleSecondFloor, new Mode());

                    break;
                case LocationID.CastleTowerEntrance:

                    Name = "Dungeon";
                    _standardItemProvided = _game.Items[ItemType.HyruleCastleSecondFloorAccess];
                    _invertedItemProvided = _game.Items[ItemType.HyruleCastleSecondFloorAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.CanClearAgaTowerBarrier())
                                return _game.Regions[RegionID.HyruleCastleSecondFloor].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.TowerCrystals))
                                return _game.Regions[RegionID.HyruleCastleSecondFloor].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.HyruleCastleSecondFloor, new Mode());

                    _itemSubscriptions.Add(ItemType.Cape, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Sword, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.TowerCrystals, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.Crystal, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.RedCrystal, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.DamEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
                case LocationID.CentralBonkRocks:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Boots))
                                return AccessibilityLevel.Normal;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Boots) && _game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.Boots, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.WitchsHutEntrance:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.WitchsHutAccess];
                    _invertedItemProvided = _game.Items[ItemType.WitchsHutAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                            return _game.Regions[RegionID.LightWorld].Accessibility;

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.LightWorld].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.WaterfallFairyEntrance:

                    Name = "Waterfall Cave";
                    _standardItemProvided = _game.Items[ItemType.WaterfallFairyAccess];
                    _invertedItemProvided = _game.Items[ItemType.WaterfallFairyAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                            {
                                if (_game.Items.Has(ItemType.Flippers))
                                    return _game.Regions[RegionID.LightWorld].Accessibility;

                                return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                            {
                                if (_game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                                {
                                    if (_game.Items.Has(ItemType.Flippers))
                                        return _game.Regions[RegionID.LightWorld].Accessibility;

                                    return AccessibilityLevel.SequenceBreak;
                                }
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.Flippers, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.SahasrahlasHutEntrance:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
                case LocationID.TreesFairyCave:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
                case LocationID.PegsFairyCave:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
                case LocationID.EasternPalaceEntrance:

                    Name = "Dungeon";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
                case LocationID.HoulihanHole:

                    Name = "Dropdown";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                            return _game.Regions[RegionID.LightWorld].Accessibility;

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                            {
                                if (_game.Items.Has(ItemType.MoonPearl))
                                    return _game.Regions[RegionID.LightWorld].Accessibility;

                                return AccessibilityLevel.Inspect;
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.SanctuaryGrave:

                    Name = "Dropdown";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                            {
                                if (_game.Items.Has(ItemType.Gloves))
                                    return _game.Regions[RegionID.LightWorld].Accessibility;

                                return AccessibilityLevel.Inspect;
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                            {
                                if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Gloves))
                                    return _game.Regions[RegionID.LightWorld].Accessibility;

                                return AccessibilityLevel.Inspect;
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.Gloves, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.NorthBonkRocks:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Boots))
                                return _game.Regions[RegionID.LightWorld].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Boots) && _game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.LightWorld].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.Boots, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.KingsTombEntrance:

                    Name = "The Crypt";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Boots))
                            {
                                AccessibilityLevel lightWorld = AccessibilityLevel.None;
                                AccessibilityLevel dWWest = AccessibilityLevel.None;

                                if (_game.Items.Has(ItemType.Gloves, 2))
                                    lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                                if (_game.Items.Has(ItemType.Mirror) && _game.Items.Has(ItemType.MoonPearl))
                                    dWWest = _game.Regions[RegionID.DarkWorldWest].Accessibility;

                                return (AccessibilityLevel)Math.Max((byte)lightWorld, (byte)dWWest);
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Gloves, 2) && _game.Items.Has(ItemType.Boots) &&
                                _game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.LightWorld].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());
                    _regionSubscriptions.Add(RegionID.DarkWorldWest, new Mode() { WorldState = WorldState.StandardOpen });

                    _itemSubscriptions.Add(ItemType.Boots, new Mode());
                    _itemSubscriptions.Add(ItemType.Gloves, new Mode());
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode());

                    break;
                case LocationID.GraveyardLedgeEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Mirror))
                                return _game.Regions[RegionID.DarkWorldWest].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DarkWorldWest, new Mode() { WorldState = WorldState.StandardOpen });
                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode() { WorldState = WorldState.Inverted });

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode());
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.DesertLeftEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DesertLeftAccess];
                    _invertedItemProvided = _game.Items[ItemType.DesertLeftAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.DesertBackAccess) && _game.Items.Has(ItemType.Gloves))
                                return AccessibilityLevel.Normal;

                            if (_game.Items.Has(ItemType.Mirror))
                                return _game.Regions[RegionID.MireArea].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.DesertBackAccess) && _game.Items.Has(ItemType.Gloves) &&
                                _game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.MireArea, new Mode() { WorldState = WorldState.StandardOpen });

                    _itemSubscriptions.Add(ItemType.DesertBackAccess, new Mode());
                    _itemSubscriptions.Add(ItemType.Gloves, new Mode());
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.DesertBackEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DesertBackAccess];
                    _invertedItemProvided = _game.Items[ItemType.DesertBackAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.DesertLeftAccess) && _game.Items.Has(ItemType.Gloves))
                                return AccessibilityLevel.Normal;

                            if (_game.Items.Has(ItemType.Mirror))
                                return _game.Regions[RegionID.MireArea].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.DesertLeftAccess) && _game.Items.Has(ItemType.Gloves) &&
                                _game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.MireArea, new Mode() { WorldState = WorldState.StandardOpen });

                    _itemSubscriptions.Add(ItemType.DesertLeftAccess, new Mode());
                    _itemSubscriptions.Add(ItemType.Gloves, new Mode());
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.DesertRightEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Mirror))
                                return _game.Regions[RegionID.MireArea].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.MireArea, new Mode() { WorldState = WorldState.StandardOpen });

                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.DesertFrontEntrance:

                    Name = "Cave";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            AccessibilityLevel direct = AccessibilityLevel.None;
                            AccessibilityLevel mirror = AccessibilityLevel.None;

                            if (_game.Items.Has(ItemType.Book))
                                direct = _game.Regions[RegionID.LightWorld].Accessibility;

                            if (_game.Items.Has(ItemType.Mirror))
                                mirror = _game.Regions[RegionID.MireArea].Accessibility;

                            return (AccessibilityLevel)Math.Max((byte)direct, (byte)mirror);
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Book))
                                return _game.Regions[RegionID.LightWorld].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());
                    _regionSubscriptions.Add(RegionID.MireArea, new Mode() { WorldState = WorldState.StandardOpen });

                    _itemSubscriptions.Add(ItemType.Book, new Mode());
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.AginahsCaveEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
                case LocationID.ThiefCave:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
                case LocationID.RupeeCave:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Gloves))
                                return _game.Regions[RegionID.LightWorld].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.LightWorld].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.Gloves, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.SkullWoodsBack:

                    Name = "Dungeon";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldWestAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldWestAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.FireRod) && _game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.DarkWorldWest].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.FireRod))
                                return _game.Regions[RegionID.DarkWorldWest].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DarkWorldWest, new Mode());

                    _itemSubscriptions.Add(ItemType.FireRod, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.ThievesTownEntrance:

                    Name = "Dungeon";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldWestAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldWestAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.DarkWorldWest].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                            return _game.Regions[RegionID.DarkWorldWest].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DarkWorldWest, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.CShapedHouseEntrance:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldWestAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldWestAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.DarkWorldWest].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DarkWorldWest, new Mode());

                    break;
                case LocationID.HammerHouse:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.HammerHouseAccess];
                    _invertedItemProvided = _game.Items[ItemType.HammerHouseAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.DarkWorldWest].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            AccessibilityLevel direct = AccessibilityLevel.None;
                            AccessibilityLevel mirror = AccessibilityLevel.None;

                            if (_game.Items.Has(ItemType.Hammer))
                                direct = _game.Regions[RegionID.DarkWorldWest].Accessibility;

                            if (_game.Items.Has(ItemType.Mirror) && _game.Items.Has(ItemType.MoonPearl))
                                mirror = _game.Regions[RegionID.LightWorld].Accessibility;

                            return (AccessibilityLevel)Math.Max((byte)direct, (byte)mirror);
                        }    

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DarkWorldWest, new Mode());
                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode() { WorldState = WorldState.Inverted });

                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode());
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.DarkVillageFortuneTeller:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldWestAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldWestAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.DarkWorldWest].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DarkWorldWest, new Mode());

                    break;
                case LocationID.DarkChapel:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldWestAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldWestAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.DarkWorldWest].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DarkWorldWest, new Mode());

                    break;
                case LocationID.ShieldShop:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldWestAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldWestAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.DarkWorldWest].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DarkWorldWest, new Mode());

                    break;
                case LocationID.DarkLumberjack:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldWestAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldWestAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.DarkWorldWest].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DarkWorldWest, new Mode());

                    break;
                case LocationID.TreasureGameEntrance:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldWestAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldWestAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.DarkWorldWest].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DarkWorldWest, new Mode());

                    break;
                case LocationID.BombableShackEntrance:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldWestAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldWestAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.DarkWorldWest].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                            return _game.Regions[RegionID.DarkWorldWest].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DarkWorldWest, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.HammerPegsEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldWestAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldWestAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Gloves, 2) && _game.Items.Has(ItemType.Hammer) &&
                                _game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.DarkWorldWest].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Hammer))
                            {
                                if (_game.Items.Has(ItemType.Gloves, 2))
                                    return _game.Regions[RegionID.DarkWorldWest].Accessibility;

                                if (_game.Items.Has(ItemType.Mirror))
                                    return _game.Regions[RegionID.LightWorld].Accessibility;
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DarkWorldWest, new Mode());
                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode() { WorldState = WorldState.Inverted });

                    _itemSubscriptions.Add(ItemType.Gloves, new Mode());
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.BumperCaveExit:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.BumperCaveAccess];
                    _invertedItemProvided = _game.Items[ItemType.BumperCaveAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.DeathMountainExitAccess) &&
                                _game.Items.Has(ItemType.Mirror))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    _itemSubscriptions.Add(ItemType.DeathMountainExitAccess, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.BumperCaveEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldWestAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldWestAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.DarkWorldWest].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Gloves))
                                return _game.Regions[RegionID.DarkWorldWest].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DarkWorldWest, new Mode());

                    _itemSubscriptions.Add(ItemType.Gloves, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.HypeCaveEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldSouthAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldSouthAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.DarkWorldSouth].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                            return _game.Regions[RegionID.DarkWorldSouth].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DarkWorldSouth, new Mode());

                    _itemSubscriptions.Add(ItemType.Boots, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.SwampPalaceEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldSouthAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldSouthAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.DarkWorldSouth].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DarkWorldSouth, new Mode());

                    break;
                case LocationID.DarkCentralBonkRocks:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldSouthAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldSouthAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Boots) && _game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.DarkWorldSouth].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Boots))
                                return _game.Regions[RegionID.DarkWorldSouth].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DarkWorldSouth, new Mode());

                    _itemSubscriptions.Add(ItemType.Boots, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.SouthOfGroveEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Mirror))
                                return _game.Regions[RegionID.DarkWorldSouth].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                            return _game.Regions[RegionID.LightWorld].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DarkWorldSouth, new Mode() { WorldState = WorldState.StandardOpen });
                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode() { WorldState = WorldState.Inverted });

                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.BombShop:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldSouthAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldSouthAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.DarkWorldSouth].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DarkWorldSouth, new Mode());

                    break;
                case LocationID.ArrowGame:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldSouthAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldSouthAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.DarkWorldSouth].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DarkWorldSouth, new Mode());

                    break;
                case LocationID.DarkHyliaFortuneTeller:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldSouthAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldSouthAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.DarkWorldSouth].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DarkWorldSouth, new Mode());

                    break;
                case LocationID.DarkTreesFairyCave:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldEastAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldEastAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.DarkWorldEast].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DarkWorldEast, new Mode());

                    break;
                case LocationID.DarkSahasrahla:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldEastAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldEastAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.DarkWorldEast].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DarkWorldEast, new Mode());

                    break;
                case LocationID.PalaceOfDarknessEntrance:

                    Name = "Dungeon";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldEastAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldEastAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.DarkWorldEast].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                            return _game.Regions[RegionID.DarkWorldEast].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DarkWorldEast, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.DarkWitchsHut:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldWitchAreaAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldWitchAreaAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.DarkWorldWitchArea].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DarkWorldWitchArea, new Mode());

                    break;
                case LocationID.DarkFluteSpotFive:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldEastAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldEastAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.DarkWorldEast].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DarkWorldEast, new Mode());

                    break;
                case LocationID.FatFairyEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldEastAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldEastAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.RedCrystal, 2))
                                return AccessibilityLevel.Normal;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.RedCrystal, 2))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DarkWorldEast, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.RedCrystal, new Mode());

                    break;
                case LocationID.GanonHole:

                    Name = "Dropdown";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldEastAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Aga2))
                                return _game.Regions[RegionID.DarkWorldEast].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Regions[RegionID.HyruleCastleSecondFloor].Accessibility >= AccessibilityLevel.SequenceBreak &&
                                _game.Items.Has(ItemType.Aga2))
                                return _game.Regions[RegionID.HyruleCastleSecondFloor].Accessibility;

                            if (_game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                                return AccessibilityLevel.Inspect;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DarkWorldEast, new Mode() { WorldState = WorldState.StandardOpen });
                    _regionSubscriptions.Add(RegionID.HyruleCastleSecondFloor, new Mode() { WorldState = WorldState.Inverted });
                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode() { WorldState = WorldState.Inverted });

                    _itemSubscriptions.Add(ItemType.Aga2, new Mode());

                    break;
                case LocationID.DarkIceRodCave:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldSouthEastAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldSouthEastAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.DarkWorldSouthEast].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                            return _game.Regions[RegionID.DarkWorldSouthEast].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DarkWorldSouthEast, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.DarkFakeIceRodCave:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldSouthEastAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldSouthEastAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.DarkWorldSouthEast].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DarkWorldSouthEast, new Mode());

                    break;
                case LocationID.DarkIceRodRock:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldSouthEastAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldSouthEastAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Gloves))
                                return _game.Regions[RegionID.DarkWorldSouthEast].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Gloves))
                                return _game.Regions[RegionID.DarkWorldSouthEast].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DarkWorldSouthEast, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Gloves, new Mode());

                    break;
                case LocationID.HypeFairyCave:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                            return _game.Regions[RegionID.LightWorld].Accessibility;

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.LightWorld].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.FortuneTeller:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
                case LocationID.LakeShop:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
                case LocationID.UpgradeFairy:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LakeHyliaFairyIslandAccess];
                    _invertedItemProvided = _game.Items[ItemType.LakeHyliaFairyIslandAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Regions[RegionID.LightWorld].Accessibility == AccessibilityLevel.Normal &&
                                _game.Items.Has(ItemType.Flippers))
                                return AccessibilityLevel.Normal;

                            if (_game.Items.Has(ItemType.IcePalaceAccess) && _game.Items.Has(ItemType.Mirror))
                                return AccessibilityLevel.Normal;

                            if (_game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return AccessibilityLevel.SequenceBreak;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if ((_game.Regions[RegionID.DarkWorldSouth].Accessibility == AccessibilityLevel.Normal ||
                                _game.Regions[RegionID.DarkWorldSouthEast].Accessibility == AccessibilityLevel.Normal) &&
                                _game.Items.Has(ItemType.Gloves, 2) && _game.Items.Has(ItemType.Flippers))
                                return AccessibilityLevel.Normal;

                            if (_game.Items.Has(ItemType.MoonPearl))
                            {
                                if (_game.Regions[RegionID.LightWorld].Accessibility == AccessibilityLevel.Normal &&
                                    _game.Items.Has(ItemType.Flippers))
                                    return AccessibilityLevel.Normal;

                                if (_game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                                    return AccessibilityLevel.SequenceBreak;
                            }

                            if ((_game.Regions[RegionID.DarkWorldSouth].Accessibility >= AccessibilityLevel.SequenceBreak ||
                                _game.Regions[RegionID.DarkWorldSouthEast].Accessibility == AccessibilityLevel.SequenceBreak) &&
                                _game.Items.Has(ItemType.Gloves, 2))
                                return AccessibilityLevel.SequenceBreak;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());
                    _regionSubscriptions.Add(RegionID.DarkWorldSouth, new Mode() { WorldState = WorldState.Inverted });
                    _regionSubscriptions.Add(RegionID.DarkWorldSouthEast, new Mode() { WorldState = WorldState.Inverted });

                    _itemSubscriptions.Add(ItemType.Flippers, new Mode());
                    _itemSubscriptions.Add(ItemType.IcePalaceAccess, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Gloves, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.MiniMoldormCaveEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                            return _game.Regions[RegionID.LightWorld].Accessibility;

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.LightWorld].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.IceRodCaveEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                            return _game.Regions[RegionID.LightWorld].Accessibility;

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.LightWorld].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.IceBeeCave:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
                case LocationID.IceFairyCave:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Gloves))
                                return _game.Regions[RegionID.LightWorld].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Gloves))
                                return _game.Regions[RegionID.LightWorld].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.Gloves, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.IcePalaceEntrance:

                    Name = "Dungeon";
                    _standardItemProvided = _game.Items[ItemType.IcePalaceAccess];
                    _invertedItemProvided = _game.Items[ItemType.IcePalaceAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Gloves, 2))
                            {
                                if (_game.Items.Has(ItemType.LakeHyliaFairyIslandAccess))
                                    return AccessibilityLevel.Normal;

                                if (_game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                                {
                                    if (_game.Items.Has(ItemType.Flippers))
                                        return _game.Regions[RegionID.LightWorld].Accessibility;

                                    return AccessibilityLevel.SequenceBreak;
                                }
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            AccessibilityLevel dWSouth = AccessibilityLevel.None;
                            AccessibilityLevel dWSouthEast = AccessibilityLevel.None;
                            AccessibilityLevel lightWorld = AccessibilityLevel.None;

                            if (_game.Items.Has(ItemType.Flippers))
                            {
                                dWSouth = _game.Regions[RegionID.DarkWorldSouth].Accessibility;
                                dWSouthEast = _game.Regions[RegionID.DarkWorldSouthEast].Accessibility;
                            }
                            else
                            {
                                dWSouth = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                    (byte)_game.Regions[RegionID.DarkWorldSouth].Accessibility);
                                dWSouthEast = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                    (byte)_game.Regions[RegionID.DarkWorldSouthEast].Accessibility);
                            }

                            if (_game.Items.Has(ItemType.Mirror))
                            {
                                if (_game.Items.Has(ItemType.LakeHyliaFairyIslandAccess) ||
                                    (_game.Items.Has(ItemType.Flippers) && _game.Items.Has(ItemType.MoonPearl)))
                                    lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;
                                else if (_game.Items.Has(ItemType.MoonPearl))
                                {
                                    lightWorld = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                        (byte)_game.Regions[RegionID.LightWorld].Accessibility);
                                }
                            }

                            if (_game.Regions[RegionID.DarkWorldSouth].Accessibility >= AccessibilityLevel.SequenceBreak ||
                                _game.Regions[RegionID.DarkWorldSouthEast].Accessibility >= AccessibilityLevel.SequenceBreak)
                                return AccessibilityLevel.SequenceBreak;

                            return (AccessibilityLevel)Math.Max(Math.Max((byte)dWSouth, (byte)dWSouthEast), (byte)lightWorld);
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());
                    _regionSubscriptions.Add(RegionID.DarkWorldSouth, new Mode() { WorldState = WorldState.Inverted });
                    _regionSubscriptions.Add(RegionID.DarkWorldSouthEast, new Mode() { WorldState = WorldState.Inverted });

                    _itemSubscriptions.Add(ItemType.Gloves, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.LakeHyliaFairyIslandAccess, new Mode());
                    _itemSubscriptions.Add(ItemType.Flippers, new Mode());
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.MiseryMireEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.MireAreaAccess];
                    _invertedItemProvided = _game.Items[ItemType.MireAreaAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.CanUseMedallions() &&
                                ((_game.Items.Has(ItemType.Bombos) && _game.Items.Has(ItemType.Ether) &&
                                _game.Items.Has(ItemType.Quake)) ||
                                (_game.Items.Has(ItemType.Bombos) &&
                                (_game.Items[ItemType.BombosDungeons].Current == 1 ||
                                _game.Items[ItemType.BombosDungeons].Current == 3)) ||
                                (_game.Items.Has(ItemType.Ether) &&
                                (_game.Items[ItemType.EtherDungeons].Current == 1 ||
                                _game.Items[ItemType.EtherDungeons].Current == 3)) ||
                                (_game.Items.Has(ItemType.Quake) &&
                                (_game.Items[ItemType.QuakeDungeons].Current == 1 ||
                                _game.Items[ItemType.QuakeDungeons].Current == 3))))
                                return _game.Regions[RegionID.MireArea].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.CanUseMedallions() &&
                                ((_game.Items.Has(ItemType.Bombos) && _game.Items.Has(ItemType.Ether) &&
                                _game.Items.Has(ItemType.Quake)) ||
                                (_game.Items.Has(ItemType.Bombos) &&
                                (_game.Items[ItemType.BombosDungeons].Current == 1 ||
                                _game.Items[ItemType.BombosDungeons].Current == 3)) ||
                                (_game.Items.Has(ItemType.Ether) &&
                                (_game.Items[ItemType.EtherDungeons].Current == 1 ||
                                _game.Items[ItemType.EtherDungeons].Current == 3)) ||
                                (_game.Items.Has(ItemType.Quake) &&
                                (_game.Items[ItemType.QuakeDungeons].Current == 1 ||
                                _game.Items[ItemType.QuakeDungeons].Current == 3))))
                                return _game.Regions[RegionID.MireArea].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.MireArea, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Sword, new Mode());
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.Bombos, new Mode());
                    _itemSubscriptions.Add(ItemType.BombosDungeons, new Mode());
                    _itemSubscriptions.Add(ItemType.Ether, new Mode());
                    _itemSubscriptions.Add(ItemType.EtherDungeons, new Mode());
                    _itemSubscriptions.Add(ItemType.Quake, new Mode());
                    _itemSubscriptions.Add(ItemType.QuakeDungeons, new Mode());

                    break;
                case LocationID.MireShackEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.MireAreaAccess];
                    _invertedItemProvided = _game.Items[ItemType.MireAreaAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.MireArea].Accessibility; };

                    _regionSubscriptions.Add(RegionID.MireArea, new Mode());

                    break;
                case LocationID.MireRightShack:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.MireAreaAccess];
                    _invertedItemProvided = _game.Items[ItemType.MireAreaAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.MireArea].Accessibility; };

                    _regionSubscriptions.Add(RegionID.MireArea, new Mode());

                    break;
                case LocationID.MireCave:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.MireAreaAccess];
                    _invertedItemProvided = _game.Items[ItemType.MireAreaAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.MireArea].Accessibility; };

                    _regionSubscriptions.Add(RegionID.MireArea, new Mode());

                    break;
                case LocationID.CheckerboardCaveEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.CheckerboardCaveAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Mirror) && _game.Items.Has(ItemType.Gloves))
                                return _game.Regions[RegionID.MireArea].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Gloves))
                                return _game.Regions[RegionID.LightWorld].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.MireArea, new Mode() { WorldState = WorldState.StandardOpen });
                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode() { WorldState = WorldState.Inverted });

                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Gloves, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.DeathMountainEntranceBack:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DeathMountainWestBottomAccess];
                    _invertedItemProvided = _game.Items[ItemType.DeathMountainWestBottomAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.DeathMountainWestBottom].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DeathMountainWestBottom, new Mode());

                    break;
                case LocationID.OldManResidence:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DeathMountainWestBottomAccess];
                    _invertedItemProvided = _game.Items[ItemType.DeathMountainWestBottomAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.DeathMountainWestBottom].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DeathMountainWestBottom, new Mode());

                    break;
                case LocationID.OldManBackResidence:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DeathMountainWestBottomAccess];
                    _invertedItemProvided = _game.Items[ItemType.DeathMountainWestBottomAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.DeathMountainWestBottom].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DeathMountainWestBottom, new Mode());

                    break;
                case LocationID.DeathMountainExitFront:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DeathMountainWestBottomAccess];
                    _invertedItemProvided = _game.Items[ItemType.DeathMountainWestBottomAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.DeathMountainWestBottom].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DeathMountainWestBottom, new Mode());

                    break;
                case LocationID.SpectacleRockLeft:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DeathMountainWestBottomAccess];
                    _invertedItemProvided = _game.Items[ItemType.DeathMountainWestBottomAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.DeathMountainWestBottom].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DeathMountainWestBottom, new Mode());

                    break;
                case LocationID.SpectacleRockRight:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DeathMountainWestBottomAccess];
                    _invertedItemProvided = _game.Items[ItemType.DeathMountainWestBottomAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.DeathMountainWestBottom].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DeathMountainWestBottom, new Mode());

                    break;
                case LocationID.SpectacleRockTop:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DeathMountainWestBottomAccess];
                    _invertedItemProvided = _game.Items[ItemType.DeathMountainWestBottomAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.DeathMountainWestBottom].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DeathMountainWestBottom, new Mode());

                    break;
                case LocationID.SpikeCaveEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkDeathMountainWestBottomAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkDeathMountainWestBottomAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.DarkDeathMountainWestBottom].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DarkDeathMountainWestBottom, new Mode());

                    break;
                case LocationID.DarkMountainFairy:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkDeathMountainWestBottomAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkDeathMountainWestBottomAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.DarkDeathMountainWestBottom].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DarkDeathMountainWestBottom, new Mode());

                    break;
                case LocationID.TowerOfHeraEntrance:

                    Name = "Dungeon";
                    _standardItemProvided = _game.Items[ItemType.DeathMountainWestTopAccess];
                    _invertedItemProvided = _game.Items[ItemType.DeathMountainWestTopAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.DeathMountainWestTop].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DeathMountainWestTop, new Mode());

                    break;
                case LocationID.SpiralCaveBottom:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DeathMountainEastBottomAccess];
                    _invertedItemProvided = _game.Items[ItemType.DeathMountainEastBottomAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.DeathMountainEastBottom].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DeathMountainEastBottom, new Mode());

                    break;
                case LocationID.EDMFairyCave:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DeathMountainEastBottomAccess];
                    _invertedItemProvided = _game.Items[ItemType.DeathMountainEastBottomAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.DeathMountainEastBottom].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DeathMountainEastBottom, new Mode());

                    break;
                case LocationID.ParadoxCaveMiddle:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DeathMountainEastBottomAccess];
                    _invertedItemProvided = _game.Items[ItemType.DeathMountainEastBottomAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.DeathMountainEastBottom].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DeathMountainEastBottom, new Mode());

                    break;
                case LocationID.ParadoxCaveBottom:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DeathMountainEastBottomAccess];
                    _invertedItemProvided = _game.Items[ItemType.DeathMountainEastBottomAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.DeathMountainEastBottom].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DeathMountainEastBottom, new Mode());

                    break;
                case LocationID.EDMConnectorBottom:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DeathMountainEastBottomAccess];
                    _invertedItemProvided = _game.Items[ItemType.DeathMountainEastBottomAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            AccessibilityLevel dMEastTop = _game.Regions[RegionID.DeathMountainEastTop].Accessibility;
                            AccessibilityLevel dMEastBottom = AccessibilityLevel.None;
                            AccessibilityLevel dDMEastBottom = AccessibilityLevel.None;

                            if (_game.Items.Has(ItemType.DeathMountainEastTopConnectorAccess) ||
                                _game.Items.Has(ItemType.Gloves, 2))
                                dMEastBottom = _game.Regions[RegionID.DeathMountainEastBottom].Accessibility;

                            if (_game.Items.Has(ItemType.Mirror))
                            {
                                if (_game.Items.Has(ItemType.TurtleRockSafetyDoorAccess))
                                    return AccessibilityLevel.Normal;

                                if (_game.Items.Has(ItemType.MoonPearl))
                                    dDMEastBottom = _game.Regions[RegionID.DarkDeathMountainEastBottom].Accessibility;
                            }

                            return (AccessibilityLevel)Math.Max(Math.Max((byte)dMEastTop, (byte)dMEastBottom),
                                (byte)dDMEastBottom);
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.DeathMountainEastTopConnectorAccess))
                                return AccessibilityLevel.Normal;

                            AccessibilityLevel dMEastTop = _game.Regions[RegionID.DeathMountainEastTop].Accessibility;
                            AccessibilityLevel dMEastBottom = AccessibilityLevel.None;

                            if (_game.Items.Has(ItemType.Gloves, 2) && _game.Items.Has(ItemType.MoonPearl))
                                dMEastBottom = _game.Regions[RegionID.DeathMountainEastBottom].Accessibility;

                            return (AccessibilityLevel)Math.Max((byte)dMEastTop, (byte)dMEastBottom);
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DeathMountainEastTop, new Mode());
                    _regionSubscriptions.Add(RegionID.DeathMountainEastBottom, new Mode());
                    _regionSubscriptions.Add(RegionID.DarkDeathMountainEastBottom, new Mode() { WorldState = WorldState.StandardOpen });

                    _itemSubscriptions.Add(ItemType.DeathMountainEastTopConnectorAccess, new Mode());
                    _itemSubscriptions.Add(ItemType.Gloves, new Mode());
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.TurtleRockSafetyDoorAccess, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode());

                    _game.Regions[RegionID.DeathMountainEastTop].PropertyChanged += OnRequirementChanged;
                    _game.Regions[RegionID.DarkDeathMountainEastBottom].PropertyChanged += OnRequirementChanged;

                    break;
                case LocationID.SpiralCaveTop:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.SpiralCaveTopAccess];
                    _invertedItemProvided = _game.Items[ItemType.SpiralCaveTopAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.TurtleRockTunnelAccess) && _game.Items.Has(ItemType.Mirror))
                                return AccessibilityLevel.Normal;

                            return _game.Regions[RegionID.DeathMountainEastTop].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                            return _game.Regions[RegionID.DeathMountainEastTop].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DeathMountainEastTop, new Mode());

                    _itemSubscriptions.Add(ItemType.TurtleRockTunnelAccess, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.MimicCaveEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.MimicCaveAccess];
                    _invertedItemProvided = _game.Items[ItemType.MimicCaveAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Mirror))
                                return _game.Regions[RegionID.TurtleRockTunnel].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                            return _game.Regions[RegionID.DeathMountainEastTop].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.TurtleRockTunnel, new Mode() { WorldState = WorldState.StandardOpen });
                    _regionSubscriptions.Add(RegionID.DeathMountainEastTop, new Mode() { WorldState = WorldState.Inverted });

                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.EDMConnectorTop:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DeathMountainEastTopConnectorAccess];
                    _invertedItemProvided = _game.Items[ItemType.DeathMountainEastTopConnectorAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.TurtleRockSafetyDoorAccess) &&
                                _game.Items.Has(ItemType.Mirror))
                                return AccessibilityLevel.Normal;

                            return _game.Regions[RegionID.DeathMountainEastTop].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                            return _game.Regions[RegionID.DeathMountainEastTop].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DeathMountainEastTop, new Mode());

                    _itemSubscriptions.Add(ItemType.TurtleRockSafetyDoorAccess, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.ParadoxCaveTop:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DeathMountainEastTopAccess];
                    _invertedItemProvided = _game.Items[ItemType.DeathMountainEastTopAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.DeathMountainEastTop].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DeathMountainEastTop, new Mode());

                    break;
                case LocationID.SuperBunnyCaveBottom:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkDeathMountainEastBottomAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkDeathMountainEastBottomAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.DeathMountainEastBottom].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DeathMountainEastBottom, new Mode());

                    break;
                case LocationID.DeathMountainShop:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkDeathMountainEastBottomAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkDeathMountainEastBottomAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.DeathMountainEastBottom].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DeathMountainEastBottom, new Mode());

                    break;
                case LocationID.SuperBunnyCaveTop:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkDeathMountainTopAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkDeathMountainTopAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.DarkDeathMountainTop].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DarkDeathMountainTop, new Mode());

                    break;
                case LocationID.HookshotCaveEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkDeathMountainTopAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkDeathMountainTopAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.DarkDeathMountainTop].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Gloves))
                                return _game.Regions[RegionID.DarkDeathMountainTop].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DarkDeathMountainTop, new Mode());

                    _itemSubscriptions.Add(ItemType.Gloves, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.TurtleRockEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkDeathMountainTopAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkDeathMountainTopAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Hammer) &&
                                _game.Items.Has(ItemType.Gloves, 2) && _game.Items.CanUseMedallions() &&
                                ((_game.Items.Has(ItemType.Bombos) && _game.Items.Has(ItemType.Ether) &&
                                _game.Items.Has(ItemType.Quake)) ||
                                (_game.Items.Has(ItemType.Bombos) &&
                                _game.Items[ItemType.BombosDungeons].Current >= 2) ||
                                (_game.Items.Has(ItemType.Ether) &&
                                _game.Items[ItemType.EtherDungeons].Current >= 2) ||
                                (_game.Items.Has(ItemType.Quake) &&
                                _game.Items[ItemType.QuakeDungeons].Current >= 2)))
                                return _game.Regions[RegionID.DeathMountainEastTop].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.CanUseMedallions() && ((_game.Items.Has(ItemType.Bombos) &&
                                _game.Items.Has(ItemType.Ether) && _game.Items.Has(ItemType.Quake)) ||
                                (_game.Items.Has(ItemType.Bombos) &&
                                _game.Items[ItemType.BombosDungeons].Current >= 2) ||
                                (_game.Items.Has(ItemType.Ether) &&
                                _game.Items[ItemType.EtherDungeons].Current >= 2) ||
                                (_game.Items.Has(ItemType.Quake) &&
                                _game.Items[ItemType.QuakeDungeons].Current >= 2)))
                                return _game.Regions[RegionID.DarkDeathMountainTop].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DeathMountainEastTop, new Mode() { WorldState = WorldState.StandardOpen });
                    _regionSubscriptions.Add(RegionID.DarkDeathMountainTop, new Mode() { WorldState = WorldState.Inverted });

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Gloves, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Sword, new Mode());
                    _itemSubscriptions.Add(ItemType.Bombos, new Mode());
                    _itemSubscriptions.Add(ItemType.BombosDungeons, new Mode());
                    _itemSubscriptions.Add(ItemType.Ether, new Mode());
                    _itemSubscriptions.Add(ItemType.EtherDungeons, new Mode());
                    _itemSubscriptions.Add(ItemType.Quake, new Mode());
                    _itemSubscriptions.Add(ItemType.QuakeDungeons, new Mode());

                    break;
                case LocationID.GanonsTowerEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkDeathMountainTopAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkDeathMountainTopAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.TowerCrystals))
                                return _game.Regions[RegionID.DarkDeathMountainTop].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                            return _game.Regions[RegionID.DarkDeathMountainTop].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DarkDeathMountainTop, new Mode());

                    _itemSubscriptions.Add(ItemType.TowerCrystals, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Crystal, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.RedCrystal, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.TRLedgeLeft:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.TurtleRockTunnelAccess];
                    _invertedItemProvided = _game.Items[ItemType.TurtleRockTunnelAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.TurtleRockTunnel].Accessibility; };

                    _regionSubscriptions.Add(RegionID.TurtleRockTunnel, new Mode());

                    break;
                case LocationID.TRLedgeRight:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.TurtleRockTunnelAccess];
                    _invertedItemProvided = _game.Items[ItemType.TurtleRockTunnelAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.TurtleRockTunnel].Accessibility; };

                    _regionSubscriptions.Add(RegionID.TurtleRockTunnel, new Mode());

                    break;
                case LocationID.TRSafetyDoor:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.TurtleRockSafetyDoorAccess];
                    _invertedItemProvided = _game.Items[ItemType.TurtleRockSafetyDoorAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Mirror))
                            {
                                if (_game.Items.Has(ItemType.DeathMountainEastTopConnectorAccess))
                                    return AccessibilityLevel.Normal;

                                return _game.Regions[RegionID.DeathMountainEastTop].Accessibility;
                            }
                        }    

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DeathMountainEastTop, new Mode() { WorldState = WorldState.Inverted });

                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.DeathMountainEastTopConnectorAccess, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.HookshotCaveTop:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkDeathMountainFloatingIslandAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkDeathMountainFloatingIslandAccess];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Mirror))
                                return _game.Regions[RegionID.DarkDeathMountainEastBottom].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DarkDeathMountainEastBottom, new Mode() { WorldState = WorldState.Inverted });

                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.LinksHouseEntrance:

                    Name = "Link's House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
            }

            _game.Mode.PropertyChanged += OnModeChanged;

            UpdateRegionSubscriptions();
            UpdateItemSubscriptions();

            UpdateAccessibility();
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (propertyName == nameof(Available))
            {
                if (_game.Mode.WorldState == WorldState.StandardOpen && _standardItemProvided != null)
                {
                    if (Available)
                        _standardItemProvided.Change(-1, true);
                    else
                        _standardItemProvided.Change(1, true);
                }

                if (_game.Mode.WorldState == WorldState.Inverted && _invertedItemProvided != null)
                {
                    if (Available)
                        _invertedItemProvided.Change(-1, true);
                    else
                        _invertedItemProvided.Change(1, true);
                }
            }
        }

        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_game.Mode.WorldState))
            {
                if (!Available)
                {
                    if (_standardItemProvided != null)
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                            _standardItemProvided.Change(1, true);

                        if (_game.Mode.WorldState == WorldState.Inverted)
                            _standardItemProvided.Change(-1, true);
                    }

                    if (_invertedItemProvided != null)
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                            _invertedItemProvided.Change(-1, true);

                        if (_game.Mode.WorldState == WorldState.Inverted)
                            _invertedItemProvided.Change(1, true);
                    }
                }

                UpdateRegionSubscriptions();
                UpdateItemSubscriptions();

                UpdateAccessibility();
            }

            if (e.PropertyName == nameof(_game.Mode.EntranceShuffle))
                UpdateAccessibility();
        }

        private void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateAccessibility();
        }

        private void UpdateRegionSubscriptions()
        {
            foreach (RegionID region in _regionSubscriptions.Keys)
            {
                if (_game.Mode.Validate(_regionSubscriptions[region]))
                {
                    if (_regionIsSubscribed.ContainsKey(region))
                    {
                        if (!_regionIsSubscribed[region])
                        {
                            _regionIsSubscribed[region] = true;
                            _game.Regions[region].PropertyChanged += OnRequirementChanged;
                        }
                    }
                    else
                    {
                        _regionIsSubscribed.Add(region, true);
                        _game.Regions[region].PropertyChanged += OnRequirementChanged;
                    }
                }
                else
                {
                    if (_regionIsSubscribed.ContainsKey(region))
                    {
                        if (_regionIsSubscribed[region])
                        {
                            _regionIsSubscribed[region] = false;
                            _game.Regions[region].PropertyChanged -= OnRequirementChanged;
                        }
                    }
                    else
                        _regionIsSubscribed.Add(region, false);
                }
            }
        }

        private void UpdateItemSubscriptions()
        {
            foreach (ItemType item in _itemSubscriptions.Keys)
            {
                if (_game.Mode.Validate(_itemSubscriptions[item]))
                {
                    if (_itemIsSubscribed.ContainsKey(item))
                    {
                        if (!_itemIsSubscribed[item])
                        {
                            _itemIsSubscribed[item] = true;
                            _game.Items[item].PropertyChanged += OnRequirementChanged;
                        }
                    }
                    else
                    {
                        _itemIsSubscribed.Add(item, true);
                        _game.Items[item].PropertyChanged += OnRequirementChanged;
                    }
                }
                else
                {
                    if (_itemIsSubscribed.ContainsKey(item))
                    {
                        if (_itemIsSubscribed[item])
                        {
                            _itemIsSubscribed[item] = false;
                            _game.Items[item].PropertyChanged -= OnRequirementChanged;
                        }
                    }
                    else
                        _itemIsSubscribed.Add(item, false);
                }
            }
        }

        private void UpdateAccessibility()
        {
            if (_game.Mode.EntranceShuffle.Value)
                Accessibility = GetAccessibility();
        }

        public void Clear()
        {
            Available = false;
        }

        public bool IsAvailable()
        {
            return Available;
        }

        public void Reset()
        {
            Marking = null;
            Available = true;
        }
    }
}
