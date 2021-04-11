using System.ComponentModel;
using OpenTracker.Models.AutoTracking;

namespace OpenTracker.Models.Requirements.AutoTracking
{
    /// <summary>
    ///     This class contains race illegal tracking requirement data.
    /// </summary>
    public class RaceIllegalTrackingRequirement : BooleanRequirement, IRaceIllegalTrackingRequirement
    {
        private readonly IAutoTracker _autoTracker;
        private readonly bool _expectedValue;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="autoTracker">
        ///     The auto-tracker data.
        /// </param>
        public RaceIllegalTrackingRequirement(IAutoTracker autoTracker)
        {
            _autoTracker = autoTracker;
            _expectedValue = true;

            _autoTracker.PropertyChanged += OnAutoTrackerChanged;

            UpdateValue();
        }

        /// <summary>
        ///     Subscribes to the PropertyChanged event on the IAutoTracker interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
        /// </param>
        private void OnAutoTrackerChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IAutoTracker.RaceIllegalTracking))
            {
                UpdateValue();
            }
        }

        protected override bool ConditionMet()
        {
            return _autoTracker.RaceIllegalTracking == _expectedValue;
        }
    }
}
