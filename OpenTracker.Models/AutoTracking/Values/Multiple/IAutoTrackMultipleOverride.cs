using System.Collections.Generic;

namespace OpenTracker.Models.AutoTracking.Values.Multiple
{
    /// <summary>
    ///     This interface contains the auto-tracking result value of an ordered priority list of results.
    /// </summary>
    public interface IAutoTrackMultipleOverride : IAutoTrackValue
    {
        /// <summary>
        ///     A factory for creating auto-tracking result values of an ordered priority list of results.
        /// </summary>
        /// <param name="values">
        ///     The list of auto-tracking result values.
        /// </param>
        /// <returns>
        ///     A new auto-tracking result value of an ordered priority list of results.
        /// </returns>
        delegate IAutoTrackMultipleOverride Factory(IList<IAutoTrackValue> values);
    }
}