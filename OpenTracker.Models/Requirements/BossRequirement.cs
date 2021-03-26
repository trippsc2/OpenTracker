using System;
using System.ComponentModel;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Modes;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This class contains boss placement requirement data.
    /// </summary>
    public class BossRequirement : AccessibilityRequirement
    {
        private readonly IRequirementDictionary _requirements;
        private readonly IBossPlacement _bossPlacement;

        private IRequirement? _currentBossRequirement;
        private IRequirement CurrentBossRequirement
        {
            get => _currentBossRequirement ??
                throw new NullReferenceException();
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

                    UpdateValue();
                }
            }
        }

        public delegate BossRequirement Factory(IBossPlacement bossPlacement);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode settings.
        /// </param>
        /// <param name="requirements">
        /// The requirement dictionary.
        /// </param>
        /// <param name="bossPlacement">
        /// The boss placement to provide requirements.
        /// </param>
        public BossRequirement(
            IMode mode, IRequirementDictionary requirements, IBossPlacement bossPlacement)
        {
            _requirements = requirements;
            _bossPlacement = bossPlacement;

            mode.PropertyChanged += OnModeChanged;
            _bossPlacement.PropertyChanged += OnBossPlacementChanged;
            
            UpdateRequirement();
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IMode interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnModeChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IMode.BossShuffle))
            {
                UpdateRequirement();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IBossPlacement interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnBossPlacementChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IBossPlacement.Boss))
            {
                UpdateRequirement();
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
        private void OnRequirementChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IRequirement.Accessibility))
            {
                UpdateValue();
            }
        }

        /// <summary>
        /// Updates boss requirement of this requirement.
        /// </summary>
        private void UpdateRequirement()
        {
            var boss = _bossPlacement.GetCurrentBoss();

            if (boss == null)
            {
                CurrentBossRequirement = _requirements[RequirementType.UnknownBoss];
                return;
            }
            
            CurrentBossRequirement = boss.Value switch
            {
                BossType.Test => _requirements[RequirementType.NoRequirement],
                BossType.Armos => _requirements[RequirementType.Armos],
                BossType.Lanmolas => _requirements[RequirementType.Lanmolas],
                BossType.Moldorm => _requirements[RequirementType.Moldorm],
                BossType.HelmasaurKing => _requirements[RequirementType.HelmasaurKing],
                BossType.Arrghus => _requirements[RequirementType.Arrghus],
                BossType.Mothula => _requirements[RequirementType.Mothula],
                BossType.Blind => _requirements[RequirementType.Blind],
                BossType.Kholdstare => _requirements[RequirementType.Kholdstare],
                BossType.Vitreous => _requirements[RequirementType.Vitreous],
                BossType.Trinexx => _requirements[RequirementType.Trinexx],
                BossType.Aga => _requirements[RequirementType.AgaBoss],
                _ => throw new Exception()
            };
        }

        protected override AccessibilityLevel GetAccessibility()
        {
            return CurrentBossRequirement.Accessibility;
        }
    }
}
