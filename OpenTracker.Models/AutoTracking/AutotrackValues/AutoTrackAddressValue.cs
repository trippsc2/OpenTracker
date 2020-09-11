using System;
using System.ComponentModel;

namespace OpenTracker.Models.AutoTracking.AutotrackValues
{
    public class AutoTrackAddressValue : AutoTrackValue
    {
        private readonly MemoryAddress _address;
        private readonly byte _maximum;
        private readonly int _adjustment;

        public AutoTrackAddressValue(MemoryAddress address, byte maximum, int adjustment)
        {
            _address = address ?? throw new ArgumentNullException(nameof(address));
            _maximum = maximum;
            _adjustment = adjustment;

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
            int newValue = _address.Value + _adjustment;

            if (newValue > _maximum)
            {
                CurrentValue = null;
            }
            else
            {
                CurrentValue = newValue;
            }
        }
    }
}
