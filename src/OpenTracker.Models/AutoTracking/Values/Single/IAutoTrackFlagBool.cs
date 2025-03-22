using OpenTracker.Models.AutoTracking.Memory;

namespace OpenTracker.Models.AutoTracking.Values.Single
{
    /// <summary>
    /// This interface contains the auto-tracking result value of a memory flag.
    /// </summary>
    public interface IAutoTrackFlagBool : IAutoTrackValue
    {
        /// <summary>
        /// A factory for creating new <see cref="IAutoTrackFlagBool"/> objects.
        /// </summary>
        /// <param name="flag">
        ///     The <see cref="IMemoryFlag"/> to be checked.
        /// </param>
        /// <param name="trueValue">
        ///     A <see cref="int"/> representing the resultant value, if the flag is set.
        /// </param>
        /// <returns>
        ///     A new <see cref="IAutoTrackFlagBool"/> object.
        /// </returns>
        delegate IAutoTrackFlagBool Factory(IMemoryFlag flag, int trueValue);
    }
}