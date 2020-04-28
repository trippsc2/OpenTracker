using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models
{
    public class BossSection : ISection
    {
        private readonly Game _game;
        private readonly Boss _defaultBoss;
        private readonly bool _updateOnItemPlacementChange;
        private readonly bool _updateOnWorldStateChange;
        private readonly bool _updateOnEnemyShuffleChange;
        private readonly Dictionary<RegionID, Mode> _regionSubscriptions;
        private readonly Dictionary<RegionID, bool> _regionIsSubscribed;
        private readonly Dictionary<ItemType, Mode> _itemSubscriptions;
        private readonly Dictionary<ItemType, bool> _itemIsSubscribed;
        private Boss _currentBossSubscription;

        public string Name { get => "Boss"; }
        public bool HasMarking { get => false; }
        public Mode RequiredMode { get; }
        public bool UserManipulated { get; set; }
        public MarkingType? Marking { get => null; set { } }

        public Func<AccessibilityLevel> GetAccessibility { get; }
        public Action AutoTrack { get; }

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

        private Boss _boss;
        public Boss Boss
        {
            get => _boss;
            set
            {
                if (_boss != value)
                {
                    OnPropertyChanging(nameof(Boss));
                    _boss = value;
                    OnPropertyChanged(nameof(Boss));
                }
            }
        }

        private Item _prize;
        public Item Prize
        {
            get => _prize;
            set
            {
                if (_prize != value)
                {
                    OnPropertyChanging(nameof(Prize));
                    _prize = value;
                    OnPropertyChanged(nameof(Prize));
                }
            }
        }

        public BossSection(Game game, LocationID iD)
        {
            _game = game;

            _regionSubscriptions = new Dictionary<RegionID, Mode>();
            _regionIsSubscribed = new Dictionary<RegionID, bool>();
            _itemSubscriptions = new Dictionary<ItemType, Mode>();
            _itemIsSubscribed = new Dictionary<ItemType, bool>();

            RequiredMode = new Mode();
            Available = 1;

            switch (iD)
            {
                case LocationID.AgahnimTower:

                    _defaultBoss = _game.Bosses[BossType.Aga];
                    _boss = _game.Bosses[BossType.Aga];
                    Prize = _game.Items[ItemType.Aga];

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel aga = _game.Regions[RegionID.Agahnim].Accessibility;
                        
                        if (_game.Items.Has(ItemType.ATSmallKey, 2) && _game.Items.CanRemoveCurtains())
                        {
                            if (_game.Items.Has(ItemType.Lamp))
                                return aga;
                            
                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)aga);
                        }

                        return AccessibilityLevel.None;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 133, 2);

                        if (result.HasValue)
                        {
                            if (result.Value)
                                Available = 0;
                            else
                                Available = 1;
                        }
                   };

                    _game.AutoTracker.ItemMemory[133].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.Agahnim, new Mode());

                    _itemSubscriptions.Add(ItemType.ATSmallKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys });
                    _itemSubscriptions.Add(ItemType.Sword, new Mode());
                    _itemSubscriptions.Add(ItemType.Lamp, new Mode());

                    break;
                case LocationID.EasternPalace:

                    _defaultBoss = _game.Bosses[BossType.Armos];

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel eP = _game.Regions[RegionID.EasternPalace].Accessibility;
                        
                        if (_game.Items.CanClearRedEyegoreGoriyaRooms() && _game.Items.Has(ItemType.EPBigKey))
                        {
                            if (_game.Items.Has(ItemType.Lamp) || (_game.Items.Has(ItemType.FireRod) &&
                                _game.Mode.ItemPlacement == ItemPlacement.Advanced))
                                return eP;
                            
                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)eP);
                        }

                        return AccessibilityLevel.None;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 401, 8);

                        if (result.HasValue)
                        {
                            if (result.Value)
                                Available = 0;
                            else
                                Available = 1;
                        }
                    };

                    _game.AutoTracker.RoomMemory[401].PropertyChanged += OnMemoryChanged;

                    _updateOnItemPlacementChange = true;
                    _updateOnEnemyShuffleChange = true;

                    _regionSubscriptions.Add(RegionID.EasternPalace, new Mode());

                    _itemSubscriptions.Add(ItemType.Bow, new Mode() { EnemyShuffle = false });
                    _itemSubscriptions.Add(ItemType.EPBigKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity });
                    _itemSubscriptions.Add(ItemType.Lamp, new Mode());
                    _itemSubscriptions.Add(ItemType.FireRod, new Mode() { ItemPlacement = ItemPlacement.Advanced });

                    break;
                case LocationID.DesertPalace:

                    _defaultBoss = _game.Bosses[BossType.Lanmolas];

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel dP = _game.Regions[RegionID.DesertPalace].Accessibility;
                        
                        if ((_game.Items.Has(ItemType.Gloves) || _game.Mode.EntranceShuffle.Value) &&
                            (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.Lamp)) &&
                            _game.Items.Has(ItemType.DPBigKey))
                        {
                            if (_game.Items.Has(ItemType.DPSmallKey))
                                return dP;
                            
                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)dP);
                        }

                        return AccessibilityLevel.None;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 103, 8);

                        if (result.HasValue)
                        {
                            if (result.Value)
                                Available = 0;
                            else
                                Available = 1;
                        }
                    };

                    _game.AutoTracker.RoomMemory[103].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.DesertPalace, new Mode());

                    _itemSubscriptions.Add(ItemType.Gloves, new Mode() { EntranceShuffle = false });
                    _itemSubscriptions.Add(ItemType.FireRod, new Mode());
                    _itemSubscriptions.Add(ItemType.Lamp, new Mode());
                    _itemSubscriptions.Add(ItemType.DPBigKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity });
                    _itemSubscriptions.Add(ItemType.DPSmallKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys });

                    break;
                case LocationID.TowerOfHera:

                    _defaultBoss = _game.Bosses[BossType.Moldorm];

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel tH = _game.Regions[RegionID.TowerOfHera].Accessibility;

                        if (_game.Mode.DungeonItemShuffle == DungeonItemShuffle.Keysanity)
                        {
                            if (_game.Items.Has(ItemType.ToHBigKey))
                                return tH;

                            if (_game.Items.Has(ItemType.Hookshot))
                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)tH);
                        }
                        else
                        {
                            if (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod))
                                return tH;

                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)tH);
                        }

                        return AccessibilityLevel.None;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 15, 8);

                        if (result.HasValue)
                        {
                            if (result.Value)
                                Available = 0;
                            else
                                Available = 1;
                        }
                    };

                    _game.AutoTracker.RoomMemory[15].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.TowerOfHera, new Mode());

                    _itemSubscriptions.Add(ItemType.ToHBigKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity });
                    _itemSubscriptions.Add(ItemType.Hookshot, new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity });
                    _itemSubscriptions.Add(ItemType.Lamp, new Mode());
                    _itemSubscriptions.Add(ItemType.FireRod, new Mode());

                    break;
                case LocationID.PalaceOfDarkness:

                    _defaultBoss = _game.Bosses[BossType.HelmasaurKing];

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel pD = _game.Regions[RegionID.PalaceOfDarkness].Accessibility;
                        
                        if (_game.Items.Has(ItemType.Hammer) && _game.Items.CanShootArrows() &&
                            _game.Items.Has(ItemType.PoDBigKey) && _game.Items.Has(ItemType.PoDSmallKey))
                        {
                            if (_game.Items.Has(ItemType.Lamp))
                                return pD;
                            
                            return AccessibilityLevel.SequenceBreak;
                        }

                        return AccessibilityLevel.None;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 181, 8);

                        if (result.HasValue)
                        {
                            if (result.Value)
                                Available = 0;
                            else
                                Available = 1;
                        }
                    };

                    _game.AutoTracker.RoomMemory[181].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.PalaceOfDarkness, new Mode());

                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.Bow, new Mode());
                    _itemSubscriptions.Add(ItemType.PoDBigKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity });
                    _itemSubscriptions.Add(ItemType.PoDSmallKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys });
                    _itemSubscriptions.Add(ItemType.Lamp, new Mode());

                    break;
                case LocationID.SwampPalace:

                    _defaultBoss = _game.Bosses[BossType.Arrghus];

                    GetAccessibility = () =>
                    {
                        if (_game.Items.Has(ItemType.Flippers) && _game.Items.Has(ItemType.Hookshot) &&
                            _game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.SPSmallKey))
                            return _game.Regions[RegionID.SwampPalace].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 13, 8);

                        if (result.HasValue)
                        {
                            if (result.Value)
                                Available = 0;
                            else
                                Available = 1;
                        }
                    };

                    _game.AutoTracker.RoomMemory[13].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.SwampPalace, new Mode());

                    _itemSubscriptions.Add(ItemType.Flippers, new Mode());
                    _itemSubscriptions.Add(ItemType.Hookshot, new Mode());
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.SPSmallKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys });

                    break;
                case LocationID.SkullWoods:

                    _defaultBoss = _game.Bosses[BossType.Mothula];

                    GetAccessibility = () =>
                    {
                        if ((_game.Items.Has(ItemType.FireRod) || _game.Mode.EntranceShuffle.Value) &&
                            _game.Items.CanRemoveCurtains())
                            return _game.Regions[RegionID.SkullWoods].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 83, 8);

                        if (result.HasValue)
                        {
                            if (result.Value)
                                Available = 0;
                            else
                                Available = 1;
                        }
                    };

                    _game.AutoTracker.RoomMemory[83].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.SkullWoods, new Mode());

                    _itemSubscriptions.Add(ItemType.FireRod, new Mode() { EntranceShuffle = false });
                    _itemSubscriptions.Add(ItemType.Sword, new Mode());

                    break;
                case LocationID.ThievesTown:

                    _defaultBoss = _game.Bosses[BossType.Blind];

                    GetAccessibility = () =>
                    {
                        if (_game.Items.Has(ItemType.TTBigKey))
                            return _game.Regions[RegionID.ThievesTown].Accessibility;
                        
                        return AccessibilityLevel.None;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 345, 8);

                        if (result.HasValue)
                        {
                            if (result.Value)
                                Available = 0;
                            else
                                Available = 1;
                        }
                    };

                    _game.AutoTracker.RoomMemory[345].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.ThievesTown, new Mode());

                    _itemSubscriptions.Add(ItemType.TTBigKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity });

                    break;
                case LocationID.IcePalace:

                    _defaultBoss = _game.Bosses[BossType.Kholdstare];

                    GetAccessibility = () =>
                    {
                        if (_game.Items.CanMeltThings() && _game.Items.Has(ItemType.Hammer) &&
                            (_game.Items.Has(ItemType.CaneOfSomaria) || _game.Items.Has(ItemType.IPSmallKey)))
                            return _game.Regions[RegionID.IcePalace].Accessibility;
                        
                        return AccessibilityLevel.None;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 445, 8);

                        if (result.HasValue)
                        {
                            if (result.Value)
                                Available = 0;
                            else
                                Available = 1;
                        }
                    };

                    _game.AutoTracker.RoomMemory[445].PropertyChanged += OnMemoryChanged;

                    _regionSubscriptions.Add(RegionID.IcePalace, new Mode());

                    _itemSubscriptions.Add(ItemType.FireRod, new Mode());
                    _itemSubscriptions.Add(ItemType.Bombos, new Mode());
                    _itemSubscriptions.Add(ItemType.Sword, new Mode());
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.CaneOfSomaria, new Mode());
                    _itemSubscriptions.Add(ItemType.IPSmallKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys });

                    break;
                case LocationID.MiseryMire:

                    _defaultBoss = _game.Bosses[BossType.Vitreous];

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel mM = _game.Regions[RegionID.MiseryMire].Accessibility;
                        
                        if ((_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots)) &&
                            _game.Items.Has(ItemType.CaneOfSomaria) && _game.Items.Has(ItemType.MMBigKey))
                        {
                            if ((_game.Items.Has(ItemType.Hookshot) || (_game.Items.Has(ItemType.Boots) &&
                                _game.Mode.ItemPlacement == ItemPlacement.Advanced)) && _game.Items.Has(ItemType.Lamp))
                                return mM;
                            
                            return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)mM);
                        }

                        return AccessibilityLevel.None;
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 289, 8);

                        if (result.HasValue)
                        {
                            if (result.Value)
                                Available = 0;
                            else
                                Available = 1;
                        }
                    };

                    _game.AutoTracker.RoomMemory[289].PropertyChanged += OnMemoryChanged;

                    _updateOnItemPlacementChange = true;

                    _regionSubscriptions.Add(RegionID.MiseryMire, new Mode());

                    _itemSubscriptions.Add(ItemType.Hookshot, new Mode());
                    _itemSubscriptions.Add(ItemType.Boots, new Mode());
                    _itemSubscriptions.Add(ItemType.CaneOfSomaria, new Mode());
                    _itemSubscriptions.Add(ItemType.MMBigKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity });
                    _itemSubscriptions.Add(ItemType.Lamp, new Mode());

                    break;
                case LocationID.TurtleRock:

                    _defaultBoss = _game.Bosses[BossType.Trinexx];

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel frontAccess = _game.Regions[RegionID.TurtleRockFront].Accessibility;
                        AccessibilityLevel backAccess = _game.Regions[RegionID.TurtleRockBack].Accessibility;

                        AccessibilityLevel front = AccessibilityLevel.None;
                        AccessibilityLevel backFront = AccessibilityLevel.None;
                        AccessibilityLevel back = AccessibilityLevel.None;

                        switch (_game.Mode.DungeonItemShuffle.Value)
                        {
                            case DungeonItemShuffle.Standard:
                            case DungeonItemShuffle.MapsCompasses:

                                if (_game.Items.Has(ItemType.CaneOfSomaria))
                                {
                                    front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)frontAccess);
                                    back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)backAccess);

                                    if (_game.Items.Has(ItemType.FireRod) && _game.Items.Has(ItemType.Lamp))
                                    {
                                        front = frontAccess;
                                        back = backAccess;
                                    }
                                }

                                break;
                            case DungeonItemShuffle.MapsCompassesSmallKeys:

                                if (_game.Items.Has(ItemType.CaneOfSomaria))
                                {
                                    if (_game.Items.Has(ItemType.TRSmallKey))
                                    {
                                        backFront = (AccessibilityLevel)Math.Min(Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)frontAccess), (byte)backAccess);
                                        back = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)backAccess);

                                        if (_game.Items.Has(ItemType.FireRod))
                                            backFront = (AccessibilityLevel)Math.Min((byte)frontAccess, (byte)backAccess);
                                    }

                                    if (_game.Items.Has(ItemType.FireRod) && _game.Items.Has(ItemType.TRSmallKey, 2))
                                        back = backAccess;

                                    if (_game.Items.Has(ItemType.FireRod) && _game.Items.Has(ItemType.TRSmallKey, 4))
                                    {
                                        front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)frontAccess);

                                        if (_game.Items.Has(ItemType.Lamp))
                                            front = frontAccess;
                                    }
                                }

                                break;
                            case DungeonItemShuffle.Keysanity:

                                if (_game.Items.Has(ItemType.CaneOfSomaria) && _game.Items.Has(ItemType.TRBigKey))
                                {
                                    if (_game.Items.Has(ItemType.TRSmallKey))
                                        back = backAccess;

                                    if (_game.Items.Has(ItemType.TRSmallKey, 4))
                                    {
                                        front = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)frontAccess);

                                        if (_game.Items.Has(ItemType.Lamp))
                                            front = frontAccess;
                                    }
                                }

                                break;
                        }

                        return (AccessibilityLevel)Math.Max(Math.Max((byte)front, (byte)backFront), (byte)back);
                    };

                    AutoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 329, 8);

                        if (result.HasValue)
                        {
                            if (result.Value)
                                Available = 0;
                            else
                                Available = 1;
                        }
                    };

                    _game.AutoTracker.RoomMemory[329].PropertyChanged += OnMemoryChanged;

                    _updateOnWorldStateChange = true;

                    _regionSubscriptions.Add(RegionID.TurtleRockFront, new Mode());
                    _regionSubscriptions.Add(RegionID.TurtleRockBack, new Mode());

                    _itemSubscriptions.Add(ItemType.CaneOfSomaria, new Mode());
                    _itemSubscriptions.Add(ItemType.TRBigKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity });
                    _itemSubscriptions.Add(ItemType.TRSmallKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys });
                    _itemSubscriptions.Add(ItemType.Lamp, new Mode());
                    _itemSubscriptions.Add(ItemType.FireRod, new Mode());

                    break;

                case LocationID.GanonsTower:

                    _defaultBoss = _game.Bosses[BossType.Aga];
                    _boss = _game.Bosses[BossType.Aga];
                    Prize = _game.Items[ItemType.Aga2];

                    GetAccessibility = () =>
                    {
                        AccessibilityLevel gT = _game.Regions[RegionID.GanonsTower].Accessibility;

                        if (_game.Mode.DungeonItemShuffle >= DungeonItemShuffle.MapsCompassesSmallKeys)
                        {
                            if (_game.Items.CanClearRedEyegoreGoriyaRooms() && _game.Items.Has(ItemType.GTBigKey))
                            {
                                if (_game.Items.Has(ItemType.GTSmallKey, 2))
                                    return gT;

                                if (_game.Items.Has(ItemType.GTSmallKey, 1))
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)gT);
                            }
                        }
                        else
                        {
                            if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                            {
                                if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Hookshot) &&
                                    _game.Items.Has(ItemType.Boots) && _game.Items.Has(ItemType.CaneOfSomaria) &&
                                    _game.Items.Has(ItemType.FireRod))
                                    return gT;

                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak, (byte)gT);
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    _updateOnEnemyShuffleChange = true;

                    _regionSubscriptions.Add(RegionID.GanonsTower, new Mode());

                    _itemSubscriptions.Add(ItemType.Bow, new Mode() { EnemyShuffle = false });
                    _itemSubscriptions.Add(ItemType.GTSmallKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys });
                    _itemSubscriptions.Add(ItemType.GTBigKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity });
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.Hookshot, new Mode());
                    _itemSubscriptions.Add(ItemType.Boots, new Mode());
                    _itemSubscriptions.Add(ItemType.CaneOfSomaria, new Mode());
                    _itemSubscriptions.Add(ItemType.FireRod, new Mode());

                    break;
            }

            _game.Bosses.PropertyChanged += OnRequirementChanged;
            _game.Mode.PropertyChanged += OnModeChanged;

            UpdateRegionSubscriptions();
            UpdateItemSubscriptions();
            UpdateBossSubscription();

            UpdateAccessibility();
        }

        private void OnPropertyChanging(string propertyName)
        {
            if (propertyName == nameof(Prize) && Prize != null && !IsAvailable())
                Prize.Change(-1, true);

            if (PropertyChanging != null)
                PropertyChanging.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (propertyName == nameof(Available) && Prize != null)
            {
                if (IsAvailable())
                    Prize.Change(-1, true);
                else
                    Prize.Change(1, true);
            }

            if (propertyName == nameof(Boss))
            {
                UpdateBossSubscription();
                UpdateAccessibility();
            }

            if (propertyName == nameof(Prize) && Prize != null && !IsAvailable())
                Prize.Change(1, true);

            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_game.Mode.ItemPlacement) && _updateOnItemPlacementChange)
                UpdateAccessibility();

            if (e.PropertyName == nameof(_game.Mode.DungeonItemShuffle))
                UpdateAccessibility();

            if (e.PropertyName == nameof(_game.Mode.WorldState) && _updateOnWorldStateChange)
                UpdateAccessibility();

            if (e.PropertyName == nameof(_game.Mode.EntranceShuffle))
                UpdateAccessibility();

            if (e.PropertyName == nameof(_game.Mode.BossShuffle))
            {
                UpdateBossSubscription();
                UpdateAccessibility();
            }

            if (e.PropertyName == nameof(_game.Mode.EnemyShuffle) && _updateOnEnemyShuffleChange)
                UpdateAccessibility();

            UpdateRegionSubscriptions();
            UpdateItemSubscriptions();
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

        private void UpdateBossSubscription()
        {
            if (_game.Mode.BossShuffle.Value)
            {
                if (Boss != _currentBossSubscription)
                {
                    if (_currentBossSubscription == null)
                        _game.Bosses.PropertyChanged -= OnRequirementChanged;
                    else
                        _currentBossSubscription.PropertyChanged -= OnRequirementChanged;

                    _currentBossSubscription = Boss;

                    if (_currentBossSubscription == null)
                        _game.Bosses.PropertyChanged += OnRequirementChanged;
                    else
                        _currentBossSubscription.PropertyChanged += OnRequirementChanged;
                }
            }
            else
            {
                if (_defaultBoss != _currentBossSubscription)
                {
                    if (_currentBossSubscription == null)
                        _game.Bosses.PropertyChanged -= OnRequirementChanged;
                    else
                        _currentBossSubscription.PropertyChanged -= OnRequirementChanged;

                    _currentBossSubscription = _defaultBoss;

                    if (_currentBossSubscription == null)
                        _game.Bosses.PropertyChanged += OnRequirementChanged;
                    else
                        _currentBossSubscription.PropertyChanged += OnRequirementChanged;
                }
            }
        }

        private void UpdateAccessibility()
        {
            AccessibilityLevel sectionAccessibility = GetAccessibility();
            AccessibilityLevel bossAccessibility = AccessibilityLevel.None;
            
            if (_game.Mode.BossShuffle.Value)
            {
                if (Boss == null)
                    bossAccessibility = _game.Bosses.UnknownBossAccessibility;
                else
                    bossAccessibility = Boss.Accessibility;
            }
            else
            {
                if (_defaultBoss != null)
                    bossAccessibility = _defaultBoss.Accessibility;
            }

            Accessibility = (AccessibilityLevel)Math.Min((byte)sectionAccessibility, (byte)bossAccessibility);
        }

        public void Clear()
        {
            Available = 0;
        }

        public bool IsAvailable()
        {
            return Available > 0;
        }

        public void Reset()
        {
            Available = 1;

            if (_defaultBoss != _game.Bosses[BossType.Aga])
            {
                Boss = null;
                Prize = null;
            }
            
        }
    }
}
