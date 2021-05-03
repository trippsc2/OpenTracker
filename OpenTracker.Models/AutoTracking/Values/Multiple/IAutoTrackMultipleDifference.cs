namespace OpenTracker.Models.AutoTracking.Values.Multiple
{
    /// <summary>
    ///     This interface contains the auto-tracking result value data of subtracting a pair of values.
    /// </summary>
    public interface IAutoTrackMultipleDifference : IAutoTrackValue
    {
        /// <summary>
        ///     A factory for creating auto-tracking result values of subtracting a pair of values.
        /// </summary>
        /// <param name="value1">
        ///     The value from which to subtract.
        /// </param>
        /// <param name="value2">
        ///     The value to be subtracted.
        /// </param>
        /// <returns>
        ///     A new auto-tracking result value of subtracting a pair of values.
        /// </returns>
        delegate IAutoTrackMultipleDifference Factory(IAutoTrackValue value1, IAutoTrackValue value2);
    }
}