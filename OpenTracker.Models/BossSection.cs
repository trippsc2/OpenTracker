using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models
{
    public class BossSection : ISection, INotifyPropertyChanging
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
        public MarkingType? Marking { get => null; set { } }

        public Func<AccessibilityLevel> GetAccessibility { get; }

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
            Available = true;

            switch (iD)
            {
                case LocationID.AgahnimTower:

                    _defaultBoss = _game.Bosses[BossType.Aga];
                    _boss = _game.Bosses[BossType.Aga];
                    Prize = _game.Items[ItemType.Aga];

                    GetAccessibility = () =>
                    {
                        if (_game.Regions[RegionID.Agahnim].Accessibility >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Items.Has(ItemType.ATSmallKey, 2) && _game.Items.CanRemoveCurtains())
                            {
                                if (_game.Items.Has(ItemType.Lamp))
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Normal,
                                        (byte)_game.Regions[RegionID.Agahnim].Accessibility);
                                }

                                return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.Agahnim, new Mode());

                    _itemSubscriptions.Add(ItemType.ATSmallKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys });
                    _itemSubscriptions.Add(ItemType.Sword, new Mode());
                    _itemSubscriptions.Add(ItemType.Lamp, new Mode());

                    break;
                case LocationID.EasternPalace:

                    _defaultBoss = _game.Bosses[BossType.Armos];

                    GetAccessibility = () =>
                    {
                        if (_game.Regions[RegionID.EasternPalace].Accessibility >= AccessibilityLevel.SequenceBreak)
                        {
                            if ((_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value) &&
                                _game.Items.Has(ItemType.EPBigKey))
                            {
                                if (_game.Items.Has(ItemType.Lamp) || (_game.Items.Has(ItemType.FireRod) &&
                                    _game.Mode.ItemPlacement == ItemPlacement.Advanced))
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Normal,
                                        (byte)_game.Regions[RegionID.EasternPalace].Accessibility);
                                }

                                return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        return AccessibilityLevel.None;
                    };

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
                        if (_game.Regions[RegionID.DesertPalace].Accessibility >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Items.Has(ItemType.Gloves) && (_game.Items.Has(ItemType.FireRod) ||
                                _game.Items.Has(ItemType.Lamp)) && _game.Items.Has(ItemType.DPBigKey))
                            {
                                if (_game.Items.Has(ItemType.DPSmallKey))
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Normal,
                                        (byte)_game.Regions[RegionID.DesertPalace].Accessibility);
                                }

                                return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DesertPalace, new Mode());

                    _itemSubscriptions.Add(ItemType.Gloves, new Mode());
                    _itemSubscriptions.Add(ItemType.FireRod, new Mode());
                    _itemSubscriptions.Add(ItemType.Lamp, new Mode());
                    _itemSubscriptions.Add(ItemType.DPBigKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity });
                    _itemSubscriptions.Add(ItemType.DPSmallKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys });

                    break;
                case LocationID.TowerOfHera:

                    _defaultBoss = _game.Bosses[BossType.Moldorm];

                    GetAccessibility = () =>
                    {
                        if (_game.Regions[RegionID.TowerOfHera].Accessibility >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Mode.DungeonItemShuffle == DungeonItemShuffle.Keysanity)
                            {
                                if (_game.Items.Has(ItemType.ToHBigKey))
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Normal,
                                        (byte)_game.Regions[RegionID.TowerOfHera].Accessibility);
                                }

                                if (_game.Items.Has(ItemType.Hookshot))
                                    return AccessibilityLevel.SequenceBreak;
                            }
                            else
                            {
                                if (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod))
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Normal,
                                        (byte)_game.Regions[RegionID.TowerOfHera].Accessibility);
                                }

                                return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        return AccessibilityLevel.None;
                    };

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
                        if (_game.Regions[RegionID.PalaceOfDarkness].Accessibility >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Bow) &&
                                _game.Items.Has(ItemType.PoDBigKey) && _game.Items.Has(ItemType.PoDSmallKey))
                            {
                                if (_game.Items.Has(ItemType.Lamp))
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Normal,
                                        (byte)_game.Regions[RegionID.PalaceOfDarkness].Accessibility);
                                }

                                return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        return AccessibilityLevel.None;
                    };

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
                        if (_game.Regions[RegionID.SwampPalace].Accessibility >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Items.Has(ItemType.Flippers) && _game.Items.Has(ItemType.Hookshot) &&
                                _game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.SPSmallKey))
                            {
                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Normal,
                                    (byte)_game.Regions[RegionID.SwampPalace].Accessibility);
                            }
                        }

                        return AccessibilityLevel.None;
                    };

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
                        if (_game.Regions[RegionID.SkullWoods].Accessibility >= AccessibilityLevel.SequenceBreak)
                        {
                            if ((_game.Items.Has(ItemType.FireRod) || _game.Mode.EntranceShuffle.Value) &&
                                _game.Items.CanRemoveCurtains())
                            {
                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Normal,
                                    (byte)_game.Regions[RegionID.SkullWoods].Accessibility);
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.SkullWoods, new Mode());

                    _itemSubscriptions.Add(ItemType.FireRod, new Mode() { EntranceShuffle = false });
                    _itemSubscriptions.Add(ItemType.Sword, new Mode());

                    break;
                case LocationID.ThievesTown:

                    _defaultBoss = _game.Bosses[BossType.Blind];

                    GetAccessibility = () =>
                    {
                        if (_game.Regions[RegionID.ThievesTown].Accessibility >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Items.Has(ItemType.TTBigKey))
                            {
                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Normal,
                                    (byte)_game.Regions[RegionID.ThievesTown].Accessibility);
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.ThievesTown, new Mode());

                    _itemSubscriptions.Add(ItemType.TTBigKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity });

                    break;
                case LocationID.IcePalace:

                    _defaultBoss = _game.Bosses[BossType.Kholdstare];

                    GetAccessibility = () =>
                    {
                        if (_game.Regions[RegionID.IcePalace].Accessibility >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Items.CanMeltThings() && _game.Items.Has(ItemType.Hammer) &&
                                (_game.Items.Has(ItemType.CaneOfSomaria) || _game.Items.Has(ItemType.IPSmallKey)))
                            {
                                return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Normal,
                                    (byte)_game.Regions[RegionID.IcePalace].Accessibility);
                            }
                        }
                        
                        return AccessibilityLevel.None;
                    };

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
                        if (_game.Regions[RegionID.MiseryMire].Accessibility >= AccessibilityLevel.SequenceBreak)
                        {
                            if ((_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots)) &&
                                _game.Items.Has(ItemType.CaneOfSomaria) && _game.Items.Has(ItemType.MMBigKey))
                            {
                                if ((_game.Items.Has(ItemType.Hookshot) || (_game.Items.Has(ItemType.Boots) &&
                                    _game.Mode.ItemPlacement == ItemPlacement.Advanced)) && _game.Items.Has(ItemType.Lamp))
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Normal,
                                        (byte)_game.Regions[RegionID.MiseryMire].Accessibility);
                                }

                                return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        return AccessibilityLevel.None;
                    };

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
                        if (_game.Mode.EntranceShuffle.Value)
                        {
                            if (_game.Items.Has(ItemType.CaneOfSomaria) && _game.Items.Has(ItemType.TRBigKey)
                                && _game.Items.Has(ItemType.TRSmallKey))
                                return AccessibilityLevel.Normal;
                        }

                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Regions[RegionID.TurtleRock].Accessibility >= AccessibilityLevel.SequenceBreak)
                            {
                                if (_game.Items.Has(ItemType.CaneOfSomaria) && _game.Items.Has(ItemType.TRBigKey) &&
                                    _game.Items.Has(ItemType.TRSmallKey, 3))
                                {
                                    if (_game.Items.Has(ItemType.Lamp))
                                    {
                                        if (_game.Mode.DungeonItemShuffle < DungeonItemShuffle.MapsCompassesSmallKeys &&
                                            _game.Items.Has(ItemType.FireRod))
                                            return AccessibilityLevel.Normal;
                                    }

                                    return AccessibilityLevel.SequenceBreak;
                                }
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Regions[RegionID.DeathMountainEastTop].Accessibility == AccessibilityLevel.Normal &&
                                _game.Items.Has(ItemType.Mirror))
                            {
                                if (_game.Items.Has(ItemType.TRBigKey) && _game.Items.Has(ItemType.TRSmallKey))
                                    return AccessibilityLevel.Normal;

                                return AccessibilityLevel.SequenceBreak;
                            }

                            if (_game.Items.CanUseMedallions() && _game.Items.Has(ItemType.CaneOfSomaria) &&
                                ((_game.Items.Has(ItemType.Bombos) && _game.Items.Has(ItemType.Ether) &&
                                _game.Items.Has(ItemType.Quake)) ||
                                (_game.Items.Has(ItemType.Bombos) &&
                                _game.Items[ItemType.BombosDungeons].Current >= 2) ||
                                (_game.Items.Has(ItemType.Ether) &&
                                _game.Items[ItemType.EtherDungeons].Current >= 2) ||
                                (_game.Items.Has(ItemType.Quake) &&
                                _game.Items[ItemType.QuakeDungeons].Current >= 2)) &&
                                _game.Regions[RegionID.DarkDeathMountainTop].Accessibility >= AccessibilityLevel.SequenceBreak)
                            {
                                if (_game.Items.Has(ItemType.TRBigKey) && _game.Items.Has(ItemType.TRSmallKey, 3) &&
                                    _game.Items.Has(ItemType.FireRod) && _game.Items.Has(ItemType.Lamp))
                                    return _game.Regions[RegionID.DarkDeathMountainTop].Accessibility;

                                return AccessibilityLevel.SequenceBreak;
                            }

                            if (_game.Regions[RegionID.DeathMountainEastTop].Accessibility == AccessibilityLevel.SequenceBreak &&
                                _game.Items.Has(ItemType.Mirror) && _game.Items.Has(ItemType.TRBigKey) &&
                                _game.Items.Has(ItemType.TRSmallKey))
                                return AccessibilityLevel.SequenceBreak;
                        }

                        return AccessibilityLevel.None;
                    };

                    _updateOnWorldStateChange = true;

                    _regionSubscriptions.Add(RegionID.TurtleRock, new Mode());
                    _regionSubscriptions.Add(RegionID.DeathMountainEastTop, new Mode() { WorldState = WorldState.Inverted });
                    _regionSubscriptions.Add(RegionID.DarkDeathMountainTop, new Mode() { WorldState = WorldState.Inverted });

                    _itemSubscriptions.Add(ItemType.CaneOfSomaria, new Mode());
                    _itemSubscriptions.Add(ItemType.TRBigKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity });
                    _itemSubscriptions.Add(ItemType.TRSmallKey, new Mode() { DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys });
                    _itemSubscriptions.Add(ItemType.Lamp, new Mode());
                    _itemSubscriptions.Add(ItemType.FireRod, new Mode());
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.Sword, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.Bombos, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.BombosDungeons, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.Ether, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.EtherDungeons, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.Quake, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.QuakeDungeons, new Mode() { WorldState = WorldState.Inverted });

                    break;

                case LocationID.GanonsTower:

                    _defaultBoss = _game.Bosses[BossType.Aga];
                    _boss = _game.Bosses[BossType.Aga];
                    Prize = _game.Items[ItemType.Aga2];

                    GetAccessibility = () =>
                    {
                        if (_game.Regions[RegionID.GanonsTower].Accessibility >= AccessibilityLevel.SequenceBreak)
                        {
                            if (_game.Mode.DungeonItemShuffle == DungeonItemShuffle.Keysanity)
                            {
                                if ((_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value) &&
                                    _game.Items.Has(ItemType.GTSmallKey) && _game.Items.Has(ItemType.GTBigKey))
                                {
                                    return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Normal,
                                        (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                }
                            }
                            else
                            {
                                if (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value)
                                {
                                    if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Hookshot) &&
                                        _game.Items.Has(ItemType.Boots) && _game.Items.Has(ItemType.CaneOfSomaria) &&
                                        _game.Items.Has(ItemType.FireRod) && _game.Items.Has(ItemType.GTSmallKey, 2))
                                    {
                                        return (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.Normal,
                                            (byte)_game.Regions[RegionID.GanonsTower].Accessibility);
                                    }

                                    return AccessibilityLevel.SequenceBreak;
                                }
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
            if (propertyName == nameof(Prize) && Prize != null && !Available)
                Prize.Change(-1, true);

            if (PropertyChanging != null)
                PropertyChanging.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (propertyName == nameof(Available) && Prize != null)
            {
                if (Available)
                    Prize.Change(-1, true);
                else
                    Prize.Change(1, true);
            }

            if (propertyName == nameof(Boss))
            {
                UpdateBossSubscription();
                UpdateAccessibility();
            }

            if (propertyName == nameof(Prize) && Prize != null && !Available)
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
            Available = false;
        }

        public bool IsAvailable()
        {
            return Available;
        }

        public void Reset()
        {
            Available = true;

            if (_defaultBoss != _game.Bosses[BossType.Aga])
            {
                Boss = null;
                Prize = null;
            }
            
        }
    }
}
