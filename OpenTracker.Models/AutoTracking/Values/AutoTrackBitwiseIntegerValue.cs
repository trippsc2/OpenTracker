using System;
using System.ComponentModel;

namespace OpenTracker.Models.AutoTracking.Values
{
    /// <summary>
    /// This is the class for representing the autotracking result value of a memory address
    /// bitwise integer.
    /// </summary>
    public class AutoTrackBitwiseIntegerValue : AutoTrackValue
    {
        private readonly IMemoryAddress _address;
        private readonly byte _mask;
        private readonly int _shift;

        public delegate AutoTrackBitwiseIntegerValue Factory(
            IMemoryAddress address, byte mask, int shift);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="address">
        /// The memory address to be represented.
        /// </param>
        /// <param name="mask">
        /// The bitwise mask of to be represented.
        /// </param>
        /// <param name="adjustment">
        /// The number of bitwise digits to right shift the address value.
        /// </param>
        public AutoTrackBitwiseIntegerValue(IMemoryAddress address, byte mask, int shift)
        {
            _address = address ?? throw new ArgumentNullException(nameof(address));
            _mask = mask;
            _shift = shift;

            _address.PropertyChanged += OnMemoryChanged;
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
            if (e.PropertyName == nameof(IMemoryAddress.Value))
            {
                UpdateCurrentValue();
            }
        }

        /// <summary>
        /// Updates the current value of this value.
        /// </summary>
        private void UpdateCurrentValue()
        {
            byte maskedValue = (byte)(_address.Value & _mask);
            byte newValue = (byte)(maskedValue >> _shift);
            CurrentValue = newValue;
        }
    }
}
