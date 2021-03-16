using System.ComponentModel;

namespace OpenTracker.Models.AutoTracking.Values
{
    /// <summary>
    /// This class contains the auto-tracking result value of a memory address value.
    /// </summary>
    public class AutoTrackAddressValue : AutoTrackValue
    {
        private readonly IMemoryAddress _address;
        private readonly byte _maximum;
        private readonly int _adjustment;

        public delegate AutoTrackAddressValue Factory(IMemoryAddress address, byte maximum, int adjustment);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="address">
        /// The memory address for the comparison.
        /// </param>
        /// <param name="maximum">
        /// The maximum valid value of the memory address.
        /// </param>
        /// <param name="adjustment">
        /// The amount that the result value should be adjusted from the true value.
        /// </param>
        public AutoTrackAddressValue(IMemoryAddress address, byte maximum, int adjustment)
        {
            _address = address;
            _maximum = maximum;
            _adjustment = adjustment;

            UpdateValue();
            
            _address.PropertyChanged += OnMemoryChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IMemoryAddress interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnMemoryChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IMemoryAddress.Value))
            {
                UpdateValue();
            }
        }

        protected override int? GetNewValue()
        {
            var newValue = _address.Value + _adjustment;

            return newValue > _maximum ? (int?) null : newValue;
        }
    }
}
