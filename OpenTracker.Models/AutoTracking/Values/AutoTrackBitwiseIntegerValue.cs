using System.ComponentModel;
using OpenTracker.Models.AutoTracking.Memory;

namespace OpenTracker.Models.AutoTracking.Values
{
    /// <summary>
    /// This class contains the auto-tracking result value of a memory address bitwise integer.
    /// </summary>
    public class AutoTrackBitwiseIntegerValue : AutoTrackValue
    {
        private readonly IMemoryAddress _address;
        private readonly byte _mask;
        private readonly int _shift;

        public delegate AutoTrackBitwiseIntegerValue Factory(IMemoryAddress address, byte mask, int shift);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="address">
        /// The memory address to be represented.
        /// </param>
        /// <param name="mask">
        /// The bitwise mask of to be represented.
        /// </param>
        /// <param name="shift">
        /// The number of bitwise digits to right shift the address value.
        /// </param>
        public AutoTrackBitwiseIntegerValue(IMemoryAddress address, byte mask, int shift)
        {
            _address = address;
            _mask = mask;
            _shift = shift;
            
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

        /// <summary>
        /// Updates the current value of this value.
        /// </summary>
        protected override int? GetNewValue()
        {
            if (_address.Value is null)
            {
                return null;
            }
            
            var maskedValue = (byte)(_address.Value & _mask);
            var newValue = (byte)(maskedValue >> _shift);
            return newValue;
        }
    }
}
