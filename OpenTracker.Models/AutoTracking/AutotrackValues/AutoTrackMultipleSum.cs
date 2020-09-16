using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models.AutoTracking.AutotrackValues
{
    /// <summary>
    /// This is the class for representing the autotracking result value of a list of results to
    /// be summed.
    /// </summary>
    public class AutoTrackMultipleSum : AutoTrackValue
    {
        private readonly List<IAutoTrackValue> _values;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="values">
        /// The list of autotracking result values.
        /// </param>
        public AutoTrackMultipleSum(List<IAutoTrackValue> values)
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
            int newValue = 0;

            foreach (var value in _values)
            {
                if (value.CurrentValue.HasValue)
                {
                    newValue += value.CurrentValue.Value;
                }
            }

            CurrentValue = newValue;
        }
    }
}
