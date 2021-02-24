using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models.AutoTracking.Values
{
    /// <summary>
    /// This is the class for representing the autotracking result value of an ordered priority
    /// list of results.
    /// </summary>
    public class AutoTrackMultipleOverride : AutoTrackValue
    {
        private readonly List<IAutoTrackValue> _values;

        public delegate AutoTrackMultipleOverride Factory(List<IAutoTrackValue> values);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="values">
        /// The list of autotracking result values.
        /// </param>
        public AutoTrackMultipleOverride(List<IAutoTrackValue> values)
        {
            _values = values ?? throw new ArgumentNullException(nameof(values));

            foreach (var value in values)
            {
                value.PropertyChanged += OnMemoryChanged;
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the MemoryAddress class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnMemoryChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IAutoTrackValue.CurrentValue))
            {
                UpdateCurrentValue();
            }
        }

        /// <summary>
        /// Updates the current value of this value.
        /// </summary>
        private void UpdateCurrentValue()
        {
            foreach (var value in _values)
            {
                if (value.CurrentValue.HasValue && value.CurrentValue > 0)
                {
                    CurrentValue = value.CurrentValue;
                    break;
                }
            }
        }
    }
}
