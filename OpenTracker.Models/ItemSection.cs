using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        private readonly bool _subscribeToRoomMemory;
        private readonly bool _subscribeToOverworldEventMemory;
        private readonly bool _subscribeToItemMemory;
        private readonly bool _subscribeToNPCItemMemory;

        public string Name { get; }
        public bool HasMarking { get; }
        public Mode RequiredMode { get; }
        public Action AutoTrack { get; }
        public Func<AccessibilityLevel> GetAccessibility { get; }
        public Func<int> GetAccessible { get; }
        public bool UserManipulated { get; set; }

        public event PropertyChangingEventHandler PropertyChanging;
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

        private MarkingType? _marking;
        public MarkingType? Marking
        {
            get => _marking;
            set
            {
                if (_marking != value)
                {
                    OnPropertyChanging(nameof(Marking));
                    _marking = value;
                    OnPropertyChanged(nameof(Marking));
                }
            }
        }

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
                        AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                        if (_game.Items.Has(ItemType.GreenPendant) && _game.Items.Has(ItemType.Pendant, 2))
                        {
                            //  Basic Item Placement requires Book in logic
                            if (_game.Mode.ItemPlacement == ItemPlacement.Advanced || _game.Items.Has(ItemType.Book))
                                return lightWorld;

                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)lightWorld);
                        }

                        if (_game.Items.Has(ItemType.Book))
                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Inspect, (byte)lightWorld);

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Items.Has(ItemType.GreenPendant) && _game.Items.Has(ItemType.Pendant, 2) &&
                            _game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.OverworldEvent, 128, 64);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToOverworldEventMemory = true;
                    _updateOnItemPlacementChange = true;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.GreenPendant, new Mode());
                    _itemSubscriptions.Add(ItemType.Pendant, new Mode());
                    _itemSubscriptions.Add(ItemType.Book, new Mode());

                    break;
                case LocationID.LumberjackCave:

                    _baseTotal = 1;
                    Name = "Cave";
                    HasMarking = true;

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                        if (_game.Items.Has(ItemType.Aga) && _game.Items.Has(ItemType.Boots) &&
                            (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)))
                            return lightWorld;
                        
                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Inspect, (byte)lightWorld);
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Items.Has(ItemType.Aga) && _game.Items.Has(ItemType.Boots) &&
                            (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)) &&
                            _game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 453, 2);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToRoomMemory = true;

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
                        if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                            return _game.Regions[RegionID.LightWorld].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if ((_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)) &&
                            _game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        (MemorySegmentType, int, byte)[] addressFlags = new (MemorySegmentType, int, byte)[4]
                        {
                            (MemorySegmentType.Room, 570, 32),
                            (MemorySegmentType.Room, 570, 64),
                            (MemorySegmentType.Room, 570, 128),
                            (MemorySegmentType.Room, 571, 1)
                        };

                        int? result = _game.AutoTracker.CheckMemoryFlagArray(addressFlags);

                        if (result.HasValue)
                            Available = Total - result.Value;
                    };

                    _subscribeToRoomMemory = true;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.BlindsHouse:

                    _baseTotal = 1;
                    Name = "Bomb";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                            return _game.Regions[RegionID.LightWorld].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if ((_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)) &&
                            _game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 570, 16);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToRoomMemory = true;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.TheWell when index == 0:

                    _baseTotal = 4;
                    Name = "Cave";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                            return _game.Regions[RegionID.LightWorld].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if ((_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)) &&
                            _game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        (MemorySegmentType, int, byte)[] addressFlags = new (MemorySegmentType, int, byte)[4]
                        {
                            (MemorySegmentType.Room, 94, 32),
                            (MemorySegmentType.Room, 94, 64),
                            (MemorySegmentType.Room, 94, 128),
                            (MemorySegmentType.Room, 95, 1)
                        };

                        int? result = _game.AutoTracker.CheckMemoryFlagArray(addressFlags);

                        if (result.HasValue)
                            Available = Total - result.Value;
                    };

                    _subscribeToRoomMemory = true;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.TheWell:

                    _baseTotal = 1;
                    Name = "Bomb";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                            return _game.Regions[RegionID.LightWorld].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if ((_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)) &&
                            _game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 94, 16);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToRoomMemory = true;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.BottleVendor:

                    _baseTotal = 1;
                    Name = "Man";

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    GetAccessible = () =>
                    {
                        if (_game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 137, 2);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToItemMemory = true;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
                case LocationID.ChickenHouse:

                    _baseTotal = 1;
                    Name = "Bombable Wall";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.LightWorld].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if ((_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)) &&
                            _game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 528, 16);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToRoomMemory = true;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.Tavern:

                    _baseTotal = 1;
                    Name = "Back Room";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.LightWorld].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if ((_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)) &&
                            _game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 518, 16);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToRoomMemory = true;

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

                    GetAccessible = () =>
                    {
                        if (_game.Items.Has(ItemType.Bottle) &&
                            _game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 0, 4);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToNPCItemMemory = true;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.Bottle, new Mode());

                    break;
                case LocationID.MagicBat:

                    _baseTotal = 1;
                    Name = "Magic Bowl";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Powder))
                            {
                                if (_game.Items.Has(ItemType.Hammer))
                                    return lightWorld;

                                if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Gloves, 2) &&
                                    _game.Items.Has(ItemType.Mirror))
                                    return AccessibilityLevel.Normal;
                            }

                            if (_game.Items.Has(ItemType.Mushroom) && _game.Items.Has(ItemType.CaneOfSomaria))
                            {
                                if (_game.Items.Has(ItemType.Hammer))
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)lightWorld);

                                if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Gloves, 2) &&
                                    _game.Items.Has(ItemType.Mirror))
                                    return AccessibilityLevel.SequenceBreak;
                            }
                        }
                        else
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Hammer))
                            {
                                if (_game.Items.Has(ItemType.Powder))
                                    return lightWorld;

                                if (_game.Items.Has(ItemType.Mushroom) && _game.Items.Has(ItemType.CaneOfSomaria))
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)lightWorld);
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Powder) ||
                                (_game.Items.Has(ItemType.Mushroom) && _game.Items.Has(ItemType.CaneOfSomaria)))
                            {
                                if (_game.Items.Has(ItemType.Hammer) &&
                                    _game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                                    return Available;

                                if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Gloves, 2) &&
                                    _game.Items.Has(ItemType.Mirror))
                                    return Available;
                            }
                        }
                        else
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Hammer) &&
                                (_game.Items.Has(ItemType.Powder) ||
                                (_game.Items.Has(ItemType.Mushroom) && _game.Items.Has(ItemType.CaneOfSomaria))))
                                return Available;
                        }

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 1, 128);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToNPCItemMemory = true;

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
                        if (_game.Mode.WorldState != WorldState.Inverted)
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
                                dWSouth = _game.Regions[RegionID.DarkWorldSouth].Accessibility;

                            return (AccessibilityLevel)Math.Max((byte)lightWorld, (byte)dWSouth);
                        }
                        else
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                            {
                                if (_game.Items.Has(ItemType.RaceGameAccess))
                                    return AccessibilityLevel.Normal;

                                AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                                if (!_game.Mode.EntranceShuffle.Value)
                                    return lightWorld;

                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Inspect, (byte)lightWorld);
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.RaceGameAccess))
                                return Available;

                            if (!_game.Mode.EntranceShuffle.Value)
                                return Available;

                            if (_game.Items.Has(ItemType.Mirror) &&
                                _game.Regions[RegionID.DarkWorldSouth].Accessibility >= AccessibilityLevel.SequenceBreak)
                                return Available;
                        }
                        else
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                            {
                                if (_game.Items.Has(ItemType.RaceGameAccess))
                                    return Available;

                                if (!_game.Mode.EntranceShuffle.Value)
                                    return Available;
                            }
                        }

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.OverworldEvent, 40, 64);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToOverworldEventMemory = true;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());
                    _regionSubscriptions.Add(RegionID.DarkWorldSouth, new Mode() { WorldState = WorldState.StandardOpen });

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
                        AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;
                        
                        if (_game.Items.Has(ItemType.Boots) &&
                            (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)))
                                return lightWorld;
                        
                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Inspect, (byte)lightWorld);
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Items.Has(ItemType.Boots) &&
                            (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)))
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 0, 128);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToNPCItemMemory = true;

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
                        if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                            return _game.Regions[RegionID.LightWorld].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if ((_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)) &&
                            _game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 1, 16);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToNPCItemMemory = true;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.LostWoods:

                    _baseTotal = 1;
                    Name = "Hideout";

                    RequiredMode = new Mode() { EntranceShuffle = false };

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    GetAccessible = () =>
                    {
                        if (_game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 451, 2);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToRoomMemory = true;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
                case LocationID.CastleSecret when index == 0:

                    _baseTotal = 1;
                    Name = "Uncle";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                            return _game.Regions[RegionID.LightWorld].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if ((_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)) &&
                            _game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 134, 1);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToItemMemory = true;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.CastleSecret:

                    _baseTotal = 1;
                    Name = "Hallway";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                            return _game.Regions[RegionID.LightWorld].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if ((_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)) &&
                            _game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 170, 16);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToRoomMemory = true;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.LinksHouse:

                    _baseTotal = 1;
                    Name = "By The Door";

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };
                    GetAccessible = () => { return Available; };

                    AutoTrack = () =>
                    {
                        (MemorySegmentType, int, byte)[] addressFlags = new (MemorySegmentType, int, byte)[2]
                        {
                            (MemorySegmentType.Room, 1, 4),
                            (MemorySegmentType.Room, 520, 16)
                        };

                        int? result = _game.AutoTracker.CheckMemoryFlagArray(addressFlags);

                        if (result.HasValue)
                            Available = result.Value > 0 ? 0 : 1;
                    };

                    _subscribeToRoomMemory = true;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
                case LocationID.GroveDiggingSpot:

                    _baseTotal = 1;
                    Name = "Hidden Treasure";

                    GetAccessibility = () =>
                    {
                        if (_game.Items.Has(ItemType.Shovel) &&
                            (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)))
                            return _game.Regions[RegionID.LightWorld].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if ((_game.Items.Has(ItemType.Shovel) &&
                            (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))) &&
                            _game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 0, 32);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToNPCItemMemory = true;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.Shovel, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.PyramidLedge:

                    _baseTotal = 1;
                    Name = "Ledge";

                    GetAccessibility = () => { return _game.Regions[RegionID.DarkWorldEast].Accessibility; };

                    GetAccessible = () =>
                    {
                        if (_game.Regions[RegionID.DarkWorldEast].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.OverworldEvent, 91, 64);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToOverworldEventMemory = true;

                    _regionSubscriptions.Add(RegionID.DarkWorldEast, new Mode());

                    break;
                case LocationID.FatFairy:

                    _baseTotal = 2;
                    Name = "Big Bomb Spot";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.RedCrystal, 2))
                            {
                                if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Gloves))
                                    return lightWorld;

                                if (_game.Items.Has(ItemType.Aga))
                                {
                                    if (_game.Items.Has(ItemType.Hammer))
                                        return lightWorld;

                                    if (_game.Items.Has(ItemType.Mirror))
                                    {
                                        if (_game.Items.Has(ItemType.Gloves, 2))
                                            return lightWorld;

                                        if (_game.Items.Has(ItemType.Hookshot) &&
                                            (_game.Items.Has(ItemType.Flippers) || _game.Items.Has(ItemType.Gloves)))
                                            return lightWorld;
                                    }
                                }

                                if (_game.Items.Has(ItemType.Gloves, 2))
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)lightWorld);
                            }
                        }
                        else
                        {
                            if (_game.Items.Has(ItemType.RedCrystal, 2) && _game.Items.Has(ItemType.Mirror))
                                return lightWorld;
                        }

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Mode.WorldState != WorldState.Inverted)
                            {
                                if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.RedCrystal, 2))
                                {
                                    if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Gloves))
                                        return Available;

                                    if (_game.Items.Has(ItemType.Aga))
                                    {
                                        if (_game.Items.Has(ItemType.Hammer))
                                            return Available;

                                        if (_game.Items.Has(ItemType.Mirror))
                                        {
                                            if (_game.Items.Has(ItemType.Gloves, 2))
                                                return Available;

                                            if (_game.Items.Has(ItemType.Hookshot) &&
                                                (_game.Items.Has(ItemType.Flippers) || _game.Items.Has(ItemType.Gloves)))
                                                return Available;
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.Gloves, 2))
                                        return Available;
                                }
                            }
                            else
                            {
                                if (_game.Items.Has(ItemType.RedCrystal, 2) && _game.Items.Has(ItemType.Mirror))
                                    return Available;
                            }
                        }

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        (MemorySegmentType, int, byte)[] addressFlags = new (MemorySegmentType, int, byte)[2]
                        {
                            (MemorySegmentType.Room, 556, 16),
                            (MemorySegmentType.Room, 556, 32)
                        };

                        int? result = _game.AutoTracker.CheckMemoryFlagArray(addressFlags);

                        if (result.HasValue)
                            Available = Total - result.Value;
                    };

                    _subscribeToRoomMemory = true;

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
                        if (_game.Mode.WorldState == WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                            return _game.Regions[RegionID.DarkWorldSouth].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if ((_game.Mode.WorldState == WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)) &&
                            _game.Regions[RegionID.DarkWorldSouth].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 0, 8);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToNPCItemMemory = true;

                    _regionSubscriptions.Add(RegionID.DarkWorldSouth, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.HypeCave:

                    _baseTotal = 5;
                    Name = "Cave";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                            return _game.Regions[RegionID.DarkWorldSouth].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if ((_game.Mode.WorldState == WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)) &&
                            _game.Regions[RegionID.DarkWorldSouth].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        (MemorySegmentType, int, byte)[] addressFlags = new (MemorySegmentType, int, byte)[5]
                        {
                            (MemorySegmentType.Room, 572, 16),
                            (MemorySegmentType.Room, 572, 32),
                            (MemorySegmentType.Room, 572, 64),
                            (MemorySegmentType.Room, 572, 128),
                            (MemorySegmentType.Room, 573, 4)
                        };

                        int? result = _game.AutoTracker.CheckMemoryFlagArray(addressFlags);

                        if (result.HasValue)
                            Available = Total - result.Value;
                    };

                    _subscribeToRoomMemory = true;

                    _regionSubscriptions.Add(RegionID.DarkWorldSouth, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.BombosTablet:

                    _baseTotal = 1;
                    Name = "Tablet";
                    HasMarking = true;

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Book) && _game.Items.Has(ItemType.Mirror))
                            {
                                AccessibilityLevel dWSouth = _game.Regions[RegionID.DarkWorldSouth].Accessibility;

                                if (_game.Items.CanActivateTablets())
                                    return dWSouth;

                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Inspect, (byte)dWSouth);
                            }
                        }
                        else
                        {
                            if (_game.Items.Has(ItemType.Book))
                            {
                                AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                                if (_game.Items.CanActivateTablets())
                                    return lightWorld;

                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Inspect, (byte)lightWorld);
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Book) && _game.Items.Has(ItemType.Mirror) &&
                                _game.Items.CanActivateTablets() &&
                                _game.Regions[RegionID.DarkWorldSouth].Accessibility >= AccessibilityLevel.SequenceBreak)
                                return Available;
                        }
                        else
                        {
                            if (_game.Items.Has(ItemType.Book) && _game.Items.CanActivateTablets() &&
                                _game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                                return Available;
                        }

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 1, 2);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToNPCItemMemory = true;

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
                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Mirror))
                                return _game.Regions[RegionID.DarkWorldSouth].Accessibility;
                        }
                        else
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.LightWorld].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Mirror) &&
                                _game.Regions[RegionID.DarkWorldSouth].Accessibility >= AccessibilityLevel.SequenceBreak)
                                return Available;
                        }
                        else
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) &&
                                _game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                                return Available;
                        }

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 567, 4);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToRoomMemory = true;

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
                        if (_game.Mode.WorldState == WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                            return _game.Regions[RegionID.DarkWorldSouth].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if ((_game.Mode.WorldState == WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)) &&
                            _game.Regions[RegionID.DarkWorldSouth].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.OverworldEvent, 104, 64);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToOverworldEventMemory = true;

                    _regionSubscriptions.Add(RegionID.DarkWorldSouth, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.WitchsHut:

                    _baseTotal = 1;
                    Name = "Assistant";

                    GetAccessibility = () =>
                    {
                        if (_game.Items.Has(ItemType.Mushroom) &&
                            (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)))
                            return _game.Regions[RegionID.LightWorld].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Items.Has(ItemType.Mushroom) &&
                            (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)) &&
                            _game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 1, 32);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToNPCItemMemory = true;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.Mushroom, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.WaterfallFairy:

                    _baseTotal = 2;
                    Name = "Waterfall Cave";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                        {
                            AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                            if (_game.Items.Has(ItemType.Flippers))
                                return lightWorld;

                            if (_game.Items.Has(ItemType.MoonPearl))
                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)lightWorld);
                        }

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if ((_game.Items.Has(ItemType.MoonPearl) ||
                            (_game.Items.Has(ItemType.Flippers) && _game.Mode.WorldState != WorldState.Inverted)) &&
                            _game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        (MemorySegmentType, int, byte)[] addressFlags = new (MemorySegmentType, int, byte)[2]
                        {
                            (MemorySegmentType.Room, 552, 16),
                            (MemorySegmentType.Room, 552, 32)
                        };

                        int? result = _game.AutoTracker.CheckMemoryFlagArray(addressFlags);

                        if (result.HasValue)
                            Available = Total - result.Value;
                    };

                    _subscribeToRoomMemory = true;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.Flippers, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode());

                    break;
                case LocationID.ZoraArea when index == 0:

                    _baseTotal = 1;
                    Name = "King Zora";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                        {
                            AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                            if (_game.Items.Has(ItemType.Gloves) || _game.Items.Has(ItemType.Flippers))
                                return lightWorld;

                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)lightWorld);
                        }

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if ((_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)) &&
                            _game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 0, 2);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToNPCItemMemory = true;

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
                        if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                        {
                            AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                            if (_game.Items.Has(ItemType.Flippers))
                                return lightWorld;

                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)lightWorld);
                        }

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if ((_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)) &&
                            _game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.OverworldEvent, 129, 64);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToOverworldEventMemory = true;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.Flippers, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.Catfish:

                    _baseTotal = 1;
                    Name = "Ring of Stones";

                    GetAccessibility = () =>
                    {
                        if ((_game.Mode.WorldState == WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)) &&
                            _game.Items.Has(ItemType.Gloves))
                            return _game.Regions[RegionID.DarkWorldEast].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if ((_game.Mode.WorldState == WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)) &&
                            _game.Items.Has(ItemType.Gloves) &&
                            _game.Regions[RegionID.DarkWorldEast].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 0, 32);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToNPCItemMemory = true;

                    _regionSubscriptions.Add(RegionID.DarkWorldEast, new Mode());

                    _itemSubscriptions.Add(ItemType.Gloves, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.SahasrahlasHut when index == 0:

                    _baseTotal = 3;
                    Name = "Back Room";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                            return _game.Regions[RegionID.LightWorld].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if ((_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)) &&
                            _game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        (MemorySegmentType, int, byte)[] addressFlags = new (MemorySegmentType, int, byte)[3]
                        {
                            (MemorySegmentType.Room, 522, 16),
                            (MemorySegmentType.Room, 522, 32),
                            (MemorySegmentType.Room, 522, 64)
                        };

                        int? result = _game.AutoTracker.CheckMemoryFlagArray(addressFlags);

                        if (result.HasValue)
                            Available = Total - result.Value;
                    };

                    _subscribeToRoomMemory = true;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.SahasrahlasHut:

                    _baseTotal = 1;
                    Name = "Saha";

                    GetAccessibility = () =>
                    {
                        if (_game.Items.Has(ItemType.GreenPendant))
                            return _game.Regions[RegionID.LightWorld].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Items.Has(ItemType.GreenPendant) &&
                            _game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 0, 16);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToNPCItemMemory = true;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.GreenPendant, new Mode());

                    break;
                case LocationID.BonkRocks:

                    _baseTotal = 1;
                    Name = "Cave";

                    GetAccessibility = () =>
                    {
                        if ((_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)) &&
                            _game.Items.Has(ItemType.Boots))
                            return _game.Regions[RegionID.LightWorld].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if ((_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)) &&
                            _game.Items.Has(ItemType.Boots))
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 584, 16);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToRoomMemory = true;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.Boots, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.KingsTomb:

                    _baseTotal = 1;
                    Name = "The Crypt";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted)
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
                        else
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Boots) &&
                                _game.Items.Has(ItemType.Gloves, 2))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Boots))
                            {
                                if (_game.Items.Has(ItemType.Gloves, 2) &&
                                    _game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                                    return Available;

                                if (_game.Items.Has(ItemType.Mirror) && _game.Items.Has(ItemType.MoonPearl) &&
                                    _game.Regions[RegionID.DarkWorldWest].Accessibility >= AccessibilityLevel.SequenceBreak)
                                    return Available;
                            }
                        }
                        else
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Boots) &&
                                _game.Items.Has(ItemType.Gloves, 2))
                                return Available;
                        }

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 550, 16);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToRoomMemory = true;

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
                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Mirror) && _game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.DarkWorldWest].Accessibility;
                        }
                        else
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.LightWorld].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Mirror) && _game.Items.Has(ItemType.MoonPearl) &&
                                _game.Regions[RegionID.DarkWorldWest].Accessibility >= AccessibilityLevel.SequenceBreak)
                                return Available;
                        }
                        else
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) &&
                                _game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                                return Available;
                        }

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 567, 2);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToRoomMemory = true;

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

                        if (_game.Mode.WorldState != WorldState.Inverted)
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
                        else
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
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Items.Has(ItemType.DesertLeftAccess))
                            return Available;

                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.DesertBackAccess) && _game.Items.Has(ItemType.Gloves))
                                return Available;

                            if (!_game.Mode.EntranceShuffle.Value && _game.Items.Has(ItemType.Book)
                                && _game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                                return Available;

                            if (_game.Items.Has(ItemType.Mirror) &&
                                _game.Regions[RegionID.MireArea].Accessibility >= AccessibilityLevel.SequenceBreak)
                                return Available;
                        }
                        else
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                            {
                                if (_game.Items.Has(ItemType.DesertBackAccess) && _game.Items.Has(ItemType.Gloves))
                                    return Available;

                                if (!_game.Mode.EntranceShuffle.Value && _game.Items.Has(ItemType.Book) &&
                                    _game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                                    return Available;
                            }
                        }

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.OverworldEvent, 48, 64);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToOverworldEventMemory = true;

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
                        if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                            return _game.Regions[RegionID.LightWorld].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if ((_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)) &&
                            _game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 532, 16);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToRoomMemory = true;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.CShapedHouse:

                    _baseTotal = 1;
                    Name = "House";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                            return _game.Regions[RegionID.DarkWorldWest].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if ((_game.Mode.WorldState == WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)) &&
                            _game.Regions[RegionID.DarkWorldWest].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 568, 16);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToRoomMemory = true;

                    _regionSubscriptions.Add(RegionID.DarkWorldWest, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.TreasureGame:

                    _baseTotal = 1;
                    Name = "Prize";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                            return _game.Regions[RegionID.DarkWorldWest].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if ((_game.Mode.WorldState == WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)) &&
                            _game.Regions[RegionID.DarkWorldWest].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 525, 4);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToRoomMemory = true;

                    _regionSubscriptions.Add(RegionID.DarkWorldWest, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.BombableShack:

                    _baseTotal = 1;
                    Name = "Downstairs";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                            return _game.Regions[RegionID.DarkWorldWest].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if ((_game.Mode.WorldState == WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)) &&
                            _game.Regions[RegionID.DarkWorldWest].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 524, 16);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToRoomMemory = true;

                    _regionSubscriptions.Add(RegionID.DarkWorldWest, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.Blacksmith:

                    _baseTotal = 1;
                    Name = "House";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Gloves, 2) && _game.Items.Has(ItemType.MoonPearl))
                            {
                                AccessibilityLevel dWWest = AccessibilityLevel.None;
                                AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                                if (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                    _game.Items.Has(ItemType.Mirror))
                                    dWWest = _game.Regions[RegionID.DarkWorldWest].Accessibility;
                                else
                                {
                                    dWWest = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                        (byte)_game.Regions[RegionID.DarkWorldWest].Accessibility);
                                }

                                return (AccessibilityLevel)Math.Min((byte)dWWest, (byte)lightWorld);
                            }
                        }
                        else
                        {
                            AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;
                            AccessibilityLevel dWWest = AccessibilityLevel.None;

                            if (_game.Items.Has(ItemType.Mirror))
                                return lightWorld;

                            if (_game.Items.Has(ItemType.Gloves, 2))
                                dWWest = _game.Regions[RegionID.DarkWorldWest].Accessibility;
                            
                            return (AccessibilityLevel)Math.Min((byte)lightWorld, (byte)dWWest);
                        }

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Gloves, 2) && _game.Items.Has(ItemType.MoonPearl))
                                return Available;
                        }
                        else
                        {
                            if (_game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                            {
                                if (_game.Items.Has(ItemType.Mirror))
                                    return Available;

                                if (_game.Items.Has(ItemType.Gloves, 2) &&
                                    _game.Regions[RegionID.DarkWorldWest].Accessibility >= AccessibilityLevel.SequenceBreak)
                                    return Available;
                            }
                        }

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 1, 4);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToNPCItemMemory = true;

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
                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Gloves, 2) && _game.Items.Has(ItemType.MoonPearl))
                            {
                                AccessibilityLevel dWWest = _game.Regions[RegionID.DarkWorldWest].Accessibility;
                                AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                                return (AccessibilityLevel)Math.Min((byte)dWWest, (byte)lightWorld);
                            }
                        }
                        else
                        {
                            AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;
                            AccessibilityLevel dWWest = AccessibilityLevel.None;

                            if (_game.Items.Has(ItemType.Mirror))
                                return lightWorld;

                            if (_game.Items.Has(ItemType.Gloves, 2))
                                dWWest = _game.Regions[RegionID.DarkWorldWest].Accessibility;

                            return (AccessibilityLevel)Math.Min((byte)lightWorld, (byte)dWWest);
                        }

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Gloves, 2) && _game.Items.Has(ItemType.MoonPearl) &&
                                _game.Regions[RegionID.DarkWorldWest].Accessibility >= AccessibilityLevel.SequenceBreak &&
                                _game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                                return Available;
                        }
                        else
                        {
                            if (_game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                            {
                                if (_game.Items.Has(ItemType.Mirror))
                                    return Available;

                                if (_game.Items.Has(ItemType.Gloves, 2) &&
                                    _game.Regions[RegionID.DarkWorldWest].Accessibility >= AccessibilityLevel.SequenceBreak)
                                    return Available;
                            }
                        }

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 137, 16);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToItemMemory = true;

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
                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Gloves, 2) && _game.Items.Has(ItemType.Hammer) &&
                                _game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.DarkWorldWest].Accessibility;
                        }
                        else
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

                    GetAccessible = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Gloves, 2) && _game.Items.Has(ItemType.Hammer) &&
                                _game.Items.Has(ItemType.MoonPearl) &&
                                _game.Regions[RegionID.DarkWorldWest].Accessibility >= AccessibilityLevel.SequenceBreak)
                                return Available;
                        }
                        else
                        {
                            if (_game.Items.Has(ItemType.Hammer))
                            {
                                if (_game.Items.Has(ItemType.Gloves, 2) &&
                                    _game.Regions[RegionID.DarkWorldWest].Accessibility >= AccessibilityLevel.SequenceBreak)
                                    return Available;

                                if (_game.Items.Has(ItemType.Mirror) &&
                                    _game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                                    return Available;
                            }
                        }

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 591, 4);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToRoomMemory = true;

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

                        if (_game.Mode.WorldState != WorldState.Inverted)
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
                        else
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
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Items.Has(ItemType.BumperCaveAccess))
                            return Available;

                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (!_game.Mode.EntranceShuffle.Value)
                            {
                                if (_game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.Cape) &&
                                    _game.Items.Has(ItemType.MoonPearl) &&
                                    _game.Regions[RegionID.DarkWorldWest].Accessibility >= AccessibilityLevel.SequenceBreak)
                                    return Available;
                            }
                        }
                        else
                        {
                            if (_game.Items.Has(ItemType.DeathMountainExitAccess) && _game.Items.Has(ItemType.Mirror))
                                return Available;

                            if (!_game.Mode.EntranceShuffle.Value && _game.Items.Has(ItemType.MoonPearl) &&
                                _game.Items.Has(ItemType.Cape) && _game.Items.Has(ItemType.Gloves) &&
                                _game.Items.Has(ItemType.Mirror) &&
                                _game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                                return Available;
                        }

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.OverworldEvent, 74, 64);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToOverworldEventMemory = true;

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
                        if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                            return _game.Regions[RegionID.LightWorld].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl) &&
                            _game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 534, 16);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToRoomMemory = true;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.Dam:

                    _baseTotal = 1;
                    Name = "Outside";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted || _game.Mode.EntranceShuffle.Value ||
                            _game.Items.Has(ItemType.MoonPearl))
                            return _game.Regions[RegionID.LightWorld].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if ((_game.Mode.WorldState != WorldState.Inverted || _game.Mode.EntranceShuffle.Value ||
                            _game.Items.Has(ItemType.MoonPearl)) &&
                            _game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.OverworldEvent, 59, 64);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToOverworldEventMemory = true;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.MiniMoldormCave:

                    _baseTotal = 5;
                    Name = "Cave";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                            return _game.Regions[RegionID.LightWorld].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        (MemorySegmentType, int, byte)[] addressFlags = new (MemorySegmentType, int, byte)[5]
                        {
                            (MemorySegmentType.Room, 582, 16),
                            (MemorySegmentType.Room, 582, 32),
                            (MemorySegmentType.Room, 582, 64),
                            (MemorySegmentType.Room, 582, 128),
                            (MemorySegmentType.Room, 583, 4)
                        };

                        int? result = _game.AutoTracker.CheckMemoryFlagArray(addressFlags);

                        if (result.HasValue)
                            Available = Total - result.Value;
                    };

                    _subscribeToRoomMemory = true;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.IceRodCave:

                    _baseTotal = 1;
                    Name = "Cave";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                            return _game.Regions[RegionID.LightWorld].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 576, 16);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToRoomMemory = true;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.LakeHyliaIsland:

                    _baseTotal = 1;
                    Name = "Island";
                    HasMarking = true;

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted)
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
                        else
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                            {
                                AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                                if (_game.Items.Has(ItemType.Flippers))
                                    return lightWorld;

                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)lightWorld);

                            }   
                        }

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Mirror) &&
                                (_game.Regions[RegionID.DarkWorldEast].Accessibility >= AccessibilityLevel.SequenceBreak ||
                                _game.Regions[RegionID.DarkWorldSouth].Accessibility >= AccessibilityLevel.SequenceBreak ||
                                _game.Regions[RegionID.DarkWorldSouthEast].Accessibility >= AccessibilityLevel.SequenceBreak))
                                return Available;
                        }
                        else
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) &&
                                _game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                                return Available;
                        }

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.OverworldEvent, 53, 64);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToOverworldEventMemory = true;

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
                        if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                        {
                            AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                            if (_game.Items.Has(ItemType.Flippers))
                                return lightWorld;

                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)lightWorld);
                        }

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 137, 1);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToItemMemory = true;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.Flippers, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.MireShack:

                    _baseTotal = 2;
                    Name = "Shack";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel mireArea = _game.Regions[RegionID.MireArea].Accessibility;

                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return mireArea;

                            if (_game.Items.Has(ItemType.Mirror))
                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)mireArea);
                        }
                        else
                            return _game.Regions[RegionID.MireArea].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if ((_game.Mode.WorldState == WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl) ||
                            _game.Items.Has(ItemType.Mirror)) &&
                            _game.Regions[RegionID.MireArea].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        (MemorySegmentType, int, byte)[] addressFlags = new (MemorySegmentType, int, byte)[2]
                        {
                            (MemorySegmentType.Room, 538, 16),
                            (MemorySegmentType.Room, 538, 32)
                        };

                        int? result = _game.AutoTracker.CheckMemoryFlagArray(addressFlags);

                        if (result.HasValue)
                            Available = Total - result.Value;
                    };

                    _subscribeToRoomMemory = true;

                    _regionSubscriptions.Add(RegionID.MireArea, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.CheckerboardCave:

                    _baseTotal = 1;
                    Name = "Cave";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Mirror) && _game.Items.Has(ItemType.Gloves))
                                return _game.Regions[RegionID.MireArea].Accessibility;
                        }
                        else
                        {
                            if (_game.Items.Has(ItemType.Gloves))
                                return _game.Regions[RegionID.LightWorld].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Mirror) && _game.Items.Has(ItemType.Gloves) &&
                                _game.Regions[RegionID.MireArea].Accessibility >= AccessibilityLevel.SequenceBreak)
                                return Available;
                        }
                        else
                        {
                            if (_game.Items.Has(ItemType.Gloves) &&
                                _game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                                return Available;
                        }

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 589, 2);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToRoomMemory = true;

                    _regionSubscriptions.Add(RegionID.MireArea, new Mode() { WorldState = WorldState.StandardOpen });
                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode() { WorldState = WorldState.Inverted });

                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Gloves, new Mode());

                    break;
                case LocationID.OldMan:

                    _baseTotal = 1;
                    Name = "Old Man";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel dMWestBottom = _game.Regions[RegionID.DeathMountainWestBottom].Accessibility;

                        if (_game.Items.Has(ItemType.Lamp))
                            return dMWestBottom;
                        
                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)dMWestBottom);
                    };

                    GetAccessible = () =>
                    {

                        if (_game.Regions[RegionID.DeathMountainWestBottom].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 0, 1);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToNPCItemMemory = true;

                    _regionSubscriptions.Add(RegionID.DeathMountainWestBottom, new Mode());

                    _itemSubscriptions.Add(ItemType.Lamp, new Mode());

                    break;
                case LocationID.SpectacleRock when index == 0:

                    _baseTotal = 1;
                    Name = "Cave";

                    RequiredMode = new Mode() { EntranceShuffle = false };

                    GetAccessibility = () => { return _game.Regions[RegionID.DeathMountainWestBottom].Accessibility; };

                    GetAccessible = () =>
                    {
                        if (_game.Regions[RegionID.DeathMountainWestBottom].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 469, 4);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToRoomMemory = true;

                    _regionSubscriptions.Add(RegionID.DeathMountainWestBottom, new Mode());

                    break;
                case LocationID.SpectacleRock:

                    _baseTotal = 1;
                    Name = "Top";
                    HasMarking = true;

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            AccessibilityLevel dMWestBottom = AccessibilityLevel.None;
                            AccessibilityLevel inspect = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Inspect,
                                (byte)_game.Regions[RegionID.DeathMountainWestBottom].Accessibility);

                            if (_game.Items.Has(ItemType.Mirror))
                                dMWestBottom = _game.Regions[RegionID.DeathMountainWestBottom].Accessibility;

                            return (AccessibilityLevel)Math.Max((byte)dMWestBottom, (byte)inspect);
                        }
                        else
                        {
                            AccessibilityLevel dMWestTop = _game.Regions[RegionID.DeathMountainWestTop].Accessibility;
                            AccessibilityLevel inspect = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Inspect,
                                (byte)_game.Regions[RegionID.DeathMountainWestBottom].Accessibility);

                            return (AccessibilityLevel)Math.Max((byte)dMWestTop, (byte)inspect);
                        }
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Mirror) &&
                                _game.Regions[RegionID.DeathMountainWestBottom].Accessibility >= AccessibilityLevel.SequenceBreak)
                                return Available;
                        }
                        else
                        {
                            if (_game.Regions[RegionID.DeathMountainWestTop].Accessibility >= AccessibilityLevel.SequenceBreak)
                                return Available;
                        }

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.OverworldEvent, 3, 64);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToOverworldEventMemory = true;

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

                    GetAccessible = () =>
                    {
                        if (_game.Items.Has(ItemType.Book) && _game.Items.CanActivateTablets() &&
                            _game.Regions[RegionID.DeathMountainWestTop].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 1, 1);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToNPCItemMemory = true;

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
                        if (_game.Mode.WorldState != WorldState.Inverted)
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
                        else
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

                    GetAccessible = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.Hammer) &&
                                _game.Items.Has(ItemType.MoonPearl))
                                return Available;
                        }
                        else
                        {
                            if (_game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.Hammer))
                                return Available;
                        }

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 558, 16);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToRoomMemory = true;

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
                        if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                            return _game.Regions[RegionID.DeathMountainEastTop].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if ((_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)) &&
                            _game.Regions[RegionID.DeathMountainEastTop].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 508, 16);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToRoomMemory = true;

                    _regionSubscriptions.Add(RegionID.DeathMountainEastTop, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.ParadoxCave when index == 0:

                    _baseTotal = 2;
                    Name = "Bottom";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                            return _game.Regions[RegionID.DeathMountainEastTop].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if ((_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)) &&
                            _game.Regions[RegionID.DeathMountainEastTop].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        (MemorySegmentType, int, byte)[] addressFlags = new (MemorySegmentType, int, byte)[2]
                        {
                            (MemorySegmentType.Room, 510, 16),
                            (MemorySegmentType.Room, 510, 32)
                        };

                        int? result = _game.AutoTracker.CheckMemoryFlagArray(addressFlags);

                        if (result.HasValue)
                            Available = Total - result.Value;
                    };

                    _subscribeToRoomMemory = true;

                    _regionSubscriptions.Add(RegionID.DeathMountainEastTop, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.ParadoxCave:

                    _baseTotal = 5;
                    Name = "Top";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                            return _game.Regions[RegionID.DeathMountainEastTop].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if ((_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)) &&
                            _game.Regions[RegionID.DeathMountainEastTop].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        (MemorySegmentType, int, byte)[] addressFlags = new (MemorySegmentType, int, byte)[5]
                        {
                            (MemorySegmentType.Room, 478, 16),
                            (MemorySegmentType.Room, 478, 32),
                            (MemorySegmentType.Room, 478, 64),
                            (MemorySegmentType.Room, 478, 128),
                            (MemorySegmentType.Room, 479, 1)
                        };

                        int? result = _game.AutoTracker.CheckMemoryFlagArray(addressFlags);

                        if (result.HasValue)
                            Available = Total - result.Value;
                    };

                    _subscribeToRoomMemory = true;

                    _regionSubscriptions.Add(RegionID.DeathMountainEastTop, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.SuperBunnyCave:

                    _baseTotal = 2;
                    Name = "Cave";

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.DarkDeathMountainTop].Accessibility;

                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                (byte)_game.Regions[RegionID.DarkDeathMountainTop].Accessibility);
                        }
                        else
                            return _game.Regions[RegionID.DarkDeathMountainTop].Accessibility;
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Regions[RegionID.DarkDeathMountainTop].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return Available;
                        
                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        (MemorySegmentType, int, byte)[] addressFlags = new (MemorySegmentType, int, byte)[2]
                        {
                            (MemorySegmentType.Room, 496, 16),
                            (MemorySegmentType.Room, 496, 32)
                        };

                        int? result = _game.AutoTracker.CheckMemoryFlagArray(addressFlags);

                        if (result.HasValue)
                            Available = Total - result.Value;
                    };

                    _subscribeToRoomMemory = true;

                    _regionSubscriptions.Add(RegionID.DarkDeathMountainTop, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.HookshotCave when index == 0:

                    _baseTotal = 1;
                    Name = "Bonkable Chest";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel dDMTop = _game.Regions[RegionID.DarkDeathMountainTop].Accessibility;

                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Gloves))
                            {
                                if (_game.Items.Has(ItemType.Hookshot) ||
                                    (_game.Items.Has(ItemType.Boots) &&
                                    (_game.Mode.ItemPlacement == ItemPlacement.Advanced)))
                                    return dDMTop;

                                if (_game.Items.Has(ItemType.Boots))
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)dDMTop);
                            }
                        }
                        else
                        {
                            if (_game.Items.Has(ItemType.Gloves) &&
                                (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots)))
                                return dDMTop;
                        }

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Gloves) &&
                            _game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots))
                                return Available;
                        }
                        else
                        {
                            if (_game.Items.Has(ItemType.Gloves) &&
                                (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots)))
                                return Available;
                        }

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 120, 128);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToRoomMemory = true;

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
                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.MoonPearl) &&
                                _game.Items.Has(ItemType.Hookshot))
                                return _game.Regions[RegionID.DarkDeathMountainTop].Accessibility;
                        }
                        else
                        {
                            if (_game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.Hookshot))
                                return _game.Regions[RegionID.DarkDeathMountainTop].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.MoonPearl) &&
                                _game.Items.Has(ItemType.Hookshot))
                                return Available;
                        }
                        else
                        {
                            if (_game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.Hookshot))
                                return Available;
                        }

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        (MemorySegmentType, int, byte)[] addressFlags = new (MemorySegmentType, int, byte)[3]
                        {
                            (MemorySegmentType.Room, 120, 16),
                            (MemorySegmentType.Room, 120, 32),
                            (MemorySegmentType.Room, 120, 64)
                        };

                        int? result = _game.AutoTracker.CheckMemoryFlagArray(addressFlags);

                        if (result.HasValue)
                            Available = Total - result.Value;
                    };

                    _subscribeToRoomMemory = true;

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
                        if (_game.Mode.WorldState != WorldState.Inverted)
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
                        else
                            return _game.Regions[RegionID.DeathMountainEastTop].Accessibility;
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Mirror))
                            {
                                if (_game.Items.Has(ItemType.DarkDeathMountainFloatingIslandAccess))
                                    return Available;

                                if (!_game.Mode.EntranceShuffle.Value && _game.Items.Has(ItemType.Gloves) &&
                                    _game.Items.Has(ItemType.MoonPearl) &&
                                    _game.Regions[RegionID.DarkDeathMountainTop].Accessibility >= AccessibilityLevel.SequenceBreak)
                                    return Available;
                            }
                        }
                        else
                        {
                            if (_game.Regions[RegionID.DeathMountainEastTop].Accessibility >= AccessibilityLevel.SequenceBreak)
                                return Available;
                        }

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.OverworldEvent, 5, 64);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToOverworldEventMemory = true;

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
                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Mirror) && _game.Items.Has(ItemType.Hammer) &&
                                _game.Items.Has(ItemType.CaneOfSomaria))
                            {
                                if (_game.Items.Has(ItemType.TRSmallKey, 2) &&
                                    (_game.Mode.DungeonItemShuffle.Value >= DungeonItemShuffle.MapsCompassesSmallKeys ||
                                    _game.Items.Has(ItemType.FireRod)))
                                    return _game.Regions[RegionID.TurtleRockFront].Accessibility;

                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                    (byte)_game.Regions[RegionID.TurtleRockFront].Accessibility);
                            }
                        }
                        else
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Hammer))
                                return _game.Regions[RegionID.DeathMountainEastTop].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Mirror) && _game.Items.Has(ItemType.Hammer) &&
                                _game.Items.Has(ItemType.CaneOfSomaria) &&
                                _game.Regions[RegionID.TurtleRockFront].Accessibility >= AccessibilityLevel.SequenceBreak)
                                return Available;
                        }
                        else
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Hammer) &&
                                _game.Regions[RegionID.DeathMountainEastTop].Accessibility >= AccessibilityLevel.SequenceBreak)
                                return Available;
                        }

                        return 0;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 536, 16);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _subscribeToRoomMemory = true;

                    _updateOnDungeonItemShuffleChange = true;

                    _regionSubscriptions.Add(RegionID.TurtleRockFront, new Mode() { WorldState = WorldState.StandardOpen });
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

                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                        (byte)_game.Regions[RegionID.HyruleCastle].Accessibility);
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

                    GetAccessible = () =>
                    {
                        if (_game.Regions[RegionID.HyruleCastle].Accessibility >= AccessibilityLevel.SequenceBreak)
                        {
                            switch (_game.Mode.DungeonItemShuffle.Value)
                            {
                                case DungeonItemShuffle.Standard:
                                case DungeonItemShuffle.MapsCompasses:
                                    return Available;
                                case DungeonItemShuffle.MapsCompassesSmallKeys:
                                case DungeonItemShuffle.Keysanity:

                                    if (_game.Items.Has(ItemType.Gloves) || _game.Items.Has(ItemType.HCSmallKey))
                                        return Available;

                                    return Math.Max(0, Available - 3);
                            }
                        }

                        return 0;
                    };

                    _updateOnItemPlacementChange = true;
                    _updateOnDungeonItemShuffleChange = true;

                    _regionSubscriptions.Add(RegionID.HyruleCastle, new Mode());

                    _itemSubscriptions.Add(ItemType.Gloves, new Mode());
                    _itemSubscriptions.Add(ItemType.HCSmallKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys });
                    _itemSubscriptions.Add(ItemType.Lamp, new Mode());
                    _itemSubscriptions.Add(ItemType.FireRod, new Mode() { ItemPlacement = ItemPlacement.Advanced });

                    break;
                case LocationID.AgahnimTower:

                    _baseTotal = 0;
                    _smallKey = 2;
                    Name = "Dungeon";

                    RequiredMode = new Mode() { DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys };

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel agahnimTower = _game.Regions[RegionID.Agahnim].Accessibility;

                        if (_game.Items.Has(ItemType.ATSmallKey))
                        {
                            if (_game.Items.Has(ItemType.Lamp))
                                return agahnimTower;

                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)agahnimTower);
                        }

                        if (Available > 1)
                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)agahnimTower);

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Regions[RegionID.Agahnim].Accessibility >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Items.Has(ItemType.ATSmallKey))
                                return Available;

                            return Math.Max(0, Available - 1);
                        }

                        return 0;
                    };

                    _regionSubscriptions.Add(RegionID.Agahnim, new Mode() { DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys });

                    _itemSubscriptions.Add(ItemType.Lamp, new Mode() { DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys });
                    _itemSubscriptions.Add(ItemType.ATSmallKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys });

                    break;
                case LocationID.EasternPalace:

                    _mapCompass = 2;
                    _bigKey = 1;
                    _baseTotal = 3;
                    Name = "Dungeon";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel eP = _game.Regions[RegionID.EasternPalace].Accessibility;

                        switch (_game.Mode.DungeonItemShuffle.Value)
                        {
                            case DungeonItemShuffle.Standard:

                                if (_game.Items.Has(ItemType.Lamp) && _game.Items.CanClearRedEyegoreGoriyaRooms())
                                    return eP;

                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)eP);

                            case DungeonItemShuffle.MapsCompasses:
                            case DungeonItemShuffle.MapsCompassesSmallKeys:

                                if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                {
                                    if (_game.Items.Has(ItemType.Lamp))
                                        return eP;

                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)eP);
                                }

                                if (Available > 1)
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)eP);

                                break;
                            case DungeonItemShuffle.Keysanity:

                                if (_game.Items.Has(ItemType.EPBigKey) && _game.Items.CanClearRedEyegoreGoriyaRooms())
                                {
                                    if (_game.Items.Has(ItemType.Lamp))
                                        return eP;

                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)eP);
                                }

                                if (Available > 1 && _game.Items.Has(ItemType.EPBigKey))
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)eP);

                                if (Available > 2)
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)eP);

                                break;
                        }
                        
                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        switch (_game.Mode.DungeonItemShuffle.Value)
                        {
                            case DungeonItemShuffle.Standard:
                                
                                if (_game.Regions[RegionID.EasternPalace].Accessibility >= AccessibilityLevel.SequenceBreak)
                                    return Available;

                                break;
                            case DungeonItemShuffle.MapsCompasses:
                            case DungeonItemShuffle.MapsCompassesSmallKeys:

                                if (_game.Regions[RegionID.EasternPalace].Accessibility >= AccessibilityLevel.SequenceBreak)
                                {
                                    if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                        return Available;

                                    return Math.Max(0, Available - 1);
                                }

                                break;
                            case DungeonItemShuffle.Keysanity:

                                if (_game.Regions[RegionID.EasternPalace].Accessibility >= AccessibilityLevel.SequenceBreak)
                                {
                                    if (_game.Items.Has(ItemType.EPBigKey) && _game.Items.CanClearRedEyegoreGoriyaRooms())
                                        return Available;

                                    if (_game.Items.Has(ItemType.EPBigKey))
                                        return Math.Max(0, Available - 1);

                                    return Math.Max(0, Available - 2);
                                }

                                break;
                        }

                        return 0;
                    };

                    _updateOnDungeonItemShuffleChange = true;
                    _updateOnEnemyShuffleChange = true;

                    _regionSubscriptions.Add(RegionID.EasternPalace, new Mode());

                    _itemSubscriptions.Add(ItemType.Lamp, new Mode());
                    _itemSubscriptions.Add(ItemType.Bow, new Mode() { EnemyShuffle = false });
                    _itemSubscriptions.Add(ItemType.EPBigKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity });

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
                        AccessibilityLevel dP = _game.Regions[RegionID.DesertPalace].Accessibility;

                        switch (_game.Mode.DungeonItemShuffle.Value)
                        {
                            case DungeonItemShuffle.Standard:

                                if (_game.Items.Has(ItemType.Boots) &&
                                    (_game.Items.Has(ItemType.Gloves) || _game.Mode.EntranceShuffle.Value) &&
                                    (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)))
                                    return dP;

                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)dP);

                            case DungeonItemShuffle.MapsCompasses:

                                if (_game.Items.Has(ItemType.Boots) &&
                                    (_game.Items.Has(ItemType.Gloves) || _game.Mode.EntranceShuffle.Value) &&
                                    (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)))
                                    return dP;

                                if ((_game.Items.Has(ItemType.Gloves) || _game.Mode.EntranceShuffle.Value) &&
                                    (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)))
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)dP);

                                if (Available > 1)
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)dP);

                                break;
                            case DungeonItemShuffle.MapsCompassesSmallKeys:

                                if (_game.Items.Has(ItemType.Boots) && _game.Items.Has(ItemType.DPSmallKey) &&
                                    (_game.Items.Has(ItemType.Gloves) || _game.Mode.EntranceShuffle.Value) &&
                                    (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)))
                                    return dP;

                                if (_game.Items.Has(ItemType.DPSmallKey) &&
                                    (_game.Items.Has(ItemType.Gloves) || _game.Mode.EntranceShuffle.Value) &&
                                    (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)))
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)dP);

                                if (Available > 1 && _game.Items.Has(ItemType.DPSmallKey))
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)dP);

                                if (Available > 2 && 
                                    (_game.Items.Has(ItemType.Gloves) || _game.Mode.EntranceShuffle.Value) &&
                                    (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)))
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)dP);

                                if (Available > 3)
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)dP);

                                break;
                            case DungeonItemShuffle.Keysanity:

                                if (_game.Items.Has(ItemType.Boots) && _game.Items.Has(ItemType.DPBigKey) &&
                                    _game.Items.Has(ItemType.DPSmallKey) &&
                                    (_game.Items.Has(ItemType.Gloves) || _game.Mode.EntranceShuffle.Value) &&
                                    (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)))
                                    return dP;

                                if (_game.Items.Has(ItemType.DPBigKey) && _game.Items.Has(ItemType.DPSmallKey) &&
                                    (_game.Items.Has(ItemType.Gloves) || _game.Mode.EntranceShuffle.Value) &&
                                    (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)))
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)dP);

                                if (Available > 1 && _game.Items.Has(ItemType.DPBigKey) && _game.Items.Has(ItemType.DPSmallKey))
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)dP);

                                if (Available < 2 && _game.Items.Has(ItemType.DPBigKey) &&
                                    (_game.Items.Has(ItemType.Gloves) || _game.Mode.EntranceShuffle.Value) &&
                                    (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)))
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)dP);

                                if (Available > 2 && _game.Items.Has(ItemType.DPSmallKey))
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)dP);

                                if (Available > 3 && _game.Items.Has(ItemType.DPBigKey))
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)dP);

                                if (Available > 4)
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)dP);

                                break;
                        }
                        
                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Regions[RegionID.DesertPalace].Accessibility >= AccessibilityLevel.SequenceBreak)
                        {
                            switch (_game.Mode.DungeonItemShuffle.Value)
                            {
                                case DungeonItemShuffle.Standard:
                                    return Available;
                                case DungeonItemShuffle.MapsCompasses:

                                    if ((_game.Items.Has(ItemType.Gloves) || _game.Mode.EntranceShuffle.Value) &&
                                        (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)))
                                        return Available;
                                    
                                    return Math.Max(0, Available - 1);

                                case DungeonItemShuffle.MapsCompassesSmallKeys:

                                    if (_game.Items.Has(ItemType.DPSmallKey) &&
                                        (_game.Items.Has(ItemType.Gloves) || _game.Mode.EntranceShuffle.Value) &&
                                        (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)))
                                        return Available;

                                    if (_game.Items.Has(ItemType.DPSmallKey))
                                        return Math.Max(0, Available - 1);

                                    if ((_game.Items.Has(ItemType.Gloves) || _game.Mode.EntranceShuffle.Value) &&
                                        (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)))
                                        return Math.Max(0, Available - 2);
                                    
                                    return Math.Max(0, Available - 3);

                                case DungeonItemShuffle.Keysanity:

                                    if (_game.Items.Has(ItemType.DPBigKey) && _game.Items.Has(ItemType.DPSmallKey) &&
                                        (_game.Items.Has(ItemType.Gloves) || _game.Mode.EntranceShuffle.Value) &&
                                        (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)))
                                        return Available;

                                    if (_game.Items.Has(ItemType.DPBigKey) && _game.Items.Has(ItemType.DPSmallKey))
                                        return Math.Max(0, Available - 1);

                                    if (_game.Items.Has(ItemType.DPBigKey) &&
                                        (_game.Items.Has(ItemType.Gloves) || _game.Mode.EntranceShuffle.Value) &&
                                        (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)))
                                        return Math.Max(0, Available - 2);

                                    if (_game.Items.Has(ItemType.DPSmallKey))
                                        return Math.Max(0, Available - 2);

                                    if (_game.Items.Has(ItemType.DPBigKey))
                                        return Math.Max(0, Available - 3);
                                    
                                    return Math.Max(0, Available - 4);
                            }
                        }

                        return 0;
                    };

                    _updateOnDungeonItemShuffleChange = true;

                    _regionSubscriptions.Add(RegionID.DesertPalace, new Mode());

                    _itemSubscriptions.Add(ItemType.Boots, new Mode());
                    _itemSubscriptions.Add(ItemType.Gloves, new Mode() { EntranceShuffle = false });
                    _itemSubscriptions.Add(ItemType.Lamp, new Mode());
                    _itemSubscriptions.Add(ItemType.FireRod, new Mode());
                    _itemSubscriptions.Add(ItemType.DPBigKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity });
                    _itemSubscriptions.Add(ItemType.DPSmallKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys });

                    break;
                case LocationID.TowerOfHera:

                    _mapCompass = 2;
                    _smallKey = 1;
                    _bigKey = 1;
                    _baseTotal = 2;
                    Name = "Dungeon";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel tH = _game.Regions[RegionID.TowerOfHera].Accessibility;

                        switch (_game.Mode.DungeonItemShuffle.Value)
                        {
                            case DungeonItemShuffle.Standard:
                            case DungeonItemShuffle.MapsCompasses:

                                if (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod))
                                    return tH;

                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)tH);

                            case DungeonItemShuffle.MapsCompassesSmallKeys:

                                if ((_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)) &&
                                    _game.Items.Has(ItemType.ToHSmallKey))
                                    return tH;
                                
                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)tH);

                            case DungeonItemShuffle.Keysanity:

                                if ((_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)) &&
                                    _game.Items.Has(ItemType.ToHSmallKey) && _game.Items.Has(ItemType.ToHBigKey))
                                    return tH;

                                if ((_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)) &&
                                    _game.Items.Has(ItemType.ToHSmallKey))
                                {
                                    if (Available > 1 && _game.Items.Has(ItemType.Hookshot))
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)tH);

                                    if (Available > 3)
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)tH);
                                }

                                if (Available > 1 && _game.Items.Has(ItemType.ToHBigKey))
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)tH);

                                if (Available > 2 && _game.Items.Has(ItemType.Hookshot))
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)tH);

                                if (Available > 4)
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)tH);

                                break;
                        }

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Regions[RegionID.TowerOfHera].Accessibility >= AccessibilityLevel.SequenceBreak)
                        {
                            switch (_game.Mode.DungeonItemShuffle.Value)
                            {
                                case DungeonItemShuffle.Standard:
                                case DungeonItemShuffle.MapsCompasses:
                                case DungeonItemShuffle.MapsCompassesSmallKeys:
                                    return Available;
                                case DungeonItemShuffle.Keysanity:

                                    if ((_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)) &&
                                        _game.Items.Has(ItemType.ToHSmallKey) && _game.Items.Has(ItemType.ToHBigKey))
                                        return Available;

                                    if ((_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)) &&
                                        _game.Items.Has(ItemType.ToHSmallKey))
                                    {
                                        if (_game.Items.Has(ItemType.Hookshot))
                                            return Math.Max(0, Available - 1);
                                        
                                        return Math.Max(0, Available - 3);
                                    }

                                    if (_game.Items.Has(ItemType.ToHBigKey))
                                        return Math.Max(0, Available - 1);

                                    if (_game.Items.Has(ItemType.Hookshot))
                                        return Math.Max(0, Available - 2);
                                    
                                    return Math.Max(0, Available - 4);
                            }
                        }

                        return 0;
                    };

                    _updateOnDungeonItemShuffleChange = true;

                    _regionSubscriptions.Add(RegionID.TowerOfHera, new Mode());

                    _itemSubscriptions.Add(ItemType.Lamp, new Mode());
                    _itemSubscriptions.Add(ItemType.FireRod, new Mode());
                    _itemSubscriptions.Add(ItemType.ToHSmallKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys });
                    _itemSubscriptions.Add(ItemType.ToHBigKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity });
                    _itemSubscriptions.Add(ItemType.Hookshot, new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity });

                    break;
                case LocationID.PalaceOfDarkness:

                    _mapCompass = 2;
                    _smallKey = 6;
                    _bigKey = 1;
                    _baseTotal = 5;
                    Name = "Dungeon";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel pD = _game.Regions[RegionID.PalaceOfDarkness].Accessibility;

                        switch (_game.Mode.DungeonItemShuffle.Value)
                        {
                            case DungeonItemShuffle.Standard:

                                if (_game.Items.Has(ItemType.Lamp) && _game.Items.Has(ItemType.Hammer) &&
                                    _game.Items.CanShootArrows())
                                    return pD;
                                
                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)pD);

                            case DungeonItemShuffle.MapsCompasses:

                                if (_game.Items.Has(ItemType.Lamp) && _game.Items.Has(ItemType.Hammer) &&
                                    _game.Items.CanShootArrows())
                                    return pD;

                                if (_game.Items.Has(ItemType.Hammer) && _game.Items.CanShootArrows())
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)pD);

                                if (Available > 1)
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)pD);

                                break;
                            case DungeonItemShuffle.MapsCompassesSmallKeys:

                                if (_game.Items.Has(ItemType.Lamp) && _game.Items.Has(ItemType.Hammer) &&
                                    _game.Items.CanShootArrows() && _game.Items.Has(ItemType.PoDSmallKey, 5))
                                    return pD;

                                if (_game.Items.Has(ItemType.Hammer) && _game.Items.CanShootArrows() &&
                                    _game.Items.Has(ItemType.PoDSmallKey, 4))
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)pD);

                                if (_game.Items.CanClearRedEyegoreGoriyaRooms() || _game.Items.Has(ItemType.Bottle))
                                {
                                    if (Available > 1 && (_game.Items.Has(ItemType.PoDSmallKey, 4) ||
                                        (_game.Items.Has(ItemType.PoDSmallKey, 3) && _game.Items.Has(ItemType.Hammer))))
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)pD);

                                    if (Available > 2 && (_game.Items.Has(ItemType.PoDSmallKey, 3) ||
                                        (_game.Items.Has(ItemType.PoDSmallKey, 2) && _game.Items.Has(ItemType.Hammer))))
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)pD);

                                    if (Available > 3 && (_game.Items.Has(ItemType.PoDSmallKey, 2) ||
                                        (_game.Items.Has(ItemType.PoDSmallKey) && _game.Items.Has(ItemType.Hammer))))
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)pD);

                                    if (Available > 8 &&
                                        (_game.Items.Has(ItemType.PoDSmallKey) || _game.Items.Has(ItemType.Hammer)))
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)pD);

                                    if (Available > 10)
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)pD);
                                }

                                if (Available > 3 && _game.Items.Has(ItemType.PoDSmallKey, 4))
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)pD);

                                if (Available > 4 && _game.Items.Has(ItemType.PoDSmallKey, 3))
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)pD);

                                if (Available > 5 && _game.Items.Has(ItemType.PoDSmallKey, 2))
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)pD);

                                if (Available > 10 && _game.Items.Has(ItemType.PoDSmallKey))
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)pD);

                                if (Available > 12)
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)pD);

                                break;
                            case DungeonItemShuffle.Keysanity:

                                if (_game.Items.Has(ItemType.Lamp) && _game.Items.Has(ItemType.Hammer) &&
                                    _game.Items.CanShootArrows() && _game.Items.Has(ItemType.PoDSmallKey, 5) &&
                                    _game.Items.Has(ItemType.PoDBigKey))
                                    return pD;

                                if (_game.Items.Has(ItemType.Hammer) && _game.Items.CanShootArrows() &&
                                    _game.Items.Has(ItemType.PoDSmallKey, 4) && _game.Items.Has(ItemType.PoDBigKey))
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)pD);

                                if (_game.Items.CanClearRedEyegoreGoriyaRooms() || _game.Items.Has(ItemType.Bottle))
                                {
                                    if (_game.Items.Has(ItemType.PoDSmallKey, 4) ||
                                        (_game.Items.Has(ItemType.PoDSmallKey, 3) && _game.Items.Has(ItemType.Hammer)))
                                    {
                                        if (Available > 1 && _game.Items.Has(ItemType.PoDBigKey))
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)pD);

                                        if (Available > 2)
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)pD);
                                    }

                                    if (_game.Items.Has(ItemType.PoDSmallKey, 3) ||
                                        (_game.Items.Has(ItemType.PoDSmallKey, 2) && _game.Items.Has(ItemType.Hammer)))
                                    {
                                        if (Available > 2 && _game.Items.Has(ItemType.PoDBigKey))
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)pD);

                                        if (Available > 3)
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)pD);
                                    }

                                    if (_game.Items.Has(ItemType.PoDSmallKey, 2) ||
                                        (_game.Items.Has(ItemType.PoDSmallKey) && _game.Items.Has(ItemType.Hammer)))
                                    {
                                        if (Available > 3 && _game.Items.Has(ItemType.PoDBigKey))
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)pD);

                                        if (Available > 4)
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)pD);
                                    }

                                    if (Available > 9 &&
                                        (_game.Items.Has(ItemType.PoDSmallKey) || _game.Items.Has(ItemType.Hammer)))
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)pD);

                                    if (Available > 11)
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)pD);
                                }

                                if (_game.Items.Has(ItemType.PoDSmallKey, 4))
                                {
                                    if (Available > 3 && _game.Items.Has(ItemType.PoDBigKey))
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)pD);

                                    if (Available > 4)
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)pD);
                                }

                                if (_game.Items.Has(ItemType.PoDSmallKey, 3))
                                {
                                    if (Available > 4 && _game.Items.Has(ItemType.PoDBigKey))
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)pD);

                                    if (Available > 5)
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)pD);
                                }

                                if (_game.Items.Has(ItemType.PoDSmallKey, 2))
                                {
                                    if (Available > 5 && _game.Items.Has(ItemType.PoDBigKey))
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)pD);

                                    if (Available > 6)
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)pD);
                                }

                                if (Available > 11 && _game.Items.Has(ItemType.PoDSmallKey))
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)pD);

                                if (Available > 13)
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)pD);

                                break;
                        }

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Regions[RegionID.PalaceOfDarkness].Accessibility >= AccessibilityLevel.SequenceBreak)
                        {
                            switch (_game.Mode.DungeonItemShuffle.Value)
                            {
                                case DungeonItemShuffle.Standard:
                                    return Available;
                                case DungeonItemShuffle.MapsCompasses:

                                    if (_game.Items.Has(ItemType.Hammer) && _game.Items.CanShootArrows())
                                        return Available;
                                    
                                    return Math.Max(0, Available - 1);

                                case DungeonItemShuffle.MapsCompassesSmallKeys:

                                    if (_game.Items.Has(ItemType.Hammer) && _game.Items.CanShootArrows() &&
                                        _game.Items.Has(ItemType.PoDSmallKey, 4))
                                        return Available;

                                    if (_game.Items.CanClearRedEyegoreGoriyaRooms() || _game.Items.Has(ItemType.Bottle))
                                    {
                                        if (_game.Items.Has(ItemType.PoDSmallKey, 4) ||
                                            (_game.Items.Has(ItemType.PoDSmallKey, 3) && _game.Items.Has(ItemType.Hammer)))
                                            return Math.Max(0, Available - 1);

                                        if (_game.Items.Has(ItemType.PoDSmallKey, 3) ||
                                            (_game.Items.Has(ItemType.PoDSmallKey, 2) && _game.Items.Has(ItemType.Hammer)))
                                            return Math.Max(0, Available - 2);

                                        if (_game.Items.Has(ItemType.PoDSmallKey, 2) ||
                                            (_game.Items.Has(ItemType.PoDSmallKey) && _game.Items.Has(ItemType.Hammer)))
                                            return Math.Max(0, Available - 3);

                                        if (_game.Items.Has(ItemType.PoDSmallKey) || _game.Items.Has(ItemType.Hammer))
                                            return Math.Max(0, Available - 8);
                                        
                                        return Math.Max(0, Available - 10);
                                    }

                                    if (_game.Items.Has(ItemType.PoDSmallKey, 4))
                                        return Math.Max(0, Available - 3);

                                    if (_game.Items.Has(ItemType.PoDSmallKey, 3))
                                        return Math.Max(0, Available - 4);

                                    if (_game.Items.Has(ItemType.PoDSmallKey, 2))
                                        return Math.Max(0, Available - 5);

                                    if (_game.Items.Has(ItemType.PoDSmallKey))
                                        return Math.Max(0, Available - 10);
                                    
                                    return Math.Max(0, Available - 12);

                                case DungeonItemShuffle.Keysanity:

                                    if (_game.Items.Has(ItemType.Hammer) && _game.Items.CanShootArrows() &&
                                        _game.Items.Has(ItemType.PoDSmallKey, 4) && _game.Items.Has(ItemType.PoDBigKey))
                                        return Available;

                                    if (_game.Items.CanClearRedEyegoreGoriyaRooms() || _game.Items.Has(ItemType.Bottle))
                                    {
                                        if (_game.Items.Has(ItemType.PoDSmallKey, 4) ||
                                            (_game.Items.Has(ItemType.PoDSmallKey, 3) && _game.Items.Has(ItemType.Hammer)))
                                        {
                                            if (_game.Items.Has(ItemType.PoDBigKey))
                                                return Math.Max(0, Available - 1);
                                            
                                            return Math.Max(0, Available - 2);
                                        }

                                        if (_game.Items.Has(ItemType.PoDSmallKey, 3) ||
                                            (_game.Items.Has(ItemType.PoDSmallKey, 2) && _game.Items.Has(ItemType.Hammer)))
                                        {
                                            if (_game.Items.Has(ItemType.PoDBigKey) && Available > 2)
                                                return Math.Max(0, Available - 2);
                                            
                                            return Math.Max(0, Available - 3);
                                        }

                                        if (_game.Items.Has(ItemType.PoDSmallKey, 2) ||
                                            (_game.Items.Has(ItemType.PoDSmallKey) && _game.Items.Has(ItemType.Hammer)))
                                        {
                                            if (_game.Items.Has(ItemType.PoDBigKey))
                                                return Math.Max(0, Available - 3);
                                            
                                            return Math.Max(0, Available - 4);
                                        }

                                        if (_game.Items.Has(ItemType.PoDSmallKey) || _game.Items.Has(ItemType.Hammer))
                                            return Math.Max(0, Available - 9);
                                        
                                        return Math.Max(0, Available - 11);
                                    }

                                    if (_game.Items.Has(ItemType.PoDSmallKey, 4))
                                    {
                                        if (_game.Items.Has(ItemType.PoDBigKey))
                                            return Math.Max(0, Available - 3);
                                        
                                        return Math.Max(0, Available - 4);
                                    }

                                    if (_game.Items.Has(ItemType.PoDSmallKey, 3))
                                    {
                                        if (_game.Items.Has(ItemType.PoDBigKey))
                                            return Math.Max(0, Available - 4);
                                        
                                        return Math.Max(0, Available - 5);
                                    }

                                    if (_game.Items.Has(ItemType.PoDSmallKey, 2))
                                    {
                                        if (_game.Items.Has(ItemType.PoDBigKey) && Available > 5)
                                            return Math.Max(0, Available - 5);
                                        
                                        return Math.Max(0, Available - 6);
                                    }

                                    if (_game.Items.Has(ItemType.PoDSmallKey))
                                        return Math.Max(0, Available - 11);
                                    
                                    return Math.Max(0, Available - 13);
                            }
                        }

                        return 0;
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

                    break;
                case LocationID.SwampPalace:

                    _mapCompass = 2;
                    _smallKey = 1;
                    _bigKey = 1;
                    _baseTotal = 6;
                    Name = "Dungeon";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel sP = _game.Regions[RegionID.SwampPalace].Accessibility;

                        switch (_game.Mode.DungeonItemShuffle.Value)
                        {
                            case DungeonItemShuffle.Standard:

                                if (_game.Items.Has(ItemType.Flippers))
                                {
                                    if (_game.Items.Has(ItemType.Hammer))
                                    {
                                        if (_game.Items.Has(ItemType.Hookshot))
                                            return sP;

                                        if (Available > 2)
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)sP);
                                    }

                                    if (Available > 5)
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)sP);
                                }

                                break;
                            case DungeonItemShuffle.MapsCompasses:

                                if (_game.Items.Has(ItemType.Flippers))
                                {
                                    if (_game.Items.Has(ItemType.Hammer))
                                    {
                                        if (_game.Items.Has(ItemType.Hookshot))
                                            return sP;

                                        if (Available > 4)
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)sP);
                                    }

                                    if (Available > 7)
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)sP);
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
                                                return sP;

                                            if (Available > 4)
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)sP);
                                        }

                                        if (Available > 7)
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)sP);
                                    }

                                    if (Available > 8)
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)sP);
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
                                                return sP;

                                            if (Available > 1 && _game.Items.Has(ItemType.Hookshot))
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)sP);

                                            if (Available > 4 && _game.Items.Has(ItemType.SPBigKey))
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)sP);

                                            if (Available > 5)
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)sP);
                                        }

                                        if (Available > 8)
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)sP);
                                    }

                                    if (Available > 9)
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)sP);
                                }

                                break;
                        }

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Regions[RegionID.SwampPalace].Accessibility >= AccessibilityLevel.SequenceBreak)
                        {
                            switch (_game.Mode.DungeonItemShuffle.Value)
                            {
                                case DungeonItemShuffle.Standard:

                                    if (_game.Items.Has(ItemType.Flippers))
                                    {
                                        if (_game.Items.Has(ItemType.Hammer))
                                        {
                                            if (_game.Items.Has(ItemType.Hookshot))
                                                return Available;

                                            return Math.Max(0, Available - 2);
                                        }

                                        return Math.Max(0, Available - 5);
                                    }

                                    break;
                                case DungeonItemShuffle.MapsCompasses:

                                    if (_game.Items.Has(ItemType.Flippers))
                                    {
                                        if (_game.Items.Has(ItemType.Hammer))
                                        {
                                            if (_game.Items.Has(ItemType.Hookshot))
                                                return Available;

                                            return Math.Max(0, Available - 4);
                                        }

                                        return Math.Max(0, Available - 7);
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
                                                    return Available;

                                                return Math.Max(0, Available - 4);
                                            }

                                            return Math.Max(0, Available - 7);
                                        }

                                        return Math.Max(0, Available - 8);
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
                                                    return Available;

                                                if (_game.Items.Has(ItemType.Hookshot))
                                                    return Math.Max(0, Available - 1);

                                                if (_game.Items.Has(ItemType.SPBigKey))
                                                    return Math.Max(0, Available - 4);

                                                return Math.Max(0, Available - 5);
                                            }

                                            return Math.Max(0, Available - 8);
                                        }

                                        return Math.Max(0, Available - 9);
                                    }

                                    break;
                            }
                        };

                        return 0;
                    };

                    _updateOnDungeonItemShuffleChange = true;

                    _regionSubscriptions.Add(RegionID.SwampPalace, new Mode());

                    _itemSubscriptions.Add(ItemType.Flippers, new Mode());
                    _itemSubscriptions.Add(ItemType.Hookshot, new Mode());
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.SPSmallKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys });
                    _itemSubscriptions.Add(ItemType.SPBigKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity });

                    break;
                case LocationID.SkullWoods:

                    _mapCompass = 2;
                    _smallKey = 3;
                    _bigKey = 1;
                    _baseTotal = 2;
                    Name = "Dungeon";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel sW = _game.Regions[RegionID.SkullWoods].Accessibility;

                        switch (_game.Mode.DungeonItemShuffle.Value)
                        {
                            case DungeonItemShuffle.Standard:
                            case DungeonItemShuffle.MapsCompasses:

                                if ((_game.Items.Has(ItemType.FireRod) || _game.Mode.EntranceShuffle.Value) &&
                                    _game.Items.CanRemoveCurtains())
                                    return sW;

                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)sW);

                            case DungeonItemShuffle.MapsCompassesSmallKeys:

                                if (_game.Items.Has(ItemType.FireRod) || _game.Mode.EntranceShuffle.Value)
                                {
                                    if (_game.Items.CanRemoveCurtains())
                                    {
                                        if (_game.Items.Has(ItemType.SWSmallKey))
                                            return sW;

                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)sW);
                                    }

                                    if (Available > 1)
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)sW);
                                }

                                if (Available > 2)
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)sW);

                                break;
                            case DungeonItemShuffle.Keysanity:

                                if (_game.Items.Has(ItemType.FireRod) || _game.Mode.EntranceShuffle.Value)
                                {
                                    if (_game.Items.CanRemoveCurtains())
                                    {
                                        if (_game.Items.Has(ItemType.SWBigKey) && _game.Items.Has(ItemType.SPSmallKey))
                                            return sW;

                                        if (_game.Items.Has(ItemType.SWBigKey))
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)sW);

                                        if (Available > 1)
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)sW);
                                    }

                                    if (Available > 1 && _game.Items.Has(ItemType.SWBigKey))
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)sW);

                                    if (Available > 2)
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)sW);
                                }

                                if (Available > 2 && _game.Items.Has(ItemType.SWBigKey))
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)sW);

                                if (Available > 3)
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)sW);

                                break;
                        }
                        
                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Regions[RegionID.SkullWoods].Accessibility >= AccessibilityLevel.SequenceBreak)
                        {
                            switch (_game.Mode.DungeonItemShuffle.Value)
                            {
                                case DungeonItemShuffle.Standard:
                                case DungeonItemShuffle.MapsCompasses:
                                    return Available;
                                case DungeonItemShuffle.MapsCompassesSmallKeys:

                                    if (_game.Items.Has(ItemType.FireRod) || _game.Mode.EntranceShuffle.Value)
                                    {
                                        if (_game.Items.CanRemoveCurtains())
                                            return Available;

                                        return Math.Max(0, Available - 1);
                                    }

                                    return Math.Max(0, Available - 2);

                                case DungeonItemShuffle.Keysanity:

                                    if (_game.Items.Has(ItemType.FireRod) || _game.Mode.EntranceShuffle.Value)
                                    {
                                        if (_game.Items.CanRemoveCurtains())
                                        {
                                            if (_game.Items.Has(ItemType.SWBigKey))
                                                return Available;

                                            return Math.Max(0, Available - 1);
                                        }

                                        if (_game.Items.Has(ItemType.SWBigKey))
                                            return Math.Max(0, Available - 1);

                                        return Math.Max(0, Available - 2);
                                    }

                                    if (_game.Items.Has(ItemType.SWBigKey))
                                        return Math.Max(0, Available - 2);

                                    return Math.Max(0, Available - 3);
                            }
                        }

                        return 0;
                    };

                    _updateOnDungeonItemShuffleChange = true;

                    _regionSubscriptions.Add(RegionID.SkullWoods, new Mode());

                    _itemSubscriptions.Add(ItemType.FireRod, new Mode());
                    _itemSubscriptions.Add(ItemType.Sword, new Mode());
                    _itemSubscriptions.Add(ItemType.SWBigKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity });
                    _itemSubscriptions.Add(ItemType.SWSmallKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys });

                    break;
                case LocationID.ThievesTown:

                    _mapCompass = 2;
                    _smallKey = 1;
                    _bigKey = 1;
                    _baseTotal = 4;
                    Name = "Dungeon";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel tT = _game.Regions[RegionID.ThievesTown].Accessibility;

                        switch (_game.Mode.DungeonItemShuffle.Value)
                        {
                            case DungeonItemShuffle.Standard:
                            case DungeonItemShuffle.MapsCompasses:

                                if (_game.Items.Has(ItemType.Hammer))
                                    return tT;

                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)tT);

                            case DungeonItemShuffle.MapsCompassesSmallKeys:

                                if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.TTSmallKey))
                                    return tT;

                                if (Available > 1)
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)tT);

                                break;
                            case DungeonItemShuffle.Keysanity:

                                if (_game.Items.Has(ItemType.TTBigKey))
                                {
                                    if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.TTSmallKey))
                                        return tT;

                                    if (Available > 1)
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)tT);
                                }

                                if (Available > 4)
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)tT);

                                break;
                        }
                        
                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Regions[RegionID.ThievesTown].Accessibility >= AccessibilityLevel.SequenceBreak)
                        {
                            switch (_game.Mode.DungeonItemShuffle.Value)
                            {
                                case DungeonItemShuffle.Standard:
                                case DungeonItemShuffle.MapsCompasses:
                                    return Available;
                                case DungeonItemShuffle.MapsCompassesSmallKeys:

                                    if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.TTSmallKey))
                                        return Available;
                                    
                                    return Math.Max(0, Available - 1);

                                case DungeonItemShuffle.Keysanity:

                                    if (_game.Items.Has(ItemType.TTBigKey))
                                    {
                                        if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.TTSmallKey))
                                            return Available;
                                        
                                        return Math.Max(0, Available - 1);
                                    }
                                    
                                    return Math.Max(0, Available - 4);
                            }
                        }

                        return 0;
                    };

                    _updateOnDungeonItemShuffleChange = true;

                    _regionSubscriptions.Add(RegionID.ThievesTown, new Mode());

                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.TTBigKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity });
                    _itemSubscriptions.Add(ItemType.TTSmallKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys });

                    break;
                case LocationID.IcePalace:

                    _mapCompass = 2;
                    _smallKey = 2;
                    _bigKey = 1;
                    _baseTotal = 3;
                    Name = "Dungeon";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel iP = _game.Regions[RegionID.IcePalace].Accessibility;

                        switch (_game.Mode.DungeonItemShuffle.Value)
                        {
                            case DungeonItemShuffle.Standard:

                                if (_game.Items.CanMeltThings())
                                {
                                    if (_game.Items.Has(ItemType.Hammer) &&
                                        (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.CaneOfSomaria)))
                                        return iP;

                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)iP);
                                }

                                break;
                            case DungeonItemShuffle.MapsCompasses:

                                if (_game.Items.CanMeltThings())
                                {
                                    if (_game.Items.Has(ItemType.Hammer))
                                    {
                                        if (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.CaneOfSomaria))
                                            return iP;

                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)iP);
                                    }

                                    if (Available > 1)
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)iP);
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
                                            return iP;

                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)iP);
                                    }

                                    if (Available > 3)
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)iP);
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
                                            return iP;

                                        if (_game.Items.Has(ItemType.IPBigKey))
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)iP);

                                        if (Available > 1)
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)iP);
                                    }

                                    if (Available > 3 && _game.Items.Has(ItemType.IPBigKey))
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)iP);

                                    if (Available > 4)
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)iP);
                                }

                                break;
                        }

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Regions[RegionID.IcePalace].Accessibility >= AccessibilityLevel.SequenceBreak)
                        {
                            switch (_game.Mode.DungeonItemShuffle.Value)
                            {
                                case DungeonItemShuffle.Standard:

                                    if (_game.Items.CanMeltThings())
                                        return Available;

                                    break;
                                case DungeonItemShuffle.MapsCompasses:

                                    if (_game.Items.CanMeltThings())
                                    {
                                        if (_game.Items.Has(ItemType.Hammer))
                                            return Available;
                                        
                                        return Math.Max(0, Available - 1);
                                    }

                                    break;
                                case DungeonItemShuffle.MapsCompassesSmallKeys:

                                    if (_game.Items.CanMeltThings())
                                    {
                                        if (_game.Items.Has(ItemType.Hammer))
                                            return Available;
                                        
                                        return Math.Max(0, Available - 3);
                                    }

                                    break;
                                case DungeonItemShuffle.Keysanity:

                                    if (_game.Items.CanMeltThings())
                                    {
                                        if (_game.Items.Has(ItemType.Hammer))
                                        {
                                            if (_game.Items.Has(ItemType.IPBigKey))
                                                return Available;
                                            
                                            return Math.Max(0, Available - 1);
                                        }

                                        if (_game.Items.Has(ItemType.IPBigKey))
                                            return Math.Max(0, Available - 3);
                                        
                                        return Math.Max(0, Available - 4);
                                    }

                                    break;
                            }
                        }

                        return 0;
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

                    break;
                case LocationID.MiseryMire:

                    _mapCompass = 2;
                    _smallKey = 3;
                    _bigKey = 1;
                    _baseTotal = 2;
                    Name = "Dungeon";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel mM = _game.Regions[RegionID.MiseryMire].Accessibility;

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
                                                return mM;
                                        }
                                    }

                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)mM);
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
                                                return mM;

                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)mM);
                                        }

                                        if (Available > 1)
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)mM);
                                    }

                                    if (Available > 3)
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)mM);
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
                                                return mM;

                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)mM);
                                        }

                                        if (Available > 1)
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)mM);
                                    }

                                    if (Available > 2 && _game.Items.Has(ItemType.CaneOfSomaria))
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)mM);

                                    if (Available > 3)
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)mM);
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
                                                return mM;

                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)mM);
                                        }

                                        if (Available > 1 && _game.Items.Has(ItemType.MMBigKey))
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)mM);

                                        if (Available > 2)
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)mM);
                                    }

                                    if (Available > 2 &&
                                        _game.Items.Has(ItemType.CaneOfSomaria) && _game.Items.Has(ItemType.MMBigKey))
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)mM);

                                    if (Available > 3 && _game.Items.Has(ItemType.MMBigKey))
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)mM);

                                    if (Available > 4)
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)mM);
                                }

                                break;
                        }

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Regions[RegionID.MiseryMire].Accessibility >= AccessibilityLevel.SequenceBreak)
                        {
                            switch (_game.Mode.DungeonItemShuffle.Value)
                            {
                                case DungeonItemShuffle.Standard:

                                    if (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots))
                                        return Available;

                                    break;
                                case DungeonItemShuffle.MapsCompasses:

                                    if (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots))
                                    {
                                        if (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod))
                                        {
                                            if (_game.Items.Has(ItemType.CaneOfSomaria))
                                                return Available;
                                            
                                            return Math.Max(0, Available - 1);
                                        }
                                        
                                        return Math.Max(0, Available - 3);
                                    }

                                    break;
                                case DungeonItemShuffle.MapsCompassesSmallKeys:

                                    if (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots))
                                    {
                                        if (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod))
                                        {
                                            if (_game.Items.Has(ItemType.CaneOfSomaria))
                                                return Available;
                                            
                                            return Math.Max(0, Available - 1);
                                        }

                                        if (_game.Items.Has(ItemType.CaneOfSomaria))
                                            return Math.Max(0, Available - 2);
                                        
                                        return Math.Max(0, Available - 3);
                                    }

                                    break;
                                case DungeonItemShuffle.Keysanity:

                                    if (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots))
                                    {
                                        if (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod))
                                        {
                                            if (_game.Items.Has(ItemType.CaneOfSomaria) && _game.Items.Has(ItemType.MMBigKey))
                                                return Available;

                                            if (_game.Items.Has(ItemType.MMBigKey))
                                                return Math.Max(0, Available - 1);
                                            
                                            return Math.Max(0, Available - 2);
                                        }

                                        if (_game.Items.Has(ItemType.CaneOfSomaria) && _game.Items.Has(ItemType.MMBigKey))
                                            return Math.Max(0, Available - 2);

                                        if (_game.Items.Has(ItemType.MMBigKey))
                                            return Math.Max(0, Available - 3);
                                        
                                        return Math.Max(0, Available - 4);
                                    }

                                    break;
                            }
                        }

                        return 0;
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

                    break;
                case LocationID.TurtleRock:

                    _mapCompass = 2;
                    _smallKey = 4;
                    _bigKey = 1;
                    _baseTotal = 5;
                    Name = "Dungeon";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel backAccess = _game.Regions[RegionID.TurtleRockBack].Accessibility;
                        AccessibilityLevel frontAccess = _game.Regions[RegionID.TurtleRockFront].Accessibility;

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
                                    return frontAccess;

                                back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                    (byte)backAccess);

                                if (_game.Items.Has(ItemType.CaneOfSomaria))
                                    front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)frontAccess);

                                return (AccessibilityLevel)Math.Max((byte)back, (byte)front);

                            case DungeonItemShuffle.MapsCompasses:

                                if (_game.Items.Has(ItemType.CaneOfSomaria) && _game.Items.Has(ItemType.FireRod) &&
                                    _game.Items.Has(ItemType.Lamp) &&
                                    (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                    _game.Items.Has(ItemType.Cape) || _game.Items.Has(ItemType.CaneOfByrna) ||
                                    _game.Items.Has(ItemType.Shield, 3)))
                                    return frontAccess;

                                if (Available > 1)
                                    back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)backAccess);

                                if (_game.Items.Has(ItemType.CaneOfSomaria))
                                {
                                    back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)backAccess);

                                    if (Available > 2)
                                        front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)frontAccess);

                                    if (_game.Items.Has(ItemType.FireRod))
                                        front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)frontAccess);
                                }

                                return (AccessibilityLevel)Math.Max((byte)back, (byte)front);

                            case DungeonItemShuffle.MapsCompassesSmallKeys:

                                if (Available > 5)
                                    back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)backAccess);

                                if (_game.Items.Has(ItemType.Hookshot))
                                {
                                    if (Available > 4)
                                        back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)backAccess);

                                    if (_game.Items.Has(ItemType.TRSmallKey))
                                    {
                                        if (Available > 3)
                                            back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)backAccess);
                                    }
                                }

                                if (_game.Items.Has(ItemType.CaneOfSomaria))
                                {
                                    if (Available > 10)
                                        front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)frontAccess);

                                    if (Available > 4)
                                        back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)backAccess);

                                    if (Available > 3)
                                    {
                                        backFront = (AccessibilityLevel)Math.Min(Math.Min((byte)AccessibilityLevel.Partial, (byte)backAccess),
                                            (byte)frontAccess);
                                    }

                                    if (_game.Items.Has(ItemType.FireRod))
                                    {
                                        if (Available > 8)
                                            front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)frontAccess);

                                        if (Available > 1)
                                            backFront = (AccessibilityLevel)Math.Min(Math.Min((byte)AccessibilityLevel.Partial, (byte)backAccess),
                                                (byte)frontAccess);
                                    }

                                    if (_game.Items.Has(ItemType.TRSmallKey))
                                    {
                                        if (Available > 9)
                                            front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)frontAccess);

                                        if (Available > 3)
                                            back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)backAccess);

                                        if (Available > 2)
                                        {
                                            backFront = (AccessibilityLevel)Math.Min(Math.Min((byte)AccessibilityLevel.Partial, (byte)backAccess),
                                                (byte)frontAccess);
                                        }

                                        if (_game.Items.Has(ItemType.FireRod))
                                        {
                                            if (Available > 7)
                                                front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)frontAccess);

                                            if (Available > 1)
                                                back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)backAccess);
                                            
                                            backFront = (AccessibilityLevel)Math.Min(Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)backAccess),
                                                (byte)frontAccess);

                                            if (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                                _game.Items.Has(ItemType.Cape) || _game.Items.Has(ItemType.CaneOfByrna) ||
                                                _game.Items.Has(ItemType.Shield, 3))
                                                backFront = (AccessibilityLevel)Math.Min((byte)backAccess, (byte)frontAccess);
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.TRSmallKey, 2))
                                    {
                                        if (Available > 7)
                                            front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)frontAccess);

                                        if (Available > 2)
                                            back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)backAccess);

                                        if (_game.Items.Has(ItemType.FireRod))
                                        {
                                            if (Available > 5)
                                                front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)frontAccess);

                                            back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)backAccess);

                                            if (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                                _game.Items.Has(ItemType.Cape) || _game.Items.Has(ItemType.CaneOfByrna) ||
                                                _game.Items.Has(ItemType.Shield, 3))
                                                back = backAccess;
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.TRSmallKey, 3))
                                    {
                                        if (Available > 3)
                                            front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)frontAccess);

                                        if (_game.Items.Has(ItemType.FireRod))
                                        {
                                            if (Available > 1)
                                                front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)frontAccess);
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.TRSmallKey, 4))
                                    {
                                        if (Available > 2)
                                            front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)frontAccess);

                                        if (_game.Items.Has(ItemType.FireRod))
                                        {
                                            front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)frontAccess);

                                            if ((_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                                _game.Items.Has(ItemType.Cape) || _game.Items.Has(ItemType.CaneOfByrna) ||
                                                _game.Items.Has(ItemType.Shield, 3)) && _game.Items.Has(ItemType.Lamp))
                                                front = frontAccess;
                                        }
                                    }
                                }

                                return (AccessibilityLevel)Math.Max(Math.Max((byte)back, (byte)front), (byte)backFront);

                           case DungeonItemShuffle.Keysanity:

                                if (Available > 6)
                                    back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)backAccess);

                                if (_game.Items.Has(ItemType.TRBigKey))
                                {
                                    if (Available > 5)
                                        back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)backAccess);

                                    if (_game.Items.Has(ItemType.Hookshot))
                                    {
                                        if (Available > 4)
                                            back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)backAccess);

                                        if (_game.Items.Has(ItemType.TRSmallKey))
                                        {
                                            if (Available > 3)
                                                back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)backAccess);
                                        }
                                    }
                                }

                                if (_game.Items.Has(ItemType.CaneOfSomaria))
                                {
                                    if (Available > 11)
                                        front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)frontAccess);

                                    if (Available > 5)
                                        back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)backAccess);

                                    if (Available > 4)
                                    {
                                        backFront = (AccessibilityLevel)Math.Min(Math.Min((byte)AccessibilityLevel.Partial, (byte)backAccess),
                                            (byte)frontAccess);
                                    }

                                    if (_game.Items.Has(ItemType.TRBigKey))
                                    {
                                        if (Available > 4)
                                            back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)backAccess);

                                        if (Available > 3)
                                        {
                                            backFront = (AccessibilityLevel)Math.Min(Math.Min((byte)AccessibilityLevel.Partial, (byte)backAccess),
                                                (byte)frontAccess);
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.FireRod))
                                    {
                                        if (Available > 9)
                                            front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)frontAccess);

                                        if (Available > 2)
                                        {
                                            backFront = (AccessibilityLevel)Math.Min(Math.Min((byte)AccessibilityLevel.Partial, (byte)backAccess),
                                                (byte)frontAccess);
                                        }

                                        if (_game.Items.Has(ItemType.TRBigKey))
                                        {
                                            if (Available > 1)
                                            {
                                                backFront = (AccessibilityLevel)Math.Min(Math.Min((byte)AccessibilityLevel.Partial, (byte)backAccess),
                                                    (byte)frontAccess);
                                            }
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.TRSmallKey))
                                    {
                                        if (Available > 10)
                                            front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)frontAccess);

                                        if (Available > 4)
                                            back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)backAccess);

                                        if (_game.Items.Has(ItemType.TRBigKey))
                                        {
                                            if (Available > 3)
                                                back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)backAccess);

                                            if (Available > 2)
                                            {
                                                backFront = (AccessibilityLevel)Math.Min(Math.Min((byte)AccessibilityLevel.Partial, (byte)backAccess),
                                                    (byte)frontAccess);
                                            }
                                        }

                                        if (_game.Items.Has(ItemType.FireRod))
                                        {
                                            if (Available > 8)
                                                front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)frontAccess);

                                            if (Available > 2)
                                                back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)backAccess);

                                            if (_game.Items.Has(ItemType.TRBigKey))
                                            {
                                                if (Available > 1)
                                                    back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)backAccess);

                                                backFront = (AccessibilityLevel)Math.Min(Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)backAccess),
                                                    (byte)frontAccess);

                                                if (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                                    _game.Items.Has(ItemType.Cape) || _game.Items.Has(ItemType.CaneOfByrna) ||
                                                    _game.Items.Has(ItemType.Shield, 3))
                                                    backFront = (AccessibilityLevel)Math.Min((byte)backAccess, (byte)frontAccess);
                                            }
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.TRSmallKey, 2))
                                    {
                                        if (Available > 9)
                                            front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)frontAccess);

                                        if (_game.Items.Has(ItemType.TRBigKey))
                                        {
                                            if (Available > 7)
                                                front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)frontAccess);

                                            if (Available > 2)
                                                back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)backAccess);
                                        }

                                        if (_game.Items.Has(ItemType.FireRod))
                                        {
                                            if (Available > 7)
                                                front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)frontAccess);

                                            if (_game.Items.Has(ItemType.TRBigKey))
                                            {
                                                if (Available > 5)
                                                    front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)frontAccess);
                                                
                                                back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)backAccess);

                                                if (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                                    _game.Items.Has(ItemType.Cape) || _game.Items.Has(ItemType.CaneOfByrna) ||
                                                    _game.Items.Has(ItemType.Shield, 3))
                                                    back = backAccess;
                                            }
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.TRBigKey))
                                    {
                                        if (_game.Items.Has(ItemType.TRSmallKey, 3))
                                        {
                                            if (Available > 3)
                                                front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)frontAccess);

                                            if (_game.Items.Has(ItemType.FireRod))
                                            {
                                                if (Available > 1)
                                                    front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)frontAccess);
                                            }
                                        }

                                        if (_game.Items.Has(ItemType.TRSmallKey, 4))
                                        {
                                            if (Available > 2)
                                                front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)frontAccess);

                                            if (_game.Items.Has(ItemType.FireRod))
                                            {
                                                front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)frontAccess);

                                                if ((_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                                    _game.Items.Has(ItemType.Cape) || _game.Items.Has(ItemType.CaneOfByrna) ||
                                                    _game.Items.Has(ItemType.Shield, 3)) && _game.Items.Has(ItemType.Lamp))
                                                    front = frontAccess;
                                            }
                                        }
                                    }
                                }

                                return (AccessibilityLevel)Math.Max(Math.Max((byte)back, (byte)front), (byte)backFront);

                        }

                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        bool backAccess = _game.Regions[RegionID.TurtleRockBack].Accessibility >= AccessibilityLevel.SequenceBreak;
                        bool frontAccess = _game.Regions[RegionID.TurtleRockFront].Accessibility >= AccessibilityLevel.SequenceBreak;

                        int back = 0;
                        int backFront = 0;
                        int front = 0;

                        switch (_game.Mode.DungeonItemShuffle.Value)
                        {
                            case DungeonItemShuffle.Standard:

                                if (backAccess)
                                    back = Available;

                                if (_game.Items.Has(ItemType.CaneOfSomaria) && frontAccess)
                                    front = Available;

                                return Math.Max(back, front);

                            case DungeonItemShuffle.MapsCompasses:

                                if (_game.Items.Has(ItemType.CaneOfSomaria) && _game.Items.Has(ItemType.FireRod) &&
                                    _game.Items.Has(ItemType.Lamp) &&
                                    (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                    _game.Items.Has(ItemType.Cape) || _game.Items.Has(ItemType.CaneOfByrna) ||
                                    _game.Items.Has(ItemType.Shield, 3)) && frontAccess)
                                    return Available;

                                if (backAccess)
                                    back = Math.Max(0, Available - 1);

                                if (_game.Items.Has(ItemType.CaneOfSomaria))
                                {
                                    if (backAccess)
                                        back = Available;

                                    if (frontAccess)
                                        front = Math.Max(0, Available - 2);

                                    if (_game.Items.Has(ItemType.FireRod) && frontAccess)
                                        front = Available;
                                }

                                return Math.Max(back, front);

                            case DungeonItemShuffle.MapsCompassesSmallKeys:

                                if (backAccess)
                                    back = Math.Max(0, Available - 5);

                                if (_game.Items.Has(ItemType.Hookshot))
                                {
                                    if (backAccess)
                                        back = Math.Max(0, Available - 4);

                                    if (_game.Items.Has(ItemType.TRSmallKey) && backAccess)
                                        back = Math.Max(0, Available - 3);
                                }

                                if (_game.Items.Has(ItemType.CaneOfSomaria))
                                {
                                    if (frontAccess)
                                        front = Math.Max(0, Available - 10);

                                    if (backAccess)
                                        back = Math.Max(0, Available - 4);

                                    if (frontAccess && backAccess)
                                        backFront = Math.Max(0, Available - 3);

                                    if (_game.Items.Has(ItemType.FireRod))
                                    {
                                        if (frontAccess)
                                            front = Math.Max(0, Available - 8);

                                        if (frontAccess && backAccess)
                                            backFront = Math.Max(0, Available - 1);
                                    }

                                    if (_game.Items.Has(ItemType.TRSmallKey))
                                    {
                                        if (frontAccess)
                                            front = Math.Max(0, Available - 9);

                                        if (backAccess)
                                            back = Math.Max(0, Available - 3);

                                        if (frontAccess && backAccess)
                                            backFront = Math.Max(0, Available - 2);

                                        if (_game.Items.Has(ItemType.FireRod))
                                        {
                                            if (frontAccess)
                                                front = Math.Max(0, Available - 7);

                                            if (backAccess)
                                                back = Math.Max(0, Available - 1);

                                            if (frontAccess && backAccess)
                                                backFront = Available;
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.TRSmallKey, 2))
                                    {
                                        if (frontAccess)
                                            front = Math.Max(0, Available - 7);

                                        if (backAccess)
                                            back = Math.Max(0, Available - 2);

                                        if (_game.Items.Has(ItemType.FireRod))
                                        {
                                            if (frontAccess)
                                                front = Math.Max(0, Available - 5);

                                            if (backAccess)
                                                back = Available;
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.TRSmallKey, 3))
                                    {
                                        if (frontAccess)
                                            front = Math.Max(0, Available - 3);

                                        if (_game.Items.Has(ItemType.FireRod) && frontAccess)
                                            front = Math.Max(0, Available - 1);
                                    }

                                    if (_game.Items.Has(ItemType.TRSmallKey, 4))
                                    {
                                        if (frontAccess)
                                            front = Math.Max(0, Available - 2);

                                        if (_game.Items.Has(ItemType.FireRod) && frontAccess)
                                            front = Available;
                                    }
                                }

                                return Math.Max(Math.Max(back, front), backFront);

                            case DungeonItemShuffle.Keysanity:

                                if (backAccess)
                                    back = Math.Max(0, Available - 6);

                                if (_game.Items.Has(ItemType.TRBigKey))
                                {
                                    if (backAccess)
                                        back = Math.Max(0, Available - 5);

                                    if (_game.Items.Has(ItemType.Hookshot))
                                    {
                                        if (backAccess)
                                            back = Math.Max(0, Available - 4);

                                        if (_game.Items.Has(ItemType.TRSmallKey) & backAccess)
                                            back = Math.Max(0, Available - 3);
                                    }
                                }

                                if (_game.Items.Has(ItemType.CaneOfSomaria))
                                {
                                    if (frontAccess)
                                        front = Math.Max(0, Available - 11);

                                    if (backAccess)
                                        back = Math.Max(0, Available - 5);

                                    if (frontAccess && backAccess)
                                        backFront = Math.Max(0, Available - 4);

                                    if (_game.Items.Has(ItemType.TRBigKey))
                                    {
                                        if (backAccess)
                                            back = Math.Max(0, Available - 4);

                                        if (frontAccess && backAccess)
                                            backFront = Math.Max(0, Available - 3);
                                    }

                                    if (_game.Items.Has(ItemType.FireRod))
                                    {
                                        if (frontAccess)
                                            front = Math.Max(0, Available - 9);

                                        if (frontAccess && backAccess)
                                            backFront = Math.Max(0, Available - 2);

                                        if (_game.Items.Has(ItemType.TRBigKey) && frontAccess && backAccess)
                                            backFront = Math.Max(0, Available - 1);
                                    }

                                    if (_game.Items.Has(ItemType.TRSmallKey))
                                    {
                                        if (frontAccess)
                                            front = Math.Max(0, Available - 10);

                                        if (backAccess)
                                            back = Math.Max(0, Available - 4);

                                        if (_game.Items.Has(ItemType.TRBigKey))
                                        {
                                            if (backAccess)
                                                back = Math.Max(0, Available - 3);

                                            if (frontAccess && backAccess)
                                                backFront = Math.Max(0, Available - 2);
                                        }

                                        if (_game.Items.Has(ItemType.FireRod))
                                        {
                                            if (frontAccess)
                                                front = Math.Max(0, Available - 8);

                                            if (backAccess)
                                                back = Math.Max(0, Available - 2);

                                            if (_game.Items.Has(ItemType.TRBigKey))
                                            {
                                                if (backAccess)
                                                    back = Math.Max(0, Available - 1);

                                                if (frontAccess && backAccess)
                                                    backFront = Available;
                                            }
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.TRSmallKey, 2))
                                    {
                                        if (frontAccess)
                                            front = Math.Max(0, Available - 9);

                                        if (_game.Items.Has(ItemType.TRBigKey))
                                        {
                                            if (frontAccess)
                                                front = Math.Max(0, Available - 7);

                                            if (backAccess)
                                                back = Math.Max(0, Available - 2);
                                        }

                                        if (_game.Items.Has(ItemType.FireRod))
                                        {
                                            if (frontAccess)
                                                front = Math.Max(0, Available - 7);

                                            if (_game.Items.Has(ItemType.TRBigKey))
                                            {
                                                if (frontAccess)
                                                    front = Math.Max(0, Available - 5);

                                                if (backAccess)
                                                    back = Available;
                                            }
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.TRBigKey))
                                    {
                                        if (_game.Items.Has(ItemType.TRSmallKey, 3))
                                        {
                                            if (frontAccess)
                                                front = Math.Max(0, Available - 3);

                                            if (_game.Items.Has(ItemType.FireRod) && frontAccess)
                                                front = Math.Max(0, Available - 1);
                                        }

                                        if (_game.Items.Has(ItemType.TRSmallKey, 4))
                                        {
                                            if (frontAccess)
                                                front = Math.Max(0, Available - 2);

                                            if (_game.Items.Has(ItemType.FireRod) && frontAccess)
                                                front = Available;
                                        }
                                    }
                                }

                                return Math.Max(Math.Max(back, front), backFront);
                        }

                        return 0;
                    };

                    _updateOnItemPlacementChange = true;
                    _updateOnDungeonItemShuffleChange = true;

                    _regionSubscriptions.Add(RegionID.TurtleRockFront, new Mode());
                    _regionSubscriptions.Add(RegionID.TurtleRockBack, new Mode());

                    _itemSubscriptions.Add(ItemType.CaneOfSomaria, new Mode());
                    _itemSubscriptions.Add(ItemType.FireRod, new Mode());
                    _itemSubscriptions.Add(ItemType.Lamp, new Mode());
                    _itemSubscriptions.Add(ItemType.Cape, new Mode() { ItemPlacement = ItemPlacement.Basic });
                    _itemSubscriptions.Add(ItemType.CaneOfByrna, new Mode() { ItemPlacement = ItemPlacement.Basic });
                    _itemSubscriptions.Add(ItemType.Shield, new Mode() { ItemPlacement = ItemPlacement.Basic });
                    _itemSubscriptions.Add(ItemType.TRSmallKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys });
                    _itemSubscriptions.Add(ItemType.TRBigKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity });
                    _itemSubscriptions.Add(ItemType.Hookshot, new Mode());

                    break;
                case LocationID.GanonsTower:

                    _mapCompass = 2;
                    _smallKey = 4;
                    _bigKey = 1;
                    _baseTotal = 20;
                    Name = "Dungeon";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel gT = _game.Regions[RegionID.GanonsTower].Accessibility;

                        switch (_game.Mode.DungeonItemShuffle.Value)
                        {
                            case DungeonItemShuffle.Standard:

                                if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Hookshot) &&
                                    _game.Items.Has(ItemType.Boots) && _game.Items.Has(ItemType.CaneOfSomaria) &&
                                    _game.Items.CanClearRedEyegoreGoriyaRooms() && _game.Items.Has(ItemType.FireRod))
                                    return gT;

                                if (_game.Items.Has(ItemType.Hammer) &&
                                    (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots)))
                                {
                                    if (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp))
                                    {
                                        if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        {
                                            if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)gT);

                                            if (Available > 1)
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);
                                        }

                                        if (Available > 2 && _game.Items.CanClearRedEyegoreGoriyaRooms())
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);
                                    }

                                    if (Available > 5)
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);
                                }

                                if (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp))
                                {
                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                    {
                                        if (Available > 4 && _game.Items.CanClearRedEyegoreGoriyaRooms())
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);

                                        if (Available > 7)
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);
                                    }

                                    if (Available > 14 && _game.Items.CanClearRedEyegoreGoriyaRooms())
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);
                                }

                                if (Available > 16 && _game.Items.Has(ItemType.CaneOfSomaria))
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);

                                if (Available > 17)
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);

                                break;
                            case DungeonItemShuffle.MapsCompasses:

                                if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Hookshot) &&
                                    _game.Items.Has(ItemType.Boots) && _game.Items.Has(ItemType.CaneOfSomaria) &&
                                    _game.Items.CanClearRedEyegoreGoriyaRooms() && _game.Items.Has(ItemType.FireRod))
                                    return gT;

                                if (_game.Items.Has(ItemType.Hammer) &&
                                    (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots)))
                                {
                                    if (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp))
                                    {
                                        if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        {
                                            if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)gT);

                                            if (Available > 3)
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);
                                        }

                                        if (Available > 4 && _game.Items.CanClearRedEyegoreGoriyaRooms())
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);
                                    }

                                    if (Available > 6)
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);
                                }

                                if (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp))
                                {
                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                    {
                                        if (Available > 6 && _game.Items.CanClearRedEyegoreGoriyaRooms())
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);

                                        if (Available > 9)
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);
                                    }

                                    if (Available > 16 && _game.Items.CanClearRedEyegoreGoriyaRooms())
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);
                                }

                                if (Available > 18 && _game.Items.Has(ItemType.CaneOfSomaria))
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);

                                if (Available > 19)
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);

                                break;
                            case DungeonItemShuffle.MapsCompassesSmallKeys:

                                if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Hookshot) &&
                                    _game.Items.Has(ItemType.Boots) && _game.Items.Has(ItemType.CaneOfSomaria) &&
                                    _game.Items.CanClearRedEyegoreGoriyaRooms() && _game.Items.Has(ItemType.FireRod) &&
                                    _game.Items.Has(ItemType.GTSmallKey, 2))
                                    return gT;

                                if (_game.Items.Has(ItemType.Hammer) &&
                                    (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots)))
                                {
                                    if (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp))
                                    {
                                        if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        {
                                            if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                            {
                                                if (_game.Items.Has(ItemType.GTSmallKey, 2))
                                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)gT);

                                                if (Available > 1 && _game.Items.Has(ItemType.GTSmallKey))
                                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);

                                                if (Available > 2)
                                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);
                                            }

                                            if (Available > 4 && _game.Items.Has(ItemType.GTSmallKey))
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);

                                            if (Available > 5)
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);
                                        }

                                        if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                        {
                                            if (Available > 5 && _game.Items.Has(ItemType.GTSmallKey))
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);

                                            if (Available > 6)
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                    {
                                        if (Available > 8 && _game.Items.Has(ItemType.GTSmallKey))
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);

                                        if (Available > 9)
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);
                                    }

                                    if (Available > 9 && _game.Items.Has(ItemType.GTSmallKey))
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);

                                    if (Available > 10)
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);
                                }

                                if (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp))
                                {
                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                    {
                                        if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                        {
                                            if (Available > 9 && _game.Items.Has(ItemType.GTSmallKey) &&
                                                (_game.Items.Has(ItemType.Boots) || _game.Items.Has(ItemType.Hookshot)))
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);

                                            if (Available > 10)
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);
                                        }

                                        if (Available > 13)
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);
                                    }

                                    if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                    {
                                        if (Available > 20 &&
                                            (_game.Items.Has(ItemType.Boots) || _game.Items.Has(ItemType.Hookshot)))
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);

                                        if (Available > 21)
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);
                                    }
                                }

                                if (Available > 22 && _game.Items.Has(ItemType.CaneOfSomaria))
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);

                                if (Available > 23)
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);

                                break;
                            case DungeonItemShuffle.Keysanity:

                                if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Hookshot) &&
                                    _game.Items.Has(ItemType.Boots) && _game.Items.Has(ItemType.CaneOfSomaria) &&
                                    _game.Items.CanClearRedEyegoreGoriyaRooms() && _game.Items.Has(ItemType.FireRod) &&
                                    _game.Items.Has(ItemType.GTSmallKey, 2) && _game.Items.Has(ItemType.GTBigKey))
                                    return gT;

                                if (_game.Items.Has(ItemType.Hammer) &&
                                    (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots)))
                                {
                                    if (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp))
                                    {
                                        if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        {
                                            if (_game.Items.Has(ItemType.GTBigKey))
                                            {
                                                if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                                {
                                                    if (_game.Items.Has(ItemType.GTSmallKey, 2))
                                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)gT);

                                                    if (Available > 1 && _game.Items.Has(ItemType.GTSmallKey))
                                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);

                                                    if (Available > 2)
                                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);
                                                }

                                                if (Available > 4 && _game.Items.Has(ItemType.GTSmallKey))
                                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);

                                                if (Available > 5)
                                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);
                                            }

                                            if (Available > 5 && _game.Items.Has(ItemType.GTSmallKey))
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);

                                            if (Available > 6)
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);
                                        }

                                        if (_game.Items.Has(ItemType.GTBigKey))
                                        {
                                            if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                            {
                                                if (Available > 5 && _game.Items.Has(ItemType.GTSmallKey))
                                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);

                                                if (Available > 6)
                                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);
                                            }
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                    {
                                        if (_game.Items.Has(ItemType.GTBigKey))
                                        {
                                            if (Available > 8 && _game.Items.Has(ItemType.GTSmallKey))
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);

                                            if (Available > 9)
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);
                                        }
                                        
                                        if (Available > 9 && _game.Items.Has(ItemType.GTSmallKey))
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);

                                        if (Available > 10)
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);
                                    }

                                    if (_game.Items.Has(ItemType.GTBigKey))
                                    {
                                        if (Available > 9 && _game.Items.Has(ItemType.GTSmallKey))
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);

                                        if (Available > 10)
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);
                                    }

                                    if (Available > 10 && _game.Items.Has(ItemType.GTSmallKey))
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);

                                    if (Available > 11)
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);
                                }

                                if (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp))
                                {
                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                    {
                                        if (_game.Items.Has(ItemType.GTBigKey))
                                        {
                                            if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                            {
                                                if (Available > 9 && _game.Items.Has(ItemType.GTSmallKey) &&
                                                    (_game.Items.Has(ItemType.Boots) || _game.Items.Has(ItemType.Hookshot)))
                                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);

                                                if (Available > 10)
                                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);
                                            }

                                            if (Available > 13)
                                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);
                                        }

                                        if (Available > 14)
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);
                                    }

                                    if (_game.Items.CanClearRedEyegoreGoriyaRooms() && _game.Items.Has(ItemType.GTBigKey))
                                    {
                                        if (Available > 20 && _game.Items.Has(ItemType.GTSmallKey) &&
                                            (_game.Items.Has(ItemType.Boots) || _game.Items.Has(ItemType.Hookshot)))
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);

                                        if (Available > 21)
                                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);
                                    }
                                }

                                if (Available > 22 && _game.Items.Has(ItemType.CaneOfSomaria))
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);

                                if (Available > 23)
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Partial, (byte)gT);

                                break;
                        }
                        
                        return AccessibilityLevel.None;
                    };

                    GetAccessible = () =>
                    {
                        if (_game.Regions[RegionID.GanonsTower].Accessibility >= AccessibilityLevel.SequenceBreak)
                        {
                            switch (_game.Mode.DungeonItemShuffle.Value)
                            {
                                case DungeonItemShuffle.Standard:

                                    if (_game.Items.Has(ItemType.Hammer) &&
                                        (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots)))
                                    {
                                        if (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp))
                                        {
                                            if (_game.Items.Has(ItemType.CaneOfSomaria))
                                            {
                                                if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                                    return Available;
                                                
                                                return Math.Max(0, Available - 1);
                                            }

                                            if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                                return Math.Max(0, Available - 2);
                                        }
                                        
                                        return Math.Max(0, Available - 5);
                                    }

                                    if (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp))
                                    {
                                        if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        {
                                            if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                                return Math.Max(0, Available - 4);

                                            return Math.Max(0, Available - 7);
                                        }

                                        if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                            return Math.Max(0, Available - 14);
                                    }

                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        return Math.Max(0, Available - 16);
                                    
                                    return Math.Max(0, Available - 17);

                                case DungeonItemShuffle.MapsCompasses:

                                    if (_game.Items.Has(ItemType.Hammer) &&
                                        (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots)))
                                    {
                                        if (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp))
                                        {
                                            if (_game.Items.Has(ItemType.CaneOfSomaria))
                                            {
                                                if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                                    return Available;

                                                return Math.Max(0, Available - 3);
                                            }

                                            if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                                return Math.Max(0, Available - 4);
                                        }

                                        return Math.Max(0, Available - 7);
                                    }

                                    if (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp))
                                    {
                                        if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        {
                                            if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                                return Math.Max(0, Available - 6);

                                            return Math.Max(0, Available - 9);
                                        }

                                        if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                            return Math.Max(0, Available - 16);
                                    }

                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        return Math.Max(0, Available - 18);

                                    return Math.Max(0, Available - 19);

                                case DungeonItemShuffle.MapsCompassesSmallKeys:

                                    if (_game.Items.Has(ItemType.Hammer) &&
                                        (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots)))
                                    {
                                        if (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp))
                                        {
                                            if (_game.Items.Has(ItemType.CaneOfSomaria))
                                            {
                                                if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                                {
                                                    if (_game.Items.Has(ItemType.GTSmallKey, 2))
                                                        return Available;

                                                    if (_game.Items.Has(ItemType.GTSmallKey))
                                                        return Math.Max(0, Available - 1);
                                                    
                                                    return Math.Max(0, Available - 2);
                                                }
                                                
                                                if (_game.Items.Has(ItemType.GTSmallKey))
                                                    return Math.Max(0, Available - 4);

                                                return Math.Max(0, Available - 5);
                                            }

                                            if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                            {
                                                if (_game.Items.Has(ItemType.GTSmallKey))
                                                    return Math.Max(0, Available - 5);
                                                
                                                return Math.Max(0, Available - 6);
                                            }
                                        }

                                        if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        {
                                            if (_game.Items.Has(ItemType.GTSmallKey))
                                                return Math.Max(0, Available - 8);
                                            
                                            return Math.Max(0, Available - 9);
                                        }

                                        if (_game.Items.Has(ItemType.GTSmallKey))
                                            return Math.Max(0, Available - 9);
                                        
                                        return Math.Max(0, Available - 10);
                                    }

                                    if (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp))
                                    {
                                        if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        {
                                            if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                            {
                                                if (_game.Items.Has(ItemType.GTSmallKey) &&
                                                    (_game.Items.Has(ItemType.Boots) || _game.Items.Has(ItemType.Hookshot)))
                                                    return Math.Max(0, Available - 9);
                                                
                                                return Math.Max(0, Available - 10);
                                            }
                                            
                                            return Math.Max(0, Available - 13);
                                        }

                                        if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                        {
                                            if (_game.Items.Has(ItemType.GTSmallKey) &&
                                                (_game.Items.Has(ItemType.Boots) || _game.Items.Has(ItemType.Hookshot)))
                                                return Math.Max(0, Available - 20);
                                            
                                            return Math.Max(0, Available - 21);
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        return Math.Max(0, Available - 22);
                                    
                                    return Math.Max(0, Available - 23);

                                case DungeonItemShuffle.Keysanity:

                                    if (_game.Items.Has(ItemType.Hammer) &&
                                        (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots)))
                                    {
                                        if (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp))
                                        {
                                            if (_game.Items.Has(ItemType.CaneOfSomaria))
                                            {
                                                if (_game.Items.Has(ItemType.GTBigKey))
                                                {
                                                    if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                                    {
                                                        if (_game.Items.Has(ItemType.GTSmallKey, 2))
                                                            return Available;

                                                        if (_game.Items.Has(ItemType.GTSmallKey))
                                                            return Math.Max(0, Available - 1);

                                                        return Math.Max(0, Available - 2);
                                                    }

                                                    if (_game.Items.Has(ItemType.GTSmallKey))
                                                        return Math.Max(0, Available - 4);

                                                    return Math.Max(0, Available - 5);
                                                }

                                                if (_game.Items.Has(ItemType.GTSmallKey))
                                                    return Math.Max(0, Available - 5);

                                                return Math.Max(0, Available - 6);
                                            }

                                            if (_game.Items.Has(ItemType.GTBigKey))
                                            {
                                                if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                                {
                                                    if (_game.Items.Has(ItemType.GTSmallKey, 2))
                                                        return Math.Max(0, Available - 5);

                                                    if (_game.Items.Has(ItemType.GTSmallKey))
                                                        return Math.Max(0, Available - 6);
                                                    
                                                    return Math.Max(0, Available - 7);
                                                }
                                            }
                                        }

                                        if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        {
                                            if (_game.Items.Has(ItemType.GTBigKey))
                                            {
                                                if (_game.Items.Has(ItemType.GTSmallKey))
                                                    return Math.Max(0, Available - 8);
                                                
                                                return Math.Max(0, Available - 9);
                                            }

                                            if (_game.Items.Has(ItemType.GTSmallKey))
                                                return Math.Max(0, Available - 9);
                                            
                                            return Math.Max(0, Available - 10);
                                        }

                                        if (_game.Items.Has(ItemType.GTBigKey))
                                        {
                                            if (_game.Items.Has(ItemType.GTSmallKey))
                                                return Math.Max(0, Available - 9);

                                            return Math.Max(0, Available - 10);
                                        }

                                        if (_game.Items.Has(ItemType.GTSmallKey))
                                            return Math.Max(0, Available - 10);

                                        return Math.Max(0, Available - 11);
                                    }

                                    if (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp))
                                    {
                                        if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        {
                                            if (_game.Items.Has(ItemType.GTBigKey))
                                            {
                                                if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                                {
                                                    if (_game.Items.Has(ItemType.GTSmallKey) &&
                                                        (_game.Items.Has(ItemType.Boots) || _game.Items.Has(ItemType.Hookshot)))
                                                        return Math.Max(0, Available - 9);
                                                    
                                                    return Math.Max(0, Available - 10);
                                                }
                                                
                                                return Math.Max(0, Available - 13);
                                            }
                                            
                                            return Math.Max(0, Available - 14);
                                        }

                                        if (_game.Items.CanClearRedEyegoreGoriyaRooms() && _game.Items.Has(ItemType.GTBigKey))
                                        {
                                            if (_game.Items.Has(ItemType.GTSmallKey) &&
                                                (_game.Items.Has(ItemType.Boots) || _game.Items.Has(ItemType.Hookshot)))
                                                return Math.Max(0, Available - 20);
                                            
                                            return Math.Max(0, Available - 21);
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        return Math.Max(0, Available - 22);
                                    
                                    return Math.Max(0, Available - 23);
                            }
                        }

                        return 0;
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

                    break;
            }

            if (RequiredMode == null)
                RequiredMode = new Mode();

            SetTotal();

            _game.Mode.PropertyChanged += OnModeChanged;

            PropertyChanged += OnRequirementChanged;

            UpdateRegionSubscriptions();
            UpdateItemSubscriptions();

            UpdateAccessibility();

            SubscribeToAutoTracker();
        }

        private void OnPropertyChanging(string propertyName)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (propertyName == nameof(Available) && !IsAvailable())
                CollectMarkingItem();
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

            if (e.PropertyName == nameof(_game.Mode.EntranceShuffle))
                UpdateAccessibility();

        }

        private void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateAccessibility();
        }

        private void OnMemoryCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_game.AutoTracker.IsInGame() && !UserManipulated)
                AutoTrack();
        }

        private void SubscribeToAutoTracker()
        {
            if (_subscribeToRoomMemory)
                _game.AutoTracker.NPCItemMemory.CollectionChanged += OnMemoryCollectionChanged;

            if (_subscribeToOverworldEventMemory)
                _game.AutoTracker.OverworldEventMemory.CollectionChanged += OnMemoryCollectionChanged;

            if (_subscribeToItemMemory)
                _game.AutoTracker.ItemMemory.CollectionChanged += OnMemoryCollectionChanged;

            if (_subscribeToNPCItemMemory)
                _game.AutoTracker.NPCItemMemory.CollectionChanged += OnMemoryCollectionChanged;
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

            if (GetAccessible != null)
                Accessible = GetAccessible();
        }

        private void CollectMarkingItem()
        {
            if (Marking.HasValue)
            {
                if (Enum.TryParse(Marking.Value.ToString(), out ItemType itemType))
                    _game.Items[itemType].Change(1);

                Marking = null;
            }
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
