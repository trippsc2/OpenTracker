using System.ComponentModel;

namespace OpenTracker.Models.AutoTracking.AutotrackValues
{
    /// <summary>
    /// This is the class for representing the autotracking result of a static value.
    /// </summary>
    public class AutoTrackStaticValue : IAutoTrackValue
    {
        private readonly int _value;

        public int? CurrentValue =>
            _value;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">
        /// A 32-bit signed integer for the static value.
        /// </param>
        public AutoTrackStaticValue(int value)
        {
            _value = value;
        }
    }
}
