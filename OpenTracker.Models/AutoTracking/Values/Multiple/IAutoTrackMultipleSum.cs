using System.Collections.Generic;

namespace OpenTracker.Models.AutoTracking.Values.Multiple
{
    /// <summary>
    ///     This interface contains the auto-tracking result value of a list of results to be summed.
    /// </summary>
    public interface IAutoTrackMultipleSum : IAutoTrackValue
    {
        /// <summary>
        ///     A factory for creating auto-tracking result values of a list of results to be summed.
        /// </summary>
        /// <param name="values">
        ///     The list of auto-tracking result values.
        /// </param>
        /// <returns>
        ///     A new auto-tracking result value of a list of results to be summed.
        /// </returns>
        delegate IAutoTrackMultipleSum Factory(List<IAutoTrackValue> values);
    }
}