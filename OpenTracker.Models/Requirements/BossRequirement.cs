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
                UpdateAccessibility();
            }
        }

        /// <summary>
        /// Updates the accessibility of this requirement.
        /// </summary>
        private void UpdateAccessibility()
        {
            BossType? boss = _bossPlacement.GetCurrentBoss();

            if (boss.HasValue)
            {
                Accessibility = boss.Value switch
                {
                    BossType.Armos => _game.Requirements[RequirementType.Armos].Accessibility,
                    BossType.Lanmolas => _game.Requirements[RequirementType.Lanmolas].Accessibility,
                    BossType.Moldorm => _game.Requirements[RequirementType.Moldorm].Accessibility,
                    BossType.HelmasaurKing => _game.Requirements[RequirementType.None].Accessibility,
                    BossType.Arrghus => _game.Requirements[RequirementType.Arrghus].Accessibility,
                    BossType.Mothula => _game.Requirements[RequirementType.Mothula].Accessibility,
                    BossType.Blind => _game.Requirements[RequirementType.Blind].Accessibility,
                    BossType.Kholdstare => _game.Requirements[RequirementType.Kholdstare].Accessibility,
                    BossType.Vitreous => _game.Requirements[RequirementType.Vitreous].Accessibility,
                    BossType.Trinexx => _game.Requirements[RequirementType.Trinexx].Accessibility,
                    BossType.Aga => _game.Requirements[RequirementType.AgaBoss].Accessibility,
                    _ => throw new Exception()
                };
            }
            else
            {
                Accessibility = _game.Requirements[RequirementType.UnknownBoss].Accessibility;
            }

        }
    }
}
