﻿using System;
using System.ComponentModel;
using ReactiveUI;

namespace OpenTracker.Models.AutoTracking.Values.Multiple
{
    /// <summary>
    /// This class contains the auto-tracking result value data of subtracting a pair of values.
    /// </summary>
    public class AutoTrackMultipleDifference : ReactiveObject, IAutoTrackMultipleDifference
    {
        private readonly IAutoTrackValue _value1;
        private readonly IAutoTrackValue _value2;

        public int? CurrentValue =>
            _value1.CurrentValue.HasValue ? _value2.CurrentValue.HasValue
                ? Math.Max(0, _value1.CurrentValue.Value - _value2.CurrentValue.Value)
                : _value1.CurrentValue.Value : null;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value1">
        ///     The <see cref="IAutoTrackValue"/> from which to subtract.
        /// </param>
        /// <param name="value2">
        ///     The <see cref="IAutoTrackValue"/> to be subtracted.
        /// </param>
        public AutoTrackMultipleDifference(IAutoTrackValue value1, IAutoTrackValue value2)
        {
            _value1 = value1;
            _value2 = value2;

            _value1.PropertyChanged += OnValueChanged;
            _value2.PropertyChanged += OnValueChanged;
        }

        /// <summary>
        /// Subscribes to the <see cref="IAutoTrackValue.PropertyChanged"/> event.
        /// </summary>
        /// <param name="sender">
        ///     The <see cref="object"/> from which the event was sent.
        /// </param>
        /// <param name="e">
        ///     The <see cref="PropertyChangedEventArgs"/>.
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
