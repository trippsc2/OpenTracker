using OpenTracker.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models
{
    public class Boss : INotifyPropertyChanged
    {
        private readonly Game _game;
        private readonly bool _updateOnItemPlacementChange;

        public BossType Type { get; }
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

        public Boss(Game game, BossType type)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
            Type = type;

            switch (Type)
            {
                case BossType.Armos:
                    {
                        GetAccessibility = () =>
                        {
                            if (_game.Items.Has(ItemType.Sword) || _game.Items.Has(ItemType.Hammer) ||
                                _game.Items.Has(ItemType.Bow) || _game.Items.Has(ItemType.Boomerang) ||
                                _game.Items.Has(ItemType.RedBoomerang) || (_game.Items.CanExtendMagic(4) &&
                                (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.IceRod))) ||
                                (_game.Items.CanExtendMagic() &&
                                (_game.Items.Has(ItemType.CaneOfByrna) || _game.Items.Has(ItemType.CaneOfSomaria))))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };

                        _game.Items[ItemType.Sword].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Hammer].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Bow].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Boomerang].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.RedBoomerang].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Bottle].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.HalfMagic].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.FireRod].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.IceRod].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.CaneOfByrna].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.CaneOfSomaria].PropertyChanged += OnRequirementChanged;
                    }
                    break;
                case BossType.Lanmolas:
                    {
                        GetAccessibility = () =>
                        {
                            if (_game.Items.Has(ItemType.Sword) || _game.Items.Has(ItemType.Hammer) ||
                                _game.Items.Has(ItemType.Bow) || _game.Items.Has(ItemType.FireRod) ||
                                _game.Items.Has(ItemType.IceRod) || _game.Items.Has(ItemType.CaneOfByrna) ||
                                _game.Items.Has(ItemType.CaneOfSomaria))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.SequenceBreak;
                        };

                        _game.Items[ItemType.Sword].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Hammer].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Bow].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.FireRod].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.IceRod].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.CaneOfByrna].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.CaneOfSomaria].PropertyChanged += OnRequirementChanged;
                    }
                    break;
                case BossType.Moldorm:
                    {
                        GetAccessibility = () =>
                        {
                            if (_game.Items.Has(ItemType.Sword) || _game.Items.Has(ItemType.Hammer))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };

                        _game.Items[ItemType.Sword].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Hammer].PropertyChanged += OnRequirementChanged;
                    }
                    break;
                case BossType.HelmasaurKing:
                    {
                        GetAccessibility = () => { return AccessibilityLevel.Normal; };
                    }
                    break;
                case BossType.Arrghus:
                    {
                        GetAccessibility = () =>
                        {
                            if (_game.Items.Has(ItemType.Hookshot) && (_game.Items.Has(ItemType.Hammer) ||
                                _game.Items.Has(ItemType.Sword) ||
                                ((_game.Items.CanExtendMagic(2) || _game.Items.Has(ItemType.Bow)) &&
                                (_game.Items.Has(ItemType.FireRod) || _game.Items.Has(ItemType.IceRod)))))
                            {
                                if (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                    _game.Items.Swordless() || _game.Items.Has(ItemType.Sword, 2))
                                    return AccessibilityLevel.Normal;

                                return AccessibilityLevel.SequenceBreak;
                            }

                            return AccessibilityLevel.None;
                        };

                        _updateOnItemPlacementChange = true;

                        _game.Items[ItemType.Hookshot].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Hammer].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Sword].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Bottle].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.HalfMagic].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Bow].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.FireRod].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.IceRod].PropertyChanged += OnRequirementChanged;
                    }
                    break;
                case BossType.Mothula:
                    {
                        GetAccessibility = () =>
                        {
                            if (_game.Items.Has(ItemType.Sword) || _game.Items.Has(ItemType.Hammer) ||
                                (_game.Items.CanExtendMagic(2) && (_game.Items.Has(ItemType.FireRod) ||
                                _game.Items.Has(ItemType.CaneOfSomaria) || _game.Items.Has(ItemType.CaneOfByrna))))
                            {
                                if (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                    _game.Items.Has(ItemType.Sword, 2) || (_game.Items.CanExtendMagic(2) &&
                                    _game.Items.Has(ItemType.FireRod)))
                                    return AccessibilityLevel.Normal;

                                return AccessibilityLevel.SequenceBreak;
                            }

                            return AccessibilityLevel.None;
                        };

                        _updateOnItemPlacementChange = true;

                        _game.Items[ItemType.Sword].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Hammer].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Bottle].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.HalfMagic].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.FireRod].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.CaneOfSomaria].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.CaneOfByrna].PropertyChanged += OnRequirementChanged;
                    }
                    break;
                case BossType.Blind:
                    {
                        GetAccessibility = () =>
                        {
                            if (_game.Items.Has(ItemType.Sword) || _game.Items.Has(ItemType.Hammer) ||
                                _game.Items.Has(ItemType.CaneOfSomaria) || _game.Items.Has(ItemType.CaneOfByrna))
                            {
                                if (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                    _game.Items.Swordless() || (_game.Items.Has(ItemType.Sword) &&
                                    (_game.Items.Has(ItemType.Cape) || _game.Items.Has(ItemType.CaneOfByrna))))
                                    return AccessibilityLevel.Normal;

                                return AccessibilityLevel.SequenceBreak;
                            }

                            return AccessibilityLevel.None;
                        };

                        _updateOnItemPlacementChange = true;

                        _game.Items[ItemType.Sword].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Hammer].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.CaneOfSomaria].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.CaneOfByrna].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Cape].PropertyChanged += OnRequirementChanged;
                    }
                    break;
                case BossType.Kholdstare:
                    {
                        GetAccessibility = () =>
                        {
                            if (_game.Items.CanMeltThings() && (_game.Items.Has(ItemType.Hammer) ||
                                _game.Items.Has(ItemType.Sword) ||
                                (_game.Items.CanExtendMagic(3) && _game.Items.Has(ItemType.FireRod))
                                || (_game.Items.CanExtendMagic(2) && _game.Items.Has(ItemType.FireRod)
                                && _game.Items.Has(ItemType.Bombos) && _game.Items.Swordless())))
                            {
                                if (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                    _game.Items.Has(ItemType.Sword, 2) ||
                                    (_game.Items.CanExtendMagic(3) && _game.Items.Has(ItemType.FireRod)) ||
                                    (_game.Items.Has(ItemType.Bombos) &&
                                    (_game.Items.Swordless() || _game.Items.Has(ItemType.Sword)) &&
                                    _game.Items.CanExtendMagic(2) && _game.Items.Has(ItemType.FireRod)))
                                    return AccessibilityLevel.Normal;

                                return AccessibilityLevel.SequenceBreak;
                            }

                            return AccessibilityLevel.None;
                        };

                        _updateOnItemPlacementChange = true;

                        _game.Items[ItemType.FireRod].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Bombos].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Sword].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Hammer].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Bottle].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.HalfMagic].PropertyChanged += OnRequirementChanged;
                    }
                    break;
                case BossType.Vitreous:
                    {
                        GetAccessibility = () =>
                        {
                            if (_game.Items.Has(ItemType.Hammer) || _game.Items.Has(ItemType.Sword) ||
                                _game.Items.Has(ItemType.Bow))
                            {
                                if (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                    _game.Items.Has(ItemType.Sword, 2) || _game.Items.Has(ItemType.Bow))
                                    return AccessibilityLevel.Normal;

                                return AccessibilityLevel.SequenceBreak;
                            }

                            return AccessibilityLevel.None;
                        };

                        _updateOnItemPlacementChange = true;

                        _game.Items[ItemType.Hammer].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Sword].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Bow].PropertyChanged += OnRequirementChanged;
                    }
                    break;
                case BossType.Trinexx:
                    {
                        GetAccessibility = () =>
                        {
                            if (_game.Items.Has(ItemType.FireRod) && _game.Items.Has(ItemType.IceRod) &&
                                (_game.Items.Has(ItemType.Sword, 3) || _game.Items.Has(ItemType.Hammer) ||
                                (_game.Items.CanExtendMagic(2) && _game.Items.Has(ItemType.Sword, 2)) ||
                                (_game.Items.CanExtendMagic(4) && _game.Items.Has(ItemType.Sword))))
                            {
                                if (_game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                    _game.Items.Swordless() || _game.Items.Has(ItemType.Sword, 3) ||
                                    (_game.Items.CanExtendMagic(2) && _game.Items.Has(ItemType.Sword, 2)))
                                    return AccessibilityLevel.Normal;

                                return AccessibilityLevel.SequenceBreak;
                            }

                            return AccessibilityLevel.None;
                        };

                        _updateOnItemPlacementChange = true;

                        _game.Items[ItemType.FireRod].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.IceRod].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Sword].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Hammer].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Bottle].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.HalfMagic].PropertyChanged += OnRequirementChanged;
                    }
                    break;
                case BossType.Aga:
                    {
                        GetAccessibility = () =>
                        {
                            if (_game.Items.Has(ItemType.Sword) || _game.Items.Has(ItemType.Hammer) ||
                                _game.Items.Has(ItemType.Net))
                                return AccessibilityLevel.Normal;

                            return AccessibilityLevel.None;
                        };

                        _game.Items[ItemType.Sword].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Hammer].PropertyChanged += OnRequirementChanged;
                        _game.Items[ItemType.Net].PropertyChanged += OnRequirementChanged;
                    }
                    break;
            }

            _game.Mode.PropertyChanged += OnModeChanged;

            UpdateAccessibility();
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ItemPlacement" && _updateOnItemPlacementChange)
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
    }
}
