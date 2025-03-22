using System.ComponentModel;
using OpenTracker.Models.AutoTracking.Memory;

namespace OpenTracker.Models.AutoTracking.Values.Single
{
    /// <summary>
    /// This class contains the auto-tracking result value of a comparison of a SNES memory address to a given value.
    /// </summary>
    public class AutoTrackAddressBool : AutoTrackValueBase, IAutoTrackAddressBool
    {
        private readonly IMemoryAddress _address;
        private readonly byte _comparison;
        private readonly int _trueValue;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="address">
        ///     The <see cref="IMemoryAddress"/> for the comparison.
        /// </param>
        /// <param name="comparison">
        ///     A <see cref="byte"/> representing the value to which the memory address is compared.
        /// </param>
        /// <param name="trueValue">
        ///     A <see cref="int"/> representing the resultant value, if the comparison is true.
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
        /// Subscribes to the <see cref="IMemoryAddress.PropertyChanged"/> event.
        /// </summary>
        /// <param name="sender">
        ///     The <see cref="object"/> from which the event was sent.
        /// </param>
        /// <param name="e">
        ///     The <see cref="PropertyChangedEventArgs"/>.
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
            
            return _address.Value > _comparison ? _trueValue : 0;
        }
    }
}
