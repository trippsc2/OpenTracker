using System;
using System.ComponentModel;

namespace OpenTracker.Models.AutoTracking.AutotrackValues
{
    /// <summary>
    /// This is the class for representing the autotracking result value of a comparison of a SNES
    /// memory address to a given value.
    /// </summary>
    public class AutoTrackAddressBool : AutoTrackValue
    {
        private readonly MemoryAddress _address;
        private readonly byte _comparison;
        private readonly int _trueValue;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="address">
        /// The memory address for the comparison.
        /// </param>
        /// <param name="comparison">
        /// The value to which the memory address is compared.
        /// </param>
        /// <param name="trueValue">
        /// The resultant value, if the comparison is true.
        /// </param>
        public AutoTrackAddressBool(MemoryAddress address, byte comparison, int trueValue)
        {
            _address = address ?? throw new ArgumentNullException(nameof(address));
            _comparison = comparison;
            _trueValue = trueValue;

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
            if (e.PropertyName == nameof(MemoryAddress.Value))
            {
                UpdateCurrentValue();
            }
        }

        /// <summary>
        /// Updates the current value of this value.
        /// </summary>
        private void UpdateCurrentValue()
        {
            CurrentValue = _address.Value > _comparison ? _trueValue : 0;
        }
    }
}
