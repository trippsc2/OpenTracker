using System.Collections.Generic;

namespace OpenTracker.Models.AutoTracking.Values.Multiple
{
    /// <summary>
    ///     This interface contains the auto-tracking result value of an ordered priority list of results.
    /// </summary>
    public interface IAutoTrackMultipleOverride : IAutoTrackValue
    {
        /// <summary>
        /// A factory for creating new <see cref="IAutoTrackMultipleOverride"/> objects.
        /// </summary>
        /// <param name="values">
        ///     The <see cref="IList{T}"/> of <see cref="IAutoTrackValue"/>.
        /// </param>
        /// <returns>
        ///     A new <see cref="IAutoTrackMultipleOverride"/> object.
        /// </returns>
        delegate IAutoTrackMultipleOverride Factory(IList<IAutoTrackValue> values);
    }
}