using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This is the class containing boss/prize sections of dungeons.
    /// </summary>
    public class PrizeSection : BossSection, IPrizeSection
    {
        private readonly bool _alwaysClearable;
        private readonly IAutoTrackValue? _autoTrackValue;

        public IPrizePlacement PrizePlacement { get; }

        public new delegate PrizeSection Factory(
            string name, IBossPlacement bossPlacement, IPrizePlacement prizePlacement,
            IAutoTrackValue? autoTrackValue, IRequirement requirement,
            bool alwaysClearable = false);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">
        /// A string representing the name of the section.
        /// </param>
        /// <param name="bossPlacement">
        /// The boss placement of this section.
        /// </param>
        /// <param name="prizePlacement">
        /// The prize placement of this section.
        /// </param>
        /// <param name="autoTrackFunction">
        /// The autotracking function.
        /// </param>
        /// <param name="memoryAddresses">
        /// The list of memory addresses to subscribe to for autotracking.
        /// </param>
        /// <param name="requirement">
        /// The requirement for this section to be visible.
        /// </param>
        public PrizeSection(
            string name, IBossPlacement bossPlacement, IPrizePlacement prizePlacement,
            IAutoTrackValue? autoTrackValue, IRequirement requirement,
            bool alwaysClearable = false) : base(name, bossPlacement, requirement)
        {
            _alwaysClearable = alwaysClearable;
            _autoTrackValue = autoTrackValue;
            PrizePlacement = prizePlacement ??
                throw new ArgumentNullException(nameof(prizePlacement));

            PropertyChanged += OnPropertyChanged;
            PrizePlacement.PropertyChanging += OnPrizeChanging;
            PrizePlacement.PropertyChanged += OnPrizeChanged;

            if (_autoTrackValue != null)
            {
                _autoTrackValue.PropertyChanged += OnAutoTrackChanged;
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on itself.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Available))
            {
                if (PrizePlacement.Prize != null)
                {
                    if (IsAvailable())
                    {
                        PrizePlacement.Prize.Remove();
                    }
                    else
                    {
                        PrizePlacement.Prize.Add();
                    }
                }
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanging event on IPrizePlacement interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanging event.
        /// </param>
        private void OnPrizeChanging(object sender, PropertyChangingEventArgs e)
        {
            if (e.PropertyName == nameof(IPrizePlacement.Prize))
            {
                if (!IsAvailable() && PrizePlacement != null &&
                PrizePlacement.Prize != null)
                {
                    PrizePlacement.Prize.Remove();
                }
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IPrizePlacement interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnPrizeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IPrizePlacement.Prize))
            {
                if (!IsAvailable() && PrizePlacement != null &&
                PrizePlacement.Prize != null)
                {
                    PrizePlacement.Prize.Add();
                }
            }
        }

        private void OnAutoTrackChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IAutoTrackValue.CurrentValue))
            {
                AutoTrackUpdate();
            }
        }

        private void AutoTrackUpdate()
        {
            if (_autoTrackValue!.CurrentValue.HasValue)
            {
                if (Available != 1 - _autoTrackValue.CurrentValue.Value)
                {
                    Available = 1 - _autoTrackValue.CurrentValue.Value;
                    //SaveLoadManager.Instance.Unsaved = true;
                }
            }
        }

        /// <summary>
        /// Returns whether the section can be cleared or collected.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the section can be cleared or collected.
        /// </returns>
        public override bool CanBeCleared(bool force)
        {
            if (IsAvailable() && _alwaysClearable)
            {
                return true;
            }

            return base.CanBeCleared(force);
        }
    }
}
