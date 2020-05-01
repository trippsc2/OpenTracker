using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models
{
    public class TakeAnySection : ISection
    {
        private readonly Game _game;
        private readonly Dictionary<RegionID, Mode> _regionSubscriptions;
        private readonly Dictionary<RegionID, bool> _regionIsSubscribed;
        private readonly Dictionary<ItemType, Mode> _itemSubscriptions;
        private readonly Dictionary<ItemType, bool> _itemIsSubscribed;

        public bool HasMarking => false;
        public string Name => "Take Any";
        public Mode RequiredMode { get; }
        public bool UserManipulated { get; set; }

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
                    OnPropertyChanging(nameof(Marking));
                    _marking = value;
                    OnPropertyChanged(nameof(Marking));
                }
            }
        }

        public TakeAnySection(Game game, LocationID iD)
        {
            _game = game;

            _regionSubscriptions = new Dictionary<RegionID, Mode>();
            _regionIsSubscribed = new Dictionary<RegionID, bool>();
            _itemSubscriptions = new Dictionary<ItemType, Mode>();
            _itemIsSubscribed = new Dictionary<ItemType, bool>();

            RequiredMode = new Mode();
            Available = 1;

            List<Item> itemReqs = new List<Item>();

            switch (iD)
            {
                case LocationID.TreesFairyCaveTakeAny:

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode() { WorldState = WorldState.Retro });

                    break;
                case LocationID.PegsFairyCaveTakeAny:

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode() { WorldState = WorldState.Retro });

                    break;
                case LocationID.KakarikoFortuneTellerTakeAny:

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode() { WorldState = WorldState.Retro });

                    break;
                case LocationID.GrassHouseTakeAny:

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode() { WorldState = WorldState.Retro });

                    break;
                case LocationID.ForestChestGameTakeAny:

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode() { WorldState = WorldState.Retro });

                    break;
                case LocationID.LumberjackHouseTakeAny:

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode() { WorldState = WorldState.Retro });

                    break;
                case LocationID.LeftSnitchHouseTakeAny:

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode() { WorldState = WorldState.Retro });

                    break;
                case LocationID.RightSnitchHouseTakeAny:

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode() { WorldState = WorldState.Retro });

                    break;
                case LocationID.BombHutTakeAny:

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode() { WorldState = WorldState.Retro });

                    break;
                case LocationID.IceFairyCaveTakeAny:

                    GetAccessibility = () =>
                    {
                        if (_game.Items.Has(ItemType.Gloves))
                            return _game.Regions[RegionID.LightWorld].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode() { WorldState = WorldState.Retro });

                    _itemSubscriptions.Add(ItemType.Gloves, new Mode() { WorldState = WorldState.Retro });

                    break;
                case LocationID.RupeeCaveTakeAny:

                    GetAccessibility = () =>
                    {
                        if (_game.Items.Has(ItemType.Gloves))
                            return _game.Regions[RegionID.LightWorld].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode() { WorldState = WorldState.Retro });

                    _itemSubscriptions.Add(ItemType.Gloves, new Mode() { WorldState = WorldState.Retro });

                    break;
                case LocationID.CentralBonkRocksTakeAny:

                    GetAccessibility = () =>
                    {
                        if (_game.Items.Has(ItemType.Boots))
                            return AccessibilityLevel.Normal;

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode() { WorldState = WorldState.Retro });

                    _itemSubscriptions.Add(ItemType.Boots, new Mode() { WorldState = WorldState.Retro });

                    break;
                case LocationID.ThiefCaveTakeAny:

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode() { WorldState = WorldState.Retro });

                    break;
                case LocationID.IceBeeCaveTakeAny:

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode() { WorldState = WorldState.Retro });

                    break;
                case LocationID.FortuneTellerTakeAny:

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode() { WorldState = WorldState.Retro });

                    break;
                case LocationID.HypeFairyCaveTakeAny:

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode() { WorldState = WorldState.Retro });

                    break;
                case LocationID.ChestGameTakeAny:

                    GetAccessibility = () => { return _game.Regions[RegionID.LightWorld].Accessibility; };

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode() { WorldState = WorldState.Retro });

                    break;
                case LocationID.EDMFairyCaveTakeAny:

                    GetAccessibility = () => { return _game.Regions[RegionID.DeathMountainEastBottom].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DeathMountainEastBottom, new Mode() { WorldState = WorldState.Retro });

                    break;
                case LocationID.DarkChapelTakeAny:

                    GetAccessibility = () => { return _game.Regions[RegionID.DarkWorldWest].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DarkWorldWest, new Mode() { WorldState = WorldState.Retro });

                    break;
                case LocationID.DarkVillageFortuneTellerTakeAny:

                    GetAccessibility = () => { return _game.Regions[RegionID.DarkWorldWest].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DarkWorldWest, new Mode() { WorldState = WorldState.Retro });

                    break;
                case LocationID.DarkTreesFairyCaveTakeAny:

                    GetAccessibility = () => { return _game.Regions[RegionID.DarkWorldEast].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DarkWorldEast, new Mode() { WorldState = WorldState.Retro });

                    break;
                case LocationID.DarkSahasrahlaTakeAny:

                    GetAccessibility = () => { return _game.Regions[RegionID.DarkWorldEast].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DarkWorldEast, new Mode() { WorldState = WorldState.Retro });

                    break;
                case LocationID.DarkFluteSpotFiveTakeAny:

                    GetAccessibility = () => { return _game.Regions[RegionID.DarkWorldEast].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DarkWorldEast, new Mode() { WorldState = WorldState.Retro });

                    break;
                case LocationID.ArrowGameTakeAny:

                    GetAccessibility = () => { return _game.Regions[RegionID.DarkWorldSouth].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DarkWorldSouth, new Mode() { WorldState = WorldState.Retro });

                    break;
                case LocationID.DarkCentralBonkRocksTakeAny:

                    GetAccessibility = () =>
                    {
                        if (_game.Items.Has(ItemType.Boots) && _game.Items.Has(ItemType.MoonPearl))
                            return _game.Regions[RegionID.DarkWorldSouth].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DarkWorldSouth, new Mode() { WorldState = WorldState.Retro });

                    _itemSubscriptions.Add(ItemType.Boots, new Mode() { WorldState = WorldState.Retro });
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Retro });

                    break;
                case LocationID.DarkIceRodCaveTakeAny:

                    GetAccessibility = () =>
                    {
                        if (_game.Items.Has(ItemType.MoonPearl))
                            return _game.Regions[RegionID.DarkWorldSouthEast].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DarkWorldSouthEast, new Mode() { WorldState = WorldState.Retro });

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Retro });

                    break;
                case LocationID.DarkFakeIceRodCaveTakeAny:

                    GetAccessibility = () => { return _game.Regions[RegionID.DarkWorldSouthEast].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DarkWorldSouthEast, new Mode() { WorldState = WorldState.Retro });

                    break;
                case LocationID.DarkIceRodRockTakeAny:

                    GetAccessibility = () =>
                    {
                        if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Gloves))
                            return _game.Regions[RegionID.DarkWorldSouthEast].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DarkWorldSouthEast, new Mode() { WorldState = WorldState.Retro });

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Retro });
                    _itemSubscriptions.Add(ItemType.Gloves, new Mode() { WorldState = WorldState.Retro });

                    break;
                case LocationID.DarkMountainFairyTakeAny:

                    GetAccessibility = () => { return _game.Regions[RegionID.DarkDeathMountainWestBottom].Accessibility; };

                    _regionSubscriptions.Add(RegionID.DarkDeathMountainWestBottom, new Mode() { WorldState = WorldState.Retro });

                    break;
                case LocationID.MireRightShackTakeAny:

                    GetAccessibility = () => { return _game.Regions[RegionID.MireArea].Accessibility; };

                    _regionSubscriptions.Add(RegionID.MireArea, new Mode() { WorldState = WorldState.Retro });

                    break;
                case LocationID.MireCaveTakeAny:

                    GetAccessibility = () => { return _game.Regions[RegionID.MireArea].Accessibility; };

                    _regionSubscriptions.Add(RegionID.MireArea, new Mode() { WorldState = WorldState.Retro });

                    break;
            }

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
        }

        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_game.Mode.WorldState))
            {
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
            if (_game.Mode.WorldState.Value == WorldState.Retro)
                Accessibility = GetAccessibility();
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
            Marking = null;
            Available = 1;
        }
    }
}
