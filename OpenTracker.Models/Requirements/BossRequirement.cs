using OpenTracker.Models.Enums;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This is the class for the requirement of boss placements.
    /// </summary>
    internal class BossRequirement : IRequirement
    {
        private readonly Game _game;
        private readonly BossPlacement _bossPlacement;

        public event PropertyChangedEventHandler PropertyChanged;

        private IRequirement _currentBossRequirement;
        private IRequirement CurrentBossRequirement
        {
            get => _currentBossRequirement;
            set
            {
                if (_currentBossRequirement != value)
                {
                    if (_currentBossRequirement != null)
                    {
                        _currentBossRequirement.PropertyChanged -= OnRequirementChanged;
                    }

                    _currentBossRequirement = value;

                    if (_currentBossRequirement != null)
                    {
                        _currentBossRequirement.PropertyChanged += OnRequirementChanged;
                    }
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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bossPlacement">
        /// The boss placement to provide requirements.
        /// </param>
        public BossRequirement(Game game, BossPlacement bossPlacement)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
            _bossPlacement = bossPlacement ?? throw new ArgumentNullException(nameof(bossPlacement));

            game.Mode.PropertyChanged += OnModeChanged;
            _bossPlacement.PropertyChanged += OnBossPlacementChanged;
            
            UpdateRequirement();
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
            {
                UpdateRequirement();
                UpdateAccessibility();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the BossPlacement class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnBossPlacementChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BossPlacement.Boss))
            {
                UpdateRequirement();
                UpdateAccessibility();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IRequirement interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IRequirement.Accessibility))
            {
                UpdateAccessibility();
            }
        }

        /// <summary>
        /// Updates boss requirement of this requirement.
        /// </summary>
        private void UpdateRequirement()
        {
            BossType? boss = _bossPlacement.GetCurrentBoss();

            if (boss.HasValue)
            {
                CurrentBossRequirement = boss.Value switch
                {
                    BossType.Armos => _game.Requirements[RequirementType.Armos],
                    BossType.Lanmolas => _game.Requirements[RequirementType.Lanmolas],
                    BossType.Moldorm => _game.Requirements[RequirementType.Moldorm],
                    BossType.HelmasaurKing => _game.Requirements[RequirementType.None],
                    BossType.Arrghus => _game.Requirements[RequirementType.Arrghus],
                    BossType.Mothula => _game.Requirements[RequirementType.Mothula],
                    BossType.Blind => _game.Requirements[RequirementType.Blind],
                    BossType.Kholdstare => _game.Requirements[RequirementType.Kholdstare],
                    BossType.Vitreous => _game.Requirements[RequirementType.Vitreous],
                    BossType.Trinexx => _game.Requirements[RequirementType.Trinexx],
                    BossType.Aga => _game.Requirements[RequirementType.AgaBoss],
                    _ => throw new Exception()
                };
            }
            else
            {
                Accessibility = _game.Requirements[RequirementType.UnknownBoss].Accessibility;
            }
        }

        /// <summary>
        /// Updates the accessibility of this requirement.
        /// </summary>
        private void UpdateAccessibility()
        {
            Accessibility = CurrentBossRequirement.Accessibility;
        }
    }
}
