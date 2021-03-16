using System.ComponentModel;

namespace OpenTracker.Models.AutoTracking.Values
{
    /// <summary>
    /// This class contains the auto-tracking result value of a comparison of a SNES memory address to a given value.
    /// </summary>
    public class AutoTrackAddressBool : AutoTrackValue
    {
        private readonly IMemoryAddress _address;
        private readonly byte _comparison;
        private readonly int _trueValue;

        public delegate AutoTrackAddressBool Factory(IMemoryAddress address, byte comparison, int trueValue);

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
        public AutoTrackAddressBool(IMemoryAddress address, byte comparison, int trueValue)
        {
            _address = address;
            _comparison = comparison;
            _trueValue = trueValue;
            
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
            return _address.Value > _comparison ? _trueValue : 0;
        }
    }
}
