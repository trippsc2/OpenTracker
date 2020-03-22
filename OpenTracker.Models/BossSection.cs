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
        private readonly Region _standardRegion;
        private readonly Region _invertedRegion;

        public string Name { get => "Boss"; }

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
                    if (_boss != null)
                        _boss.PropertyChanged -= OnBossChanged;
                    _boss = value;
                    if (_boss != null)
                        _boss.PropertyChanged += OnBossChanged;
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
                    if (_prize != null && !Available)
                        _prize.Current--;
                    _prize = value;
                    if (_prize != null && !Available)
                        _prize.Current++;
                    OnPropertyChanged(nameof(Prize));
                }
            }
        }

        public Func<AccessibilityLevel> GetAccessibility { get; }

        public BossSection(Game game, LocationID iD)
        {
            _game = game;

            List<Item> itemReqs = new List<Item>();

            switch (iD)
            {
                case LocationID.Agahnim:

                    _defaultBoss = _game.Bosses[BossType.Aga];
                    _boss = _game.Bosses[BossType.Aga];
                    Prize = _game.Items[ItemType.Aga];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.DarkDeathMountainTop];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.CanClearAgaTowerBarrier() && _game.Items.Has(ItemType.ATSmallKey, 2) &&
                                _game.Items.CanRemoveCurtains())
                            {
                                if (_game.Items.Has(ItemType.Lamp))
                                    return AccessibilityLevel.Normal;

                                return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.ATSmallKey, 2) && _game.Items.CanRemoveCurtains())
                            {
                                if (_game.Items.Has(ItemType.Lamp))
                                    return AccessibilityLevel.Normal;
                                
                                return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Cape]);
                    itemReqs.Add(_game.Items[ItemType.Sword]);
                    itemReqs.Add(_game.Items[ItemType.Lamp]);
                    itemReqs.Add(_game.Items[ItemType.ATSmallKey]);

                    break;
                case LocationID.EasternPalace:

                    _defaultBoss = _game.Bosses[BossType.Armos];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.EPBigKey) &&
                                (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value))
                            {
                                if (_game.Items.Has(ItemType.Lamp) || (_game.Items.Has(ItemType.FireRod) &&
                                _game.Mode.ItemPlacement == ItemPlacement.Advanced))
                                    return AccessibilityLevel.Normal;

                                return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.EPBigKey) && _game.Items.Has(ItemType.MoonPearl) &&
                                (_game.Items.Has(ItemType.Bow) || _game.Mode.EnemyShuffle.Value))
                            {
                                if (_game.Items.Has(ItemType.Lamp) || (_game.Items.Has(ItemType.FireRod) &&
                                _game.Mode.ItemPlacement == ItemPlacement.Advanced))
                                    return AccessibilityLevel.Normal;

                                return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.EPBigKey]);
                    itemReqs.Add(_game.Items[ItemType.Bow]);
                    itemReqs.Add(_game.Items[ItemType.Lamp]);
                    itemReqs.Add(_game.Items[ItemType.FireRod]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.DesertPalace:

                    _defaultBoss = _game.Bosses[BossType.Lanmolas];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Book))
                            {
                                if (_game.Items.Has(ItemType.Gloves) && (_game.Items.Has(ItemType.FireRod) ||
                                    _game.Items.Has(ItemType.Lamp)) && _game.Items.Has(ItemType.DPBigKey))
                                    return AccessibilityLevel.Normal;
                            }

                            if (_game.Items.Has(ItemType.Mirror))
                            {
                                if (_game.Regions[RegionID.MireArea].Accessibility == AccessibilityLevel.Normal)
                                {
                                    if (_game.Items.Has(ItemType.Gloves) && (_game.Items.Has(ItemType.FireRod) ||
                                        _game.Items.Has(ItemType.Lamp)) && _game.Items.Has(ItemType.DPBigKey))
                                        return AccessibilityLevel.Normal;
                                }

                                if (_game.Regions[RegionID.MireArea].Accessibility == AccessibilityLevel.SequenceBreak)
                                    return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Book) && _game.Items.Has(ItemType.MoonPearl))
                            {
                                if (_game.Items.Has(ItemType.Gloves) && (_game.Items.Has(ItemType.FireRod) ||
                                    _game.Items.Has(ItemType.Lamp)) && _game.Items.Has(ItemType.DPBigKey))
                                    return AccessibilityLevel.Normal;
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Book]);
                    itemReqs.Add(_game.Items[ItemType.FireRod]);
                    itemReqs.Add(_game.Items[ItemType.Lamp]);
                    itemReqs.Add(_game.Items[ItemType.DPBigKey]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    _game.Regions[RegionID.MireArea].PropertyChanged += OnRegionChanged;

                    break;
                case LocationID.TowerOfHera:

                    _defaultBoss = _game.Bosses[BossType.Moldorm];
                    _standardRegion = _game.Regions[RegionID.DeathMountainWestTop];
                    _invertedRegion = _game.Regions[RegionID.DeathMountainWestTop];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.ToHBigKey))
                                return AccessibilityLevel.Normal;

                            if (_game.Items.Has(ItemType.Hookshot))
                                return AccessibilityLevel.SequenceBreak;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                            {
                                if (_game.Items.Has(ItemType.ToHBigKey))
                                    return AccessibilityLevel.Normal;

                                if (_game.Items.Has(ItemType.Hookshot))
                                    return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.ToHBigKey]);
                    itemReqs.Add(_game.Items[ItemType.Hookshot]);

                    break;
                case LocationID.PalaceOfDarkness:

                    _defaultBoss = _game.Bosses[BossType.HelmasaurKing];
                    _standardRegion = _game.Regions[RegionID.DarkWorldEast];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldEast];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                            {
                                if (_game.Items.Has(ItemType.PoDBigKey) && _game.Items.Has(ItemType.PoDSmallKey) &&
                                    _game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Bow))
                                {
                                    if (_game.Items.Has(ItemType.Lamp))
                                        return AccessibilityLevel.Normal;

                                    return AccessibilityLevel.SequenceBreak;
                                }
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.PoDBigKey) && _game.Items.Has(ItemType.PoDSmallKey) &&
                                _game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Bow))
                            {
                                if (_game.Items.Has(ItemType.Lamp))
                                    return AccessibilityLevel.Normal;

                                return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.PoDBigKey]);
                    itemReqs.Add(_game.Items[ItemType.PoDSmallKey]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.Bow]);
                    itemReqs.Add(_game.Items[ItemType.Lamp]);

                    break;
                case LocationID.SwampPalace:

                    _defaultBoss = _game.Bosses[BossType.Arrghus];

                    GetAccessibility = () =>
                    {
                        if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Flippers) &&
                            _game.Items.Has(ItemType.Mirror))
                        {
                            if (_game.Items.Has(ItemType.Hookshot) && _game.Items.Has(ItemType.Hammer) &&
                                _game.Items.Has(ItemType.SPSmallKey))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.SequenceBreak;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Flippers]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.Hookshot]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.SPSmallKey]);
                    _standardRegion = _game.Regions[RegionID.DarkWorldSouth];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    break;
                case LocationID.SkullWoods:

                    _defaultBoss = _game.Bosses[BossType.Mothula];
                    _standardRegion = _game.Regions[RegionID.DarkWorldWest];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldWest];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                            {
                                if (_game.Items.Has(ItemType.SWBigKey) && _game.Items.Has(ItemType.SWSmallKey) &&
                                    _game.Items.Has(ItemType.FireRod) && _game.Items.CanRemoveCurtains())
                                    return AccessibilityLevel.Normal;
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.SWBigKey) && _game.Items.Has(ItemType.SWSmallKey) &&
                                _game.Items.Has(ItemType.FireRod) && _game.Items.CanRemoveCurtains())
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.SWBigKey]);
                    itemReqs.Add(_game.Items[ItemType.SWSmallKey]);
                    itemReqs.Add(_game.Items[ItemType.FireRod]);
                    itemReqs.Add(_game.Items[ItemType.Sword]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);

                    break;
                case LocationID.ThievesTown:

                    _defaultBoss = _game.Bosses[BossType.Blind];
                    _standardRegion = _game.Regions[RegionID.DarkWorldWest];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldWest];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                            {
                                if (_game.Items.Has(ItemType.TTBigKey))
                                    return AccessibilityLevel.Normal;
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.TTBigKey))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.TTBigKey]);

                    break;
                case LocationID.IcePalace:

                    _defaultBoss = _game.Bosses[BossType.Kholdstare];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldSouth];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Gloves, 2) && _game.Items.Has(ItemType.MoonPearl) &&
                                _game.Items.CanMeltThings())
                            {
                                if (_game.Items.Has(ItemType.Hammer) && 
                                    (_game.Items.Has(ItemType.CaneOfSomaria) || _game.Items.Has(ItemType.IPSmallKey)) &&
                                    _game.Items.Has(ItemType.Flippers))
                                    return AccessibilityLevel.Normal;
                                
                                return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.CanMeltThings())
                            {
                                if (_game.Items.Has(ItemType.Hammer) &&
                                    (_game.Items.Has(ItemType.CaneOfSomaria) || _game.Items.Has(ItemType.IPSmallKey)) &&
                                    _game.Items.Has(ItemType.Flippers))
                                    return AccessibilityLevel.Normal;

                                return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.FireRod]);
                    itemReqs.Add(_game.Items[ItemType.Bombos]);
                    itemReqs.Add(_game.Items[ItemType.Sword]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.Flippers]);
                    itemReqs.Add(_game.Items[ItemType.IPSmallKey]);
                    itemReqs.Add(_game.Items[ItemType.CaneOfSomaria]);

                    break;
                case LocationID.MiseryMire:

                    _defaultBoss = _game.Bosses[BossType.Vitreous];
                    _standardRegion = _game.Regions[RegionID.MireArea];
                    _invertedRegion = _game.Regions[RegionID.MireArea];

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
                            {
                                if (_game.Items.Has(ItemType.CaneOfSomaria) && _game.Items.Has(ItemType.MMBigKey))
                                {
                                    if ((_game.Items.Has(ItemType.Hookshot) || (_game.Items.Has(ItemType.Boots) &&
                                        _game.Mode.ItemPlacement == ItemPlacement.Advanced)) &&
                                        _game.Items.Has(ItemType.Lamp))
                                        return AccessibilityLevel.Normal;

                                    if (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots))
                                        return AccessibilityLevel.SequenceBreak;
                                }
                            }
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
                            {
                                if (_game.Items.Has(ItemType.CaneOfSomaria) && _game.Items.Has(ItemType.MMBigKey) &&
                                    (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Boots)))
                                {
                                    if (_game.Items.Has(ItemType.Lamp))
                                        return AccessibilityLevel.Normal;

                                    return AccessibilityLevel.SequenceBreak;
                                }
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Sword]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.Bombos]);
                    itemReqs.Add(_game.Items[ItemType.BombosDungeons]);
                    itemReqs.Add(_game.Items[ItemType.Ether]);
                    itemReqs.Add(_game.Items[ItemType.EtherDungeons]);
                    itemReqs.Add(_game.Items[ItemType.Quake]);
                    itemReqs.Add(_game.Items[ItemType.QuakeDungeons]);
                    itemReqs.Add(_game.Items[ItemType.CaneOfSomaria]);
                    itemReqs.Add(_game.Items[ItemType.MMBigKey]);
                    itemReqs.Add(_game.Items[ItemType.Hookshot]);
                    itemReqs.Add(_game.Items[ItemType.Boots]);
                    itemReqs.Add(_game.Items[ItemType.Lamp]);

                    break;
                case LocationID.TurtleRock:

                    _defaultBoss = _game.Bosses[BossType.Trinexx];
                    _standardRegion = _game.Regions[RegionID.DarkDeathMountainTop];
                    _invertedRegion = _game.Regions[RegionID.DarkDeathMountainTop];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Hammer) &&
                                _game.Items.CanUseMedallions() && _game.Items.Has(ItemType.CaneOfSomaria) &&
                                ((_game.Items.Has(ItemType.Bombos) && _game.Items.Has(ItemType.Ether) &&
                                _game.Items.Has(ItemType.Quake)) ||
                                (_game.Items.Has(ItemType.Bombos) &&
                                _game.Items[ItemType.BombosDungeons].Current >= 2) ||
                                (_game.Items.Has(ItemType.Ether) &&
                                _game.Items[ItemType.EtherDungeons].Current >= 2) ||
                                (_game.Items.Has(ItemType.Quake) &&
                                _game.Items[ItemType.QuakeDungeons].Current >= 2)))
                            {
                                if (_game.Items.Has(ItemType.FireRod) && _game.Items.Has(ItemType.TRBigKey) &&
                                    _game.Items.Has(ItemType.TRSmallKey, 3))
                                {
                                    if (_game.Items.Has(ItemType.TRSmallKey, 4) && _game.Items.Has(ItemType.Lamp))
                                        return AccessibilityLevel.Normal;

                                    return AccessibilityLevel.SequenceBreak;
                                }
                            }
                        }
                        
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.CaneOfSomaria))
                            {
                                if (_game.Regions[RegionID.DeathMountainEastTop].Accessibility == AccessibilityLevel.Normal &&
                                    _game.Items.Has(ItemType.Mirror))
                                {
                                    if (_game.Items.Has(ItemType.TRBigKey) && _game.Items.Has(ItemType.TRSmallKey))
                                        return AccessibilityLevel.Normal;
                                }

                                if (_game.Items.CanUseMedallions() && ((_game.Items.Has(ItemType.Bombos) &&
                                    _game.Items.Has(ItemType.Ether) && _game.Items.Has(ItemType.Quake)) ||
                                    (_game.Items.Has(ItemType.Bombos) &&
                                    _game.Items[ItemType.BombosDungeons].Current >= 2) ||
                                    (_game.Items.Has(ItemType.Ether) &&
                                    _game.Items[ItemType.EtherDungeons].Current >= 2) ||
                                    (_game.Items.Has(ItemType.Quake) &&
                                    _game.Items[ItemType.QuakeDungeons].Current >= 2)))
                                {
                                    if (_game.Items.Has(ItemType.FireRod) && _game.Items.Has(ItemType.TRBigKey) &&
                                        _game.Items.Has(ItemType.TRSmallKey, 3))
                                    {
                                        if (_game.Items.Has(ItemType.TRSmallKey, 4) && _game.Items.Has(ItemType.Lamp))
                                            return AccessibilityLevel.Normal;

                                        return AccessibilityLevel.SequenceBreak;
                                    }
                                }
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.Sword]);
                    itemReqs.Add(_game.Items[ItemType.CaneOfSomaria]);
                    itemReqs.Add(_game.Items[ItemType.Bombos]);
                    itemReqs.Add(_game.Items[ItemType.BombosDungeons]);
                    itemReqs.Add(_game.Items[ItemType.Ether]);
                    itemReqs.Add(_game.Items[ItemType.EtherDungeons]);
                    itemReqs.Add(_game.Items[ItemType.Quake]);
                    itemReqs.Add(_game.Items[ItemType.QuakeDungeons]);
                    itemReqs.Add(_game.Items[ItemType.FireRod]);
                    itemReqs.Add(_game.Items[ItemType.TRBigKey]);
                    itemReqs.Add(_game.Items[ItemType.TRSmallKey]);
                    itemReqs.Add(_game.Items[ItemType.Lamp]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);

                    _game.Regions[RegionID.DeathMountainEastTop].PropertyChanged += OnRegionChanged;

                    break;
            }

            foreach (Item item in itemReqs)
                item.PropertyChanged += OnItemRequirementChanged;

            _game.Mode.PropertyChanged += OnModeChanged;
            _defaultBoss.PropertyChanged += OnBossChanged;

            if (_standardRegion != null)
                _standardRegion.PropertyChanged += OnRegionChanged;

            if (_invertedRegion != null)
                _invertedRegion.PropertyChanged += OnRegionChanged;

            Available = true;

            UpdateAccessibility();
        }

        private void UpdateAccessibility()
        {
            AccessibilityLevel? regionAccessibility = null;
            AccessibilityLevel sectionAccessibility = GetAccessibility();
            AccessibilityLevel bossAccessibility = AccessibilityLevel.None;

            if (!_game.Mode.EntranceShuffle.Value)
            {
                if (_game.Mode.WorldState == WorldState.StandardOpen && _standardRegion != null)
                    regionAccessibility = _standardRegion.Accessibility;
                if (_game.Mode.WorldState == WorldState.Inverted && _invertedRegion != null)
                    regionAccessibility = _invertedRegion.Accessibility;
            }

            if (_game.Mode.BossShuffle.Value)
            {
                if (Boss != null)
                    bossAccessibility = Boss.Accessibility;
            }
            else
            {
                if (_defaultBoss != null)
                    bossAccessibility = _defaultBoss.Accessibility;
            }

            if (regionAccessibility != null)
                Accessibility = (AccessibilityLevel)Math.Min(Math.Min((byte)regionAccessibility.Value,
                    (byte)sectionAccessibility), (byte)bossAccessibility);
            else
                Accessibility = (AccessibilityLevel)Math.Min((byte)sectionAccessibility, (byte)bossAccessibility);
        }

        private void OnRegionChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateAccessibility();
        }

        private void OnBossChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateAccessibility();
        }

        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateAccessibility();
        }

        private void OnItemRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateAccessibility();
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (propertyName == nameof(Available) && Prize != null)
            {
                if (Available)
                    Prize.Current--;
                else
                    Prize.Current++;
            }

            if (propertyName == nameof(Boss))
                UpdateAccessibility();
        }

        public void Clear()
        {
            Available = false;
        }

        public bool IsAvailable()
        {
            return Available;
        }
    }
}
