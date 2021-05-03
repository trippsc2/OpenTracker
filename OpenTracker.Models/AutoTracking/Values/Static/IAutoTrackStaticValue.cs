namespace OpenTracker.Models.AutoTracking.Values.Static
{
    /// <summary>
    ///     This interface contains the auto-tracking result of a static value.
    /// </summary>
    public interface IAutoTrackStaticValue : IAutoTrackValue
    {
        /// <summary>
        ///     A factory for creating auto-tracking results of static values.
        /// </summary>
        /// <param name="value">
        ///     A 32-bit signed integer for the static value.
        /// </param>
        /// <returns>
        ///     A new auto-tracking result of a static value.
        /// </returns>
        public delegate IAutoTrackStaticValue Factory(int value);
    }
}