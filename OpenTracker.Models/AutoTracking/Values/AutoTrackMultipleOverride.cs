using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace OpenTracker.Models.AutoTracking.Values
{
    /// <summary>
    /// This is the class for representing the auto-tracking result value of an ordered priority
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
        /// The list of auto-tracking result values.
        /// </param>
        public AutoTrackMultipleOverride(List<IAutoTrackValue> values)
        {
            _values = values;

            foreach (var value in values)
            {
                value.PropertyChanged += OnValueChanged;
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
        private void OnValueChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IAutoTrackValue.CurrentValue))
            {
                UpdateValue();
            }
        }

        /// <summary>
        /// Updates the current value of this value.
        /// </summary>
        protected override int? GetNewValue()
        {
            if (!_values.Exists(value => value.CurrentValue.HasValue))
            {
                return null;
            }
            
            foreach (var value in _values.Where(value => value.CurrentValue.HasValue && value.CurrentValue > 0))
            {
                return value.CurrentValue;
            }

            return 0;
        }
    }
}
