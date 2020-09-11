using System;
using System.ComponentModel;

namespace OpenTracker.Models.AutoTracking.AutotrackValues
{
    public class AutoTrackAddressBool : AutoTrackValue
    {
        private readonly MemoryAddress _address;
        private readonly byte _comparison;
        private readonly int _trueValue;

        public AutoTrackAddressBool(MemoryAddress address, byte comparison, int trueValue)
        {
            _address = address ?? throw new ArgumentNullException(nameof(address));
            _comparison = comparison;
            _trueValue = trueValue;

            _address.PropertyChanged += OnMemoryChanged;
        }

        private void OnMemoryChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MemoryAddress.Value))
            {
                UpdateCurrentValue();
            }
        }

        private void UpdateCurrentValue()
        {
            CurrentValue = _address.Value > _comparison ? _trueValue : 0;
        }
    }
}
