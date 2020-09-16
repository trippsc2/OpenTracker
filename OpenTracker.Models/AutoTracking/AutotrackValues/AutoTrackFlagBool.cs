using System;
using System.ComponentModel;

namespace OpenTracker.Models.AutoTracking.AutotrackValues
{
    /// <summary>
    /// This is the class for representing the autotracking result value of a memory flag.
    /// </summary>
    public class AutoTrackFlagBool : AutoTrackValue
    {
        private readonly MemoryFlag _flag;
        private readonly int _trueValue;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="flag">
        /// The memory flag to be checked.
        /// </param>
        /// <param name="trueValue">
        /// The resultant value, if the flag is set.
        /// </param>
        public AutoTrackFlagBool(MemoryFlag flag, int trueValue)
        {
            _flag = flag ?? throw new ArgumentNullException(nameof(flag));
            _trueValue = trueValue;

            _flag.PropertyChanged += OnMemoryChanged;
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
            if (e.PropertyName == nameof(MemoryFlag.Status))
            {
                UpdateCurrentValue();
            }
        }

        /// <summary>
        /// Updates the current value of this value.
        /// </summary>
        private void UpdateCurrentValue()
        {
            CurrentValue = _flag.Status ? _trueValue : 0;
        }
    }
}
