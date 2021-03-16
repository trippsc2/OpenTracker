using System.Collections.Generic;
using System.ComponentModel;

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

            UpdateValue();

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

        protected override int? GetNewValue()
        {
            var valuesNotNull = _values.FindAll(x => x.CurrentValue.HasValue);

            if (valuesNotNull.Count == 0)
            {
                return null;
            }

            var result = 0;
            
            foreach (var value in valuesNotNull)
            {
                if (result < value.CurrentValue)
                {
                    result = value.CurrentValue.Value;
                }
            }

            return result;
        }
    }
}
