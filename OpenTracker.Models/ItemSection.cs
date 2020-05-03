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
        public bool HasMarking { get; }
        public Mode RequiredMode { get; }
        public Action AutoTrack { get; }
        public Func<(int, AccessibilityLevel)> GetAccessibility { get; }
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

                        if (lightWorld >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Items.Has(ItemType.GreenPendant) && _game.Items.Has(ItemType.Pendant, 2))
                            {
                                if (lightWorld == AccessibilityLevel.Normal)
                                {
                                    if (_game.Mode.ItemPlacement == ItemPlacement.Advanced || _game.Items.Has(ItemType.Book))
                                        return (Available, AccessibilityLevel.Normal);
                                }
                                
                                return (Available, AccessibilityLevel.SequenceBreak);
                            }

                            if (_game.Items.Has(ItemType.Book))
                                return (0, AccessibilityLevel.Inspect);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.OverworldEvent, 128, 64);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.OverworldEventMemory[128].PropertyChanged += OnMemoryChanged;

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

                        if (lightWorld >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Items.Has(ItemType.Aga) && _game.Items.Has(ItemType.Boots) &&
                                (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)))
                                return (Available, lightWorld);

                            return (0, AccessibilityLevel.Inspect);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 453, 2);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.RoomMemory[453].PropertyChanged += OnMemoryChanged;

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
                        AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                        if (lightWorld >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                                return (Available, lightWorld);
                        }

                        return (0, AccessibilityLevel.None);
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

                    _game.AutoTracker.RoomMemory[570].PropertyChanged += OnMemoryChanged;
                    _game.AutoTracker.RoomMemory[571].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.BlindsHouse:

                    _baseTotal = 1;
                    Name = "Bomb";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                        if (lightWorld >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                                return (Available, lightWorld);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 570, 16);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.RoomMemory[570].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.TheWell when index == 0:

                    _baseTotal = 4;
                    Name = "Cave";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                        if (lightWorld >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                                return (Available, lightWorld);

                            return (Available, AccessibilityLevel.SequenceBreak);
                        }

                        return (0, AccessibilityLevel.None);
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

                    _game.AutoTracker.RoomMemory[94].PropertyChanged += OnMemoryChanged;
                    _game.AutoTracker.RoomMemory[95].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.TheWell:

                    _baseTotal = 1;
                    Name = "Bomb";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                        if (lightWorld >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                                return (Available, lightWorld);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 94, 16);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.RoomMemory[94].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.BottleVendor:

                    _baseTotal = 1;
                    Name = "Man";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                        if (lightWorld >= AccessibilityLevel.SequenceBreak)
                            return (Available, _game.Regions[RegionID.LightWorld].Accessibility);

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 137, 2);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.ItemMemory[137].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
                case LocationID.ChickenHouse:

                    _baseTotal = 1;
                    Name = "Bombable Wall";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                        if (lightWorld >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                                return (Available, lightWorld);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 528, 16);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.RoomMemory[528].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.Tavern:

                    _baseTotal = 1;
                    Name = "Back Room";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                        if (lightWorld >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                                return (Available, lightWorld);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 518, 16);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.RoomMemory[518].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.SickKid:

                    _baseTotal = 1;
                    Name = "By The Bed";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                        if (lightWorld >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Items.Has(ItemType.Bottle))
                                return (Available, _game.Regions[RegionID.LightWorld].Accessibility);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 0, 4);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.NPCItemMemory[0].PropertyChanged += OnMemoryChanged;

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
                            AccessibilityLevel direct = AccessibilityLevel.None;
                            AccessibilityLevel mirror = AccessibilityLevel.None;

                            if (lightWorld >= AccessibilityLevel.SequenceBreak && _game.Items.Has(ItemType.Hammer))
                            {
                                if (_game.Items.Has(ItemType.Powder))
                                    direct = lightWorld;
                                else if (_game.Items.Has(ItemType.Mushroom) && _game.Items.Has(ItemType.CaneOfSomaria))
                                    direct = AccessibilityLevel.SequenceBreak;
                            }

                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Gloves, 2) &&
                                _game.Items.Has(ItemType.Mirror))
                            {
                                if (_game.Items.Has(ItemType.Powder))
                                    mirror = AccessibilityLevel.Normal;
                                else if (_game.Items.Has(ItemType.Mushroom) && _game.Items.Has(ItemType.CaneOfSomaria))
                                    mirror = AccessibilityLevel.SequenceBreak;
                            }

                            AccessibilityLevel access = (AccessibilityLevel)Math.Max((byte)direct, (byte)mirror);

                            if (access >= AccessibilityLevel.SequenceBreak)
                                return (Available, access);
                        }
                        else
                        {
                            if (lightWorld >= AccessibilityLevel.SequenceBreak && 
                                _game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Hammer))
                            {
                                if (_game.Items.Has(ItemType.Powder))
                                    return (Available, lightWorld);

                                if (_game.Items.Has(ItemType.Mushroom) && _game.Items.Has(ItemType.CaneOfSomaria))
                                    return (Available, AccessibilityLevel.SequenceBreak);
                            }
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 1, 128);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.NPCItemMemory[1].PropertyChanged += OnMemoryChanged;

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
                        AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.RaceGameAccess))
                                return (Available, AccessibilityLevel.Normal);

                            AccessibilityLevel dWSouth = _game.Regions[RegionID.DarkWorldSouth].Accessibility;

                            AccessibilityLevel direct = AccessibilityLevel.None;
                            AccessibilityLevel mirror = AccessibilityLevel.None;

                            if (lightWorld >= AccessibilityLevel.SequenceBreak)
                            {
                                if (!_game.Mode.EntranceShuffle.Value)
                                    direct = lightWorld;
                                else
                                    direct = AccessibilityLevel.Inspect;
                            }

                            if (dWSouth >= AccessibilityLevel.SequenceBreak && _game.Items.Has(ItemType.Mirror))
                                mirror = dWSouth;

                            AccessibilityLevel access = (AccessibilityLevel)Math.Max((byte)direct, (byte)mirror);

                            if (access >= AccessibilityLevel.SequenceBreak)
                                return (Available, access);
                            else
                                return (0, access);
                        }
                        else
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.RaceGameAccess))
                                return (Available, AccessibilityLevel.Normal);

                            if (lightWorld >= AccessibilityLevel.SequenceBreak)
                            {
                                if (_game.Items.Has(ItemType.MoonPearl) && !_game.Mode.EntranceShuffle.Value)
                                    return (Available, lightWorld);

                                return (0, AccessibilityLevel.Inspect);
                            }
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.OverworldEvent, 40, 64);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.OverworldEventMemory[40].PropertyChanged += OnMemoryChanged;

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

                        if (lightWorld >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Items.Has(ItemType.Boots) &&
                                (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)))
                                return (Available, lightWorld);

                            return (0, AccessibilityLevel.Inspect);
                        }
                        
                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 0, 128);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.NPCItemMemory[0].PropertyChanged += OnMemoryChanged;

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
                        AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                        if (lightWorld >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                                return (Available, lightWorld);

                            return (0, AccessibilityLevel.Inspect);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 1, 16);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.NPCItemMemory[1].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.LostWoods:

                    _baseTotal = 1;
                    Name = "Hideout";
                    HasMarking = true;

                    RequiredMode = new Mode() { EntranceShuffle = false };

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                        if (lightWorld >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                                return (Available, lightWorld);

                            return (0, AccessibilityLevel.Inspect);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 451, 2);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.RoomMemory[451].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
                case LocationID.CastleSecret when index == 0:

                    _baseTotal = 1;
                    Name = "Uncle";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                        if (lightWorld >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                                return (Available, lightWorld);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 134, 1);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.ItemMemory[134].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.CastleSecret:

                    _baseTotal = 1;
                    Name = "Hallway";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                        if (lightWorld >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                                return (Available, lightWorld);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 170, 16);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.RoomMemory[170].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.LinksHouse:

                    _baseTotal = 1;
                    Name = "By The Door";

                    GetAccessibility = () => { return (Available, AccessibilityLevel.Normal); };

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

                    _game.AutoTracker.RoomMemory[1].PropertyChanged += OnMemoryChanged;
                    _game.AutoTracker.RoomMemory[520].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    break;
                case LocationID.GroveDiggingSpot:

                    _baseTotal = 1;
                    Name = "Hidden Treasure";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                        if (lightWorld >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Items.Has(ItemType.Shovel) &&
                               (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)))
                                return (Available, lightWorld);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.OverworldEvent, 42, 64);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.OverworldEventMemory[42].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.Shovel, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.PyramidLedge:

                    _baseTotal = 1;
                    Name = "Ledge";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel dWEast = _game.Regions[RegionID.DarkWorldEast].Accessibility;

                        if (dWEast >= AccessibilityLevel.SequenceBreak)
                            return (Available, dWEast);

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.OverworldEvent, 91, 64);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.OverworldEventMemory[91].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.DarkWorldEast, new Mode());

                    break;
                case LocationID.FatFairy:

                    _baseTotal = 2;
                    Name = "Big Bomb Spot";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                        if (lightWorld >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Mode.WorldState != WorldState.Inverted)
                            {
                                if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.RedCrystal, 2))
                                {
                                    if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Gloves))
                                        return (Available, lightWorld);

                                    if (_game.Items.Has(ItemType.Aga))
                                    {
                                        if (_game.Items.Has(ItemType.Hammer))
                                            return (Available, lightWorld);

                                        if (_game.Items.Has(ItemType.Mirror))
                                        {
                                            if (_game.Items.Has(ItemType.Gloves, 2))
                                                return (Available, lightWorld);

                                            if (_game.Items.Has(ItemType.Hookshot) &&
                                                (_game.Items.Has(ItemType.Flippers) || _game.Items.Has(ItemType.Gloves)))
                                                return (Available, lightWorld);
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.Gloves, 2))
                                        return (Available, AccessibilityLevel.SequenceBreak);
                                }
                            }
                            else
                            {
                                if (_game.Items.Has(ItemType.RedCrystal, 2) && _game.Items.Has(ItemType.Mirror))
                                    return (Available, lightWorld);
                            }
                        }

                        return (0, AccessibilityLevel.None);
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

                    _game.AutoTracker.RoomMemory[556].PropertyChanged += OnMemoryChanged;

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
                        AccessibilityLevel dWSouth = _game.Regions[RegionID.DarkWorldSouth].Accessibility;

                        if (dWSouth >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Mode.WorldState == WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                                return (Available, dWSouth);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 0, 8);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.NPCItemMemory[0].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.DarkWorldSouth, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.HypeCave:

                    _baseTotal = 5;
                    Name = "Cave";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel dWSouth = _game.Regions[RegionID.DarkWorldSouth].Accessibility;

                        if (dWSouth >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Mode.WorldState == WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                                return (Available, dWSouth);
                        }

                        return (0, AccessibilityLevel.None);
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

                    _game.AutoTracker.RoomMemory[572].PropertyChanged += OnMemoryChanged;
                    _game.AutoTracker.RoomMemory[573].PropertyChanged += OnMemoryChanged;

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
                            AccessibilityLevel dWSouth = _game.Regions[RegionID.DarkWorldSouth].Accessibility;

                            if (dWSouth >= AccessibilityLevel.SequenceBreak)
                            {
                                if (_game.Items.Has(ItemType.Book) && _game.Items.Has(ItemType.Mirror))
                                {
                                    if (_game.Items.CanActivateTablets())
                                        return (Available, dWSouth);

                                    return (0, AccessibilityLevel.Inspect);
                                }
                            }
                        }
                        else
                        {
                            AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                            if (lightWorld >= AccessibilityLevel.SequenceBreak)
                            {
                                if (_game.Items.Has(ItemType.Book))
                                {
                                    if (_game.Items.CanActivateTablets())
                                        return (Available, lightWorld);

                                    return (0, AccessibilityLevel.Inspect);
                                }
                            }
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 1, 2);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.NPCItemMemory[1].PropertyChanged += OnMemoryChanged;

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
                            AccessibilityLevel dWSouth = _game.Regions[RegionID.DarkWorldSouth].Accessibility;

                            if (dWSouth >= AccessibilityLevel.SequenceBreak)
                            {
                                if (_game.Items.Has(ItemType.Mirror))
                                    return (Available, dWSouth);
                            }
                        }
                        else
                        {
                            AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                            if (lightWorld >= AccessibilityLevel.SequenceBreak)
                            {
                                if (_game.Items.Has(ItemType.MoonPearl))
                                    return (Available, lightWorld);
                            }
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 567, 4);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.RoomMemory[567].PropertyChanged += OnMemoryChanged;

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
                        AccessibilityLevel dWSouth = _game.Regions[RegionID.DarkWorldSouth].Accessibility;

                        if (dWSouth >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Mode.WorldState == WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                                return (Available, dWSouth);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.OverworldEvent, 104, 64);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.OverworldEventMemory[104].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.DarkWorldSouth, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.WitchsHut:

                    _baseTotal = 1;
                    Name = "Assistant";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                        if (lightWorld >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Items.Has(ItemType.Mushroom) &&
                                (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)))
                                return (Available, lightWorld);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 1, 32);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.NPCItemMemory[1].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.Mushroom, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.WaterfallFairy:

                    _baseTotal = 2;
                    Name = "Waterfall Cave";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                        if (lightWorld >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                            {
                                if (_game.Items.Has(ItemType.Flippers))
                                    return (Available, lightWorld);

                                if (_game.Items.Has(ItemType.MoonPearl) || _game.Items.Has(ItemType.Boots))
                                    return (Available, AccessibilityLevel.SequenceBreak);
                            }
                        }

                        return (0, AccessibilityLevel.None);
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

                    _game.AutoTracker.RoomMemory[552].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.Boots, new Mode());
                    _itemSubscriptions.Add(ItemType.Flippers, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode());

                    break;
                case LocationID.ZoraArea when index == 0:

                    _baseTotal = 1;
                    Name = "King Zora";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                        if (lightWorld >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                            {
                                if (_game.Items.Has(ItemType.Gloves) || _game.Items.Has(ItemType.Flippers))
                                    return (Available, lightWorld);

                                return (Available, AccessibilityLevel.SequenceBreak);
                            }
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 0, 2);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.NPCItemMemory[0].PropertyChanged += OnMemoryChanged;

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
                        AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                        if (lightWorld >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                            {
                                if (_game.Items.Has(ItemType.Flippers))
                                    return (Available, lightWorld);

                                return (Available, AccessibilityLevel.SequenceBreak);
                            }
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.OverworldEvent, 129, 64);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.OverworldEventMemory[129].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.Flippers, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.Catfish:

                    _baseTotal = 1;
                    Name = "Ring of Stones";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel dWEast = _game.Regions[RegionID.DarkWorldEast].Accessibility;

                        if (dWEast >= AccessibilityLevel.SequenceBreak)
                        {
                            if ((_game.Mode.WorldState == WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)) &&
                                _game.Items.Has(ItemType.Gloves))
                                return (Available, dWEast);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 0, 32);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.NPCItemMemory[0].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.DarkWorldEast, new Mode());

                    _itemSubscriptions.Add(ItemType.Gloves, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.SahasrahlasHut when index == 0:

                    _baseTotal = 3;
                    Name = "Back Room";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                        if (lightWorld >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                                return (Available, lightWorld);
                        }

                        return (0, AccessibilityLevel.None);
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

                    _game.AutoTracker.RoomMemory[522].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.SahasrahlasHut:

                    _baseTotal = 1;
                    Name = "Saha";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                        if (lightWorld >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Items.Has(ItemType.GreenPendant))
                                return (Available, lightWorld);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 0, 16);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.NPCItemMemory[0].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.GreenPendant, new Mode());

                    break;
                case LocationID.BonkRocks:

                    _baseTotal = 1;
                    Name = "Cave";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                        if (lightWorld >= AccessibilityLevel.SequenceBreak)
                        {
                            if ((_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)) &&
                                _game.Items.Has(ItemType.Boots))
                                return (Available, lightWorld);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 584, 16);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.RoomMemory[584].PropertyChanged += OnMemoryChanged;

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
                                AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;
                                AccessibilityLevel dWWest = _game.Regions[RegionID.DarkWorldWest].Accessibility;

                                AccessibilityLevel direct = AccessibilityLevel.None;
                                AccessibilityLevel mirror = AccessibilityLevel.None;

                                if (lightWorld >= AccessibilityLevel.SequenceBreak && _game.Items.Has(ItemType.Gloves, 2))
                                    direct = lightWorld;

                                if (dWWest >= AccessibilityLevel.SequenceBreak && _game.Items.Has(ItemType.Mirror) &&
                                    _game.Items.Has(ItemType.MoonPearl))
                                    mirror = dWWest;

                                AccessibilityLevel access = (AccessibilityLevel)Math.Max((byte)direct, (byte)mirror);

                                if (access >= AccessibilityLevel.SequenceBreak)
                                    return (Available, access);
                            }
                        }
                        else
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Boots) &&
                                _game.Items.Has(ItemType.Gloves, 2))
                                return (Available, AccessibilityLevel.Normal);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 550, 16);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.RoomMemory[550].PropertyChanged += OnMemoryChanged;

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
                            AccessibilityLevel dWWest = _game.Regions[RegionID.DarkWorldWest].Accessibility;

                            if (dWWest >= AccessibilityLevel.SequenceBreak && _game.Items.Has(ItemType.Mirror) &&
                                _game.Items.Has(ItemType.MoonPearl))
                                return (Available, dWWest);
                        }
                        else
                        {
                            AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                            if (lightWorld >= AccessibilityLevel.SequenceBreak && _game.Items.Has(ItemType.MoonPearl))
                                return (Available, lightWorld);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 567, 2);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.RoomMemory[567].PropertyChanged += OnMemoryChanged;

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
                            return (Available, AccessibilityLevel.Normal);

                        AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.DesertBackAccess) && _game.Items.Has(ItemType.Gloves))
                                return (Available, AccessibilityLevel.Normal);

                            AccessibilityLevel mireArea = _game.Regions[RegionID.MireArea].Accessibility;

                            AccessibilityLevel direct = AccessibilityLevel.None;
                            AccessibilityLevel mirror = AccessibilityLevel.None;

                            if (lightWorld >= AccessibilityLevel.SequenceBreak)
                            {
                                if (!_game.Mode.EntranceShuffle.Value && _game.Items.Has(ItemType.Book))
                                    direct = lightWorld;
                                else
                                    direct = AccessibilityLevel.Inspect;
                            }

                            if (mireArea >= AccessibilityLevel.SequenceBreak && _game.Items.Has(ItemType.Mirror))
                                mirror = mireArea;

                            AccessibilityLevel access = (AccessibilityLevel)Math.Max((byte)direct, (byte)mirror);

                            if (access >= AccessibilityLevel.SequenceBreak)
                                return (Available, access);
                            else
                                return (0, access);
                        }
                        else
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                            {
                                if (_game.Items.Has(ItemType.DesertBackAccess) && _game.Items.Has(ItemType.Gloves))
                                    return (Available, AccessibilityLevel.Normal);

                                if (lightWorld >= AccessibilityLevel.SequenceBreak)
                                {
                                    if (!_game.Mode.EntranceShuffle.Value && _game.Items.Has(ItemType.Book))
                                        return (Available, lightWorld);

                                    return (0, AccessibilityLevel.Inspect);
                                }
                            }
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.OverworldEvent, 48, 64);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.OverworldEventMemory[48].PropertyChanged += OnMemoryChanged;

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
                        AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                        if (lightWorld >= AccessibilityLevel.SequenceBreak &&
                            (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)))
                            return (Available, lightWorld);

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 532, 16);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.RoomMemory[532].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.CShapedHouse:

                    _baseTotal = 1;
                    Name = "House";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel dWWest = _game.Regions[RegionID.DarkWorldWest].Accessibility;

                        if (dWWest >= AccessibilityLevel.SequenceBreak &&
                            (_game.Mode.WorldState == WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)))
                            return (Available, dWWest);

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 568, 16);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.RoomMemory[568].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.DarkWorldWest, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.TreasureGame:

                    _baseTotal = 1;
                    Name = "Prize";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel dWWest = _game.Regions[RegionID.DarkWorldWest].Accessibility;

                        if (dWWest >= AccessibilityLevel.SequenceBreak &&
                            (_game.Mode.WorldState == WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)))
                            return (Available, dWWest);

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 525, 4);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.RoomMemory[525].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.DarkWorldWest, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.BombableShack:

                    _baseTotal = 1;
                    Name = "Downstairs";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel dWWest = _game.Regions[RegionID.DarkWorldWest].Accessibility;

                        if (dWWest >= AccessibilityLevel.SequenceBreak &&
                            (_game.Mode.WorldState == WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)))
                            return (Available, dWWest);

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 524, 16);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.RoomMemory[524].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.DarkWorldWest, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.Blacksmith:

                    _baseTotal = 1;
                    Name = "Bring Frog Home";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel dWWest = _game.Regions[RegionID.DarkWorldWest].Accessibility;

                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (dWWest >= AccessibilityLevel.SequenceBreak &&
                                _game.Items.Has(ItemType.Gloves, 2) && _game.Items.Has(ItemType.MoonPearl))
                            {
                                if (_game.Mode.ItemPlacement == ItemPlacement.Advanced || _game.Items.Has(ItemType.Mirror))
                                    return (Available, dWWest);
                                else
                                    return (Available, AccessibilityLevel.SequenceBreak);
                            }
                        }
                        else
                        {
                            AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                            AccessibilityLevel house = AccessibilityLevel.None;
                            AccessibilityLevel jeremiah = AccessibilityLevel.None;

                            if (lightWorld >= AccessibilityLevel.SequenceBreak && _game.Items.Has(ItemType.Mirror))
                                house = lightWorld;

                            if (dWWest >= AccessibilityLevel.SequenceBreak && _game.Items.Has(ItemType.Gloves, 2))
                                jeremiah = dWWest;

                            AccessibilityLevel access = (AccessibilityLevel)Math.Min((byte)lightWorld, (byte)dWWest);

                            if (access >= AccessibilityLevel.SequenceBreak)
                                return (Available, access);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 1, 4);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.NPCItemMemory[1].PropertyChanged += OnMemoryChanged;

                    _updateOnItemPlacementChange = true;

                    _regionSubscriptions.Add(RegionID.DarkWorldWest, new Mode());
                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.Gloves, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode());

                    break;
                case LocationID.PurpleChest:

                    _baseTotal = 1;
                    Name = "Gary";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel dWWest = _game.Regions[RegionID.DarkWorldWest].Accessibility;

                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (dWWest >= AccessibilityLevel.SequenceBreak &&
                                _game.Items.Has(ItemType.Gloves, 2) && _game.Items.Has(ItemType.MoonPearl))
                                return (Available, dWWest);
                        }
                        else
                        {
                            AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                            AccessibilityLevel gary = AccessibilityLevel.None;
                            AccessibilityLevel chestDirect = AccessibilityLevel.None;
                            AccessibilityLevel chestMirror = AccessibilityLevel.None;

                            if (lightWorld >= AccessibilityLevel.SequenceBreak)
                            {
                                if (_game.Items.Has(ItemType.Mirror))
                                    chestMirror = lightWorld;
                                
                                gary = lightWorld;
                            }

                            if (dWWest >= AccessibilityLevel.SequenceBreak && _game.Items.Has(ItemType.Gloves, 2))
                                chestDirect = dWWest;

                            AccessibilityLevel chest = (AccessibilityLevel)Math.Max((byte)chestDirect, (byte)chestMirror);
                            AccessibilityLevel access = (AccessibilityLevel)Math.Min((byte)chest, (byte)gary);

                            if (access >= AccessibilityLevel.SequenceBreak)
                                return (Available, access);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 137, 16);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.ItemMemory[137].PropertyChanged += OnMemoryChanged;

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
                                return (Available, AccessibilityLevel.Normal);
                        }
                        else
                        {
                            if (_game.Items.Has(ItemType.Hammer))
                            {
                                AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;
                                AccessibilityLevel dWWest = _game.Regions[RegionID.DarkWorldWest].Accessibility;

                                AccessibilityLevel direct = AccessibilityLevel.None;
                                AccessibilityLevel mirror = AccessibilityLevel.None;

                                if (dWWest >= AccessibilityLevel.SequenceBreak && _game.Items.Has(ItemType.Gloves, 2))
                                    direct = dWWest;

                                if (lightWorld >= AccessibilityLevel.SequenceBreak && _game.Items.Has(ItemType.Mirror))
                                    mirror = lightWorld;

                                AccessibilityLevel access = (AccessibilityLevel)Math.Max((byte)direct, (byte)mirror);

                                if (access >= AccessibilityLevel.SequenceBreak)
                                    return (Available, access);
                            }
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 591, 4);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.RoomMemory[591].PropertyChanged += OnMemoryChanged;

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
                            return (Available, AccessibilityLevel.Normal);

                        AccessibilityLevel dWWest = _game.Regions[RegionID.DarkWorldWest].Accessibility;

                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (dWWest >= AccessibilityLevel.SequenceBreak)
                            {
                                if (!_game.Mode.EntranceShuffle.Value && _game.Items.Has(ItemType.Gloves) &&
                                    _game.Items.Has(ItemType.Cape) && _game.Items.Has(ItemType.MoonPearl))
                                {
                                    if (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                        _game.Items.Has(ItemType.Hookshot))
                                        return (Available, dWWest);
                                    else
                                        return (Available, AccessibilityLevel.SequenceBreak);
                                }

                                return (0, AccessibilityLevel.Inspect);
                            }
                        }
                        else
                        {
                            AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                            if (_game.Items.Has(ItemType.Mirror))
                            {
                                if (_game.Items.Has(ItemType.DeathMountainExitAccess))
                                    return (Available, AccessibilityLevel.Normal);

                                if (lightWorld >= AccessibilityLevel.SequenceBreak &&
                                    !_game.Mode.EntranceShuffle.Value && _game.Items.Has(ItemType.MoonPearl) &&
                                    _game.Items.Has(ItemType.Cape) && _game.Items.Has(ItemType.Gloves))
                                    return (Available, lightWorld);
                            }

                            if (dWWest >= AccessibilityLevel.SequenceBreak)
                                return (0, AccessibilityLevel.Inspect);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.OverworldEvent, 74, 64);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.OverworldEventMemory[74].PropertyChanged += OnMemoryChanged;

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
                        AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                        if (lightWorld >= AccessibilityLevel.SequenceBreak &&
                            (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)))
                            return (Available, lightWorld);

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 534, 16);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.RoomMemory[534].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.Dam:

                    _baseTotal = 1;
                    Name = "Outside";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                        if (lightWorld >= AccessibilityLevel.SequenceBreak && (_game.Mode.WorldState != WorldState.Inverted ||
                            _game.Mode.EntranceShuffle.Value || _game.Items.Has(ItemType.MoonPearl)))
                            return (Available, lightWorld);

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.OverworldEvent, 59, 64);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.OverworldEventMemory[59].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.MiniMoldormCave:

                    _baseTotal = 5;
                    Name = "Cave";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                        if (lightWorld >= AccessibilityLevel.SequenceBreak &&
                            (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)))
                            return (Available, lightWorld);

                        return (0, AccessibilityLevel.None);
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

                    _game.AutoTracker.RoomMemory[582].PropertyChanged += OnMemoryChanged;
                    _game.AutoTracker.RoomMemory[583].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.IceRodCave:

                    _baseTotal = 1;
                    Name = "Cave";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                        if (lightWorld >= AccessibilityLevel.SequenceBreak &&
                            (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)))
                            return (Available, lightWorld);

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 576, 16);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.RoomMemory[576].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.LakeHyliaIsland:

                    _baseTotal = 1;
                    Name = "Island";
                    HasMarking = true;

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Mirror))
                            {
                                AccessibilityLevel dWEast = _game.Regions[RegionID.DarkWorldEast].Accessibility;
                                AccessibilityLevel dWSouth = _game.Regions[RegionID.DarkWorldSouth].Accessibility;
                                AccessibilityLevel dWSouthEast = _game.Regions[RegionID.DarkWorldSouthEast].Accessibility;

                                if ((dWEast == AccessibilityLevel.Normal || dWSouth >= AccessibilityLevel.Normal ||
                                    dWSouthEast >= AccessibilityLevel.Normal) && _game.Items.Has(ItemType.Flippers))
                                    return (Available, AccessibilityLevel.Normal);

                                if (dWEast >= AccessibilityLevel.SequenceBreak || dWSouth >= AccessibilityLevel.SequenceBreak ||
                                    dWSouthEast >= AccessibilityLevel.SequenceBreak)
                                    return (Available, AccessibilityLevel.SequenceBreak);
                            }

                            if (lightWorld >= AccessibilityLevel.SequenceBreak)
                                return (0, AccessibilityLevel.Inspect);
                        }
                        else
                        {
                            if (lightWorld >= AccessibilityLevel.SequenceBreak && _game.Items.Has(ItemType.MoonPearl))
                            {
                                if (_game.Items.Has(ItemType.Flippers))
                                    return (Available, lightWorld);

                                return (0, AccessibilityLevel.Inspect);
                            }   
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.OverworldEvent, 53, 64);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.OverworldEventMemory[53].PropertyChanged += OnMemoryChanged;

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
                        AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                        if (lightWorld >= AccessibilityLevel.SequenceBreak &&
                            (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)))
                        {
                            if (_game.Items.Has(ItemType.Flippers))
                                return (Available, lightWorld);

                            return (Available, AccessibilityLevel.SequenceBreak);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 137, 1);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.ItemMemory[137].PropertyChanged += OnMemoryChanged;

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

                        if (mireArea >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Mode.WorldState == WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                                return (Available, mireArea);
                            
                            if (_game.Items.Has(ItemType.Mirror))
                                return (Available, AccessibilityLevel.SequenceBreak);
                        }

                        return (0, AccessibilityLevel.None);
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

                    _game.AutoTracker.RoomMemory[538].PropertyChanged += OnMemoryChanged;

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
                            AccessibilityLevel mireArea = _game.Regions[RegionID.MireArea].Accessibility;

                            if (mireArea >= AccessibilityLevel.SequenceBreak &&
                                _game.Items.Has(ItemType.Mirror) && _game.Items.Has(ItemType.Gloves))
                                return (Available, mireArea);
                        }
                        else
                        {
                            AccessibilityLevel lightWorld = _game.Regions[RegionID.LightWorld].Accessibility;

                            if (lightWorld >= AccessibilityLevel.SequenceBreak && _game.Items.Has(ItemType.Gloves))
                                return (Available, lightWorld);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 589, 2);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.RoomMemory[589].PropertyChanged += OnMemoryChanged;

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

                        if (dMWestBottom >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Items.Has(ItemType.Lamp))
                                return (Available, dMWestBottom);

                            return (Available, AccessibilityLevel.SequenceBreak);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 0, 1);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.NPCItemMemory[0].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.DeathMountainWestBottom, new Mode());

                    _itemSubscriptions.Add(ItemType.Lamp, new Mode());

                    break;
                case LocationID.SpectacleRock when index == 0:

                    _baseTotal = 1;
                    Name = "Cave";

                    RequiredMode = new Mode() { EntranceShuffle = false };

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel dMWestBottom = _game.Regions[RegionID.DeathMountainWestBottom].Accessibility;

                        if (dMWestBottom >= AccessibilityLevel.SequenceBreak)
                            return (Available, dMWestBottom);

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 469, 4);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.RoomMemory[469].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.DeathMountainWestBottom, new Mode());

                    break;
                case LocationID.SpectacleRock:

                    _baseTotal = 1;
                    Name = "Top";
                    HasMarking = true;

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel dMWestBottom = _game.Regions[RegionID.DeathMountainWestBottom].Accessibility;

                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            if (dMWestBottom >= AccessibilityLevel.SequenceBreak)
                            {
                                if (_game.Items.Has(ItemType.Mirror))
                                    return (Available, dMWestBottom);

                                return (0, AccessibilityLevel.Inspect);
                            }
                        }
                        else
                        {
                            AccessibilityLevel dMWestTop = _game.Regions[RegionID.DeathMountainWestTop].Accessibility;

                            if (dMWestBottom >= AccessibilityLevel.SequenceBreak)
                                return (Available, dMWestBottom);

                            if (dMWestTop >= AccessibilityLevel.SequenceBreak)
                                return (0, AccessibilityLevel.Inspect);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.OverworldEvent, 3, 64);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.OverworldEventMemory[3].PropertyChanged += OnMemoryChanged;

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
                        AccessibilityLevel dMWestTop = _game.Regions[RegionID.DeathMountainWestTop].Accessibility;

                        if (dMWestTop >= AccessibilityLevel.SequenceBreak && _game.Items.Has(ItemType.Book))
                        {
                            if (_game.Items.CanActivateTablets())
                                return (Available, dMWestTop);
                            
                            return (0, AccessibilityLevel.Inspect);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 1, 1);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.NPCItemMemory[1].PropertyChanged += OnMemoryChanged;

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
                        AccessibilityLevel dDMWestBottom = _game.Regions[RegionID.DarkDeathMountainWestBottom].Accessibility;

                        if (dDMWestBottom >= AccessibilityLevel.SequenceBreak && _game.Items.Has(ItemType.Gloves) &&
                            _game.Items.Has(ItemType.Hammer) &&
                            (_game.Mode.WorldState == WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)))
                        {
                            if (_game.Items.Has(ItemType.CaneOfByrna) || (_game.Items.Has(ItemType.Cape) &&
                                (_game.Items.Has(ItemType.HalfMagic) || _game.Items.Has(ItemType.Bottle))))
                                return (Available, dDMWestBottom);

                            return (Available, AccessibilityLevel.SequenceBreak);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 558, 16);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.RoomMemory[558].PropertyChanged += OnMemoryChanged;

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
                        AccessibilityLevel dMEastTop = _game.Regions[RegionID.DeathMountainEastTop].Accessibility;

                        if (dMEastTop >= AccessibilityLevel.SequenceBreak &&
                            (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)))
                            return (Available, dMEastTop);

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 508, 16);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.RoomMemory[508].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.DeathMountainEastTop, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.ParadoxCave when index == 0:

                    _baseTotal = 2;
                    Name = "Bottom";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel dMEastTop = _game.Regions[RegionID.DeathMountainEastTop].Accessibility;

                        if (dMEastTop >= AccessibilityLevel.SequenceBreak &&
                            (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)))
                            return (Available, dMEastTop);

                        return (0, AccessibilityLevel.None);
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

                    _game.AutoTracker.RoomMemory[510].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.DeathMountainEastTop, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.ParadoxCave:

                    _baseTotal = 5;
                    Name = "Top";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel dMEastTop = _game.Regions[RegionID.DeathMountainEastTop].Accessibility;

                        if (dMEastTop >= AccessibilityLevel.SequenceBreak && 
                            (_game.Mode.WorldState != WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)))
                            return (Available, dMEastTop);

                        return (0, AccessibilityLevel.None);
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

                    _game.AutoTracker.RoomMemory[478].PropertyChanged += OnMemoryChanged;
                    _game.AutoTracker.RoomMemory[479].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.DeathMountainEastTop, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case LocationID.SuperBunnyCave:

                    _baseTotal = 2;
                    Name = "Cave";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel dDMTop = _game.Regions[RegionID.DarkDeathMountainTop].Accessibility;

                        if (dDMTop >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Mode.WorldState == WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl))
                                return (Available, dDMTop);

                            return (Available, AccessibilityLevel.SequenceBreak);
                        }

                        return (0, AccessibilityLevel.None);
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

                    _game.AutoTracker.RoomMemory[496].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.DarkDeathMountainTop, new Mode());

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case LocationID.HookshotCave when index == 0:

                    _baseTotal = 1;
                    Name = "Bonkable Chest";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel dDMTop = _game.Regions[RegionID.DarkDeathMountainTop].Accessibility;

                        if (dDMTop >= AccessibilityLevel.SequenceBreak && _game.Items.Has(ItemType.Gloves) &&
                            (_game.Mode.WorldState == WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)) &&
                            (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots)))
                        {
                            if (_game.Mode.ItemPlacement == ItemPlacement.Advanced || _game.Items.Has(ItemType.Hookshot))
                                return (Available, dDMTop);

                            return (Available, AccessibilityLevel.SequenceBreak);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 120, 128);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.RoomMemory[120].PropertyChanged += OnMemoryChanged;

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
                        AccessibilityLevel dDMTop = _game.Regions[RegionID.DarkDeathMountainTop].Accessibility;

                        if (dDMTop >= AccessibilityLevel.SequenceBreak && _game.Items.Has(ItemType.Gloves) &&
                            _game.Items.Has(ItemType.Hookshot) &&
                            (_game.Mode.WorldState == WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)))
                            return (Available, dDMTop);

                        return (0, AccessibilityLevel.None);
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

                    _game.AutoTracker.RoomMemory[120].PropertyChanged += OnMemoryChanged;

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
                        AccessibilityLevel dMEastTop = _game.Regions[RegionID.DeathMountainEastTop].Accessibility;

                        if (_game.Mode.WorldState != WorldState.Inverted)
                        {
                            AccessibilityLevel dDMTop = _game.Regions[RegionID.DarkDeathMountainTop].Accessibility;

                            if (_game.Items.Has(ItemType.Mirror))
                            {
                                if (_game.Items.Has(ItemType.DarkDeathMountainFloatingIslandAccess))
                                    return (Available, AccessibilityLevel.Normal);

                                if (dDMTop >= AccessibilityLevel.SequenceBreak && !_game.Mode.EntranceShuffle.Value &&
                                    _game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.MoonPearl))
                                    return (Available, dDMTop);
                            }

                            if (dMEastTop >= AccessibilityLevel.SequenceBreak)
                                return (0, AccessibilityLevel.Inspect);
                        }
                        else
                        {
                            if (dMEastTop >= AccessibilityLevel.SequenceBreak)
                                return (Available, dMEastTop);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.OverworldEvent, 5, 64);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.OverworldEventMemory[5].PropertyChanged += OnMemoryChanged;

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
                            AccessibilityLevel tRFront = _game.Regions[RegionID.TurtleRockFront].Accessibility;

                            if (tRFront >= AccessibilityLevel.SequenceBreak && _game.Items.Has(ItemType.Mirror) &&
                                _game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.CaneOfSomaria))
                            {
                                if (_game.Mode.SmallKeyShuffle)
                                {
                                    if (_game.Items.Has(ItemType.TRSmallKey, 2))
                                        return (Available, tRFront);
                                }
                                else
                                {
                                    if (_game.Items.Has(ItemType.FireRod))
                                        return (Available, tRFront);

                                    return (Available, AccessibilityLevel.SequenceBreak);
                                }
                            }
                        }
                        else
                        {
                            AccessibilityLevel dMEastTop = _game.Regions[RegionID.DeathMountainEastTop].Accessibility;

                            if (dMEastTop >= AccessibilityLevel.SequenceBreak && _game.Items.Has(ItemType.MoonPearl) &&
                                _game.Items.Has(ItemType.Hammer))
                                return (Available, dMEastTop);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 536, 16);

                        if (result.HasValue)
                            Available = result.Value ? 0 : 1;
                    };

                    _game.AutoTracker.RoomMemory[536].PropertyChanged += OnMemoryChanged;

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
                        AccessibilityLevel hC = _game.Regions[RegionID.HyruleCastle].Accessibility;

                        if (hC >= AccessibilityLevel.SequenceBreak)
                        {
                            int inaccessible = 0;
                            bool sequenceBreak = true;

                            if (_game.Mode.SmallKeyShuffle)
                            {
                                if (_game.Items.Has(ItemType.Gloves) || _game.Items.Has(ItemType.HCSmallKey))
                                {
                                    if (_game.Items.Has(ItemType.Lamp) ||
                                        (_game.Mode.ItemPlacement == ItemPlacement.Advanced && _game.Items.Has(ItemType.FireRod)))
                                        sequenceBreak = false;
                                }
                                else
                                    inaccessible = 3;
                            }
                            else
                            {
                                if (_game.Items.Has(ItemType.Gloves) && (_game.Items.Has(ItemType.Lamp) ||
                                    (_game.Mode.ItemPlacement == ItemPlacement.Advanced && _game.Items.Has(ItemType.FireRod))))
                                    sequenceBreak = false;
                            }

                            if (!_game.Mode.MapCompassShuffle)
                                inaccessible = Math.Max(0, inaccessible - _mapCompass);

                            if (inaccessible == 0)
                            {
                                if (!sequenceBreak)
                                    return (Available, hC);

                                return (Available, AccessibilityLevel.SequenceBreak);
                            }

                            if (Available > inaccessible)
                                return (Available - inaccessible, AccessibilityLevel.Partial);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    _updateOnItemPlacementChange = true;
                    _updateOnDungeonItemShuffleChange = true;

                    _regionSubscriptions.Add(RegionID.HyruleCastle, new Mode());

                    _itemSubscriptions.Add(ItemType.Gloves, new Mode());
                    _itemSubscriptions.Add(ItemType.HCSmallKey, new Mode());
                    _itemSubscriptions.Add(ItemType.SmallKey, new Mode() { WorldState = WorldState.Retro });
                    _itemSubscriptions.Add(ItemType.Lamp, new Mode());
                    _itemSubscriptions.Add(ItemType.FireRod, new Mode() { ItemPlacement = ItemPlacement.Advanced });

                    break;
                case LocationID.AgahnimTower:

                    _baseTotal = 0;
                    _smallKey = 2;
                    Name = "Dungeon";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel aT = _game.Regions[RegionID.Agahnim].Accessibility;

                        if (aT >= AccessibilityLevel.SequenceBreak)
                        {
                            int inaccessible = 0;
                            bool sequenceBreak = true;

                            if (_game.Items.Has(ItemType.ATSmallKey))
                            {
                                if (_game.Items.Has(ItemType.Lamp))
                                    sequenceBreak = false;
                            }
                            else
                                inaccessible = 1;

                            if (inaccessible == 0)
                            {
                                if (!sequenceBreak)
                                    return (Available, aT);

                                return (Available, AccessibilityLevel.SequenceBreak);
                            }

                            if (Available > inaccessible)
                                return (Available - inaccessible, AccessibilityLevel.Partial);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    _regionSubscriptions.Add(RegionID.Agahnim, new Mode());

                    _itemSubscriptions.Add(ItemType.Lamp, new Mode());
                    _itemSubscriptions.Add(ItemType.ATSmallKey, new Mode());
                    _itemSubscriptions.Add(ItemType.SmallKey, new Mode() { WorldState = WorldState.Retro });

                    break;
                case LocationID.EasternPalace:

                    _mapCompass = 2;
                    _bigKey = 1;
                    _baseTotal = 3;
                    Name = "Dungeon";

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel eP = _game.Regions[RegionID.EasternPalace].Accessibility;

                        if (eP >= AccessibilityLevel.SequenceBreak)
                        {
                            int inaccessible = 6;
                            bool sequenceBreak = true;

                            if (_game.Mode.BigKeyShuffle)
                            {
                                inaccessible = 2;

                                if (_game.Items.Has(ItemType.EPBigKey))
                                {
                                    inaccessible = 1;

                                    if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                    {
                                        inaccessible = 0;

                                        if (_game.Items.Has(ItemType.Lamp))
                                            sequenceBreak = false;
                                    }
                                }
                            }
                            else
                            {
                                inaccessible = 1;

                                if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                {
                                    inaccessible = 0;

                                    if (_game.Items.Has(ItemType.Lamp))
                                        sequenceBreak = false;
                                }
                            }

                            if (!_game.Mode.MapCompassShuffle)
                                inaccessible = Math.Max(0, inaccessible - _mapCompass);

                            if (inaccessible == 0)
                            {
                                if (!sequenceBreak)
                                    return (Available, eP);

                                return (Available, AccessibilityLevel.SequenceBreak);
                            }

                            if (Available > inaccessible)
                                return (Available - inaccessible, AccessibilityLevel.Partial);
                        }

                        return (0, AccessibilityLevel.None);
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

                        if (dP >= AccessibilityLevel.SequenceBreak)
                        {
                            int inaccessible = 6;
                            bool sequenceBreak = true;

                            if (_game.Mode.BigKeyShuffle)
                            {
                                if (_game.Mode.SmallKeyShuffle)
                                {
                                    inaccessible = 4;

                                    if (_game.Items.Has(ItemType.DPBigKey))
                                        inaccessible = 3;

                                    if (_game.Items.Has(ItemType.Gloves) || _game.Mode.EntranceShuffle.Value)
                                    {
                                        inaccessible = 2;

                                        if (_game.Items.Has(ItemType.DPBigKey))
                                            inaccessible = 1;
                                    }

                                    if (_game.Items.Has(ItemType.DPSmallKey))
                                    {
                                        inaccessible = 2;

                                        if (_game.Items.Has(ItemType.DPBigKey))
                                        {
                                            inaccessible = 1;

                                            if ((_game.Mode.EntranceShuffle.Value || _game.Items.Has(ItemType.Gloves)) &&
                                                (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)))
                                            {
                                                inaccessible = 0;

                                                if (_game.Items.Has(ItemType.Boots))
                                                    sequenceBreak = false;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    inaccessible = 2;

                                    if (_game.Mode.EntranceShuffle.Value || _game.Items.Has(ItemType.Gloves))
                                        inaccessible = 1;

                                    if (_game.Items.Has(ItemType.DPBigKey))
                                    {
                                        inaccessible = 1;

                                        if ((_game.Mode.EntranceShuffle.Value || _game.Items.Has(ItemType.Gloves)) &&
                                            (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)))
                                        {
                                            inaccessible = 0;

                                            if (_game.Items.Has(ItemType.Boots))
                                                sequenceBreak = false;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (_game.Mode.SmallKeyShuffle)
                                {
                                    inaccessible = 3;

                                    if (_game.Mode.EntranceShuffle.Value || _game.Items.Has(ItemType.Gloves))
                                        inaccessible = 1;

                                    if (_game.Items.Has(ItemType.DPSmallKey))
                                    {
                                        inaccessible = 1;

                                        if ((_game.Items.Has(ItemType.Gloves) || _game.Mode.EntranceShuffle.Value) &&
                                            (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)))
                                        {
                                            inaccessible = 0;

                                            if (_game.Items.Has(ItemType.Boots))
                                                sequenceBreak = false;
                                        }
                                    }
                                }
                                else
                                {
                                    inaccessible = 1;

                                    if ((_game.Items.Has(ItemType.Gloves) || _game.Mode.EntranceShuffle.Value) &&
                                        (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)))
                                    {
                                        inaccessible = 0;

                                        if (_game.Items.Has(ItemType.Boots))
                                            sequenceBreak = false;
                                    }
                                }
                            }

                            if (!_game.Mode.MapCompassShuffle)
                                inaccessible = Math.Max(0, inaccessible - _mapCompass);

                            if (inaccessible == 0)
                            {
                                if (!sequenceBreak)
                                    return (Available, dP);

                                return (Available, AccessibilityLevel.SequenceBreak);
                            }

                            if (Available > inaccessible)
                                return (Available - inaccessible, AccessibilityLevel.Partial);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    _updateOnDungeonItemShuffleChange = true;

                    _regionSubscriptions.Add(RegionID.DesertPalace, new Mode());

                    _itemSubscriptions.Add(ItemType.Boots, new Mode());
                    _itemSubscriptions.Add(ItemType.Gloves, new Mode() { EntranceShuffle = false });
                    _itemSubscriptions.Add(ItemType.Lamp, new Mode());
                    _itemSubscriptions.Add(ItemType.FireRod, new Mode());
                    _itemSubscriptions.Add(ItemType.DPSmallKey, new Mode());
                    _itemSubscriptions.Add(ItemType.SmallKey, new Mode() { WorldState = WorldState.Retro });
                    _itemSubscriptions.Add(ItemType.DPBigKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity });

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

                        if (tH >= AccessibilityLevel.SequenceBreak)
                        {
                            int inaccessible = 6;
                            bool sequenceBreak = true;

                            if (_game.Mode.BigKeyShuffle)
                            {
                                if (_game.Mode.SmallKeyShuffle)
                                {
                                    inaccessible = 4;

                                    if (_game.Items.Has(ItemType.Hookshot))
                                        inaccessible = 2;

                                    if ((_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)) &&
                                        _game.Items.Has(ItemType.ToHSmallKey))
                                    {
                                        inaccessible = 3;

                                        if (_game.Items.Has(ItemType.Hookshot))
                                            inaccessible = 1;
                                    }

                                    if (_game.Items.Has(ItemType.ToHBigKey))
                                    {
                                        inaccessible = 1;

                                        if ((_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)) &&
                                            _game.Items.Has(ItemType.ToHSmallKey))
                                        {
                                            inaccessible = 0;
                                            sequenceBreak = false;
                                        }
                                    }
                                }
                                else
                                {
                                    inaccessible = 3;

                                    if (_game.Items.Has(ItemType.Hookshot))
                                        inaccessible = 1;

                                    if (_game.Items.Has(ItemType.ToHBigKey))
                                    {
                                        inaccessible = 0;

                                        if (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod))
                                            sequenceBreak = false;
                                    }
                                }
                            }
                            else
                            {
                                if (_game.Mode.SmallKeyShuffle)
                                {
                                    inaccessible = 1;

                                    if ((_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod)) &&
                                        _game.Items.Has(ItemType.ToHSmallKey))
                                    {
                                        inaccessible = 0;
                                        sequenceBreak = false;
                                    }
                                }
                                else
                                {
                                    if (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod))
                                    {
                                        inaccessible = 0;
                                        sequenceBreak = false;
                                    }
                                }
                            }

                            if (!_game.Mode.MapCompassShuffle)
                                inaccessible = Math.Max(0, inaccessible - _mapCompass);

                            if (inaccessible == 0)
                            {
                                if (!sequenceBreak)
                                    return (Available, tH);

                                return (Available, AccessibilityLevel.SequenceBreak);
                            }

                            if (Available > inaccessible)
                                return (Available - inaccessible, AccessibilityLevel.Partial);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    _updateOnDungeonItemShuffleChange = true;

                    _regionSubscriptions.Add(RegionID.TowerOfHera, new Mode());

                    _itemSubscriptions.Add(ItemType.Lamp, new Mode());
                    _itemSubscriptions.Add(ItemType.FireRod, new Mode());
                    _itemSubscriptions.Add(ItemType.ToHSmallKey, new Mode());
                    _itemSubscriptions.Add(ItemType.SmallKey, new Mode() { WorldState = WorldState.Retro });
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

                        if (pD >= AccessibilityLevel.SequenceBreak)
                        {
                            int inaccessible = 14;
                            bool sequenceBreak = true;

                            if (_game.Mode.BigKeyShuffle)
                            {
                                if (_game.Mode.SmallKeyShuffle)
                                {
                                    inaccessible = 13;

                                    if (_game.Items.Has(ItemType.PoDSmallKey))
                                        inaccessible = 11;

                                    if (_game.Items.Has(ItemType.PoDSmallKey, 2))
                                    {
                                        inaccessible = 6;

                                        if (_game.Items.Has(ItemType.PoDBigKey))
                                            inaccessible = 5;
                                    }

                                    if (_game.Items.Has(ItemType.PoDSmallKey, 3))
                                    {
                                        inaccessible = 5;

                                        if (_game.Items.Has(ItemType.PoDBigKey))
                                            inaccessible = 4;
                                    }

                                    if (_game.Items.Has(ItemType.PoDSmallKey, 4))
                                    {
                                        inaccessible = 4;

                                        if (_game.Items.Has(ItemType.PoDBigKey))
                                            inaccessible = 3;
                                    }

                                    if (_game.Items.CanClearRedEyegoreGoriyaRooms() || _game.Items.Has(ItemType.Bottle))
                                    {
                                        inaccessible = 11;

                                        if (_game.Items.Has(ItemType.PoDSmallKey))
                                            inaccessible = 9;

                                        if (_game.Items.Has(ItemType.PoDSmallKey, 2))
                                            inaccessible = 4;

                                        if (_game.Items.Has(ItemType.PoDSmallKey, 3))
                                            inaccessible = 3;

                                        if (_game.Items.Has(ItemType.PoDSmallKey, 4))
                                            inaccessible = 2;

                                        if (_game.Items.Has(ItemType.Hammer))
                                        {
                                            inaccessible = 9;

                                            if (_game.Items.Has(ItemType.PoDSmallKey))
                                                inaccessible = 4;

                                            if (_game.Items.Has(ItemType.PoDSmallKey, 2))
                                                inaccessible = 3;

                                            if (_game.Items.Has(ItemType.PoDSmallKey, 3))
                                                inaccessible = 2;
                                        }

                                        if (_game.Items.Has(ItemType.PoDBigKey))
                                        {
                                            if (_game.Items.Has(ItemType.PoDSmallKey, 2))
                                                inaccessible = 3;

                                            if (_game.Items.Has(ItemType.PoDSmallKey, 3))
                                                inaccessible = 2;

                                            if (_game.Items.Has(ItemType.PoDSmallKey, 4))
                                                inaccessible = 1;

                                            if (_game.Items.Has(ItemType.Hammer))
                                            {
                                                if (_game.Items.Has(ItemType.PoDSmallKey))
                                                    inaccessible = 3;

                                                if (_game.Items.Has(ItemType.PoDSmallKey, 2))
                                                    inaccessible = 2;

                                                if (_game.Items.Has(ItemType.PoDSmallKey, 3))
                                                    inaccessible = 1;
                                            }
                                        }
                                    }

                                    if (_game.Items.CanShootArrows() && _game.Items.Has(ItemType.PoDBigKey) &&
                                        _game.Items.Has(ItemType.Hammer))
                                    {
                                        if (_game.Items.Has(ItemType.PoDSmallKey, 4))
                                            inaccessible = 0;

                                        if (_game.Items.Has(ItemType.Lamp) && _game.Items.Has(ItemType.PoDSmallKey, 5))
                                            sequenceBreak = false;
                                    }
                                }
                                else
                                {
                                    inaccessible = 2;

                                    if (_game.Items.Has(ItemType.PoDBigKey))
                                    {
                                        inaccessible = 1;

                                        if (_game.Items.Has(ItemType.Hammer) && _game.Items.CanShootArrows())
                                        {
                                            inaccessible = 0;

                                            if (_game.Items.Has(ItemType.Lamp))
                                                sequenceBreak = false;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (_game.Mode.SmallKeyShuffle)
                                {
                                    inaccessible = 12;

                                    if (_game.Items.Has(ItemType.PoDSmallKey))
                                        inaccessible = 10;

                                    if (_game.Items.Has(ItemType.PoDSmallKey, 2))
                                        inaccessible = 5;

                                    if (_game.Items.Has(ItemType.PoDSmallKey, 3))
                                        inaccessible = 4;

                                    if (_game.Items.Has(ItemType.PoDSmallKey, 4))
                                        inaccessible = 3;

                                    if (_game.Items.CanClearRedEyegoreGoriyaRooms() || _game.Items.Has(ItemType.Bottle))
                                    {
                                        inaccessible = 10;

                                        if (_game.Items.Has(ItemType.PoDSmallKey))
                                            inaccessible = 8;

                                        if (_game.Items.Has(ItemType.PoDSmallKey, 2))
                                            inaccessible = 3;

                                        if (_game.Items.Has(ItemType.PoDSmallKey, 3))
                                            inaccessible = 2;

                                        if (_game.Items.Has(ItemType.PoDSmallKey, 4))
                                            inaccessible = 1;

                                        if (_game.Items.Has(ItemType.Hammer))
                                        {
                                            inaccessible = 8;

                                            if (_game.Items.Has(ItemType.PoDSmallKey))
                                                inaccessible = 3;

                                            if (_game.Items.Has(ItemType.PoDSmallKey, 2))
                                                inaccessible = 2;

                                            if (_game.Items.Has(ItemType.PoDSmallKey, 3))
                                                inaccessible = 1;
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.Hammer) && _game.Items.CanShootArrows())
                                    {
                                        if (_game.Items.Has(ItemType.PoDSmallKey, 4))
                                            inaccessible = 0;

                                        if (_game.Items.Has(ItemType.Lamp) && _game.Items.Has(ItemType.PoDSmallKey, 5))
                                            sequenceBreak = false;
                                    }
                                }
                                else
                                {
                                    inaccessible = 1;

                                    if (_game.Items.Has(ItemType.Hammer) && _game.Items.CanShootArrows())
                                    {
                                        inaccessible = 0;

                                        if (_game.Items.Has(ItemType.Lamp))
                                            sequenceBreak = false;
                                    }
                                }
                            }

                            if (!_game.Mode.MapCompassShuffle)
                                inaccessible = Math.Max(0, inaccessible - _mapCompass);

                            if (inaccessible == 0)
                            {
                                if (!sequenceBreak)
                                    return (Available, pD);

                                return (Available, AccessibilityLevel.SequenceBreak);
                            }

                            if (Available > inaccessible)
                                return (Available - inaccessible, AccessibilityLevel.Partial);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    _updateOnDungeonItemShuffleChange = true;
                    _updateOnEnemyShuffleChange = true;

                    _regionSubscriptions.Add(RegionID.PalaceOfDarkness, new Mode());

                    _itemSubscriptions.Add(ItemType.Lamp, new Mode());
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.Bow, new Mode());
                    _itemSubscriptions.Add(ItemType.Bottle, new Mode());
                    _itemSubscriptions.Add(ItemType.PoDSmallKey, new Mode());
                    _itemSubscriptions.Add(ItemType.SmallKey, new Mode() { WorldState = WorldState.Retro });
                    _itemSubscriptions.Add(ItemType.PoDBigKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity });

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

                        if (sP >= AccessibilityLevel.SequenceBreak)
                        {
                            int inaccessible = 10;
                            bool sequenceBreak = true;

                            if (_game.Mode.BigKeyShuffle)
                            {
                                if (_game.Mode.SmallKeyShuffle)
                                {
                                    if (_game.Items.Has(ItemType.Flippers))
                                    {
                                        inaccessible = 9;

                                        if (_game.Items.Has(ItemType.SPSmallKey))
                                        {
                                            inaccessible = 8;

                                            if (_game.Items.Has(ItemType.Hammer))
                                            {
                                                inaccessible = 5;

                                                if (_game.Items.Has(ItemType.SPBigKey))
                                                    inaccessible = 4;

                                                if (_game.Items.Has(ItemType.Hookshot))
                                                {
                                                    inaccessible = 1;

                                                    if (_game.Items.Has(ItemType.SPBigKey))
                                                    {
                                                        inaccessible = 0;
                                                        sequenceBreak = false;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (_game.Items.Has(ItemType.Flippers))
                                    {
                                        inaccessible = 8;

                                        if (_game.Items.Has(ItemType.Hammer))
                                        {
                                            inaccessible = 5;

                                            if (_game.Items.Has(ItemType.SPBigKey))
                                                inaccessible = 4;

                                            if (_game.Items.Has(ItemType.Hookshot))
                                            {
                                                inaccessible = 1;

                                                if (_game.Items.Has(ItemType.SPBigKey))
                                                {
                                                    inaccessible = 0;
                                                    sequenceBreak = false;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (_game.Mode.SmallKeyShuffle)
                                {
                                    if (_game.Items.Has(ItemType.Flippers))
                                    {
                                        inaccessible = 8;

                                        if (_game.Items.Has(ItemType.SPSmallKey))
                                        {
                                            inaccessible = 7;

                                            if (_game.Items.Has(ItemType.Hammer))
                                            {
                                                inaccessible = 4;

                                                if (_game.Items.Has(ItemType.Hookshot))
                                                {
                                                    inaccessible = 0;
                                                    sequenceBreak = false;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (_game.Items.Has(ItemType.Flippers))
                                    {
                                        inaccessible = 7;

                                        if (_game.Items.Has(ItemType.Hammer))
                                        {
                                            inaccessible = 4;

                                            if (_game.Items.Has(ItemType.Hookshot))
                                            {
                                                inaccessible = 0;
                                                sequenceBreak = false;
                                            }
                                        }
                                    }
                                }
                            }

                            if (!_game.Mode.MapCompassShuffle)
                                inaccessible = Math.Max(0, inaccessible - _mapCompass);

                            if (inaccessible == 0)
                            {
                                if (!sequenceBreak)
                                    return (Available, sP);

                                return (Available, AccessibilityLevel.SequenceBreak);
                            }

                            if (Available > inaccessible)
                                return (Available - inaccessible, AccessibilityLevel.Partial);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    _updateOnDungeonItemShuffleChange = true;

                    _regionSubscriptions.Add(RegionID.SwampPalace, new Mode());

                    _itemSubscriptions.Add(ItemType.Flippers, new Mode());
                    _itemSubscriptions.Add(ItemType.Hookshot, new Mode());
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.SPSmallKey, new Mode());
                    _itemSubscriptions.Add(ItemType.SmallKey, new Mode() { WorldState = WorldState.Retro });
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

                        if (sW >= AccessibilityLevel.SequenceBreak)
                        {
                            int inaccessible = 8;
                            bool sequenceBreak = true;

                            if (_game.Mode.BigKeyShuffle)
                            {
                                if (_game.Mode.SmallKeyShuffle)
                                {
                                    inaccessible = 3;

                                    if (_game.Items.Has(ItemType.SWBigKey))
                                        inaccessible = 2;

                                    if (_game.Items.Has(ItemType.FireRod) || _game.Mode.EntranceShuffle.Value)
                                    {
                                        inaccessible = 2;

                                        if (_game.Items.Has(ItemType.SWBigKey))
                                            inaccessible = 1;

                                        if (_game.Items.CanRemoveCurtains())
                                        {
                                            inaccessible = 1;

                                            if (_game.Items.Has(ItemType.SWBigKey))
                                            {
                                                inaccessible = 0;

                                                if (_game.Items.Has(ItemType.SWSmallKey))
                                                    sequenceBreak = false;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    inaccessible = 0;

                                    if ((_game.Items.Has(ItemType.FireRod) || _game.Mode.EntranceShuffle.Value) &&
                                        _game.Items.CanRemoveCurtains() && _game.Items.Has(ItemType.SWBigKey))
                                        sequenceBreak = false;
                                }
                            }
                            else
                            {
                                if (_game.Mode.SmallKeyShuffle)
                                {
                                    inaccessible = 2;

                                    if (_game.Items.Has(ItemType.FireRod) || _game.Mode.EntranceShuffle.Value)
                                    {
                                        inaccessible = 1;

                                        if (_game.Items.CanRemoveCurtains())
                                        {
                                            inaccessible = 0;

                                            if (_game.Items.Has(ItemType.SWSmallKey))
                                                sequenceBreak = false;
                                        }
                                    }
                                }
                                else
                                {

                                }
                            }

                            if (!_game.Mode.MapCompassShuffle)
                                inaccessible = Math.Max(0, inaccessible - _mapCompass);

                            if (inaccessible == 0)
                            {
                                if (!sequenceBreak)
                                    return (Available, sW);

                                return (Available, AccessibilityLevel.SequenceBreak);
                            }

                            if (Available > inaccessible)
                                return (Available - inaccessible, AccessibilityLevel.Partial);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    _updateOnDungeonItemShuffleChange = true;

                    _regionSubscriptions.Add(RegionID.SkullWoods, new Mode());

                    _itemSubscriptions.Add(ItemType.FireRod, new Mode());
                    _itemSubscriptions.Add(ItemType.Sword, new Mode());
                    _itemSubscriptions.Add(ItemType.SWSmallKey, new Mode());
                    _itemSubscriptions.Add(ItemType.SmallKey, new Mode() { WorldState = WorldState.Retro });
                    _itemSubscriptions.Add(ItemType.SWBigKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity });

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

                        if (tT >= AccessibilityLevel.SequenceBreak)
                        {
                            int inaccessible = 0;
                            bool sequenceBreak = true;

                            if (_game.Mode.BigKeyShuffle)
                            {
                                if (_game.Mode.SmallKeyShuffle)
                                {
                                    inaccessible = 4;

                                    if (_game.Items.Has(ItemType.TTBigKey))
                                    {
                                        inaccessible = 1;

                                        if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.TTSmallKey))
                                        {
                                            inaccessible = 0;
                                            sequenceBreak = false;
                                        }
                                    }
                                }
                                else
                                {
                                    inaccessible = 0;

                                    if (_game.Items.Has(ItemType.Hammer))
                                        sequenceBreak = false;
                                }
                            }
                            else
                            {
                                if (_game.Mode.SmallKeyShuffle)
                                {
                                    inaccessible = 1;

                                    if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.TTSmallKey))
                                    {
                                        inaccessible = 0;
                                        sequenceBreak = false;
                                    }
                                }
                                else
                                {
                                    inaccessible = 0;

                                    if (_game.Items.Has(ItemType.Hammer))
                                        sequenceBreak = false;
                                }
                            }

                            if (!_game.Mode.MapCompassShuffle)
                                inaccessible = Math.Max(0, inaccessible - _mapCompass);

                            if (inaccessible == 0)
                            {
                                if (!sequenceBreak)
                                    return (Available, tT);

                                return (Available, AccessibilityLevel.SequenceBreak);
                            }

                            if (Available > inaccessible)
                                return (Available - inaccessible, AccessibilityLevel.Partial);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    _updateOnDungeonItemShuffleChange = true;

                    _regionSubscriptions.Add(RegionID.ThievesTown, new Mode());

                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.TTSmallKey, new Mode());
                    _itemSubscriptions.Add(ItemType.SmallKey, new Mode() { WorldState = WorldState.Retro });
                    _itemSubscriptions.Add(ItemType.TTBigKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity });

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

                        if (iP >= AccessibilityLevel.SequenceBreak)
                        {
                            int inaccessible = 8;
                            bool sequenceBreak = true;

                            if (_game.Mode.BigKeyShuffle)
                            {
                                if (_game.Mode.SmallKeyShuffle)
                                {
                                    if (_game.Items.CanMeltThings())
                                    {
                                        inaccessible = 4;

                                        if (_game.Items.Has(ItemType.IPBigKey))
                                            inaccessible = 3;

                                        if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        {
                                            inaccessible = 3;

                                            if (_game.Items.Has(ItemType.IPBigKey))
                                                inaccessible = 2;
                                        }

                                        if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Gloves))
                                        {
                                            inaccessible = 1;

                                            if (_game.Items.Has(ItemType.IPBigKey))
                                            {
                                                inaccessible = 0;

                                                if ((_game.Items.Has(ItemType.Hookshot) && _game.Items.Has(ItemType.CaneOfSomaria)) ||
                                                    (_game.Items.Has(ItemType.Hookshot) && _game.Items.Has(ItemType.IPSmallKey)) ||
                                                    (_game.Items.Has(ItemType.CaneOfSomaria) && _game.Items.Has(ItemType.IPSmallKey)) ||
                                                    _game.Items.Has(ItemType.IPSmallKey, 2))
                                                    sequenceBreak = false;
                                            }
                                        }

                                    }
                                }
                                else
                                {
                                    if (_game.Items.CanMeltThings())
                                    {
                                        inaccessible = 2;

                                        if (_game.Items.Has(ItemType.IPBigKey))
                                            inaccessible = 1;

                                        if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        {
                                            inaccessible = 1;

                                            if (_game.Items.Has(ItemType.IPBigKey))
                                                inaccessible = 0;
                                        }

                                        if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Gloves))
                                        {
                                            inaccessible = 0;

                                            if (_game.Items.Has(ItemType.Hookshot) && _game.Items.Has(ItemType.CaneOfSomaria) &&
                                                _game.Items.Has(ItemType.IPBigKey))
                                                sequenceBreak = false;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (_game.Mode.SmallKeyShuffle)
                                {
                                    if (_game.Items.CanMeltThings())
                                    {
                                        inaccessible = 3;

                                        if (_game.Items.Has(ItemType.CaneOfSomaria))
                                            inaccessible = 2;

                                        if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Gloves))
                                        {
                                            inaccessible = 0;

                                            if ((_game.Items.Has(ItemType.Hookshot) && _game.Items.Has(ItemType.CaneOfSomaria)) ||
                                                (_game.Items.Has(ItemType.Hookshot) && _game.Items.Has(ItemType.IPSmallKey)) ||
                                                (_game.Items.Has(ItemType.CaneOfSomaria) && _game.Items.Has(ItemType.IPSmallKey)) ||
                                                _game.Items.Has(ItemType.IPSmallKey, 2))
                                                sequenceBreak = false;
                                        }
                                    }
                                }
                                else
                                {
                                    if (_game.Items.CanMeltThings())
                                    {
                                        inaccessible = 0;

                                        if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Gloves) &&
                                            _game.Items.Has(ItemType.Hookshot) && _game.Items.Has(ItemType.CaneOfSomaria))
                                            sequenceBreak = false;
                                    }
                                }
                            }

                            if (!_game.Mode.MapCompassShuffle)
                                inaccessible = Math.Max(0, inaccessible - _mapCompass);

                            if (inaccessible == 0)
                            {
                                if (!sequenceBreak)
                                    return (Available, iP);

                                return (Available, AccessibilityLevel.SequenceBreak);
                            }

                            if (Available > inaccessible)
                                return (Available - inaccessible, AccessibilityLevel.Partial);
                        }

                        return (0, AccessibilityLevel.None);
                    };

                    _updateOnDungeonItemShuffleChange = true;

                    _regionSubscriptions.Add(RegionID.IcePalace, new Mode());

                    _itemSubscriptions.Add(ItemType.FireRod, new Mode());
                    _itemSubscriptions.Add(ItemType.Bombos, new Mode());
                    _itemSubscriptions.Add(ItemType.Sword, new Mode());
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.Gloves, new Mode());
                    _itemSubscriptions.Add(ItemType.Hookshot, new Mode());
                    _itemSubscriptions.Add(ItemType.CaneOfSomaria, new Mode());
                    _itemSubscriptions.Add(ItemType.IPSmallKey, new Mode());
                    _itemSubscriptions.Add(ItemType.SmallKey, new Mode() { WorldState = WorldState.Retro });
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

                        if (mM >= AccessibilityLevel.SequenceBreak)
                        {
                            int inaccessible = 8;
                            bool sequenceBreak = true;

                            if (_game.Mode.BigKeyShuffle)
                            {
                                if (_game.Mode.SmallKeyShuffle)
                                {
                                    if (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots))
                                    {
                                        inaccessible = 4;

                                        if (_game.Items.Has(ItemType.MMBigKey))
                                            inaccessible = 3;

                                        if (_game.Items.Has(ItemType.CaneOfSomaria) && _game.Items.Has(ItemType.MMBigKey))
                                            inaccessible = 2;

                                        if (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod))
                                        {
                                            inaccessible = 2;

                                            if (_game.Items.Has(ItemType.MMBigKey))
                                                inaccessible = 1;

                                            if (_game.Items.Has(ItemType.CaneOfSomaria) && _game.Items.Has(ItemType.MMBigKey))
                                            {
                                                inaccessible = 0;

                                                if ((_game.Items.Has(ItemType.Hookshot) ||
                                                    (_game.Items.Has(ItemType.Boots) && _game.Mode.ItemPlacement == ItemPlacement.Advanced)) &&
                                                    _game.Items.Has(ItemType.Lamp))
                                                    sequenceBreak = false;
                                            }
                                        }


                                    }
                                }
                                else
                                {
                                    if (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots))
                                    {
                                        inaccessible = 4;

                                        if (_game.Items.Has(ItemType.MMBigKey))
                                            inaccessible = 3;

                                        if (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod))
                                        {
                                            inaccessible = 2;

                                            if (_game.Items.Has(ItemType.MMBigKey))
                                            {
                                                inaccessible = 1;

                                                if (_game.Items.Has(ItemType.CaneOfSomaria))
                                                {
                                                    inaccessible = 0;
                                                    if ((_game.Items.Has(ItemType.Hookshot) ||
                                                        (_game.Items.Has(ItemType.Boots) &&
                                                        _game.Mode.ItemPlacement == ItemPlacement.Advanced)) &&
                                                        _game.Items.Has(ItemType.Lamp))
                                                        sequenceBreak = false;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (_game.Mode.SmallKeyShuffle)
                                {
                                    if (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots))
                                    {
                                        inaccessible = 3;

                                        if (_game.Items.Has(ItemType.CaneOfSomaria))
                                            inaccessible = 2;

                                        if (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod))
                                        {
                                            inaccessible = 1;

                                            if (_game.Items.Has(ItemType.CaneOfSomaria))
                                            {
                                                inaccessible = 0;

                                                if ((_game.Items.Has(ItemType.Hookshot) ||
                                                    (_game.Items.Has(ItemType.Boots) && _game.Mode.ItemPlacement == ItemPlacement.Advanced)) &&
                                                    _game.Items.Has(ItemType.Lamp))
                                                    sequenceBreak = false;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots))
                                    {
                                        inaccessible = 3;

                                        if (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod))
                                        {
                                            inaccessible = 1;

                                            if (_game.Items.Has(ItemType.CaneOfSomaria))
                                            {
                                                inaccessible = 0;

                                                if ((_game.Items.Has(ItemType.Hookshot) || (_game.Items.Has(ItemType.Boots) &&
                                                    _game.Mode.ItemPlacement == ItemPlacement.Advanced)) && _game.Items.Has(ItemType.Lamp))
                                                    sequenceBreak = false;
                                            }
                                        }
                                    }
                                }
                            }

                            if (!_game.Mode.MapCompassShuffle)
                                inaccessible = Math.Max(0, inaccessible - _mapCompass);

                            if (inaccessible == 0)
                            {
                                if (!sequenceBreak)
                                    return (Available, mM);

                                return (Available, AccessibilityLevel.SequenceBreak);
                            }

                            if (Available > inaccessible)
                                return (Available - inaccessible, AccessibilityLevel.Partial);
                        }

                        return (0, AccessibilityLevel.None);
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
                        AccessibilityLevel tRFront = _game.Regions[RegionID.TurtleRockFront].Accessibility;
                        AccessibilityLevel tRBack = _game.Regions[RegionID.TurtleRockBack].Accessibility;

                        int inaccessible = 12;
                        bool sequenceBreak = true;

                        if (tRFront >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Mode.BigKeyShuffle)
                            {
                                if (_game.Mode.SmallKeyShuffle)
                                {
                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                    {
                                        inaccessible = 11;

                                        if (_game.Items.Has(ItemType.TRSmallKey))
                                        {
                                            inaccessible = 10;

                                            if (_game.Items.Has(ItemType.FireRod))
                                                inaccessible = 8;
                                        }

                                        if (_game.Items.Has(ItemType.TRSmallKey, 2))
                                        {
                                            inaccessible = 9;

                                            if (_game.Items.Has(ItemType.FireRod))
                                                inaccessible = 7;
                                        }

                                        if (_game.Items.Has(ItemType.TRBigKey))
                                        {
                                            if (_game.Items.Has(ItemType.TRSmallKey, 2))
                                            {
                                                inaccessible = 7;

                                                if (_game.Items.Has(ItemType.FireRod))
                                                    inaccessible = 5;
                                            }

                                            if (_game.Items.Has(ItemType.TRSmallKey, 3))
                                            {
                                                inaccessible = 3;

                                                if (_game.Items.Has(ItemType.FireRod))
                                                    inaccessible = 1;
                                            }

                                            if (_game.Items.Has(ItemType.TRSmallKey, 4))
                                            {
                                                inaccessible = 2;

                                                if (_game.Items.Has(ItemType.FireRod))
                                                {
                                                    inaccessible = 0;

                                                    if (tRFront == AccessibilityLevel.Normal && (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                                        _game.Items.Has(ItemType.Cape) || _game.Items.Has(ItemType.CaneOfByrna) ||
                                                        _game.Items.Has(ItemType.Shield, 3)) && _game.Items.Has(ItemType.Lamp))
                                                        sequenceBreak = false;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                    {
                                        inaccessible = 9;

                                        if (_game.Items.Has(ItemType.FireRod))
                                            inaccessible = 7;

                                        if (_game.Items.Has(ItemType.TRBigKey))
                                        {
                                            inaccessible = 2;

                                            if (_game.Items.Has(ItemType.FireRod))
                                            {
                                                inaccessible = 0;

                                                if (tRFront == AccessibilityLevel.Normal && _game.Items.Has(ItemType.Lamp) &&
                                                    (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                                    _game.Items.Has(ItemType.Cape) || _game.Items.Has(ItemType.CaneOfByrna) ||
                                                    _game.Items.Has(ItemType.Shield, 3)))
                                                    sequenceBreak = false;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (_game.Mode.SmallKeyShuffle)
                                {
                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                    {
                                        inaccessible = 10;

                                        if (_game.Items.Has(ItemType.TRSmallKey))
                                            inaccessible = 9;

                                        if (_game.Items.Has(ItemType.TRSmallKey, 2))
                                            inaccessible = 7;

                                        if (_game.Items.Has(ItemType.TRSmallKey, 3))
                                            inaccessible = 3;

                                        if (_game.Items.Has(ItemType.TRSmallKey, 4))
                                            inaccessible = 2;

                                        if (_game.Items.Has(ItemType.FireRod))
                                        {
                                            inaccessible = 8;

                                            if (_game.Items.Has(ItemType.TRSmallKey))
                                                inaccessible = 7;

                                            if (_game.Items.Has(ItemType.TRSmallKey, 2))
                                                inaccessible = 5;

                                            if (_game.Items.Has(ItemType.TRSmallKey, 3))
                                                inaccessible = 1;

                                            if (_game.Items.Has(ItemType.TRSmallKey, 4))
                                            {
                                                inaccessible = 0;

                                                if (tRFront == AccessibilityLevel.Normal && (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                                    _game.Items.Has(ItemType.Cape) || _game.Items.Has(ItemType.CaneOfByrna) ||
                                                    _game.Items.Has(ItemType.Shield, 3)) && _game.Items.Has(ItemType.Lamp))
                                                    sequenceBreak = false;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                    {
                                        inaccessible = 2;

                                        if (_game.Items.Has(ItemType.FireRod))
                                        {
                                            inaccessible = 0;

                                            if (tRFront == AccessibilityLevel.Normal && _game.Items.Has(ItemType.Lamp) &&
                                                (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                                _game.Items.Has(ItemType.Cape) || _game.Items.Has(ItemType.CaneOfByrna) ||
                                                _game.Items.Has(ItemType.Shield, 3)))
                                                sequenceBreak = false;
                                        }
                                    }
                                }
                            }
                        }

                        if (tRBack >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Mode.BigKeyShuffle)
                            {
                                if (_game.Mode.SmallKeyShuffle)
                                {
                                    inaccessible = 5;

                                    if (_game.Items.Has(ItemType.TRBigKey) && _game.Items.Has(ItemType.Hookshot))
                                        inaccessible = 4;

                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                    {
                                        if (_game.Items.Has(ItemType.TRSmallKey))
                                        {
                                            inaccessible = 4;

                                            if (_game.Items.Has(ItemType.TRBigKey))
                                                inaccessible = 3;

                                            if (_game.Items.Has(ItemType.FireRod))
                                            {
                                                inaccessible = 2;

                                                if (_game.Items.Has(ItemType.TRBigKey))
                                                    inaccessible = 1;
                                            }
                                        }

                                        if (_game.Items.Has(ItemType.TRSmallKey, 2) && _game.Items.Has(ItemType.TRBigKey))
                                        {
                                            inaccessible = 2;

                                            if (_game.Items.Has(ItemType.FireRod))
                                            {
                                                inaccessible = 0;

                                                if (tRBack == AccessibilityLevel.Normal && (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                                    _game.Items.Has(ItemType.Cape) || _game.Items.Has(ItemType.CaneOfByrna) ||
                                                    _game.Items.Has(ItemType.Shield, 3)))
                                                    sequenceBreak = false;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    inaccessible = 1;

                                    if (_game.Items.Has(ItemType.Hookshot) && _game.Items.Has(ItemType.TRBigKey))
                                        inaccessible = 0;

                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                    {
                                        inaccessible = 0;
                                        
                                        if (tRBack == AccessibilityLevel.Normal && _game.Items.Has(ItemType.TRBigKey) &&
                                            _game.Items.Has(ItemType.FireRod) && (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                            _game.Items.Has(ItemType.Cape) || _game.Items.Has(ItemType.CaneOfByrna) ||
                                            _game.Items.Has(ItemType.Shield, 3)))
                                            sequenceBreak = false;
                                    }
                                }
                            }
                            else
                            {
                                if (_game.Mode.SmallKeyShuffle)
                                {
                                    inaccessible = 5;

                                    if (_game.Items.Has(ItemType.Hookshot))
                                        inaccessible = 4;

                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                    {
                                        if (_game.Items.Has(ItemType.TRSmallKey))
                                        {
                                            inaccessible = 3;

                                            if (_game.Items.Has(ItemType.FireRod))
                                                inaccessible = 1;
                                        }

                                        if (_game.Items.Has(ItemType.TRSmallKey, 2))
                                        {
                                            inaccessible = 2;

                                            if (_game.Items.Has(ItemType.FireRod))
                                            {
                                                inaccessible = 0;

                                                if (tRBack == AccessibilityLevel.Normal && (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                                    _game.Items.Has(ItemType.Cape) || _game.Items.Has(ItemType.CaneOfByrna) ||
                                                    _game.Items.Has(ItemType.Shield, 3)))
                                                    sequenceBreak = false;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    inaccessible = 1;

                                    if (_game.Items.Has(ItemType.Hookshot))
                                        inaccessible = 0;

                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                    {
                                        inaccessible = 0;

                                        if (tRBack == AccessibilityLevel.Normal && _game.Items.Has(ItemType.FireRod) &&
                                            (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                            _game.Items.Has(ItemType.Cape) || _game.Items.Has(ItemType.CaneOfByrna) ||
                                            _game.Items.Has(ItemType.Shield, 3)))
                                            sequenceBreak = false;
                                    }
                                }
                            }

                            if (tRFront >= AccessibilityLevel.SequenceBreak)
                            {
                                if (_game.Mode.BigKeyShuffle)
                                {
                                    if (_game.Mode.SmallKeyShuffle)
                                    {
                                        inaccessible = 5;

                                        if (_game.Items.Has(ItemType.TRBigKey) && _game.Items.Has(ItemType.Hookshot))
                                            inaccessible = 4;

                                        if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        {
                                            inaccessible = 4;
                                            
                                            if (_game.Items.Has(ItemType.TRBigKey))
                                                inaccessible = 3;
                                            
                                            if (_game.Items.Has(ItemType.FireRod))
                                            {
                                                inaccessible = 2;
                                                
                                                if (_game.Items.Has(ItemType.TRBigKey))
                                                    inaccessible = 1;
                                            }

                                            if (_game.Items.Has(ItemType.TRSmallKey) && _game.Items.Has(ItemType.TRBigKey))
                                            {
                                                inaccessible = 2;

                                                if (_game.Items.Has(ItemType.FireRod))
                                                {
                                                    inaccessible = 0;

                                                    if (tRBack == AccessibilityLevel.Normal && tRFront == AccessibilityLevel.Normal &&
                                                        (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                                        _game.Items.Has(ItemType.Cape) || _game.Items.Has(ItemType.CaneOfByrna) ||
                                                        _game.Items.Has(ItemType.Shield, 3)))
                                                        sequenceBreak = false;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        inaccessible = 1;

                                        if (_game.Items.Has(ItemType.Hookshot) && _game.Items.Has(ItemType.TRBigKey))
                                            inaccessible = 0;

                                        if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        {
                                            inaccessible = 0;

                                            if (tRBack == AccessibilityLevel.Normal && tRFront == AccessibilityLevel.Normal &&
                                                _game.Items.Has(ItemType.TRBigKey) && _game.Items.Has(ItemType.FireRod) &&
                                                (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                                _game.Items.Has(ItemType.Cape) || _game.Items.Has(ItemType.CaneOfByrna) ||
                                                _game.Items.Has(ItemType.Shield, 3)))
                                                sequenceBreak = false;
                                        }
                                    }
                                }
                                else
                                {
                                    if (_game.Mode.SmallKeyShuffle)
                                    {
                                        inaccessible = 5;

                                        if (_game.Items.Has(ItemType.Hookshot))
                                            inaccessible = 4;

                                        if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        {
                                            inaccessible = 3;
                                            
                                            if (_game.Items.Has(ItemType.FireRod))
                                                inaccessible = 1;

                                            if (_game.Items.Has(ItemType.TRSmallKey))
                                            {
                                                inaccessible = 2;

                                                if (_game.Items.Has(ItemType.FireRod))
                                                {
                                                    inaccessible = 0;

                                                    if (tRBack == AccessibilityLevel.Normal && tRFront == AccessibilityLevel.Normal &&
                                                        (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                                        _game.Items.Has(ItemType.Cape) || _game.Items.Has(ItemType.CaneOfByrna) ||
                                                        _game.Items.Has(ItemType.Shield, 3)))
                                                        sequenceBreak = false;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        inaccessible = 1;

                                        if (_game.Items.Has(ItemType.Hookshot))
                                            inaccessible = 0;

                                        if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        {
                                            inaccessible = 0;

                                            if (tRBack == AccessibilityLevel.Normal && tRFront == AccessibilityLevel.Normal &&
                                                _game.Items.Has(ItemType.FireRod) &&
                                                (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                                _game.Items.Has(ItemType.Cape) || _game.Items.Has(ItemType.CaneOfByrna) ||
                                                _game.Items.Has(ItemType.Shield, 3)))
                                                sequenceBreak = false;
                                        }
                                    }
                                }
                            }
                        }

                        if (!_game.Mode.MapCompassShuffle)
                            inaccessible = Math.Max(0, inaccessible - _mapCompass);

                        if (inaccessible == 0)
                        {
                            if (!sequenceBreak)
                                return (Available, AccessibilityLevel.Normal);

                            return (Available, AccessibilityLevel.SequenceBreak);
                        }

                        if (Available > inaccessible)
                            return (Available - inaccessible, AccessibilityLevel.Partial);

                        return (0, AccessibilityLevel.None);
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
                    _itemSubscriptions.Add(ItemType.TRSmallKey, new Mode());
                    _itemSubscriptions.Add(ItemType.SmallKey, new Mode() { WorldState = WorldState.Retro });
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

                        if (gT >= AccessibilityLevel.SequenceBreak)
                        {
                            int inaccessible = 27;
                            bool sequenceBreak = true;

                            if (_game.Mode.BigKeyShuffle)
                            {
                                if (_game.Mode.SmallKeyShuffle)
                                {
                                    inaccessible = 23;

                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        inaccessible = 22;

                                    if (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp))
                                    {
                                        if (_game.Items.CanClearRedEyegoreGoriyaRooms() && _game.Items.Has(ItemType.GTBigKey))
                                        {
                                            inaccessible = 21;

                                            if (_game.Items.Has(ItemType.GTSmallKey) &&
                                                (_game.Items.Has(ItemType.Boots) || _game.Items.Has(ItemType.Hookshot)))
                                                inaccessible = 20;
                                        }

                                        if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        {
                                            inaccessible = 14;

                                            if (_game.Items.Has(ItemType.GTBigKey))
                                            {
                                                inaccessible = 13;

                                                if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                                {
                                                    inaccessible = 10;

                                                    if (_game.Items.Has(ItemType.GTSmallKey) &&
                                                        (_game.Items.Has(ItemType.Boots) || _game.Items.Has(ItemType.Hookshot)))
                                                        inaccessible = 9;
                                                }
                                            }
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.Hammer) &&
                                        (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots)))
                                    {
                                        inaccessible = 11;

                                        if (_game.Items.Has(ItemType.GTSmallKey))
                                            inaccessible = 10;

                                        if (_game.Items.Has(ItemType.GTBigKey))
                                        {
                                            inaccessible = 10;

                                            if (_game.Items.Has(ItemType.GTSmallKey))
                                                inaccessible = 9;
                                        }

                                        if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        {
                                            inaccessible = 10;

                                            if (_game.Items.Has(ItemType.GTSmallKey))
                                                inaccessible = 9;

                                            if (_game.Items.Has(ItemType.GTBigKey))
                                            {
                                                inaccessible = 9;

                                                if (_game.Items.Has(ItemType.GTSmallKey))
                                                    inaccessible = 8;
                                            }
                                        }

                                        if (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp))
                                        {
                                            if (_game.Items.Has(ItemType.GTBigKey) && _game.Items.CanClearRedEyegoreGoriyaRooms())
                                            {
                                                inaccessible = 6;

                                                if (_game.Items.Has(ItemType.GTSmallKey))
                                                    inaccessible = 5;
                                            }

                                            if (_game.Items.Has(ItemType.CaneOfSomaria))
                                            {
                                                inaccessible = 6;

                                                if (_game.Items.Has(ItemType.GTSmallKey))
                                                    inaccessible = 5;

                                                if (_game.Items.Has(ItemType.GTBigKey))
                                                {
                                                    inaccessible = 5;

                                                    if (_game.Items.Has(ItemType.GTSmallKey))
                                                        inaccessible = 4;

                                                    if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                                    {
                                                        inaccessible = 2;

                                                        if (_game.Items.Has(ItemType.GTSmallKey))
                                                            inaccessible = 1;

                                                        if (_game.Items.Has(ItemType.GTSmallKey, 2))
                                                        {
                                                            inaccessible = 0;

                                                            if (_game.Items.Has(ItemType.Hookshot) && _game.Items.Has(ItemType.Boots) &&
                                                                _game.Items.Has(ItemType.FireRod))
                                                                sequenceBreak = false;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    inaccessible = 20;

                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        inaccessible = 19;

                                    if (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp))
                                    {
                                        if (_game.Items.CanClearRedEyegoreGoriyaRooms() && _game.Items.Has(ItemType.GTBigKey))
                                            inaccessible = 17;

                                        if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        {
                                            inaccessible = 10;

                                            if (_game.Items.Has(ItemType.GTBigKey))
                                            {
                                                inaccessible = 9;

                                                if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                                    inaccessible = 6;
                                            }
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.Hammer) &&
                                        (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots)))
                                    {
                                        inaccessible = 7;

                                        if (_game.Items.Has(ItemType.TRBigKey))
                                            inaccessible = 6;

                                        if (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp))
                                        {
                                            if (_game.Items.Has(ItemType.CaneOfSomaria))
                                                inaccessible = 4;

                                            if (_game.Items.Has(ItemType.TRBigKey))
                                            {
                                                if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                                    inaccessible = 4;

                                                if (_game.Items.Has(ItemType.CaneOfSomaria))
                                                {
                                                    inaccessible = 3;

                                                    if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                                    {
                                                        inaccessible = 0;

                                                        if (_game.Items.Has(ItemType.Hookshot) && _game.Items.Has(ItemType.Boots) &&
                                                            _game.Items.Has(ItemType.FireRod))
                                                            sequenceBreak = false;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (_game.Mode.SmallKeyShuffle)
                                {
                                    inaccessible = 23;

                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        inaccessible = 22;

                                    if (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp))
                                    {
                                        if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                        {
                                            inaccessible = 21;

                                            if (_game.Items.Has(ItemType.Boots) || _game.Items.Has(ItemType.Hookshot))
                                                inaccessible = 20;
                                        }

                                        if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        {
                                            inaccessible = 13;

                                            if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                            {
                                                inaccessible = 10;

                                                if (_game.Items.Has(ItemType.GTSmallKey) &&
                                                    (_game.Items.Has(ItemType.Boots) || _game.Items.Has(ItemType.Hookshot)))
                                                    inaccessible = 9;
                                            }
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.Hammer) &&
                                        (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots)))
                                    {
                                        inaccessible = 10;

                                        if (_game.Items.Has(ItemType.GTSmallKey))
                                            inaccessible = 9;

                                        if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        {
                                            inaccessible = 9;

                                            if (_game.Items.Has(ItemType.GTSmallKey))
                                                inaccessible = 8;
                                        }

                                        if (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp))
                                        {
                                            if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                            {
                                                inaccessible = 6;

                                                if (_game.Items.Has(ItemType.GTSmallKey))
                                                    inaccessible = 5;
                                            }

                                            if (_game.Items.Has(ItemType.CaneOfSomaria))
                                            {
                                                inaccessible = 5;

                                                if (_game.Items.Has(ItemType.GTSmallKey))
                                                    inaccessible = 4;

                                                if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                                {
                                                    inaccessible = 2;

                                                    if (_game.Items.Has(ItemType.GTSmallKey))
                                                        inaccessible = 1;

                                                    if (_game.Items.Has(ItemType.GTSmallKey, 2))
                                                    {
                                                        inaccessible = 0;

                                                        if (_game.Items.Has(ItemType.Hookshot) && _game.Items.Has(ItemType.Boots) &&
                                                            _game.Items.Has(ItemType.FireRod))
                                                            sequenceBreak = false;
                                                    }    
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    inaccessible = 19;

                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        inaccessible = 18;

                                    if (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp))
                                    {
                                        if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                            inaccessible = 16;

                                        if (_game.Items.Has(ItemType.CaneOfSomaria))
                                        {
                                            inaccessible = 9;

                                            if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                                inaccessible = 6;
                                        }
                                    }

                                    if (_game.Items.Has(ItemType.Hammer) &&
                                        (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots)))
                                    {
                                        inaccessible = 6;

                                        if (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp))
                                        {
                                            if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                                inaccessible = 4;

                                            if (_game.Items.Has(ItemType.CaneOfSomaria))
                                            {
                                                inaccessible = 3;

                                                if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                                {
                                                    inaccessible = 0;

                                                    if (_game.Items.Has(ItemType.Hookshot) && _game.Items.Has(ItemType.Boots) &&
                                                        _game.Items.Has(ItemType.FireRod))
                                                        sequenceBreak = false;
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            if (!_game.Mode.MapCompassShuffle)
                                inaccessible = Math.Max(0, inaccessible - _mapCompass);

                            if (inaccessible == 0)
                            {
                                if (!sequenceBreak)
                                    return (Available, gT);

                                return (Available, AccessibilityLevel.SequenceBreak);
                            }

                            if (Available > inaccessible)
                                return (Available - inaccessible, AccessibilityLevel.Partial);
                        }

                        return (0, AccessibilityLevel.None);
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
                    _itemSubscriptions.Add(ItemType.GTSmallKey, new Mode());
                    _itemSubscriptions.Add(ItemType.SmallKey, new Mode() { WorldState = WorldState.Retro });
                    _itemSubscriptions.Add(ItemType.GTBigKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity });

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

        private void OnPropertyChanging(string propertyName)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (propertyName == nameof(Available))
            {
                UpdateAccessibility();

                if (!IsAvailable())
                    CollectMarkingItem();
            }
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
            {
                SetTotal();
                UpdateAccessibility();
            }

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

        private void OnMemoryChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!UserManipulated)
                AutoTrack();
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
                (_game.Mode.MapCompassShuffle ? _mapCompass : 0) +
                (_game.Mode.SmallKeyShuffle ? _smallKey : 0) +
                (_game.Mode.BigKeyShuffle ? _bigKey : 0);

            int delta = newTotal - Total;

            Total = newTotal;
            Available = Math.Max(0, Math.Min(Total, Available + delta));
        }

        private void UpdateAccessibility()
        {
            (int, AccessibilityLevel) accessibility = GetAccessibility();

            Accessible = accessibility.Item1;
            Accessibility = accessibility.Item2;
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
            UserManipulated = false;
        }
    }
}
