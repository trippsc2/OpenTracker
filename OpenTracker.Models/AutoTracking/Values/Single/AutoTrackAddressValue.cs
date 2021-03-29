using System.ComponentModel;
using OpenTracker.Models.AutoTracking.Memory;

namespace OpenTracker.Models.AutoTracking.Values.Single
{
    /// <summary>
    ///     This class contains the auto-tracking result value of a memory address value.
    /// </summary>
    public class AutoTrackAddressValue : AutoTrackValue, IAutoTrackAddressValue
    {
        private readonly IMemoryAddress _address;
        private readonly byte _maximum;
        private readonly int _adjustment;
        
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="address">
        ///     The memory address for the comparison.
        /// </param>
        /// <param name="maximum">
        ///     An 8-bit unsigned integer representing the maximum valid value of the memory address.
        /// </param>
        /// <param name="adjustment">
        ///     A 32-bit signed integer representing the amount that the result value should be adjusted from the actual
        ///         value.
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

        protected override int? GetNewValue()
        {
            if (_address.Value is null)
            {
                return null;
            }
            
            var newValue = _address.Value.Value + _adjustment;

            return newValue > _maximum ? (int?) null : newValue;
        }
    }
}
