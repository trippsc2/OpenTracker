using OpenTracker.Models.Enums;
using System;
using System.ComponentModel;

namespace OpenTracker.Models
{
    public class Requirement : INotifyPropertyChanged
    {
        private readonly Game _game;
        private readonly RequirementType _type;
        private readonly bool _updateOnItemPlacementChanged;
        private readonly bool _updateOnWorldStateChanged;
        private readonly bool _updateOnEnemyShuffleChanged;

        private Func<AccessibilityLevel> GetAccessibility { get; }

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

        public Requirement(Game game, RequirementType type)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
            _type = type;

            switch (_type)
            {
                case RequirementType.None:
                    {
                        GetAccessibility = () => AccessibilityLevel.Normal;
                    }
                    break;
                case RequirementType.Aga:
                    {
                        _game.Items[ItemType.Aga].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.Has(ItemType.Aga))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.Aga2:
                    {
                        _game.Items[ItemType.Aga2].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.Has(ItemType.Aga2))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.ATBarrier:
                    {
                        _game.Items[ItemType.Cape].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Sword].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Hammer].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.CanClearAgaTowerBarrier())
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.BigBombToWall:
                    {
                        GetAccessibility = () => AccessibilityLevel.None;
                    }
                    break;
                case RequirementType.Book:
                    {
                        _game.Items[ItemType.Book].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.Has(ItemType.Book))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.Bottle:
                    {
                        _game.Items[ItemType.Bottle].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.Has(ItemType.Bottle))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.Bow:
                    {
                        _game.Items[ItemType.Bow].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.CanShootArrows())
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.BumperCave:
                    {
                        _updateOnItemPlacementChanged = true;
                        _game.Items[ItemType.Cape].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.MoonPearl].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Hookshot].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.Has(ItemType.Cape) && _game.Items.Has(ItemType.MoonPearl))
                            {
                                if (_game.Items.Has(ItemType.Hookshot) ||
                                    _game.Mode.ItemPlacement == ItemPlacement.Advanced)
                                    return AccessibilityLevel.Normal;

                                return AccessibilityLevel.SequenceBreak;
                            }

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.CaneOfSomaria:
                    {
                        _game.Items[ItemType.CaneOfSomaria].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.Has(ItemType.CaneOfSomaria))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.Curtains:
                    {
                        _game.Items[ItemType.Sword].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.Swordless() || _game.Items.Has(ItemType.Sword))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.DarkRoom:
                    {
                        _game.Items[ItemType.Lamp].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.Has(ItemType.Lamp))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.SequenceBreak;
                        };
                    }
                    break;
                case RequirementType.DarkRoomWithFireRod:
                    {
                        _game.Items[ItemType.Lamp].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.FireRod].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.Has(ItemType.Lamp))
                                return AccessibilityLevel.Normal;

                            if (_game.Mode.ItemPlacement == ItemPlacement.Advanced && _game.Items.Has(ItemType.FireRod))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.SequenceBreak;
                        };
                    }
                    break;
                case RequirementType.Dash:
                    {
                        _game.Items[ItemType.Boots].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.Has(ItemType.Boots))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.DashOrHookshot:
                    {
                        _game.Items[ItemType.Boots].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Hookshot].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.Has(ItemType.Hookshot))
                                return AccessibilityLevel.Normal;
                            
                            if (_game.Items.Has(ItemType.Boots))
                            {
                                if (_game.Mode.ItemPlacement == ItemPlacement.Advanced)
                                    return AccessibilityLevel.Normal;
                                
                                return AccessibilityLevel.SequenceBreak;
                            }

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.FireRod:
                    {
                        _game.Items[ItemType.FireRod].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.Has(ItemType.FireRod))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.FireSource:
                    {
                        _game.Items[ItemType.Lamp].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.FireRod].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.Has(ItemType.Lamp) || _game.Items.Has(ItemType.FireRod))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.GreenPendant:
                    {
                        _game.Items[ItemType.GreenPendant].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.Has(ItemType.GreenPendant))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.GTCrystals:
                    {
                        _game.Items[ItemType.TowerCrystals].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Crystal].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.RedCrystal].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.Has(ItemType.TowerCrystals))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.Hammer:
                    {
                        _game.Items[ItemType.Hammer].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.Has(ItemType.Hammer))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.Hookshot:
                    {
                        _game.Items[ItemType.Hookshot].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.Has(ItemType.Hookshot))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.IceBreaker:
                    {
                        _game.Items[ItemType.CaneOfSomaria].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.Has(ItemType.CaneOfSomaria))
                                return AccessibilityLevel.SequenceBreak;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.Lift1:
                    {
                        _game.Items[ItemType.Gloves].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.Has(ItemType.Gloves))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.MeltThings:
                    {
                        _game.Items[ItemType.FireRod].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Bombos].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Sword].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.CanMeltThings())
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.Mirror:
                    {
                        _game.Items[ItemType.Mirror].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.Has(ItemType.Mirror))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.MMMedallion:
                    {
                        _updateOnWorldStateChanged = true;
                        _game.Items[ItemType.MoonPearl].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Sword].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Bombos].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.BombosDungeons].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Ether].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.EtherDungeons].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Quake].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.QuakeDungeons].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if ((_game.Mode.WorldState == WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)) &&
                                _game.Items.CanUseMedallions() && _game.Items.HasMMMedallion())
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.Mushroom:
                    {
                        _game.Items[ItemType.Mushroom].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.Has(ItemType.Mushroom))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.Pendants:
                    {
                        _updateOnItemPlacementChanged = true;
                        _game.Items[ItemType.GreenPendant].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Pendant].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Book].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.Has(ItemType.GreenPendant) && _game.Items.Has(ItemType.Pendant, 2))
                            {
                                if (_game.Mode.ItemPlacement == ItemPlacement.Advanced || _game.Items.Has(ItemType.Book))
                                    return AccessibilityLevel.Normal;

                                return AccessibilityLevel.SequenceBreak;
                            }

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.RedCrystals:
                    {
                        _game.Items[ItemType.MoonPearl].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.RedCrystal].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.RedCrystal, 2))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.RedEyegoreGoriya:
                    {
                        _updateOnEnemyShuffleChanged = true;
                        _game.Items[ItemType.Bow].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.CanClearRedEyegoreGoriyaRooms())
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.SPEntry:
                    {
                        _game.Items[ItemType.MoonPearl].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Mirror].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Mirror))
                            {
                                if (_game.RequirementNodes.ContainsKey(RequirementNodeID.LightWorld))
                                    return _game.RequirementNodes[RequirementNodeID.LightWorld].Accessibility;
                            }

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.SwimNoFakeFlippers:
                    {
                        _game.Items[ItemType.Flippers].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.Has(ItemType.Flippers))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.Tablet:
                    {
                        _game.Items[ItemType.Book].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Sword].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.CanActivateTablets() && _game.Items.Has(ItemType.Book))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.Torch:
                    {
                        _game.Items[ItemType.Boots].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.Has(ItemType.Boots))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.SequenceBreak;
                        };
                    }
                    break;
                case RequirementType.TRMedallion:
                    {
                        _updateOnWorldStateChanged = true;
                        _game.Items[ItemType.MoonPearl].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Sword].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Bombos].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.BombosDungeons].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Ether].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.EtherDungeons].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Quake].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.QuakeDungeons].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if ((_game.Mode.WorldState == WorldState.Inverted || _game.Items.Has(ItemType.MoonPearl)) &&
                                _game.Items.CanUseMedallions() && _game.Items.HasTRMedallion())
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.LWNotBunny:
                    {
                        _updateOnWorldStateChanged = true;
                        _game.Items[ItemType.MoonPearl].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.NotBunnyInLightWorld())
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.LWDash:
                    {
                        _updateOnWorldStateChanged = true;
                        _game.Items[ItemType.MoonPearl].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Boots].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.NotBunnyInLightWorld() && _game.Items.Has(ItemType.Boots))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.LWDashAga:
                    {
                        _updateOnWorldStateChanged = true;
                        _game.Items[ItemType.MoonPearl].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Boots].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Aga].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.NotBunnyInLightWorld() && _game.Items.Has(ItemType.Boots) &&
                                _game.Items.Has(ItemType.Aga))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.LWFlute:
                    {
                        _updateOnWorldStateChanged = true;
                        _game.Items[ItemType.Flute].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Mode.WorldState != WorldState.Inverted && _game.Items.Has(ItemType.Flute))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.LWHammer:
                    {
                        _updateOnWorldStateChanged = true;
                        _game.Items[ItemType.MoonPearl].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Hammer].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.NotBunnyInLightWorld() && _game.Items.Has(ItemType.Hammer))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.LWHookshot:
                    {
                        _updateOnWorldStateChanged = true;
                        _game.Items[ItemType.MoonPearl].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Hookshot].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.NotBunnyInLightWorld() && _game.Items.Has(ItemType.Hookshot))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.LWLift1:
                    {
                        _updateOnWorldStateChanged = true;
                        _game.Items[ItemType.MoonPearl].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Gloves].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.NotBunnyInLightWorld() && _game.Items.Has(ItemType.Gloves))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.LWLift2:
                    {
                        _updateOnWorldStateChanged = true;
                        _game.Items[ItemType.MoonPearl].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Gloves].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.NotBunnyInLightWorld() && _game.Items.Has(ItemType.Gloves, 2))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.LWPowder:
                    {
                        _updateOnWorldStateChanged = true;
                        _game.Items[ItemType.MoonPearl].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Powder].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Mushroom].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.NotBunnyInLightWorld())
                            {
                                if (_game.Items.Has(ItemType.Powder))
                                    return AccessibilityLevel.Normal;

                                if (_game.Items.Has(ItemType.Mushroom))
                                    return AccessibilityLevel.SequenceBreak;
                            }

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.LWShovel:
                    {
                        _updateOnWorldStateChanged = true;
                        _game.Items[ItemType.MoonPearl].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Shovel].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.NotBunnyInLightWorld() && _game.Items.Has(ItemType.Shovel))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.LWSwim:
                    {
                        _updateOnWorldStateChanged = true;
                        _game.Items[ItemType.MoonPearl].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Flippers].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.NotBunnyInLightWorld())
                            {
                                if (_game.Items.Has(ItemType.Flippers))
                                    return AccessibilityLevel.Normal;

                                return AccessibilityLevel.SequenceBreak;
                            }

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.LWSwimOrWaterWalk:
                    {
                        _updateOnWorldStateChanged = true;
                        _game.Items[ItemType.MoonPearl].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Flippers].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Boots].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.NotBunnyInLightWorld())
                            {
                                if (_game.Items.Has(ItemType.Flippers))
                                    return AccessibilityLevel.Normal;

                                if (_game.Items.Has(ItemType.MoonPearl) || _game.Items.Has(ItemType.Boots))
                                    return AccessibilityLevel.SequenceBreak;
                            }

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.DWNotBunny:
                    {
                        _updateOnWorldStateChanged = true;
                        _game.Items[ItemType.MoonPearl].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.NotBunnyInDarkWorld())
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.DWDash:
                    {
                        _updateOnWorldStateChanged = true;
                        _game.Items[ItemType.MoonPearl].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Boots].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.NotBunnyInDarkWorld() && _game.Items.Has(ItemType.Boots))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.DWDashOrHookshot:
                    {
                        _updateOnWorldStateChanged = true;
                        _game.Items[ItemType.MoonPearl].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Boots].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Hookshot].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.NotBunnyInDarkWorld())
                            {
                                if (_game.Items.Has(ItemType.Hookshot))
                                    return AccessibilityLevel.Normal;

                                if (_game.Items.Has(ItemType.Boots))
                                {
                                    if (_game.Mode.ItemPlacement == ItemPlacement.Advanced)
                                        return AccessibilityLevel.Normal;

                                    return AccessibilityLevel.SequenceBreak;
                                }
                            }

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.DWFireRod:
                    {
                        _updateOnWorldStateChanged = true;
                        _game.Items[ItemType.MoonPearl].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.FireRod].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.NotBunnyInDarkWorld() && _game.Items.Has(ItemType.FireRod))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.DWFlute:
                    {
                        _updateOnWorldStateChanged = true;
                        _game.Items[ItemType.MoonPearl].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Flute].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.Has(ItemType.MoonPearl) && _game.Items.Has(ItemType.Flute))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.DWHammer:
                    {
                        _updateOnWorldStateChanged = true;
                        _game.Items[ItemType.MoonPearl].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Hammer].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.NotBunnyInDarkWorld() && _game.Items.Has(ItemType.Hammer))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.DWHookshot:
                    {
                        _updateOnWorldStateChanged = true;
                        _game.Items[ItemType.MoonPearl].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Hookshot].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.NotBunnyInDarkWorld() && _game.Items.Has(ItemType.Hookshot))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.DWLift1:
                    {
                        _updateOnWorldStateChanged = true;
                        _game.Items[ItemType.MoonPearl].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Gloves].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.NotBunnyInDarkWorld() && _game.Items.Has(ItemType.Gloves))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.DWLift2:
                    {
                        _updateOnWorldStateChanged = true;
                        _game.Items[ItemType.MoonPearl].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Gloves].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.NotBunnyInDarkWorld() && _game.Items.Has(ItemType.Gloves, 2))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.DWSpikeCave:
                    {
                        _updateOnItemPlacementChanged = true;
                        _game.Items[ItemType.MoonPearl].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Hammer].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Gloves].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.CaneOfByrna].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Cape].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.HalfMagic].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Bottle].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.NotBunnyInDarkWorld() && _game.Items.Has(ItemType.Hammer) &&
                                _game.Items.Has(ItemType.Hammer))
                            {
                                if (_game.Items.Has(ItemType.CaneOfByrna) || (_game.Items.Has(ItemType.Cape) &&
                                    _game.Items.CanExtendMagic()))
                                    return AccessibilityLevel.Normal;

                                return AccessibilityLevel.SequenceBreak;
                            }

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.DWSwim:
                    {
                        _updateOnWorldStateChanged = true;
                        _game.Items[ItemType.MoonPearl].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Flippers].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            if (_game.Items.NotBunnyInDarkWorld())
                            {
                                if (_game.Items.Has(ItemType.Flippers))
                                    return AccessibilityLevel.Normal;

                                return AccessibilityLevel.SequenceBreak;
                            }

                            return AccessibilityLevel.None;
                        };
                    }
                    break;
                case RequirementType.ATBoss:
                    {
                        _game.BossPlacements[BossPlacementID.ATBoss].PropertyChanged +=
                            OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            return _game.BossPlacements[BossPlacementID.ATBoss].Accessibility;
                        };
                    }
                    break;
                case RequirementType.EPBoss:
                    {
                        _game.BossPlacements[BossPlacementID.EPBoss].PropertyChanged +=
                            OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            return _game.BossPlacements[BossPlacementID.EPBoss].Accessibility;
                        };
                    }
                    break;
                case RequirementType.DPBoss:
                    {
                        _game.BossPlacements[BossPlacementID.DPBoss].PropertyChanged +=
                            OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            return _game.BossPlacements[BossPlacementID.DPBoss].Accessibility;
                        };
                    }
                    break;
                case RequirementType.ToHBoss:
                    {
                        _game.BossPlacements[BossPlacementID.ToHBoss].PropertyChanged +=
                            OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            return _game.BossPlacements[BossPlacementID.ToHBoss].Accessibility;
                        };
                    }
                    break;
                case RequirementType.PoDBoss:
                    {
                        _game.BossPlacements[BossPlacementID.PoDBoss].PropertyChanged +=
                            OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            return _game.BossPlacements[BossPlacementID.PoDBoss].Accessibility;
                        };
                    }
                    break;
                case RequirementType.SPBoss:
                    {
                        _game.BossPlacements[BossPlacementID.SPBoss].PropertyChanged +=
                            OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            return _game.BossPlacements[BossPlacementID.SPBoss].Accessibility;
                        };
                    }
                    break;
                case RequirementType.SWBoss:
                    {
                        _game.BossPlacements[BossPlacementID.SWBoss].PropertyChanged +=
                            OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            return _game.BossPlacements[BossPlacementID.SWBoss].Accessibility;
                        };
                    }
                    break;
                case RequirementType.TTBoss:
                    {
                        _game.BossPlacements[BossPlacementID.TTBoss].PropertyChanged +=
                            OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            return _game.BossPlacements[BossPlacementID.TTBoss].Accessibility;
                        };
                    }
                    break;
                case RequirementType.IPBoss:
                    {
                        _game.BossPlacements[BossPlacementID.IPBoss].PropertyChanged +=
                            OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            return _game.BossPlacements[BossPlacementID.IPBoss].Accessibility;
                        };
                    }
                    break;
                case RequirementType.MMBoss:
                    {
                        _game.BossPlacements[BossPlacementID.MMBoss].PropertyChanged +=
                            OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            return _game.BossPlacements[BossPlacementID.MMBoss].Accessibility;
                        };
                    }
                    break;
                case RequirementType.TRBoss:
                    {
                        _game.BossPlacements[BossPlacementID.TRBoss].PropertyChanged +=
                            OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            return _game.BossPlacements[BossPlacementID.TRBoss].Accessibility;
                        };
                    }
                    break;
                case RequirementType.GTBoss1:
                    {
                        _game.BossPlacements[BossPlacementID.GTBoss1].PropertyChanged +=
                            OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            return _game.BossPlacements[BossPlacementID.GTBoss1].Accessibility;
                        };
                    }
                    break;
                case RequirementType.GTBoss2:
                    {
                        _game.BossPlacements[BossPlacementID.GTBoss2].PropertyChanged +=
                            OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            return _game.BossPlacements[BossPlacementID.GTBoss2].Accessibility;
                        };
                    }
                    break;
                case RequirementType.GTBoss3:
                    {
                        _game.BossPlacements[BossPlacementID.GTBoss3].PropertyChanged +=
                            OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            return _game.BossPlacements[BossPlacementID.GTBoss3].Accessibility;
                        };
                    }
                    break;
                case RequirementType.GTFinalBoss:
                    {
                        _game.BossPlacements[BossPlacementID.GTFinalBoss].PropertyChanged += OnRequirementChanged;

                        GetAccessibility = () =>
                        {
                            return _game.BossPlacements[BossPlacementID.GTFinalBoss].Accessibility;
                        };
                    }
                    break;
            }

            if (GetAccessibility == null)
                GetAccessibility = () => AccessibilityLevel.None;

            _game.Mode.PropertyChanged += OnModeChanged;

            UpdateAccessibility();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if ((e.PropertyName == nameof(Mode.ItemPlacement) &&
                _updateOnItemPlacementChanged) ||
                (e.PropertyName == nameof(Mode.WorldState) &&
                _updateOnWorldStateChanged) ||
                (e.PropertyName == nameof(Mode.EnemyShuffle) &&
                _updateOnEnemyShuffleChanged))
                UpdateAccessibility();
        }

        private void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateAccessibility();
        }

        private void UpdateAccessibility()
        {
            Accessibility = GetAccessibility();
        }

        public void Initialize()
        {
            switch (_type)
            {
                case RequirementType.SPEntry:
                    {
                        _game.RequirementNodes[RequirementNodeID.LightWorld].PropertyChanged +=
                            OnRequirementChanged;
                    }
                    break;
            }
        }
    }
}
