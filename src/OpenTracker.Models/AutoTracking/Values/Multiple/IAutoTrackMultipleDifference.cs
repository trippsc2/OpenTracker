namespace OpenTracker.Models.AutoTracking.Values.Multiple
{
    /// <summary>
    ///     This interface contains the auto-tracking result value data of subtracting a pair of values.
    /// </summary>
    public interface IAutoTrackMultipleDifference : IAutoTrackValue
    {
        /// <summary>
        /// A factory for creating new <see cref="IAutoTrackMultipleDifference"/> objects.
        /// </summary>
        /// <param name="value1">
        ///     The <see cref="IAutoTrackValue"/> from which to subtract.
        /// </param>
        /// <param name="value2">
        ///     The <see cref="IAutoTrackValue"/> to be subtracted.
        /// </param>
        /// <returns>
        ///     A new <see cref="IAutoTrackMultipleDifference"/> object.
        /// </returns>
        delegate IAutoTrackMultipleDifference Factory(IAutoTrackValue value1, IAutoTrackValue value2);
    }
}