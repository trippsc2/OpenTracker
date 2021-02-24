using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.AutoTracking;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This is the class for race illegal tracking requirements.
    /// </summary>
    public class RaceIllegalTrackingRequirement : BooleanRequirement
    {
        private readonly IAutoTracker _autoTracker;
        private readonly bool _expectedValue;

        public delegate RaceIllegalTrackingRequirement Factory(bool expectedValue);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="expectedValue">
        /// The required value.
        /// </param>
        public RaceIllegalTrackingRequirement(IAutoTracker autoTracker, bool expectedValue)
        {
            _autoTracker = autoTracker;
            _expectedValue = expectedValue;

            _autoTracker.PropertyChanged += OnAutoTrackerChanged;

            UpdateValue();
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the AutoTracker class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnAutoTrackerChanged(object sender, PropertyChangedEventArgs e)
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
