using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models.AutoTracking.AutotrackValues
{
    public class AutoTrackMultipleOverride : AutoTrackValue
    {
        private readonly List<IAutoTrackValue> _values;

        public AutoTrackMultipleOverride(List<IAutoTrackValue> values)
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
            foreach (var value in _values)
            {
                if (value.CurrentValue > 0)
                {
                    CurrentValue = value.CurrentValue;
                    break;
                }
            }
        }
    }
}
