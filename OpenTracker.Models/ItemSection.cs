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
        private readonly Region _standardRegion;
        private readonly Region _invertedRegion;
        private readonly int _mapCompass;
        private readonly int _smallKey;
        private readonly int _bigKey;
        private readonly int _baseTotal;

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

            _game.Mode.PropertyChanged += OnModeChanged;

            List<Item> itemReqs = new List<Item>();

            switch (_location.ID)
            {
                case LocationID.Pedestal:

                    _baseTotal = 1;
                    Name = "Pedestal";
                    HasMarking = true;
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Items.Has(ItemType.GreenPendant) && _game.Items.Has(ItemType.Pendant, 2))
                        {
                            //  Basic Item Placement requires Book in logic
                            if (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                _game.Mode.WorldState == WorldState.Inverted ||
                                _game.Items.Has(ItemType.Book))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.SequenceBreak;
                        }
                        
                        if (_game.Items.Has(ItemType.Book))
                            return AccessibilityLevel.Inspect;

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.GreenPendant]);
                    itemReqs.Add(_game.Items[ItemType.Pendant]);
                    itemReqs.Add(_game.Items[ItemType.Book]);

                    break;
                case LocationID.LumberjackCave:

                    _baseTotal = 1;
                    Name = "Cave";
                    HasMarking = true;
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Aga) && _game.Items.Has(ItemType.Boots))
                                return AccessibilityLevel.Normal;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Aga) && _game.Items.Has(ItemType.Boots) &&
                                _game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.Inspect;
                    };

                    itemReqs.Add(_game.Items[ItemType.Aga]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Boots]);

                    break;
                case LocationID.BlindsHouse when index == 0:

                    _baseTotal = 4;
                    Name = "Main";
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.BlindsHouse:

                    _baseTotal = 1;
                    Name = "Bomb";
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.TheWell when index == 0:

                    _baseTotal = 4;
                    Name = "Cave";
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.TheWell:

                    _baseTotal = 1;
                    Name = "Bomb";
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;
                        }
                        
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.BottleVendor:

                    _baseTotal = 1;
                    Name = "This Jerk";
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.ChickenHouse:

                    _baseTotal = 1;
                    Name = "Bombable Wall";
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;
                        }
                        
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.Tavern:

                    _baseTotal = 1;
                    Name = "Back Room";
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.SickKid:

                    _baseTotal = 1;
                    Name = "By The Bed";
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Items.Has(ItemType.Bottle))
                            return AccessibilityLevel.Normal;

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Bottle]);

                    break;
                case LocationID.MagicBat:

                    _baseTotal = 1;
                    Name = "Magic Bowl";
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Powder))
                            {
                                if (_game.Items.Has(ItemType.Hammer))
                                    return AccessibilityLevel.Normal;

                                if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Gloves, 2) &&
                                    _game.Items.Has(ItemType.Mirror))
                                    return AccessibilityLevel.Normal;
                            }

                            if (_game.Items.Has(ItemType.Mushroom) && _game.Items.Has(ItemType.CaneOfSomaria))
                            {
                                if (_game.Items.Has(ItemType.Hammer))
                                    return AccessibilityLevel.SequenceBreak;

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
                                    return AccessibilityLevel.Normal;

                                if (_game.Items.Has(ItemType.Mushroom) && _game.Items.Has(ItemType.CaneOfSomaria))
                                    return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Powder]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.Mushroom]);
                    itemReqs.Add(_game.Items[ItemType.CaneOfSomaria]);

                    break;
                case LocationID.RaceGame:

                    _baseTotal = 1;
                    Name = "Take This Trash";
                    HasMarking = true;
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.RaceGameAccess))
                                return AccessibilityLevel.Normal;

                            if (!_game.Mode.EntranceShuffle.Value)
                                return AccessibilityLevel.Normal;

                            if (_game.Regions[RegionID.DarkWorldSouth].Accessibility >= AccessibilityLevel.SequenceBreak &&
                                _game.Items.Has(ItemType.Mirror))
                                return _game.Regions[RegionID.DarkWorldWest].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                            {
                                if (_game.Items.Has(ItemType.RaceGameAccess))
                                    return AccessibilityLevel.Normal;

                                if (!_game.Mode.EntranceShuffle.Value)
                                    return AccessibilityLevel.Normal;
                            }
                        }

                        return AccessibilityLevel.Inspect;
                    };

                    itemReqs.Add(_game.Items[ItemType.RaceGameAccess]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    _game.Regions[RegionID.DarkWorldWest].PropertyChanged += OnRequirementChanged;

                    break;
                case LocationID.Library:

                    _baseTotal = 1;
                    Name = "On The Shelf";
                    HasMarking = true;
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Boots))
                                return AccessibilityLevel.Normal;
                        }    

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Boots))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.Inspect;
                    };

                    itemReqs.Add(_game.Items[ItemType.Boots]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.LostWoods when index == 0:

                    _baseTotal = 1;
                    Name = "Shroom";
                    HasMarking = true;
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.Inspect;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.LostWoods:

                    _baseTotal = 1;
                    Name = "Hideout";
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    RequiredMode = new Mode() { EntranceShuffle = false };

                    break;
                case LocationID.CastleSecret when index == 0:

                    _baseTotal = 1;
                    Name = "Uncle";
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.CastleSecret:

                    _baseTotal = 1;
                    Name = "Hallway";
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;
                        }
                        
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.LinksHouse:

                    _baseTotal = 1;
                    Name = "By The Door";
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldSouth];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.GroveDiggingSpot:

                    _baseTotal = 1;
                    Name = "Hidden Treasure";
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Shovel))
                                return AccessibilityLevel.Normal;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Shovel))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Shovel]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.PyramidLedge:

                    _baseTotal = 1;
                    Name = "Ledge";
                    _standardRegion = _game.Regions[RegionID.DarkWorldEast];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldEast];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.FatFairy:

                    _baseTotal = 2;
                    Name = "Big Bomb Spot";
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldSouth];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.RedCrystal, 2))
                            {
                                if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Gloves))
                                    return AccessibilityLevel.Normal;

                                if (_game.Items.Has(ItemType.Aga))
                                {
                                    if (_game.Items.Has(ItemType.Hammer))
                                        return AccessibilityLevel.Normal;

                                    if (_game.Items.Has(ItemType.Mirror))
                                    {
                                        if (_game.Items.Has(ItemType.Gloves, 2))
                                            return AccessibilityLevel.Normal;

                                        if (_game.Items.Has(ItemType.Hookshot) &&
                                            (_game.Items.Has(ItemType.Flippers) || _game.Items.Has(ItemType.Gloves)))
                                            return AccessibilityLevel.Normal;
                                    }
                                }

                                if (_game.Items.Has(ItemType.Gloves, 2))
                                    return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.RedCrystal, 2) && _game.Items.Has(ItemType.Mirror))
                                return _game.Regions[RegionID.LightWorld].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.RedCrystal]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.Aga]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.Hookshot]);
                    itemReqs.Add(_game.Items[ItemType.Flippers]);

                    _game.Regions[RegionID.LightWorld].PropertyChanged += OnRequirementChanged;

                    break;
                case LocationID.HauntedGrove:

                    _baseTotal = 1;
                    Name = "Stumpy";
                    _standardRegion = _game.Regions[RegionID.DarkWorldSouth];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldSouth];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.Inverted ||
                            _game.Items.Has(ItemType.MoonPearl))
                            return AccessibilityLevel.Normal;

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.HypeCave:

                    _baseTotal = 5;
                    Name = "Cave";
                    _standardRegion = _game.Regions[RegionID.DarkWorldSouth];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldSouth];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                            return AccessibilityLevel.Normal;

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.BombosTablet:

                    _baseTotal = 1;
                    Name = "Tablet";
                    HasMarking = true;
                    _standardRegion = _game.Regions[RegionID.DarkWorldSouth];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Book) && _game.Items.Has(ItemType.Mirror))
                            {
                                if (_game.Items.CanActivateTablets())
                                    return AccessibilityLevel.Normal;

                                return AccessibilityLevel.Inspect;
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Book))
                            {
                                if (_game.Items.CanActivateTablets())
                                    return AccessibilityLevel.Normal;

                                return AccessibilityLevel.Inspect;
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Book]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.Sword]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);

                    break;
                case LocationID.SouthOfGrove:

                    _baseTotal = 1;
                    Name = "Circle of Bushes";
                    _standardRegion = _game.Regions[RegionID.DarkWorldSouth];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Mirror))
                                return AccessibilityLevel.Normal;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.DiggingGame:

                    _baseTotal = 1;
                    Name = "Dig For Treasure";
                    _standardRegion = _game.Regions[RegionID.DarkWorldSouth];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldSouth];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                            return AccessibilityLevel.Normal;

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.WitchsHut:

                    _baseTotal = 1;
                    Name = "Assistant";
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Mushroom))
                                return AccessibilityLevel.Normal;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Mushroom))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Mushroom]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.WaterfallFairy:

                    _baseTotal = 2;
                    Name = "Waterfall Cave";
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Flippers))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.SequenceBreak;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                            {
                                if (_game.Items.Has(ItemType.Flippers))
                                    return AccessibilityLevel.Normal;

                                return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Flippers]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.ZoraArea when index == 0:

                    _baseTotal = 1;
                    Name = "King Zora";
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Gloves) || _game.Items.Has(ItemType.Flippers))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.SequenceBreak;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                            {
                                if (_game.Items.Has(ItemType.Gloves) || _game.Items.Has(ItemType.Flippers))
                                    return AccessibilityLevel.Normal;

                                return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.Flippers]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.ZoraArea:

                    _baseTotal = 1;
                    Name = "Ledge";
                    HasMarking = true;
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Flippers))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.SequenceBreak;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                            {
                                if (_game.Items.Has(ItemType.Flippers))
                                    return AccessibilityLevel.Normal;

                                return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Flippers]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.Catfish:

                    _baseTotal = 1;
                    Name = "Ring of Stones";
                    _standardRegion = _game.Regions[RegionID.DarkWorldEast];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldEast];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Gloves))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.SahasrahlasHut when index == 0:

                    _baseTotal = 3;
                    Name = "Back Room";
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.SahasrahlasHut:

                    _baseTotal = 1;
                    Name = "Shabba";
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Items.Has(ItemType.GreenPendant))
                            return AccessibilityLevel.Normal;

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.GreenPendant]);

                    break;
                case LocationID.BonkRocks:

                    _baseTotal = 1;
                    Name = "Cave";
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

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

                    itemReqs.Add(_game.Items[ItemType.Boots]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.KingsTomb:

                    _baseTotal = 1;
                    Name = "The Crypt";
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Boots))
                            {
                                if (_game.Items.Has(ItemType.Gloves, 2))
                                    return AccessibilityLevel.Normal;

                                if (_game.Items.Has(ItemType.Mirror))
                                    return _game.Regions[RegionID.DarkWorldWest].Accessibility;
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

                    itemReqs.Add(_game.Items[ItemType.Boots]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    _game.Regions[RegionID.DarkWorldWest].PropertyChanged += OnRequirementChanged;

                    break;
                case LocationID.GraveyardLedge:

                    _baseTotal = 1;
                    Name = "Cave";
                    _standardRegion = _game.Regions[RegionID.DarkWorldWest];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Mirror))
                                return AccessibilityLevel.Normal;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.DesertLedge:

                    _baseTotal = 1;
                    Name = "Ledge";
                    HasMarking = true;
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Items.Has(ItemType.DesertLeftAccess))
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.DesertBackAccess) && _game.Items.Has(ItemType.Gloves))
                                return AccessibilityLevel.Normal;

                            if (!_game.Mode.EntranceShuffle.Value && _game.Items.Has(ItemType.Book))
                                return AccessibilityLevel.Normal;

                            if (_game.Regions[RegionID.MireArea].Accessibility >= AccessibilityLevel.SequenceBreak &&
                                _game.Items.Has(ItemType.Mirror))
                                return _game.Regions[RegionID.MireArea].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                            {
                                if (_game.Items.Has(ItemType.DesertBackAccess) && _game.Items.Has(ItemType.Gloves))
                                    return AccessibilityLevel.Normal;

                                if (!_game.Mode.EntranceShuffle.Value && _game.Items.Has(ItemType.Book))
                                    return AccessibilityLevel.Normal;
                            }
                        }

                        return AccessibilityLevel.Inspect;
                    };

                    itemReqs.Add(_game.Items[ItemType.DesertLeftAccess]);
                    itemReqs.Add(_game.Items[ItemType.DesertBackAccess]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.Book]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    _game.Regions[RegionID.MireArea].PropertyChanged += OnRequirementChanged;

                    break;
                case LocationID.AginahsCave:

                    _baseTotal = 1;
                    Name = "Cave";
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.CShapedHouse:

                    _baseTotal = 1;
                    Name = "House";
                    _standardRegion = _game.Regions[RegionID.DarkWorldWest];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldWest];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.TreasureGame:

                    _baseTotal = 1;
                    Name = "Prize";
                    _standardRegion = _game.Regions[RegionID.DarkWorldWest];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldWest];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.BombableShack:

                    _baseTotal = 1;
                    Name = "Downstairs";
                    _standardRegion = _game.Regions[RegionID.DarkWorldWest];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldWest];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.Blacksmith:

                    _baseTotal = 1;
                    Name = "House";
                    _standardRegion = _game.Regions[RegionID.DarkWorldWest];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldWest];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Gloves, 2))
                            {
                                if (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                    _game.Items.Has(ItemType.Mirror))
                                    return AccessibilityLevel.Normal;

                                return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Gloves, 2))
                                return AccessibilityLevel.Normal;

                            if (_game.Items.Has(ItemType.Mirror))
                                return _game.Regions[RegionID.LightWorld].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);

                    _game.Regions[RegionID.LightWorld].PropertyChanged += OnRequirementChanged;

                    break;
                case LocationID.PurpleChest:

                    _baseTotal = 1;
                    Name = "Show To Gary";
                    _standardRegion = _game.Regions[RegionID.DarkWorldWest];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldWest];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Gloves, 2))
                                return AccessibilityLevel.Normal;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Gloves, 2))
                                return AccessibilityLevel.Normal;

                            if (_game.Items.Has(ItemType.Mirror))
                                return _game.Regions[RegionID.LightWorld].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);

                    _game.Regions[RegionID.LightWorld].PropertyChanged += OnRequirementChanged;

                    break;
                case LocationID.HammerPegs:

                    _baseTotal = 1;
                    Name = "Cave";
                    _standardRegion = _game.Regions[RegionID.DarkWorldWest];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldWest];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Gloves, 2) && _game.Items.Has(ItemType.Hammer))
                                return AccessibilityLevel.Normal;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Hammer))
                            {
                                if (_game.Items.Has(ItemType.Gloves, 2))
                                    return AccessibilityLevel.Normal;

                                if (_game.Items.Has(ItemType.Mirror))
                                    return _game.Regions[RegionID.LightWorld].Accessibility;
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);

                    _game.Regions[RegionID.LightWorld].PropertyChanged += OnRequirementChanged;

                    break;
                case LocationID.BumperCave:

                    _baseTotal = 1;
                    Name = "Ledge";
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldSouth];
                    HasMarking = true;

                    GetAccessibility = () =>
                    {
                        if (_game.Items.Has(ItemType.BumperCaveAccess))
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            AccessibilityLevel dWWest = _game.Regions[RegionID.DarkWorldWest].Accessibility;

                            if (dWWest >= AccessibilityLevel.SequenceBreak)
                            {
                                if (!_game.Mode.EntranceShuffle.Value)
                                {
                                    if (_game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.Cape))
                                    {
                                        if (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                            _game.Items.Has(ItemType.Hookshot))
                                            return dWWest;

                                        return (AccessibilityLevel)Math.Min((byte)dWWest, (byte)AccessibilityLevel.SequenceBreak);
                                    }
                                }

                                return AccessibilityLevel.Inspect;
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.DeathMountainExitAccess) && _game.Items.Has(ItemType.Mirror))
                                return AccessibilityLevel.Normal;

                            AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                            if (!_game.Mode.EntranceShuffle.Value && _game.Items.Has(ItemType.MoonPearl) &&
                                _game.Items.Has(ItemType.Cape) && _game.Items.Has(ItemType.Gloves) &&
                                _game.Items.Has(ItemType.Mirror) && lightWorld >= AccessibilityLevel.SequenceBreak)
                                return (AccessibilityLevel)Math.Min((byte)lightWorld, (byte)AccessibilityLevel.Normal);

                            if (_game.Regions[RegionID.DarkWorldWest].Accessibility >= AccessibilityLevel.SequenceBreak)
                                return AccessibilityLevel.Inspect;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.BumperCaveAccess]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.Cape]);
                    itemReqs.Add(_game.Items[ItemType.Hookshot]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainExitAccess]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    _game.Regions[RegionID.DarkWorldWest].PropertyChanged += OnRequirementChanged;

                    break;
                case LocationID.Dam when index == 0:

                    _baseTotal = 1;
                    Name = "Inside";
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];
                    RequiredMode = new Mode() { EntranceShuffle = false };

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.Dam:

                    _baseTotal = 1;
                    Name = "Outside";
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.MiniMoldormCave:

                    _baseTotal = 5;
                    Name = "Cave";
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.IceRodCave:

                    _baseTotal = 1;
                    Name = "Cave";
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.LakeHyliaIsland:

                    _baseTotal = 1;
                    Name = "Island";
                    HasMarking = true;
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Mirror))
                            {
                                if (_game.Regions[RegionID.DarkWorldEast].Accessibility == AccessibilityLevel.Normal ||
                                    _game.Regions[RegionID.DarkWorldSouth].Accessibility == AccessibilityLevel.Normal ||
                                    _game.Regions[RegionID.DarkWorldSouthEast].Accessibility == AccessibilityLevel.Normal)
                                {
                                    if (_game.Items.Has(ItemType.Flippers))
                                        return AccessibilityLevel.Normal;

                                    return AccessibilityLevel.SequenceBreak;
                                }

                                if (_game.Regions[RegionID.DarkWorldEast].Accessibility == AccessibilityLevel.SequenceBreak ||
                                    _game.Regions[RegionID.DarkWorldSouth].Accessibility == AccessibilityLevel.SequenceBreak ||
                                    _game.Regions[RegionID.DarkWorldSouthEast].Accessibility == AccessibilityLevel.SequenceBreak)
                                    return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Flippers) && _game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.Inspect;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.Flippers]);

                    _game.Regions[RegionID.DarkWorldEast].PropertyChanged += OnRequirementChanged;
                    _game.Regions[RegionID.DarkWorldSouth].PropertyChanged += OnRequirementChanged;
                    _game.Regions[RegionID.DarkWorldSouthEast].PropertyChanged += OnRequirementChanged;

                    break;
                case LocationID.Hobo:

                    _baseTotal = 1;
                    Name = "Under The Bridge";
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Flippers))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.SequenceBreak;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                            {
                                if (_game.Items.Has(ItemType.Flippers))
                                    return AccessibilityLevel.Normal;

                                return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        return AccessibilityLevel.SequenceBreak;
                    };

                    itemReqs.Add(_game.Items[ItemType.Flippers]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.MireShack:

                    _baseTotal = 2;
                    Name = "Shack";
                    _standardRegion = _game.Regions[RegionID.MireArea];
                    _invertedRegion = _game.Regions[RegionID.MireArea];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;

                            if (_game.Items.Has(ItemType.Mirror))
                                return AccessibilityLevel.SequenceBreak;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                            return AccessibilityLevel.Normal;

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);

                    break;
                case LocationID.CheckerboardCave:

                    _baseTotal = 1;
                    Name = "Cave";
                    _standardRegion = _game.Regions[RegionID.MireArea];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Mirror))
                                return AccessibilityLevel.Normal;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Gloves))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);

                    break;
                case LocationID.OldMan:

                    _baseTotal = 1;
                    Name = "Bring Him Home";
                    _standardRegion = _game.Regions[RegionID.DeathMountainWestBottom];
                    _invertedRegion = _game.Regions[RegionID.DeathMountainWestBottom];

                    GetAccessibility = () =>
                    {
                        if (_game.Items.Has(ItemType.Lamp))
                            return AccessibilityLevel.Normal;
                        
                        return AccessibilityLevel.SequenceBreak;
                    };

                    itemReqs.Add(_game.Items[ItemType.Lamp]);

                    break;
                case LocationID.SpectacleRock when index == 0:

                    _baseTotal = 1;
                    Name = "Cave";
                    _standardRegion = _game.Regions[RegionID.DeathMountainWestBottom];
                    _invertedRegion = _game.Regions[RegionID.DeathMountainWestBottom];
                    RequiredMode = new Mode() { EntranceShuffle = false };

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.SpectacleRock:

                    _baseTotal = 1;
                    Name = "Up On Top";
                    _standardRegion = _game.Regions[RegionID.DeathMountainWestBottom];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldSouth];
                    HasMarking = true;

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Mirror))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.Inspect;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Regions[RegionID.DeathMountainWestTop].Accessibility >= AccessibilityLevel.SequenceBreak)
                                return _game.Regions[RegionID.DeathMountainWestTop].Accessibility;

                            if (_game.Regions[RegionID.DeathMountainWestBottom].Accessibility >= AccessibilityLevel.SequenceBreak)
                                return AccessibilityLevel.Inspect;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Mirror]);

                    _game.Regions[RegionID.DeathMountainWestTop].PropertyChanged += OnRequirementChanged;

                    break;
                case LocationID.EtherTablet:

                    _baseTotal = 1;
                    Name = "Tablet";
                    HasMarking = true;
                    _standardRegion = _game.Regions[RegionID.DeathMountainWestTop];
                    _invertedRegion = _game.Regions[RegionID.DeathMountainWestTop];

                    GetAccessibility = () =>
                    {
                        if (_game.Items.Has(ItemType.Book))
                        {
                            if (_game.Items.CanActivateTablets())
                                return AccessibilityLevel.Normal;
                            
                            return AccessibilityLevel.Inspect;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Book]);
                    itemReqs.Add(_game.Items[ItemType.Sword]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);

                    break;
                case LocationID.SpikeCave:

                    _baseTotal = 1;
                    Name = "Cave";
                    _standardRegion = _game.Regions[RegionID.DarkDeathMountainWestBottom];
                    _invertedRegion = _game.Regions[RegionID.DarkDeathMountainWestBottom];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.Hammer) &&
                                _game.Items.Has(ItemType.MoonPearl))
                            {
                                if (_game.Items.Has(ItemType.CaneOfByrna) || (_game.Items.Has(ItemType.Cape) &&
                                    (_game.Items.Has(ItemType.HalfMagic) || _game.Items.Has(ItemType.Bottle))))
                                    return AccessibilityLevel.Normal;

                                return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.Hammer))
                            {
                                if (_game.Items.Has(ItemType.CaneOfByrna) || (_game.Items.Has(ItemType.Cape) &&
                                    (_game.Items.Has(ItemType.HalfMagic) || _game.Items.Has(ItemType.Bottle))))
                                    return AccessibilityLevel.Normal;

                                return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.CaneOfByrna]);
                    itemReqs.Add(_game.Items[ItemType.Cape]);
                    itemReqs.Add(_game.Items[ItemType.HalfMagic]);
                    itemReqs.Add(_game.Items[ItemType.Bottle]);

                    break;
                case LocationID.SpiralCave:

                    _baseTotal = 1;
                    Name = "Cave";
                    _standardRegion = _game.Regions[RegionID.DeathMountainEastTop];
                    _invertedRegion = _game.Regions[RegionID.DeathMountainEastTop];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.ParadoxCave when index == 0:

                    _baseTotal = 2;
                    Name = "Bottom";
                    _standardRegion = _game.Regions[RegionID.DeathMountainEastTop];
                    _invertedRegion = _game.Regions[RegionID.DeathMountainEastTop];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.SequenceBreak;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.ParadoxCave:

                    _baseTotal = 5;
                    Name = "Top";
                    _standardRegion = _game.Regions[RegionID.DeathMountainEastTop];
                    _invertedRegion = _game.Regions[RegionID.DeathMountainEastTop];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.SequenceBreak;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.SuperBunnyCave:

                    _baseTotal = 2;
                    Name = "Cave";
                    _standardRegion = _game.Regions[RegionID.DarkDeathMountainTop];
                    _invertedRegion = _game.Regions[RegionID.DarkDeathMountainTop];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.SequenceBreak;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                            return AccessibilityLevel.Normal;

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.HookshotCave when index == 0:

                    _baseTotal = 1;
                    Name = "Bonkable Chest";
                    _standardRegion = _game.Regions[RegionID.DarkDeathMountainTop];
                    _invertedRegion = _game.Regions[RegionID.DarkDeathMountainTop];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Gloves))
                            {
                                if (_game.Items.Has(ItemType.Hookshot) ||
                                    (_game.Items.Has(ItemType.Boots) &&
                                    (_game.Mode.ItemPlacement == ItemPlacement.Advanced)))
                                    return AccessibilityLevel.Normal;

                                if (_game.Items.Has(ItemType.Boots))
                                    return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Gloves) &&
                                (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots)))
                                    return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.Hookshot]);
                    itemReqs.Add(_game.Items[ItemType.Boots]);

                    break;
                case LocationID.HookshotCave:

                    _baseTotal = 3;
                    Name = "Back";
                    _standardRegion = _game.Regions[RegionID.DarkDeathMountainTop];
                    _invertedRegion = _game.Regions[RegionID.DarkDeathMountainTop];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.MoonPearl) &&
                                _game.Items.Has(ItemType.Hookshot))
                                return AccessibilityLevel.Normal;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.Hookshot))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Hookshot]);

                    break;
                case LocationID.FloatingIsland:

                    _baseTotal = 1;
                    Name = "Island";
                    HasMarking = true;
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldSouth];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Mirror))
                            {
                                if (_game.Items.Has(ItemType.DarkDeathMountainFloatingIslandAccess))
                                    return AccessibilityLevel.Normal;

                                if (!_game.Mode.EntranceShuffle.Value && _game.Items.Has(ItemType.Gloves) &&
                                    _game.Items.Has(ItemType.MoonPearl) &&
                                    _game.Regions[RegionID.DarkDeathMountainTop].Accessibility >= AccessibilityLevel.SequenceBreak)
                                    return _game.Regions[RegionID.DarkDeathMountainTop].Accessibility;
                            }

                            if (_game.Regions[RegionID.DeathMountainEastTop].Accessibility >= AccessibilityLevel.SequenceBreak)
                                return AccessibilityLevel.Inspect;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Regions[RegionID.DeathMountainEastTop].Accessibility >= AccessibilityLevel.SequenceBreak)
                                return _game.Regions[RegionID.DeathMountainEastTop].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.DarkDeathMountainFloatingIslandAccess]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    _game.Regions[RegionID.DarkDeathMountainTop].PropertyChanged += OnRequirementChanged;
                    _game.Regions[RegionID.DeathMountainEastTop].PropertyChanged += OnRequirementChanged;

                    break;
                case LocationID.MimicCave:

                    _baseTotal = 1;
                    Name = "Cave";
                    _standardRegion = _game.Regions[RegionID.TurtleRock];
                    _invertedRegion = _game.Regions[RegionID.DeathMountainEastTop];

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
                                    return AccessibilityLevel.Normal;

                                return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Hammer))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.CaneOfSomaria]);
                    itemReqs.Add(_game.Items[ItemType.TRSmallKey]);
                    itemReqs.Add(_game.Items[ItemType.FireRod]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.HyruleCastle:

                    _mapCompass = 1;
                    _smallKey = 1;
                    _baseTotal = 6;
                    Name = "Escape";
                    _standardRegion = _game.Regions[RegionID.HyruleCastle];
                    _invertedRegion = _game.Regions[RegionID.HyruleCastle];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Gloves) || (_game.Items.Has(ItemType.HCSmallKey) &&
                                _game.Mode.DungeonItemShuffle >= DungeonItemShuffle.MapsCompassesSmallKeys))
                            {
                                if (_game.Items.Has(ItemType.Lamp) ||
                                    (_game.Mode.ItemPlacement.Value == ItemPlacement.Advanced &&
                                    _game.Items.Has(ItemType.FireRod)))
                                    return AccessibilityLevel.Normal;
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Gloves) || (_game.Items.Has(ItemType.HCSmallKey) &&
                                _game.Mode.DungeonItemShuffle >= DungeonItemShuffle.MapsCompassesSmallKeys))
                            {
                                if (_game.Items.Has(ItemType.Lamp))
                                    return AccessibilityLevel.Normal;
                            }
                        }

                        return AccessibilityLevel.SequenceBreak;
                    };

                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.HCSmallKey]);
                    itemReqs.Add(_game.Items[ItemType.Lamp]);
                    itemReqs.Add(_game.Items[ItemType.FireRod]);

                    break;
                case LocationID.AgahnimTower:

                    _baseTotal = 0;
                    _smallKey = 2;
                    Name = "Dungeon";
                    _standardRegion = _game.Regions[RegionID.Agahnim];
                    _invertedRegion = _game.Regions[RegionID.Agahnim];

                    RequiredMode = new Mode() { DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys };

                    GetAccessibility = () =>
                    {
                        if (_game.Items.Has(ItemType.ATSmallKey))
                        {
                            if (_game.Items.Has(ItemType.Lamp))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.SequenceBreak;
                        }

                        if (Available == 2)
                            return AccessibilityLevel.Partial;

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Lamp]);
                    itemReqs.Add(_game.Items[ItemType.ATSmallKey]);

                    PropertyChanged += OnRequirementChanged;

                    break;
                case LocationID.EasternPalace:

                    _mapCompass = 2;
                    _bigKey = 1;
                    _baseTotal = 3;
                    Name = "Dungeon";
                    _standardRegion = _game.Regions[RegionID.EasternPalace];
                    _invertedRegion = _game.Regions[RegionID.EasternPalace];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.DungeonItemShuffle <= DungeonItemShuffle.MapsCompassesSmallKeys)
                        {
                            if (_game.Items.Has(ItemType.Lamp) &&
                                (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value))
                                return AccessibilityLevel.Normal;
                            
                            return AccessibilityLevel.SequenceBreak;
                        }
                        
                        if (_game.Items.Has(ItemType.EPBigKey) && 
                            (_game.Mode.EnemyShuffle.Value || _game.Items.Has(ItemType.Bow)))
                        {
                            if (_game.Items.Has(ItemType.Lamp))
                                return AccessibilityLevel.Normal;
                            
                            return AccessibilityLevel.SequenceBreak;
                        }

                        if (_game.Items.Has(ItemType.EPBigKey) && Available > 1)
                            return AccessibilityLevel.Partial;
                        
                        if (Available > 2)
                            return AccessibilityLevel.Partial;
                        
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Lamp]);
                    itemReqs.Add(_game.Items[ItemType.Bow]);
                    itemReqs.Add(_game.Items[ItemType.EPBigKey]);

                    PropertyChanged += OnRequirementChanged;

                    break;
                case LocationID.DesertPalace:

                    _mapCompass = 2;
                    _smallKey = 1;
                    _bigKey = 1;
                    _baseTotal = 2;
                    Name = "Dungeon";
                    HasMarking = true;
                    _standardRegion = _game.Regions[RegionID.DesertPalace];
                    _invertedRegion = _game.Regions[RegionID.DesertPalace];

                    GetAccessibility = () =>
                    {
                        switch (_game.Mode.DungeonItemShuffle.Value)
                        {
                            case DungeonItemShuffle.Standard:

                                if (_game.Items.Has(ItemType.Boots) &&
                                    (_game.Items.Has(ItemType.Gloves) || _game.Mode.EntranceShuffle.Value) &&
                                    (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)))
                                    return AccessibilityLevel.Normal;

                                return AccessibilityLevel.SequenceBreak;

                            case DungeonItemShuffle.MapsCompasses:

                                if (_game.Items.Has(ItemType.Boots) &&
                                    (_game.Items.Has(ItemType.Gloves) || _game.Mode.EntranceShuffle.Value) &&
                                    (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)))
                                    return AccessibilityLevel.Normal;

                                if ((_game.Items.Has(ItemType.Gloves) || _game.Mode.EntranceShuffle.Value) &&
                                    (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)))
                                    return AccessibilityLevel.SequenceBreak;

                                if (Available > 1)
                                    return AccessibilityLevel.Partial;

                                break;
                            case DungeonItemShuffle.MapsCompassesSmallKeys:

                                if (_game.Items.Has(ItemType.Boots) && _game.Items.Has(ItemType.DPSmallKey) &&
                                    (_game.Items.Has(ItemType.Gloves) || _game.Mode.EntranceShuffle.Value) &&
                                    (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)))
                                    return AccessibilityLevel.Normal;

                                if (_game.Items.Has(ItemType.DPSmallKey) &&
                                    (_game.Items.Has(ItemType.Gloves) || _game.Mode.EntranceShuffle.Value) &&
                                    (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)))
                                    return AccessibilityLevel.SequenceBreak;
                                
                                if (_game.Items.Has(ItemType.DPSmallKey))
                                {
                                    if (Available > 1)
                                        return AccessibilityLevel.Partial;
                                }

                                if (Available > 3)
                                    return AccessibilityLevel.Partial;

                                break;
                            case DungeonItemShuffle.Keysanity:

                                if (_game.Items.Has(ItemType.Boots) && _game.Items.Has(ItemType.DPBigKey) &&
                                    _game.Items.Has(ItemType.DPSmallKey) &&
                                    (_game.Items.Has(ItemType.Gloves) || _game.Mode.EntranceShuffle.Value) &&
                                    (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)))
                                    return AccessibilityLevel.Normal;

                                if (_game.Items.Has(ItemType.DPBigKey) && _game.Items.Has(ItemType.DPSmallKey) &&
                                    (_game.Items.Has(ItemType.Gloves) || _game.Mode.EntranceShuffle.Value) &&
                                    (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)))
                                    return AccessibilityLevel.SequenceBreak;

                                if (_game.Items.Has(ItemType.DPBigKey) && _game.Items.Has(ItemType.DPSmallKey))
                                {
                                    if (Available > 1)
                                        return AccessibilityLevel.Partial;
                                }

                                if (_game.Items.Has(ItemType.DPBigKey) &&
                                    (_game.Items.Has(ItemType.Gloves) || _game.Mode.EntranceShuffle.Value) &&
                                    (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)))
                                {
                                    if (Available > 2)
                                        return AccessibilityLevel.Partial;
                                }

                                if (_game.Items.Has(ItemType.DPSmallKey))
                                {
                                    if (Available > 2)
                                        return AccessibilityLevel.Partial;
                                }

                                if (_game.Items.Has(ItemType.DPBigKey))
                                {
                                    if (Available > 3)
                                        return AccessibilityLevel.Partial;
                                }

                                if (Available > 4)
                                    return AccessibilityLevel.Partial;

                                break;
                        }
                        
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Boots]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.Lamp]);
                    itemReqs.Add(_game.Items[ItemType.FireRod]);
                    itemReqs.Add(_game.Items[ItemType.DPBigKey]);
                    itemReqs.Add(_game.Items[ItemType.DPSmallKey]);

                    PropertyChanged += OnRequirementChanged;

                    break;
                case LocationID.TowerOfHera:

                    _mapCompass = 2;
                    _smallKey = 1;
                    _bigKey = 1;
                    _baseTotal = 2;
                    Name = "Dungeon";
                    _standardRegion = _game.Regions[RegionID.TowerOfHera];
                    _invertedRegion = _game.Regions[RegionID.TowerOfHera];

                    GetAccessibility = () =>
                    {
                        switch (_game.Mode.DungeonItemShuffle.Value)
                        {
                            case DungeonItemShuffle.Standard:
                            case DungeonItemShuffle.MapsCompasses:

                                if (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod))
                                    return AccessibilityLevel.Normal;

                                return AccessibilityLevel.SequenceBreak;

                            case DungeonItemShuffle.MapsCompassesSmallKeys:

                                if ((_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)) &&
                                    _game.Items.Has(ItemType.ToHSmallKey))
                                    return AccessibilityLevel.Normal;

                                if (Available > 1)
                                    return AccessibilityLevel.Partial;

                                break;
                            case DungeonItemShuffle.Keysanity:

                                if ((_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)) &&
                                    _game.Items.Has(ItemType.ToHSmallKey) && _game.Items.Has(ItemType.ToHBigKey))
                                    return AccessibilityLevel.Normal;

                                if ((_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)) &&
                                    _game.Items.Has(ItemType.ToHSmallKey))
                                {
                                    if (Available > 3)
                                        return AccessibilityLevel.Partial;

                                    if (_game.Items.Has(ItemType.Hookshot) && Available > 1)
                                        return AccessibilityLevel.Partial;
                                }

                                if (_game.Items.Has(ItemType.ToHBigKey))
                                {
                                    if (Available > 1)
                                        return AccessibilityLevel.Partial;
                                }

                                if (_game.Items.Has(ItemType.Hookshot) && Available > 2)
                                    return AccessibilityLevel.Partial;

                                if (Available > 4)
                                    return AccessibilityLevel.Partial;

                                break;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Lamp]);
                    itemReqs.Add(_game.Items[ItemType.FireRod]);
                    itemReqs.Add(_game.Items[ItemType.ToHSmallKey]);
                    itemReqs.Add(_game.Items[ItemType.ToHBigKey]);
                    itemReqs.Add(_game.Items[ItemType.Hookshot]);

                    PropertyChanged += OnRequirementChanged;

                    break;
                case LocationID.PalaceOfDarkness:

                    _mapCompass = 2;
                    _smallKey = 6;
                    _bigKey = 1;
                    _baseTotal = 5;
                    Name = "Dungeon";
                    _standardRegion = _game.Regions[RegionID.PalaceOfDarkness];
                    _invertedRegion = _game.Regions[RegionID.PalaceOfDarkness];

                    GetAccessibility = () =>
                    {
                        switch (_game.Mode.DungeonItemShuffle.Value)
                        {
                            case DungeonItemShuffle.Standard:

                                if (_game.Items.Has(ItemType.Lamp) && _game.Items.Has(ItemType.Hammer) &&
                                    _game.Items.Has(ItemType.Bow))
                                    return AccessibilityLevel.Normal;
                                
                                return AccessibilityLevel.SequenceBreak;

                            case DungeonItemShuffle.MapsCompasses:

                                if (_game.Items.Has(ItemType.Lamp) && _game.Items.Has(ItemType.Hammer) &&
                                    _game.Items.Has(ItemType.Bow))
                                    return AccessibilityLevel.Normal;

                                if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Bow))
                                    return AccessibilityLevel.SequenceBreak;

                                if (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value)
                                        return AccessibilityLevel.SequenceBreak;

                                if (Available > 1)
                                    return AccessibilityLevel.Partial;

                                break;
                            case DungeonItemShuffle.MapsCompassesSmallKeys:

                                if (_game.Items.Has(ItemType.Lamp) && _game.Items.Has(ItemType.Hammer) &&
                                    _game.Items.Has(ItemType.Bow) && _game.Items.Has(ItemType.PoDSmallKey, 5))
                                    return AccessibilityLevel.Normal;

                                if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Bow) &&
                                    _game.Items.Has(ItemType.PoDSmallKey, 4))
                                    return AccessibilityLevel.SequenceBreak;

                                if (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value)
                                {
                                    if (Available > 1 && (_game.Items.Has(ItemType.PoDSmallKey, 4) ||
                                        (_game.Items.Has(ItemType.PoDSmallKey, 3) && _game.Items.Has(ItemType.Hammer))))
                                        return AccessibilityLevel.Partial;

                                    if (Available > 2 && (_game.Items.Has(ItemType.PoDSmallKey, 3) ||
                                        (_game.Items.Has(ItemType.PoDSmallKey, 2) && _game.Items.Has(ItemType.Hammer))))
                                        return AccessibilityLevel.Partial;

                                    if (Available > 3 && (_game.Items.Has(ItemType.PoDSmallKey, 2) ||
                                        (_game.Items.Has(ItemType.PoDSmallKey) && _game.Items.Has(ItemType.Hammer))))
                                        return AccessibilityLevel.Partial;

                                    if (Available > 8 &&
                                        (_game.Items.Has(ItemType.PoDSmallKey) || _game.Items.Has(ItemType.Hammer)))
                                        return AccessibilityLevel.Partial;
                                }

                                if (_game.Items.Has(ItemType.PoDSmallKey, 4) && Available > 3)
                                    return AccessibilityLevel.Partial;

                                if (_game.Items.Has(ItemType.PoDSmallKey, 3) && Available > 4)
                                    return AccessibilityLevel.Partial;

                                if (_game.Items.Has(ItemType.PoDSmallKey, 2) && Available > 5)
                                    return AccessibilityLevel.Partial;

                                if (_game.Items.Has(ItemType.PoDSmallKey) && Available > 10)
                                    return AccessibilityLevel.Partial;

                                if (Available > 12)
                                    return AccessibilityLevel.Partial;

                                break;
                            case DungeonItemShuffle.Keysanity:

                                if (_game.Items.Has(ItemType.Lamp) && _game.Items.Has(ItemType.Hammer) &&
                                    _game.Items.Has(ItemType.Bow) && _game.Items.Has(ItemType.PoDSmallKey, 5) &&
                                    _game.Items.Has(ItemType.PoDBigKey))
                                    return AccessibilityLevel.Normal;

                                if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Bow) &&
                                    _game.Items.Has(ItemType.PoDSmallKey, 4) && _game.Items.Has(ItemType.PoDBigKey))
                                    return AccessibilityLevel.SequenceBreak;

                                if (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value)
                                {
                                    if (_game.Items.Has(ItemType.PoDSmallKey, 4) ||
                                        (_game.Items.Has(ItemType.PoDSmallKey, 3) && _game.Items.Has(ItemType.Hammer)))
                                    {
                                        if (_game.Items.Has(ItemType.PoDBigKey) && Available > 1)
                                            return AccessibilityLevel.Partial;

                                        if (Available > 2)
                                            return AccessibilityLevel.Partial;
                                    }

                                    if (_game.Items.Has(ItemType.PoDSmallKey, 3) ||
                                        (_game.Items.Has(ItemType.PoDSmallKey, 2) && _game.Items.Has(ItemType.Hammer)))
                                    {
                                        if (_game.Items.Has(ItemType.PoDBigKey) && Available > 2)
                                            return AccessibilityLevel.Partial;

                                        if (Available > 3)
                                            return AccessibilityLevel.Partial;
                                    }

                                    if (_game.Items.Has(ItemType.PoDSmallKey, 2) ||
                                        (_game.Items.Has(ItemType.PoDSmallKey) && _game.Items.Has(ItemType.Hammer)))
                                    {
                                        if (_game.Items.Has(ItemType.PoDBigKey) && Available > 3)
                                            return AccessibilityLevel.Partial;

                                        if (Available > 4)
                                            return AccessibilityLevel.Partial;
                                    }

                                    if (Available > 9 &&
                                        (_game.Items.Has(ItemType.PoDSmallKey) || _game.Items.Has(ItemType.Hammer)))
                                        return AccessibilityLevel.Partial;
                                }

                                if (_game.Items.Has(ItemType.PoDSmallKey, 4))
                                {
                                    if (_game.Items.Has(ItemType.PoDBigKey) && Available > 3)
                                        return AccessibilityLevel.Partial;

                                    if (Available > 4)
                                        return AccessibilityLevel.Partial;
                                }

                                if (_game.Items.Has(ItemType.PoDSmallKey, 3))
                                {
                                    if (_game.Items.Has(ItemType.PoDBigKey) && Available > 4)
                                        return AccessibilityLevel.Partial;

                                    if (Available > 5)
                                        return AccessibilityLevel.Partial;
                                }

                                if (_game.Items.Has(ItemType.PoDSmallKey, 2))
                                {
                                    if (_game.Items.Has(ItemType.PoDBigKey) && Available > 5)
                                        return AccessibilityLevel.Partial;

                                    if (Available > 6)
                                        return AccessibilityLevel.Partial;
                                }

                                if (_game.Items.Has(ItemType.PoDSmallKey) && Available > 11)
                                    return AccessibilityLevel.Partial;

                                if (Available > 13)
                                    return AccessibilityLevel.Partial;

                                break;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Lamp]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.Bow]);
                    itemReqs.Add(_game.Items[ItemType.PoDBigKey]);
                    itemReqs.Add(_game.Items[ItemType.PoDSmallKey]);

                    PropertyChanged += OnRequirementChanged;

                    break;
                case LocationID.SwampPalace:

                    _mapCompass = 2;
                    _smallKey = 1;
                    _bigKey = 1;
                    _baseTotal = 6;
                    Name = "Dungeon";
                    _standardRegion = _game.Regions[RegionID.SwampPalace];
                    _invertedRegion = _game.Regions[RegionID.SwampPalace];

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
                                            return AccessibilityLevel.Normal;

                                        if (Available > 2)
                                            return AccessibilityLevel.Partial;
                                    }

                                    if (Available > 5)
                                        return AccessibilityLevel.Partial;
                                }

                                break;
                            case DungeonItemShuffle.MapsCompasses:

                                if (_game.Items.Has(ItemType.Flippers))
                                {
                                    if (_game.Items.Has(ItemType.Hammer))
                                    {
                                        if (_game.Items.Has(ItemType.Hookshot))
                                            return AccessibilityLevel.Normal;

                                        if (Available > 4)
                                            return AccessibilityLevel.Partial;
                                    }

                                    if (Available > 7)
                                        return AccessibilityLevel.Partial;
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
                                                return AccessibilityLevel.Normal;

                                            if (Available > 4)
                                                return AccessibilityLevel.Partial;
                                        }

                                        if (Available > 7)
                                            return AccessibilityLevel.Partial;
                                    }

                                    if (Available > 8)
                                        return AccessibilityLevel.Partial;
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
                                                return AccessibilityLevel.Normal;

                                            if (Available > 1 && _game.Items.Has(ItemType.Hookshot))
                                                return AccessibilityLevel.Partial;

                                            if (Available > 4 && _game.Items.Has(ItemType.SPBigKey))
                                                return AccessibilityLevel.Partial;

                                            if (Available > 5)
                                                return AccessibilityLevel.Partial;
                                        }

                                        if (Available > 8)
                                            return AccessibilityLevel.Partial;
                                    }

                                    if (Available > 9)
                                        return AccessibilityLevel.Partial;
                                }

                                break;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Flippers]);
                    itemReqs.Add(_game.Items[ItemType.Hookshot]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.SPSmallKey]);
                    itemReqs.Add(_game.Items[ItemType.SPBigKey]);

                    PropertyChanged += OnRequirementChanged;

                    break;
                case LocationID.SkullWoods:

                    _mapCompass = 2;
                    _smallKey = 3;
                    _bigKey = 1;
                    _baseTotal = 2;
                    Name = "Dungeon";
                    _standardRegion = _game.Regions[RegionID.SkullWoods];
                    _invertedRegion = _game.Regions[RegionID.SkullWoods];

                    GetAccessibility = () =>
                    {
                        switch (_game.Mode.DungeonItemShuffle.Value)
                        {
                            case DungeonItemShuffle.Standard:
                            case DungeonItemShuffle.MapsCompasses:

                                if ((_game.Items.Has(ItemType.FireRod) || _game.Mode.EntranceShuffle.Value) &&
                                    _game.Items.CanRemoveCurtains())
                                    return AccessibilityLevel.Normal;

                                return AccessibilityLevel.SequenceBreak;

                            case DungeonItemShuffle.MapsCompassesSmallKeys:

                                if (_game.Items.Has(ItemType.FireRod) || _game.Mode.EntranceShuffle.Value)
                                {
                                    if (_game.Items.CanRemoveCurtains())
                                    {
                                        if (_game.Items.Has(ItemType.SWSmallKey))
                                            return AccessibilityLevel.Normal;

                                        return AccessibilityLevel.SequenceBreak;
                                    }

                                    if (Available > 1)
                                        return AccessibilityLevel.Partial;
                                }

                                if (Available > 2)
                                    return AccessibilityLevel.Partial;

                                break;
                            case DungeonItemShuffle.Keysanity:

                                if (_game.Items.Has(ItemType.FireRod) || _game.Mode.EntranceShuffle.Value)
                                {
                                    if (_game.Items.CanRemoveCurtains())
                                    {
                                        if (_game.Items.Has(ItemType.SWBigKey) && _game.Items.Has(ItemType.SPSmallKey))
                                            return AccessibilityLevel.Normal;

                                        if (_game.Items.Has(ItemType.SWBigKey))
                                            return AccessibilityLevel.SequenceBreak;

                                        if (Available > 1)
                                            return AccessibilityLevel.Partial;
                                    }

                                    if (Available > 1 && _game.Items.Has(ItemType.SWBigKey))
                                        return AccessibilityLevel.Partial;

                                    if (Available > 2)
                                        return AccessibilityLevel.Partial;
                                }

                                if (Available > 2 && _game.Items.Has(ItemType.SWBigKey))
                                    return AccessibilityLevel.Partial;

                                if (Available > 3)
                                    return AccessibilityLevel.Partial;

                                break;
                        }
                        
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.FireRod]);
                    itemReqs.Add(_game.Items[ItemType.Sword]);
                    itemReqs.Add(_game.Items[ItemType.SWBigKey]);
                    itemReqs.Add(_game.Items[ItemType.SWSmallKey]);

                    PropertyChanged += OnRequirementChanged;

                    break;
                case LocationID.ThievesTown:

                    _mapCompass = 2;
                    _smallKey = 1;
                    _bigKey = 1;
                    _baseTotal = 4;
                    Name = "Dungeon";
                    _standardRegion = _game.Regions[RegionID.ThievesTown];
                    _invertedRegion = _game.Regions[RegionID.ThievesTown];

                    GetAccessibility = () =>
                    {
                        switch (_game.Mode.DungeonItemShuffle.Value)
                        {
                            case DungeonItemShuffle.Standard:
                            case DungeonItemShuffle.MapsCompasses:

                                if (_game.Items.Has(ItemType.Hammer))
                                    return AccessibilityLevel.Normal;

                                return AccessibilityLevel.SequenceBreak;

                            case DungeonItemShuffle.MapsCompassesSmallKeys:

                                if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.TTSmallKey))
                                    return AccessibilityLevel.Normal;

                                return AccessibilityLevel.SequenceBreak;

                            case DungeonItemShuffle.Keysanity:

                                if (_game.Items.Has(ItemType.TTBigKey))
                                {
                                    if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.TTSmallKey))
                                        return AccessibilityLevel.Normal;

                                    if (Available > 1)
                                        return AccessibilityLevel.Partial;
                                }

                                if (Available > 4)
                                    return AccessibilityLevel.Partial;

                                break;
                        }
                        
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.TTBigKey]);
                    itemReqs.Add(_game.Items[ItemType.TTSmallKey]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);

                    PropertyChanged += OnRequirementChanged;

                    break;
                case LocationID.IcePalace:

                    _mapCompass = 2;
                    _smallKey = 2;
                    _bigKey = 1;
                    _baseTotal = 3;
                    Name = "Dungeon";
                    _standardRegion = _game.Regions[RegionID.IcePalace];
                    _invertedRegion = _game.Regions[RegionID.IcePalace];

                    GetAccessibility = () =>
                    {
                        switch (_game.Mode.DungeonItemShuffle.Value)
                        {
                            case DungeonItemShuffle.Standard:

                                if (_game.Items.CanMeltThings())
                                {
                                    if (_game.Items.Has(ItemType.Hammer) &&
                                        (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.CaneOfSomaria)))
                                        return AccessibilityLevel.Normal;

                                    return AccessibilityLevel.SequenceBreak;
                                }

                                break;
                            case DungeonItemShuffle.MapsCompasses:

                                if (_game.Items.CanMeltThings())
                                {
                                    if (_game.Items.Has(ItemType.Hammer))
                                    {
                                        if (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.CaneOfSomaria))
                                            return AccessibilityLevel.Normal;

                                        return AccessibilityLevel.SequenceBreak;
                                    }

                                    if (Available > 1)
                                        return AccessibilityLevel.Partial;
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
                                            return AccessibilityLevel.Normal;

                                        return AccessibilityLevel.SequenceBreak;
                                    }

                                    if (Available > 3)
                                        return AccessibilityLevel.Partial;
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
                                            return AccessibilityLevel.Normal;

                                        if (_game.Items.Has(ItemType.IPBigKey))
                                            return AccessibilityLevel.SequenceBreak;

                                        if (Available > 1)
                                            return AccessibilityLevel.Partial;
                                    }

                                    if (Available > 3 && _game.Items.Has(ItemType.IPBigKey))
                                        return AccessibilityLevel.Partial;

                                    if (Available > 4)
                                        return AccessibilityLevel.Partial;
                                }

                                break;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.FireRod]);
                    itemReqs.Add(_game.Items[ItemType.Bombos]);
                    itemReqs.Add(_game.Items[ItemType.Sword]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.Hookshot]);
                    itemReqs.Add(_game.Items[ItemType.CaneOfSomaria]);
                    itemReqs.Add(_game.Items[ItemType.IPSmallKey]);
                    itemReqs.Add(_game.Items[ItemType.IPBigKey]);

                    PropertyChanged += OnRequirementChanged;

                    break;
                case LocationID.MiseryMire:

                    _mapCompass = 2;
                    _smallKey = 3;
                    _bigKey = 1;
                    _baseTotal = 2;
                    Name = "Dungeon";
                    _standardRegion = _game.Regions[RegionID.MiseryMire];
                    _invertedRegion = _game.Regions[RegionID.MiseryMire];

                    GetAccessibility = () =>
                    {
                        switch (_game.Mode.DungeonItemShuffle.Value)
                        {
                            case DungeonItemShuffle.Standard:
                            case DungeonItemShuffle.MapsCompasses:

                                if (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots))
                                {
                                    if (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod))
                                    {
                                        if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        {
                                            if ((_game.Items.Has(ItemType.Hookshot) ||
                                                (_game.Items.Has(ItemType.Boots) && _game.Mode.ItemPlacement == ItemPlacement.Advanced)) &&
                                                _game.Items.Has(ItemType.Lamp))
                                                return AccessibilityLevel.Normal;
                                        }
                                    }

                                    return AccessibilityLevel.SequenceBreak;
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
                                                return AccessibilityLevel.Normal;

                                            return AccessibilityLevel.SequenceBreak;
                                        }

                                        if (Available > 1)
                                            return AccessibilityLevel.Partial;
                                    }

                                    if (Available > 2 && _game.Items.Has(ItemType.CaneOfSomaria))
                                        return AccessibilityLevel.Partial;

                                    if (Available > 3)
                                        return AccessibilityLevel.Partial;
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
                                                return AccessibilityLevel.Normal;

                                            return AccessibilityLevel.SequenceBreak;
                                        }

                                        if (Available > 1 && _game.Items.Has(ItemType.MMBigKey))
                                            return AccessibilityLevel.Partial;

                                        if (Available > 2)
                                            return AccessibilityLevel.Partial;
                                    }

                                    if (Available > 2 &&
                                        _game.Items.Has(ItemType.CaneOfSomaria) && _game.Items.Has(ItemType.MMBigKey))
                                        return AccessibilityLevel.Partial;

                                    if (Available > 3 && _game.Items.Has(ItemType.MMBigKey))
                                        return AccessibilityLevel.Partial;

                                    if (Available > 4)
                                        return AccessibilityLevel.Partial;
                                }

                                break;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Hookshot]);
                    itemReqs.Add(_game.Items[ItemType.Boots]);
                    itemReqs.Add(_game.Items[ItemType.Lamp]);
                    itemReqs.Add(_game.Items[ItemType.FireRod]);
                    itemReqs.Add(_game.Items[ItemType.CaneOfSomaria]);
                    itemReqs.Add(_game.Items[ItemType.MMBigKey]);

                    PropertyChanged += OnRequirementChanged;

                    break;
                case LocationID.TurtleRock:

                    _mapCompass = 2;
                    _smallKey = 4;
                    _bigKey = 1;
                    _baseTotal = 5;
                    Name = "Dungeon";
                    _standardRegion = _game.Regions[RegionID.TurtleRock];
                    _invertedRegion = _game.Regions[RegionID.TurtleRock];

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

                        switch (_game.Mode.DungeonItemShuffle.Value)
                        {
                            case DungeonItemShuffle.Standard:

                                if (_game.Items.Has(ItemType.CaneOfSomaria) && _game.Items.Has(ItemType.FireRod) &&
                                    _game.Items.Has(ItemType.Lamp) && (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                    _game.Mode.WorldState == WorldState.Inverted || _game.Items.Has(ItemType.Cape) ||
                                    _game.Items.Has(ItemType.CaneOfByrna) || _game.Items.Has(ItemType.Shield, 3)))
                                    return AccessibilityLevel.Normal;

                                if (backAccess() >= AccessibilityLevel.SequenceBreak)
                                    return AccessibilityLevel.SequenceBreak;

                                if (frontAccess() >= AccessibilityLevel.SequenceBreak && _game.Items.Has(ItemType.CaneOfSomaria))
                                    return AccessibilityLevel.SequenceBreak;

                                break;
                            case DungeonItemShuffle.MapsCompasses:

                                if (_game.Items.Has(ItemType.CaneOfSomaria) && _game.Items.Has(ItemType.FireRod) &&
                                    _game.Items.Has(ItemType.Lamp) && (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                    _game.Mode.WorldState == WorldState.Inverted || _game.Items.Has(ItemType.Cape) ||
                                    _game.Items.Has(ItemType.CaneOfByrna) || _game.Items.Has(ItemType.Shield, 3)))
                                    return AccessibilityLevel.Normal;

                                if (backAccess() >= AccessibilityLevel.SequenceBreak)
                                    return AccessibilityLevel.SequenceBreak;

                                if (frontAccess() >= AccessibilityLevel.SequenceBreak)
                                {
                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                    {
                                        if (_game.Items.Has(ItemType.FireRod))
                                            return AccessibilityLevel.SequenceBreak;

                                        if (Available > 2)
                                            return AccessibilityLevel.Partial;
                                    }
                                }

                                break;
                            case DungeonItemShuffle.MapsCompassesSmallKeys:

                                if (backAccess() >= AccessibilityLevel.SequenceBreak)
                                {
                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                    {
                                        if (_game.Items.Has(ItemType.TRSmallKey, 2) ||
                                            (_game.Items.Has(ItemType.TRSmallKey) && frontAccess() >= AccessibilityLevel.SequenceBreak))
                                        {
                                            if (_game.Items.Has(ItemType.FireRod) && (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                                _game.Mode.WorldState == WorldState.Inverted || _game.Items.Has(ItemType.Cape) ||
                                                _game.Items.Has(ItemType.CaneOfByrna) || _game.Items.Has(ItemType.Shield, 3)))
                                                return AccessibilityLevel.Normal;

                                            if (Available > 2)
                                                return AccessibilityLevel.Partial;
                                        }

                                        if (_game.Items.Has(ItemType.TRSmallKey) || frontAccess() >= AccessibilityLevel.SequenceBreak)
                                        {
                                            if (Available > 1 && _game.Items.Has(ItemType.FireRod))
                                                return AccessibilityLevel.Partial;

                                            if (Available > 3)
                                                return AccessibilityLevel.Partial;
                                        }
                                    }

                                    if (Available > 4)
                                        return AccessibilityLevel.Partial;
                                }

                                if (frontAccess() >= AccessibilityLevel.SequenceBreak)
                                {
                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                    {
                                        if (_game.Items.Has(ItemType.TRSmallKey, 4))
                                        {
                                            if (_game.Items.Has(ItemType.FireRod))
                                            {
                                                if (_game.Items.Has(ItemType.Lamp) && (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                                    _game.Mode.WorldState == WorldState.Inverted || _game.Items.Has(ItemType.Cape) ||
                                                    _game.Items.Has(ItemType.CaneOfByrna) || _game.Items.Has(ItemType.Shield, 3)))
                                                    return AccessibilityLevel.Normal;

                                                return AccessibilityLevel.SequenceBreak;
                                            }

                                            if (Available > 2)
                                                return AccessibilityLevel.Partial;
                                        }

                                        if (_game.Items.Has(ItemType.TRSmallKey, 3))
                                        {
                                            if (Available > 1 && _game.Items.Has(ItemType.FireRod))
                                                return AccessibilityLevel.Partial;

                                            if (Available > 3)
                                                return AccessibilityLevel.Partial;
                                        }

                                        if (_game.Items.Has(ItemType.TRSmallKey, 2))
                                        {
                                            if (Available > 5 && _game.Items.Has(ItemType.FireRod))
                                                return AccessibilityLevel.Partial;

                                            if (Available > 7)
                                                return AccessibilityLevel.Partial;
                                        }

                                        if (_game.Items.Has(ItemType.TRSmallKey))
                                        {
                                            if (Available > 7 && _game.Items.Has(ItemType.FireRod))
                                                return AccessibilityLevel.Partial;

                                            if (Available > 9)
                                                return AccessibilityLevel.Partial;
                                        }

                                        if (Available > 8 && _game.Items.Has(ItemType.FireRod))
                                            return AccessibilityLevel.Partial;

                                        if (Available > 10)
                                            return AccessibilityLevel.Partial;
                                    }
                                }

                                break;
                            case DungeonItemShuffle.Keysanity:

                                if (backAccess() >= AccessibilityLevel.SequenceBreak)
                                {
                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                    {
                                        if (frontAccess() >= AccessibilityLevel.SequenceBreak)
                                        {
                                            if (_game.Items.Has(ItemType.FireRod) && _game.Items.Has(ItemType.TRSmallKey) &&
                                                _game.Items.Has(ItemType.TRBigKey) && (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                                _game.Mode.WorldState == WorldState.Inverted || _game.Items.Has(ItemType.Cape) ||
                                                _game.Items.Has(ItemType.CaneOfByrna) || _game.Items.Has(ItemType.Shield, 3)))
                                                return AccessibilityLevel.Normal;

                                            if (Available > 4)
                                                return AccessibilityLevel.Partial;

                                            if (Available > 3 && _game.Items.Has(ItemType.TRBigKey))
                                                return AccessibilityLevel.Partial;

                                            if (Available > 2 &&
                                                _game.Items.Has(ItemType.TRSmallKey) && _game.Items.Has(ItemType.TRBigKey))
                                                return AccessibilityLevel.Partial;

                                            if (Available > 2 && _game.Items.Has(ItemType.FireRod))
                                                return AccessibilityLevel.Partial;

                                            if (Available > 1 &&
                                                _game.Items.Has(ItemType.FireRod) && _game.Items.Has(ItemType.TRBigKey))
                                                return AccessibilityLevel.Partial;
                                        }

                                        if (_game.Items.Has(ItemType.FireRod) && _game.Items.Has(ItemType.TRSmallKey, 2) &&
                                            _game.Items.Has(ItemType.TRBigKey) && (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                            _game.Mode.WorldState == WorldState.Inverted || _game.Items.Has(ItemType.Cape) ||
                                            _game.Items.Has(ItemType.CaneOfByrna) || _game.Items.Has(ItemType.Shield, 3)))
                                            return AccessibilityLevel.Normal;

                                        if (Available > 4 && _game.Items.Has(ItemType.TRSmallKey))
                                            return AccessibilityLevel.Partial;

                                        if (Available > 4 && _game.Items.Has(ItemType.TRBigKey))
                                            return AccessibilityLevel.Partial;

                                        if (Available > 3 &&
                                            _game.Items.Has(ItemType.TRSmallKey) && _game.Items.Has(ItemType.TRBigKey))
                                            return AccessibilityLevel.Partial;

                                        if (Available > 2 &&
                                            _game.Items.Has(ItemType.TRSmallKey) && _game.Items.Has(ItemType.FireRod))
                                            return AccessibilityLevel.Partial;

                                        if (Available > 2 &&
                                            _game.Items.Has(ItemType.TRSmallKey, 2) && _game.Items.Has(ItemType.TRBigKey))
                                            return AccessibilityLevel.Partial;

                                        if (Available > 1 && _game.Items.Has(ItemType.FireRod) &&
                                            _game.Items.Has(ItemType.TRSmallKey) && _game.Items.Has(ItemType.TRBigKey))
                                            return AccessibilityLevel.Partial;
                                    }

                                    if (Available > 4 &&
                                        _game.Items.Has(ItemType.TRBigKey) && _game.Items.Has(ItemType.Hookshot))
                                        return AccessibilityLevel.Partial;

                                    if (Available > 5)
                                        return AccessibilityLevel.Partial;
                                }

                                if (frontAccess() >= AccessibilityLevel.SequenceBreak)
                                {
                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                    {
                                        if (_game.Items.Has(ItemType.TRSmallKey, 4) && _game.Items.Has(ItemType.TRBigKey))
                                        {
                                            if (_game.Items.Has(ItemType.FireRod))
                                            {
                                                if (_game.Items.Has(ItemType.Lamp))
                                                    return AccessibilityLevel.Normal;

                                                return AccessibilityLevel.SequenceBreak;
                                            }
                                            
                                            if (Available > 2)
                                                return AccessibilityLevel.Partial;
                                        }

                                        if (_game.Items.Has(ItemType.TRSmallKey, 3) && _game.Items.Has(ItemType.TRBigKey))
                                        {
                                            if (Available > 1 && _game.Items.Has(ItemType.FireRod))
                                                return AccessibilityLevel.Partial;
                                            
                                            if (Available > 3)
                                                return AccessibilityLevel.Partial;
                                        }

                                        if (_game.Items.Has(ItemType.TRSmallKey, 2))
                                        {
                                            if (_game.Items.Has(ItemType.TRBigKey))
                                            {
                                                if (Available > 5 && _game.Items.Has(ItemType.FireRod))
                                                    return AccessibilityLevel.Partial;

                                                if (Available > 7)
                                                    return AccessibilityLevel.Partial;
                                            }

                                            if (Available > 7 && _game.Items.Has(ItemType.FireRod))
                                                return AccessibilityLevel.Partial;

                                            if (Available > 9)
                                                return AccessibilityLevel.Partial;
                                        }

                                        if (_game.Items.Has(ItemType.TRSmallKey))
                                        {
                                            if (Available > 8 && _game.Items.Has(ItemType.FireRod))
                                                return AccessibilityLevel.Partial;

                                            if (Available > 10)
                                                return AccessibilityLevel.Partial;
                                        }

                                        if (Available > 9 && _game.Items.Has(ItemType.FireRod))
                                            return AccessibilityLevel.Partial;

                                        if (Available > 11)
                                            return AccessibilityLevel.Partial;
                                    }
                                }

                                break;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.Sword]);
                    itemReqs.Add(_game.Items[ItemType.Bombos]);
                    itemReqs.Add(_game.Items[ItemType.BombosDungeons]);
                    itemReqs.Add(_game.Items[ItemType.Ether]);
                    itemReqs.Add(_game.Items[ItemType.EtherDungeons]);
                    itemReqs.Add(_game.Items[ItemType.Quake]);
                    itemReqs.Add(_game.Items[ItemType.QuakeDungeons]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.CaneOfSomaria]);
                    itemReqs.Add(_game.Items[ItemType.FireRod]);
                    itemReqs.Add(_game.Items[ItemType.Lamp]);
                    itemReqs.Add(_game.Items[ItemType.Cape]);
                    itemReqs.Add(_game.Items[ItemType.CaneOfByrna]);
                    itemReqs.Add(_game.Items[ItemType.Shield]);
                    itemReqs.Add(_game.Items[ItemType.TRSmallKey]);
                    itemReqs.Add(_game.Items[ItemType.TRBigKey]);
                    itemReqs.Add(_game.Items[ItemType.Hookshot]);

                    _game.Regions[RegionID.DeathMountainEastTop].PropertyChanged += OnRequirementChanged;
                    _game.Regions[RegionID.DarkDeathMountainTop].PropertyChanged += OnRequirementChanged;

                    PropertyChanged += OnRequirementChanged;

                    break;
                case LocationID.GanonsTower:

                    _mapCompass = 2;
                    _smallKey = 4;
                    _bigKey = 1;
                    _baseTotal = 19;
                    Name = "Dungeon";
                    _standardRegion = _game.Regions[RegionID.GanonsTower];
                    _invertedRegion = _game.Regions[RegionID.GanonsTower];

                    GetAccessibility = () =>
                    {
                        switch (_game.Mode.DungeonItemShuffle.Value)
                        {
                            case DungeonItemShuffle.Standard:

                                if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Hookshot) &&
                                    _game.Items.Has(ItemType.Boots) && _game.Items.Has(ItemType.CaneOfSomaria) &&
                                    (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value) &&
                                    _game.Items.Has(ItemType.FireRod))
                                    return AccessibilityLevel.Normal;

                                if (_game.Items.Has(ItemType.Hammer) &&
                                    (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots)))
                                {
                                    if (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp))
                                    {
                                        if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        {
                                            if (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value)
                                                return AccessibilityLevel.SequenceBreak;

                                            if (Available > 1)
                                                return AccessibilityLevel.Partial;
                                        }

                                        if (Available > 2 &&
                                            (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value))
                                            return AccessibilityLevel.Partial;

                                        if (Available > 5)
                                            return AccessibilityLevel.Partial;
                                    }

                                    if (Available > 5)
                                        return AccessibilityLevel.Partial;
                                }

                                if (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp))
                                {
                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                    {
                                        if (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value)
                                        {
                                            if (Available > 4 &&
                                                (_game.Items.Has(ItemType.Boots) || _game.Items.Has(ItemType.Hookshot)))
                                                return AccessibilityLevel.Partial;

                                            if (Available > 5)
                                                return AccessibilityLevel.Partial;
                                        }

                                        if (Available > 7 &&
                                            (_game.Items.Has(ItemType.Boots) || _game.Items.Has(ItemType.Hookshot)))
                                            return AccessibilityLevel.Partial;

                                        if (Available > 8)
                                            return AccessibilityLevel.Partial;
                                    }

                                    if (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value)
                                    {
                                        if (Available > 13 &&
                                            (_game.Items.Has(ItemType.Boots) || _game.Items.Has(ItemType.Hookshot)))
                                            return AccessibilityLevel.Partial;

                                        if (Available > 14)
                                            return AccessibilityLevel.Partial;
                                    }

                                }

                                if (Available > 15 && _game.Items.Has(ItemType.CaneOfSomaria))
                                    return AccessibilityLevel.Partial;

                                if (Available > 16)
                                    return AccessibilityLevel.Partial;

                                break;
                            case DungeonItemShuffle.MapsCompasses:

                                if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Hookshot) &&
                                    _game.Items.Has(ItemType.Boots) && _game.Items.Has(ItemType.CaneOfSomaria) &&
                                    (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value) &&
                                    _game.Items.Has(ItemType.FireRod))
                                    return AccessibilityLevel.Normal;

                                if (_game.Items.Has(ItemType.Hammer) &&
                                    (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots)))
                                {
                                    if (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp))
                                    {
                                        if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        {
                                            if (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value)
                                                return AccessibilityLevel.SequenceBreak;

                                            if (Available > 3)
                                                return AccessibilityLevel.Partial;
                                        }

                                        if (Available > 4 &&
                                            (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value))
                                            return AccessibilityLevel.Partial;

                                        if (Available > 7)
                                            return AccessibilityLevel.Partial;
                                    }

                                    if (Available > 7)
                                        return AccessibilityLevel.Partial;
                                }

                                if (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp))
                                {
                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                    {
                                        if (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value)
                                        {
                                            if (Available > 6 &&
                                                (_game.Items.Has(ItemType.Boots) || _game.Items.Has(ItemType.Hookshot)))
                                                return AccessibilityLevel.Partial;

                                            if (Available > 7)
                                                return AccessibilityLevel.Partial;
                                        }

                                        if (Available > 10)
                                            return AccessibilityLevel.Partial;
                                    }

                                    if (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value)
                                    {
                                        if (Available > 15 &&
                                            (_game.Items.Has(ItemType.Boots) || _game.Items.Has(ItemType.Hookshot)))
                                            return AccessibilityLevel.Partial;

                                        if (Available > 16)
                                            return AccessibilityLevel.Partial;
                                    }
                                }

                                if (Available > 17 && _game.Items.Has(ItemType.CaneOfSomaria))
                                    return AccessibilityLevel.Partial;

                                if (Available > 18)
                                    return AccessibilityLevel.Partial;

                                break;
                            case DungeonItemShuffle.MapsCompassesSmallKeys:

                                if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Hookshot) &&
                                    _game.Items.Has(ItemType.Boots) && _game.Items.Has(ItemType.CaneOfSomaria) &&
                                    (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value) &&
                                    _game.Items.Has(ItemType.FireRod) && _game.Items.Has(ItemType.GTSmallKey, 2))
                                    return AccessibilityLevel.Normal;

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
                                                    return AccessibilityLevel.SequenceBreak;

                                                if (Available > 1)
                                                    return AccessibilityLevel.Partial;
                                            }

                                            if (Available > 4)
                                                return AccessibilityLevel.Partial;
                                        }

                                        if (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value)
                                        {
                                            if (Available > 5 && _game.Items.Has(ItemType.GTSmallKey, 2))
                                                return AccessibilityLevel.Partial;

                                            if (Available > 6 && _game.Items.Has(ItemType.GTSmallKey))
                                                return AccessibilityLevel.Partial;

                                            if (Available > 7)
                                                return AccessibilityLevel.Partial;
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                    {
                                        if (Available > 8 && _game.Items.Has(ItemType.GTSmallKey))
                                            return AccessibilityLevel.Partial;

                                        if (Available > 9)
                                            return AccessibilityLevel.Partial;
                                    }

                                    if (Available > 9 && _game.Items.Has(ItemType.GTSmallKey))
                                        return AccessibilityLevel.Partial;

                                    if (Available > 10)
                                        return AccessibilityLevel.Partial;
                                }

                                if (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp))
                                {
                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                    {
                                        if (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value)
                                        {
                                            if (Available > 9 && _game.Items.Has(ItemType.GTSmallKey) &&
                                                (_game.Items.Has(ItemType.Boots) || _game.Items.Has(ItemType.Hookshot)))
                                                return AccessibilityLevel.Partial;

                                            if (Available > 10)
                                                return AccessibilityLevel.Partial;
                                        }

                                        if (Available > 13)
                                            return AccessibilityLevel.Partial;
                                    }

                                    if (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value)
                                    {
                                        if (Available > 19 &&
                                            (_game.Items.Has(ItemType.Boots) || _game.Items.Has(ItemType.Hookshot)))
                                            return AccessibilityLevel.Partial;

                                        if (Available > 20)
                                            return AccessibilityLevel.Partial;
                                    }
                                }

                                if (Available > 21 && _game.Items.Has(ItemType.CaneOfSomaria))
                                    return AccessibilityLevel.Partial;

                                if (Available > 22)
                                    return AccessibilityLevel.Partial;

                                break;
                            case DungeonItemShuffle.Keysanity:

                                if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Hookshot) &&
                                    _game.Items.Has(ItemType.Boots) && _game.Items.Has(ItemType.CaneOfSomaria) &&
                                    (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value) &&
                                    _game.Items.Has(ItemType.FireRod) && _game.Items.Has(ItemType.GTSmallKey, 2) &&
                                    _game.Items.Has(ItemType.GTBigKey))
                                    return AccessibilityLevel.Normal;

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
                                                        return AccessibilityLevel.SequenceBreak;

                                                    if (Available > 1)
                                                        return AccessibilityLevel.Partial;
                                                }

                                                if (Available > 4)
                                                    return AccessibilityLevel.Partial;
                                            }

                                            if (Available > 5)
                                                return AccessibilityLevel.Partial;
                                        }

                                        if (_game.Items.Has(ItemType.GTBigKey))
                                        {
                                            if (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value)
                                            {
                                                if (Available > 5 && _game.Items.Has(ItemType.GTSmallKey, 2))
                                                    return AccessibilityLevel.Partial;

                                                if (Available > 6 && _game.Items.Has(ItemType.GTSmallKey))
                                                    return AccessibilityLevel.Partial;

                                                if (Available > 7)
                                                    return AccessibilityLevel.Partial;
                                            }
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                    {
                                        if (Available > 8 &&
                                            _game.Items.Has(ItemType.GTBigKey) && _game.Items.Has(ItemType.GTSmallKey))
                                            return AccessibilityLevel.Partial;

                                        if (Available > 9 && _game.Items.Has(ItemType.GTSmallKey))
                                            return AccessibilityLevel.Partial;

                                        if (Available > 9 && _game.Items.Has(ItemType.GTBigKey))
                                            return AccessibilityLevel.Partial;

                                        if (Available > 10)
                                            return AccessibilityLevel.Partial;
                                    }

                                    if (Available > 9 &&
                                        _game.Items.Has(ItemType.GTSmallKey) && _game.Items.Has(ItemType.GTBigKey))
                                        return AccessibilityLevel.Partial;

                                    if (Available > 10 && _game.Items.Has(ItemType.GTSmallKey))
                                        return AccessibilityLevel.Partial;

                                    if (Available > 10 && _game.Items.Has(ItemType.GTBigKey))
                                        return AccessibilityLevel.Partial;

                                    if (Available > 11)
                                        return AccessibilityLevel.Partial;
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
                                                    return AccessibilityLevel.Partial;

                                                if (Available > 11)
                                                    return AccessibilityLevel.Partial;
                                            }

                                            if (Available > 13)
                                                return AccessibilityLevel.Partial;
                                        }

                                        if (Available > 14)
                                            return AccessibilityLevel.Partial;
                                    }

                                    if ((_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value) &&
                                        _game.Items.Has(ItemType.GTBigKey))
                                    {
                                        if (Available > 20 &&
                                            (_game.Items.Has(ItemType.Boots) || _game.Items.Has(ItemType.Hookshot)))
                                            return AccessibilityLevel.Partial;

                                        if (Available > 21)
                                            return AccessibilityLevel.Partial;
                                    }
                                }

                                if (Available > 22 && _game.Items.Has(ItemType.CaneOfSomaria))
                                    return AccessibilityLevel.Partial;

                                if (Available > 23)
                                    return AccessibilityLevel.Partial;

                                break;
                        }
                        
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.Hookshot]);
                    itemReqs.Add(_game.Items[ItemType.Boots]);
                    itemReqs.Add(_game.Items[ItemType.CaneOfSomaria]);
                    itemReqs.Add(_game.Items[ItemType.Bow]);
                    itemReqs.Add(_game.Items[ItemType.FireRod]);
                    itemReqs.Add(_game.Items[ItemType.Lamp]);
                    itemReqs.Add(_game.Items[ItemType.GTSmallKey]);
                    itemReqs.Add(_game.Items[ItemType.GTBigKey]);

                    PropertyChanged += OnRequirementChanged;

                    break;
            }

            Total = _baseTotal;
            Available = _baseTotal;

            foreach (Item item in itemReqs)
                item.PropertyChanged += OnRequirementChanged;

            if (RequiredMode == null)
                RequiredMode = new Mode();

            if (_standardRegion != null)
                _standardRegion.PropertyChanged += OnRequirementChanged;

            if (_invertedRegion != null)
                _invertedRegion.PropertyChanged += OnRequirementChanged;

            UpdateAccessibility();
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
            AccessibilityLevel? regionAccessibility = null;
            
            if (_game.Mode.WorldState == WorldState.StandardOpen && _standardRegion != null)
                regionAccessibility = _standardRegion.Accessibility;
            
            if (_game.Mode.WorldState == WorldState.Inverted && _invertedRegion != null)
                regionAccessibility = _invertedRegion.Accessibility;
            
            if (regionAccessibility != null)
            {
                AccessibilityLevel sectionAccessibility = GetAccessibility();
                Accessibility = (AccessibilityLevel)Math.Min((byte)regionAccessibility.Value,
                    (byte)sectionAccessibility);
            }
            else
                Accessibility = GetAccessibility();
        }
        
        private void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateAccessibility();
        }

        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateAccessibility();

            if (e.PropertyName == nameof(_game.Mode.DungeonItemShuffle))
                SetTotal();
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
