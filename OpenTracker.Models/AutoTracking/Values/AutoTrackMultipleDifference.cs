using System;
using System.ComponentModel;
using ReactiveUI;

namespace OpenTracker.Models.AutoTracking.Values
{
    /// <summary>
    /// This is the class for representing the auto-tracking result value of subtracting a pair of values.
    /// </summary>
    public class AutoTrackMultipleDifference : ReactiveObject, IAutoTrackValue
    {
        private readonly IAutoTrackValue _value1;
        private readonly IAutoTrackValue _value2;

        public int? CurrentValue =>
            _value1.CurrentValue.HasValue ?
                (int?) (_value2.CurrentValue.HasValue ?
                    Math.Max(0, _value1.CurrentValue.Value - _value2.CurrentValue.Value) :
                    _value1.CurrentValue.Value) : null;

        public delegate AutoTrackMultipleDifference Factory(IAutoTrackValue value1, IAutoTrackValue value2);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value1">
        /// The value from which to subtract.
        /// </param>
        /// <param name="value2">
        /// The value to be subtracted.
        /// </param>
        public AutoTrackMultipleDifference(IAutoTrackValue value1, IAutoTrackValue value2)
        {
            _value1 = value1;
            _value2 = value2;

            _value1.PropertyChanged += OnValueChanged;
            _value2.PropertyChanged += OnValueChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IAutoTrackValue interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnValueChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IAutoTrackValue.CurrentValue))
            {
                this.RaisePropertyChanged(nameof(CurrentValue));
            }
        }
    }
}
