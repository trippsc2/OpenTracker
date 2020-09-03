using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models.AutoTracking.AutotrackValues
{
    public class AutoTrackMultipleSum : AutoTrackValue
    {
        private readonly List<IAutoTrackValue> _values;

        public AutoTrackMultipleSum(List<IAutoTrackValue> values)
        {
            _values = values ?? throw new ArgumentNullException(nameof(values));

            foreach (var value in values)
            {
                value.PropertyChanged += OnMemoryChanged;
            }
        }

        private void OnMemoryChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IAutoTrackValue.CurrentValue))
            {
                UpdateCurrentValue();
            }
        }

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
