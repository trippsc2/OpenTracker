using System.Collections.Generic;

namespace OpenTracker.Models.AutoTracking.Values.Multiple
{
    /// <summary>
    /// This interface contains the auto-tracking result value of a list of results to be summed.
    /// </summary>
    public interface IAutoTrackMultipleSum : IAutoTrackValue
    {
        /// <summary>
        /// A factory for creating new <see cref="IAutoTrackMultipleSum"/> objects.
        /// </summary>
        /// <param name="values">
        ///     The <see cref="IList{T}"/> of <see cref="IAutoTrackValue"/>.
        /// </param>
        /// <returns>
        ///     A new <see cref="IAutoTrackMultipleSum"/> object.
        /// </returns>
        delegate IAutoTrackMultipleSum Factory(IList<IAutoTrackValue> values);
    }
}