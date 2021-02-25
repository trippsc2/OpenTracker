using OpenTracker.Models.AutoTracking;
using System.ComponentModel;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This class contains race illegal tracking requirement data.
    /// </summary>
    public class RaceIllegalTrackingRequirement : BooleanRequirement
    {
        private readonly IAutoTracker _autoTracker;
        private readonly bool _expectedValue;

        public delegate RaceIllegalTrackingRequirement Factory(bool expectedValue);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="autoTracker">
        /// The autotracker data.
        /// </param>
        /// <param name="expectedValue">
        /// A boolean expected race illegal tracking requirement value.
        /// </param>
        public RaceIllegalTrackingRequirement(IAutoTracker autoTracker, bool expectedValue)
        {
            _autoTracker = autoTracker;
            _expectedValue = expectedValue;

            _autoTracker.PropertyChanged += OnAutoTrackerChanged;

            UpdateValue();
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IAutoTracker interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
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
