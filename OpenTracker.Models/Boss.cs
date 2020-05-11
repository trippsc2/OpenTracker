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
        private readonly Dictionary<ItemType, Mode> _itemSubscriptions;
        private readonly Dictionary<ItemType, bool> _itemIsSubscribed;

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

            _itemSubscriptions = new Dictionary<ItemType, Mode>();
            _itemIsSubscribed = new Dictionary<ItemType, bool>();

            Type = type;

            switch (Type)
            {
                case BossType.Armos:

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

                    _itemSubscriptions.Add(ItemType.Sword, new Mode());
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.Bow, new Mode());
                    _itemSubscriptions.Add(ItemType.Boomerang, new Mode());
                    _itemSubscriptions.Add(ItemType.RedBoomerang, new Mode());
                    _itemSubscriptions.Add(ItemType.Bottle, new Mode());
                    _itemSubscriptions.Add(ItemType.HalfMagic, new Mode());
                    _itemSubscriptions.Add(ItemType.FireRod, new Mode());
                    _itemSubscriptions.Add(ItemType.IceRod, new Mode());
                    _itemSubscriptions.Add(ItemType.CaneOfByrna, new Mode());
                    _itemSubscriptions.Add(ItemType.CaneOfSomaria, new Mode());

                    break;
                case BossType.Lanmolas:

                    GetAccessibility = () =>
                    {
                        if (_game.Items.Has(ItemType.Sword) || _game.Items.Has(ItemType.Hammer) ||
                            _game.Items.Has(ItemType.Bow) || _game.Items.Has(ItemType.FireRod) ||
                            _game.Items.Has(ItemType.IceRod) || _game.Items.Has(ItemType.CaneOfByrna) ||
                            _game.Items.Has(ItemType.CaneOfSomaria))
                            return AccessibilityLevel.Normal;

                        return AccessibilityLevel.SequenceBreak;
                    };

                    _itemSubscriptions.Add(ItemType.Sword, new Mode());
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.Bow, new Mode());
                    _itemSubscriptions.Add(ItemType.FireRod, new Mode());
                    _itemSubscriptions.Add(ItemType.IceRod, new Mode());
                    _itemSubscriptions.Add(ItemType.CaneOfByrna, new Mode());
                    _itemSubscriptions.Add(ItemType.CaneOfSomaria, new Mode());

                    break;
                case BossType.Moldorm:

                    GetAccessibility = () =>
                    {
                        if (_game.Items.Has(ItemType.Sword) || _game.Items.Has(ItemType.Hammer))
                            return AccessibilityLevel.Normal;

                        return AccessibilityLevel.None;
                    };

                    _itemSubscriptions.Add(ItemType.Sword, new Mode());
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());

                    break;
                case BossType.HelmasaurKing:

                    GetAccessibility = () => { return AccessibilityLevel.Normal; };

                    break;
                case BossType.Arrghus:

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

                    _itemSubscriptions.Add(ItemType.Hookshot, new Mode());
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.Sword, new Mode());
                    _itemSubscriptions.Add(ItemType.Bottle, new Mode());
                    _itemSubscriptions.Add(ItemType.HalfMagic, new Mode());
                    _itemSubscriptions.Add(ItemType.Bow, new Mode());
                    _itemSubscriptions.Add(ItemType.FireRod, new Mode());
                    _itemSubscriptions.Add(ItemType.IceRod, new Mode());

                    break;
                case BossType.Mothula:

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

                    _itemSubscriptions.Add(ItemType.Sword, new Mode());
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.Bottle, new Mode());
                    _itemSubscriptions.Add(ItemType.HalfMagic, new Mode());
                    _itemSubscriptions.Add(ItemType.FireRod, new Mode());
                    _itemSubscriptions.Add(ItemType.CaneOfSomaria, new Mode());
                    _itemSubscriptions.Add(ItemType.CaneOfByrna, new Mode());

                    break;
                case BossType.Blind:

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

                    _itemSubscriptions.Add(ItemType.Sword, new Mode());
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.CaneOfSomaria, new Mode());
                    _itemSubscriptions.Add(ItemType.CaneOfByrna, new Mode());
                    _itemSubscriptions.Add(ItemType.Cape, new Mode() { ItemPlacement = ItemPlacement.Advanced });

                    break;
                case BossType.Kholdstare:

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

                    _itemSubscriptions.Add(ItemType.FireRod, new Mode());
                    _itemSubscriptions.Add(ItemType.Bombos, new Mode());
                    _itemSubscriptions.Add(ItemType.Sword, new Mode());
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.Bottle, new Mode());
                    _itemSubscriptions.Add(ItemType.HalfMagic, new Mode());

                    break;
                case BossType.Vitreous:

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

                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.Sword, new Mode());
                    _itemSubscriptions.Add(ItemType.Bow, new Mode());

                    break;
                case BossType.Trinexx:

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

                    _itemSubscriptions.Add(ItemType.FireRod, new Mode());
                    _itemSubscriptions.Add(ItemType.IceRod, new Mode());
                    _itemSubscriptions.Add(ItemType.Sword, new Mode());
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.Bottle, new Mode());
                    _itemSubscriptions.Add(ItemType.HalfMagic, new Mode());

                    break;
                case BossType.Aga:

                    GetAccessibility = () =>
                    {
                        if (_game.Items.Has(ItemType.Sword) || _game.Items.Has(ItemType.Hammer) ||
                            _game.Items.Has(ItemType.Net))
                            return AccessibilityLevel.Normal;

                        return AccessibilityLevel.None;
                    };

                    _itemSubscriptions.Add(ItemType.Sword, new Mode());
                    _itemSubscriptions.Add(ItemType.Hammer, new Mode());
                    _itemSubscriptions.Add(ItemType.Net, new Mode());

                    break;
            }

            _game.Mode.PropertyChanged += OnGameModeChanged;

            UpdateItemSubscriptions();
            UpdateAccessibility();
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnGameModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ItemPlacement" && _updateOnItemPlacementChange)
            {
                UpdateItemSubscriptions();
                UpdateAccessibility();
            }
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

        private void UpdateAccessibility()
        {
            Accessibility = GetAccessibility();
        }

    }
}
