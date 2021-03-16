using ReactiveUI;

namespace OpenTracker.Models.AutoTracking.Values
{
    /// <summary>
    /// This is the class for representing the auto-tracking result of a static value.
    /// </summary>
    public class AutoTrackStaticValue : ReactiveObject, IAutoTrackValue
    {
        public int? CurrentValue { get; }

        public delegate AutoTrackStaticValue Factory(int value);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">
        /// A 32-bit signed integer for the static value.
        /// </param>
        public AutoTrackStaticValue(int value)
        {
            CurrentValue = value;
        }
    }
}
