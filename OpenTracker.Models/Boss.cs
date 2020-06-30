using OpenTracker.Models.Enums;
using System;
using System.ComponentModel;

namespace OpenTracker.Models
{
    /// <summary>
    /// This is the data class for boss data
    /// </summary>
    public class Boss : INotifyPropertyChanged
    {
        private readonly bool _updateOnItemPlacementChange;

        private Func<AccessibilityLevel> GetAccessibility { get; }

        public BossType Type { get; }

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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">The parent class.</param>
        /// <param name="type">The type of boss.</param>
        public Boss(Game game, BossType type)
        {
            if (game == null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            Type = type;

            switch (Type)
            {
                case BossType.Armos:
                    {
                        GetAccessibility = () =>
                        {
                            if (game.Items.Has(ItemType.Sword) || game.Items.Has(ItemType.Hammer) ||
                                game.Items.Has(ItemType.Bow) || game.Items.Has(ItemType.Boomerang) ||
                                game.Items.Has(ItemType.RedBoomerang) || (game.Items.CanExtendMagic(4) &&
                                (game.Items.Has(ItemType.FireRod) || game.Items.Has(ItemType.IceRod))) ||
                                (game.Items.CanExtendMagic() &&
                                (game.Items.Has(ItemType.CaneOfByrna) || game.Items.Has(ItemType.CaneOfSomaria))))
                            {
                                return AccessibilityLevel.Normal;
                            }

                            return AccessibilityLevel.None;
                        };

                        game.Items[ItemType.Sword].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.Hammer].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.Bow].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.Boomerang].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.RedBoomerang].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.Bottle].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.HalfMagic].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.FireRod].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.IceRod].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.CaneOfByrna].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.CaneOfSomaria].PropertyChanged += OnRequirementChanged;
                    }
                    break;
                case BossType.Lanmolas:
                    {
                        GetAccessibility = () =>
                        {
                            if (game.Items.Has(ItemType.Sword) || game.Items.Has(ItemType.Hammer) ||
                                game.Items.Has(ItemType.Bow) || game.Items.Has(ItemType.FireRod) ||
                                game.Items.Has(ItemType.IceRod) || game.Items.Has(ItemType.CaneOfByrna) ||
                                game.Items.Has(ItemType.CaneOfSomaria))
                            {
                                return AccessibilityLevel.Normal;
                            }

                            return AccessibilityLevel.SequenceBreak;
                        };

                        game.Items[ItemType.Sword].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.Hammer].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.Bow].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.FireRod].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.IceRod].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.CaneOfByrna].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.CaneOfSomaria].PropertyChanged += OnRequirementChanged;
                    }
                    break;
                case BossType.Moldorm:
                    {
                        GetAccessibility = () =>
                        {
                            if (game.Items.Has(ItemType.Sword) || game.Items.Has(ItemType.Hammer))
                            {
                                return AccessibilityLevel.Normal;
                            }

                            return AccessibilityLevel.None;
                        };

                        game.Items[ItemType.Sword].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.Hammer].PropertyChanged += OnRequirementChanged;
                    }
                    break;
                case BossType.HelmasaurKing:
                    {
                        GetAccessibility = () => AccessibilityLevel.Normal;
                    }
                    break;
                case BossType.Arrghus:
                    {
                        GetAccessibility = () =>
                        {
                            if (game.Items.Has(ItemType.Hookshot) && (game.Items.Has(ItemType.Hammer) ||
                                game.Items.Has(ItemType.Sword) ||
                                ((game.Items.CanExtendMagic(2) || game.Items.Has(ItemType.Bow)) &&
                                (game.Items.Has(ItemType.FireRod) || game.Items.Has(ItemType.IceRod)))))
                            {
                                if (game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                    game.Items.Swordless() || game.Items.Has(ItemType.Sword, 2))
                                {
                                    return AccessibilityLevel.Normal;
                                }

                                return AccessibilityLevel.SequenceBreak;
                            }

                            return AccessibilityLevel.None;
                        };

                        _updateOnItemPlacementChange = true;

                        game.Items[ItemType.Hookshot].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.Hammer].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.Sword].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.Bottle].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.HalfMagic].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.Bow].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.FireRod].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.IceRod].PropertyChanged += OnRequirementChanged;
                    }
                    break;
                case BossType.Mothula:
                    {
                        GetAccessibility = () =>
                        {
                            if (game.Items.Has(ItemType.Sword) || game.Items.Has(ItemType.Hammer) ||
                                (game.Items.CanExtendMagic(2) && (game.Items.Has(ItemType.FireRod) ||
                                game.Items.Has(ItemType.CaneOfSomaria) || game.Items.Has(ItemType.CaneOfByrna))))
                            {
                                if (game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                    game.Items.Has(ItemType.Sword, 2) || (game.Items.CanExtendMagic(2) &&
                                    game.Items.Has(ItemType.FireRod)))
                                {
                                    return AccessibilityLevel.Normal;
                                }

                                return AccessibilityLevel.SequenceBreak;
                            }

                            return AccessibilityLevel.None;
                        };

                        _updateOnItemPlacementChange = true;

                        game.Items[ItemType.Sword].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.Hammer].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.Bottle].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.HalfMagic].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.FireRod].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.CaneOfSomaria].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.CaneOfByrna].PropertyChanged += OnRequirementChanged;
                    }
                    break;
                case BossType.Blind:
                    {
                        GetAccessibility = () =>
                        {
                            if (game.Items.Has(ItemType.Sword) || game.Items.Has(ItemType.Hammer) ||
                                game.Items.Has(ItemType.CaneOfSomaria) || game.Items.Has(ItemType.CaneOfByrna))
                            {
                                if (game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                    game.Items.Swordless() || (game.Items.Has(ItemType.Sword) &&
                                    (game.Items.Has(ItemType.Cape) || game.Items.Has(ItemType.CaneOfByrna))))
                                {
                                    return AccessibilityLevel.Normal;
                                }

                                return AccessibilityLevel.SequenceBreak;
                            }

                            return AccessibilityLevel.None;
                        };

                        _updateOnItemPlacementChange = true;

                        game.Items[ItemType.Sword].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.Hammer].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.CaneOfSomaria].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.CaneOfByrna].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.Cape].PropertyChanged += OnRequirementChanged;
                    }
                    break;
                case BossType.Kholdstare:
                    {
                        GetAccessibility = () =>
                        {
                            if (game.Items.CanMeltThings() && (game.Items.Has(ItemType.Hammer) ||
                                game.Items.Has(ItemType.Sword) ||
                                (game.Items.CanExtendMagic(3) && game.Items.Has(ItemType.FireRod))
                                || (game.Items.CanExtendMagic(2) && game.Items.Has(ItemType.FireRod)
                                && game.Items.Has(ItemType.Bombos) && game.Items.Swordless())))
                            {
                                if (game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                    game.Items.Has(ItemType.Sword, 2) ||
                                    (game.Items.CanExtendMagic(3) && game.Items.Has(ItemType.FireRod)) ||
                                    (game.Items.Has(ItemType.Bombos) &&
                                    (game.Items.Swordless() || game.Items.Has(ItemType.Sword)) &&
                                    game.Items.CanExtendMagic(2) && game.Items.Has(ItemType.FireRod)))
                                {
                                    return AccessibilityLevel.Normal;
                                }

                                return AccessibilityLevel.SequenceBreak;
                            }

                            return AccessibilityLevel.None;
                        };

                        _updateOnItemPlacementChange = true;

                        game.Items[ItemType.FireRod].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.Bombos].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.Sword].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.Hammer].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.Bottle].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.HalfMagic].PropertyChanged += OnRequirementChanged;
                    }
                    break;
                case BossType.Vitreous:
                    {
                        GetAccessibility = () =>
                        {
                            if (game.Items.Has(ItemType.Hammer) || game.Items.Has(ItemType.Sword) ||
                                game.Items.Has(ItemType.Bow))
                            {
                                if (game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                    game.Items.Has(ItemType.Sword, 2) || game.Items.Has(ItemType.Bow))
                                {
                                    return AccessibilityLevel.Normal;
                                }

                                return AccessibilityLevel.SequenceBreak;
                            }

                            return AccessibilityLevel.None;
                        };

                        _updateOnItemPlacementChange = true;

                        game.Items[ItemType.Hammer].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.Sword].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.Bow].PropertyChanged += OnRequirementChanged;
                    }
                    break;
                case BossType.Trinexx:
                    {
                        GetAccessibility = () =>
                        {
                            if (game.Items.Has(ItemType.FireRod) && game.Items.Has(ItemType.IceRod) &&
                                (game.Items.Has(ItemType.Sword, 3) || game.Items.Has(ItemType.Hammer) ||
                                (game.Items.CanExtendMagic(2) && game.Items.Has(ItemType.Sword, 2)) ||
                                (game.Items.CanExtendMagic(4) && game.Items.Has(ItemType.Sword))))
                            {
                                if (game.Mode.ItemPlacement == ItemPlacement.Advanced ||
                                    game.Items.Swordless() || game.Items.Has(ItemType.Sword, 3) ||
                                    (game.Items.CanExtendMagic(2) && game.Items.Has(ItemType.Sword, 2)))
                                {
                                    return AccessibilityLevel.Normal;
                                }

                                return AccessibilityLevel.SequenceBreak;
                            }

                            return AccessibilityLevel.None;
                        };

                        _updateOnItemPlacementChange = true;

                        game.Items[ItemType.FireRod].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.IceRod].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.Sword].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.Hammer].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.Bottle].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.HalfMagic].PropertyChanged += OnRequirementChanged;
                    }
                    break;
                case BossType.Aga:
                    {
                        GetAccessibility = () =>
                        {
                            if (game.Items.Has(ItemType.Sword) || game.Items.Has(ItemType.Hammer) ||
                                game.Items.Has(ItemType.Net))
                            {
                                return AccessibilityLevel.Normal;
                            }

                            return AccessibilityLevel.None;
                        };

                        game.Items[ItemType.Sword].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.Hammer].PropertyChanged += OnRequirementChanged;
                        game.Items[ItemType.Net].PropertyChanged += OnRequirementChanged;
                    }
                    break;
            }

            game.Mode.PropertyChanged += OnModeChanged;

            UpdateAccessibility();
        }

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The string of the property name of the changed property.
        /// </param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the Mode class
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Mode.ItemPlacement) && _updateOnItemPlacementChange)
            {
                UpdateAccessibility();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the Item class for
        /// the boss requirements.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateAccessibility();
        }

        /// <summary>
        /// Updates the accessibility of this boss.
        /// </summary>
        private void UpdateAccessibility()
        {
            Accessibility = GetAccessibility();
        }
    }
}
