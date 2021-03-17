using System.ComponentModel;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This class contains boss/prize section data (end of each dungeon).
    /// </summary>
    public class PrizeSection : BossSection, IPrizeSection
    {
        private readonly bool _alwaysClearable;
        private readonly IAutoTrackValue? _autoTrackValue;

        public IPrizePlacement PrizePlacement { get; }

        public delegate PrizeSection Factory(
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
        /// <param name="autoTrackValue">
        /// The section auto-track value.
        /// </param>
        /// <param name="requirement">
        /// The requirement for this section to be visible.
        /// </param>
        /// <param name="alwaysClearable">
        /// A boolean representing whether the section is always clearable (used for GT final).
        /// </param>
        public PrizeSection(
            string name, IBossPlacement bossPlacement, IPrizePlacement prizePlacement, IAutoTrackValue? autoTrackValue,
            IRequirement requirement, bool alwaysClearable = false) : base(name, bossPlacement, requirement)
        {
            _alwaysClearable = alwaysClearable;
            _autoTrackValue = autoTrackValue;
            PrizePlacement = prizePlacement;

            PropertyChanged += OnPropertyChanged;
            PrizePlacement.PropertyChanging += OnPrizeChanging;
            PrizePlacement.PropertyChanged += OnPrizeChanged;

            if (_autoTrackValue != null)
            {
                _autoTrackValue.PropertyChanged += OnAutoTrackValueChanged;
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
        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(Available) || PrizePlacement.Prize is null)
            {
                return;
            }
            
            if (IsAvailable())
            {
                PrizePlacement.Prize.Remove();
            }
            else
            {
                PrizePlacement.Prize.Add();
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
        private void OnPrizeChanging(object? sender, PropertyChangingEventArgs e)
        {
            if (e.PropertyName != nameof(IPrizePlacement.Prize))
            {
                return;
            }
            
            if (!IsAvailable())
            {
                PrizePlacement.Prize?.Remove();
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
        private void OnPrizeChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(IPrizePlacement.Prize))
            {
                return;
            }
            
            if (!IsAvailable())
            {
                PrizePlacement.Prize?.Add();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IAutoTrackValue interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnAutoTrackValueChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IAutoTrackValue.CurrentValue))
            {
                AutoTrackUpdate();
            }
        }

        /// <summary>
        /// Updates the section value from the autotracked value.
        /// </summary>
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
