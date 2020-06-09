using OpenTracker.Models.Enums;
using System;
using System.ComponentModel;

namespace OpenTracker.Models
{
    public class BossPlacement : INotifyPropertyChanged
    {
        private readonly Game _game;
        private readonly BossPlacementID _iD;
        private readonly Boss _defaultBoss;
        private Boss _currentBossSubscription;

        public event PropertyChangedEventHandler PropertyChanged;

        private Boss _boss;
        public Boss Boss
        {
            get => _boss;
            set
            {
                if (_boss != value)
                {
                    _boss = value;
                    OnPropertyChanged(nameof(Boss));
                }
            }
        }

        private AccessibilityLevel _accessibility;
        public AccessibilityLevel Accessibility
        {
            get => _accessibility;
            set
            {
                if (_accessibility != value)
                {
                    _accessibility = value;
                    OnPropertyChanged(nameof(Accessibility));
                }
            }
        }

        public BossPlacement(Game game, BossPlacementID iD)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
            _iD = iD;

            switch (_iD)
            {
                case BossPlacementID.ATBoss:
                    {
                        _defaultBoss = _game.Bosses[BossType.Aga];
                        Boss = _game.Bosses[BossType.Aga];
                    }
                    break;
                case BossPlacementID.EPBoss:
                    {
                        _defaultBoss = _game.Bosses[BossType.Armos];
                    }
                    break;
                case BossPlacementID.DPBoss:
                    {
                        _defaultBoss = _game.Bosses[BossType.Lanmolas];
                    }
                    break;
                case BossPlacementID.ToHBoss:
                    {
                        _defaultBoss = _game.Bosses[BossType.Moldorm];
                    }
                    break;
                case BossPlacementID.PoDBoss:
                    {
                        _defaultBoss = _game.Bosses[BossType.HelmasaurKing];
                    }
                    break;
                case BossPlacementID.SPBoss:
                    {
                        _defaultBoss = _game.Bosses[BossType.Arrghus];
                    }
                    break;
                case BossPlacementID.SWBoss:
                    {
                        _defaultBoss = _game.Bosses[BossType.Mothula];
                    }
                    break;
                case BossPlacementID.TTBoss:
                    {
                        _defaultBoss = _game.Bosses[BossType.Blind];
                    }
                    break;
                case BossPlacementID.IPBoss:
                    {
                        _defaultBoss = _game.Bosses[BossType.Kholdstare];
                    }
                    break;
                case BossPlacementID.MMBoss:
                    {
                        _defaultBoss = _game.Bosses[BossType.Vitreous];
                    }
                    break;
                case BossPlacementID.TRBoss:
                    {
                        _defaultBoss = _game.Bosses[BossType.Trinexx];
                    }
                    break;
                case BossPlacementID.GTBoss1:
                    {
                        _defaultBoss = _game.Bosses[BossType.Armos];
                    }
                    break;
                case BossPlacementID.GTBoss2:
                    {
                        _defaultBoss = _game.Bosses[BossType.Lanmolas];
                    }
                    break;
                case BossPlacementID.GTBoss3:
                    {
                        _defaultBoss = _game.Bosses[BossType.Moldorm];
                    }
                    break;
                case BossPlacementID.GTFinalBoss:
                    {
                        _defaultBoss = _game.Bosses[BossType.Aga];
                        Boss = _game.Bosses[BossType.Aga];
                    }
                    break;
            }

            _game.Mode.PropertyChanged += OnModeChanged;

            UpdateBossSubscription();
            UpdateAccessibility();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (propertyName == nameof(Boss))
                UpdateBossSubscription();
        }

        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Mode.BossShuffle))
                UpdateBossSubscription();
        }

        private void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateAccessibility();
        }

        private void UpdateBossSubscription()
        {
            if (_game.Mode.BossShuffle.Value)
            {
                if (Boss != _currentBossSubscription)
                {
                    if (_currentBossSubscription == null)
                        _game.Bosses.PropertyChanged -= OnRequirementChanged;
                    else
                        _currentBossSubscription.PropertyChanged -= OnRequirementChanged;

                    _currentBossSubscription = Boss;

                    if (_currentBossSubscription == null)
                        _game.Bosses.PropertyChanged += OnRequirementChanged;
                    else
                        _currentBossSubscription.PropertyChanged += OnRequirementChanged;
                }
            }
            else
            {
                if (_defaultBoss != _currentBossSubscription)
                {
                    if (_currentBossSubscription == null)
                        _game.Bosses.PropertyChanged -= OnRequirementChanged;
                    else
                        _currentBossSubscription.PropertyChanged -= OnRequirementChanged;

                    _currentBossSubscription = _defaultBoss;

                    if (_currentBossSubscription == null)
                        _game.Bosses.PropertyChanged += OnRequirementChanged;
                    else
                        _currentBossSubscription.PropertyChanged += OnRequirementChanged;
                }
            }
        }

        private void UpdateAccessibility()
        {
            if (_game.Mode.BossShuffle.Value)
            {
                if (Boss == null)
                    Accessibility = _game.Bosses.UnknownBossAccessibility;
                else
                    Accessibility = Boss.Accessibility;
            }
            else
            {
                if (_defaultBoss != null)
                    Accessibility = _defaultBoss.Accessibility;
            }
        }

        public void Reset()
        {
            if (_defaultBoss != _game.Bosses[BossType.Aga])
                Boss = null;
        }
    }
}
