using OpenTracker.Models.Items;

namespace OpenTracker.Models.AutoTracking.Values.Single
{
    /// <summary>
    /// This interface contains the auto-tracking result value of a hidden item count.
    /// </summary>
    public interface IAutoTrackItemValue : IAutoTrackValue
    {
        /// <summary>
        /// A factory for creating new <see cref="IAutoTrackItemValue"/> objects.
        /// </summary>
        /// <param name="item">
        ///     The <see cref="IItem"/> for which the count is represented.
        /// </param>
        /// <returns>
        ///     A new <see cref="IAutoTrackItemValue"/> object.
        /// </returns>
        delegate IAutoTrackItemValue Factory(IItem item);
    }
}