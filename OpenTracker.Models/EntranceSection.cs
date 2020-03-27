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
        private readonly Region _standardRegion;
        private readonly Region _invertedRegion;
        private readonly Item _standardItemProvided;
        private readonly Item _invertedItemProvided;

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
            RequiredMode = new Mode();

            Available = true;

            List<Item> itemReqs = new List<Item>();

            switch (iD)
            {
                case LocationID.LumberjackHouse:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.LumberjackCaveEntrance:

                    Name = "Dropdown";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
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
                    itemReqs.Add(_game.Items[ItemType.Boots]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.DeathMountainEntryCave:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Gloves))
                                return AccessibilityLevel.Normal;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.DeathMountainExitCave:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DeathMountainExitAccess];
                    _invertedItemProvided = _game.Items[ItemType.DeathMountainExitAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.BumperCaveAccess) && _game.Items.Has(ItemType.Mirror))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.BumperCaveAccess]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);

                    break;
                case LocationID.KakarikoFortuneTeller:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.WomanLeftDoor:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.WomanRightDoor:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.LeftSnitchHouse:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.RightSnitchHouse:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.BlindsHouseEntrance:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.TheWellEntrance:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.ChickenHouseEntrance:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.GrassHouse:

                    Name = "Move your lawn";
                    _standardItemProvided = _game.Items[ItemType.GrassHouseAccess];
                    _invertedItemProvided = _game.Items[ItemType.GrassHouseAccess];
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
                case LocationID.TavernFront:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.KakarikoShop:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.BombHut:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
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
                case LocationID.SickKidEntrance:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.BlacksmithHouse:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.MagicBatEntrance:

                    Name = "Dropdown";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Hammer))
                                return AccessibilityLevel.Normal;

                            if (_game.Items.Has(ItemType.Mirror) && _game.Items.Has(ItemType.Gloves, 2) &&
                                _game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.Inspect;
                    };

                    break;
                case LocationID.ChestGame:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.RaceHouseLeft:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.RaceGameAccess];
                    _invertedItemProvided = _game.Items[ItemType.RaceGameAccess];
                    _standardRegion = _game.Regions[RegionID.DarkWorldSouth];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Mirror))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Mirror]);

                    break;
                case LocationID.RaceHouseRight:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.LibraryEntrance:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.ForestHideout:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
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

                    itemReqs.Add(_game.Items[ItemType.Mirror]);

                    break;
                case LocationID.ForestChestGame:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.CastleSecretEntrance:

                    Name = "Dropdown";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
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
                case LocationID.CastleMainEntrance:

                    Name = "Dungeon";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.CastleLeftEntrance:

                    Name = "Dungeon";
                    _standardItemProvided = _game.Items[ItemType.HyruleCastleSecondFloorAccess];
                    _invertedItemProvided = _game.Items[ItemType.HyruleCastleSecondFloorAccess];
                    _standardRegion = _game.Regions[RegionID.HyruleCastleSecondFloor];
                    _invertedRegion = _game.Regions[RegionID.HyruleCastleSecondFloor];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.CastleRightEntrance:

                    Name = "Dungeon";
                    _standardItemProvided = _game.Items[ItemType.HyruleCastleSecondFloorAccess];
                    _invertedItemProvided = _game.Items[ItemType.HyruleCastleSecondFloorAccess];
                    _standardRegion = _game.Regions[RegionID.HyruleCastleSecondFloor];
                    _invertedRegion = _game.Regions[RegionID.HyruleCastleSecondFloor];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.CastleTowerEntrance:

                    Name = "Dungeon";
                    _standardItemProvided = _game.Items[ItemType.HyruleCastleSecondFloorAccess];
                    _invertedItemProvided = _game.Items[ItemType.HyruleCastleSecondFloorAccess];
                    _standardRegion = _game.Regions[RegionID.HyruleCastleSecondFloor];
                    _invertedRegion = _game.Regions[RegionID.HyruleCastleSecondFloor];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.CanClearAgaTowerBarrier())
                                return AccessibilityLevel.Normal;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.TowerCrystals))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Cape]);
                    itemReqs.Add(_game.Items[ItemType.Sword]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.TowerCrystals]);

                    break;
                case LocationID.DamEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.CentralBonkRocks:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
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
                case LocationID.WitchsHutEntrance:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.WitchsHutAccess];
                    _invertedItemProvided = _game.Items[ItemType.WitchsHutAccess];
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

                    break;
                case LocationID.WaterfallFairyEntrance:

                    Name = "Waterfall Cave";
                    _standardItemProvided = _game.Items[ItemType.WaterfallFairyAccess];
                    _invertedItemProvided = _game.Items[ItemType.WaterfallFairyAccess];
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
                case LocationID.SahasrahlasHutEntrance:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.TreesFairyCave:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.PegsFairyCave:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.EasternPalaceEntrance:

                    Name = "Dungeon";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.HoulihanHole:

                    Name = "Dropdown";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
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
                case LocationID.SanctuaryGrave:

                    Name = "Dropdown";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
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

                    break;
                case LocationID.NorthBonkRocks:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
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
                case LocationID.KingsTombEntrance:

                    Name = "The Crypt";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Gloves, 2) && _game.Items.Has(ItemType.Boots))
                                return AccessibilityLevel.Normal;

                            if (_game.Items.Has(ItemType.Mirror) && _game.Items.Has(ItemType.MoonPearl) &&
                                _game.Items.Has(ItemType.Boots))
                                return _game.Regions[RegionID.DarkWorldWest].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Gloves, 2) && _game.Items.Has(ItemType.Boots) &&
                                _game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.Boots]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    _game.Regions[RegionID.DarkWorldWest].PropertyChanged += OnItemRequirementChanged;

                    break;
                case LocationID.GraveyardLedgeEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _standardRegion = _game.Regions[RegionID.DarkWorldWest];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Mirror))
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
                case LocationID.DesertLeftEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DesertLeftAccess];
                    _invertedItemProvided = _game.Items[ItemType.DesertLeftAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

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

                    itemReqs.Add(_game.Items[ItemType.DesertBackAccess]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    _game.Regions[RegionID.MireArea].PropertyChanged += OnItemRequirementChanged;

                    break;
                case LocationID.DesertBackEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DesertBackAccess];
                    _invertedItemProvided = _game.Items[ItemType.DesertBackAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

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

                    itemReqs.Add(_game.Items[ItemType.DesertLeftAccess]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    _game.Regions[RegionID.MireArea].PropertyChanged += OnItemRequirementChanged;

                    break;
                case LocationID.DesertRightEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () => { return AccessibilityLevel.None; };

                    break;
                case LocationID.DesertFrontEntrance:

                    Name = "Cave";
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Book))
                                return AccessibilityLevel.Normal;

                            if (_game.Items.Has(ItemType.Mirror))
                                return _game.Regions[RegionID.MireArea].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Book))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Book]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    _game.Regions[RegionID.MireArea].PropertyChanged += OnItemRequirementChanged;

                    break;
                case LocationID.AginahsCaveEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.ThiefCave:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.RupeeCave:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Gloves))
                                return AccessibilityLevel.Normal;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.SkullWoodsBack:

                    Name = "Dungeon";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldWestAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldWestAccess];
                    _standardRegion = _game.Regions[RegionID.DarkWorldWest];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldWest];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.FireRod) && _game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.FireRod))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.FireRod]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.ThievesTownEntrance:

                    Name = "Dungeon";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldWestAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldWestAccess];
                    _standardRegion = _game.Regions[RegionID.DarkWorldWest];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldWest];

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

                    break;
                case LocationID.CShapedHouseEntrance:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldWestAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldWestAccess];
                    _standardRegion = _game.Regions[RegionID.DarkWorldWest];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldWest];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.HammerHouse:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.HammerHouseAccess];
                    _invertedItemProvided = _game.Items[ItemType.HammerHouseAccess];
                    _standardRegion = _game.Regions[RegionID.DarkWorldWest];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldWest];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Hammer))
                                return AccessibilityLevel.Normal;
                        }    

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.DarkVillageFortuneTeller:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldWestAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldWestAccess];
                    _standardRegion = _game.Regions[RegionID.DarkWorldWest];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldWest];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.DarkChapel:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldWestAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldWestAccess];
                    _standardRegion = _game.Regions[RegionID.DarkWorldWest];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldWest];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.ShieldShop:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldWestAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldWestAccess];
                    _standardRegion = _game.Regions[RegionID.DarkWorldWest];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldWest];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.DarkLumberjack:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldWestAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldWestAccess];
                    _standardRegion = _game.Regions[RegionID.DarkWorldWest];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldWest];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.TreasureGameEntrance:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldWestAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldWestAccess];
                    _standardRegion = _game.Regions[RegionID.DarkWorldWest];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldWest];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.BombableShackEntrance:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldWestAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldWestAccess];
                    _standardRegion = _game.Regions[RegionID.DarkWorldWest];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldWest];

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
                case LocationID.HammerPegsEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldWestAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldWestAccess];
                    _standardRegion = _game.Regions[RegionID.DarkWorldWest];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldWest];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Gloves, 2) && _game.Items.Has(ItemType.Hammer) &&
                                _game.Items.Has(ItemType.MoonPearl))
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
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);

                    _game.Regions[RegionID.LightWorld].PropertyChanged += OnItemRequirementChanged;

                    break;
                case LocationID.BumperCaveExit:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.BumperCaveAccess];
                    _invertedItemProvided = _game.Items[ItemType.BumperCaveAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldSouth];

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

                    itemReqs.Add(_game.Items[ItemType.DeathMountainExitAccess]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);

                    break;
                case LocationID.BumperCaveEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldWestAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldWestAccess];
                    _standardRegion = _game.Regions[RegionID.DarkWorldWest];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldWest];

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
                case LocationID.HypeCaveEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldSouthAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldSouthAccess];
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
                case LocationID.SwampPalaceEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldSouthAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldSouthAccess];
                    _standardRegion = _game.Regions[RegionID.DarkWorldSouth];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldSouth];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.DarkCentralBonkRocks:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldSouthAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldSouthAccess];
                    _standardRegion = _game.Regions[RegionID.DarkWorldSouth];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldSouth];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Boots) && _game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Boots))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Boots]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.SouthOfGroveEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
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
                            return AccessibilityLevel.Normal;

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Mirror]);

                    break;
                case LocationID.BombShop:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldSouthAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _standardRegion = _game.Regions[RegionID.DarkWorldSouth];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.ArrowGame:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldSouthAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldSouthAccess];
                    _standardRegion = _game.Regions[RegionID.DarkWorldSouth];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldSouth];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.DarkHyliaFortuneTeller:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldSouthAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldSouthAccess];
                    _standardRegion = _game.Regions[RegionID.DarkWorldSouth];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldSouth];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.DarkTreesFairyCave:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldEastAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldEastAccess];
                    _standardRegion = _game.Regions[RegionID.DarkWorldEast];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldEast];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.DarkSahasrahla:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldEastAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldEastAccess];
                    _standardRegion = _game.Regions[RegionID.DarkWorldEast];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldEast];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.PalaceOfDarknessEntrance:

                    Name = "Dungeon";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldEastAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldEastAccess];
                    _standardRegion = _game.Regions[RegionID.DarkWorldEast];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldEast];

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
                case LocationID.DarkWitchsHut:

                    Name = "House";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldWitchAreaAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldWitchAreaAccess];
                    _standardRegion = _game.Regions[RegionID.DarkWorldWitchArea];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldWitchArea];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.DarkFluteSpotFive:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldEastAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldEastAccess];
                    _standardRegion = _game.Regions[RegionID.DarkWorldEast];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldEast];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.FatFairyEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldEastAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldEastAccess];
                    _standardRegion = _game.Regions[RegionID.DarkWorldEast];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldEast];

                    GetAccessibility = () =>
                    {
                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.RedCrystal, 2))
                                return AccessibilityLevel.Normal;
                        }

                        //  Inverted mode
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.RedCrystal, 2))
                                return AccessibilityLevel.Normal;
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.RedCrystal]);

                    break;
                case LocationID.GanonHole:

                    Name = "Dropdown";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldEastAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _standardRegion = _game.Regions[RegionID.DarkWorldEast];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        //  Default to full access, but not open until GT completed or Fast Ganon mode
                        return AccessibilityLevel.Normal;
                    };

                    break;
                case LocationID.DarkIceRodCave:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldSouthEastAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldSouthEastAccess];
                    _standardRegion = _game.Regions[RegionID.DarkWorldSouthEast];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldSouthEast];

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
                case LocationID.DarkFakeIceRodCave:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldSouthEastAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldSouthEastAccess];
                    _standardRegion = _game.Regions[RegionID.DarkWorldSouthEast];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldSouthEast];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.DarkIceRodRock:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkWorldSouthEastAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkWorldSouthEastAccess];
                    _standardRegion = _game.Regions[RegionID.DarkWorldSouthEast];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldSouthEast];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Gloves))
                                return AccessibilityLevel.Normal;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Gloves))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);

                    break;
                case LocationID.HypeFairyCave:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
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
                case LocationID.FortuneTeller:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.LakeShop:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.UpgradeFairy:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LakeHyliaFairyIslandAccess];
                    _invertedItemProvided = _game.Items[ItemType.LakeHyliaFairyIslandAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldSouth];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Flippers))
                                return AccessibilityLevel.Normal;

                            if (_game.Items.Has(ItemType.IcePalaceAccess) && _game.Items.Has(ItemType.Mirror))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.SequenceBreak;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Gloves, 2))
                                return AccessibilityLevel.Normal;

                            if (_game.Items.Has(ItemType.MoonPearl))
                            {
                                if (_game.Regions[RegionID.LightWorld].Accessibility == AccessibilityLevel.Normal &&
                                    _game.Items.Has(ItemType.Flippers))
                                    return AccessibilityLevel.Normal;

                                if (_game.Regions[RegionID.LightWorld].Accessibility >= AccessibilityLevel.SequenceBreak)
                                    return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Flippers]);
                    itemReqs.Add(_game.Items[ItemType.IcePalaceAccess]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    _game.Regions[RegionID.LightWorld].PropertyChanged += OnItemRequirementChanged;

                    break;
                case LocationID.MiniMoldormCaveEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
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
                case LocationID.IceRodCaveEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
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
                case LocationID.IceBeeCave:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.IceFairyCave:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _invertedItemProvided = _game.Items[ItemType.LightWorldAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Gloves))
                                return AccessibilityLevel.Normal;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Gloves))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);

                    break;
                case LocationID.IcePalaceEntrance:

                    Name = "Dungeon";
                    _standardItemProvided = _game.Items[ItemType.IcePalaceAccess];
                    _invertedItemProvided = _game.Items[ItemType.IcePalaceAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.DarkWorldSouth];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Gloves, 2))
                            {
                                if (_game.Items.Has(ItemType.Flippers))
                                    return AccessibilityLevel.Normal;

                                return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Flippers))
                                return AccessibilityLevel.Normal;

                            if (_game.Items.Has(ItemType.Mirror))
                            {
                                if (_game.Items.Has(ItemType.LakeHyliaFairyIslandAccess))
                                    return AccessibilityLevel.Normal;

                                if (_game.Regions[RegionID.LightWorld].Accessibility == AccessibilityLevel.Normal &&
                                    _game.Items.Has(ItemType.Flippers) && _game.Items.Has(ItemType.MoonPearl))
                                    return AccessibilityLevel.Normal;
                            }

                            return AccessibilityLevel.SequenceBreak;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.Flippers]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.LakeHyliaFairyIslandAccess]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    _game.Regions[RegionID.LightWorld].PropertyChanged += OnItemRequirementChanged;

                    break;
                case LocationID.MiseryMireEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.MireAreaAccess];
                    _invertedItemProvided = _game.Items[ItemType.MireAreaAccess];
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
                                return AccessibilityLevel.Normal;
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
                                return AccessibilityLevel.Normal;
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

                    break;
                case LocationID.MireShackEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.MireAreaAccess];
                    _invertedItemProvided = _game.Items[ItemType.MireAreaAccess];
                    _standardRegion = _game.Regions[RegionID.MireArea];
                    _invertedRegion = _game.Regions[RegionID.MireArea];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.MireRightShack:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.MireAreaAccess];
                    _invertedItemProvided = _game.Items[ItemType.MireAreaAccess];
                    _standardRegion = _game.Regions[RegionID.MireArea];
                    _invertedRegion = _game.Regions[RegionID.MireArea];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.MireCave:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.MireAreaAccess];
                    _invertedItemProvided = _game.Items[ItemType.MireAreaAccess];
                    _standardRegion = _game.Regions[RegionID.MireArea];
                    _invertedRegion = _game.Regions[RegionID.MireArea];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.CheckerboardCaveEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.CheckerboardCaveAccess];
                    _invertedItemProvided = _game.Items[ItemType.CheckerboardCaveAccess];
                    _standardRegion = _game.Regions[RegionID.MireArea];
                    _invertedRegion = _game.Regions[RegionID.LightWorld];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Mirror) && _game.Items.Has(ItemType.Gloves))
                                return AccessibilityLevel.Normal;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Gloves))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    break;
                case LocationID.DeathMountainEntranceBack:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DeathMountainWestBottomAccess];
                    _invertedItemProvided = _game.Items[ItemType.DeathMountainWestBottomAccess];
                    _standardRegion = _game.Regions[RegionID.DeathMountainWestBottom];
                    _invertedRegion = _game.Regions[RegionID.DeathMountainWestBottom];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.OldManResidence:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DeathMountainWestBottomAccess];
                    _invertedItemProvided = _game.Items[ItemType.DeathMountainWestBottomAccess];
                    _standardRegion = _game.Regions[RegionID.DeathMountainWestBottom];
                    _invertedRegion = _game.Regions[RegionID.DeathMountainWestBottom];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.OldManBackResidence:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DeathMountainWestBottomAccess];
                    _invertedItemProvided = _game.Items[ItemType.DeathMountainWestBottomAccess];
                    _standardRegion = _game.Regions[RegionID.DeathMountainWestBottom];
                    _invertedRegion = _game.Regions[RegionID.DeathMountainWestBottom];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.DeathMountainExitFront:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DeathMountainWestBottomAccess];
                    _invertedItemProvided = _game.Items[ItemType.DeathMountainWestBottomAccess];
                    _standardRegion = _game.Regions[RegionID.DeathMountainWestBottom];
                    _invertedRegion = _game.Regions[RegionID.DeathMountainWestBottom];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.SpectacleRockLeft:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DeathMountainWestBottomAccess];
                    _invertedItemProvided = _game.Items[ItemType.DeathMountainWestBottomAccess];
                    _standardRegion = _game.Regions[RegionID.DeathMountainWestBottom];
                    _invertedRegion = _game.Regions[RegionID.DeathMountainWestBottom];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.SpectacleRockRight:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DeathMountainWestBottomAccess];
                    _invertedItemProvided = _game.Items[ItemType.DeathMountainWestBottomAccess];
                    _standardRegion = _game.Regions[RegionID.DeathMountainWestBottom];
                    _invertedRegion = _game.Regions[RegionID.DeathMountainWestBottom];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.SpectacleRockTop:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DeathMountainWestBottomAccess];
                    _invertedItemProvided = _game.Items[ItemType.DeathMountainWestBottomAccess];
                    _standardRegion = _game.Regions[RegionID.DeathMountainWestBottom];
                    _invertedRegion = _game.Regions[RegionID.DeathMountainWestBottom];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.SpikeCaveEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkDeathMountainWestBottomAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkDeathMountainWestBottomAccess];
                    _standardRegion = _game.Regions[RegionID.DarkDeathMountainWestBottom];
                    _invertedRegion = _game.Regions[RegionID.DarkDeathMountainWestBottom];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.DarkMountainFairy:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkDeathMountainWestBottomAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkDeathMountainWestBottomAccess];
                    _standardRegion = _game.Regions[RegionID.DarkDeathMountainWestBottom];
                    _invertedRegion = _game.Regions[RegionID.DarkDeathMountainWestBottom];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.TowerOfHeraEntrance:

                    Name = "Dungeon";
                    _standardItemProvided = _game.Items[ItemType.DeathMountainWestTopAccess];
                    _invertedItemProvided = _game.Items[ItemType.DeathMountainWestTopAccess];
                    _standardRegion = _game.Regions[RegionID.DeathMountainWestTop];
                    _invertedRegion = _game.Regions[RegionID.DeathMountainWestTop];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.SpiralCaveBottom:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DeathMountainEastBottomAccess];
                    _invertedItemProvided = _game.Items[ItemType.DeathMountainEastBottomAccess];
                    _standardRegion = _game.Regions[RegionID.DeathMountainEastBottom];
                    _invertedRegion = _game.Regions[RegionID.DeathMountainEastBottom];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.EDMFairyCave:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DeathMountainEastBottomAccess];
                    _invertedItemProvided = _game.Items[ItemType.DeathMountainEastBottomAccess];
                    _standardRegion = _game.Regions[RegionID.DeathMountainEastBottom];
                    _invertedRegion = _game.Regions[RegionID.DeathMountainEastBottom];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.ParadoxCaveMiddle:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DeathMountainEastBottomAccess];
                    _invertedItemProvided = _game.Items[ItemType.DeathMountainEastBottomAccess];
                    _standardRegion = _game.Regions[RegionID.DeathMountainEastBottom];
                    _invertedRegion = _game.Regions[RegionID.DeathMountainEastBottom];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.ParadoxCaveBottom:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DeathMountainEastBottomAccess];
                    _invertedItemProvided = _game.Items[ItemType.DeathMountainEastBottomAccess];
                    _standardRegion = _game.Regions[RegionID.DeathMountainEastBottom];
                    _invertedRegion = _game.Regions[RegionID.DeathMountainEastBottom];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.EDMConnectorBottom:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DeathMountainEastBottomAccess];
                    _invertedItemProvided = _game.Items[ItemType.DeathMountainEastBottomAccess];
                    _standardRegion = _game.Regions[RegionID.DeathMountainEastBottom];
                    _invertedRegion = _game.Regions[RegionID.DeathMountainEastBottom];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Gloves, 2))
                                return AccessibilityLevel.Normal;

                            if (_game.Items.Has(ItemType.DeathMountainEastTopConnectorAccess))
                                return AccessibilityLevel.Normal;

                            AccessibilityLevel dMEastTop = _game.Regions[RegionID.DeathMountainEastTop].Accessibility;
                            AccessibilityLevel dDMEastBottom = AccessibilityLevel.None;

                            if (_game.Items.Has(ItemType.Mirror))
                            {
                                if (_game.Items.Has(ItemType.TurtleRockSafetyDoorAccess))
                                    return AccessibilityLevel.Normal;

                                dDMEastBottom = _game.Regions[RegionID.DarkDeathMountainEastBottom].Accessibility;
                            }

                            return (AccessibilityLevel)Math.Max((byte)dMEastTop, (byte)dDMEastBottom);
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Gloves, 2) && _game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;

                            return _game.Regions[RegionID.DeathMountainEastTop].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainEastTopConnectorAccess]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    _game.Regions[RegionID.DeathMountainEastTop].PropertyChanged += OnItemRequirementChanged;
                    _game.Regions[RegionID.DarkDeathMountainEastBottom].PropertyChanged += OnItemRequirementChanged;

                    break;
                case LocationID.SpiralCaveTop:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.SpiralCaveTopAccess];
                    _invertedItemProvided = _game.Items[ItemType.SpiralCaveTopAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.DeathMountainEastTop];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.TurtleRockTunnelAccess) && _game.Items.Has(ItemType.Mirror))
                                return AccessibilityLevel.Normal;

                            return _game.Regions[RegionID.DeathMountainEastTop].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                            return AccessibilityLevel.Normal;

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.TurtleRockTunnelAccess]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);

                    break;
                case LocationID.MimicCaveEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.MimicCaveAccess];
                    _invertedItemProvided = _game.Items[ItemType.MimicCaveAccess];
                    _standardRegion = _game.Regions[RegionID.TurtleRockTunnel];
                    _invertedRegion = _game.Regions[RegionID.DeathMountainEastTop];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Mirror))
                                return AccessibilityLevel.Normal;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                            return AccessibilityLevel.Normal;

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Mirror]);

                    break;
                case LocationID.EDMConnectorTop:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DeathMountainEastTopConnectorAccess];
                    _invertedItemProvided = _game.Items[ItemType.DeathMountainEastTopConnectorAccess];
                    _standardRegion = _game.Regions[RegionID.LightWorld];
                    _invertedRegion = _game.Regions[RegionID.DeathMountainEastTop];

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
                            return AccessibilityLevel.Normal;

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.TurtleRockSafetyDoorAccess]);

                    break;
                case LocationID.ParadoxCaveTop:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DeathMountainEastTopAccess];
                    _invertedItemProvided = _game.Items[ItemType.DeathMountainEastTopAccess];
                    _standardRegion = _game.Regions[RegionID.DeathMountainEastTop];
                    _invertedRegion = _game.Regions[RegionID.DeathMountainEastTop];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.SuperBunnyCaveBottom:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkDeathMountainEastBottomAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkDeathMountainEastBottomAccess];
                    _standardRegion = _game.Regions[RegionID.DarkDeathMountainEastBottom];
                    _invertedRegion = _game.Regions[RegionID.DarkDeathMountainEastBottom];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.DeathMountainShop:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkDeathMountainEastBottomAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkDeathMountainEastBottomAccess];
                    _standardRegion = _game.Regions[RegionID.DarkDeathMountainEastBottom];
                    _invertedRegion = _game.Regions[RegionID.DarkDeathMountainEastBottom];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.SuperBunnyCaveTop:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkDeathMountainTopAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkDeathMountainTopAccess];
                    _standardRegion = _game.Regions[RegionID.DarkDeathMountainTop];
                    _invertedRegion = _game.Regions[RegionID.DarkDeathMountainTop];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.HookshotCaveEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkDeathMountainTopAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkDeathMountainTopAccess];
                    _standardRegion = _game.Regions[RegionID.DarkDeathMountainTop];
                    _invertedRegion = _game.Regions[RegionID.DarkDeathMountainTop];

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
                case LocationID.TurtleRockEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkDeathMountainTopAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkDeathMountainTopAccess];
                    _standardRegion = _game.Regions[RegionID.DarkDeathMountainTop];
                    _invertedRegion = _game.Regions[RegionID.DarkDeathMountainTop];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Hammer) &&
                                _game.Items.CanUseMedallions() && ((_game.Items.Has(ItemType.Bombos) &&
                                _game.Items.Has(ItemType.Ether) && _game.Items.Has(ItemType.Quake)) ||
                                (_game.Items.Has(ItemType.Bombos) &&
                                _game.Items[ItemType.BombosDungeons].Current >= 2) ||
                                (_game.Items.Has(ItemType.Ether) &&
                                _game.Items[ItemType.EtherDungeons].Current >= 2) ||
                                (_game.Items.Has(ItemType.Quake) &&
                                _game.Items[ItemType.QuakeDungeons].Current >= 2)))
                                return AccessibilityLevel.Normal;
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
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.Sword]);
                    itemReqs.Add(_game.Items[ItemType.Bombos]);
                    itemReqs.Add(_game.Items[ItemType.BombosDungeons]);
                    itemReqs.Add(_game.Items[ItemType.Ether]);
                    itemReqs.Add(_game.Items[ItemType.EtherDungeons]);
                    itemReqs.Add(_game.Items[ItemType.Quake]);
                    itemReqs.Add(_game.Items[ItemType.QuakeDungeons]);

                    break;
                case LocationID.GanonsTowerEntrance:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkDeathMountainTopAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkDeathMountainTopAccess];
                    _standardRegion = _game.Regions[RegionID.DarkDeathMountainTop];
                    _invertedRegion = _game.Regions[RegionID.DarkDeathMountainTop];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.TowerCrystals))
                                return AccessibilityLevel.Normal;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                            return AccessibilityLevel.Normal;

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.TowerCrystals]);
                    itemReqs.Add(_game.Items[ItemType.Crystal]);
                    itemReqs.Add(_game.Items[ItemType.RedCrystal]);

                    break;
                case LocationID.TRLedgeLeft:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.TurtleRockTunnelAccess];
                    _invertedItemProvided = _game.Items[ItemType.TurtleRockTunnelAccess];
                    _standardRegion = _game.Regions[RegionID.TurtleRockTunnel];
                    _invertedRegion = _game.Regions[RegionID.TurtleRockTunnel];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.TRLedgeRight:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.TurtleRockTunnelAccess];
                    _invertedItemProvided = _game.Items[ItemType.TurtleRockTunnelAccess];
                    _standardRegion = _game.Regions[RegionID.TurtleRockTunnel];
                    _invertedRegion = _game.Regions[RegionID.TurtleRockTunnel];

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case LocationID.TRSafetyDoor:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.TurtleRockSafetyDoorAccess];
                    _invertedItemProvided = _game.Items[ItemType.TurtleRockSafetyDoorAccess];
                    _standardRegion = _game.Regions[RegionID.DarkDeathMountainTop];
                    _invertedRegion = _game.Regions[RegionID.DeathMountainEastTop];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Mirror))
                                return AccessibilityLevel.Normal;
                        }    

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Mirror]);

                    break;
                case LocationID.HookshotCaveTop:

                    Name = "Cave";
                    _standardItemProvided = _game.Items[ItemType.DarkDeathMountainFloatingIslandAccess];
                    _invertedItemProvided = _game.Items[ItemType.DarkDeathMountainFloatingIslandAccess];
                    _standardRegion = _game.Regions[RegionID.DarkDeathMountainEastBottom];
                    _invertedRegion = _game.Regions[RegionID.DeathMountainEastTop];

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Mirror))
                                return AccessibilityLevel.Normal;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Mirror]);

                    break;
            }

            if (GetAccessibility == null)
                GetAccessibility = () => { return AccessibilityLevel.None; };

            _game.Mode.PropertyChanged += OnModeChanged;

            foreach (Item item in itemReqs)
                item.PropertyChanged += OnItemRequirementChanged;

            if (_standardRegion != null)
                _standardRegion.PropertyChanged += OnRegionChanged;

            if (_invertedRegion != null)
                _invertedRegion.PropertyChanged += OnRegionChanged;

            UpdateAccessibility();
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

        private void OnRegionChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateAccessibility();
        }

        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!Available && e.PropertyName == nameof(_game.Mode.WorldState))
            {
                if (_standardItemProvided != null && _invertedItemProvided != null)
                {
                    if (_game.Mode.WorldState == WorldState.StandardOpen)
                    {
                        _invertedItemProvided.Current--;
                        _standardItemProvided.Current++;
                    }

                    if (_game.Mode.WorldState == WorldState.Inverted)
                    {
                        _standardItemProvided.Current--;
                        _invertedItemProvided.Current++;
                    }
                }
            }

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

            if (propertyName == nameof(Available))
            {
                if (_game.Mode.WorldState == WorldState.StandardOpen && _standardItemProvided != null)
                {
                    if (Available)
                        _standardItemProvided.Current--;
                    else
                        _standardItemProvided.Current++;
                }

                if (_game.Mode.WorldState == WorldState.Inverted && _invertedItemProvided != null)
                {
                    if (Available)
                        _invertedItemProvided.Current--;
                    else
                        _invertedItemProvided.Current++;
                }
            }
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
