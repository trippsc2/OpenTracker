using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace OpenTracker.Models.AutoTracking.Values.Multiple
{
    /// <summary>
    ///     This class contains the auto-tracking result value of a list of results to be summed.
    /// </summary>
    public class AutoTrackMultipleSum : AutoTrackValue, IAutoTrackMultipleSum
    {
        private readonly IList<IAutoTrackValue> _values;
        
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="values">
        ///     The list of auto-tracking result values.
        /// </param>
        public AutoTrackMultipleSum(IList<IAutoTrackValue> values)
        {
            _values = values;
            
            UpdateValue();

            foreach (var value in values)
            {
                value.PropertyChanged += OnValueChanged;
            }
        }

        /// <summary>
        ///     Subscribes to the PropertyChanged event on the IAutoTrackValue interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
        /// </param>
        private void OnValueChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IAutoTrackValue.CurrentValue))
            {
                UpdateValue();
            }
        }

        /// <summary>
        ///     Updates the current value of this value.
        /// </summary>
        protected override int? GetNewValue()
        {
            if (!_values.Any(x => x.CurrentValue.HasValue))
            {
                return null;
            }
            
            var newValue = _values.Sum(value => value.CurrentValue ?? 0);

            return newValue;
        }
    }
}
