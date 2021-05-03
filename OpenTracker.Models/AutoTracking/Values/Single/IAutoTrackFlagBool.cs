using OpenTracker.Models.AutoTracking.Memory;

namespace OpenTracker.Models.AutoTracking.Values.Single
{
    /// <summary>
    ///     This interface contains the auto-tracking result value of a memory flag.
    /// </summary>
    public interface IAutoTrackFlagBool : IAutoTrackValue
    {
        /// <summary>
        ///     A factory for creating auto-tracking result values of memory flags.
        /// </summary>
        /// <param name="flag">
        ///     The memory flag to be checked.
        /// </param>
        /// <param name="trueValue">
        ///     A 32-bit signed integer representing the resultant value, if the flag is set.
        /// </param>
        /// <returns>
        ///     An auto-tracking result value of a memory flag.
        /// </returns>
        delegate IAutoTrackFlagBool Factory(IMemoryFlag flag, int trueValue);
    }
}