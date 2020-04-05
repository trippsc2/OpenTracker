using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models
{
    public class ItemSection : ISection
    {
        private readonly Game _game;
        private readonly Location _location;
        private readonly int _mapCompass;
        private readonly int _smallKey;
        private readonly int _bigKey;
        private readonly int _baseTotal;
        private readonly bool _updateOnDungeonItemShuffleChange;
        private readonly bool _updateOnItemPlacementChange;
        private readonly bool _updateOnEnemyShuffleChange;
        private readonly Dictionary<RegionID, Mode> _regionSubscriptions;
        private readonly Dictionary<RegionID, bool> _regionIsSubscribed;
        private readonly Dictionary<ItemType, Mode> _itemSubscriptions;
        private readonly Dictionary<ItemType, bool> _itemIsSubscribed;

        public string Name { get; }
        public int Total { get; private set; }
        public bool HasMarking { get; }
        public Mode RequiredMode { get; }

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

        public Func<AccessibilityLevel> GetAccessibility { get; }

        public ItemSection(Game game, Location location, int index = 0)
        {
            _game = game;
            _location = location;

            _regionSubscriptions = new Dictionary<RegionID, Mode>();
            _regionIsSubscribed = new Dictionary<RegionID, bool>();
            _itemSubscriptions = new Dictionary<ItemType, Mode>();
            _itemIsSubscribed = new Dictionary<ItemType, bool>();

            switch (_location.ID)
            {
                case LocationID.Pedestal:

                    _baseTotal = 1;
                    Name = "Pedestal";
                    HasMarking = true;

                    GetAccessibility = () =>
                    {
                        if (_game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Items.Has(ItemType.GreenPendant) && _game.Items.Has(ItemType.Pendant, 2))
                            {
                                //  Basic Item Placement requires Book in logic
                                if (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                    _game.Items.Has(ItemType.Book))
                                    return _game.Regions[RegionID.LightWorld].Accessibility;

                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                    (byte)_game.Regions[RegionID.LightWorld].Accessibility);
                            }

                            if (_game.Items.Has(ItemType.Book))
                                return AccessibilityLevel.Inspect;
                        }

                        return AccessibilityLevel.None;
                    };

                    _updateOnItemPlacementChange = true;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.GreenPendant, new Mode());
                    _itemSubscriptions.Add(ItemType.Pendant, new Mode());
                    _itemSubscriptions.Add(ItemType.Book, new Mode() { ItemPlacement = ItemPlacement.Basic });

                    break;
                case LocationID.LumberjackCave:

                    _baseTotal = 1;
                    Name = "Cave";
                    HasMarking = true;

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
                case LocationID.BlindsHouse when index == 0:

                    _baseTotal = 4;
                    Name = "Main";

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
                case LocationID.BlindsHouse:

                    _baseTotal = 1;
                    Name = "Bomb";

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
                case LocationID.TheWell when index == 0:

                    _baseTotal = 4;
                    Name = "Cave";

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
                case LocationID.TheWell:

                    _baseTotal = 1;
                    Name = "Bomb";

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
                case LocationID.BottleVendor:

                    _baseTotal = 1;
                    Name = "This Jerk";
                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
                case LocationID.ChickenHouse:

                    _baseTotal = 1;
                    Name = "Bombable Wall";

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
                case LocationID.Tavern:

                    _baseTotal = 1;
                    Name = "Back Room";

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
                case LocationID.SickKid:

                    _baseTotal = 1;
                    Name = "By The Bed";

                    GetAccessibility = () =>
                    {
                        if (_game.Items.Has(ItemType.Bottle))
                            return _game.Regions[RegionID.LightWorld].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.Bottle, new Mode());

                    break;
                case LocationID.MagicBat:

                    _baseTotal = 1;
                    Name = "Magic Bowl";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Powder))
                            {
                                if (_game.Items.Has(ItemType.Hammer))
                                    return _game.Regions[RegionID.LightWorld].Accessibility;

                                if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Gloves, 2) &&
                                    _game.Items.Has(ItemType.Mirror))
                                    return AccessibilityLevel.Normal;
                            }

                            if (_game.Items.Has(ItemType.Mushroom) && _game.Items.Has(ItemType.CaneOfSomaria))
                            {
                                if (_game.Items.Has(ItemType.Hammer))
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                        (byte)_game.Regions[RegionID.LightWorld].Accessibility);
                                }

                                if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Gloves, 2) &&
                                    _game.Items.Has(ItemType.Mirror))
                                    return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Hammer))
                            {
                                if (_game.Items.Has(ItemType.Powder))
                                    return _game.Regions[RegionID.LightWorld].Accessibility;

                                if (_game.Items.Has(ItemType.Mushroom) && _game.Items.Has(ItemType.CaneOfSomaria))
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                        (byte)_game.Regions[RegionID.LightWorld].Accessibility);
                                }
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.Powder, new Mode());
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode());
                    _itemSubscriptions.Add(ItemType.Gloves, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Mushroom, new Mode());
                    _itemSubscriptions.Add(ItemType.CaneOfSomaria, new Mode());

                    break;
                case LocationID.RaceGame:

                    _baseTotal = 1;
                    Name = "Take This Trash";
                    HasMarking = true;

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.RaceGameAccess))
                                return AccessibilityLevel.Normal;

                            AccessibilityLevel lightWorld = AccessibilityLevel.None;
                            AccessibilityLevel dWSouth = AccessibilityLevel.None;

                            if (!_game.Mode.EntranceShuffle.Value)
                                lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;
                            else
                            {
                                lightWorld = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Inspect,
                                    (byte)_game.Regions[RegionID.LightWorld].Accessibility);
                            }

                            if (_game.Items.Has(ItemType.Mirror))
                                dWSouth = _game.Regions[RegionID.DarkWorldWest].Accessibility;

                            return (AccessibilityLevel)Math.Max((byte)lightWorld, (byte)dWSouth);
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                            {
                                if (_game.Items.Has(ItemType.RaceGameAccess))
                                    return AccessibilityLevel.Normal;

                                if (!_game.Mode.EntranceShuffle.Value)
                                    return _game.Regions[RegionID.LightWorld].Accessibility;

                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Inspect,
                                    (byte)_game.Regions[RegionID.LightWorld].Accessibility);
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());
                    _regionSubscriptions.Add(RegionID.DarkWorldWest, new Mode() { WorldState = WorldState.StandardOpen });

                    _itemSubscriptions.Add(ItemType.RaceGameAccess, new Mode());
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    _game.Regions[RegionID.DarkWorldWest].PropertyChanged += OnRequirementChanged;

                    break;
                case LocationID.Library:

                    _baseTotal = 1;
                    Name = "On The Shelf";
                    HasMarking = true;

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Boots))
                                return _game.Regions[RegionID.LightWorld].Accessibility;

                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Inspect,
                                (byte)_game.Regions[RegionID.LightWorld].Accessibility);
                        }    

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Boots))
                                return _game.Regions[RegionID.LightWorld].Accessibility;

                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Inspect,
                                (byte)_game.Regions[RegionID.LightWorld].Accessibility);
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.Boots, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.LostWoods when index == 0:

                    _baseTotal = 1;
                    Name = "Shroom";
                    HasMarking = true;

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
                case LocationID.LostWoods:

                    _baseTotal = 1;
                    Name = "Hideout";

                    RequiredMode = new Mode() { EntranceShuffle = false };

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
                case LocationID.CastleSecret when index == 0:

                    _baseTotal = 1;
                    Name = "Uncle";

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
                case LocationID.CastleSecret:

                    _baseTotal = 1;
                    Name = "Hallway";

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
                case LocationID.LinksHouse:

                    _baseTotal = 1;
                    Name = "By The Door";

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
                case LocationID.GroveDiggingSpot:

                    _baseTotal = 1;
                    Name = "Hidden Treasure";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Shovel))
                                return _game.Regions[RegionID.LightWorld].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Shovel))
                                return _game.Regions[RegionID.LightWorld].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.Shovel, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.PyramidLedge:

                    _baseTotal = 1;
                    Name = "Ledge";

                    GetAccessibility = () => { return _game.Regions[RegionID.DarkWorldEast].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DarkWorldEast, new Mode());

                    break;
                case LocationID.FatFairy:

                    _baseTotal = 2;
                    Name = "Big Bomb Spot";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.RedCrystal, 2))
                            {
                                if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Gloves))
                                    return _game.Regions[RegionID.LightWorld].Accessibility;

                                if (_game.Items.Has(ItemType.Aga))
                                {
                                    if (_game.Items.Has(ItemType.Hammer))
                                        return _game.Regions[RegionID.LightWorld].Accessibility;

                                    if (_game.Items.Has(ItemType.Mirror))
                                    {
                                        if (_game.Items.Has(ItemType.Gloves, 2))
                                            return _game.Regions[RegionID.LightWorld].Accessibility;

                                        if (_game.Items.Has(ItemType.Hookshot) &&
                                            (_game.Items.Has(ItemType.Flippers) || _game.Items.Has(ItemType.Gloves)))
                                            return _game.Regions[RegionID.LightWorld].Accessibility;
                                    }
                                }

                                if (_game.Items.Has(ItemType.Gloves, 2))
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                        (byte)_game.Regions[RegionID.LightWorld].Accessibility);
                                }
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.RedCrystal, 2) && _game.Items.Has(ItemType.Mirror))
                                return _game.Regions[RegionID.LightWorld].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.RedCrystal, new Mode());
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Gloves, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Aga, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode());
                    _itemSubscriptions.Add(ItemType.Hookshot, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Flippers, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.HauntedGrove:

                    _baseTotal = 1;
                    Name = "Stumpy";

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

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.HypeCave:

                    _baseTotal = 5;
                    Name = "Cave";

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

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.BombosTablet:

                    _baseTotal = 1;
                    Name = "Tablet";
                    HasMarking = true;

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Book) && _game.Items.Has(ItemType.Mirror))
                            {
                                if (_game.Items.CanActivateTablets())
                                    return _game.Regions[RegionID.DarkWorldSouth].Accessibility;

                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Inspect,
                                    (byte)_game.Regions[RegionID.DarkWorldSouth].Accessibility);
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Book))
                            {
                                if (_game.Items.CanActivateTablets())
                                    return _game.Regions[RegionID.LightWorld].Accessibility;

                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Inspect,
                                    (byte)_game.Regions[RegionID.LightWorld].Accessibility);
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DarkWorldSouth, new Mode() { WorldState = WorldState.StandardOpen });
                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode() { WorldState = WorldState.Inverted });

                    _itemSubscriptions.Add(ItemType.Book, new Mode());
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Sword, new Mode());
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());

                    break;
                case LocationID.SouthOfGrove:

                    _baseTotal = 1;
                    Name = "Circle of Bushes";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Mirror))
                                return _game.Regions[RegionID.DarkWorldSouth].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.LightWorld].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DarkWorldSouth, new Mode() { WorldState = WorldState.StandardOpen });
                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode() { WorldState = WorldState.Inverted });

                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.DiggingGame:

                    _baseTotal = 1;
                    Name = "Dig For Treasure";

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

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.WitchsHut:

                    _baseTotal = 1;
                    Name = "Assistant";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Mushroom))
                                return _game.Regions[RegionID.LightWorld].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Mushroom))
                                return _game.Regions[RegionID.LightWorld].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.Mushroom, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.WaterfallFairy:

                    _baseTotal = 2;
                    Name = "Waterfall Cave";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Flippers))
                                return _game.Regions[RegionID.LightWorld].Accessibility;

                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                (byte)_game.Regions[RegionID.LightWorld].Accessibility);
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                            {
                                if (_game.Items.Has(ItemType.Flippers))
                                    return _game.Regions[RegionID.LightWorld].Accessibility;

                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                    (byte)_game.Regions[RegionID.LightWorld].Accessibility);
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.Flippers, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.ZoraArea when index == 0:

                    _baseTotal = 1;
                    Name = "King Zora";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Gloves) || _game.Items.Has(ItemType.Flippers))
                                return _game.Regions[RegionID.LightWorld].Accessibility;

                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                (byte)_game.Regions[RegionID.LightWorld].Accessibility);
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                            {
                                if (_game.Items.Has(ItemType.Gloves) || _game.Items.Has(ItemType.Flippers))
                                    return _game.Regions[RegionID.LightWorld].Accessibility;

                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                    (byte)_game.Regions[RegionID.LightWorld].Accessibility);
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.Gloves, new Mode());
                    _itemSubscriptions.Add(ItemType.Flippers, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.ZoraArea:

                    _baseTotal = 1;
                    Name = "Ledge";
                    HasMarking = true;

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Flippers))
                                return _game.Regions[RegionID.LightWorld].Accessibility;

                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                (byte)_game.Regions[RegionID.LightWorld].Accessibility);
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                            {
                                if (_game.Items.Has(ItemType.Flippers))
                                    return _game.Regions[RegionID.LightWorld].Accessibility;

                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                    (byte)_game.Regions[RegionID.LightWorld].Accessibility);
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.Flippers, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.Catfish:

                    _baseTotal = 1;
                    Name = "Ring of Stones";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.DarkWorldEast].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Gloves))
                                return _game.Regions[RegionID.DarkWorldEast].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DarkWorldEast, new Mode());

                    _itemSubscriptions.Add(ItemType.Gloves, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.SahasrahlasHut when index == 0:

                    _baseTotal = 3;
                    Name = "Back Room";

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
                case LocationID.SahasrahlasHut:

                    _baseTotal = 1;
                    Name = "Shabba";

                    GetAccessibility = () =>
                    {
                        if (_game.Items.Has(ItemType.GreenPendant))
                            return _game.Regions[RegionID.LightWorld].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.GreenPendant, new Mode());

                    break;
                case LocationID.BonkRocks:

                    _baseTotal = 1;
                    Name = "Cave";

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
                case LocationID.KingsTomb:

                    _baseTotal = 1;
                    Name = "The Crypt";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Boots))
                            {
                                AccessibilityLevel direct = AccessibilityLevel.None;
                                AccessibilityLevel mirror = AccessibilityLevel.None;

                                if (_game.Items.Has(ItemType.Gloves, 2))
                                    direct = _game.Regions[RegionID.LightWorld].Accessibility;

                                if (_game.Items.Has(ItemType.Mirror) && _game.Items.Has(ItemType.MoonPearl))
                                    mirror = _game.Regions[RegionID.DarkWorldWest].Accessibility;

                                return (AccessibilityLevel)Math.Max((byte)direct, (byte)mirror);
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Boots) &&
                                _game.Items.Has(ItemType.Gloves, 2))
                                return AccessibilityLevel.Normal;
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
                case LocationID.GraveyardLedge:

                    _baseTotal = 1;
                    Name = "Cave";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Mirror) && _game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.DarkWorldWest].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.LightWorld].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DarkWorldWest, new Mode() { WorldState = WorldState.StandardOpen });
                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode() { WorldState = WorldState.Inverted });

                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode());

                    break;
                case LocationID.DesertLedge:

                    _baseTotal = 1;
                    Name = "Ledge";
                    HasMarking = true;

                    GetAccessibility = () =>
                    {
                        if (_game.Items.Has(ItemType.DesertLeftAccess))
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.DesertBackAccess) && _game.Items.Has(ItemType.Gloves))
                                return AccessibilityLevel.Normal;

                            AccessibilityLevel lightWorld = AccessibilityLevel.None;
                            AccessibilityLevel mireArea = AccessibilityLevel.None;

                            if (!_game.Mode.EntranceShuffle.Value && _game.Items.Has(ItemType.Book))
                                lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;
                            else
                                lightWorld = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Inspect,
                                    (byte)_game.Regions[RegionID.LightWorld].Accessibility);

                            if (_game.Items.Has(ItemType.Mirror))
                                mireArea = _game.Regions[RegionID.MireArea].Accessibility;

                            return (AccessibilityLevel)Math.Max((byte)lightWorld, (byte)mireArea);
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                            {
                                if (_game.Items.Has(ItemType.DesertBackAccess) && _game.Items.Has(ItemType.Gloves))
                                    return AccessibilityLevel.Normal;

                                if (!_game.Mode.EntranceShuffle.Value && _game.Items.Has(ItemType.Book))
                                    return _game.Regions[RegionID.LightWorld].Accessibility;
                            }

                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Inspect,
                                (byte)_game.Regions[RegionID.LightWorld].Accessibility);
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());
                    _regionSubscriptions.Add(RegionID.MireArea, new Mode() { WorldState = WorldState.StandardOpen });

                    _itemSubscriptions.Add(ItemType.DesertLeftAccess, new Mode());
                    _itemSubscriptions.Add(ItemType.DesertBackAccess, new Mode());
                    _itemSubscriptions.Add(ItemType.Gloves, new Mode());
                    _itemSubscriptions.Add(ItemType.Book, new Mode());
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.AginahsCave:

                    _baseTotal = 1;
                    Name = "Cave";

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
                case LocationID.CShapedHouse:

                    _baseTotal = 1;
                    Name = "House";

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

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.TreasureGame:

                    _baseTotal = 1;
                    Name = "Prize";

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

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.BombableShack:

                    _baseTotal = 1;
                    Name = "Downstairs";

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

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.Blacksmith:

                    _baseTotal = 1;
                    Name = "House";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Gloves, 2) && _game.Items.Has(ItemType.MoonPearl))
                            {
                                AccessibilityLevel dWWest = AccessibilityLevel.None;
                                AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                                if (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                    _game.Items.Has(ItemType.Mirror))
                                    dWWest = _game.Regions[RegionID.DarkWorldWest].Accessibility;

                                dWWest = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                    (byte)_game.Regions[RegionID.DarkWorldWest].Accessibility);

                                return (AccessibilityLevel)Math.Min((byte)dWWest, (byte)lightWorld);
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;
                            AccessibilityLevel dWWest = AccessibilityLevel.None;

                            if (_game.Items.Has(ItemType.Mirror))
                                return lightWorld;

                            if (_game.Items.Has(ItemType.Gloves, 2))
                            {
                                dWWest = _game.Regions[RegionID.DarkWorldWest].Accessibility;

                                return (AccessibilityLevel)Math.Min((byte)lightWorld, (byte)dWWest);
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    _updateOnItemPlacementChange = true;

                    _regionSubscriptions.Add(RegionID.DarkWorldWest, new Mode());
                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.Gloves, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode());

                    break;
                case LocationID.PurpleChest:

                    _baseTotal = 1;
                    Name = "Show To Gary";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Gloves, 2) && _game.Items.Has(ItemType.MoonPearl))
                            {
                                AccessibilityLevel dWWest = AccessibilityLevel.None;
                                AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                                dWWest = _game.Regions[RegionID.DarkWorldWest].Accessibility;

                                return (AccessibilityLevel)Math.Min((byte)dWWest, (byte)lightWorld);
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;
                            AccessibilityLevel dWWest = AccessibilityLevel.None;

                            if (_game.Items.Has(ItemType.Mirror))
                                return lightWorld;

                            if (_game.Items.Has(ItemType.Gloves, 2))
                            {
                                dWWest = _game.Regions[RegionID.DarkWorldWest].Accessibility;

                                return (AccessibilityLevel)Math.Min((byte)lightWorld, (byte)dWWest);
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DarkWorldWest, new Mode());
                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.Gloves, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode());

                    break;
                case LocationID.HammerPegs:

                    _baseTotal = 1;
                    Name = "Cave";

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
                                AccessibilityLevel dWWest = AccessibilityLevel.None;
                                AccessibilityLevel lightWorld = AccessibilityLevel.None;

                                if (_game.Items.Has(ItemType.Gloves, 2))
                                    dWWest = _game.Regions[RegionID.DarkWorldWest].Accessibility;

                                if (_game.Items.Has(ItemType.Mirror))
                                    lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                                return (AccessibilityLevel)Math.Max((byte)dWWest, (byte)lightWorld);
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
                case LocationID.BumperCave:

                    _baseTotal = 1;
                    Name = "Ledge";
                    HasMarking = true;

                    GetAccessibility = () =>
                    {
                        if (_game.Items.Has(ItemType.BumperCaveAccess))
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            AccessibilityLevel dWWest = AccessibilityLevel.None;
                            AccessibilityLevel inspect = AccessibilityLevel.None;

                            if (!_game.Mode.EntranceShuffle.Value)
                            {
                                if (_game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.Cape) &&
                                    _game.Items.Has(ItemType.MoonPearl))
                                {
                                    if (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                        _game.Items.Has(ItemType.Hookshot))
                                        dWWest = _game.Regions[RegionID.DarkWorldWest].Accessibility;
                                    else
                                        dWWest = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                            (byte)_game.Regions[RegionID.DarkWorldWest].Accessibility);
                                }
                            }
                            
                            inspect = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Inspect,
                                (byte)_game.Regions[RegionID.DarkWorldWest].Accessibility);

                            return (AccessibilityLevel)Math.Max((byte)dWWest, (byte)inspect);
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.DeathMountainExitAccess) && _game.Items.Has(ItemType.Mirror))
                                return AccessibilityLevel.Normal;

                            AccessibilityLevel lightWorld = AccessibilityLevel.None;
                            AccessibilityLevel dWWest = AccessibilityLevel.None;

                            if (!_game.Mode.EntranceShuffle.Value && _game.Items.Has(ItemType.MoonPearl) &&
                                _game.Items.Has(ItemType.Cape) && _game.Items.Has(ItemType.Gloves) &&
                                _game.Items.Has(ItemType.Mirror))
                                lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;
                            
                            dWWest = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Inspect,
                                (byte)_game.Regions[RegionID.DarkWorldWest].Accessibility);

                            return (AccessibilityLevel)Math.Max((byte)lightWorld, (byte)dWWest);
                        }

                        return AccessibilityLevel.None;
                    };

                    _updateOnItemPlacementChange = true;

                    _regionSubscriptions.Add(RegionID.DarkWorldWest, new Mode());

                    _itemSubscriptions.Add(ItemType.BumperCaveAccess, new Mode());
                    _itemSubscriptions.Add(ItemType.Gloves, new Mode());
                    _itemSubscriptions.Add(ItemType.Cape, new Mode());
                    _itemSubscriptions.Add(ItemType.Hookshot, new Mode() { ItemPlacement = ItemPlacement.Advanced });
                    _itemSubscriptions.Add(ItemType.DeathMountainExitAccess, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode());

                    _game.Regions[RegionID.DarkWorldWest].PropertyChanged += OnRequirementChanged;

                    break;
                case LocationID.Dam when index == 0:

                    _baseTotal = 1;
                    Name = "Inside";
                    RequiredMode = new Mode() { EntranceShuffle = false };

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
                case LocationID.Dam:

                    _baseTotal = 1;
                    Name = "Outside";

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
                case LocationID.MiniMoldormCave:

                    _baseTotal = 5;
                    Name = "Cave";

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
                case LocationID.IceRodCave:

                    _baseTotal = 1;
                    Name = "Cave";

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
                case LocationID.LakeHyliaIsland:

                    _baseTotal = 1;
                    Name = "Island";
                    HasMarking = true;

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            AccessibilityLevel lightWorld = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Inspect,
                                (byte)_game.Regions[RegionID.LightWorld].Accessibility);
                            AccessibilityLevel dWEast = AccessibilityLevel.None;
                            AccessibilityLevel dWSouth = AccessibilityLevel.None;
                            AccessibilityLevel dWSouthEast = AccessibilityLevel.None;

                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Mirror))
                            {
                                if (_game.Items.Has(ItemType.Flippers))
                                {
                                    dWEast = _game.Regions[RegionID.DarkWorldWest].Accessibility;
                                    dWSouth = _game.Regions[RegionID.DarkWorldSouth].Accessibility;
                                    dWSouthEast = _game.Regions[RegionID.DarkWorldSouthEast].Accessibility;
                                }
                                else
                                {
                                    dWEast = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                        (byte)_game.Regions[RegionID.DarkWorldWest].Accessibility);
                                    dWSouth = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                        (byte)_game.Regions[RegionID.DarkWorldSouth].Accessibility);
                                    dWSouthEast = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                        (byte)_game.Regions[RegionID.DarkWorldSouthEast].Accessibility);
                                }
                            }

                            return (AccessibilityLevel)Math.Max(Math.Max(Math.Max((byte)lightWorld, (byte)dWEast),
                                (byte)dWSouth), (byte)dWSouthEast);
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                            {
                                if (_game.Items.Has(ItemType.Flippers))
                                    return _game.Regions[RegionID.LightWorld].Accessibility;

                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                    (byte)_game.Regions[RegionID.LightWorld].Accessibility);

                            }   
                        }

                        return AccessibilityLevel.Inspect;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());
                    _regionSubscriptions.Add(RegionID.DarkWorldEast, new Mode() { WorldState = WorldState.StandardOpen });
                    _regionSubscriptions.Add(RegionID.DarkWorldSouth, new Mode() { WorldState = WorldState.StandardOpen });
                    _regionSubscriptions.Add(RegionID.DarkWorldSouthEast, new Mode() { WorldState = WorldState.StandardOpen });

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode());
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Flippers, new Mode());

                    break;
                case LocationID.Hobo:

                    _baseTotal = 1;
                    Name = "Under The Bridge";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Flippers))
                                return _game.Regions[RegionID.LightWorld].Accessibility;

                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                (byte)_game.Regions[RegionID.LightWorld].Accessibility);
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                            {
                                if (_game.Items.Has(ItemType.Flippers))
                                    return _game.Regions[RegionID.LightWorld].Accessibility;

                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                    (byte)_game.Regions[RegionID.LightWorld].Accessibility);
                            }
                        }

                        return AccessibilityLevel.SequenceBreak;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.Flippers, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.MireShack:

                    _baseTotal = 2;
                    Name = "Shack";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.MireArea].Accessibility;

                            if (_game.Items.Has(ItemType.Mirror))
                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                    (byte)_game.Regions[RegionID.MireArea].Accessibility);
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                            return _game.Regions[RegionID.MireArea].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.MireArea, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.CheckerboardCave:

                    _baseTotal = 1;
                    Name = "Cave";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Mirror) && _game.Items.Has(ItemType.Gloves))
                                return _game.Regions[RegionID.MireArea].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Gloves))
                                return _game.Regions[RegionID.LightWorld].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.MireArea, new Mode() { WorldState = WorldState.StandardOpen });
                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode() { WorldState = WorldState.Inverted });

                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Gloves, new Mode());

                    break;
                case LocationID.OldMan:

                    _baseTotal = 1;
                    Name = "Bring Him Home";

                    GetAccessibility = () =>
                    {
                        if (_game.Items.Has(ItemType.Lamp))
                            return _game.Regions[RegionID.DeathMountainWestBottom].Accessibility;
                        
                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                            (byte)_game.Regions[RegionID.DeathMountainWestBottom].Accessibility);
                    };

                    _regionSubscriptions.Add(RegionID.DeathMountainWestBottom, new Mode());

                    _itemSubscriptions.Add(ItemType.Lamp, new Mode());

                    break;
                case LocationID.SpectacleRock when index == 0:

                    _baseTotal = 1;
                    Name = "Cave";

                    RequiredMode = new Mode() { EntranceShuffle = false };

                    GetAccessibility = () => { return _game.Regions[RegionID.DeathMountainWestBottom].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DeathMountainWestBottom, new Mode());

                    break;
                case LocationID.SpectacleRock:

                    _baseTotal = 1;
                    Name = "Up On Top";
                    HasMarking = true;

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            AccessibilityLevel dMWestBottom = AccessibilityLevel.None;
                            AccessibilityLevel inspect = AccessibilityLevel.None;

                            if (_game.Items.Has(ItemType.Mirror))
                                dMWestBottom = _game.Regions[RegionID.DeathMountainWestBottom].Accessibility;

                            inspect = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Inspect,
                                (byte)_game.Regions[RegionID.DeathMountainWestBottom].Accessibility);

                            return (AccessibilityLevel)Math.Max((byte)dMWestBottom, (byte)inspect);
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            AccessibilityLevel dMWestTop = _game.Regions[RegionID.DeathMountainWestTop].Accessibility;
                            AccessibilityLevel inspect = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Inspect,
                                (byte)_game.Regions[RegionID.DeathMountainWestBottom].Accessibility);

                            return (AccessibilityLevel)Math.Max((byte)dMWestTop, (byte)inspect);
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DeathMountainWestBottom, new Mode());
                    _regionSubscriptions.Add(RegionID.DeathMountainWestTop, new Mode() { WorldState = WorldState.Inverted });

                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.EtherTablet:

                    _baseTotal = 1;
                    Name = "Tablet";
                    HasMarking = true;

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel dMWestTop = AccessibilityLevel.None;
                        AccessibilityLevel inspect = AccessibilityLevel.None;

                        if (_game.Items.Has(ItemType.Book))
                        {
                            if (_game.Items.CanActivateTablets())
                                dMWestTop = _game.Regions[RegionID.DeathMountainWestTop].Accessibility;
                            
                            inspect = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Inspect,
                                (byte)_game.Regions[RegionID.DeathMountainWestTop].Accessibility);

                            return (AccessibilityLevel)Math.Max((byte)dMWestTop, (byte)inspect);
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DeathMountainWestTop, new Mode());

                    _itemSubscriptions.Add(ItemType.Book, new Mode());
                    _itemSubscriptions.Add(ItemType.Sword, new Mode());
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());

                    break;
                case LocationID.SpikeCave:

                    _baseTotal = 1;
                    Name = "Cave";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.Hammer) &&
                                _game.Items.Has(ItemType.MoonPearl))
                            {
                                if (_game.Items.Has(ItemType.CaneOfByrna) || (_game.Items.Has(ItemType.Cape) &&
                                    (_game.Items.Has(ItemType.HalfMagic) || _game.Items.Has(ItemType.Bottle))))
                                    return _game.Regions[RegionID.DarkDeathMountainWestBottom].Accessibility;

                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                    (byte)_game.Regions[RegionID.DarkDeathMountainWestBottom].Accessibility);
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.Hammer))
                            {
                                if (_game.Items.Has(ItemType.CaneOfByrna) || (_game.Items.Has(ItemType.Cape) &&
                                    (_game.Items.Has(ItemType.HalfMagic) || _game.Items.Has(ItemType.Bottle))))
                                    return _game.Regions[RegionID.DarkDeathMountainWestBottom].Accessibility;

                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                    (byte)_game.Regions[RegionID.DarkDeathMountainWestBottom].Accessibility);
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DarkDeathMountainWestBottom, new Mode());

                    _itemSubscriptions.Add(ItemType.Gloves, new Mode());
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.CaneOfByrna, new Mode());
                    _itemSubscriptions.Add(ItemType.Cape, new Mode());
                    _itemSubscriptions.Add(ItemType.HalfMagic, new Mode());
                    _itemSubscriptions.Add(ItemType.Bottle, new Mode());

                    break;
                case LocationID.SpiralCave:

                    _baseTotal = 1;
                    Name = "Cave";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                            return _game.Regions[RegionID.DeathMountainEastTop].Accessibility;

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.DeathMountainEastTop].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DeathMountainEastTop, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.ParadoxCave when index == 0:

                    _baseTotal = 2;
                    Name = "Bottom";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                            return _game.Regions[RegionID.DeathMountainEastTop].Accessibility;

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.DeathMountainEastTop].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DeathMountainEastTop, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.ParadoxCave:

                    _baseTotal = 5;
                    Name = "Top";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                            return _game.Regions[RegionID.DeathMountainEastTop].Accessibility;

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.DeathMountainEastTop].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DeathMountainEastTop, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.SuperBunnyCave:

                    _baseTotal = 2;
                    Name = "Cave";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.DarkDeathMountainTop].Accessibility;

                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                (byte)_game.Regions[RegionID.DarkDeathMountainTop].Accessibility);
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                            return _game.Regions[RegionID.DarkDeathMountainTop].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DarkDeathMountainTop, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.HookshotCave when index == 0:

                    _baseTotal = 1;
                    Name = "Bonkable Chest";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Gloves))
                            {
                                if (_game.Items.Has(ItemType.Hookshot) ||
                                    (_game.Items.Has(ItemType.Boots) &&
                                    (_game.Mode.ItemPlacement == ItemPlacement.Advanced)))
                                    return _game.Regions[RegionID.DarkDeathMountainTop].Accessibility;

                                if (_game.Items.Has(ItemType.Boots))
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                        (byte)_game.Regions[RegionID.DarkDeathMountainTop].Accessibility);
                                }
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Gloves) &&
                                (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots)))
                                    return _game.Regions[RegionID.DarkDeathMountainTop].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _updateOnItemPlacementChange = true;

                    _regionSubscriptions.Add(RegionID.DarkDeathMountainTop, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Gloves, new Mode());
                    _itemSubscriptions.Add(ItemType.Hookshot, new Mode());
                    _itemSubscriptions.Add(ItemType.Boots, new Mode());

                    break;
                case LocationID.HookshotCave:

                    _baseTotal = 3;
                    Name = "Back";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.MoonPearl) &&
                                _game.Items.Has(ItemType.Hookshot))
                                return _game.Regions[RegionID.DarkDeathMountainTop].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.Hookshot))
                                return _game.Regions[RegionID.DarkDeathMountainTop].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DarkDeathMountainTop, new Mode());

                    _itemSubscriptions.Add(ItemType.Gloves, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Hookshot, new Mode());

                    break;
                case LocationID.FloatingIsland:

                    _baseTotal = 1;
                    Name = "Island";
                    HasMarking = true;

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            AccessibilityLevel dDMTop = AccessibilityLevel.None;
                            AccessibilityLevel inspect = AccessibilityLevel.None;

                            if (_game.Items.Has(ItemType.Mirror))
                            {
                                if (_game.Items.Has(ItemType.DarkDeathMountainFloatingIslandAccess))
                                    return AccessibilityLevel.Normal;

                                if (!_game.Mode.EntranceShuffle.Value && _game.Items.Has(ItemType.Gloves) &&
                                    _game.Items.Has(ItemType.MoonPearl))
                                    dDMTop = _game.Regions[RegionID.DarkDeathMountainTop].Accessibility;
                            }
                            
                            inspect = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Inspect,
                                (byte)_game.Regions[RegionID.DeathMountainEastTop].Accessibility);

                            return (AccessibilityLevel)Math.Max((byte)dDMTop, (byte)inspect);
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                            return _game.Regions[RegionID.DeathMountainEastTop].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DarkDeathMountainTop, new Mode() { WorldState = WorldState.StandardOpen });
                    _regionSubscriptions.Add(RegionID.DeathMountainEastTop, new Mode());

                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.DarkDeathMountainFloatingIslandAccess, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Gloves, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.MimicCave:

                    _baseTotal = 1;
                    Name = "Cave";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Mirror) && _game.Items.Has(ItemType.Hammer) &&
                                _game.Items.Has(ItemType.CaneOfSomaria))
                            {
                                if (_game.Items.Has(ItemType.TRSmallKey, 2) &&
                                    (_game.Mode.DungeonItemShuffle.Value >= DungeonItemShuffle.MapsCompassesSmallKeys ||
                                    _game.Items.Has(ItemType.FireRod)))
                                    return _game.Regions[RegionID.TurtleRock].Accessibility;

                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                    (byte)_game.Regions[RegionID.TurtleRock].Accessibility);
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Hammer))
                                return _game.Regions[RegionID.DeathMountainEastTop].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    _updateOnDungeonItemShuffleChange = true;

                    _regionSubscriptions.Add(RegionID.TurtleRock, new Mode() { WorldState = WorldState.StandardOpen });
                    _regionSubscriptions.Add(RegionID.DeathMountainEastTop, new Mode() { WorldState = WorldState.Inverted });

                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.CaneOfSomaria, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.TRSmallKey, new Mode()
                    {
                        WorldState = WorldState.StandardOpen,
                        DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys
                    });
                    _itemSubscriptions.Add(ItemType.FireRod, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.HyruleCastle:

                    _mapCompass = 1;
                    _smallKey = 1;
                    _baseTotal = 6;
                    Name = "Escape";

                    GetAccessibility = () =>
                    {
                        switch (_game.Mode.DungeonItemShuffle.Value)
                        {
                            case DungeonItemShuffle.Standard:
                            case DungeonItemShuffle.MapsCompasses:

                                if (_game.Items.Has(ItemType.Gloves) && (_game.Items.Has(ItemType.Lamp) ||
                                    (_game.Mode.ItemPlacement == ItemPlacement.Advanced &&
                                    _game.Items.Has(ItemType.FireRod))))
                                    return _game.Regions[RegionID.HyruleCastle].Accessibility;

                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                    (byte)_game.Regions[RegionID.HyruleCastle].Accessibility);

                            case DungeonItemShuffle.MapsCompassesSmallKeys:
                            case DungeonItemShuffle.Keysanity:

                                if (_game.Items.Has(ItemType.Gloves) || _game.Items.Has(ItemType.HCSmallKey))
                                {
                                    if (_game.Items.Has(ItemType.Lamp) ||
                                        (_game.Mode.ItemPlacement.Value == ItemPlacement.Advanced &&
                                        _game.Items.Has(ItemType.FireRod)))
                                        return _game.Regions[RegionID.HyruleCastle].Accessibility;
                                }

                                if (Available > 3)
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                        (byte)_game.Regions[RegionID.HyruleCastle].Accessibility);
                                }

                                break;
                        }

                        return AccessibilityLevel.None;
                    };

                    _updateOnItemPlacementChange = true;
                    _updateOnDungeonItemShuffleChange = true;

                    _regionSubscriptions.Add(RegionID.HyruleCastle, new Mode());

                    _itemSubscriptions.Add(ItemType.Gloves, new Mode());
                    _itemSubscriptions.Add(ItemType.HCSmallKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys });
                    _itemSubscriptions.Add(ItemType.Lamp, new Mode());
                    _itemSubscriptions.Add(ItemType.FireRod, new Mode() { ItemPlacement = ItemPlacement.Basic });

                    break;
                case LocationID.AgahnimTower:

                    _baseTotal = 0;
                    _smallKey = 2;
                    Name = "Dungeon";

                    RequiredMode = new Mode() { DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys };

                    GetAccessibility = () =>
                    {
                        if (_game.Items.Has(ItemType.ATSmallKey))
                        {
                            if (_game.Items.Has(ItemType.Lamp))
                                return _game.Regions[RegionID.Agahnim].Accessibility;

                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                (byte)_game.Regions[RegionID.Agahnim].Accessibility);
                        }

                        if (Available > 1)
                        {
                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                (byte)_game.Regions[RegionID.Agahnim].Accessibility);
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.Agahnim, new Mode() { DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys });

                    _itemSubscriptions.Add(ItemType.Lamp, new Mode() { DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys });
                    _itemSubscriptions.Add(ItemType.ATSmallKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys });

                    PropertyChanged += OnRequirementChanged;

                    break;
                case LocationID.EasternPalace:

                    _mapCompass = 2;
                    _bigKey = 1;
                    _baseTotal = 3;
                    Name = "Dungeon";

                    GetAccessibility = () =>
                    {
                        switch (_game.Mode.DungeonItemShuffle.Value)
                        {
                            case DungeonItemShuffle.Standard:

                                if (_game.Items.Has(ItemType.Lamp) &&
                                    (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value))
                                    return _game.Regions[RegionID.EasternPalace].Accessibility;

                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                    (byte)_game.Regions[RegionID.EasternPalace].Accessibility);

                            case DungeonItemShuffle.MapsCompasses:
                            case DungeonItemShuffle.MapsCompassesSmallKeys:

                                if (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value)
                                {
                                    if (_game.Items.Has(ItemType.Lamp))
                                        return _game.Regions[RegionID.EasternPalace].Accessibility;

                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                        (byte)_game.Regions[RegionID.EasternPalace].Accessibility);
                                }

                                if (Available > 1)
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                        (byte)_game.Regions[RegionID.EasternPalace].Accessibility);
                                }

                                break;
                            case DungeonItemShuffle.Keysanity:

                                if (_game.Items.Has(ItemType.EPBigKey) &&
                                    (_game.Mode.EnemyShuffle.Value || _game.Items.Has(ItemType.Bow)))
                                {
                                    if (_game.Items.Has(ItemType.Lamp))
                                        return _game.Regions[RegionID.EasternPalace].Accessibility;

                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                        (byte)_game.Regions[RegionID.EasternPalace].Accessibility);
                                }

                                if (Available > 1 && _game.Items.Has(ItemType.EPBigKey))
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                        (byte)_game.Regions[RegionID.EasternPalace].Accessibility);
                                }

                                if (Available > 2)
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                        (byte)_game.Regions[RegionID.EasternPalace].Accessibility);
                                }

                                break;
                        }
                        
                        return AccessibilityLevel.None;
                    };

                    _updateOnDungeonItemShuffleChange = true;
                    _updateOnEnemyShuffleChange = true;

                    _regionSubscriptions.Add(RegionID.EasternPalace, new Mode());

                    _itemSubscriptions.Add(ItemType.Lamp, new Mode());
                    _itemSubscriptions.Add(ItemType.Bow, new Mode() { EnemyShuffle = false });
                    _itemSubscriptions.Add(ItemType.EPBigKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity });

                    PropertyChanged += OnRequirementChanged;

                    break;
                case LocationID.DesertPalace:

                    _mapCompass = 2;
                    _smallKey = 1;
                    _bigKey = 1;
                    _baseTotal = 2;
                    Name = "Dungeon";
                    HasMarking = true;

                    GetAccessibility = () =>
                    {
                        switch (_game.Mode.DungeonItemShuffle.Value)
                        {
                            case DungeonItemShuffle.Standard:

                                if (_game.Items.Has(ItemType.Boots) &&
                                    (_game.Items.Has(ItemType.Gloves) || _game.Mode.EntranceShuffle.Value) &&
                                    (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)))
                                    return _game.Regions[RegionID.DesertPalace].Accessibility;

                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                    (byte)_game.Regions[RegionID.DesertPalace].Accessibility);

                            case DungeonItemShuffle.MapsCompasses:

                                if (_game.Items.Has(ItemType.Boots) &&
                                    (_game.Items.Has(ItemType.Gloves) || _game.Mode.EntranceShuffle.Value) &&
                                    (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)))
                                    return _game.Regions[RegionID.DesertPalace].Accessibility;

                                if ((_game.Items.Has(ItemType.Gloves) || _game.Mode.EntranceShuffle.Value) &&
                                    (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)))
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                        (byte)_game.Regions[RegionID.DesertPalace].Accessibility);
                                }

                                if (Available > 1)
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                        (byte)_game.Regions[RegionID.DesertPalace].Accessibility);
                                }

                                break;
                            case DungeonItemShuffle.MapsCompassesSmallKeys:

                                if (_game.Items.Has(ItemType.Boots) && _game.Items.Has(ItemType.DPSmallKey) &&
                                    (_game.Items.Has(ItemType.Gloves) || _game.Mode.EntranceShuffle.Value) &&
                                    (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)))
                                    return _game.Regions[RegionID.DesertPalace].Accessibility;

                                if (_game.Items.Has(ItemType.DPSmallKey) &&
                                    (_game.Items.Has(ItemType.Gloves) || _game.Mode.EntranceShuffle.Value) &&
                                    (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)))
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                        (byte)_game.Regions[RegionID.DesertPalace].Accessibility);
                                }

                                if (Available > 1 && _game.Items.Has(ItemType.DPSmallKey))
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                        (byte)_game.Regions[RegionID.DesertPalace].Accessibility);
                                }

                                if (Available > 2 && 
                                    (_game.Items.Has(ItemType.Gloves) || _game.Mode.EntranceShuffle.Value) &&
                                    (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)))
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                        (byte)_game.Regions[RegionID.DesertPalace].Accessibility);
                                }

                                if (Available > 3)
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                        (byte)_game.Regions[RegionID.DesertPalace].Accessibility);
                                }

                                break;
                            case DungeonItemShuffle.Keysanity:

                                if (_game.Items.Has(ItemType.Boots) && _game.Items.Has(ItemType.DPBigKey) &&
                                    _game.Items.Has(ItemType.DPSmallKey) &&
                                    (_game.Items.Has(ItemType.Gloves) || _game.Mode.EntranceShuffle.Value) &&
                                    (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)))
                                    return _game.Regions[RegionID.DesertPalace].Accessibility;

                                if (_game.Items.Has(ItemType.DPBigKey) && _game.Items.Has(ItemType.DPSmallKey) &&
                                    (_game.Items.Has(ItemType.Gloves) || _game.Mode.EntranceShuffle.Value) &&
                                    (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)))
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                        (byte)_game.Regions[RegionID.DesertPalace].Accessibility);
                                }

                                if (_game.Items.Has(ItemType.DPBigKey) && _game.Items.Has(ItemType.DPSmallKey))
                                {
                                    if (Available > 1)
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.DesertPalace].Accessibility);
                                    }
                                }

                                if (_game.Items.Has(ItemType.DPBigKey) &&
                                    (_game.Items.Has(ItemType.Gloves) || _game.Mode.EntranceShuffle.Value) &&
                                    (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)))
                                {
                                    if (Available > 2)
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.DesertPalace].Accessibility);
                                    }
                                }

                                if (_game.Items.Has(ItemType.DPSmallKey))
                                {
                                    if (Available > 2)
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.DesertPalace].Accessibility);
                                    }
                                }

                                if (_game.Items.Has(ItemType.DPBigKey))
                                {
                                    if (Available > 3)
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.DesertPalace].Accessibility);
                                    }
                                }

                                if (Available > 4)
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                        (byte)_game.Regions[RegionID.DesertPalace].Accessibility);
                                }

                                break;
                        }
                        
                        return AccessibilityLevel.None;
                    };

                    _updateOnDungeonItemShuffleChange = true;

                    _regionSubscriptions.Add(RegionID.DesertPalace, new Mode());

                    _itemSubscriptions.Add(ItemType.Boots, new Mode());
                    _itemSubscriptions.Add(ItemType.Gloves, new Mode() { EntranceShuffle = false });
                    _itemSubscriptions.Add(ItemType.Lamp, new Mode());
                    _itemSubscriptions.Add(ItemType.FireRod, new Mode());
                    _itemSubscriptions.Add(ItemType.DPBigKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity });
                    _itemSubscriptions.Add(ItemType.DPSmallKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys });

                    PropertyChanged += OnRequirementChanged;

                    break;
                case LocationID.TowerOfHera:

                    _mapCompass = 2;
                    _smallKey = 1;
                    _bigKey = 1;
                    _baseTotal = 2;
                    Name = "Dungeon";

                    GetAccessibility = () =>
                    {
                        switch (_game.Mode.DungeonItemShuffle.Value)
                        {
                            case DungeonItemShuffle.Standard:
                            case DungeonItemShuffle.MapsCompasses:

                                if (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod))
                                    return _game.Regions[RegionID.TowerOfHera].Accessibility;

                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                    (byte)_game.Regions[RegionID.TowerOfHera].Accessibility);

                            case DungeonItemShuffle.MapsCompassesSmallKeys:

                                if ((_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)) &&
                                    _game.Items.Has(ItemType.ToHSmallKey))
                                    return _game.Regions[RegionID.TowerOfHera].Accessibility;
                                
                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                    (byte)_game.Regions[RegionID.TowerOfHera].Accessibility);

                            case DungeonItemShuffle.Keysanity:

                                if ((_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)) &&
                                    _game.Items.Has(ItemType.ToHSmallKey) && _game.Items.Has(ItemType.ToHBigKey))
                                    return _game.Regions[RegionID.TowerOfHera].Accessibility;

                                if ((_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)) &&
                                    _game.Items.Has(ItemType.ToHSmallKey))
                                {
                                    if (Available > 1 && _game.Items.Has(ItemType.Hookshot))
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.TowerOfHera].Accessibility);
                                    }

                                    if (Available > 3)
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.TowerOfHera].Accessibility);
                                    }
                                }

                                if (Available > 1 && _game.Items.Has(ItemType.ToHBigKey))
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                        (byte)_game.Regions[RegionID.TowerOfHera].Accessibility);
                                }

                                if (Available > 2 && _game.Items.Has(ItemType.Hookshot))
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                        (byte)_game.Regions[RegionID.TowerOfHera].Accessibility);
                                }

                                if (Available > 4)
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                        (byte)_game.Regions[RegionID.TowerOfHera].Accessibility);
                                }

                                break;
                        }

                        return AccessibilityLevel.None;
                    };

                    _updateOnDungeonItemShuffleChange = true;

                    _regionSubscriptions.Add(RegionID.TowerOfHera, new Mode());

                    _itemSubscriptions.Add(ItemType.Lamp, new Mode());
                    _itemSubscriptions.Add(ItemType.FireRod, new Mode());
                    _itemSubscriptions.Add(ItemType.ToHSmallKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys });
                    _itemSubscriptions.Add(ItemType.ToHBigKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity });
                    _itemSubscriptions.Add(ItemType.Hookshot, new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity });

                    PropertyChanged += OnRequirementChanged;

                    break;
                case LocationID.PalaceOfDarkness:

                    _mapCompass = 2;
                    _smallKey = 6;
                    _bigKey = 1;
                    _baseTotal = 5;
                    Name = "Dungeon";

                    GetAccessibility = () =>
                    {
                        switch (_game.Mode.DungeonItemShuffle.Value)
                        {
                            case DungeonItemShuffle.Standard:

                                if (_game.Items.Has(ItemType.Lamp) && _game.Items.Has(ItemType.Hammer) &&
                                    _game.Items.Has(ItemType.Bow))
                                    return _game.Regions[RegionID.PalaceOfDarkness].Accessibility;
                                
                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                    (byte)_game.Regions[RegionID.PalaceOfDarkness].Accessibility);

                            case DungeonItemShuffle.MapsCompasses:

                                if (_game.Items.Has(ItemType.Lamp) && _game.Items.Has(ItemType.Hammer) &&
                                    _game.Items.Has(ItemType.Bow))
                                    return AccessibilityLevel.Normal;

                                if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Bow))
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                        (byte)_game.Regions[RegionID.PalaceOfDarkness].Accessibility);
                                }

                                if (Available > 1)
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                        (byte)_game.Regions[RegionID.PalaceOfDarkness].Accessibility);
                                }

                                break;
                            case DungeonItemShuffle.MapsCompassesSmallKeys:

                                if (_game.Items.Has(ItemType.Lamp) && _game.Items.Has(ItemType.Hammer) &&
                                    _game.Items.Has(ItemType.Bow) && _game.Items.Has(ItemType.PoDSmallKey, 5))
                                    return AccessibilityLevel.Normal;

                                if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Bow) &&
                                    _game.Items.Has(ItemType.PoDSmallKey, 4))
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                        (byte)_game.Regions[RegionID.PalaceOfDarkness].Accessibility);
                                }

                                if (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value ||
                                    _game.Items.Has(ItemType.Bottle))
                                {
                                    if (Available > 1 && (_game.Items.Has(ItemType.PoDSmallKey, 4) ||
                                        (_game.Items.Has(ItemType.PoDSmallKey, 3) && _game.Items.Has(ItemType.Hammer))))
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.PalaceOfDarkness].Accessibility);
                                    }

                                    if (Available > 2 && (_game.Items.Has(ItemType.PoDSmallKey, 3) ||
                                        (_game.Items.Has(ItemType.PoDSmallKey, 2) && _game.Items.Has(ItemType.Hammer))))
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.PalaceOfDarkness].Accessibility);
                                    }

                                    if (Available > 3 && (_game.Items.Has(ItemType.PoDSmallKey, 2) ||
                                        (_game.Items.Has(ItemType.PoDSmallKey) && _game.Items.Has(ItemType.Hammer))))
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.PalaceOfDarkness].Accessibility);
                                    }

                                    if (Available > 8 &&
                                        (_game.Items.Has(ItemType.PoDSmallKey) || _game.Items.Has(ItemType.Hammer)))
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.PalaceOfDarkness].Accessibility);
                                    }

                                    if (Available > 10)
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.PalaceOfDarkness].Accessibility);
                                    }
                                }

                                if (_game.Items.Has(ItemType.PoDSmallKey, 4) && Available > 3)
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                        (byte)_game.Regions[RegionID.PalaceOfDarkness].Accessibility);
                                }

                                if (_game.Items.Has(ItemType.PoDSmallKey, 3) && Available > 4)
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                        (byte)_game.Regions[RegionID.PalaceOfDarkness].Accessibility);
                                }

                                if (_game.Items.Has(ItemType.PoDSmallKey, 2) && Available > 5)
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                        (byte)_game.Regions[RegionID.PalaceOfDarkness].Accessibility);
                                }

                                if (_game.Items.Has(ItemType.PoDSmallKey) && Available > 10)
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                        (byte)_game.Regions[RegionID.PalaceOfDarkness].Accessibility);
                                }

                                if (Available > 12)
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                        (byte)_game.Regions[RegionID.PalaceOfDarkness].Accessibility);
                                }

                                break;
                            case DungeonItemShuffle.Keysanity:

                                if (_game.Items.Has(ItemType.Lamp) && _game.Items.Has(ItemType.Hammer) &&
                                    _game.Items.Has(ItemType.Bow) && _game.Items.Has(ItemType.PoDSmallKey, 5) &&
                                    _game.Items.Has(ItemType.PoDBigKey))
                                    return AccessibilityLevel.Normal;

                                if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Bow) &&
                                    _game.Items.Has(ItemType.PoDSmallKey, 4) && _game.Items.Has(ItemType.PoDBigKey))
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                        (byte)_game.Regions[RegionID.PalaceOfDarkness].Accessibility);
                                }

                                if (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value ||
                                    _game.Items.Has(ItemType.Bottle))
                                {
                                    if (_game.Items.Has(ItemType.PoDSmallKey, 4) ||
                                        (_game.Items.Has(ItemType.PoDSmallKey, 3) && _game.Items.Has(ItemType.Hammer)))
                                    {
                                        if (_game.Items.Has(ItemType.PoDBigKey) && Available > 1)
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.PalaceOfDarkness].Accessibility);
                                        }

                                        if (Available > 2)
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.PalaceOfDarkness].Accessibility);
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.PoDSmallKey, 3) ||
                                        (_game.Items.Has(ItemType.PoDSmallKey, 2) && _game.Items.Has(ItemType.Hammer)))
                                    {
                                        if (_game.Items.Has(ItemType.PoDBigKey) && Available > 2)
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.PalaceOfDarkness].Accessibility);
                                        }

                                        if (Available > 3)
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.PalaceOfDarkness].Accessibility);
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.PoDSmallKey, 2) ||
                                        (_game.Items.Has(ItemType.PoDSmallKey) && _game.Items.Has(ItemType.Hammer)))
                                    {
                                        if (_game.Items.Has(ItemType.PoDBigKey) && Available > 3)
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.PalaceOfDarkness].Accessibility);
                                        }

                                        if (Available > 4)
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.PalaceOfDarkness].Accessibility);
                                        }
                                    }

                                    if (Available > 9 &&
                                        (_game.Items.Has(ItemType.PoDSmallKey) || _game.Items.Has(ItemType.Hammer)))
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.PalaceOfDarkness].Accessibility);
                                    }

                                    if (Available > 11)
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.PalaceOfDarkness].Accessibility);
                                    }
                                }

                                if (_game.Items.Has(ItemType.PoDSmallKey, 4))
                                {
                                    if (_game.Items.Has(ItemType.PoDBigKey) && Available > 3)
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.PalaceOfDarkness].Accessibility);
                                    }

                                    if (Available > 4)
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.PalaceOfDarkness].Accessibility);
                                    }
                                }

                                if (_game.Items.Has(ItemType.PoDSmallKey, 3))
                                {
                                    if (_game.Items.Has(ItemType.PoDBigKey) && Available > 4)
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.PalaceOfDarkness].Accessibility);
                                    }

                                    if (Available > 5)
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.PalaceOfDarkness].Accessibility);
                                    }
                                }

                                if (_game.Items.Has(ItemType.PoDSmallKey, 2))
                                {
                                    if (_game.Items.Has(ItemType.PoDBigKey) && Available > 5)
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.PalaceOfDarkness].Accessibility);
                                    }

                                    if (Available > 6)
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.PalaceOfDarkness].Accessibility);
                                    }
                                }

                                if (_game.Items.Has(ItemType.PoDSmallKey) && Available > 11)
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                        (byte)_game.Regions[RegionID.PalaceOfDarkness].Accessibility);
                                }

                                if (Available > 13)
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                        (byte)_game.Regions[RegionID.PalaceOfDarkness].Accessibility);
                                }

                                break;
                        }

                        return AccessibilityLevel.None;
                    };

                    _updateOnDungeonItemShuffleChange = true;
                    _updateOnEnemyShuffleChange = true;

                    _regionSubscriptions.Add(RegionID.PalaceOfDarkness, new Mode());

                    _itemSubscriptions.Add(ItemType.Lamp, new Mode());
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.Bow, new Mode());
                    _itemSubscriptions.Add(ItemType.Bottle, new Mode());
                    _itemSubscriptions.Add(ItemType.PoDBigKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity });
                    _itemSubscriptions.Add(ItemType.PoDSmallKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys });

                    PropertyChanged += OnRequirementChanged;

                    break;
                case LocationID.SwampPalace:

                    _mapCompass = 2;
                    _smallKey = 1;
                    _bigKey = 1;
                    _baseTotal = 6;
                    Name = "Dungeon";

                    GetAccessibility = () =>
                    {
                        switch (_game.Mode.DungeonItemShuffle.Value)
                        {
                            case DungeonItemShuffle.Standard:

                                if (_game.Items.Has(ItemType.Flippers))
                                {
                                    if (_game.Items.Has(ItemType.Hammer))
                                    {
                                        if (_game.Items.Has(ItemType.Hookshot))
                                            return _game.Regions[RegionID.SwampPalace].Accessibility;

                                        if (Available > 2)
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.SwampPalace].Accessibility);
                                        }
                                    }

                                    if (Available > 5)
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.SwampPalace].Accessibility);
                                    }
                                }

                                break;
                            case DungeonItemShuffle.MapsCompasses:

                                if (_game.Items.Has(ItemType.Flippers))
                                {
                                    if (_game.Items.Has(ItemType.Hammer))
                                    {
                                        if (_game.Items.Has(ItemType.Hookshot))
                                            return _game.Regions[RegionID.SwampPalace].Accessibility;

                                        if (Available > 4)
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.SwampPalace].Accessibility);
                                        }
                                    }

                                    if (Available > 7)
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.SwampPalace].Accessibility);
                                    }
                                }

                                break;
                            case DungeonItemShuffle.MapsCompassesSmallKeys:

                                if (_game.Items.Has(ItemType.Flippers))
                                {
                                    if (_game.Items.Has(ItemType.SPSmallKey))
                                    {
                                        if (_game.Items.Has(ItemType.Hammer))
                                        {
                                            if (_game.Items.Has(ItemType.Hookshot))
                                                return _game.Regions[RegionID.SwampPalace].Accessibility;

                                            if (Available > 4)
                                            {
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                    (byte)_game.Regions[RegionID.SwampPalace].Accessibility);
                                            }
                                        }

                                        if (Available > 7)
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.SwampPalace].Accessibility);
                                        }
                                    }

                                    if (Available > 8)
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.SwampPalace].Accessibility);
                                    }
                                }

                                break;
                            case DungeonItemShuffle.Keysanity:

                                if (_game.Items.Has(ItemType.Flippers))
                                {
                                    if (_game.Items.Has(ItemType.SPSmallKey))
                                    {
                                        if (_game.Items.Has(ItemType.Hammer))
                                        {
                                            if (_game.Items.Has(ItemType.Hookshot) && _game.Items.Has(ItemType.SPBigKey))
                                                return _game.Regions[RegionID.SwampPalace].Accessibility;

                                            if (Available > 1 && _game.Items.Has(ItemType.Hookshot))
                                            {
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                    (byte)_game.Regions[RegionID.SwampPalace].Accessibility);
                                            }

                                            if (Available > 4 && _game.Items.Has(ItemType.SPBigKey))
                                            {
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                    (byte)_game.Regions[RegionID.SwampPalace].Accessibility);
                                            }

                                            if (Available > 5)
                                            {
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                    (byte)_game.Regions[RegionID.SwampPalace].Accessibility);
                                            }
                                        }

                                        if (Available > 8)
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.SwampPalace].Accessibility);
                                        }
                                    }

                                    if (Available > 9)
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.SwampPalace].Accessibility);
                                    }
                                }

                                break;
                        }

                        return AccessibilityLevel.None;
                    };

                    _updateOnDungeonItemShuffleChange = true;

                    _regionSubscriptions.Add(RegionID.SwampPalace, new Mode());

                    _itemSubscriptions.Add(ItemType.Flippers, new Mode());
                    _itemSubscriptions.Add(ItemType.Hookshot, new Mode());
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.SPSmallKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys });
                    _itemSubscriptions.Add(ItemType.SPBigKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity });

                    PropertyChanged += OnRequirementChanged;

                    break;
                case LocationID.SkullWoods:

                    _mapCompass = 2;
                    _smallKey = 3;
                    _bigKey = 1;
                    _baseTotal = 2;
                    Name = "Dungeon";

                    GetAccessibility = () =>
                    {
                        switch (_game.Mode.DungeonItemShuffle.Value)
                        {
                            case DungeonItemShuffle.Standard:
                            case DungeonItemShuffle.MapsCompasses:

                                if ((_game.Items.Has(ItemType.FireRod) || _game.Mode.EntranceShuffle.Value) &&
                                    _game.Items.CanRemoveCurtains())
                                    return _game.Regions[RegionID.SkullWoods].Accessibility;

                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                    (byte)_game.Regions[RegionID.SkullWoods].Accessibility);

                            case DungeonItemShuffle.MapsCompassesSmallKeys:

                                if (_game.Items.Has(ItemType.FireRod) || _game.Mode.EntranceShuffle.Value)
                                {
                                    if (_game.Items.CanRemoveCurtains())
                                    {
                                        if (_game.Items.Has(ItemType.SWSmallKey))
                                            return _game.Regions[RegionID.SkullWoods].Accessibility;

                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                            (byte)_game.Regions[RegionID.SkullWoods].Accessibility);
                                    }

                                    if (Available > 1)
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.SkullWoods].Accessibility);
                                    }
                                }

                                if (Available > 2)
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                        (byte)_game.Regions[RegionID.SkullWoods].Accessibility);
                                }

                                break;
                            case DungeonItemShuffle.Keysanity:

                                if (_game.Items.Has(ItemType.FireRod) || _game.Mode.EntranceShuffle.Value)
                                {
                                    if (_game.Items.CanRemoveCurtains())
                                    {
                                        if (_game.Items.Has(ItemType.SWBigKey) && _game.Items.Has(ItemType.SPSmallKey))
                                            return _game.Regions[RegionID.SkullWoods].Accessibility;

                                        if (_game.Items.Has(ItemType.SWBigKey))
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                                (byte)_game.Regions[RegionID.SkullWoods].Accessibility);
                                        }

                                        if (Available > 1)
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.SkullWoods].Accessibility);
                                        }
                                    }

                                    if (Available > 1 && _game.Items.Has(ItemType.SWBigKey))
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.SkullWoods].Accessibility);
                                    }

                                    if (Available > 2)
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.SkullWoods].Accessibility);
                                    }
                                }

                                if (Available > 2 && _game.Items.Has(ItemType.SWBigKey))
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                        (byte)_game.Regions[RegionID.SkullWoods].Accessibility);
                                }

                                if (Available > 3)
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                        (byte)_game.Regions[RegionID.SkullWoods].Accessibility);
                                }

                                break;
                        }
                        
                        return AccessibilityLevel.None;
                    };

                    _updateOnDungeonItemShuffleChange = true;

                    _regionSubscriptions.Add(RegionID.SkullWoods, new Mode());

                    _itemSubscriptions.Add(ItemType.FireRod, new Mode());
                    _itemSubscriptions.Add(ItemType.Sword, new Mode());
                    _itemSubscriptions.Add(ItemType.SWBigKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity });
                    _itemSubscriptions.Add(ItemType.SWSmallKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys });

                    PropertyChanged += OnRequirementChanged;

                    break;
                case LocationID.ThievesTown:

                    _mapCompass = 2;
                    _smallKey = 1;
                    _bigKey = 1;
                    _baseTotal = 4;
                    Name = "Dungeon";

                    GetAccessibility = () =>
                    {
                        switch (_game.Mode.DungeonItemShuffle.Value)
                        {
                            case DungeonItemShuffle.Standard:
                            case DungeonItemShuffle.MapsCompasses:

                                if (_game.Items.Has(ItemType.Hammer))
                                    return _game.Regions[RegionID.ThievesTown].Accessibility;

                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                    (byte)_game.Regions[RegionID.ThievesTown].Accessibility);

                            case DungeonItemShuffle.MapsCompassesSmallKeys:

                                if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.TTSmallKey))
                                    return _game.Regions[RegionID.ThievesTown].Accessibility;

                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                    (byte)_game.Regions[RegionID.ThievesTown].Accessibility);

                            case DungeonItemShuffle.Keysanity:

                                if (_game.Items.Has(ItemType.TTBigKey))
                                {
                                    if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.TTSmallKey))
                                        return _game.Regions[RegionID.ThievesTown].Accessibility;

                                    if (Available > 1)
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.ThievesTown].Accessibility);
                                    }
                                }

                                if (Available > 4)
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                        (byte)_game.Regions[RegionID.ThievesTown].Accessibility);
                                }

                                break;
                        }
                        
                        return AccessibilityLevel.None;
                    };

                    _updateOnDungeonItemShuffleChange = true;

                    _regionSubscriptions.Add(RegionID.ThievesTown, new Mode());

                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.TTBigKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity });
                    _itemSubscriptions.Add(ItemType.TTSmallKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys });

                    PropertyChanged += OnRequirementChanged;

                    break;
                case LocationID.IcePalace:

                    _mapCompass = 2;
                    _smallKey = 2;
                    _bigKey = 1;
                    _baseTotal = 3;
                    Name = "Dungeon";

                    GetAccessibility = () =>
                    {
                        switch (_game.Mode.DungeonItemShuffle.Value)
                        {
                            case DungeonItemShuffle.Standard:

                                if (_game.Items.CanMeltThings())
                                {
                                    if (_game.Items.Has(ItemType.Hammer) &&
                                        (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.CaneOfSomaria)))
                                        return _game.Regions[RegionID.IcePalace].Accessibility;

                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                        (byte)_game.Regions[RegionID.IcePalace].Accessibility);
                                }

                                break;
                            case DungeonItemShuffle.MapsCompasses:

                                if (_game.Items.CanMeltThings())
                                {
                                    if (_game.Items.Has(ItemType.Hammer))
                                    {
                                        if (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.CaneOfSomaria))
                                            return _game.Regions[RegionID.IcePalace].Accessibility;

                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                            (byte)_game.Regions[RegionID.IcePalace].Accessibility);
                                    }

                                    if (Available > 1)
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.IcePalace].Accessibility);
                                    }
                                }

                                break;
                            case DungeonItemShuffle.MapsCompassesSmallKeys:

                                if (_game.Items.CanMeltThings())
                                {
                                    if (_game.Items.Has(ItemType.Hammer))
                                    {
                                        if ((_game.Items.Has(ItemType.Hookshot) && _game.Items.Has(ItemType.CaneOfSomaria)) ||
                                            (_game.Items.Has(ItemType.Hookshot) && _game.Items.Has(ItemType.IPSmallKey)) ||
                                            (_game.Items.Has(ItemType.CaneOfSomaria) && _game.Items.Has(ItemType.IPSmallKey)) ||
                                            _game.Items.Has(ItemType.IPSmallKey, 2))
                                            return _game.Regions[RegionID.IcePalace].Accessibility;

                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                            (byte)_game.Regions[RegionID.IcePalace].Accessibility);
                                    }

                                    if (Available > 3)
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.IcePalace].Accessibility);
                                    }
                                }

                                break;
                            case DungeonItemShuffle.Keysanity:

                                if (_game.Items.CanMeltThings())
                                {
                                    if (_game.Items.Has(ItemType.Hammer))
                                    {
                                        if (((_game.Items.Has(ItemType.Hookshot) && _game.Items.Has(ItemType.CaneOfSomaria)) ||
                                            (_game.Items.Has(ItemType.Hookshot) && _game.Items.Has(ItemType.IPSmallKey)) ||
                                            (_game.Items.Has(ItemType.CaneOfSomaria) && _game.Items.Has(ItemType.IPSmallKey)) ||
                                            _game.Items.Has(ItemType.IPSmallKey, 2)) && _game.Items.Has(ItemType.IPBigKey))
                                            return _game.Regions[RegionID.IcePalace].Accessibility;

                                        if (_game.Items.Has(ItemType.IPBigKey))
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                                (byte)_game.Regions[RegionID.IcePalace].Accessibility);
                                        }

                                        if (Available > 1)
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.IcePalace].Accessibility);
                                        }
                                    }

                                    if (Available > 3 && _game.Items.Has(ItemType.IPBigKey))
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.IcePalace].Accessibility);
                                    }

                                    if (Available > 4)
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.IcePalace].Accessibility);
                                    }
                                }

                                break;
                        }

                        return AccessibilityLevel.None;
                    };

                    _updateOnDungeonItemShuffleChange = true;

                    _regionSubscriptions.Add(RegionID.IcePalace, new Mode());

                    _itemSubscriptions.Add(ItemType.FireRod, new Mode());
                    _itemSubscriptions.Add(ItemType.Bombos, new Mode());
                    _itemSubscriptions.Add(ItemType.Sword, new Mode());
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.Hookshot, new Mode());
                    _itemSubscriptions.Add(ItemType.CaneOfSomaria, new Mode());
                    _itemSubscriptions.Add(ItemType.IPSmallKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys });
                    _itemSubscriptions.Add(ItemType.IPBigKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity });

                    PropertyChanged += OnRequirementChanged;

                    break;
                case LocationID.MiseryMire:

                    _mapCompass = 2;
                    _smallKey = 3;
                    _bigKey = 1;
                    _baseTotal = 2;
                    Name = "Dungeon";

                    GetAccessibility = () =>
                    {
                        switch (_game.Mode.DungeonItemShuffle.Value)
                        {
                            case DungeonItemShuffle.Standard:

                                if (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots))
                                {
                                    if (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod))
                                    {
                                        if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        {
                                            if ((_game.Items.Has(ItemType.Hookshot) ||
                                                (_game.Items.Has(ItemType.Boots) &&
                                                _game.Mode.ItemPlacement == ItemPlacement.Advanced)) &&
                                                _game.Items.Has(ItemType.Lamp))
                                                return _game.Regions[RegionID.MiseryMire].Accessibility;
                                        }
                                    }

                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                        (byte)_game.Regions[RegionID.MiseryMire].Accessibility);
                                }

                                break;
                            case DungeonItemShuffle.MapsCompasses:

                                if (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots))
                                {
                                    if (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod))
                                    {
                                        if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        {
                                            if ((_game.Items.Has(ItemType.Hookshot) ||
                                                (_game.Items.Has(ItemType.Boots) &&
                                                _game.Mode.ItemPlacement == ItemPlacement.Advanced)) &&
                                                _game.Items.Has(ItemType.Lamp))
                                                return _game.Regions[RegionID.MiseryMire].Accessibility;

                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                                (byte)_game.Regions[RegionID.MiseryMire].Accessibility);
                                        }

                                        if (Available > 1)
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.MiseryMire].Accessibility);
                                        }
                                    }

                                    if (Available > 3)
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.MiseryMire].Accessibility);
                                    }
                                }

                                break;
                            case DungeonItemShuffle.MapsCompassesSmallKeys:

                                if (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots))
                                {
                                    if (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod))
                                    {
                                        if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        {
                                            if ((_game.Items.Has(ItemType.Hookshot) ||
                                                (_game.Items.Has(ItemType.Boots) && _game.Mode.ItemPlacement == ItemPlacement.Advanced)) &&
                                                _game.Items.Has(ItemType.Lamp))
                                                return _game.Regions[RegionID.MiseryMire].Accessibility;

                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                                (byte)_game.Regions[RegionID.MiseryMire].Accessibility);
                                        }

                                        if (Available > 1)
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.MiseryMire].Accessibility);
                                        }
                                    }

                                    if (Available > 2 && _game.Items.Has(ItemType.CaneOfSomaria))
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.MiseryMire].Accessibility);
                                    }

                                    if (Available > 3)
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.MiseryMire].Accessibility);
                                    }
                                }

                                break;
                            case DungeonItemShuffle.Keysanity:

                                if (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots))
                                {
                                    if (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod))
                                    {
                                        if (_game.Items.Has(ItemType.CaneOfSomaria) && _game.Items.Has(ItemType.MMBigKey))
                                        {
                                            if ((_game.Items.Has(ItemType.Hookshot) ||
                                                (_game.Items.Has(ItemType.Boots) && _game.Mode.ItemPlacement == ItemPlacement.Advanced)) &&
                                                _game.Items.Has(ItemType.Lamp))
                                                return _game.Regions[RegionID.MiseryMire].Accessibility;

                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                                (byte)_game.Regions[RegionID.MiseryMire].Accessibility);
                                        }

                                        if (Available > 1 && _game.Items.Has(ItemType.MMBigKey))
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.MiseryMire].Accessibility);
                                        }

                                        if (Available > 2)
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.MiseryMire].Accessibility);
                                        }
                                    }

                                    if (Available > 2 &&
                                        _game.Items.Has(ItemType.CaneOfSomaria) && _game.Items.Has(ItemType.MMBigKey))
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.MiseryMire].Accessibility);
                                    }

                                    if (Available > 3 && _game.Items.Has(ItemType.MMBigKey))
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.MiseryMire].Accessibility);
                                    }

                                    if (Available > 4)
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.MiseryMire].Accessibility);
                                    }
                                }

                                break;
                        }

                        return AccessibilityLevel.None;
                    };

                    _updateOnItemPlacementChange = true;
                    _updateOnDungeonItemShuffleChange = true;

                    _regionSubscriptions.Add(RegionID.MiseryMire, new Mode());

                    _itemSubscriptions.Add(ItemType.Hookshot, new Mode());
                    _itemSubscriptions.Add(ItemType.Boots, new Mode());
                    _itemSubscriptions.Add(ItemType.Lamp, new Mode());
                    _itemSubscriptions.Add(ItemType.FireRod, new Mode());
                    _itemSubscriptions.Add(ItemType.CaneOfSomaria, new Mode());
                    _itemSubscriptions.Add(ItemType.MMBigKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity });

                    PropertyChanged += OnRequirementChanged;

                    break;
                case LocationID.TurtleRock:

                    _mapCompass = 2;
                    _smallKey = 4;
                    _bigKey = 1;
                    _baseTotal = 5;
                    Name = "Dungeon";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel frontAccess()
                        {
                            if (_game.Mode.EntranceShuffle.Value)
                                return AccessibilityLevel.Normal;

                            if (_game.Mode.WorldState == WorldState.StandardOpen)
                            {
                                if (_game.Items.Has(ItemType.Hammer) &&
                                    _game.Items.CanUseMedallions() && ((_game.Items.Has(ItemType.Bombos) &&
                                    _game.Items.Has(ItemType.Ether) && _game.Items.Has(ItemType.Quake)) ||
                                    (_game.Items.Has(ItemType.Bombos) &&
                                    _game.Items[ItemType.BombosDungeons].Current >= 2) ||
                                    (_game.Items.Has(ItemType.Ether) &&
                                    _game.Items[ItemType.EtherDungeons].Current >= 2) ||
                                    (_game.Items.Has(ItemType.Quake) &&
                                    _game.Items[ItemType.QuakeDungeons].Current >= 2)))
                                    return _game.Regions[RegionID.DarkDeathMountainTop].Accessibility;
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
                        }

                        AccessibilityLevel backAccess()
                        {
                            if (_game.Mode.EntranceShuffle.Value)
                                return AccessibilityLevel.Normal;

                            if (_game.Mode.WorldState == WorldState.Inverted)
                            {
                                if (_game.Regions[RegionID.DeathMountainEastTop].Accessibility >= AccessibilityLevel.SequenceBreak &&
                                    _game.Items.Has(ItemType.Mirror))
                                    return _game.Regions[RegionID.DeathMountainEastTop].Accessibility;
                            }

                            return AccessibilityLevel.None;
                        }

                        AccessibilityLevel back = AccessibilityLevel.None;
                        AccessibilityLevel backFront = AccessibilityLevel.None;
                        AccessibilityLevel front = AccessibilityLevel.None;

                        switch (_game.Mode.DungeonItemShuffle.Value)
                        {
                            case DungeonItemShuffle.Standard:

                                if (_game.Items.Has(ItemType.CaneOfSomaria) && _game.Items.Has(ItemType.FireRod) &&
                                    _game.Items.Has(ItemType.Lamp) &&
                                    (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                    _game.Items.Has(ItemType.Cape) || _game.Items.Has(ItemType.CaneOfByrna) ||
                                    _game.Items.Has(ItemType.Shield, 3)))
                                    return _game.Regions[RegionID.TurtleRock].Accessibility;

                                back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                    (byte)backAccess());

                                if (_game.Items.Has(ItemType.CaneOfSomaria))
                                {
                                    front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                        (byte)frontAccess());
                                }

                                return (AccessibilityLevel)Math.Max((byte)back, (byte)front);

                            case DungeonItemShuffle.MapsCompasses:

                                if (_game.Items.Has(ItemType.CaneOfSomaria) && _game.Items.Has(ItemType.FireRod) &&
                                    _game.Items.Has(ItemType.Lamp) &&
                                    (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                    _game.Items.Has(ItemType.Cape) || _game.Items.Has(ItemType.CaneOfByrna) ||
                                    _game.Items.Has(ItemType.Shield, 3)))
                                    return _game.Regions[RegionID.TurtleRock].Accessibility;

                                if (Available > 1)
                                {
                                    back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                        (byte)backAccess());
                                }

                                if (_game.Items.Has(ItemType.CaneOfSomaria))
                                {
                                    back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                        (byte)backAccess());

                                    if (Available > 2)
                                    {
                                        front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)frontAccess());
                                    }

                                    if (_game.Items.Has(ItemType.FireRod))
                                    {
                                        front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                            (byte)frontAccess());
                                    }

                                }

                                return (AccessibilityLevel)Math.Max((byte)back, (byte)front);

                            case DungeonItemShuffle.MapsCompassesSmallKeys:

                                if (Available > 5)
                                {
                                    back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                        (byte)backAccess());
                                }

                                if (_game.Items.Has(ItemType.Hookshot))
                                {
                                    if (Available > 4)
                                    {
                                        back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)backAccess());
                                    }

                                    if (_game.Items.Has(ItemType.TRSmallKey))
                                    {
                                        if (Available > 3)
                                        {
                                            back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)backAccess());
                                        }
                                    }
                                }

                                if (_game.Items.Has(ItemType.CaneOfSomaria))
                                {
                                    if (Available > 10)
                                    {
                                        front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)frontAccess());
                                    }

                                    if (Available > 4)
                                    {
                                        back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)backAccess());
                                    }

                                    if (Available > 3)
                                    {
                                        backFront = (AccessibilityLevel)Math.Min(Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)backAccess()), (byte)frontAccess());
                                    }

                                    if (_game.Items.Has(ItemType.FireRod))
                                    {
                                        if (Available > 8)
                                        {
                                            front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)frontAccess());
                                        }

                                        if (Available > 1)
                                        {
                                            backFront = (AccessibilityLevel)Math.Min(Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)backAccess()), (byte)frontAccess());
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.TRSmallKey))
                                    {
                                        if (Available > 9)
                                        {
                                            front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)frontAccess());
                                        }

                                        if (Available > 3)
                                        {
                                            back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)backAccess());
                                        }

                                        if (Available > 2)
                                        {
                                            backFront = (AccessibilityLevel)Math.Min(Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)backAccess()), (byte)frontAccess());
                                        }

                                        if (_game.Items.Has(ItemType.FireRod))
                                        {
                                            if (Available > 7)
                                            {
                                                front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                    (byte)frontAccess());
                                            }

                                            if (Available > 1)
                                            {
                                                back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                    (byte)backAccess());
                                            }
                                            
                                            backFront = (AccessibilityLevel)Math.Min(Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                                (byte)backAccess()), (byte)frontAccess());

                                            if (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                                _game.Items.Has(ItemType.Cape) || _game.Items.Has(ItemType.CaneOfByrna) ||
                                                _game.Items.Has(ItemType.Shield, 3))
                                                backFront = (AccessibilityLevel)Math.Min((byte)backAccess(), (byte)frontAccess());
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.TRSmallKey, 2))
                                    {
                                        if (Available > 7)
                                        {
                                            front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)frontAccess());
                                        }

                                        if (Available > 2)
                                        {
                                            back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)backAccess());
                                        }

                                        if (_game.Items.Has(ItemType.FireRod))
                                        {
                                            if (Available > 5)
                                            {
                                                front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                    (byte)frontAccess());
                                            }

                                            back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                                (byte)backAccess());

                                            if (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                                _game.Items.Has(ItemType.Cape) || _game.Items.Has(ItemType.CaneOfByrna) ||
                                                _game.Items.Has(ItemType.Shield, 3))
                                                back = backAccess();
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.TRSmallKey, 3))
                                    {
                                        if (Available > 3)
                                        {
                                            front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)frontAccess());
                                        }

                                        if (_game.Items.Has(ItemType.FireRod))
                                        {
                                            if (Available > 1)
                                            {
                                                front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                    (byte)frontAccess());
                                            }
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.TRSmallKey, 4))
                                    {
                                        if (Available > 2)
                                        {
                                            front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)frontAccess());
                                        }

                                        if (_game.Items.Has(ItemType.FireRod))
                                        {
                                            front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                                (byte)frontAccess());

                                            if ((_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                                _game.Items.Has(ItemType.Cape) || _game.Items.Has(ItemType.CaneOfByrna) ||
                                                _game.Items.Has(ItemType.Shield, 3)) && _game.Items.Has(ItemType.Lamp))
                                                front = frontAccess();
                                        }
                                    }
                                }

                                return (AccessibilityLevel)Math.Max(Math.Max((byte)back, (byte)front), (byte)backFront);

                           case DungeonItemShuffle.Keysanity:

                                if (Available > 6)
                                {
                                    back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                        (byte)backAccess());
                                }

                                if (_game.Items.Has(ItemType.TRBigKey))
                                {
                                    if (Available > 5)
                                    {
                                        back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)backAccess());
                                    }

                                    if (_game.Items.Has(ItemType.Hookshot))
                                    {
                                        if (Available > 4)
                                        {
                                            back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)backAccess());
                                        }

                                        if (_game.Items.Has(ItemType.TRSmallKey))
                                        {
                                            if (Available > 3)
                                            {
                                                back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                    (byte)backAccess());
                                            }
                                        }
                                    }
                                }

                                if (_game.Items.Has(ItemType.CaneOfSomaria))
                                {
                                    if (Available > 11)
                                    {
                                        front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)frontAccess());
                                    }

                                    if (Available > 5)
                                    {
                                        back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)backAccess());
                                    }

                                    if (Available > 4)
                                    {
                                        backFront = (AccessibilityLevel)Math.Min(Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)backAccess()), (byte)frontAccess());
                                    }

                                    if (_game.Items.Has(ItemType.TRBigKey))
                                    {
                                        if (Available > 4)
                                        {
                                            back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)backAccess());
                                        }

                                        if (Available > 3)
                                        {
                                            backFront = (AccessibilityLevel)Math.Min(Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)backAccess()), (byte)frontAccess());
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.FireRod))
                                    {
                                        if (Available > 9)
                                        {
                                            front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)frontAccess());
                                        }

                                        if (Available > 2)
                                        {
                                            backFront = (AccessibilityLevel)Math.Min(Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)backAccess()), (byte)frontAccess());
                                        }

                                        if (_game.Items.Has(ItemType.TRBigKey))
                                        {
                                            if (Available > 1)
                                            {
                                                backFront = (AccessibilityLevel)Math.Min(Math.Min((byte)AccessibilityLevel.Partial,
                                                    (byte)backAccess()), (byte)frontAccess());
                                            }
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.TRSmallKey))
                                    {
                                        if (Available > 10)
                                        {
                                            front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)frontAccess());
                                        }

                                        if (Available > 4)
                                        {
                                            back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)backAccess());
                                        }

                                        if (_game.Items.Has(ItemType.TRBigKey))
                                        {
                                            if (Available > 3)
                                            {
                                                back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                    (byte)backAccess());
                                            }

                                            if (Available > 2)
                                            {
                                                backFront = (AccessibilityLevel)Math.Min(Math.Min((byte)AccessibilityLevel.Partial,
                                                    (byte)backAccess()), (byte)frontAccess());
                                            }
                                        }

                                        if (_game.Items.Has(ItemType.FireRod))
                                        {
                                            if (Available > 8)
                                            {
                                                front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                    (byte)frontAccess());
                                            }

                                            if (Available > 2)
                                            {
                                                back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                    (byte)backAccess());
                                            }

                                            if (_game.Items.Has(ItemType.TRBigKey))
                                            {
                                                if (Available > 1)
                                                {
                                                    back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                        (byte)backAccess());
                                                }

                                                backFront = (AccessibilityLevel)Math.Min(Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                                    (byte)backAccess()), (byte)frontAccess());

                                                if (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                                    _game.Items.Has(ItemType.Cape) || _game.Items.Has(ItemType.CaneOfByrna) ||
                                                    _game.Items.Has(ItemType.Shield, 3))
                                                    backFront = (AccessibilityLevel)Math.Min((byte)backAccess(), (byte)frontAccess());
                                            }
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.TRSmallKey, 2))
                                    {
                                        if (Available > 9)
                                        {
                                            front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)frontAccess());
                                        }

                                        if (_game.Items.Has(ItemType.TRBigKey))
                                        {
                                            if (Available > 7)
                                            {
                                                front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                    (byte)frontAccess());
                                            }

                                            if (Available > 2)
                                            {
                                                back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                    (byte)backAccess());
                                            }
                                        }

                                        if (_game.Items.Has(ItemType.FireRod))
                                        {
                                            if (Available > 7)
                                            {
                                                front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                    (byte)frontAccess());
                                            }

                                            if (_game.Items.Has(ItemType.TRBigKey))
                                            {
                                                if (Available > 5)
                                                {
                                                    front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                        (byte)frontAccess());
                                                }
                                                
                                                back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                                    (byte)backAccess());

                                                if (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                                    _game.Items.Has(ItemType.Cape) || _game.Items.Has(ItemType.CaneOfByrna) ||
                                                    _game.Items.Has(ItemType.Shield, 3))
                                                    back = backAccess();
                                            }
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.TRBigKey))
                                    {
                                        if (_game.Items.Has(ItemType.TRSmallKey, 3))
                                        {
                                            if (Available > 3)
                                            {
                                                front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                    (byte)frontAccess());
                                            }

                                            if (_game.Items.Has(ItemType.FireRod))
                                            {
                                                if (Available > 1)
                                                {
                                                    front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                        (byte)frontAccess());
                                                }
                                            }
                                        }

                                        if (_game.Items.Has(ItemType.TRSmallKey, 4))
                                        {
                                            if (Available > 2)
                                            {
                                                front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                    (byte)frontAccess());
                                            }

                                            if (_game.Items.Has(ItemType.FireRod))
                                            {
                                                front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                                    (byte)frontAccess());

                                                if ((_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                                    _game.Items.Has(ItemType.Cape) || _game.Items.Has(ItemType.CaneOfByrna) ||
                                                    _game.Items.Has(ItemType.Shield, 3)) && _game.Items.Has(ItemType.Lamp))
                                                    front = frontAccess();
                                            }
                                        }
                                    }
                                }

                                return (AccessibilityLevel)Math.Max(Math.Max((byte)back, (byte)front), (byte)backFront);

                        }

                        return AccessibilityLevel.None;
                    };

                    _updateOnItemPlacementChange = true;
                    _updateOnDungeonItemShuffleChange = true;

                    _regionSubscriptions.Add(RegionID.TurtleRock, new Mode());
                    _regionSubscriptions.Add(RegionID.DeathMountainEastTop, new Mode());
                    _regionSubscriptions.Add(RegionID.DarkDeathMountainTop, new Mode());

                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.Sword, new Mode());
                    _itemSubscriptions.Add(ItemType.Bombos, new Mode());
                    _itemSubscriptions.Add(ItemType.BombosDungeons, new Mode());
                    _itemSubscriptions.Add(ItemType.Ether, new Mode());
                    _itemSubscriptions.Add(ItemType.EtherDungeons, new Mode());
                    _itemSubscriptions.Add(ItemType.Quake, new Mode());
                    _itemSubscriptions.Add(ItemType.QuakeDungeons, new Mode());
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode());
                    _itemSubscriptions.Add(ItemType.CaneOfSomaria, new Mode());
                    _itemSubscriptions.Add(ItemType.FireRod, new Mode());
                    _itemSubscriptions.Add(ItemType.Lamp, new Mode());
                    _itemSubscriptions.Add(ItemType.Cape, new Mode() { ItemPlacement = ItemPlacement.Basic });
                    _itemSubscriptions.Add(ItemType.CaneOfByrna, new Mode() { ItemPlacement = ItemPlacement.Basic });
                    _itemSubscriptions.Add(ItemType.Shield, new Mode() { ItemPlacement = ItemPlacement.Basic });
                    _itemSubscriptions.Add(ItemType.TRSmallKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys });
                    _itemSubscriptions.Add(ItemType.TRBigKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity });
                    _itemSubscriptions.Add(ItemType.Hookshot, new Mode());

                    PropertyChanged += OnRequirementChanged;

                    break;
                case LocationID.GanonsTower:

                    _mapCompass = 2;
                    _smallKey = 4;
                    _bigKey = 1;
                    _baseTotal = 19;
                    Name = "Dungeon";

                    GetAccessibility = () =>
                    {
                        switch (_game.Mode.DungeonItemShuffle.Value)
                        {
                            case DungeonItemShuffle.Standard:

                                if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Hookshot) &&
                                    _game.Items.Has(ItemType.Boots) && _game.Items.Has(ItemType.CaneOfSomaria) &&
                                    (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value) &&
                                    _game.Items.Has(ItemType.FireRod))
                                    return _game.Regions[RegionID.GanonsTower].Accessibility;

                                if (_game.Items.Has(ItemType.Hammer) &&
                                    (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots)))
                                {
                                    if (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp))
                                    {
                                        if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        {
                                            if (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value)
                                            {
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                                    (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                            }

                                            if (Available > 1)
                                            {
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                    (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                            }
                                        }

                                        if (Available > 2 &&
                                            (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value))
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                        }

                                        if (Available > 5)
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                        }
                                    }

                                    if (Available > 5)
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                    }
                                }

                                if (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp))
                                {
                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                    {
                                        if (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value)
                                        {
                                            if (Available > 4 &&
                                                (_game.Items.Has(ItemType.Boots) || _game.Items.Has(ItemType.Hookshot)))
                                            {
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                    (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                            }

                                            if (Available > 5)
                                            {
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                    (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                            }
                                        }

                                        if (Available > 7 &&
                                            (_game.Items.Has(ItemType.Boots) || _game.Items.Has(ItemType.Hookshot)))
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                        }

                                        if (Available > 8)
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value)
                                    {
                                        if (Available > 13 &&
                                            (_game.Items.Has(ItemType.Boots) || _game.Items.Has(ItemType.Hookshot)))
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                        }

                                        if (Available > 14)
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                        }
                                    }

                                }

                                if (Available > 15 && _game.Items.Has(ItemType.CaneOfSomaria))
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                        (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                }

                                if (Available > 16)
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                        (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                }

                                break;
                            case DungeonItemShuffle.MapsCompasses:

                                if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Hookshot) &&
                                    _game.Items.Has(ItemType.Boots) && _game.Items.Has(ItemType.CaneOfSomaria) &&
                                    (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value) &&
                                    _game.Items.Has(ItemType.FireRod))
                                    return _game.Regions[RegionID.GanonsTower].Accessibility;

                                if (_game.Items.Has(ItemType.Hammer) &&
                                    (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots)))
                                {
                                    if (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp))
                                    {
                                        if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        {
                                            if (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value)
                                            {
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                                    (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                            }

                                            if (Available > 3)
                                            {
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                    (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                            }
                                        }

                                        if (Available > 4 &&
                                            (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value))
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                        }

                                        if (Available > 7)
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                        }
                                    }

                                    if (Available > 7)
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                    }
                                }

                                if (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp))
                                {
                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                    {
                                        if (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value)
                                        {
                                            if (Available > 6 &&
                                                (_game.Items.Has(ItemType.Boots) || _game.Items.Has(ItemType.Hookshot)))
                                            {
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                    (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                            }

                                            if (Available > 7)
                                            {
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                    (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                            }
                                        }

                                        if (Available > 10)
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value)
                                    {
                                        if (Available > 15 &&
                                            (_game.Items.Has(ItemType.Boots) || _game.Items.Has(ItemType.Hookshot)))
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                        }

                                        if (Available > 16)
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                        }
                                    }
                                }

                                if (Available > 17 && _game.Items.Has(ItemType.CaneOfSomaria))
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                        (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                }

                                if (Available > 18)
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                        (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                }

                                break;
                            case DungeonItemShuffle.MapsCompassesSmallKeys:

                                if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Hookshot) &&
                                    _game.Items.Has(ItemType.Boots) && _game.Items.Has(ItemType.CaneOfSomaria) &&
                                    (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value) &&
                                    _game.Items.Has(ItemType.FireRod) && _game.Items.Has(ItemType.GTSmallKey, 2))
                                    return _game.Regions[RegionID.GanonsTower].Accessibility;

                                if (_game.Items.Has(ItemType.Hammer) &&
                                    (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots)))
                                {
                                    if (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp))
                                    {
                                        if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        {
                                            if (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value)
                                            {
                                                if (_game.Items.Has(ItemType.GTSmallKey))
                                                {
                                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                                        (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                                }

                                                if (Available > 1)
                                                {
                                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                        (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                                }
                                            }

                                            if (Available > 4)
                                            {
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                    (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                            }
                                        }

                                        if (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value)
                                        {
                                            if (Available > 5 && _game.Items.Has(ItemType.GTSmallKey, 2))
                                            {
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                    (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                            }

                                            if (Available > 6 && _game.Items.Has(ItemType.GTSmallKey))
                                            {
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                    (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                            }

                                            if (Available > 7)
                                            {
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                    (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                            }
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                    {
                                        if (Available > 8 && _game.Items.Has(ItemType.GTSmallKey))
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                        }

                                        if (Available > 9)
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                        }
                                    }

                                    if (Available > 9 && _game.Items.Has(ItemType.GTSmallKey))
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                    }

                                    if (Available > 10)
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                    }
                                }

                                if (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp))
                                {
                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                    {
                                        if (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value)
                                        {
                                            if (Available > 9 && _game.Items.Has(ItemType.GTSmallKey) &&
                                                (_game.Items.Has(ItemType.Boots) || _game.Items.Has(ItemType.Hookshot)))
                                            {
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                    (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                            }

                                            if (Available > 10)
                                            {
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                    (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                            }
                                        }

                                        if (Available > 13)
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value)
                                    {
                                        if (Available > 19 &&
                                            (_game.Items.Has(ItemType.Boots) || _game.Items.Has(ItemType.Hookshot)))
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                        }

                                        if (Available > 20)
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                        }
                                    }
                                }

                                if (Available > 21 && _game.Items.Has(ItemType.CaneOfSomaria))
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                        (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                }

                                if (Available > 22)
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                        (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                }

                                break;
                            case DungeonItemShuffle.Keysanity:

                                if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Hookshot) &&
                                    _game.Items.Has(ItemType.Boots) && _game.Items.Has(ItemType.CaneOfSomaria) &&
                                    (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value) &&
                                    _game.Items.Has(ItemType.FireRod) && _game.Items.Has(ItemType.GTSmallKey, 2) &&
                                    _game.Items.Has(ItemType.GTBigKey))
                                    return _game.Regions[RegionID.GanonsTower].Accessibility;

                                if (_game.Items.Has(ItemType.Hammer) &&
                                    (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots)))
                                {
                                    if (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp))
                                    {
                                        if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        {
                                            if (_game.Items.Has(ItemType.GTBigKey))
                                            {
                                                if ((_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value) &&
                                                    _game.Items.Has(ItemType.GTBigKey))
                                                {
                                                    if (_game.Items.Has(ItemType.GTSmallKey))
                                                    {
                                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                                            (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                                    }

                                                    if (Available > 1)
                                                    {
                                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                            (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                                    }
                                                }

                                                if (Available > 4)
                                                {
                                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                        (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                                }
                                            }

                                            if (Available > 5)
                                            {
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                    (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                            }
                                        }

                                        if (_game.Items.Has(ItemType.GTBigKey))
                                        {
                                            if (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value)
                                            {
                                                if (Available > 5 && _game.Items.Has(ItemType.GTSmallKey, 2))
                                                {
                                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                        (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                                }

                                                if (Available > 6 && _game.Items.Has(ItemType.GTSmallKey))
                                                {
                                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                        (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                                }

                                                if (Available > 7)
                                                {
                                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                        (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                                }
                                            }
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                    {
                                        if (Available > 8 &&
                                            _game.Items.Has(ItemType.GTBigKey) && _game.Items.Has(ItemType.GTSmallKey))
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                        }

                                        if (Available > 9 && _game.Items.Has(ItemType.GTSmallKey))
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                        }

                                        if (Available > 9 && _game.Items.Has(ItemType.GTBigKey))
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                        }

                                        if (Available > 10)
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                        }
                                    }

                                    if (Available > 9 &&
                                        _game.Items.Has(ItemType.GTSmallKey) && _game.Items.Has(ItemType.GTBigKey))
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                    }

                                    if (Available > 10 && _game.Items.Has(ItemType.GTSmallKey))
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                    }

                                    if (Available > 10 && _game.Items.Has(ItemType.GTBigKey))
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                    }

                                    if (Available > 11)
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                            (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                    }
                                }

                                if (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp))
                                {
                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                    {
                                        if (_game.Items.Has(ItemType.GTBigKey))
                                        {
                                            if (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value)
                                            {
                                                if (Available > 10 && _game.Items.Has(ItemType.GTSmallKey) &&
                                                    (_game.Items.Has(ItemType.Boots) || _game.Items.Has(ItemType.Hookshot)))
                                                {
                                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                        (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                                }

                                                if (Available > 11)
                                                {
                                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                        (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                                }
                                            }

                                            if (Available > 13)
                                            {
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                    (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                            }
                                        }

                                        if (Available > 14)
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                        }
                                    }

                                    if ((_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value) &&
                                        _game.Items.Has(ItemType.GTBigKey))
                                    {
                                        if (Available > 20 &&
                                            (_game.Items.Has(ItemType.Boots) || _game.Items.Has(ItemType.Hookshot)))
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                        }

                                        if (Available > 21)
                                        {
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                                (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                        }
                                    }
                                }

                                if (Available > 22 && _game.Items.Has(ItemType.CaneOfSomaria))
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                        (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                }

                                if (Available > 23)
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial,
                                        (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                }

                                break;
                        }
                        
                        return AccessibilityLevel.None;
                    };

                    _updateOnDungeonItemShuffleChange = true;
                    _updateOnEnemyShuffleChange = true;

                    _regionSubscriptions.Add(RegionID.GanonsTower, new Mode());

                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.Hookshot, new Mode());
                    _itemSubscriptions.Add(ItemType.Boots, new Mode());
                    _itemSubscriptions.Add(ItemType.CaneOfSomaria, new Mode());
                    _itemSubscriptions.Add(ItemType.Bow, new Mode() { EnemyShuffle = false });
                    _itemSubscriptions.Add(ItemType.FireRod, new Mode());
                    _itemSubscriptions.Add(ItemType.Lamp, new Mode());
                    _itemSubscriptions.Add(ItemType.GTSmallKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys });
                    _itemSubscriptions.Add(ItemType.GTBigKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity });

                    PropertyChanged += OnRequirementChanged;

                    break;
            }

            if (RequiredMode == null)
                RequiredMode = new Mode();

            SetTotal();

            _game.Mode.PropertyChanged += OnModeChanged;

            UpdateRegionSubscriptions();
            UpdateItemSubscriptions();

            UpdateAccessibility();
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateItemSubscriptions();
            UpdateRegionSubscriptions();

            if (e.PropertyName == nameof(_game.Mode.ItemPlacement) &&
                _updateOnItemPlacementChange)
                UpdateAccessibility();

            if (e.PropertyName == nameof(_game.Mode.DungeonItemShuffle))
            {
                SetTotal();

                if (_updateOnDungeonItemShuffleChange)
                    UpdateAccessibility();
            }

            if (e.PropertyName == nameof(_game.Mode.WorldState))
                UpdateAccessibility();

            if (e.PropertyName == nameof(_game.Mode.EnemyShuffle) &&
                _updateOnEnemyShuffleChange)
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

        private void SetTotal()
        {
            int newTotal = _baseTotal +
                ((_game.Mode.DungeonItemShuffle.Value >= DungeonItemShuffle.MapsCompasses) ? _mapCompass : 0) +
                ((_game.Mode.DungeonItemShuffle.Value >= DungeonItemShuffle.MapsCompassesSmallKeys) ? _smallKey : 0) +
                ((_game.Mode.DungeonItemShuffle.Value >= DungeonItemShuffle.Keysanity) ? _bigKey : 0);

            int delta = newTotal - Total;

            Total = newTotal;
            Available = Math.Max(0, Math.Min(Total, Available + delta));
        }

        private void UpdateAccessibility()
        {
            Accessibility = GetAccessibility();
        }
        
        public void Clear()
        {
            do
            {
                Available--;
            } while (Accessibility >= AccessibilityLevel.Inspect && Available > 0);
        }

        public bool IsAvailable()
        {
            return Available > 0;
        }

        public void Reset()
        {
            Marking = null;
            Available = Total;
        }
    }
}
