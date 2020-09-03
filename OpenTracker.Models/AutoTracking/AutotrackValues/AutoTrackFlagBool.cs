using System;
using System.ComponentModel;

namespace OpenTracker.Models.AutoTracking.AutotrackValues
{
    public class AutoTrackFlagBool : AutoTrackValue
    {
        private readonly MemoryFlag _flag;
        private readonly int _trueValue;

        public AutoTrackFlagBool(MemoryFlag flag, int trueValue)
        {
            _flag = flag ?? throw new ArgumentNullException(nameof(flag));
            _trueValue = trueValue;

            _flag.PropertyChanged += OnMemoryChanged;
        }

        private void OnMemoryChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MemoryFlag.Status))
            {
                UpdateCurrentValue();
            }
        }

        private void UpdateCurrentValue()
        {
            CurrentValue = _flag.Status ? _trueValue : 0;
        }
    }
}
