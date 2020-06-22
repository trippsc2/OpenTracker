using OpenTracker.Models.Enums;
using System;
using System.ComponentModel;

namespace OpenTracker.Models
{
    /// <summary>
    /// This is the class for the boss placement.
    /// </summary>
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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">
        /// The game data parent class.
        /// </param>
        /// <param name="iD">
        /// The boss placement ID.
        /// </param>
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

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The string of the property name of the changed property.
        /// </param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (propertyName == nameof(Boss))
                UpdateBossSubscription();
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the Mode class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Mode.BossShuffle))
                UpdateBossSubscription();
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the Boss classes contained.
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
        /// Updates the currently subscribed boss.
        /// </summary>
        private void UpdateBossSubscription()
        {
            if (_game.Mode.BossShuffle)
            {
                if (Boss != _currentBossSubscription)
                {
                    if (_currentBossSubscription == null)
                    {
                        _game.Bosses.PropertyChanged -= OnRequirementChanged;
                    }
                    else
                    {
                        _currentBossSubscription.PropertyChanged -= OnRequirementChanged;
                    }

                    _currentBossSubscription = Boss;

                    if (_currentBossSubscription == null)
                    {
                        _game.Bosses.PropertyChanged += OnRequirementChanged;
                    }
                    else
                    {
                        _currentBossSubscription.PropertyChanged += OnRequirementChanged;
                    }
                }
            }
            else
            {
                if (_defaultBoss != _currentBossSubscription)
                {
                    if (_currentBossSubscription == null)
                    {
                        _game.Bosses.PropertyChanged -= OnRequirementChanged;
                    }
                    else
                    {
                        _currentBossSubscription.PropertyChanged -= OnRequirementChanged;
                    }

                    _currentBossSubscription = _defaultBoss;

                    if (_currentBossSubscription == null)
                    {
                        _game.Bosses.PropertyChanged += OnRequirementChanged;
                    }
                    else
                    {
                        _currentBossSubscription.PropertyChanged += OnRequirementChanged;
                    }
                }
            }
        }

        /// <summary>
        /// Updates the accessibility of this boss placement.
        /// </summary>
        private void UpdateAccessibility()
        {
            if (_game.Mode.BossShuffle)
            {
                if (Boss == null)
                {
                    Accessibility = _game.Bosses.UnknownBossAccessibility;
                }
                else
                {
                    Accessibility = Boss.Accessibility;
                }
            }
            else
            {
                if (_defaultBoss != null)
                {
                    Accessibility = _defaultBoss.Accessibility;
                }
            }
        }

        /// <summary>
        /// Resets the boss placement to their starting values.
        /// </summary>
        public void Reset()
        {
            if (_defaultBoss != _game.Bosses[BossType.Aga])
            {
                Boss = null;
            }
        }
    }
}
