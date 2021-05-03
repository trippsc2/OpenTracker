using OpenTracker.Models.Items;

namespace OpenTracker.Models.AutoTracking.Values.Single
{
    /// <summary>
    ///     This interface contains the auto-tracking result value of a hidden item count.
    /// </summary>
    public interface IAutoTrackItemValue : IAutoTrackValue
    {
        /// <summary>
        ///     A factory for creating new auto-tracking result values of hidden item counts.
        /// </summary>
        /// <param name="item">
        ///     The item for which the count is represented.
        /// </param>
        /// <returns>
        ///     A new auto-tracking result value of a hidden item count.
        /// </returns>
        delegate IAutoTrackItemValue Factory(IItem item);
    }
}