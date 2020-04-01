using OpenTracker.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models
{
    public class Region : INotifyPropertyChanged
    {
        private readonly Game _game;
        private readonly List<RegionID> _observedRegions;

        public RegionID ID { get; }
        public Func<AccessibilityLevel> GetDirectAccessibility { get; }
        public Func<List<RegionID>, AccessibilityLevel> GetIndirectAccessibility { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private AccessibilityLevel _directAccessibility;
        public AccessibilityLevel DirectAccessibility
        {
            get => _directAccessibility;
            private set
            {
                if (_directAccessibility != value)
                {
                    _directAccessibility = value;
                    OnPropertyChanged(nameof(DirectAccessibility));
                }
            }
        }

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
            _observedRegions = new List<RegionID>();

            switch (ID)
            {
                case RegionID.LightWorld:

                    GetDirectAccessibility = () =>
                    {
                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                            return AccessibilityLevel.Normal;

                        //  Inverted mode
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
                                //  Access via Grass House exit
                                if (_game.Items.Has(ItemType.GrassHouseAccess))
                                    return AccessibilityLevel.Normal;

                                //  Access via Witch's Hut exit
                                if (_game.Items.Has(ItemType.WitchsHutAccess))
                                    return AccessibilityLevel.Normal;

                                //  Access via either Village of Outcasts portal, Palace of Darkness portal,
                                //    or portal north of the Dam
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
                    GetIndirectAccessibility = (excludedRegions) => { return AccessibilityLevel.None; };

                    itemReqs.Add(_game.Items[ItemType.Aga]);
                    itemReqs.Add(_game.Items[ItemType.LightWorldAccess]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainExitAccess]);
                    itemReqs.Add(_game.Items[ItemType.RaceGameAccess]);
                    itemReqs.Add(_game.Items[ItemType.HyruleCastleSecondFloorAccess]);
                    itemReqs.Add(_game.Items[ItemType.DesertLeftAccess]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.GrassHouseAccess]);
                    itemReqs.Add(_game.Items[ItemType.WitchsHutAccess]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.DesertBackAccess]);
                    itemReqs.Add(_game.Items[ItemType.LakeHyliaFairyIslandAccess]);
                    itemReqs.Add(_game.Items[ItemType.Flippers]);
                    itemReqs.Add(_game.Items[ItemType.WaterfallFairyAccess]);

                    break;
                case RegionID.HyruleCastleSecondFloor:

                    GetDirectAccessibility = () =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.HyruleCastleSecondFloorAccess))
                            return AccessibilityLevel.Normal;

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };
                    GetIndirectAccessibility = (excludedRegions) =>
                    {
                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            //  Access via mirror from east Dark World
                            if (_game.Items.Has(ItemType.Mirror) && !excludedRegions.Contains(RegionID.DarkWorldEast))
                                return _game.Regions[RegionID.DarkWorldEast].GetAccessibility(newExcludedRegions);
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.HyruleCastleSecondFloorAccess]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);

                    _observedRegions.Add(RegionID.DarkWorldEast);

                    break;
                case RegionID.DarkWorldWest:

                    GetDirectAccessibility = () =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.DarkWorldWestAccess) ||
                            _game.Items.Has(ItemType.BumperCaveAccess))
                            return AccessibilityLevel.Normal;

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl))
                            {
                                //  Access via Hammer House exit
                                if (_game.Items.Has(ItemType.HammerHouseAccess) && _game.Items.Has(ItemType.Hammer))
                                    return AccessibilityLevel.Normal;

                                //  Access via Kakariko Village portal
                                if ((_game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.Hammer)) ||
                                    _game.Items.Has(ItemType.Gloves, 2))
                                    return AccessibilityLevel.Normal;
                            }
                        }

                        //  Inverted mode
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            //  Access from the start in non-entrance Inverted mode
                            if (!_game.Mode.EntranceShuffle.Value)
                                return AccessibilityLevel.Normal;

                            //  Access via Hammer House exit
                            if (_game.Items.Has(ItemType.HammerHouseAccess) && _game.Items.Has(ItemType.Hammer))
                                return AccessibilityLevel.Normal;
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };
                    GetIndirectAccessibility = (excludedRegions) =>
                    {
                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            //  Access via Dark World Witch area by hookshot
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Hookshot) &&
                                !excludedRegions.Contains(RegionID.DarkWorldWitchArea))
                                return _game.Regions[RegionID.DarkWorldWitchArea].GetAccessibility(newExcludedRegions);
                        }

                        //  Inverted mode
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            AccessibilityLevel dWSouth = AccessibilityLevel.None;
                            AccessibilityLevel dWWitch = AccessibilityLevel.None;
                            AccessibilityLevel lightWorld = AccessibilityLevel.None;

                            //  Access via South Dark World by mitts
                            if (_game.Items.Has(ItemType.Gloves, 2) && !excludedRegions.Contains(RegionID.DarkWorldSouth))
                                dWSouth = _game.Regions[RegionID.DarkWorldSouth].GetAccessibility(newExcludedRegions);

                            //  Access via Dark World Witch area by hookshot
                            if (_game.Items.Has(ItemType.Hookshot) && !excludedRegions.Contains(RegionID.DarkWorldWitchArea))
                                dWWitch = _game.Regions[RegionID.DarkWorldWitchArea].GetAccessibility(newExcludedRegions);

                            //  Access via Light World by mirror
                            if (_game.Items.Has(ItemType.Mirror) && !excludedRegions.Contains(RegionID.LightWorld))
                                lightWorld = _game.Regions[RegionID.LightWorld].GetAccessibility(newExcludedRegions);

                            return (AccessibilityLevel)Math.Max(Math.Max((byte)dWSouth, (byte)dWWitch), (byte)lightWorld);
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.DarkWorldWestAccess]);
                    itemReqs.Add(_game.Items[ItemType.BumperCaveAccess]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.HammerHouseAccess]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.Hookshot]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);

                    _observedRegions.Add(RegionID.DarkWorldSouth);
                    _observedRegions.Add(RegionID.DarkWorldWitchArea);
                    _observedRegions.Add(RegionID.LightWorld);

                    break;
                case RegionID.DarkWorldSouth:

                    GetDirectAccessibility = () =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.DarkWorldSouthAccess))
                            return AccessibilityLevel.Normal;

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            //  Access via north of Dam portal
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Hammer) &&
                                _game.Items.Has(ItemType.Gloves))
                                return AccessibilityLevel.Normal;
                        }

                        //  Inverted mode
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            //  Access is provided from the start in non-entrance shuffle
                            if (!_game.Mode.EntranceShuffle.Value)
                                return AccessibilityLevel.Normal;
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };
                    GetIndirectAccessibility = (excludedRegions) =>
                    {
                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            AccessibilityLevel darkWorldWest = AccessibilityLevel.None;
                            AccessibilityLevel darkWorldEast = AccessibilityLevel.None;

                            //  Access via Dark World West
                            if (!excludedRegions.Contains(RegionID.DarkWorldWest))
                                darkWorldWest = _game.Regions[RegionID.DarkWorldWest].GetAccessibility(newExcludedRegions);

                            //  Access via Dark World East by hammer
                            if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.MoonPearl) &&
                                !excludedRegions.Contains(RegionID.DarkWorldEast))
                                darkWorldEast = _game.Regions[RegionID.DarkWorldEast].GetAccessibility(newExcludedRegions);

                            return (AccessibilityLevel)Math.Max((byte)darkWorldWest, (byte)darkWorldEast);
                        }

                        //  Inverted mode
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            AccessibilityLevel dWWest = AccessibilityLevel.None;
                            AccessibilityLevel dWEast = AccessibilityLevel.None;
                            AccessibilityLevel lightWorld = AccessibilityLevel.None;

                            //  Access via West Dark World
                            if (!excludedRegions.Contains(RegionID.DarkWorldWest))
                                dWWest = _game.Regions[RegionID.DarkWorldWest].GetAccessibility(newExcludedRegions);

                            //  Access via East Dark World by hammer
                            if (_game.Items.Has(ItemType.Hammer) && !excludedRegions.Contains(RegionID.DarkWorldEast))
                                dWEast = _game.Regions[RegionID.DarkWorldEast].GetAccessibility(newExcludedRegions);

                            //  Access via Light World by mirror
                            if (_game.Items.Has(ItemType.Mirror) && !excludedRegions.Contains(RegionID.LightWorld))
                                lightWorld = _game.Regions[RegionID.LightWorld].GetAccessibility(newExcludedRegions);

                            return (AccessibilityLevel)Math.Max(Math.Max((byte)dWWest, (byte)dWEast), (byte)lightWorld);
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.DarkWorldSouthAccess]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);

                    _observedRegions.Add(RegionID.DarkWorldWest);
                    _observedRegions.Add(RegionID.DarkWorldEast);
                    _observedRegions.Add(RegionID.LightWorld);

                    break;
                case RegionID.DarkWorldEast:

                    GetDirectAccessibility = () =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.DarkWorldEastAccess))
                            return AccessibilityLevel.Normal;

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            //  Access via Agahnim portal
                            if (_game.Items.Has(ItemType.Aga))
                                return AccessibilityLevel.Normal;

                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Gloves) &&
                                _game.Items.Has(ItemType.Hammer))
                                return AccessibilityLevel.Normal;
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };
                    GetIndirectAccessibility = (excludedRegions) =>
                    {
                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            AccessibilityLevel darkWorldWitch = AccessibilityLevel.None;
                            AccessibilityLevel darkWorldSouth = AccessibilityLevel.None;
                            AccessibilityLevel darkWorldSouthEast = AccessibilityLevel.None;

                            if (_game.Items.Has(ItemType.MoonPearl))
                            {
                                //  Access via Dark World Witch area
                                if ((_game.Items.Has(ItemType.Gloves) || _game.Items.Has(ItemType.Hammer)) &&
                                    !excludedRegions.Contains(RegionID.DarkWorldWitchArea))
                                    darkWorldWitch = _game.Regions[RegionID.DarkWorldWitchArea].GetAccessibility(newExcludedRegions);

                                //  Access via South Dark World
                                if (!excludedRegions.Contains(RegionID.DarkWorldSouth))
                                {
                                    if (_game.Items.Has(ItemType.Hammer) || _game.Items.Has(ItemType.Flippers))
                                        darkWorldSouth = _game.Regions[RegionID.DarkWorldSouth].GetAccessibility(newExcludedRegions);
                                    else
                                    {
                                        darkWorldSouth = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                            (byte)_game.Regions[RegionID.DarkWorldSouth].GetAccessibility(newExcludedRegions));
                                    }
                                }

                                //  Access via Southeast Dark World
                                if (!excludedRegions.Contains(RegionID.DarkWorldSouthEast))
                                {
                                    if (_game.Items.Has(ItemType.Flippers))
                                        darkWorldSouthEast = _game.Regions[RegionID.DarkWorldSouthEast].GetAccessibility(newExcludedRegions);
                                    else
                                    {
                                        darkWorldSouthEast = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                            (byte)_game.Regions[RegionID.DarkWorldSouthEast].GetAccessibility(newExcludedRegions));
                                    }
                                }
                            }

                            return (AccessibilityLevel)Math.Max(Math.Max((byte)darkWorldWitch, (byte)darkWorldSouth),
                                (byte)darkWorldSouthEast);
                        }

                        //  Inverted mode
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            AccessibilityLevel dWSouth = AccessibilityLevel.None;
                            AccessibilityLevel dWWitch = AccessibilityLevel.None;
                            AccessibilityLevel lightWorld = AccessibilityLevel.None;

                            //  Access via South Dark World by hammer, flippers, or fake flippers sequence break
                            if (!excludedRegions.Contains(RegionID.DarkWorldSouth))
                            {
                                if (_game.Items.Has(ItemType.Hammer) || _game.Items.Has(ItemType.Flippers))
                                    dWSouth = _game.Regions[RegionID.DarkWorldSouth].GetAccessibility(newExcludedRegions);
                                else
                                    dWSouth = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                        (byte)_game.Regions[RegionID.DarkWorldSouth].GetAccessibility(newExcludedRegions));
                            }

                            //  Access via Dark World Witch area by hammer or gloves
                            if ((_game.Items.Has(ItemType.Gloves) || _game.Items.Has(ItemType.Hammer))
                                && !excludedRegions.Contains(RegionID.DarkWorldWitchArea))
                                dWWitch = _game.Regions[RegionID.DarkWorldWitchArea].GetAccessibility(newExcludedRegions);

                            //  Access via Light World by mirror
                            if (_game.Items.Has(ItemType.Mirror) && !excludedRegions.Contains(RegionID.LightWorld))
                                lightWorld = _game.Regions[RegionID.LightWorld].GetAccessibility(newExcludedRegions);

                            return (AccessibilityLevel)Math.Max(Math.Max((byte)dWSouth, (byte)dWWitch), (byte)lightWorld);
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.DarkWorldEastAccess]);
                    itemReqs.Add(_game.Items[ItemType.Aga]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.Flippers]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);

                    _observedRegions.Add(RegionID.DarkWorldWitchArea);
                    _observedRegions.Add(RegionID.DarkWorldSouth);
                    _observedRegions.Add(RegionID.DarkWorldSouthEast);
                    _observedRegions.Add(RegionID.LightWorld);

                    break;
                case RegionID.DarkWorldSouthEast:

                    GetDirectAccessibility = () =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.DarkWorldSouthEastAccess))
                            return AccessibilityLevel.Normal;

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };
                    GetIndirectAccessibility = (excludedRegions) =>
                    {
                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            AccessibilityLevel darkWorldSouth = AccessibilityLevel.None;
                            AccessibilityLevel darkWorldEast = AccessibilityLevel.None;

                            if (_game.Items.Has(ItemType.MoonPearl))
                            {
                                //  Access via South Dark World by flippers
                                if (!excludedRegions.Contains(RegionID.DarkWorldSouth))
                                {
                                    if (_game.Items.Has(ItemType.Flippers))
                                        darkWorldSouth = _game.Regions[RegionID.DarkWorldSouth].GetAccessibility(newExcludedRegions);
                                    else
                                        darkWorldSouth = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                            (byte)_game.Regions[RegionID.DarkWorldSouth].GetAccessibility(newExcludedRegions));
                                }

                                //  Access via East Dark World by flippers
                                if (!excludedRegions.Contains(RegionID.DarkWorldEast))
                                {
                                    if (_game.Items.Has(ItemType.Flippers))
                                        darkWorldEast = _game.Regions[RegionID.DarkWorldEast].GetAccessibility(newExcludedRegions);
                                    else
                                        darkWorldEast = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                            (byte)_game.Regions[RegionID.DarkWorldEast].GetAccessibility(newExcludedRegions));
                                }
                            }

                            return (AccessibilityLevel)Math.Max((byte)darkWorldSouth, (byte)darkWorldEast);
                        }

                        //  Inverted mode
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            AccessibilityLevel dWSouth = AccessibilityLevel.None;
                            AccessibilityLevel dWEast = AccessibilityLevel.None;
                            AccessibilityLevel lightWorld = AccessibilityLevel.None;

                            //  Access via South Dark World by flippers or fake flippers sequence break
                            if (!excludedRegions.Contains(RegionID.DarkWorldSouth))
                            {
                                if (_game.Items.Has(ItemType.Flippers))
                                    dWSouth = _game.Regions[RegionID.DarkWorldSouth].GetAccessibility(newExcludedRegions);
                                else
                                    dWSouth = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                        (byte)_game.Regions[RegionID.DarkWorldSouth].GetAccessibility(newExcludedRegions));
                            }

                            //  Access via East Dark World by flippers or fake flippers sequence break
                            if (!excludedRegions.Contains(RegionID.DarkWorldEast))
                            {
                                if (_game.Items.Has(ItemType.Flippers))
                                    dWEast = _game.Regions[RegionID.DarkWorldEast].GetAccessibility(newExcludedRegions);
                                else
                                    dWEast = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                        (byte)_game.Regions[RegionID.DarkWorldEast].GetAccessibility(newExcludedRegions));
                            }

                            //  Access via Light World by mirror
                            if (_game.Items.Has(ItemType.Mirror) && !excludedRegions.Contains(RegionID.LightWorld))
                                lightWorld = _game.Regions[RegionID.LightWorld].GetAccessibility(newExcludedRegions);

                            return (AccessibilityLevel)Math.Max(Math.Max((byte)dWSouth, (byte)dWEast), (byte)lightWorld);
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.DarkWorldSouthEastAccess]);
                    itemReqs.Add(_game.Items[ItemType.Flippers]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);

                    _observedRegions.Add(RegionID.DarkWorldSouth);
                    _observedRegions.Add(RegionID.DarkWorldEast);
                    _observedRegions.Add(RegionID.LightWorld);

                    break;
                case RegionID.DarkWorldWitchArea:

                    GetDirectAccessibility = () =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.DarkWorldWitchAreaAccess))
                            return AccessibilityLevel.Normal;

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };
                    GetIndirectAccessibility = (excludedRegions) =>
                    {
                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            AccessibilityLevel darkWorldEast = AccessibilityLevel.None;
                            AccessibilityLevel darkWorldWest = AccessibilityLevel.None;

                            if (_game.Items.Has(ItemType.MoonPearl))
                            {
                                //  Access via East Dark World by gloves, hammer, or flippers
                                if (!excludedRegions.Contains(RegionID.DarkWorldEast))
                                {
                                    if (_game.Items.Has(ItemType.Gloves) || _game.Items.Has(ItemType.Hammer) ||
                                        _game.Items.Has(ItemType.Flippers))
                                        darkWorldEast = _game.Regions[RegionID.DarkWorldEast].GetAccessibility(newExcludedRegions);
                                    else
                                        darkWorldEast = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                            (byte)_game.Regions[RegionID.DarkWorldEast].GetAccessibility(newExcludedRegions));
                                }

                                //  Access via West Dark World by flippers
                                if (!excludedRegions.Contains(RegionID.DarkWorldWest))
                                {
                                    if (_game.Items.Has(ItemType.Flippers))
                                        darkWorldWest = _game.Regions[RegionID.DarkWorldWest].GetAccessibility(newExcludedRegions);
                                    else
                                        darkWorldWest = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                            (byte)_game.Regions[RegionID.DarkWorldWest].GetAccessibility(newExcludedRegions));
                                }
                            }

                            return (AccessibilityLevel)Math.Max((byte)darkWorldEast, (byte)darkWorldWest);
                        }

                        //  Inverted mode
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            AccessibilityLevel darkWorldEast = AccessibilityLevel.None;
                            AccessibilityLevel darkWorldWest = AccessibilityLevel.None;
                            AccessibilityLevel lightWorld = AccessibilityLevel.None;

                            //  Access via East Dark World by gloves, hammer, flippers, or fake flippers sequence break
                            if (!excludedRegions.Contains(RegionID.DarkWorldEast))
                            {
                                if (_game.Items.Has(ItemType.Gloves) || _game.Items.Has(ItemType.Hammer) ||
                                    _game.Items.Has(ItemType.Flippers))
                                    darkWorldEast = _game.Regions[RegionID.DarkWorldEast].GetAccessibility(newExcludedRegions);
                                else
                                {
                                    darkWorldEast = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                        (byte)_game.Regions[RegionID.DarkWorldEast].GetAccessibility(newExcludedRegions));
                                }
                            }

                            //  Access via West Dark World by flippers
                            if (!excludedRegions.Contains(RegionID.DarkWorldWest))
                            {
                                if (_game.Items.Has(ItemType.Flippers))
                                    darkWorldWest = _game.Regions[RegionID.DarkWorldWest].GetAccessibility(newExcludedRegions);
                                else
                                {
                                    darkWorldWest = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                        (byte)_game.Regions[RegionID.DarkWorldWest].GetAccessibility(newExcludedRegions));
                                }
                            }

                            //  Access via Light World by mirror
                            if (_game.Items.Has(ItemType.Mirror) && !excludedRegions.Contains(RegionID.LightWorld))
                                lightWorld = _game.Regions[RegionID.LightWorld].GetAccessibility(newExcludedRegions);

                            return (AccessibilityLevel)Math.Max(Math.Max((byte)darkWorldEast, (byte)darkWorldWest),
                                (byte)lightWorld);
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.DarkWorldWitchAreaAccess]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.Flippers]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);

                    _observedRegions.Add(RegionID.DarkWorldEast);
                    _observedRegions.Add(RegionID.DarkWorldWest);
                    _observedRegions.Add(RegionID.LightWorld);

                    break;
                case RegionID.MireArea:

                    GetDirectAccessibility = () =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.MireAreaAccess))
                            return AccessibilityLevel.Normal;

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            //  Access via Flute drop spot by Mitts
                            if (_game.Items.Has(ItemType.Flute) && _game.Items.Has(ItemType.Gloves, 2))
                                return AccessibilityLevel.Normal;
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };
                    GetIndirectAccessibility = (excludedRegions) =>
                    {
                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        //  Inverted mode
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            //  Access via Flute drop spot (Flute activation requires Light World access + Moon Pearl)
                            //    or Light World by mirror
                            if ((_game.Items.Has(ItemType.Flute) && _game.Items.Has(ItemType.MoonPearl)) ||
                                _game.Items.Has(ItemType.Mirror) && !excludedRegions.Contains(RegionID.LightWorld))
                                return _game.Regions[RegionID.LightWorld].GetAccessibility(newExcludedRegions);
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MireAreaAccess]);
                    itemReqs.Add(_game.Items[ItemType.Flute]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);

                    _observedRegions.Add(RegionID.LightWorld);

                    break;
                case RegionID.DeathMountainWestBottom:

                    GetDirectAccessibility = () =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.DeathMountainWestBottomAccess))
                            return AccessibilityLevel.Normal;

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            //  Access via Flute drop spot
                            if (_game.Items.Has(ItemType.Flute))
                                return AccessibilityLevel.Normal;

                            //  Access via Death Mountain Entry cave (non-entrance shuffle only)
                            if (!_game.Mode.EntranceShuffle.Value && _game.Items.Has(ItemType.Gloves))
                            {
                                //  Lamp required by logic
                                if (_game.Items.Has(ItemType.Lamp))
                                    return AccessibilityLevel.Normal;

                                //  Sequence Break dark room
                                return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };
                    GetIndirectAccessibility = (excludedRegions) =>
                    {
                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            AccessibilityLevel dMWestTop = AccessibilityLevel.None;
                            AccessibilityLevel dMEastBottom = AccessibilityLevel.None;
                            AccessibilityLevel dDMWestBottom = AccessibilityLevel.None;

                            //  Access via West Death Mountain top
                            if (!excludedRegions.Contains(RegionID.DeathMountainWestTop))
                                dMWestTop = _game.Regions[RegionID.DeathMountainWestTop].GetAccessibility(newExcludedRegions);

                            //  Access via East Death Mountain bottom by hookshot
                            if (_game.Items.Has(ItemType.Hookshot) && !excludedRegions.Contains(RegionID.DeathMountainEastBottom))
                                dMEastBottom = _game.Regions[RegionID.DeathMountainEastBottom].GetAccessibility(newExcludedRegions);

                            //  Access via West Dark Death Mountain bottom by mirror
                            if (_game.Items.Has(ItemType.Mirror) && !excludedRegions.Contains(RegionID.DarkDeathMountainWestBottom))
                                dDMWestBottom = _game.Regions[RegionID.DarkDeathMountainWestBottom].GetAccessibility(newExcludedRegions);

                            return (AccessibilityLevel)Math.Max(Math.Max((byte)dMWestTop, (byte)dMEastBottom), (byte)dDMWestBottom);
                        }

                        //  Inverted mode
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            AccessibilityLevel dDMWestBottom = AccessibilityLevel.None;
                            AccessibilityLevel dMWestTop = AccessibilityLevel.None;
                            AccessibilityLevel dMEastBottom = AccessibilityLevel.None;

                            //  Access via West Dark Death Mountain bottom
                            if (!excludedRegions.Contains(RegionID.DarkDeathMountainWestBottom))
                                dDMWestBottom = _game.Regions[RegionID.DarkDeathMountainWestBottom].GetAccessibility(newExcludedRegions);

                            //  Access via West Death Mountain top
                            if (!excludedRegions.Contains(RegionID.DeathMountainWestTop))
                                dMWestTop = _game.Regions[RegionID.DeathMountainWestTop].GetAccessibility(newExcludedRegions);

                            //  Access via East Death Mountain by hookshot
                            if (_game.Items.Has(ItemType.Hookshot) && _game.Items.Has(ItemType.MoonPearl) &&
                                !excludedRegions.Contains(RegionID.DeathMountainEastBottom))
                                dMEastBottom = _game.Regions[RegionID.DeathMountainEastBottom].GetAccessibility(newExcludedRegions);

                            return (AccessibilityLevel)Math.Max(Math.Max((byte)dDMWestBottom, (byte)dMWestTop), (byte)dMEastBottom);
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.DeathMountainWestBottomAccess]);
                    itemReqs.Add(_game.Items[ItemType.Flute]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.Lamp]);
                    itemReqs.Add(_game.Items[ItemType.Hookshot]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    _observedRegions.Add(RegionID.DeathMountainWestTop);
                    _observedRegions.Add(RegionID.DeathMountainEastBottom);
                    _observedRegions.Add(RegionID.DarkDeathMountainWestBottom);

                    break;
                case RegionID.DeathMountainWestTop:

                    GetDirectAccessibility = () =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.DeathMountainWestTopAccess))
                            return AccessibilityLevel.Normal;

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };
                    GetIndirectAccessibility = (excludedRegions) =>
                    {
                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            AccessibilityLevel dMEastTop = AccessibilityLevel.None;
                            AccessibilityLevel dDMWestBottom = AccessibilityLevel.None;
                            AccessibilityLevel dDMTop = AccessibilityLevel.None;

                            //  Access via East Death Mountain by hammer
                            if (_game.Items.Has(ItemType.Hammer) && !excludedRegions.Contains(RegionID.DeathMountainEastTop))
                                dMEastTop = _game.Regions[RegionID.DeathMountainEastTop].GetAccessibility(newExcludedRegions);

                            if (_game.Items.Has(ItemType.Mirror))
                            {
                                //  Access via West Dark Death Mountain bottom via mirror
                                if (!excludedRegions.Contains(RegionID.DarkDeathMountainWestBottom))
                                    dDMWestBottom = _game.Regions[RegionID.DarkDeathMountainWestBottom].GetAccessibility(newExcludedRegions);

                                //  Access via Dark Death Mountain top exits via mirror
                                if (!excludedRegions.Contains(RegionID.DarkDeathMountainTop))
                                    dDMTop = _game.Regions[RegionID.DarkDeathMountainTop].GetAccessibility(newExcludedRegions);
                            }

                            return (AccessibilityLevel)Math.Max(Math.Max((byte)dMEastTop, (byte)dDMWestBottom), (byte)dDMTop);
                        }

                        //  Inverted mode
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            //  Access via East Death Mountain top by hammer
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Hammer) &&
                                !excludedRegions.Contains(RegionID.DeathMountainEastTop))
                                return _game.Regions[RegionID.DeathMountainEastTop].GetAccessibility(newExcludedRegions);
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.DeathMountainWestTopAccess]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    _observedRegions.Add(RegionID.DeathMountainEastTop);
                    _observedRegions.Add(RegionID.DarkDeathMountainWestBottom);
                    _observedRegions.Add(RegionID.DarkDeathMountainTop);

                    break;
                case RegionID.DeathMountainEastBottom:

                    GetDirectAccessibility = () =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.DeathMountainEastTopConnectorAccess) ||
                            _game.Items.Has(ItemType.DeathMountainEastBottomAccess) ||
                            _game.Items.Has(ItemType.SpiralCaveTopAccess))
                            return AccessibilityLevel.Normal;

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            //  Access via Turtle Rock Tunnel by mirror
                            if (_game.Items.Has(ItemType.TurtleRockTunnelAccess) ||
                                _game.Items.Has(ItemType.TurtleRockSafetyDoorAccess) &&
                                _game.Items.Has(ItemType.Mirror))
                                return AccessibilityLevel.Normal;
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };
                    GetIndirectAccessibility = (excludedRegions) =>
                    {
                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            AccessibilityLevel dMEastTop = AccessibilityLevel.None;
                            AccessibilityLevel dMWestBottom = AccessibilityLevel.None;
                            AccessibilityLevel dDMEastBottom = AccessibilityLevel.None;

                            //  Access via East Death Mountain top
                            if (!excludedRegions.Contains(RegionID.DeathMountainEastTop))
                                dMEastTop = _game.Regions[RegionID.DeathMountainEastTop].GetAccessibility(newExcludedRegions);

                            //  Access via West Death Mountain bottom by hookshot
                            if (_game.Items.Has(ItemType.Hookshot) && !excludedRegions.Contains(RegionID.DeathMountainWestBottom))
                                dMWestBottom = _game.Regions[RegionID.DeathMountainWestBottom].GetAccessibility(newExcludedRegions);

                            //  Access via East Dark Death Mountain by mirror
                            if (_game.Items.Has(ItemType.Mirror) && !excludedRegions.Contains(RegionID.DarkDeathMountainEastBottom))
                                dDMEastBottom = _game.Regions[RegionID.DarkDeathMountainEastBottom].GetAccessibility(newExcludedRegions);

                            return (AccessibilityLevel)Math.Max(Math.Max((byte)dMEastTop, (byte)dMWestBottom), (byte)dDMEastBottom);
                        }

                        //  Inverted mode
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            AccessibilityLevel dDMEastBottom = AccessibilityLevel.None;
                            AccessibilityLevel dMEastTop = AccessibilityLevel.None;
                            AccessibilityLevel dMWestBottom = AccessibilityLevel.None;

                            //  Access via East Dark Death Mountain portal
                            if (_game.Items.Has(ItemType.Gloves, 2) && !excludedRegions.Contains(RegionID.DarkDeathMountainEastBottom))
                                dDMEastBottom = _game.Regions[RegionID.DarkDeathMountainEastBottom].GetAccessibility(newExcludedRegions);

                            //  Access via East Death Mountain top
                            if (!excludedRegions.Contains(RegionID.DeathMountainEastTop))
                                dMEastTop = _game.Regions[RegionID.DeathMountainEastTop].GetAccessibility(newExcludedRegions);

                            //  Access via West Death Mountain bottom by hookshot
                            if (_game.Items.Has(ItemType.Hookshot) && _game.Items.Has(ItemType.MoonPearl) &&
                                !excludedRegions.Contains(RegionID.DeathMountainWestBottom))
                                dMWestBottom = _game.Regions[RegionID.DeathMountainWestBottom].GetAccessibility(newExcludedRegions);

                            return (AccessibilityLevel)Math.Max(Math.Max((byte)dDMEastBottom, (byte)dMEastTop), (byte)dMWestBottom);
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.DeathMountainEastTopConnectorAccess]);
                    itemReqs.Add(_game.Items[ItemType.DeathMountainEastBottomAccess]);
                    itemReqs.Add(_game.Items[ItemType.SpiralCaveTopAccess]);
                    itemReqs.Add(_game.Items[ItemType.TurtleRockTunnelAccess]);
                    itemReqs.Add(_game.Items[ItemType.TurtleRockSafetyDoorAccess]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.Hookshot]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    _observedRegions.Add(RegionID.DeathMountainEastTop);
                    _observedRegions.Add(RegionID.DeathMountainWestBottom);
                    _observedRegions.Add(RegionID.DarkDeathMountainEastBottom);

                    break;
                case RegionID.DeathMountainEastTop:

                    GetDirectAccessibility = () =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.DeathMountainEastTopAccess))
                            return AccessibilityLevel.Normal;

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };
                    GetIndirectAccessibility = (excludedRegions) =>
                    {
                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            AccessibilityLevel dDMTop = AccessibilityLevel.None;
                            AccessibilityLevel dMWestTop = AccessibilityLevel.None;
                            AccessibilityLevel dMEastBottom = AccessibilityLevel.None;

                            //  Access via Dark Death Mountain top by mirror
                            if (_game.Items.Has(ItemType.Mirror) && !excludedRegions.Contains(RegionID.DarkDeathMountainTop))
                                dDMTop = _game.Regions[RegionID.DarkDeathMountainTop].GetAccessibility(newExcludedRegions);

                            //  Access via West Death Mountain top by hammer
                            if (_game.Items.Has(ItemType.Hammer) && !excludedRegions.Contains(RegionID.DeathMountainWestTop))
                                dMWestTop = _game.Regions[RegionID.DeathMountainWestTop].GetAccessibility(newExcludedRegions);

                            //  Access via Paradox Cave (non-entrance shuffle only)
                            if (!game.Mode.EntranceShuffle.Value && !excludedRegions.Contains(RegionID.DeathMountainEastBottom))
                                dMEastBottom = _game.Regions[RegionID.DeathMountainEastBottom].GetAccessibility(newExcludedRegions);

                            return (AccessibilityLevel)Math.Max(Math.Max((byte)dDMTop, (byte)dMWestTop), (byte)dMEastBottom);
                        }

                        //  Inverted mode
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            AccessibilityLevel dMWestTop = AccessibilityLevel.None;
                            AccessibilityLevel dDMTop = AccessibilityLevel.None;
                            AccessibilityLevel dMEastBottom = AccessibilityLevel.None;

                            //  Access via Paradox Cave (non-entrance shuffle only)
                            if (!_game.Mode.EntranceShuffle.Value && !excludedRegions.Contains(RegionID.DeathMountainEastBottom))
                                dMEastBottom = _game.Regions[RegionID.DeathMountainEastBottom].GetAccessibility(newExcludedRegions);

                            if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.MoonPearl))
                            {
                                //  Access via West Death Mountain top by hammer
                                if (!excludedRegions.Contains(RegionID.DeathMountainWestTop))
                                    dMWestTop = _game.Regions[RegionID.DeathMountainWestTop].GetAccessibility(newExcludedRegions);

                                // Access via Turtle Rock portal
                                if (_game.Items.Has(ItemType.Gloves, 2) && !excludedRegions.Contains(RegionID.DarkDeathMountainTop))
                                    dDMTop = _game.Regions[RegionID.DarkDeathMountainTop].GetAccessibility(newExcludedRegions);
                            }

                            return (AccessibilityLevel)Math.Max(Math.Max((byte)dMWestTop, (byte)dDMTop), (byte)dMEastBottom);
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.DeathMountainEastTopAccess]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);

                    _observedRegions.Add(RegionID.DarkDeathMountainTop);
                    _observedRegions.Add(RegionID.DeathMountainWestTop);
                    _observedRegions.Add(RegionID.DeathMountainEastBottom);

                    break;
                case RegionID.DarkDeathMountainTop:

                    GetDirectAccessibility = () =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.DarkDeathMountainFloatingIslandAccess) ||
                            _game.Items.Has(ItemType.DarkDeathMountainTopAccess))
                            return AccessibilityLevel.Normal;

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };
                    GetIndirectAccessibility = (excludedRegions) =>
                    {
                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            AccessibilityLevel dMEastTop = AccessibilityLevel.None;
                            AccessibilityLevel dDMEastBottom = AccessibilityLevel.None;

                            if (_game.Items.Has(ItemType.MoonPearl))
                            {
                                //  Access via Turtle Rock access portal
                                if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.Gloves, 2) &&
                                    !excludedRegions.Contains(RegionID.DeathMountainEastTop))
                                    dMEastTop = _game.Regions[RegionID.DeathMountainEastTop].GetAccessibility(newExcludedRegions);

                                //  Access via Super-Bunny cave (non-entrance shuffle only)
                                if (!_game.Mode.EntranceShuffle.Value && !excludedRegions.Contains(RegionID.DarkDeathMountainEastBottom))
                                    dDMEastBottom = _game.Regions[RegionID.DarkDeathMountainEastBottom].GetAccessibility(newExcludedRegions);
                            }

                            return (AccessibilityLevel)Math.Max((byte)dMEastTop, (byte)dDMEastBottom);
                        }

                        //  Inverted mode
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            AccessibilityLevel dDMWestBottom = AccessibilityLevel.None;
                            AccessibilityLevel dDMEastBottom = AccessibilityLevel.None;
                            AccessibilityLevel dMWestTop = AccessibilityLevel.None;
                            AccessibilityLevel dMEastTop = AccessibilityLevel.None;

                            //  Access via West Dark Death Mountain bottom
                            if (!excludedRegions.Contains(RegionID.DarkDeathMountainWestBottom))
                                dDMWestBottom = _game.Regions[RegionID.DarkDeathMountainWestBottom].GetAccessibility(newExcludedRegions);

                            //  Access via Super-Bunny Cave (non-entrance shuffle only)
                            if (!_game.Mode.EntranceShuffle.Value && !excludedRegions.Contains(RegionID.DarkDeathMountainEastBottom))
                                dDMEastBottom = _game.Regions[RegionID.DarkDeathMountainEastBottom].GetAccessibility(newExcludedRegions);

                            if (_game.Items.Has(ItemType.Mirror))
                            {
                                //  Access via West Death Mountain top by mirror
                                if (!excludedRegions.Contains(RegionID.DeathMountainWestTop))
                                    dMWestTop = _game.Regions[RegionID.DeathMountainWestTop].GetAccessibility(newExcludedRegions);

                                //  Access via East Death Mountain top by mirror
                                if (!excludedRegions.Contains(RegionID.DeathMountainEastTop))
                                    dMEastTop = _game.Regions[RegionID.DeathMountainEastTop].GetAccessibility(newExcludedRegions);
                            }

                            return (AccessibilityLevel)Math.Max(Math.Max(Math.Max((byte)dDMWestBottom, (byte)dDMEastBottom),
                                (byte)dMWestTop), (byte)dMEastTop);
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.DarkDeathMountainFloatingIslandAccess]);
                    itemReqs.Add(_game.Items[ItemType.DarkDeathMountainTopAccess]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);

                    _observedRegions.Add(RegionID.DeathMountainEastTop);
                    _observedRegions.Add(RegionID.DarkDeathMountainEastBottom);
                    _observedRegions.Add(RegionID.DarkDeathMountainWestBottom);
                    _observedRegions.Add(RegionID.DeathMountainWestTop);

                    break;
                case RegionID.DarkDeathMountainEastBottom:

                    GetDirectAccessibility = () =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.DarkDeathMountainEastBottomAccess))
                            return AccessibilityLevel.Normal;

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };
                    GetIndirectAccessibility = (excludedRegions) =>
                    {
                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            AccessibilityLevel dDMTop = AccessibilityLevel.None;
                            AccessibilityLevel dMEastBottom = AccessibilityLevel.None;

                            //  Access via Dark Death Mountain top
                            if (!excludedRegions.Contains(RegionID.DarkDeathMountainTop))
                                dDMTop = _game.Regions[RegionID.DarkDeathMountainTop].GetAccessibility(newExcludedRegions);

                            //  Access via East Death Mountain bottom portal
                            if (_game.Items.Has(ItemType.Gloves, 2) && !excludedRegions.Contains(RegionID.DeathMountainEastBottom))
                                dMEastBottom = _game.Regions[RegionID.DeathMountainEastBottom].GetAccessibility(newExcludedRegions);

                            return (AccessibilityLevel)Math.Max((byte)dDMTop, (byte)dMEastBottom);
                        }

                        //  Inverted mode
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            AccessibilityLevel dDMTop = AccessibilityLevel.None;
                            AccessibilityLevel dMEastBottom = AccessibilityLevel.None;

                            //  Access via Dark Death Mountain top
                            if (!excludedRegions.Contains(RegionID.DarkDeathMountainTop))
                                dDMTop = _game.Regions[RegionID.DarkDeathMountainTop].GetAccessibility(newExcludedRegions);

                            //  Access via East Death Mountain bottom by mirror
                            if (_game.Items.Has(ItemType.Mirror) && !excludedRegions.Contains(RegionID.DeathMountainEastBottom))
                                dMEastBottom = _game.Regions[RegionID.DeathMountainEastBottom].GetAccessibility(newExcludedRegions);

                            return (AccessibilityLevel)Math.Max((byte)dDMTop, (byte)dMEastBottom);
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.DarkDeathMountainEastBottomAccess]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);

                    _observedRegions.Add(RegionID.DarkDeathMountainTop);
                    _observedRegions.Add(RegionID.DeathMountainEastBottom);

                    break;
                case RegionID.DarkDeathMountainWestBottom:

                    GetDirectAccessibility = () =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.DarkDeathMountainWestBottomAccess))
                            return AccessibilityLevel.Normal;

                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            //  Access via Death Mountain Entry cave (non-entrance shuffle only)
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
                    GetIndirectAccessibility = (excludedRegions) =>
                    {
                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            AccessibilityLevel dDMTop = AccessibilityLevel.None;
                            AccessibilityLevel dMWestBottom = AccessibilityLevel.None;

                            //  Access via Dark Death Mountain top
                            if (!excludedRegions.Contains(RegionID.DarkDeathMountainTop))
                                dDMTop = _game.Regions[RegionID.DarkDeathMountainTop].GetAccessibility(newExcludedRegions);

                            //  Access via West Death Mountain bottom portal
                            if (!excludedRegions.Contains(RegionID.DeathMountainWestBottom))
                                dMWestBottom = _game.Regions[RegionID.DeathMountainWestBottom].GetAccessibility(newExcludedRegions);

                            return (AccessibilityLevel)Math.Max((byte)dDMTop, (byte)dMWestBottom);
                        }

                        //  Inverted mode
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            AccessibilityLevel lightWorld = AccessibilityLevel.None;
                            AccessibilityLevel dDMTop = AccessibilityLevel.None;
                            AccessibilityLevel dMWestBottom = AccessibilityLevel.None;

                            //  Access via Flute drop spot
                            if (_game.Items.Has(ItemType.Flute) && _game.Items.Has(ItemType.MoonPearl) &&
                                !excludedRegions.Contains(RegionID.LightWorld))
                                lightWorld = _game.Regions[RegionID.LightWorld].GetAccessibility(newExcludedRegions);

                            //  Access via Dark Death Mountain top
                            if (!excludedRegions.Contains(RegionID.DarkDeathMountainTop))
                                dDMTop = _game.Regions[RegionID.DarkDeathMountainTop].GetAccessibility(newExcludedRegions);

                            //  Access via West Death Mountain bottom by mirror
                            if (_game.Items.Has(ItemType.Mirror) && !excludedRegions.Contains(RegionID.DeathMountainWestBottom))
                                dMWestBottom = _game.Regions[RegionID.DeathMountainWestBottom].GetAccessibility(newExcludedRegions);

                            return (AccessibilityLevel)Math.Max(Math.Max((byte)lightWorld, (byte)dDMTop), (byte)dMWestBottom);
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.DarkDeathMountainWestBottomAccess]);
                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.Lamp]);
                    itemReqs.Add(_game.Items[ItemType.Flute]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);

                    _observedRegions.Add(RegionID.DarkDeathMountainTop);
                    _observedRegions.Add(RegionID.DeathMountainWestBottom);
                    _observedRegions.Add(RegionID.LightWorld);

                    break;
                case RegionID.TurtleRockTunnel:

                    GetDirectAccessibility = () =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.TurtleRockTunnelAccess))
                            return AccessibilityLevel.Normal;

                        //  Inverted mode
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if ((_game.Items.Has(ItemType.SpiralCaveTopAccess) ||
                                _game.Items.Has(ItemType.MimicCaveAccess)) &&
                                _game.Items.Has(ItemType.Mirror))
                                return AccessibilityLevel.Normal;
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };
                    GetIndirectAccessibility = (excludedRegions) =>
                    {
                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        // Inverted mode
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            //  Access via East Death Mountain top by mirror from Spiral Cave or Mimic Cave ledges
                            if (_game.Items.Has(ItemType.Mirror) && !excludedRegions.Contains(RegionID.DeathMountainEastTop))
                                return _game.Regions[RegionID.DeathMountainEastTop].Accessibility;
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.TurtleRockTunnelAccess]);
                    itemReqs.Add(_game.Items[ItemType.SpiralCaveTopAccess]);
                    itemReqs.Add(_game.Items[ItemType.MimicCaveAccess]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);

                    _observedRegions.Add(RegionID.DeathMountainEastTop);

                    break;
                case RegionID.HyruleCastle:

                    GetDirectAccessibility = () =>
                    {
                        //  Access to dungeons assumed available with entrance shuffle
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                            return AccessibilityLevel.Normal;

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };
                    GetIndirectAccessibility = (excludedRegions) =>
                    {
                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        //  Inverted mode
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            //  Access via Light World with Moon Pearl
                            if (_game.Items.Has(ItemType.MoonPearl) && !excludedRegions.Contains(RegionID.LightWorld))
                                return _game.Regions[RegionID.LightWorld].GetAccessibility(newExcludedRegions);
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    _observedRegions.Add(RegionID.LightWorld);

                    break;
                case RegionID.Agahnim:

                    GetDirectAccessibility = () =>
                    {
                        //  Access to dungeons assumed available with entrance shuffle
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            //  Access via Light World entrance
                            if (_game.Items.CanClearAgaTowerBarrier())
                                return AccessibilityLevel.Normal;
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };
                    GetIndirectAccessibility = (excludedRegions) =>
                    {
                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        //  Inverted mode
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (!excludedRegions.Contains(RegionID.DarkDeathMountainTop))
                                return _game.Regions[RegionID.DarkDeathMountainTop].Accessibility;
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Cape]);
                    itemReqs.Add(_game.Items[ItemType.Sword]);
                    itemReqs.Add(_game.Items[ItemType.Hammer]);

                    _observedRegions.Add(RegionID.DarkDeathMountainTop);

                    break;
                case RegionID.EasternPalace:

                    GetDirectAccessibility = () =>
                    {
                        //  Access to dungeons assumed available with entrance shuffle
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                            return AccessibilityLevel.Normal;

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };
                    GetIndirectAccessibility = (excludedRegions) =>
                    {
                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        //  Inverted mode
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            //  Access via Light World entrance
                            if (_game.Items.Has(ItemType.MoonPearl) && !excludedRegions.Contains(RegionID.LightWorld))
                                return _game.Regions[RegionID.LightWorld].GetAccessibility(newExcludedRegions);
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    _observedRegions.Add(RegionID.LightWorld);

                    break;
                case RegionID.DesertPalace:

                    GetDirectAccessibility = () =>
                    {
                        //  Access to dungeons assumed available with entrance shuffle
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            //  Access via Light World entrance by book
                            if (_game.Items.Has(ItemType.Book))
                                return AccessibilityLevel.Normal;
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };
                    GetIndirectAccessibility = (excludedRegions) =>
                    {
                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            //  Access via Mire Area by Mirror
                            if (_game.Items.Has(ItemType.Mirror))
                                return _game.Regions[RegionID.MireArea].GetAccessibility(newExcludedRegions);
                        }

                        //  Inverted mode
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            //  Access via Light World entrance by book
                            if (_game.Items.Has(ItemType.Book) && _game.Items.Has(ItemType.MoonPearl) &&
                                !excludedRegions.Contains(RegionID.LightWorld))
                                return _game.Regions[RegionID.LightWorld].GetAccessibility(newExcludedRegions);
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.Book]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    _observedRegions.Add(RegionID.MireArea);
                    _observedRegions.Add(RegionID.LightWorld);

                    break;
                case RegionID.TowerOfHera:

                    GetDirectAccessibility = () =>
                    {
                        //  Access to dungeons assumed available with entrance shuffle
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };
                    GetIndirectAccessibility = (excludedRegions) =>
                    {
                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (!excludedRegions.Contains(RegionID.DeathMountainWestTop))
                                return _game.Regions[RegionID.DeathMountainWestTop].GetAccessibility(newExcludedRegions);
                        }

                        //  Inverted mode
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            //  Access via West Death Mountain top entrance
                            if (_game.Items.Has(ItemType.MoonPearl) && !excludedRegions.Contains(RegionID.DeathMountainWestTop))
                                return _game.Regions[RegionID.DeathMountainWestTop].GetAccessibility(newExcludedRegions);
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    _observedRegions.Add(RegionID.DeathMountainWestTop);

                    break;
                case RegionID.PalaceOfDarkness:

                    GetDirectAccessibility = () =>
                    {
                        //  Access to dungeons assumed available with entrance shuffle
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };
                    GetIndirectAccessibility = (excludedRegions) =>
                    {
                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (!excludedRegions.Contains(RegionID.DarkWorldEast))
                                return _game.Regions[RegionID.DarkWorldEast].GetAccessibility(newExcludedRegions);
                        }

                        //  Inverted mode
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            //  Access via East Dark World entrance
                            if (_game.Items.Has(ItemType.MoonPearl) && !excludedRegions.Contains(RegionID.DarkWorldEast))
                                return _game.Regions[RegionID.DarkWorldEast].GetAccessibility(newExcludedRegions);
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    _observedRegions.Add(RegionID.DarkWorldEast);

                    break;
                case RegionID.SwampPalace:

                    GetDirectAccessibility = () =>
                    {
                        //  Access to dungeons assumed available with entrance shuffle
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };
                    GetIndirectAccessibility = (excludedRegions) =>
                    {
                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        //  Access to both the Dam and Swamp Palace entrance is required
                        if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Mirror))
                        {
                            if (_game.Mode.WorldState == WorldState.StandardOpen)
                            {
                                if (!excludedRegions.Contains(RegionID.DarkWorldSouth))
                                    return _game.Regions[RegionID.DarkWorldSouth].GetAccessibility(newExcludedRegions);
                            }

                            if (_game.Mode.WorldState == WorldState.Inverted)
                            {
                                if (!excludedRegions.Contains(RegionID.LightWorld))
                                    return _game.Regions[RegionID.LightWorld].GetAccessibility(newExcludedRegions);
                            }
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Mirror]);

                    _observedRegions.Add(RegionID.DarkWorldSouth);
                    _observedRegions.Add(RegionID.LightWorld);

                    break;
                case RegionID.SkullWoods:

                    GetDirectAccessibility = () =>
                    {
                        //  Access to dungeons assumed available with entrance shuffle
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };
                    GetIndirectAccessibility = (excludedRegions) =>
                    {
                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && !excludedRegions.Contains(RegionID.DarkWorldWest))
                                return _game.Regions[RegionID.DarkWorldWest].GetAccessibility(newExcludedRegions);
                        }

                        //  Inverted mode
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            //  Access via West Dark World entrances
                            if (!excludedRegions.Contains(RegionID.DarkWorldWest))
                                return _game.Regions[RegionID.DarkWorldWest].GetAccessibility(newExcludedRegions);
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    _observedRegions.Add(RegionID.DarkWorldWest);

                    break;
                case RegionID.ThievesTown:

                    GetDirectAccessibility = () =>
                    {
                        //  Access to dungeons assumed available with entrance shuffle
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };
                    GetIndirectAccessibility = (excludedRegions) =>
                    {
                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && !excludedRegions.Contains(RegionID.DarkWorldWest))
                                return _game.Regions[RegionID.DarkWorldWest].GetAccessibility(newExcludedRegions);
                        }

                        //  Inverted mode
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            //  Access via West Dark World entrance
                            if (!excludedRegions.Contains(RegionID.DarkWorldWest))
                                return _game.Regions[RegionID.DarkWorldWest].GetAccessibility(newExcludedRegions);
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    _observedRegions.Add(RegionID.DarkWorldWest);

                    break;
                case RegionID.IcePalace:

                    GetDirectAccessibility = () =>
                    {
                        //  Access to dungeons assumed available with entrance shuffle
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            //  Access via Lake Hylia portal
                            if (_game.Items.Has(ItemType.Gloves, 2) && _game.Items.Has(ItemType.MoonPearl))
                            {
                                //  Logic requires Flippers
                                if (_game.Items.Has(ItemType.Flippers))
                                    return AccessibilityLevel.Normal;

                                //  Fake Flippers sequence break
                                return AccessibilityLevel.SequenceBreak;
                            }
                        }

                        //  Inverted mode
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            //  Access via South Dark World entrance by flippers
                            if (_game.Items.Has(ItemType.Flippers))
                                return AccessibilityLevel.Normal;

                            //  Fake Flippers sequence break
                            return AccessibilityLevel.SequenceBreak;
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };
                    GetIndirectAccessibility = (excludedRegions) => { return AccessibilityLevel.None; };

                    itemReqs.Add(_game.Items[ItemType.Gloves]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);
                    itemReqs.Add(_game.Items[ItemType.Flippers]);

                    break;
                case RegionID.MiseryMire:

                    GetDirectAccessibility = () =>
                    {
                        //  Access to dungeons assumed available with entrance shuffle
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };
                    GetIndirectAccessibility = (excludedRegions) =>
                    {
                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            //  Access via Mire Area entrance by proper medallion
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
                                _game.Items[ItemType.QuakeDungeons].Current == 3))) &&
                                !excludedRegions.Contains(RegionID.MireArea))
                                return _game.Regions[RegionID.MireArea].GetAccessibility(newExcludedRegions);
                        }

                        //  Inverted mode
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            //  Access via Mire Area by proper medallion
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
                                _game.Items[ItemType.QuakeDungeons].Current == 3))) &&
                                !excludedRegions.Contains(RegionID.MireArea))
                                return _game.Regions[RegionID.MireArea].GetAccessibility(newExcludedRegions);
                        }

                        //  Default to no access
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

                    _observedRegions.Add(RegionID.MireArea);

                    break;
                case RegionID.TurtleRock:

                    GetDirectAccessibility = () =>
                    {
                        //  Access to dungeons assumed available with entrance shuffle
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };
                    GetIndirectAccessibility = (excludedRegions) =>
                    {
                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            //  Access via Dark Death Mountain top front entrance by proper medallion
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Hammer) &&
                                _game.Items.CanUseMedallions() && ((_game.Items.Has(ItemType.Bombos) &&
                                _game.Items.Has(ItemType.Ether) && _game.Items.Has(ItemType.Quake)) ||
                                (_game.Items.Has(ItemType.Bombos) &&
                                _game.Items[ItemType.BombosDungeons].Current >= 2) ||
                                (_game.Items.Has(ItemType.Ether) &&
                                _game.Items[ItemType.EtherDungeons].Current >= 2) ||
                                (_game.Items.Has(ItemType.Quake) &&
                                _game.Items[ItemType.QuakeDungeons].Current >= 2)) &&
                                !excludedRegions.Contains(RegionID.DarkDeathMountainTop))
                                return _game.Regions[RegionID.DarkDeathMountainTop].GetAccessibility(newExcludedRegions);
                        }

                        //  Inverted mode
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            AccessibilityLevel dMEastTop = AccessibilityLevel.None;
                            AccessibilityLevel dDMTop = AccessibilityLevel.None;

                            //  Access via back entrances by mirror from East Death Mountain top
                            if (_game.Items.Has(ItemType.Mirror) && !excludedRegions.Contains(RegionID.DeathMountainEastTop))
                                dMEastTop = _game.Regions[RegionID.DeathMountainEastTop].GetAccessibility(newExcludedRegions);

                            //  Access via Dark Death Mountain top entrance
                            if (_game.Items.CanUseMedallions() && ((_game.Items.Has(ItemType.Bombos) &&
                                _game.Items.Has(ItemType.Ether) && _game.Items.Has(ItemType.Quake)) ||
                                (_game.Items.Has(ItemType.Bombos) &&
                                _game.Items[ItemType.BombosDungeons].Current >= 2) ||
                                (_game.Items.Has(ItemType.Ether) &&
                                _game.Items[ItemType.EtherDungeons].Current >= 2) ||
                                (_game.Items.Has(ItemType.Quake) &&
                                _game.Items[ItemType.QuakeDungeons].Current >= 2)) &&
                                !excludedRegions.Contains(RegionID.DarkDeathMountainTop))
                                dDMTop = _game.Regions[RegionID.DarkDeathMountainTop].GetAccessibility(newExcludedRegions);

                            return (AccessibilityLevel)Math.Max((byte)dMEastTop, (byte)dDMTop);
                        }

                        //  Default to no access
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

                    _observedRegions.Add(RegionID.DeathMountainEastTop);
                    _observedRegions.Add(RegionID.DarkDeathMountainTop);

                    break;
                case RegionID.GanonsTower:

                    GetDirectAccessibility = () =>
                    {
                        //  Access to dungeons assumed available with entrance shuffle
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

                        return AccessibilityLevel.None;
                    };
                    GetIndirectAccessibility = (excludedRegions) =>
                    {
                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            //  Access via Dark Death Mountain top entrance with proper number of crystals
                            if (_game.Items.Has(ItemType.TowerCrystals) && _game.Items.Has(ItemType.MoonPearl) &&
                                !excludedRegions.Contains(RegionID.DarkDeathMountainTop))
                                return _game.Regions[RegionID.DarkDeathMountainTop].GetAccessibility(newExcludedRegions);
                        }

                        //  Inverted mode
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            //  Access via Light World entrance with proper amount of crystals
                            if (_game.Items.Has(ItemType.TowerCrystals) && _game.Items.Has(ItemType.MoonPearl) &&
                                !excludedRegions.Contains(RegionID.LightWorld))
                                return _game.Regions[RegionID.LightWorld].GetAccessibility(newExcludedRegions);
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    itemReqs.Add(_game.Items[ItemType.TowerCrystals]);
                    itemReqs.Add(_game.Items[ItemType.Crystal]);
                    itemReqs.Add(_game.Items[ItemType.RedCrystal]);
                    itemReqs.Add(_game.Items[ItemType.MoonPearl]);

                    _observedRegions.Add(RegionID.DarkDeathMountainTop);
                    _observedRegions.Add(RegionID.LightWorld);
                    break;
            }

            foreach (Item itemReq in itemReqs)
                itemReq.PropertyChanged += OnItemRequirementChanged;
        }

        private void UpdateAccessibility()
        {
            DirectAccessibility = GetDirectAccessibility();
            Accessibility = (AccessibilityLevel)Math.Max((byte)DirectAccessibility,
                (byte)GetIndirectAccessibility(new List<RegionID>()));
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

        public AccessibilityLevel GetAccessibility(List<RegionID> excludedRegions)
        {
            if (DirectAccessibility == AccessibilityLevel.Normal)
                return DirectAccessibility;

            AccessibilityLevel indirectAccessibility = GetIndirectAccessibility(excludedRegions);

            return (AccessibilityLevel)Math.Max((byte)DirectAccessibility,
                (byte)indirectAccessibility);
        }

        public void SubscribeToRegions()
        {
            foreach (RegionID iD in _observedRegions)
                _game.Regions[iD].PropertyChanged += OnItemRequirementChanged;
        }
    }
}
