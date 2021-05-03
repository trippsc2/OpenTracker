using System;
using System.ComponentModel;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Modes;

namespace OpenTracker.Models.Requirements.Boss
{
    /// <summary>
    ///     This class contains boss placement requirement data.
    /// </summary>
    public class BossRequirement : AccessibilityRequirement, IBossRequirement
    {
        private readonly IBossTypeRequirementDictionary _bossTypeRequirements;
        private readonly IBossPlacement _bossPlacement;

        private IRequirement? _currentBossRequirement;
        private IRequirement CurrentBossRequirement
        {
            get => _currentBossRequirement ?? throw new NullReferenceException();
            set
            {
                if (_currentBossRequirement == value)
                {
                    return;
                }
                
                if (_currentBossRequirement is not null)
                {
                    _currentBossRequirement.PropertyChanged -= OnRequirementChanged;
                }

                _currentBossRequirement = value;

                if (_currentBossRequirement is not null)
                {
                    _currentBossRequirement.PropertyChanged += OnRequirementChanged;
                }

                UpdateValue();
            }
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="mode">
        ///     The mode settings.
        /// </param>
        /// <param name="bossTypeRequirements">
        ///     The boss type requirement dictionary.
        /// </param>
        /// <param name="bossPlacement">
        ///     The boss placement to provide requirements.
        /// </param>
        public BossRequirement(
            IMode mode, IBossTypeRequirementDictionary bossTypeRequirements, IBossPlacement bossPlacement)
        {
            _bossTypeRequirements = bossTypeRequirements;

            _bossPlacement = bossPlacement;

            mode.PropertyChanged += OnModeChanged;
            _bossPlacement.PropertyChanged += OnBossPlacementChanged;
            
            UpdateRequirement();
        }

        /// <summary>
        ///     Subscribes to the PropertyChanged event on the IMode interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
        /// </param>
        private void OnModeChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IMode.BossShuffle))
            {
                UpdateRequirement();
            }
        }

        /// <summary>
        ///     Subscribes to the PropertyChanged event on the IBossPlacement interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
        /// </param>
        private void OnBossPlacementChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IBossPlacement.Boss))
            {
                UpdateRequirement();
            }
        }

        /// <summary>
        ///     Subscribes to the PropertyChanged event on the IRequirement interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
        /// </param>
        private void OnRequirementChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IRequirement.Accessibility))
            {
                UpdateValue();
            }
        }

        /// <summary>
        ///     Updates boss requirement of this requirement.
        /// </summary>
        private void UpdateRequirement()
        {
            var boss = _bossPlacement.GetCurrentBoss();

            CurrentBossRequirement = boss is null ? _bossTypeRequirements.NoBoss.Value
                : _bossTypeRequirements[boss.Value];
        }

        protected override AccessibilityLevel GetAccessibility()
        {
            return CurrentBossRequirement.Accessibility;
        }
    }
}
