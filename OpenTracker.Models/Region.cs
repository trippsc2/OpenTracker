using OpenTracker.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models
{
    public class Region : INotifyPropertyChanged
    {
        private readonly Game _game;
        private readonly Dictionary<RegionID, Mode> _regionSubscriptions;
        private readonly Dictionary<RegionID, bool> _regionIsSubscribed;
        private readonly Dictionary<ItemType, Mode> _itemSubscriptions;
        private readonly Dictionary<ItemType, bool> _itemIsSubscribed;

        public RegionID ID { get; }
        public Func<List<RegionID>, AccessibilityLevel> GetAccessibility { get; }

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

            _regionSubscriptions = new Dictionary<RegionID, Mode>();
            _regionIsSubscribed = new Dictionary<RegionID, bool>();
            _itemSubscriptions = new Dictionary<ItemType, Mode>();
            _itemIsSubscribed = new Dictionary<ItemType, bool>();

            switch (ID)
            {
                case RegionID.LightWorld:

                    GetAccessibility = (excludedRegions) =>
                    {
                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            //  Access from start
                            return AccessibilityLevel.Normal;
                        }

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

                    _itemSubscriptions.Add(ItemType.Aga, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.LightWorldAccess, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.DeathMountainExitAccess, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.RaceGameAccess, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.HyruleCastleSecondFloorAccess, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.DesertLeftAccess, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.GrassHouseAccess, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.WitchsHutAccess, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.Gloves, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.DesertBackAccess, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.LakeHyliaFairyIslandAccess, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.Flippers, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.WaterfallFairyAccess, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case RegionID.HyruleCastleSecondFloor:

                    GetAccessibility = (excludedRegions) =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.HyruleCastleSecondFloorAccess))
                            return AccessibilityLevel.Normal;

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

                    _regionSubscriptions.Add(RegionID.DarkWorldEast, new Mode() { WorldState = WorldState.StandardOpen });

                    _itemSubscriptions.Add(ItemType.HyruleCastleSecondFloorAccess, new Mode());
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.StandardOpen });

                    break;
                case RegionID.DarkWorldWest:

                    GetAccessibility = (excludedRegions) =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.DarkWorldWestAccess) ||
                            _game.Items.Has(ItemType.BumperCaveAccess))
                            return AccessibilityLevel.Normal;

                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

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

                                //  Access via Dark World Witch area by hookshot
                                if (_game.Items.Has(ItemType.Hookshot) && !excludedRegions.Contains(RegionID.DarkWorldWitchArea))
                                    return _game.Regions[RegionID.DarkWorldWitchArea].GetAccessibility(newExcludedRegions);
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

                    _regionSubscriptions.Add(RegionID.DarkWorldWitchArea, new Mode());
                    _regionSubscriptions.Add(RegionID.DarkWorldSouth, new Mode() { WorldState = WorldState.Inverted });
                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode() { WorldState = WorldState.Inverted });

                    _itemSubscriptions.Add(ItemType.DarkWorldWestAccess, new Mode());
                    _itemSubscriptions.Add(ItemType.BumperCaveAccess, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.HammerHouseAccess, new Mode());
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.Gloves, new Mode());
                    _itemSubscriptions.Add(ItemType.Hookshot, new Mode());
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case RegionID.DarkWorldSouth:

                    GetAccessibility = (excludedRegions) =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.DarkWorldSouthAccess))
                            return AccessibilityLevel.Normal;

                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            //  Access via north of Dam portal
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Hammer) &&
                                _game.Items.Has(ItemType.Gloves))
                                return AccessibilityLevel.Normal;

                            AccessibilityLevel dWWest = AccessibilityLevel.None;
                            AccessibilityLevel dWEast = AccessibilityLevel.None;

                            //  Access via Dark World West
                            if (!excludedRegions.Contains(RegionID.DarkWorldWest))
                                dWWest = _game.Regions[RegionID.DarkWorldWest].GetAccessibility(newExcludedRegions);

                            //  Access via Dark World East by hammer
                            if (_game.Items.Has(ItemType.Hammer) && _game.Items.Has(ItemType.MoonPearl) &&
                                !excludedRegions.Contains(RegionID.DarkWorldEast))
                                dWEast = _game.Regions[RegionID.DarkWorldEast].GetAccessibility(newExcludedRegions);

                            return (AccessibilityLevel)Math.Max((byte)dWWest, (byte)dWEast);
                        }

                        //  Inverted mode
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            //  Access is provided from the start in non-entrance shuffle
                            if (!_game.Mode.EntranceShuffle.Value)
                                return AccessibilityLevel.Normal;

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

                    _regionSubscriptions.Add(RegionID.DarkWorldWest, new Mode());
                    _regionSubscriptions.Add(RegionID.DarkWorldEast, new Mode());
                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode() { WorldState = WorldState.Inverted });

                    _itemSubscriptions.Add(ItemType.DarkWorldSouthAccess, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.Gloves, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case RegionID.DarkWorldEast:

                    GetAccessibility = (excludedRegions) =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.DarkWorldEastAccess))
                            return AccessibilityLevel.Normal;

                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            //  Access via Agahnim portal
                            if (_game.Items.Has(ItemType.Aga))
                                return AccessibilityLevel.Normal;

                            AccessibilityLevel darkWorldWitch = AccessibilityLevel.None;
                            AccessibilityLevel darkWorldSouth = AccessibilityLevel.None;
                            AccessibilityLevel darkWorldSouthEast = AccessibilityLevel.None;

                            if (_game.Items.Has(ItemType.MoonPearl))
                            {
                                //  Access via south of Eastern Palace portal
                                if (_game.Items.Has(ItemType.Gloves) && _game.Items.Has(ItemType.Hammer))
                                    return AccessibilityLevel.Normal;

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
                            AccessibilityLevel dWWitch = AccessibilityLevel.None;
                            AccessibilityLevel dWSouth = AccessibilityLevel.None;
                            AccessibilityLevel dWSouthEast = AccessibilityLevel.None;
                            AccessibilityLevel lightWorld = AccessibilityLevel.None;

                            //  Access via Dark World Witch area by hammer or gloves
                            if ((_game.Items.Has(ItemType.Gloves) || _game.Items.Has(ItemType.Hammer))
                                && !excludedRegions.Contains(RegionID.DarkWorldWitchArea))
                                dWWitch = _game.Regions[RegionID.DarkWorldWitchArea].GetAccessibility(newExcludedRegions);

                            //  Access via South Dark World by hammer, flippers, or fake flippers sequence break
                            if (!excludedRegions.Contains(RegionID.DarkWorldSouth))
                            {
                                if (_game.Items.Has(ItemType.Hammer) || _game.Items.Has(ItemType.Flippers))
                                    dWSouth = _game.Regions[RegionID.DarkWorldSouth].GetAccessibility(newExcludedRegions);
                                else
                                {
                                    dWSouth = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                        (byte)_game.Regions[RegionID.DarkWorldSouth].GetAccessibility(newExcludedRegions));
                                }
                            }

                            //  Access via Southeast Dark World by flippers or fake flippers sequence break
                            if (!excludedRegions.Contains(RegionID.DarkWorldSouthEast))
                            {
                                if (_game.Items.Has(ItemType.Flippers))
                                    dWSouthEast = _game.Regions[RegionID.DarkWorldSouthEast].GetAccessibility(newExcludedRegions);
                                else
                                {
                                    dWSouthEast = (AccessibilityLevel)Math.Min((byte)AccessibilityLevel.SequenceBreak,
                                        (byte)_game.Regions[RegionID.DarkWorldSouthEast].GetAccessibility(newExcludedRegions));
                                }
                            }

                            //  Access via Light World by mirror
                            if (_game.Items.Has(ItemType.Mirror) && !excludedRegions.Contains(RegionID.LightWorld))
                                lightWorld = _game.Regions[RegionID.LightWorld].GetAccessibility(newExcludedRegions);

                            return (AccessibilityLevel)Math.Max(Math.Max(Math.Max((byte)dWWitch, (byte)dWSouth),
                                (byte)dWSouthEast), (byte)lightWorld);
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DarkWorldWitchArea, new Mode());
                    _regionSubscriptions.Add(RegionID.DarkWorldSouth, new Mode());
                    _regionSubscriptions.Add(RegionID.DarkWorldSouthEast, new Mode());
                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode() { WorldState = WorldState.Inverted });

                    _itemSubscriptions.Add(ItemType.DarkWorldEastAccess, new Mode());
                    _itemSubscriptions.Add(ItemType.Aga, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Gloves, new Mode());
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.Flippers, new Mode());
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case RegionID.DarkWorldSouthEast:

                    GetAccessibility = (excludedRegions) =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.DarkWorldSouthEastAccess))
                            return AccessibilityLevel.Normal;

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

                    _regionSubscriptions.Add(RegionID.DarkWorldSouth, new Mode());
                    _regionSubscriptions.Add(RegionID.DarkWorldEast, new Mode());
                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode() { WorldState = WorldState.Inverted });

                    _itemSubscriptions.Add(ItemType.DarkWorldSouthEastAccess, new Mode());
                    _itemSubscriptions.Add(ItemType.Flippers, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case RegionID.DarkWorldWitchArea:

                    GetAccessibility = (excludedRegions) =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.DarkWorldWitchAreaAccess))
                            return AccessibilityLevel.Normal;

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

                            if (_game.Items.Has(ItemType.Mirror))
                            {
                                //  Access via Witch's Hut area by mirror
                                if (_game.Items.Has(ItemType.WitchsHutAccess))
                                    return AccessibilityLevel.Normal;

                                //  Access via Light World by mirror (requires moon pearl to reach mirror area)
                                if (_game.Items.Has(ItemType.MoonPearl) && !excludedRegions.Contains(RegionID.LightWorld))
                                    lightWorld = _game.Regions[RegionID.LightWorld].GetAccessibility(newExcludedRegions);
                            }

                            return (AccessibilityLevel)Math.Max(Math.Max((byte)darkWorldEast, (byte)darkWorldWest),
                                (byte)lightWorld);
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DarkWorldEast, new Mode());
                    _regionSubscriptions.Add(RegionID.DarkWorldWest, new Mode());
                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode() { WorldState = WorldState.Inverted });

                    _itemSubscriptions.Add(ItemType.DarkWorldWitchAreaAccess, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode());
                    _itemSubscriptions.Add(ItemType.Gloves, new Mode());
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.Flippers, new Mode());
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.WitchsHutAccess, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case RegionID.MireArea:

                    GetAccessibility = (excludedRegions) =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.MireAreaAccess))
                            return AccessibilityLevel.Normal;

                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            //  Access via Flute drop spot by Mitts
                            if (_game.Items.Has(ItemType.Flute) && _game.Items.Has(ItemType.Gloves, 2))
                                return AccessibilityLevel.Normal;
                        }

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

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode() { WorldState = WorldState.Inverted });

                    _itemSubscriptions.Add(ItemType.MireAreaAccess, new Mode());
                    _itemSubscriptions.Add(ItemType.Flute, new Mode());
                    _itemSubscriptions.Add(ItemType.Gloves, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case RegionID.DeathMountainWestBottom:

                    GetAccessibility = (excludedRegions) =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.DeathMountainWestBottomAccess))
                            return AccessibilityLevel.Normal;

                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            AccessibilityLevel direct = AccessibilityLevel.None;
                            AccessibilityLevel dMWestTop = AccessibilityLevel.None;
                            AccessibilityLevel dMEastBottom = AccessibilityLevel.None;
                            AccessibilityLevel dDMWestBottom = AccessibilityLevel.None;

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
                                direct = AccessibilityLevel.SequenceBreak;
                            }

                            //  Access via West Death Mountain top
                            if (!excludedRegions.Contains(RegionID.DeathMountainWestTop))
                                dMWestTop = _game.Regions[RegionID.DeathMountainWestTop].GetAccessibility(newExcludedRegions);

                            //  Access via East Death Mountain bottom by hookshot
                            if (_game.Items.Has(ItemType.Hookshot) && !excludedRegions.Contains(RegionID.DeathMountainEastBottom))
                                dMEastBottom = _game.Regions[RegionID.DeathMountainEastBottom].GetAccessibility(newExcludedRegions);

                            //  Access via West Dark Death Mountain bottom by mirror
                            if (_game.Items.Has(ItemType.Mirror) && !excludedRegions.Contains(RegionID.DarkDeathMountainWestBottom))
                                dDMWestBottom = _game.Regions[RegionID.DarkDeathMountainWestBottom].GetAccessibility(newExcludedRegions);

                            return (AccessibilityLevel)Math.Max(Math.Max(Math.Max((byte)direct, (byte)dMWestTop),
                                (byte)dMEastBottom), (byte)dDMWestBottom);
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

                    _regionSubscriptions.Add(RegionID.DeathMountainWestTop, new Mode());
                    _regionSubscriptions.Add(RegionID.DeathMountainEastBottom, new Mode());
                    _regionSubscriptions.Add(RegionID.DarkDeathMountainWestBottom, new Mode());

                    _itemSubscriptions.Add(ItemType.DeathMountainWestBottomAccess, new Mode());
                    _itemSubscriptions.Add(ItemType.Flute, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Gloves, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Lamp, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Hookshot, new Mode());
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case RegionID.DeathMountainWestTop:

                    GetAccessibility = (excludedRegions) =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.DeathMountainWestTopAccess))
                            return AccessibilityLevel.Normal;

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

                    _regionSubscriptions.Add(RegionID.DeathMountainEastTop, new Mode());
                    _regionSubscriptions.Add(RegionID.DarkDeathMountainWestBottom, new Mode() { WorldState = WorldState.StandardOpen });
                    _regionSubscriptions.Add(RegionID.DarkDeathMountainTop, new Mode() { WorldState = WorldState.StandardOpen });

                    _itemSubscriptions.Add(ItemType.DeathMountainWestTopAccess, new Mode());
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case RegionID.DeathMountainEastBottom:

                    GetAccessibility = (excludedRegions) =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.DeathMountainEastTopConnectorAccess) ||
                            _game.Items.Has(ItemType.DeathMountainEastBottomAccess) ||
                            _game.Items.Has(ItemType.SpiralCaveTopAccess))
                            return AccessibilityLevel.Normal;

                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            //  Access via Turtle Rock Tunnel by mirror
                            if (_game.Items.Has(ItemType.TurtleRockTunnelAccess) ||
                                _game.Items.Has(ItemType.TurtleRockSafetyDoorAccess) &&
                                _game.Items.Has(ItemType.Mirror))
                                return AccessibilityLevel.Normal;

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

                    _regionSubscriptions.Add(RegionID.DeathMountainEastTop, new Mode());
                    _regionSubscriptions.Add(RegionID.DeathMountainWestBottom, new Mode());
                    _regionSubscriptions.Add(RegionID.DarkDeathMountainEastBottom, new Mode());

                    _itemSubscriptions.Add(ItemType.DeathMountainEastTopConnectorAccess, new Mode());
                    _itemSubscriptions.Add(ItemType.DeathMountainEastBottomAccess, new Mode());
                    _itemSubscriptions.Add(ItemType.SpiralCaveTopAccess, new Mode());
                    _itemSubscriptions.Add(ItemType.TurtleRockTunnelAccess, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.TurtleRockSafetyDoorAccess, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Hookshot, new Mode());
                    _itemSubscriptions.Add(ItemType.Gloves, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case RegionID.DeathMountainEastTop:

                    GetAccessibility = (excludedRegions) =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.DeathMountainEastTopAccess))
                            return AccessibilityLevel.Normal;

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

                    _regionSubscriptions.Add(RegionID.DeathMountainWestTop, new Mode());
                    _regionSubscriptions.Add(RegionID.DarkDeathMountainTop, new Mode());
                    _regionSubscriptions.Add(RegionID.DeathMountainEastBottom, new Mode());

                    _itemSubscriptions.Add(ItemType.DeathMountainEastTopAccess, new Mode());
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.Gloves, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case RegionID.DarkDeathMountainTop:

                    GetAccessibility = (excludedRegions) =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.DarkDeathMountainFloatingIslandAccess) ||
                            _game.Items.Has(ItemType.DarkDeathMountainTopAccess))
                            return AccessibilityLevel.Normal;

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

                    _regionSubscriptions.Add(RegionID.DeathMountainEastTop, new Mode());
                    _regionSubscriptions.Add(RegionID.DarkDeathMountainEastBottom, new Mode());
                    _regionSubscriptions.Add(RegionID.DarkDeathMountainWestBottom, new Mode() { WorldState = WorldState.Inverted });
                    _regionSubscriptions.Add(RegionID.DeathMountainWestTop, new Mode() { WorldState = WorldState.Inverted });

                    _itemSubscriptions.Add(ItemType.DarkDeathMountainFloatingIslandAccess, new Mode());
                    _itemSubscriptions.Add(ItemType.DarkDeathMountainTopAccess, new Mode());
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Gloves, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case RegionID.DarkDeathMountainEastBottom:

                    GetAccessibility = (excludedRegions) =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.DarkDeathMountainEastBottomAccess))
                            return AccessibilityLevel.Normal;

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

                    _regionSubscriptions.Add(RegionID.DarkDeathMountainTop, new Mode());
                    _regionSubscriptions.Add(RegionID.DeathMountainEastBottom, new Mode());

                    _itemSubscriptions.Add(ItemType.DarkDeathMountainEastBottomAccess, new Mode());
                    _itemSubscriptions.Add(ItemType.Gloves, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case RegionID.DarkDeathMountainWestBottom:

                    GetAccessibility = (excludedRegions) =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.DarkDeathMountainWestBottomAccess))
                            return AccessibilityLevel.Normal;

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
                            AccessibilityLevel direct = AccessibilityLevel.None;
                            AccessibilityLevel lightWorld = AccessibilityLevel.None;
                            AccessibilityLevel dDMTop = AccessibilityLevel.None;
                            AccessibilityLevel dMWestBottom = AccessibilityLevel.None;

                            //  Access via Death Mountain Entry cave (non-entrance shuffle only)
                            if (!_game.Mode.EntranceShuffle.Value && _game.Items.Has(ItemType.Gloves))
                            {
                                //  Lamp required by logic
                                if (_game.Items.Has(ItemType.Lamp))
                                    return AccessibilityLevel.Normal;

                                //  Sequence break dark room
                                direct = AccessibilityLevel.SequenceBreak;
                            }

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

                            return (AccessibilityLevel)Math.Max(Math.Max(Math.Max((byte)direct, (byte)lightWorld),
                                (byte)dDMTop), (byte)dMWestBottom);
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DeathMountainWestBottom, new Mode());
                    _regionSubscriptions.Add(RegionID.DarkDeathMountainTop, new Mode());
                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode() { WorldState = WorldState.Inverted });

                    _itemSubscriptions.Add(ItemType.DarkDeathMountainWestBottomAccess, new Mode());
                    _itemSubscriptions.Add(ItemType.Gloves, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.Lamp, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.Flute, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case RegionID.TurtleRockTunnel:

                    GetAccessibility = (excludedRegions) =>
                    {
                        //  Access via exits in the region
                        if (_game.Items.Has(ItemType.TurtleRockTunnelAccess))
                            return AccessibilityLevel.Normal;

                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        // Inverted mode
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (_game.Items.Has(ItemType.Mirror))
                            {
                                //  Access via mirror from Spiral Cave top and Mimic Cave ledges
                                if (_game.Items.Has(ItemType.SpiralCaveTopAccess) ||
                                    _game.Items.Has(ItemType.MimicCaveAccess))
                                    return AccessibilityLevel.Normal;

                                //  Access via East Death Mountain top by mirror from Spiral Cave or Mimic Cave ledges
                                if (!excludedRegions.Contains(RegionID.DeathMountainEastTop))
                                    return _game.Regions[RegionID.DeathMountainEastTop].Accessibility;
                            }
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DeathMountainEastTop, new Mode() { WorldState = WorldState.Inverted });

                    _itemSubscriptions.Add(ItemType.TurtleRockTunnelAccess, new Mode());
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.SpiralCaveTopAccess, new Mode() { WorldState = WorldState.Inverted });
                    _itemSubscriptions.Add(ItemType.MimicCaveAccess, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case RegionID.HyruleCastle:

                    GetAccessibility = (excludedRegions) =>
                    {
                        //  Access to dungeons assumed available with entrance shuffle
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                            return AccessibilityLevel.Normal;

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

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode() { WorldState = WorldState.Inverted });

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case RegionID.Agahnim:

                    GetAccessibility = (excludedRegions) =>
                    {
                        //  Access to dungeons assumed available with entrance shuffle
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            //  Access via Light World entrance
                            if (_game.Items.CanClearAgaTowerBarrier())
                                return AccessibilityLevel.Normal;
                        }

                        //  Inverted mode
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            if (!excludedRegions.Contains(RegionID.DarkDeathMountainTop))
                                return _game.Regions[RegionID.DarkDeathMountainTop].Accessibility;
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DarkDeathMountainTop, new Mode()
                    {
                        WorldState = WorldState.Inverted,
                        EntranceShuffle = false
                    });

                    _itemSubscriptions.Add(ItemType.Cape, new Mode()
                    {
                        WorldState = WorldState.StandardOpen,
                        EntranceShuffle = false
                    });
                    _itemSubscriptions.Add(ItemType.Sword, new Mode()
                    {
                        WorldState = WorldState.StandardOpen,
                        EntranceShuffle = false
                    });
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode()
                    {
                        WorldState = WorldState.StandardOpen,
                        EntranceShuffle = false
                    });

                    break;
                case RegionID.EasternPalace:

                    GetAccessibility = (excludedRegions) =>
                    {
                        //  Access to dungeons assumed available with entrance shuffle
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                            return AccessibilityLevel.Normal;

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

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode()
                    {
                        WorldState = WorldState.Inverted,
                        EntranceShuffle = false
                    });

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode()
                    {
                        WorldState = WorldState.Inverted,
                        EntranceShuffle = false
                    });

                    break;
                case RegionID.DesertPalace:

                    GetAccessibility = (excludedRegions) =>
                    {
                        //  Access to dungeons assumed available with entrance shuffle
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            //  Access via Light World entrance by book
                            if (_game.Items.Has(ItemType.Book))
                                return AccessibilityLevel.Normal;

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

                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode()
                    {
                        WorldState = WorldState.Inverted,
                        EntranceShuffle = false
                    });
                    _regionSubscriptions.Add(RegionID.MireArea, new Mode()
                    {
                        WorldState = WorldState.StandardOpen,
                        EntranceShuffle = false
                    });

                    _itemSubscriptions.Add(ItemType.Book, new Mode());
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { WorldState = WorldState.StandardOpen });
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { WorldState = WorldState.Inverted });

                    break;
                case RegionID.TowerOfHera:

                    GetAccessibility = (excludedRegions) =>
                    {
                        //  Access to dungeons assumed available with entrance shuffle
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

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

                    _regionSubscriptions.Add(RegionID.DeathMountainWestTop, new Mode() { EntranceShuffle = false });

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode()
                    {
                        WorldState = WorldState.Inverted,
                        EntranceShuffle = false
                    });

                    break;
                case RegionID.PalaceOfDarkness:

                    GetAccessibility = (excludedRegions) =>
                    {
                        //  Access to dungeons assumed available with entrance shuffle
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

                        List<RegionID> newExcludedRegions = excludedRegions.GetRange(0, excludedRegions.Count);
                        newExcludedRegions.Add(ID);

                        //  Standard and Open modes
                        if (_game.Mode.WorldState == WorldState.StandardOpen)
                        {
                            //  Access via East Dark World entrance
                            if (_game.Items.Has(ItemType.MoonPearl) && !excludedRegions.Contains(RegionID.DarkWorldEast))
                                return _game.Regions[RegionID.DarkWorldEast].GetAccessibility(newExcludedRegions);
                        }

                        //  Inverted mode
                        if (_game.Mode.WorldState == WorldState.Inverted)
                        {
                            //  Access via East Dark World entrance
                            if (!excludedRegions.Contains(RegionID.DarkWorldEast))
                                return _game.Regions[RegionID.DarkWorldEast].GetAccessibility(newExcludedRegions);
                        }

                        //  Default to no access
                        return AccessibilityLevel.None;
                    };

                    _regionSubscriptions.Add(RegionID.DarkWorldEast, new Mode() { EntranceShuffle = false });

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode()
                    {
                        WorldState = WorldState.StandardOpen,
                        EntranceShuffle = false
                    });

                    break;
                case RegionID.SwampPalace:

                    GetAccessibility = (excludedRegions) =>
                    {
                        //  Access to dungeons assumed available with entrance shuffle
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

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

                    _regionSubscriptions.Add(RegionID.DarkWorldSouth, new Mode()
                    {
                        WorldState = WorldState.StandardOpen,
                        EntranceShuffle = false
                    });
                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode()
                    {
                        WorldState = WorldState.Inverted,
                        EntranceShuffle = false
                    });

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { EntranceShuffle = false });
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode() { EntranceShuffle = false });

                    break;
                case RegionID.SkullWoods:

                    GetAccessibility = (excludedRegions) =>
                    {
                        //  Access to dungeons assumed available with entrance shuffle
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

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

                    _regionSubscriptions.Add(RegionID.DarkWorldWest, new Mode() { EntranceShuffle = false });

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode()
                    {
                        WorldState = WorldState.StandardOpen,
                        EntranceShuffle = false
                    });

                    break;
                case RegionID.ThievesTown:

                    GetAccessibility = (excludedRegions) =>
                    {
                        //  Access to dungeons assumed available with entrance shuffle
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

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

                    _regionSubscriptions.Add(RegionID.DarkWorldWest, new Mode() { EntranceShuffle = false });

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode()
                    {
                        WorldState = WorldState.StandardOpen,
                        EntranceShuffle = false
                    });

                    break;
                case RegionID.IcePalace:

                    GetAccessibility = (excludedRegions) =>
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

                    _itemSubscriptions.Add(ItemType.Gloves, new Mode()
                    {
                        WorldState = WorldState.StandardOpen,
                        EntranceShuffle = false
                    });
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode()
                    {
                        WorldState = WorldState.StandardOpen,
                        EntranceShuffle = false
                    });
                    _itemSubscriptions.Add(ItemType.Flippers, new Mode() { EntranceShuffle = false });

                    break;
                case RegionID.MiseryMire:

                    GetAccessibility = (excludedRegions) =>
                    {
                        //  Access to dungeons assumed available with entrance shuffle
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

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

                    _regionSubscriptions.Add(RegionID.MireArea, new Mode() { EntranceShuffle = false });

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode()
                    {
                        WorldState = WorldState.StandardOpen,
                        EntranceShuffle = false
                    });
                    _itemSubscriptions.Add(ItemType.Sword, new Mode() { EntranceShuffle = false });
                    _itemSubscriptions.Add(ItemType.Bombos, new Mode() { EntranceShuffle = false });
                    _itemSubscriptions.Add(ItemType.BombosDungeons, new Mode() { EntranceShuffle = false });
                    _itemSubscriptions.Add(ItemType.Ether, new Mode() { EntranceShuffle = false });
                    _itemSubscriptions.Add(ItemType.EtherDungeons, new Mode() { EntranceShuffle = false });
                    _itemSubscriptions.Add(ItemType.Quake, new Mode() { EntranceShuffle = false });
                    _itemSubscriptions.Add(ItemType.QuakeDungeons, new Mode() { EntranceShuffle = false });

                    break;
                case RegionID.TurtleRock:

                    GetAccessibility = (excludedRegions) =>
                    {
                        //  Access to dungeons assumed available with entrance shuffle
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

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

                    _regionSubscriptions.Add(RegionID.DeathMountainEastTop, new Mode()
                    {
                        WorldState = WorldState.Inverted,
                        EntranceShuffle = false
                    });
                    _regionSubscriptions.Add(RegionID.DarkDeathMountainTop, new Mode() { EntranceShuffle = false });

                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode()
                    {
                        WorldState = WorldState.StandardOpen,
                        EntranceShuffle = false
                    });
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode()
                    {
                        WorldState = WorldState.StandardOpen,
                        EntranceShuffle = false
                    });
                    _itemSubscriptions.Add(ItemType.Sword, new Mode() { EntranceShuffle = false });
                    _itemSubscriptions.Add(ItemType.Bombos, new Mode() { EntranceShuffle = false });
                    _itemSubscriptions.Add(ItemType.BombosDungeons, new Mode() { EntranceShuffle = false });
                    _itemSubscriptions.Add(ItemType.Ether, new Mode() { EntranceShuffle = false });
                    _itemSubscriptions.Add(ItemType.EtherDungeons, new Mode() { EntranceShuffle = false });
                    _itemSubscriptions.Add(ItemType.Quake, new Mode() { EntranceShuffle = false });
                    _itemSubscriptions.Add(ItemType.QuakeDungeons, new Mode() { EntranceShuffle = false });
                    _itemSubscriptions.Add(ItemType.Mirror, new Mode()
                    {
                        WorldState = WorldState.Inverted,
                        EntranceShuffle = false
                    });

                    break;
                case RegionID.GanonsTower:

                    GetAccessibility = (excludedRegions) =>
                    {
                        //  Access to dungeons assumed available with entrance shuffle
                        if (_game.Mode.EntranceShuffle.Value)
                            return AccessibilityLevel.Normal;

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

                    _regionSubscriptions.Add(RegionID.DarkDeathMountainTop, new Mode()
                    {
                        WorldState = WorldState.StandardOpen,
                        EntranceShuffle = false
                    });
                    _regionSubscriptions.Add(RegionID.LightWorld, new Mode()
                    {
                        WorldState = WorldState.Inverted,
                        EntranceShuffle = false
                    });

                    _itemSubscriptions.Add(ItemType.TowerCrystals, new Mode() { EntranceShuffle = false });
                    _itemSubscriptions.Add(ItemType.Crystal, new Mode() { EntranceShuffle = false });
                    _itemSubscriptions.Add(ItemType.RedCrystal, new Mode() { EntranceShuffle = false });
                    _itemSubscriptions.Add(ItemType.MoonPearl, new Mode() { EntranceShuffle = false });

                    break;
            }

            _game.Mode.PropertyChanged += OnModeChanged;

            UpdateItemSubscriptions();
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateRegionSubscriptions();
            UpdateItemSubscriptions();

            if (e.PropertyName == nameof(_game.Mode.WorldState) ||
                e.PropertyName == nameof(_game.Mode.EntranceShuffle))
                UpdateAccessibility();
        }

        private void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateAccessibility();
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

        public void UpdateRegionSubscriptions()
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

        public void UpdateAccessibility()
        {
            Accessibility = GetAccessibility(new List<RegionID>());
        }
    }
}
