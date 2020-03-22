using OpenTracker.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models
{
    public class Region : INotifyPropertyChanged
    {
        private readonly Game _game;

        public RegionID ID { get; }
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

        public Region(Game game, RegionID iD)
        {
            _game = game;
            ID = iD;

            _game.Mode.PropertyChanged += OnModeChanged;

            List<Item> itemReqs = new List<Item>();

            switch (iD)
            {
                case RegionID.LightWorld:

                    GetAccessibility = () =>
                    {
                        //  Access from the start in Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            //  Access via Agahnim portal
                            if (_game.Items.Has(ItemType.Aga))
                                return AccessibilityLevel.Normal;

                            //  Access via exits in the region
                            if (_game.Items.Has(ItemType.LightWorldAccess) ||
                                _game.Items.Has(ItemType.DeathMountainExitAccess) ||
                                _game.Items.Has(ItemType.RaceGameAccess) ||
                                _game.Items.Has(ItemType.HyruleCastleSecondFloorAccess) ||
                                _game.Items.Has(ItemType.DesertLeftAccess))
                                return AccessibilityLevel.Normal;

                            if (_game.Items.Has(ItemType.MoonPearl))
                            {

                                //  Access via either Village of Outcasts portal, Palace of Darkness portal,
                                //    or portal north of Swamp Palace
                                if (_game.Items.Has(ItemType.Gloves, 2) ||
                                    (_game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.Hammer)))
                                    return AccessibilityLevel.Normal;

                                //  Access via Desert Palace back exit
                                if (_game.Items.Has(ItemType.DesertBackAccess) && _game.Items.Has(ItemType.Gloves))
                                    return AccessibilityLevel.Normal;

                                //  Access via Lake Hylia capacity upgrade fairy island
                                if (_game.Items.Has(ItemType.LakeHyliaFairyIslandAccess) &&
                                    _game.Items.Has(ItemType.Flippers))
                                    return AccessibilityLevel.Normal;

                                //  Access via Waterfall Fairy exit
                                if (_game.Items.Has(ItemType.WaterfallFairyAccess) &&
                                    _game.Items.Has(ItemType.Flippers))
                                    return AccessibilityLevel.Normal;

                                //  Access via Lake Hylia capacity upgrade fairy island (Fake Flippers)
                                if (_game.Items.Has(ItemType.LakeHyliaFairyIslandAccess))
                                    return AccessibilityLevel.SequenceBreak;

                                //  Access via Waterfall Fairy exit (Fake Flippers)
                                if (_game.Items.Has(ItemType.WaterfallFairyAccess))
                                    return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Aga]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.LightWorldAccess]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainExitAccess]);
                    itemReqs.Add(_game.Items[ItemType.RaceGameAccess]);
                    itemReqs.Add(_game.Items[ItemType.DesertLeftAccess]);
                    itemReqs.Add(_game.Items[ItemType.DesertBackAccess]);
                    itemReqs.Add(_game.Items[ItemType.HyruleCastleSecondFloorAccess]);
                    itemReqs.Add(_game.Items[ItemType.LakeHyliaFairyIslandAccess]);
                    itemReqs.Add(_game.Items[ItemType.Flippers]);
                    itemReqs.Add(_game.Items[ItemType.WaterfallFairyAccess]);

                    break;
                case RegionID.HyruleCastleSecondFloor:

                    GetAccessibility = () =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.HyruleCastleSecondFloorAccess))
                            return AccessibilityLevel.Normal;

                        //  Access via mirror from east Dark World
                        if (_game.Mode.WorldState == WorldState.StandardOpen && _game.Items.Has(ItemType.Mirror))
                        {
                            //  East Dark World access via castle gate portal
                            if (_game.Items.Has(ItemType.Aga))
                                return AccessibilityLevel.Normal;

                            //  East Dark World access via exits in the region
                            if (_game.Items.Has(ItemType.DarkWorldEastAccess))
                                return AccessibilityLevel.Normal;

                            //  East Dark World access via Eastern Palace portal
                            if (_game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.Hammer) &&
                                _game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;

                            //  East Dark World access via Hammer House exit in Village of Outcasts
                            if (_game.Items.Has(ItemType.HammerHouseAccess) && _game.Items.Has(ItemType.Hammer) &&
                                _game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.Normal;

                            //  East Dark World access via West Dark World exits
                            if ((_game.Items.Has(ItemType.DarkWorldWestAccess) ||
                                _game.Items.Has(ItemType.BumperCaveAccess)) && _game.Items.Has(ItemType.MoonPearl) &&
                                (_game.Items.Has(ItemType.Hammer) || _game.Items.Has(ItemType.Flippers)))
                                return AccessibilityLevel.Normal;

                            //  East Dark World access via South Dark World exits
                            if (_game.Items.Has(ItemType.DarkWorldSouthAccess) &&
                                _game.Items.Has(ItemType.MoonPearl) &&
                                (_game.Items.Has(ItemType.Hammer) || _game.Items.Has(ItemType.Flippers)))
                                return AccessibilityLevel.Normal;

                            //  East Dark World access via Southeast Dark World exits
                            if (_game.Items.Has(ItemType.DarkWorldSouthEastAccess) &&
                                _game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Flippers))
                                return AccessibilityLevel.Normal;

                            //  East Dark World access via Village of Outcasts portal
                            if (_game.Items.Has(ItemType.Gloves, 2) && _game.Items.Has(ItemType.MoonPearl) &&
                                (_game.Items.Has(ItemType.Hammer) || _game.Items.Has(ItemType.Flippers)))
                                return AccessibilityLevel.Normal;

                            //  East Dark World access via Witch Area exit
                            if (_game.Items.Has(ItemType.DarkWorldWitchAreaAccess) &&
                                _game.Items.Has(ItemType.MoonPearl) &&
                                (_game.Items.Has(ItemType.Hammer) || _game.Items.Has(ItemType.Gloves) ||
                                (_game.Items.Has(ItemType.Flippers) && _game.Items.Has(ItemType.Hookshot))))
                                return AccessibilityLevel.Normal;

                            //  East Dark World access via West Dark World exits (Fake Flippers)
                            if ((_game.Items.Has(ItemType.DarkWorldWestAccess) ||
                                _game.Items.Has(ItemType.BumperCaveAccess)) && _game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.SequenceBreak;

                            //  East Dark World access via South Dark World exits (Fake Flippers)
                            if (_game.Items.Has(ItemType.DarkWorldSouthAccess) &&
                                _game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.SequenceBreak;

                            //  East Dark World access via Southeast Dark World exits (Fake Flippers)
                            if (_game.Items.Has(ItemType.DarkWorldSouthEastAccess) &&
                                _game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.SequenceBreak;

                            //  East Dark World access via Village of Outcasts portal (Fake Flippers)
                            if (_game.Items.Has(ItemType.Gloves, 2) && _game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.SequenceBreak;

                            //  East Dark World access via Witch Area exit (Fake Flippers)
                            if (_game.Items.Has(ItemType.DarkWorldWitchAreaAccess) &&
                                _game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Hookshot))
                                return AccessibilityLevel.SequenceBreak;
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.HyruleCastleSecondFloorAccess]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.Aga]);
                    itemReqs.Add(_game.Items[ItemType.DarkWorldEastAccess]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.HammerHouseAccess]);
                    itemReqs.Add(_game.Items[ItemType.DarkWorldWitchAreaAccess]);
                    itemReqs.Add(_game.Items[ItemType.Flippers]);
                    itemReqs.Add(_game.Items[ItemType.DarkWorldWestAccess]);
                    itemReqs.Add(_game.Items[ItemType.BumperCaveAccess]);
                    itemReqs.Add(_game.Items[ItemType.DarkWorldSouthAccess]);
                    itemReqs.Add(_game.Items[ItemType.DarkWorldSouthEastAccess]);

                    break;
                case RegionID.DarkWorldWest:

                    GetAccessibility = () =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.DarkWorldWestAccess) ||
                            _game.Items.Has(ItemType.BumperCaveAccess))
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.StandardOpen && _game.Items.Has(ItemType.MoonPearl))
                        {
                            //  Access via Kakariko Village portal
                            if ((_game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.Hammer)) ||
                                _game.Items.Has(ItemType.Gloves, 2))
                                return AccessibilityLevel.Normal;

                            //  Access via Hammer House exit
                            if (_game.Items.Has(ItemType.HammerHouseAccess) && _game.Items.Has(ItemType.Hammer))
                                return AccessibilityLevel.Normal;

                            //  Access from other parts of Dark World by Hookshot
                            if (_game.Items.Has(ItemType.Hookshot))
                            {
                                //  Access via Dark World Witch Area
                                if (_game.Items.Has(ItemType.DarkWorldWitchAreaAccess))
                                    return AccessibilityLevel.Normal;

                                //  Access via East Dark World exit or Hyrule Castle portal
                                if ((_game.Items.Has(ItemType.DarkWorldEastAccess) ||
                                    _game.Items.Has(ItemType.Aga)) && (_game.Items.Has(ItemType.Flippers) ||
                                    _game.Items.Has(ItemType.Gloves) || _game.Items.Has(ItemType.Hammer)))
                                    return AccessibilityLevel.Normal;

                                //  Access via Southeast Dark World exit
                                if (_game.Items.Has(ItemType.DarkWorldSouthEastAccess) &&
                                    _game.Items.Has(ItemType.Flippers))
                                    return AccessibilityLevel.Normal;

                                //  Access via South Dark World exit
                                if (_game.Items.Has(ItemType.DarkWorldSouthAccess) &&
                                    (_game.Items.Has(ItemType.Hammer) || _game.Items.Has(ItemType.Flippers)))
                                    return AccessibilityLevel.Normal;

                                //  Access via East Dark World exit or Hyrule Castle portal (Fake Flippers)
                                if (_game.Items.Has(ItemType.DarkWorldEastAccess) || _game.Items.Has(ItemType.Aga))
                                    return AccessibilityLevel.SequenceBreak;

                                //  Access via Southeast Dark World exit (Fake Flippers)
                                if (_game.Items.Has(ItemType.DarkWorldSouthEastAccess))
                                    return AccessibilityLevel.SequenceBreak;

                                //  Access via South Dark World exit (Fake Flippers)
                                if (_game.Items.Has(ItemType.DarkWorldSouthAccess))
                                    return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            //  Access from the start in non-entrance Inverted mode
                            if (!_game.Mode.EntranceShuffle.Value)
                                return AccessibilityLevel.Normal;

                            //  Access via South Dark World by lifting rock near Jeremiah
                            if (_game.Items.Has(ItemType.Gloves, 2))
                                return AccessibilityLevel.Normal;

                            //  Access via Hammer House exit
                            if (_game.Items.Has(ItemType.HammerHouseAccess) && _game.Items.Has(ItemType.Hammer))
                                return AccessibilityLevel.Normal;

                            //  Access via other parts of Dark World by Hookshoot
                            if (_game.Items.Has(ItemType.Hookshot))
                            {
                                //  Access via Dark World Witch Area exit
                                if (_game.Items.Has(ItemType.DarkWorldWitchAreaAccess))
                                    return AccessibilityLevel.Normal;

                                //  Access via Dark World East exits
                                if (_game.Items.Has(ItemType.DarkWorldEastAccess) &&
                                    (_game.Items.Has(ItemType.Flippers) || _game.Items.Has(ItemType.Hammer) ||
                                    _game.Items.Has(ItemType.Gloves)))
                                    return AccessibilityLevel.Normal;

                                //  Access via Dark World South
                                if (_game.Items.Has(ItemType.Hammer) || _game.Items.Has(ItemType.Flippers))
                                    return AccessibilityLevel.Normal;
                            }

                            //  Access via mirror from Light World
                            if (_game.Items.Has(ItemType.Mirror))
                            {
                                //  Light World access via Agahnim portal
                                if (_game.Items.Has(ItemType.Aga))
                                    return AccessibilityLevel.Normal;

                                //  Light World access via either Palace of Darkness portal
                                //    or portal north of Swamp Palace
                                if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Gloves) &&
                                    _game.Items.Has(ItemType.MoonPearl))
                                    return AccessibilityLevel.Normal;

                                //  Light World access via exits in the region
                                if (_game.Items.Has(ItemType.LightWorldAccess) ||
                                    _game.Items.Has(ItemType.DeathMountainExitAccess) ||
                                    _game.Items.Has(ItemType.RaceGameAccess))
                                    return AccessibilityLevel.Normal;

                                //  Light World access via Desert Palace exits
                                if (_game.Items.Has(ItemType.DesertLeftAccess) ||
                                    (_game.Items.Has(ItemType.DesertBackAccess) &&
                                    _game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Gloves)))
                                    return AccessibilityLevel.Normal;

                                //  Light World access via Hyrule Castle second floor exits
                                if (_game.Items.Has(ItemType.HyruleCastleSecondFloorAccess))
                                    return AccessibilityLevel.Normal;

                                //  Access via Lake Hylia capacity upgrade fairy island
                                if (_game.Items.Has(ItemType.LakeHyliaFairyIslandAccess) &&
                                    _game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Flippers))
                                    return AccessibilityLevel.Normal;

                                //  Access via Waterfall Fairy exit
                                if (_game.Items.Has(ItemType.WaterfallFairyAccess) &&
                                    _game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Flippers))
                                    return AccessibilityLevel.Normal;
                            }

                            //  Fake Flippers from Dark World South to Dark World Witch Area
                            if (_game.Items.Has(ItemType.Hookshot))
                                return AccessibilityLevel.SequenceBreak;

                            //  Access via Lake Hylia capacity upgrade fairy island (Fake Flippers)
                            if (_game.Items.Has(ItemType.Mirror) &&
                                _game.Items.Has(ItemType.LakeHyliaFairyIslandAccess) &&
                                _game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.SequenceBreak;

                            //  Access via Waterfall Fairy exit (Fake Flippers)
                            if (_game.Items.Has(ItemType.WaterfallFairyAccess) &&
                                _game.Items.Has(ItemType.MoonPearl))
                                return AccessibilityLevel.SequenceBreak;
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.DarkWorldWestAccess]);
                    itemReqs.Add(_game.Items[ItemType.BumperCaveAccess]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.HammerHouseAccess]);
                    itemReqs.Add(_game.Items[ItemType.Hookshot]);
                    itemReqs.Add(_game.Items[ItemType.DarkWorldWitchAreaAccess]);
                    itemReqs.Add(_game.Items[ItemType.DarkWorldEastAccess]);
                    itemReqs.Add(_game.Items[ItemType.Aga]);
                    itemReqs.Add(_game.Items[ItemType.Flippers]);
                    itemReqs.Add(_game.Items[ItemType.DarkWorldSouthEastAccess]);
                    itemReqs.Add(_game.Items[ItemType.DarkWorldSouthAccess]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.LightWorldAccess]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainExitAccess]);
                    itemReqs.Add(_game.Items[ItemType.RaceGameAccess]);
                    itemReqs.Add(_game.Items[ItemType.DesertLeftAccess]);
                    itemReqs.Add(_game.Items[ItemType.DesertBackAccess]);
                    itemReqs.Add(_game.Items[ItemType.HyruleCastleSecondFloorAccess]);
                    itemReqs.Add(_game.Items[ItemType.LakeHyliaFairyIslandAccess]);
                    itemReqs.Add(_game.Items[ItemType.WaterfallFairyAccess]);

                    break;
                case RegionID.DarkWorldSouth:

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            //  Access via exits in the region
                            if (_game.Items.Has(ItemType.DarkWorldSouthAccess))
                                return AccessibilityLevel.Normal;

                            //  Access via Dark World West exits
                            if (_game.Items.Has(ItemType.DarkWorldWestAccess) ||
                                _game.Items.Has(ItemType.BumperCaveAccess))
                                return AccessibilityLevel.Normal;

                            if (_game.Items.Has(ItemType.MoonPearl))
                            {
                                //  Access via Kakariko Village or north of Swamp Palace portal
                                if (_game.Items.Has(ItemType.Gloves, 2) || (_game.Items.Has(ItemType.Hammer) &&
                                _game.Items.Has(ItemType.Gloves)))
                                    return AccessibilityLevel.Normal;

                                //  Access via Hammer House exit
                                if (_game.Items.Has(ItemType.HammerHouseAccess) && _game.Items.Has(ItemType.Hammer))
                                    return AccessibilityLevel.Normal;

                                //  Access via Dark World Witch Area exit
                                if (_game.Items.Has(ItemType.DarkWorldWitchAreaAccess) &&
                                    (_game.Items.Has(ItemType.Hammer) || _game.Items.Has(ItemType.Hookshot)))
                                    return AccessibilityLevel.Normal;

                                //  Access via East Dark World exits or Agahnim portal
                                if ((_game.Items.Has(ItemType.DarkWorldEastAccess) ||
                                    _game.Items.Has(ItemType.Aga)) && (_game.Items.Has(ItemType.Hammer) ||
                                    ((_game.Items.Has(ItemType.Gloves) || _game.Items.Has(ItemType.Flippers)) &&
                                    _game.Items.Has(ItemType.Hookshot))))
                                    return AccessibilityLevel.Normal;

                                //  Access via Southeast Dark World exits
                                if (_game.Items.Has(ItemType.DarkWorldSouthEastAccess) &&
                                    _game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Flippers))
                                    return AccessibilityLevel.Normal;

                                //  Access via East Dark World exits or Agahnim portal (Fake Flippers)
                                if ((_game.Items.Has(ItemType.DarkWorldEastAccess) ||
                                    _game.Items.Has(ItemType.Aga)) && _game.Items.Has(ItemType.Hookshot))
                                    return AccessibilityLevel.SequenceBreak;

                                //  Access via Southeast Dark World exits (Fake Flippers)
                                if (_game.Items.Has(ItemType.DarkWorldSouthEastAccess) &&
                                    _game.Items.Has(ItemType.Hammer))
                                    return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        //  Access from the start in Inverted mode
                        if (_game.Mode.WorldState == WorldState.Inverted)
                            return AccessibilityLevel.Normal;

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.DarkWorldSouthAccess]);
                    itemReqs.Add(_game.Items[ItemType.DarkWorldWestAccess]);
                    itemReqs.Add(_game.Items[ItemType.BumperCaveAccess]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.HammerHouseAccess]);
                    itemReqs.Add(_game.Items[ItemType.DarkWorldWitchAreaAccess]);
                    itemReqs.Add(_game.Items[ItemType.Hookshot]);
                    itemReqs.Add(_game.Items[ItemType.DarkWorldEastAccess]);
                    itemReqs.Add(_game.Items[ItemType.Aga]);
                    itemReqs.Add(_game.Items[ItemType.Flippers]);
                    itemReqs.Add(_game.Items[ItemType.DarkWorldSouthEastAccess]);

                    break;
                case RegionID.DarkWorldEast:

                    GetAccessibility = () =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.DarkWorldEastAccess))
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            //  Access via Agahnim portal
                            if (_game.Items.Has(ItemType.Aga))
                                return AccessibilityLevel.Normal;

                            if (_game.Items.Has(ItemType.MoonPearl))
                            {
                                //  Access via Eastern Palace portal
                                if (_game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.Hammer))
                                    return AccessibilityLevel.Normal;

                                //  Access via Hammer House exit
                                if (_game.Items.Has(ItemType.HammerHouseAccess) && _game.Items.Has(ItemType.Hammer))
                                    return AccessibilityLevel.Normal;

                                //  Access via Dark World Witch Area exit
                                if (_game.Items.Has(ItemType.DarkWorldWitchAreaAccess) &&
                                    (_game.Items.Has(ItemType.Hammer) || _game.Items.Has(ItemType.Gloves) ||
                                    (_game.Items.Has(ItemType.Flippers) && _game.Items.Has(ItemType.Hookshot))))
                                    return AccessibilityLevel.Normal;

                                //  Access via Kakariko Village portal
                                if (_game.Items.Has(ItemType.Gloves, 2) && _game.Items.Has(ItemType.Flippers))
                                    return AccessibilityLevel.Normal;

                                //  Access via South Dark World exits
                                if (_game.Items.Has(ItemType.DarkWorldSouthAccess) &&
                                    (_game.Items.Has(ItemType.Hammer) || _game.Items.Has(ItemType.Flippers)))
                                    return AccessibilityLevel.Normal;

                                //  Access via Southeast Dark World exits
                                if (_game.Items.Has(ItemType.DarkWorldSouthEastAccess) &&
                                    _game.Items.Has(ItemType.Flippers))
                                    return AccessibilityLevel.Normal;

                                //  Access via West Dark World exits (Fake Flippers)
                                if (_game.Items.Has(ItemType.DarkWorldWestAccess) ||
                                    _game.Items.Has(ItemType.BumperCaveAccess))
                                    return AccessibilityLevel.SequenceBreak;

                                //  Access via Dark World Witch Area exit (Fake Flippers)
                                if (_game.Items.Has(ItemType.DarkWorldWitchAreaAccess) &&
                                    _game.Items.Has(ItemType.Hookshot))
                                    return AccessibilityLevel.SequenceBreak;

                                //  Access via Kakariko Village portal (Fake Flippers)
                                if (_game.Items.Has(ItemType.Gloves, 2))
                                    return AccessibilityLevel.SequenceBreak;

                                //  Access via South Dark World exits (Fake Flippers)
                                if (_game.Items.Has(ItemType.DarkWorldSouthAccess))
                                    return AccessibilityLevel.SequenceBreak;

                                //  Access via Southeast Dark World exits (Fake Flippers)
                                if (_game.Items.Has(ItemType.DarkWorldSouthEastAccess))
                                    return AccessibilityLevel.SequenceBreak;

                                //  Access via West Dark World exits
                                if (_game.Items.Has(ItemType.DarkWorldWestAccess) ||
                                    _game.Items.Has(ItemType.BumperCaveAccess))
                                    return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            //  Access via Dark World Witch Area
                            if (_game.Items.Has(ItemType.DarkWorldWitchAreaAccess) &&
                                _game.Items.Has(ItemType.Gloves))
                                return AccessibilityLevel.Normal;

                            //  Access via South Dark World
                            if (_game.Items.Has(ItemType.Hammer) || _game.Items.Has(ItemType.Flippers))
                                return AccessibilityLevel.Normal;

                            if (_game.Items.Has(ItemType.Mirror))
                            {
                                //  Access via Agahnim portal
                                if (_game.Items.Has(ItemType.Aga))
                                    return AccessibilityLevel.Normal;

                                //  Access via exits in the region
                                if (_game.Items.Has(ItemType.LightWorldAccess) ||
                                    _game.Items.Has(ItemType.DeathMountainExitAccess) ||
                                    _game.Items.Has(ItemType.RaceGameAccess) ||
                                    _game.Items.Has(ItemType.HyruleCastleSecondFloorAccess) ||
                                    _game.Items.Has(ItemType.DesertLeftAccess))
                                    return AccessibilityLevel.Normal;

                                if (_game.Items.Has(ItemType.MoonPearl))
                                {
                                    //  Access via either Village of Outcasts portal, Palace of Darkness portal,
                                    //    or portal north of Swamp Palace
                                    if (_game.Items.Has(ItemType.Gloves, 2))
                                        return AccessibilityLevel.Normal;

                                    //  Access via Desert Palace back exit
                                    if (_game.Items.Has(ItemType.DesertBackAccess) && _game.Items.Has(ItemType.Gloves))
                                        return AccessibilityLevel.Normal;
                                }
                            }

                            //  Fake Flippers
                            return AccessibilityLevel.SequenceBreak;
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.DarkWorldEastAccess]);
                    itemReqs.Add(_game.Items[ItemType.Aga]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.HammerHouseAccess]);
                    itemReqs.Add(_game.Items[ItemType.Flippers]);
                    itemReqs.Add(_game.Items[ItemType.Hookshot]);
                    itemReqs.Add(_game.Items[ItemType.DarkWorldSouthAccess]);
                    itemReqs.Add(_game.Items[ItemType.DarkWorldSouthEastAccess]);
                    itemReqs.Add(_game.Items[ItemType.DarkWorldWestAccess]);
                    itemReqs.Add(_game.Items[ItemType.BumperCaveAccess]);
                    itemReqs.Add(_game.Items[ItemType.DarkWorldWitchAreaAccess]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.LightWorldAccess]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainExitAccess]);
                    itemReqs.Add(_game.Items[ItemType.RaceGameAccess]);
                    itemReqs.Add(_game.Items[ItemType.HyruleCastleSecondFloorAccess]);
                    itemReqs.Add(_game.Items[ItemType.DesertLeftAccess]);
                    itemReqs.Add(_game.Items[ItemType.DesertBackAccess]);

                    break;
                case RegionID.DarkWorldSouthEast:

                    GetAccessibility = () =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.DarkWorldSouthEastAccess))
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                            {
                                if (_game.Items.Has(ItemType.Flippers))
                                {
                                    //  Access via Agahnim portal
                                    if (_game.Items.Has(ItemType.Aga))
                                        return AccessibilityLevel.Normal;

                                    //  Access via Eastern Palace, north of Swamp Palace,
                                    //    or Kakariko Village portal
                                    if (_game.Items.Has(ItemType.Gloves, 2) && (_game.Items.Has(ItemType.Gloves) ||
                                        _game.Items.Has(ItemType.Hammer)))
                                        return AccessibilityLevel.Normal;

                                    //  Access via South Dark World exits
                                    if (_game.Items.Has(ItemType.DarkWorldSouthAccess))
                                        return AccessibilityLevel.Normal;

                                    //  Access via East Dark World exits
                                    if (_game.Items.Has(ItemType.DarkWorldEastAccess))
                                        return AccessibilityLevel.Normal;

                                    //  Access via Dark World Witch Area exit
                                    if (_game.Items.Has(ItemType.DarkWorldWitchAreaAccess) &&
                                        (_game.Items.Has(ItemType.Gloves) || _game.Items.Has(ItemType.Hammer)))
                                        return AccessibilityLevel.Normal;

                                    //  Access via West Dark World exits
                                    if (_game.Items.Has(ItemType.DarkWorldWestAccess) ||
                                        _game.Items.Has(ItemType.BumperCaveAccess))
                                        return AccessibilityLevel.Normal;

                                    //  Access via Hammer House exit
                                    if (_game.Items.Has(ItemType.HammerHouseAccess) &&
                                        _game.Items.Has(ItemType.Hammer))
                                        return AccessibilityLevel.Normal;
                                }

                                //  Access via Agahnim portal
                                if (_game.Items.Has(ItemType.Aga))
                                    return AccessibilityLevel.SequenceBreak;

                                //  Access via Eastern Palace, north of Swamp Palace,
                                //    or Kakariko Village portal (Fake Flippers)
                                if (_game.Items.Has(ItemType.Gloves, 2) && (_game.Items.Has(ItemType.Gloves) ||
                                    _game.Items.Has(ItemType.Hammer)))
                                    return AccessibilityLevel.SequenceBreak;

                                //  Access via South Dark World exits (Fake Flippers)
                                if (_game.Items.Has(ItemType.DarkWorldSouthAccess))
                                    return AccessibilityLevel.SequenceBreak;

                                //  Access via East Dark World exits (Fake Flippers)
                                if (_game.Items.Has(ItemType.DarkWorldEastAccess))
                                    return AccessibilityLevel.SequenceBreak;

                                //  Access via Dark World Witch Area exit (Fake Flippers)
                                if (_game.Items.Has(ItemType.DarkWorldWitchAreaAccess) &&
                                    (_game.Items.Has(ItemType.Gloves) || _game.Items.Has(ItemType.Hammer)))
                                    return AccessibilityLevel.SequenceBreak;

                                //  Access via West Dark World exits (Fake Flippers)
                                if (_game.Items.Has(ItemType.DarkWorldWestAccess) ||
                                    _game.Items.Has(ItemType.BumperCaveAccess))
                                    return AccessibilityLevel.SequenceBreak;

                                //  Access via Hammer House exit (Fake Flippers)
                                if (_game.Items.Has(ItemType.HammerHouseAccess) &&
                                    _game.Items.Has(ItemType.Hammer))
                                    return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Flippers))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.SequenceBreak;
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.DarkWorldSouthEastAccess]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Flippers]);
                    itemReqs.Add(_game.Items[ItemType.Aga]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.DarkWorldSouthAccess]);
                    itemReqs.Add(_game.Items[ItemType.DarkWorldEastAccess]);
                    itemReqs.Add(_game.Items[ItemType.DarkWorldWitchAreaAccess]);
                    itemReqs.Add(_game.Items[ItemType.DarkWorldWestAccess]);
                    itemReqs.Add(_game.Items[ItemType.BumperCaveAccess]);
                    itemReqs.Add(_game.Items[ItemType.HammerHouseAccess]);

                    break;
                case RegionID.DarkWorldWitchArea:

                    GetAccessibility = () =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.DarkWorldWitchAreaAccess))
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                            {
                                //  Access via Eastern Palace portal
                                if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Gloves))
                                    return AccessibilityLevel.Normal;

                                //  Access via Hammer House exit
                                if (_game.Items.Has(ItemType.HammerHouseAccess) && _game.Items.Has(ItemType.Hammer))
                                    return AccessibilityLevel.Normal;

                                //  Access via Kakariko Village portal
                                if (_game.Items.Has(ItemType.Gloves, 2) && _game.Items.Has(ItemType.Flippers))
                                    return AccessibilityLevel.Normal;

                                //  Access via East Dark World exits or Agahnim portal
                                if ((_game.Items.Has(ItemType.DarkWorldEastAccess) ||
                                    _game.Items.Has(ItemType.Aga)) && (_game.Items.Has(ItemType.Hammer) ||
                                    _game.Items.Has(ItemType.Gloves) || _game.Items.Has(ItemType.Flippers)))
                                    return AccessibilityLevel.Normal;

                                //  Access via South Dark World exits
                                if (_game.Items.Has(ItemType.DarkWorldSouthAccess) &&
                                    (_game.Items.Has(ItemType.Hammer) || _game.Items.Has(ItemType.Flippers)))
                                    return AccessibilityLevel.Normal;

                                //  Access via Southeast Dark World exits
                                if (_game.Items.Has(ItemType.DarkWorldSouthEastAccess) &&
                                    _game.Items.Has(ItemType.Flippers))
                                    return AccessibilityLevel.Normal;

                                //  Access via West Dark World exits
                                if ((_game.Items.Has(ItemType.DarkWorldWestAccess) ||
                                    _game.Items.Has(ItemType.BumperCaveAccess)) &&
                                    (_game.Items.Has(ItemType.Hammer) || _game.Items.Has(ItemType.Flippers)))
                                    return AccessibilityLevel.Normal;

                                //  Access via Kakariko Village portal (Fake Flippers)
                                if (_game.Items.Has(ItemType.Gloves, 2))
                                    return AccessibilityLevel.SequenceBreak;

                                //  Access via East Dark World exits or Agahnim portal (Fake Flippers)
                                if (_game.Items.Has(ItemType.DarkWorldEastAccess) || _game.Items.Has(ItemType.Aga))
                                    return AccessibilityLevel.SequenceBreak;

                                //  Access via South Dark World exits (Fake Flippers)
                                if (_game.Items.Has(ItemType.DarkWorldSouthAccess))
                                    return AccessibilityLevel.SequenceBreak;

                                //  Access via Southeast Dark World exits (Fake Flippers)
                                if (_game.Items.Has(ItemType.DarkWorldSouthEastAccess))
                                    return AccessibilityLevel.SequenceBreak;

                                //  Access via West Dark World exits (Fake Flippers)
                                if (_game.Items.Has(ItemType.DarkWorldWestAccess) ||
                                    _game.Items.Has(ItemType.BumperCaveAccess))
                                    return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            //  Access via South Dark World
                            if (_game.Items.Has(ItemType.Hammer) || _game.Items.Has(ItemType.Flippers))
                                return AccessibilityLevel.Normal;

                            //  Access via Light World exits or Village of Outcasts portal
                            if ((_game.Items.Has(ItemType.LightWorldAccess) ||
                                _game.Items.Has(ItemType.DeathMountainExitAccess) ||
                                _game.Items.Has(ItemType.RaceGameAccess) ||
                                _game.Items.Has(ItemType.HyruleCastleSecondFloorAccess) ||
                                _game.Items.Has(ItemType.DesertLeftAccess) ||
                                _game.Items.Has(ItemType.Gloves, 2)) &&
                                _game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Mirror))
                                return AccessibilityLevel.Normal;

                            //  Access via Desert Palace back exit
                            if (_game.Items.Has(ItemType.DesertBackAccess) && _game.Items.Has(ItemType.MoonPearl) &&
                                _game.Items.Has(ItemType.Gloves))
                                return AccessibilityLevel.Normal;

                            //  Access via East Dark World exits
                            if (_game.Items.Has(ItemType.DarkWorldEastAccess) &&
                                _game.Items.Has(ItemType.Gloves))
                                return AccessibilityLevel.Normal;

                            //  Fake Flippers
                            return AccessibilityLevel.SequenceBreak;
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.DarkWorldWitchAreaAccess]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.HammerHouseAccess]);
                    itemReqs.Add(_game.Items[ItemType.Flippers]);
                    itemReqs.Add(_game.Items[ItemType.DarkWorldEastAccess]);
                    itemReqs.Add(_game.Items[ItemType.Aga]);
                    itemReqs.Add(_game.Items[ItemType.DarkWorldSouthAccess]);
                    itemReqs.Add(_game.Items[ItemType.DarkWorldSouthEastAccess]);
                    itemReqs.Add(_game.Items[ItemType.DarkWorldWestAccess]);
                    itemReqs.Add(_game.Items[ItemType.BumperCaveAccess]);
                    itemReqs.Add(_game.Items[ItemType.LightWorldAccess]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainExitAccess]);
                    itemReqs.Add(_game.Items[ItemType.RaceGameAccess]);
                    itemReqs.Add(_game.Items[ItemType.HyruleCastleSecondFloorAccess]);
                    itemReqs.Add(_game.Items[ItemType.DesertLeftAccess]);
                    itemReqs.Add(_game.Items[ItemType.DesertBackAccess]);
                    itemReqs.Add(_game.Items[ItemType.DarkWorldEastAccess]);

                    break;
                case RegionID.MireArea:

                    GetAccessibility = () =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.MireAreaAccess))
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            //  Access via flute-accessible heavy rock portal
                            if (_game.Items.Has(ItemType.Flute) && _game.Items.Has(ItemType.Gloves, 2))
                                return AccessibilityLevel.Normal;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            //  Access either via flute drop point or mirror from Light World
                            if ((_game.Items.Has(ItemType.Flute) && _game.Items.Has(ItemType.MoonPearl)) ||
                                _game.Items.Has(ItemType.Mirror))
                            {
                                //  Access via Agahnim portal
                                if (_game.Items.Has(ItemType.Aga))
                                    return AccessibilityLevel.Normal;

                                //  Access via exits in the region
                                if (_game.Items.Has(ItemType.LightWorldAccess) ||
                                    _game.Items.Has(ItemType.DeathMountainExitAccess) ||
                                    _game.Items.Has(ItemType.RaceGameAccess) ||
                                    _game.Items.Has(ItemType.HyruleCastleSecondFloorAccess) ||
                                    _game.Items.Has(ItemType.DesertLeftAccess))
                                    return AccessibilityLevel.Normal;

                                if (_game.Items.Has(ItemType.MoonPearl))
                                {

                                    //  Access via either Village of Outcasts portal, Palace of Darkness portal,
                                    //    or portal north of Swamp Palace
                                    if (_game.Items.Has(ItemType.Gloves, 2) ||
                                        (_game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.Hammer)))
                                        return AccessibilityLevel.Normal;

                                    //  Access via Desert Palace back exit
                                    if (_game.Items.Has(ItemType.DesertBackAccess) && _game.Items.Has(ItemType.Gloves))
                                        return AccessibilityLevel.Normal;

                                    //  Access via Lake Hylia capacity upgrade fairy island
                                    if (_game.Items.Has(ItemType.LakeHyliaFairyIslandAccess) &&
                                        _game.Items.Has(ItemType.Flippers))
                                        return AccessibilityLevel.Normal;

                                    //  Access via Waterfall Fairy exit
                                    if (_game.Items.Has(ItemType.WaterfallFairyAccess) &&
                                        _game.Items.Has(ItemType.Flippers))
                                        return AccessibilityLevel.Normal;

                                    //  Access via Lake Hylia capacity upgrade fairy island (Fake Flippers)
                                    if (_game.Items.Has(ItemType.LakeHyliaFairyIslandAccess))
                                        return AccessibilityLevel.SequenceBreak;

                                    //  Access via Waterfall Fairy exit (Fake Flippers)
                                    if (_game.Items.Has(ItemType.WaterfallFairyAccess))
                                        return AccessibilityLevel.SequenceBreak;

                                }
                            }
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MireAreaAccess]);
                    itemReqs.Add(_game.Items[ItemType.Flute]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.Aga]);
                    itemReqs.Add(_game.Items[ItemType.LightWorldAccess]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainExitAccess]);
                    itemReqs.Add(_game.Items[ItemType.RaceGameAccess]);
                    itemReqs.Add(_game.Items[ItemType.HyruleCastleSecondFloorAccess]);
                    itemReqs.Add(_game.Items[ItemType.DesertLeftAccess]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.DesertBackAccess]);
                    itemReqs.Add(_game.Items[ItemType.LakeHyliaFairyIslandAccess]);
                    itemReqs.Add(_game.Items[ItemType.Flippers]);
                    itemReqs.Add(_game.Items[ItemType.WaterfallFairyAccess]);

                    break;
                case RegionID.DeathMountainWestBottom:

                    GetAccessibility = () =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.DeathMountainWestBottomAccess) ||
                            _game.Items.Has(ItemType.DeathMountainWestTopAccess))
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            //  Access via flute drop point
                            if (_game.Items.Has(ItemType.Flute))
                                return AccessibilityLevel.Normal;

                            //  Access via hookshot and East Death Mountain exits
                            if ((_game.Items.Has(ItemType.DeathMountainEastTopAccess) ||
                                _game.Items.Has(ItemType.DeathMountainEastTopConnectorAccess) ||
                                _game.Items.Has(ItemType.DeathMountainEastBottomAccess)) &&
                                _game.Items.Has(ItemType.Hookshot))
                                return AccessibilityLevel.Normal;

                            //  Access via hammer and East Death Mountain top exits
                            if (_game.Items.Has(ItemType.DeathMountainEastTopAccess) &&
                                _game.Items.Has(ItemType.Hammer))
                                return AccessibilityLevel.Normal;

                            //  Access via Dark Death Mountain exits
                            if ((_game.Items.Has(ItemType.DarkDeathMountainWestBottomAccess) ||
                                _game.Items.Has(ItemType.DarkDeathMountainTopAccess) ||
                                _game.Items.Has(ItemType.DarkDeathMountainFloatingIslandAccess)) &&
                                _game.Items.Has(ItemType.Mirror))
                                return AccessibilityLevel.Normal;

                            //  Access via Turtle Rock exits
                            if ((_game.Items.Has(ItemType.TurtleRockTunnelAccess) ||
                                _game.Items.Has(ItemType.TurtleRockSafetyDoorAccess)) &&
                                _game.Items.Has(ItemType.Mirror) && _game.Items.Has(ItemType.Hookshot))
                                return AccessibilityLevel.Normal;

                            //  Access via Death Mountain entrance cave (non-entrance)
                            if (!_game.Mode.EntranceShuffle.Value && _game.Items.Has(ItemType.Gloves))
                            {
                                //  Lamp required by logic
                                if (_game.Items.Has(ItemType.Lamp))
                                    return AccessibilityLevel.Normal;

                                //  Sequence breaking dark room
                                return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            //  Access via Dark Death Mountain exits via West DDM portal
                            if (_game.Items.Has(ItemType.DarkDeathMountainFloatingIslandAccess) ||
                                _game.Items.Has(ItemType.DarkDeathMountainTopAccess) ||
                                _game.Items.Has(ItemType.DarkDeathMountainWestBottomAccess))
                                return AccessibilityLevel.Normal;

                            if (_game.Items.Has(ItemType.MoonPearl))
                            {
                                //  Access via East Death Mountain by hookshot
                                if ((_game.Items.Has(ItemType.DeathMountainEastTopAccess) ||
                                    _game.Items.Has(ItemType.DeathMountainEastTopConnectorAccess) ||
                                    _game.Items.Has(ItemType.DeathMountainEastBottomAccess)) &&
                                    _game.Items.Has(ItemType.Hookshot))
                                    return AccessibilityLevel.Normal;

                                //  Access via East Dark Death Mountain portal and hookshot
                                if (_game.Items.Has(ItemType.DarkDeathMountainEastBottomAccess) &&
                                    _game.Items.Has(ItemType.Hookshot) && _game.Items.Has(ItemType.Gloves, 2))
                                    return AccessibilityLevel.Normal;

                                //  Access via East Death Mountain by hammer
                                if (_game.Items.Has(ItemType.DeathMountainEastTopAccess) &&
                                    _game.Items.Has(ItemType.Hammer))
                                    return AccessibilityLevel.Normal;

                                //  Access via flute drop to West Dark Death Mountain portal
                                if (_game.Items.Has(ItemType.Flute))
                                {
                                    //  Light World access via Agahnim portal
                                    if (_game.Items.Has(ItemType.Aga))
                                        return AccessibilityLevel.Normal;

                                    //  Light World access via exits in the region
                                    if (_game.Items.Has(ItemType.LightWorldAccess) ||
                                        _game.Items.Has(ItemType.DeathMountainExitAccess) ||
                                        _game.Items.Has(ItemType.RaceGameAccess) ||
                                        _game.Items.Has(ItemType.HyruleCastleSecondFloorAccess) ||
                                        _game.Items.Has(ItemType.DesertLeftAccess))
                                        return AccessibilityLevel.Normal;

                                    //  Light World access via either Village of Outcasts portal,
                                    //    Palace of Darkness portal, or portal north of Swamp Palace
                                    if (_game.Items.Has(ItemType.Gloves, 2) ||
                                        (_game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.Hammer)))
                                        return AccessibilityLevel.Normal;

                                    //  Light World access via Desert Palace back exit
                                    if (_game.Items.Has(ItemType.DesertBackAccess) && _game.Items.Has(ItemType.Gloves))
                                        return AccessibilityLevel.Normal;

                                    //  Light World access via Lake Hylia capacity upgrade fairy island
                                    if (_game.Items.Has(ItemType.LakeHyliaFairyIslandAccess) &&
                                        _game.Items.Has(ItemType.Flippers))
                                        return AccessibilityLevel.Normal;

                                    //  Light World access via Waterfall Fairy exit
                                    if (_game.Items.Has(ItemType.WaterfallFairyAccess) &&
                                        _game.Items.Has(ItemType.Flippers))
                                        return AccessibilityLevel.Normal;

                                    //  Light World access via Lake Hylia capacity upgrade fairy island (Fake Flippers)
                                    if (_game.Items.Has(ItemType.LakeHyliaFairyIslandAccess))
                                        return AccessibilityLevel.SequenceBreak;

                                    //  Light World access via Waterfall Fairy exit (Fake Flippers)
                                    if (_game.Items.Has(ItemType.WaterfallFairyAccess))
                                        return AccessibilityLevel.SequenceBreak;
                                }
                            }

                            //  Access via Death Mountain Access cave and portal
                            if (!_game.Mode.EntranceShuffle.Value)
                            {
                                if (_game.Items.Has(ItemType.Gloves))
                                {
                                    //  Lamp required by logic
                                    if (_game.Items.Has(ItemType.Lamp))
                                        return AccessibilityLevel.Normal;

                                    //  Sequence break dark room
                                    return AccessibilityLevel.SequenceBreak;
                                }
                            }
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.DeathMountainWestBottomAccess]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainWestTopAccess]);
                    itemReqs.Add(_game.Items[ItemType.Flute]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainEastTopAccess]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainEastTopConnectorAccess]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainEastBottomAccess]);
                    itemReqs.Add(_game.Items[ItemType.Hookshot]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.DarkDeathMountainWestBottomAccess]);
                    itemReqs.Add(_game.Items[ItemType.DarkDeathMountainTopAccess]);
                    itemReqs.Add(_game.Items[ItemType.DarkDeathMountainFloatingIslandAccess]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.TurtleRockTunnelAccess]);
                    itemReqs.Add(_game.Items[ItemType.TurtleRockSafetyDoorAccess]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.Lamp]);
                    itemReqs.Add(_game.Items[ItemType.DarkDeathMountainEastBottomAccess]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.LightWorldAccess]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainExitAccess]);
                    itemReqs.Add(_game.Items[ItemType.RaceGameAccess]);
                    itemReqs.Add(_game.Items[ItemType.HyruleCastleSecondFloorAccess]);
                    itemReqs.Add(_game.Items[ItemType.DesertLeftAccess]);
                    itemReqs.Add(_game.Items[ItemType.DesertBackAccess]);
                    itemReqs.Add(_game.Items[ItemType.LakeHyliaFairyIslandAccess]);
                    itemReqs.Add(_game.Items[ItemType.WaterfallFairyAccess]);
                    itemReqs.Add(_game.Items[ItemType.Flippers]);

                    break;
                case RegionID.DeathMountainWestTop:

                    GetAccessibility = () =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.DeathMountainWestTopAccess))
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            //  Access via East Death Mountain via hammer
                            if (_game.Items.Has(ItemType.DeathMountainEastTopAccess) &&
                                _game.Items.Has(ItemType.Hammer))
                                return AccessibilityLevel.Normal;

                            if (_game.Items.Has(ItemType.Mirror))
                            {
                                //  Access via West Dark Death Mountaing exits via mirror
                                if (_game.Items.Has(ItemType.DarkDeathMountainWestBottomAccess))
                                    return AccessibilityLevel.Normal;

                                //  Access via Dark Death Mountain top exits via mirror
                                if (_game.Items.Has(ItemType.DarkDeathMountainTopAccess) ||
                                    _game.Items.Has(ItemType.DarkDeathMountainFloatingIslandAccess))
                                    return AccessibilityLevel.Normal;

                                //  Access via East Death Mountain exits via hookshot and mirror
                                if ((_game.Items.Has(ItemType.DeathMountainEastTopAccess) ||
                                    _game.Items.Has(ItemType.DeathMountainEastTopConnectorAccess) ||
                                    _game.Items.Has(ItemType.DeathMountainEastBottomAccess) ||
                                    _game.Items.Has(ItemType.TurtleRockTunnelAccess) ||
                                    _game.Items.Has(ItemType.TurtleRockSafetyDoorAccess) ||
                                    _game.Items.Has(ItemType.DarkDeathMountainEastBottomAccess)) &&
                                    _game.Items.Has(ItemType.Hookshot))
                                    return AccessibilityLevel.Normal;
                            }

                            if (_game.Items.Has(ItemType.Mirror) || (_game.Items.Has(ItemType.Hookshot) &&
                                _game.Items.Has(ItemType.Hammer)))
                            {
                                //  Access via flute drop spot and mirror
                                if (_game.Items.Has(ItemType.Flute))
                                    return AccessibilityLevel.Normal;

                                //  Access via West Death Mountain bottom exits
                                if (_game.Items.Has(ItemType.DeathMountainWestBottomAccess))
                                    return AccessibilityLevel.Normal;

                                //  Access via Death Mountain access cave
                                if (!_game.Mode.EntranceShuffle.Value && _game.Items.Has(ItemType.Gloves))
                                {
                                    //  Lamp required by logic
                                    if (_game.Items.Has(ItemType.Lamp))
                                        return AccessibilityLevel.Normal;

                                    //  Sequence break dark room
                                    return AccessibilityLevel.SequenceBreak;
                                }
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Hammer))
                            {
                                //  Access via Dark Death Mountain top exits
                                if (_game.Items.Has(ItemType.DarkDeathMountainFloatingIslandAccess) ||
                                    _game.Items.Has(ItemType.DarkDeathMountainTopAccess))
                                    return AccessibilityLevel.Normal;

                                //  Access via East Death Mountain top exits
                                if (_game.Items.Has(ItemType.DeathMountainEastTopAccess))
                                    return AccessibilityLevel.Normal;

                                //  Access via Flute drop spot
                                if (_game.Items.Has(ItemType.Flute))
                                {
                                    //  Light World access via Agahnim portal
                                    if (_game.Items.Has(ItemType.Aga))
                                        return AccessibilityLevel.Normal;

                                    //  Light World access via exits in the region
                                    if (_game.Items.Has(ItemType.LightWorldAccess) ||
                                        _game.Items.Has(ItemType.DeathMountainExitAccess) ||
                                        _game.Items.Has(ItemType.RaceGameAccess) ||
                                        _game.Items.Has(ItemType.HyruleCastleSecondFloorAccess) ||
                                        _game.Items.Has(ItemType.DesertLeftAccess))
                                        return AccessibilityLevel.Normal;

                                    //  Light World access via either Village of Outcasts portal,
                                    //    Palace of Darkness portal, or portal north of Swamp Palace
                                    if (_game.Items.Has(ItemType.Gloves))
                                        return AccessibilityLevel.Normal;

                                    //  Light World access via Desert Palace back exit
                                    if (_game.Items.Has(ItemType.DesertBackAccess) && _game.Items.Has(ItemType.Gloves))
                                        return AccessibilityLevel.Normal;

                                    //  Light World access via Lake Hylia capacity upgrade fairy island
                                    if (_game.Items.Has(ItemType.LakeHyliaFairyIslandAccess) &&
                                        _game.Items.Has(ItemType.Flippers))
                                        return AccessibilityLevel.Normal;

                                    //  Light World access via Waterfall Fairy exit
                                    if (_game.Items.Has(ItemType.WaterfallFairyAccess) &&
                                        _game.Items.Has(ItemType.Flippers))
                                        return AccessibilityLevel.Normal;

                                    //  Light World access via Lake Hylia capacity upgrade fairy island (Fake Flippers)
                                    if (_game.Items.Has(ItemType.LakeHyliaFairyIslandAccess))
                                        return AccessibilityLevel.SequenceBreak;

                                    //  Light World access via Waterfall Fairy exit (Fake Flippers)
                                    if (_game.Items.Has(ItemType.WaterfallFairyAccess))
                                        return AccessibilityLevel.SequenceBreak;
                                }

                                //  Access via Death Mountain access cave
                                if (!_game.Mode.EntranceShuffle.Value && _game.Items.Has(ItemType.Gloves))
                                {
                                    //  Lamp required by logic
                                    if (_game.Items.Has(ItemType.Lamp))
                                        return AccessibilityLevel.Normal;

                                    //  Sequence break dark room
                                    return AccessibilityLevel.SequenceBreak;
                                }
                            }
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.DeathMountainWestTopAccess]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainEastTopAccess]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.DarkDeathMountainWestBottomAccess]);
                    itemReqs.Add(_game.Items[ItemType.DarkDeathMountainTopAccess]);
                    itemReqs.Add(_game.Items[ItemType.DarkDeathMountainFloatingIslandAccess]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainEastTopAccess]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainEastTopConnectorAccess]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainEastBottomAccess]);
                    itemReqs.Add(_game.Items[ItemType.TurtleRockTunnelAccess]);
                    itemReqs.Add(_game.Items[ItemType.TurtleRockSafetyDoorAccess]);
                    itemReqs.Add(_game.Items[ItemType.DarkDeathMountainEastBottomAccess]);
                    itemReqs.Add(_game.Items[ItemType.Hookshot]);
                    itemReqs.Add(_game.Items[ItemType.Flute]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainWestBottomAccess]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.Lamp]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Aga]);
                    itemReqs.Add(_game.Items[ItemType.LightWorldAccess]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainExitAccess]);
                    itemReqs.Add(_game.Items[ItemType.RaceGameAccess]);
                    itemReqs.Add(_game.Items[ItemType.HyruleCastleSecondFloorAccess]);
                    itemReqs.Add(_game.Items[ItemType.DesertLeftAccess]);
                    itemReqs.Add(_game.Items[ItemType.DesertBackAccess]);
                    itemReqs.Add(_game.Items[ItemType.LakeHyliaFairyIslandAccess]);
                    itemReqs.Add(_game.Items[ItemType.Flippers]);
                    itemReqs.Add(_game.Items[ItemType.WaterfallFairyAccess]);

                    break;
                case RegionID.DeathMountainEastBottom:

                    GetAccessibility = () =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.DeathMountainEastTopConnectorAccess) ||
                            _game.Items.Has(ItemType.DeathMountainEastBottomAccess) ||
                            _game.Items.Has(ItemType.DeathMountainEastTopAccess))
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            //  Access via West Death Mountain top exits
                            if (_game.Items.Has(ItemType.DeathMountainWestTopAccess) &&
                                (_game.Items.Has(ItemType.Hammer) || _game.Items.Has(ItemType.Hookshot)))
                                return AccessibilityLevel.Normal;

                            if (_game.Items.Has(ItemType.Mirror))
                            {
                                //  Access via Turtle Rock exits and mirror
                                if (_game.Items.Has(ItemType.TurtleRockTunnelAccess) ||
                                    _game.Items.Has(ItemType.TurtleRockSafetyDoorAccess))
                                    return AccessibilityLevel.Normal;

                                //  Access via East Dark Death Mountain exits
                                if (_game.Items.Has(ItemType.DarkDeathMountainFloatingIslandAccess) ||
                                    _game.Items.Has(ItemType.DarkDeathMountainTopAccess) ||
                                    _game.Items.Has(ItemType.DarkDeathMountainEastBottomAccess))
                                    return AccessibilityLevel.Normal;

                                //  Access via West Dark Death Mountain exits
                                if (_game.Items.Has(ItemType.DarkDeathMountainWestBottomAccess) &&
                                    (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Hammer)))
                                    return AccessibilityLevel.Normal;
                            }

                            if ((_game.Items.Has(ItemType.Mirror) && _game.Items.Has(ItemType.Hammer)) ||
                                _game.Items.Has(ItemType.Hookshot))
                            {
                                //  Access via Flute drop spot
                                if (_game.Items.Has(ItemType.Flute))
                                    return AccessibilityLevel.Normal;

                                //  Access via West Death Mountain bottom exits
                                if (_game.Items.Has(ItemType.DeathMountainWestBottomAccess))
                                    return AccessibilityLevel.Normal;

                                //  Access via Death Mountain access cave (non-entrance)
                                if (!_game.Mode.EntranceShuffle.Value && _game.Items.Has(ItemType.Gloves))
                                {
                                    if (_game.Items.Has(ItemType.Lamp))
                                        return AccessibilityLevel.Normal;

                                    return AccessibilityLevel.SequenceBreak;
                                }
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            //  Access via West Death Mountain top exits
                            if (_game.Items.Has(ItemType.DeathMountainWestTopAccess) &&
                                _game.Items.Has(ItemType.MoonPearl) &&
                                (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Hammer)))
                                return AccessibilityLevel.Normal;

                            //  Access via East Dark Death Mountain bottom exits
                            if (_game.Items.Has(ItemType.DarkDeathMountainEastBottomAccess) &&
                                _game.Items.Has(ItemType.Gloves, 2))
                                return AccessibilityLevel.Normal;

                            if (((_game.Items.Has(ItemType.Hammer) || _game.Items.Has(ItemType.Hookshot)) &&
                                _game.Items.Has(ItemType.MoonPearl)) || _game.Items.Has(ItemType.Gloves, 2))
                            {
                                //  Access via Dark Death Mountain top exits
                                if (_game.Items.Has(ItemType.DarkDeathMountainFloatingIslandAccess) ||
                                    _game.Items.Has(ItemType.DarkDeathMountainTopAccess))
                                    return AccessibilityLevel.Normal;

                                //  Access via West Dark Death Mountain exits
                                if (_game.Items.Has(ItemType.DarkDeathMountainWestBottomAccess))
                                    return AccessibilityLevel.Normal;

                                //  Access via Flute drop spot
                                if (_game.Items.Has(ItemType.Flute))
                                {
                                    //  Light World access via Agahnim portal
                                    if (_game.Items.Has(ItemType.Aga))
                                        return AccessibilityLevel.Normal;

                                    //  Light World access via exits in the region
                                    if (_game.Items.Has(ItemType.LightWorldAccess) ||
                                        _game.Items.Has(ItemType.DeathMountainExitAccess) ||
                                        _game.Items.Has(ItemType.RaceGameAccess) ||
                                        _game.Items.Has(ItemType.HyruleCastleSecondFloorAccess) ||
                                        _game.Items.Has(ItemType.DesertLeftAccess))
                                        return AccessibilityLevel.Normal;
                                    //  Access via either Village of Outcasts portal,
                                    //    Palace of Darkness portal, or portal north of Swamp Palace
                                    if (_game.Items.Has(ItemType.Gloves, 2) ||
                                        (_game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.Hammer)))
                                        return AccessibilityLevel.Normal;

                                    //  Access via Desert Palace back exit
                                    if (_game.Items.Has(ItemType.DesertBackAccess) && _game.Items.Has(ItemType.Gloves))
                                        return AccessibilityLevel.Normal;

                                    //  Access via Lake Hylia capacity upgrade fairy island
                                    if (_game.Items.Has(ItemType.LakeHyliaFairyIslandAccess) &&
                                        _game.Items.Has(ItemType.Flippers))
                                        return AccessibilityLevel.Normal;

                                    //  Access via Waterfall Fairy exit
                                    if (_game.Items.Has(ItemType.WaterfallFairyAccess) &&
                                        _game.Items.Has(ItemType.Flippers))
                                        return AccessibilityLevel.Normal;

                                    //  Access via Lake Hylia capacity upgrade fairy island (Fake Flippers)
                                    if (_game.Items.Has(ItemType.LakeHyliaFairyIslandAccess))
                                        return AccessibilityLevel.SequenceBreak;

                                    //  Access via Waterfall Fairy exit (Fake Flippers)
                                    if (_game.Items.Has(ItemType.WaterfallFairyAccess))
                                        return AccessibilityLevel.SequenceBreak;
                                }

                                //  Access via Death Mountain access cave
                                if (!_game.Mode.EntranceShuffle.Value && _game.Items.Has(ItemType.Gloves))
                                {
                                    //  Lamp required by logic
                                    if (_game.Items.Has(ItemType.Lamp))
                                        return AccessibilityLevel.Normal;

                                    //  Sequence break dark room
                                    return AccessibilityLevel.SequenceBreak;
                                }
                            }
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.DeathMountainEastTopConnectorAccess]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainEastBottomAccess]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainEastTopAccess]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainWestTopAccess]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.Hookshot]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.TurtleRockTunnelAccess]);
                    itemReqs.Add(_game.Items[ItemType.TurtleRockSafetyDoorAccess]);
                    itemReqs.Add(_game.Items[ItemType.DarkDeathMountainFloatingIslandAccess]);
                    itemReqs.Add(_game.Items[ItemType.DarkDeathMountainTopAccess]);
                    itemReqs.Add(_game.Items[ItemType.DarkDeathMountainEastBottomAccess]);
                    itemReqs.Add(_game.Items[ItemType.DarkDeathMountainWestBottomAccess]);
                    itemReqs.Add(_game.Items[ItemType.Flute]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainWestBottomAccess]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.Lamp]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Aga]);
                    itemReqs.Add(_game.Items[ItemType.LightWorldAccess]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainExitAccess]);
                    itemReqs.Add(_game.Items[ItemType.RaceGameAccess]);
                    itemReqs.Add(_game.Items[ItemType.HyruleCastleSecondFloorAccess]);
                    itemReqs.Add(_game.Items[ItemType.DesertLeftAccess]);
                    itemReqs.Add(_game.Items[ItemType.DesertBackAccess]);
                    itemReqs.Add(_game.Items[ItemType.LakeHyliaFairyIslandAccess]);
                    itemReqs.Add(_game.Items[ItemType.Flippers]);
                    itemReqs.Add(_game.Items[ItemType.WaterfallFairyAccess]);

                    break;
                case RegionID.DeathMountainEastTop:

                    GetAccessibility = () =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.DeathMountainEastTopAccess))
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            //  Access via Dark Death Mountain top exits
                            if ((_game.Items.Has(ItemType.DarkDeathMountainFloatingIslandAccess) ||
                                _game.Items.Has(ItemType.DarkDeathMountainTopAccess)) &&
                                _game.Items.Has(ItemType.Mirror))
                                return AccessibilityLevel.Normal;

                            if (_game.Items.Has(ItemType.Hammer))
                            {
                                //  Access via West Death Mountain top exits
                                if (_game.Items.Has(ItemType.DeathMountainWestTopAccess))
                                    return AccessibilityLevel.Normal;

                                if (_game.Items.Has(ItemType.Mirror))
                                {
                                    //  Access via West Death Mountain bottom exits
                                    if (_game.Items.Has(ItemType.DeathMountainWestBottomAccess))
                                        return AccessibilityLevel.Normal;

                                    //  Access via Flute drop spot
                                    if (_game.Items.Has(ItemType.Flute))
                                        return AccessibilityLevel.Normal;

                                    //  Access via Death Mountain access cave
                                    if (!_game.Mode.EntranceShuffle.Value && _game.Items.Has(ItemType.Gloves))
                                    {
                                        //  Lamp required by logic
                                        if (_game.Items.Has(ItemType.Lamp))
                                            return AccessibilityLevel.Normal;

                                        //  Sequence break dark room
                                        return AccessibilityLevel.SequenceBreak;
                                    }
                                }
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.MoonPearl))
                            {
                                //  Access via West Death Mountain top exits
                                if (_game.Items.Has(ItemType.DeathMountainWestTopAccess))
                                    return AccessibilityLevel.Normal;

                                //  Access via Dark Death Mountain top exits
                                if (_game.Items.Has(ItemType.DarkDeathMountainFloatingIslandAccess) ||
                                    _game.Items.Has(ItemType.DarkDeathMountainTopAccess))
                                    return AccessibilityLevel.Normal;

                                //  Access via West Dark Death Mountain bottom exits
                                if (_game.Items.Has(ItemType.DarkDeathMountainWestBottomAccess))
                                    return AccessibilityLevel.Normal;

                                //  Access via Flute drop spot
                                if (_game.Items.Has(ItemType.Flute))
                                {
                                    //  Light World access via Agahnim portal
                                    if (_game.Items.Has(ItemType.Aga))
                                        return AccessibilityLevel.Normal;

                                    //  Light World access via exits in the region
                                    if (_game.Items.Has(ItemType.LightWorldAccess) ||
                                        _game.Items.Has(ItemType.DeathMountainExitAccess) ||
                                        _game.Items.Has(ItemType.RaceGameAccess) ||
                                        _game.Items.Has(ItemType.HyruleCastleSecondFloorAccess) ||
                                        _game.Items.Has(ItemType.DesertLeftAccess))
                                        return AccessibilityLevel.Normal;

                                    //  Light World access via either Village of Outcasts portal,
                                    //    Palace of Darkness portal, or portal north of Swamp Palace
                                    if (_game.Items.Has(ItemType.Gloves))
                                        return AccessibilityLevel.Normal;

                                    //  Light World access via Lake Hylia capacity upgrade fairy island
                                    if (_game.Items.Has(ItemType.LakeHyliaFairyIslandAccess) &&
                                        _game.Items.Has(ItemType.Flippers))
                                        return AccessibilityLevel.Normal;

                                    //  Light World access via Waterfall Fairy exit
                                    if (_game.Items.Has(ItemType.WaterfallFairyAccess) &&
                                        _game.Items.Has(ItemType.Flippers))
                                        return AccessibilityLevel.Normal;

                                    //  Light World access via Lake Hylia capacity upgrade fairy island (Fake Flippers)
                                    if (_game.Items.Has(ItemType.LakeHyliaFairyIslandAccess))
                                        return AccessibilityLevel.SequenceBreak;

                                    //  Light World access via Waterfall Fairy exit (Fake Flippers)
                                    if (_game.Items.Has(ItemType.WaterfallFairyAccess))
                                        return AccessibilityLevel.SequenceBreak;
                                }

                                //  Access via Death Mountain access cave
                                if (!_game.Mode.EntranceShuffle.Value && _game.Items.Has(ItemType.Gloves))
                                {
                                    //  Lamp required by logic
                                    if (_game.Items.Has(ItemType.Lamp))
                                        return AccessibilityLevel.Normal;

                                    //  Sequence break dark room
                                    return AccessibilityLevel.SequenceBreak;
                                }
                            }
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.DeathMountainEastTopAccess]);
                    itemReqs.Add(_game.Items[ItemType.DarkDeathMountainFloatingIslandAccess]);
                    itemReqs.Add(_game.Items[ItemType.DarkDeathMountainTopAccess]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainWestTopAccess]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainWestBottomAccess]);
                    itemReqs.Add(_game.Items[ItemType.Flute]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.Lamp]);
                    itemReqs.Add(_game.Items[ItemType.DarkDeathMountainWestBottomAccess]);
                    itemReqs.Add(_game.Items[ItemType.Aga]);
                    itemReqs.Add(_game.Items[ItemType.LightWorldAccess]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainExitAccess]);
                    itemReqs.Add(_game.Items[ItemType.RaceGameAccess]);
                    itemReqs.Add(_game.Items[ItemType.HyruleCastleSecondFloorAccess]);
                    itemReqs.Add(_game.Items[ItemType.DesertLeftAccess]);
                    itemReqs.Add(_game.Items[ItemType.DesertBackAccess]);
                    itemReqs.Add(_game.Items[ItemType.LakeHyliaFairyIslandAccess]);
                    itemReqs.Add(_game.Items[ItemType.Flippers]);
                    itemReqs.Add(_game.Items[ItemType.WaterfallFairyAccess]);

                    break;
                case RegionID.DarkDeathMountainTop:

                    GetAccessibility = () =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.DarkDeathMountainFloatingIslandAccess) ||
                            _game.Items.Has(ItemType.DarkDeathMountainTopAccess))
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Gloves, 2) &&
                                _game.Items.Has(ItemType.MoonPearl))
                            {
                                //  Access via East Death Mountain top exits
                                if (_game.Items.Has(ItemType.DeathMountainEastTopAccess))
                                    return AccessibilityLevel.Normal;

                                //  Access via West Death Mountain top exits
                                if (_game.Items.Has(ItemType.DeathMountainWestTopAccess))
                                    return AccessibilityLevel.Normal;

                                if (_game.Items.Has(ItemType.Mirror))
                                {
                                    //  Access via West Death Mountain bottom exits
                                    if (_game.Items.Has(ItemType.DeathMountainWestBottomAccess))
                                        return AccessibilityLevel.Normal;

                                    //  Access via East Death Mountain bottom exits
                                    if ((_game.Items.Has(ItemType.DeathMountainEastTopConnectorAccess) ||
                                        _game.Items.Has(ItemType.DeathMountainEastBottomAccess)) &&
                                        _game.Items.Has(ItemType.Hookshot))
                                        return AccessibilityLevel.Normal;

                                    //  Access via Flute drop spot
                                    if (_game.Items.Has(ItemType.Flute))
                                        return AccessibilityLevel.Normal;

                                    //  Access via Death Mountain access cave
                                    if (!_game.Mode.EntranceShuffle.Value)
                                    {
                                        //  Lamp required by logic
                                        if (_game.Items.Has(ItemType.Lamp))
                                            return AccessibilityLevel.Normal;

                                        //  Sequence break dark room
                                        return AccessibilityLevel.SequenceBreak;
                                    }
                                }
                            }

                            //  Access via East Dark Death Mountain bottom portal (non-Entrance)
                            if (!_game.Mode.EntranceShuffle.Value && _game.Items.Has(ItemType.MoonPearl) &&
                                _game.Items.Has(ItemType.Gloves, 2))
                            {
                                //  Access via Flute drop spot
                                if (_game.Items.Has(ItemType.Flute))
                                    return AccessibilityLevel.Normal;

                                //  Access via Death Mountain access cave
                                if (!_game.Mode.EntranceShuffle.Value)
                                {
                                    //  Lamp required by logic
                                    if (_game.Items.Has(ItemType.Lamp))
                                        return AccessibilityLevel.Normal;

                                    //  Sequence break dark room
                                    return AccessibilityLevel.SequenceBreak;
                                }
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            //  Access via West Dark Death Mountain bottom exits
                            if (_game.Items.Has(ItemType.DarkDeathMountainWestBottomAccess))
                                return AccessibilityLevel.Normal;

                            if (_game.Items.Has(ItemType.Mirror))
                            {
                                //  Access via East Death Mountain top exits
                                if (_game.Items.Has(ItemType.DeathMountainEastTopAccess))
                                    return AccessibilityLevel.Normal;

                                //  Access via West Death Mountain top exits
                                if (_game.Items.Has(ItemType.DeathMountainWestTopAccess))
                                    return AccessibilityLevel.Normal;

                                //  Access via West Death Mountain bottom exits
                                if (_game.Items.Has(ItemType.DeathMountainWestBottomAccess))
                                    return AccessibilityLevel.Normal;

                                if (_game.Items.Has(ItemType.Hookshot) && _game.Items.Has(ItemType.MoonPearl))
                                {
                                    //  Access via East Death Mountain bottom exits
                                    if (_game.Items.Has(ItemType.DeathMountainEastTopConnectorAccess) ||
                                        _game.Items.Has(ItemType.DeathMountainEastBottomAccess))
                                        return AccessibilityLevel.Normal;

                                    //  Access via East Dark Death Mountain bottom exits
                                    if (_game.Items.Has(ItemType.DarkDeathMountainEastBottomAccess) &&
                                        _game.Items.Has(ItemType.Gloves, 2))
                                        return AccessibilityLevel.Normal;
                                }
                            }

                            //  Access via Flute drop spot
                            if (_game.Items.Has(ItemType.Flute) && _game.Items.Has(ItemType.MoonPearl))
                            {
                                //  Light World access via Agahnim portal
                                if (_game.Items.Has(ItemType.Aga))
                                    return AccessibilityLevel.Normal;

                                //  Light World access via exits in the region
                                if (_game.Items.Has(ItemType.LightWorldAccess) ||
                                    _game.Items.Has(ItemType.DeathMountainExitAccess) ||
                                    _game.Items.Has(ItemType.RaceGameAccess) ||
                                    _game.Items.Has(ItemType.HyruleCastleSecondFloorAccess) ||
                                    _game.Items.Has(ItemType.DesertLeftAccess))
                                    return AccessibilityLevel.Normal;

                                //  Light World access via either Village of Outcasts portal,
                                //    Palace of Darkness portal, or portal north of Swamp Palace
                                if (_game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.Hammer))
                                    return AccessibilityLevel.Normal;

                                //  Light World access via Lake Hylia capacity upgrade fairy island
                                if (_game.Items.Has(ItemType.LakeHyliaFairyIslandAccess) &&
                                    _game.Items.Has(ItemType.Flippers))
                                    return AccessibilityLevel.Normal;

                                //  Light World access via Waterfall Fairy exit
                                if (_game.Items.Has(ItemType.WaterfallFairyAccess) &&
                                    _game.Items.Has(ItemType.Flippers))
                                    return AccessibilityLevel.Normal;

                                //  Light World access via Lake Hylia capacity upgrade fairy island (Fake Flippers)
                                if (_game.Items.Has(ItemType.LakeHyliaFairyIslandAccess))
                                    return AccessibilityLevel.SequenceBreak;

                                //  Light World access via Waterfall Fairy exit (Fake Flippers)
                                if (_game.Items.Has(ItemType.WaterfallFairyAccess))
                                    return AccessibilityLevel.SequenceBreak;
                            }

                            //  Access via Death Mountain access cave
                            if (!_game.Mode.EntranceShuffle.Value && _game.Items.Has(ItemType.Gloves))
                            {
                                //  Lamp required by logic
                                if (_game.Items.Has(ItemType.Lamp))
                                    return AccessibilityLevel.Normal;

                                //  Sequence break dark room
                                return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.DarkDeathMountainFloatingIslandAccess]);
                    itemReqs.Add(_game.Items[ItemType.DarkDeathMountainTopAccess]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainEastTopAccess]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainWestTopAccess]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainEastTopConnectorAccess]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainEastBottomAccess]);
                    itemReqs.Add(_game.Items[ItemType.Hookshot]);
                    itemReqs.Add(_game.Items[ItemType.Flute]);
                    itemReqs.Add(_game.Items[ItemType.Lamp]);
                    itemReqs.Add(_game.Items[ItemType.DarkDeathMountainWestBottomAccess]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainWestBottomAccess]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainEastTopConnectorAccess]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainEastBottomAccess]);
                    itemReqs.Add(_game.Items[ItemType.DarkDeathMountainEastBottomAccess]);
                    itemReqs.Add(_game.Items[ItemType.Aga]);
                    itemReqs.Add(_game.Items[ItemType.LightWorldAccess]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainExitAccess]);
                    itemReqs.Add(_game.Items[ItemType.RaceGameAccess]);
                    itemReqs.Add(_game.Items[ItemType.HyruleCastleSecondFloorAccess]);
                    itemReqs.Add(_game.Items[ItemType.DesertLeftAccess]);
                    itemReqs.Add(_game.Items[ItemType.DesertBackAccess]);
                    itemReqs.Add(_game.Items[ItemType.LakeHyliaFairyIslandAccess]);
                    itemReqs.Add(_game.Items[ItemType.Flippers]);
                    itemReqs.Add(_game.Items[ItemType.WaterfallFairyAccess]);

                    break;
                case RegionID.DarkDeathMountainEastBottom:

                    GetAccessibility = () =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.DarkDeathMountainEastBottomAccess))
                            return AccessibilityLevel.Normal;

                        //  Access via Dark Death Mountain top exits
                        if (_game.Items.Has(ItemType.DarkDeathMountainFloatingIslandAccess) ||
                            _game.Items.Has(ItemType.DarkDeathMountainTopAccess))
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Gloves, 2))
                            {
                                //  Access via East Death Mountain bottom exits
                                if (_game.Items.Has(ItemType.DeathMountainEastTopConnectorAccess) ||
                                    _game.Items.Has(ItemType.DeathMountainEastBottomAccess))
                                    return AccessibilityLevel.Normal;

                                //  Access via East Death Mountain top exits
                                if (_game.Items.Has(ItemType.DeathMountainEastTopAccess))
                                    return AccessibilityLevel.Normal;

                                //  Access via West Death Mountain top exits
                                if (_game.Items.Has(ItemType.DeathMountainWestTopAccess) &&
                                    (_game.Items.Has(ItemType.Hammer) || _game.Items.Has(ItemType.Hookshot)))
                                    return AccessibilityLevel.Normal;

                                //  Access via West Death Mountain bottom exits
                                if (_game.Items.Has(ItemType.DeathMountainWestBottomAccess) &&
                                    ((_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Mirror)) ||
                                    _game.Items.Has(ItemType.Hookshot)))
                                    return AccessibilityLevel.Normal;

                                //  Access via Flute drop spot
                                if (_game.Items.Has(ItemType.Flute) && ((_game.Items.Has(ItemType.Hammer) &&
                                    _game.Items.Has(ItemType.Mirror)) || _game.Items.Has(ItemType.Hookshot)))
                                    return AccessibilityLevel.Normal;

                                //  Access via Death Mountain access cave
                                if (!_game.Mode.EntranceShuffle.Value && ((_game.Items.Has(ItemType.Hammer) &&
                                    _game.Items.Has(ItemType.Mirror)) || _game.Items.Has(ItemType.Hookshot)))
                                {
                                    //  Lamp required by logic
                                    if (_game.Items.Has(ItemType.Lamp))
                                        return AccessibilityLevel.Normal;

                                    //  Sequence break dark room
                                    return AccessibilityLevel.SequenceBreak;
                                }
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            //  Access via West Dark Death Mountain bottom exits
                            if (_game.Items.Has(ItemType.DarkDeathMountainWestBottomAccess))
                                return AccessibilityLevel.Normal;

                            if (_game.Items.Has(ItemType.Mirror))
                            {
                                //  Access via East Death Mountain bottom exits
                                if (_game.Items.Has(ItemType.DeathMountainEastBottomAccess) ||
                                    _game.Items.Has(ItemType.DeathMountainEastTopConnectorAccess))
                                    return AccessibilityLevel.Normal;

                                //  Access via East Death Mountain top exits
                                if (_game.Items.Has(ItemType.DeathMountainEastTopAccess))
                                    return AccessibilityLevel.Normal;

                                //  Access via West Death Mountain top exits
                                if (_game.Items.Has(ItemType.DeathMountainWestTopAccess))
                                    return AccessibilityLevel.Normal;

                                //  Access via West Death Mountain bottom exits
                                if (_game.Items.Has(ItemType.DeathMountainWestBottomAccess))
                                    return AccessibilityLevel.Normal;
                            }

                            //  Access via Flute drop spot
                            if (_game.Items.Has(ItemType.Flute) && _game.Items.Has(ItemType.MoonPearl))
                            {
                                //  Light World access via Agahnim portal
                                if (_game.Items.Has(ItemType.Aga))
                                    return AccessibilityLevel.Normal;

                                //  Light World access via exits in the region
                                if (_game.Items.Has(ItemType.LightWorldAccess) ||
                                    _game.Items.Has(ItemType.DeathMountainExitAccess) ||
                                    _game.Items.Has(ItemType.RaceGameAccess) ||
                                    _game.Items.Has(ItemType.HyruleCastleSecondFloorAccess) ||
                                    _game.Items.Has(ItemType.DesertLeftAccess))
                                    return AccessibilityLevel.Normal;

                                //  Light World access via either Village of Outcasts portal,
                                //    Palace of Darkness portal, or portal north of Swamp Palace
                                if (_game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.Hammer))
                                    return AccessibilityLevel.Normal;

                                //  Light World access via Lake Hylia capacity upgrade fairy island
                                if (_game.Items.Has(ItemType.LakeHyliaFairyIslandAccess) &&
                                    _game.Items.Has(ItemType.Flippers))
                                    return AccessibilityLevel.Normal;

                                //  Light World access via Waterfall Fairy exit
                                if (_game.Items.Has(ItemType.WaterfallFairyAccess) &&
                                    _game.Items.Has(ItemType.Flippers))
                                    return AccessibilityLevel.Normal;

                                //  Light World access via Lake Hylia capacity upgrade fairy island (Fake Flippers)
                                if (_game.Items.Has(ItemType.LakeHyliaFairyIslandAccess))
                                    return AccessibilityLevel.SequenceBreak;

                                //  Light World access via Waterfall Fairy exit (Fake Flippers)
                                if (_game.Items.Has(ItemType.WaterfallFairyAccess))
                                    return AccessibilityLevel.SequenceBreak;
                            }

                            //  Access via Death Mountain access cave
                            if (!_game.Mode.EntranceShuffle.Value && _game.Items.Has(ItemType.Gloves))
                            {
                                //  Lamp required by logic
                                if (_game.Items.Has(ItemType.Lamp))
                                    return AccessibilityLevel.Normal;

                                //  Sequence break dark room
                                return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.DarkDeathMountainEastBottomAccess]);
                    itemReqs.Add(_game.Items[ItemType.DarkDeathMountainFloatingIslandAccess]);
                    itemReqs.Add(_game.Items[ItemType.DarkDeathMountainTopAccess]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainEastTopConnectorAccess]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainEastBottomAccess]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainEastTopAccess]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainWestTopAccess]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.Hookshot]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainWestBottomAccess]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.Flute]);
                    itemReqs.Add(_game.Items[ItemType.Lamp]);
                    itemReqs.Add(_game.Items[ItemType.DarkDeathMountainWestBottomAccess]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Aga]);
                    itemReqs.Add(_game.Items[ItemType.LightWorldAccess]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainExitAccess]);
                    itemReqs.Add(_game.Items[ItemType.RaceGameAccess]);
                    itemReqs.Add(_game.Items[ItemType.HyruleCastleSecondFloorAccess]);
                    itemReqs.Add(_game.Items[ItemType.DesertLeftAccess]);
                    itemReqs.Add(_game.Items[ItemType.DesertBackAccess]);
                    itemReqs.Add(_game.Items[ItemType.LakeHyliaFairyIslandAccess]);
                    itemReqs.Add(_game.Items[ItemType.Flippers]);
                    itemReqs.Add(_game.Items[ItemType.WaterfallFairyAccess]);

                    break;
                case RegionID.DarkDeathMountainWestBottom:

                    GetAccessibility = () =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.DarkDeathMountainWestBottomAccess))
                            return AccessibilityLevel.Normal;

                        //  Access via Dark Death Mountain top exits
                        if (_game.Items.Has(ItemType.DarkDeathMountainFloatingIslandAccess) ||
                            _game.Items.Has(ItemType.DarkDeathMountainTopAccess))
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            //  Access via West Death Mountain bottom exits
                            if (_game.Items.Has(ItemType.DeathMountainWestBottomAccess))
                                return AccessibilityLevel.Normal;

                            //  Access via West Death Mountain top exits
                            if (_game.Items.Has(ItemType.DeathMountainWestTopAccess))
                                return AccessibilityLevel.Normal;

                            //  Access via East Death Mountain top exits
                            if (_game.Items.Has(ItemType.DeathMountainEastTopAccess) &&
                                (_game.Items.Has(ItemType.Hammer) || _game.Items.Has(ItemType.Hookshot)))
                                return AccessibilityLevel.Normal;

                            if (_game.Items.Has(ItemType.Hookshot))
                            {
                                //  Access via East Death Mountain bottom exits
                                if (_game.Items.Has(ItemType.DeathMountainEastTopConnectorAccess) ||
                                    _game.Items.Has(ItemType.DeathMountainEastBottomAccess))
                                    return AccessibilityLevel.Normal;

                                //  Access via East Dark Death Mountain bottom exits
                                if (_game.Items.Has(ItemType.DarkDeathMountainEastBottomAccess) &&
                                    _game.Items.Has(ItemType.Mirror))
                                    return AccessibilityLevel.Normal;

                                //  Access via Turtle Rock exits
                                if ((_game.Items.Has(ItemType.TurtleRockTunnelAccess) ||
                                    _game.Items.Has(ItemType.TurtleRockSafetyDoorAccess)) &&
                                    _game.Items.Has(ItemType.Mirror))
                                    return AccessibilityLevel.Normal;
                            }

                            //  Access via Flute drop spot
                            if (_game.Items.Has(ItemType.Flute))
                                return AccessibilityLevel.Normal;

                            //  Access via Death Mountain access cave
                            if (!_game.Mode.EntranceShuffle.Value && _game.Items.Has(ItemType.Gloves))
                            {
                                //  Lamp required by logic
                                if (_game.Items.Has(ItemType.Lamp))
                                    return AccessibilityLevel.Normal;

                                //  Sequence break dark room
                                return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Mirror))
                            {
                                //  Access via West Death Mountain top exits
                                if (_game.Items.Has(ItemType.DeathMountainWestTopAccess))
                                    return AccessibilityLevel.Normal;

                                //  Access via West Death Mountain bottom exits
                                if (_game.Items.Has(ItemType.DeathMountainWestBottomAccess))
                                    return AccessibilityLevel.Normal;

                                if (_game.Items.Has(ItemType.Hookshot) && _game.Items.Has(ItemType.MoonPearl))
                                {
                                    //  Access via East Death Mountain bottom exits
                                    if (_game.Items.Has(ItemType.DeathMountainEastTopConnectorAccess) ||
                                        _game.Items.Has(ItemType.DeathMountainEastBottomAccess))
                                        return AccessibilityLevel.Normal;

                                    //  Access via East Dark Death Mountain bottom exits
                                    if (_game.Items.Has(ItemType.DarkDeathMountainEastBottomAccess) &&
                                        _game.Items.Has(ItemType.Gloves, 2))
                                        return AccessibilityLevel.Normal;
                                }

                                if (_game.Items.Has(ItemType.DeathMountainEastTopAccess) &&
                                    _game.Items.Has(ItemType.MoonPearl) &&
                                    (_game.Items.Has(ItemType.Hookshot) || _game.Items.Has(ItemType.Hammer)))
                                    return AccessibilityLevel.Normal;
                            }

                            //  Access via Flute drop spot
                            if (_game.Items.Has(ItemType.Flute) && _game.Items.Has(ItemType.MoonPearl))
                            {
                                //  Light World access via Agahnim portal
                                if (_game.Items.Has(ItemType.Aga))
                                    return AccessibilityLevel.Normal;

                                //  Light World access via exits in the region
                                if (_game.Items.Has(ItemType.LightWorldAccess) ||
                                    _game.Items.Has(ItemType.DeathMountainExitAccess) ||
                                    _game.Items.Has(ItemType.RaceGameAccess) ||
                                    _game.Items.Has(ItemType.HyruleCastleSecondFloorAccess) ||
                                    _game.Items.Has(ItemType.DesertLeftAccess))
                                    return AccessibilityLevel.Normal;

                                //  Light World access via either Village of Outcasts portal,
                                //    Palace of Darkness portal, or portal north of Swamp Palace
                                if (_game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.Hammer))
                                    return AccessibilityLevel.Normal;

                                //  Light World access via Lake Hylia capacity upgrade fairy island
                                if (_game.Items.Has(ItemType.LakeHyliaFairyIslandAccess) &&
                                    _game.Items.Has(ItemType.Flippers))
                                    return AccessibilityLevel.Normal;

                                //  Light World access via Waterfall Fairy exit
                                if (_game.Items.Has(ItemType.WaterfallFairyAccess) &&
                                    _game.Items.Has(ItemType.Flippers))
                                    return AccessibilityLevel.Normal;

                                //  Light World access via Lake Hylia capacity upgrade fairy island (Fake Flippers)
                                if (_game.Items.Has(ItemType.LakeHyliaFairyIslandAccess))
                                    return AccessibilityLevel.SequenceBreak;

                                //  Light World access via Waterfall Fairy exit (Fake Flippers)
                                if (_game.Items.Has(ItemType.WaterfallFairyAccess))
                                    return AccessibilityLevel.SequenceBreak;
                            }

                            //  Access via Death Mountain access cave
                            if (!_game.Mode.EntranceShuffle.Value && _game.Items.Has(ItemType.Gloves))
                            {
                                //  Lamp required by logic
                                if (_game.Items.Has(ItemType.Lamp))
                                    return AccessibilityLevel.Normal;

                                //  Sequence break dark room
                                return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.DarkDeathMountainWestBottomAccess]);
                    itemReqs.Add(_game.Items[ItemType.DarkDeathMountainFloatingIslandAccess]);
                    itemReqs.Add(_game.Items[ItemType.DarkDeathMountainTopAccess]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainWestBottomAccess]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainWestTopAccess]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainEastTopAccess]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.Hookshot]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainEastTopConnectorAccess]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainEastBottomAccess]);
                    itemReqs.Add(_game.Items[ItemType.DarkDeathMountainEastBottomAccess]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.TurtleRockTunnelAccess]);
                    itemReqs.Add(_game.Items[ItemType.TurtleRockSafetyDoorAccess]);
                    itemReqs.Add(_game.Items[ItemType.Flute]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.Lamp]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Aga]);
                    itemReqs.Add(_game.Items[ItemType.LightWorldAccess]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainExitAccess]);
                    itemReqs.Add(_game.Items[ItemType.RaceGameAccess]);
                    itemReqs.Add(_game.Items[ItemType.HyruleCastleSecondFloorAccess]);
                    itemReqs.Add(_game.Items[ItemType.DesertLeftAccess]);
                    itemReqs.Add(_game.Items[ItemType.DesertBackAccess]);
                    itemReqs.Add(_game.Items[ItemType.LakeHyliaFairyIslandAccess]);
                    itemReqs.Add(_game.Items[ItemType.Flippers]);
                    itemReqs.Add(_game.Items[ItemType.WaterfallFairyAccess]);

                    break;
                case RegionID.HyruleCastle:

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.StandardOpen ||
                            _game.Items.Has(ItemType.MoonPearl))
                            return _game.Regions[RegionID.LightWorld].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    _game.Regions[RegionID.LightWorld].PropertyChanged += OnItemRequirementChanged;

                    break;
                case RegionID.Agahnim:

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.CanClearAgaTowerBarrier())
                                return AccessibilityLevel.Normal;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                            return _game.Regions[RegionID.DarkDeathMountainTop].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Cape]);
                    itemReqs.Add(_game.Items[ItemType.Sword]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);

                    _game.Regions[RegionID.DarkDeathMountainTop].PropertyChanged += OnItemRequirementChanged;

                    break;
                case RegionID.EasternPalace:

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.StandardOpen ||
                            _game.Items.Has(ItemType.MoonPearl))
                            return _game.Regions[RegionID.LightWorld].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    _game.Regions[RegionID.LightWorld].PropertyChanged += OnItemRequirementChanged;

                    break;
                case RegionID.DesertPalace:

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Book))
                                return AccessibilityLevel.Normal;

                            if (_game.Items.Has(ItemType.Mirror))
                                return _game.Regions[RegionID.MireArea].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Book) && _game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.LightWorld].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Book]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    _game.Regions[RegionID.MireArea].PropertyChanged += OnItemRequirementChanged;
                    _game.Regions[RegionID.LightWorld].PropertyChanged += OnItemRequirementChanged;

                    break;
                case RegionID.TowerOfHera:

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.StandardOpen ||
                            _game.Items.Has(ItemType.MoonPearl))
                            return _game.Regions[RegionID.DeathMountainWestTop].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    _game.Regions[RegionID.DeathMountainWestTop].PropertyChanged += OnItemRequirementChanged;

                    break;
                case RegionID.PalaceOfDarkness:

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.Inverted ||
                            _game.Items.Has(ItemType.MoonPearl))
                            return _game.Regions[RegionID.DarkWorldEast].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    _game.Regions[RegionID.DarkWorldEast].PropertyChanged += OnItemRequirementChanged;

                    break;
                case RegionID.SwampPalace:

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

                        if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Mirror))
                        {
                            if (_game.Mode.WorldState == WorldState.StandardOpen)
                                return _game.Regions[RegionID.DarkWorldSouth].Accessibility;

                            if (_game.Mode.WorldState == WorldState.Inverted)
                                return _game.Regions[RegionID.LightWorld].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);

                    _game.Regions[RegionID.DarkWorldSouth].PropertyChanged += OnItemRequirementChanged;
                    _game.Regions[RegionID.LightWorld].PropertyChanged += OnItemRequirementChanged;

                    break;
                case RegionID.SkullWoods:

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.Inverted ||
                            _game.Items.Has(ItemType.MoonPearl))
                            return _game.Regions[RegionID.DarkWorldWest].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    _game.Regions[RegionID.DarkWorldWest].PropertyChanged += OnItemRequirementChanged;

                    break;
                case RegionID.ThievesTown:

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.Inverted ||
                            _game.Items.Has(ItemType.MoonPearl))
                            return _game.Regions[RegionID.DarkWorldWest].Accessibility;

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    _game.Regions[RegionID.DarkWorldWest].PropertyChanged += OnItemRequirementChanged;

                    break;
                case RegionID.IcePalace:

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.Gloves, 2) && _game.Items.Has(ItemType.MoonPearl))
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

                            return AccessibilityLevel.SequenceBreak;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Flippers]);

                    break;
                case RegionID.MiseryMire:

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

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

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Sword]);
                    itemReqs.Add(_game.Items[ItemType.Bombos]);
                    itemReqs.Add(_game.Items[ItemType.BombosDungeons]);
                    itemReqs.Add(_game.Items[ItemType.Ether]);
                    itemReqs.Add(_game.Items[ItemType.EtherDungeons]);
                    itemReqs.Add(_game.Items[ItemType.Quake]);
                    itemReqs.Add(_game.Items[ItemType.QuakeDungeons]);

                    _game.Regions[RegionID.MireArea].PropertyChanged += OnItemRequirementChanged;

                    break;
                case RegionID.TurtleRock:

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

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
                                return _game.Regions[RegionID.DarkDeathMountainTop].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Regions[RegionID.DeathMountainEastTop].Accessibility == AccessibilityLevel.Normal &&
                                _game.Items.Has(ItemType.Mirror))
                                return AccessibilityLevel.Normal;

                            if (_game.Items.CanUseMedallions() && ((_game.Items.Has(ItemType.Bombos) &&
                                _game.Items.Has(ItemType.Ether) && _game.Items.Has(ItemType.Quake)) ||
                                (_game.Items.Has(ItemType.Bombos) &&
                                _game.Items[ItemType.BombosDungeons].Current >= 2) ||
                                (_game.Items.Has(ItemType.Ether) &&
                                _game.Items[ItemType.EtherDungeons].Current >= 2) ||
                                (_game.Items.Has(ItemType.Quake) &&
                                _game.Items[ItemType.QuakeDungeons].Current >= 2)) &&
                                _game.Regions[RegionID.DarkDeathMountainTop].Accessibility >= AccessibilityLevel.SequenceBreak)
                                return _game.Regions[RegionID.DarkDeathMountainTop].Accessibility;

                            if (_game.Regions[RegionID.DeathMountainEastTop].Accessibility == AccessibilityLevel.SequenceBreak &&
                                _game.Items.Has(ItemType.Mirror))
                                return AccessibilityLevel.SequenceBreak;
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
                    itemReqs.Add(_game.Items[ItemType.Mirror]);

                    _game.Regions[RegionID.DeathMountainEastTop].PropertyChanged += OnItemRequirementChanged;
                    _game.Regions[RegionID.DarkDeathMountainTop].PropertyChanged += OnItemRequirementChanged;

                    break;
                case RegionID.GanonsTower:

                    GetAccessibility = () =>
                    {
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.TowerCrystals) && _game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.DarkDeathMountainTop].Accessibility;
                        }

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.TowerCrystals) && _game.Items.Has(ItemType.MoonPearl))
                                return _game.Regions[RegionID.LightWorld].Accessibility;
                        }

                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.TowerCrystals]);
                    itemReqs.Add(_game.Items[ItemType.Crystal]);
                    itemReqs.Add(_game.Items[ItemType.RedCrystal]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    _game.Regions[RegionID.DarkDeathMountainTop].PropertyChanged += OnItemRequirementChanged;
                    _game.Regions[RegionID.LightWorld].PropertyChanged += OnItemRequirementChanged;

                    break;
            }

            foreach (Item itemReq in itemReqs)
                itemReq.PropertyChanged += OnItemRequirementChanged;
        }

        private void UpdateAccessibility()
        {
            Accessibility = GetAccessibility();
        }

        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_game.Mode.WorldState) ||
                e.PropertyName == nameof(_game.Mode.EntranceShuffle))
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
        }
    }
}
