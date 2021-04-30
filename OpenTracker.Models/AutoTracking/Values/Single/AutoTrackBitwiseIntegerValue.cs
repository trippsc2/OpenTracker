using System.ComponentModel;
using OpenTracker.Models.AutoTracking.Memory;

namespace OpenTracker.Models.AutoTracking.Values.Single
{
    /// <summary>
    ///     This class contains the auto-tracking result value of a memory address bitwise integer.
    /// </summary>
    public class AutoTrackBitwiseIntegerValue : AutoTrackValueBase, IAutoTrackBitwiseIntegerValue
    {
        private readonly IMemoryAddress _address;
        private readonly byte _mask;
        private readonly int _shift;
        
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="address">
        ///     The memory address to be represented.
        /// </param>
        /// <param name="mask">
        ///     An 8-bit unsigned integer representing the bitwise mask of relevant bits.
        /// </param>
        /// <param name="shift">
        ///     A 32-bit signed integer representing the number of bitwise digits to right shift the address value.
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
        ///     Subscribes to the PropertyChanged event on the IMemoryAddress interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
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
