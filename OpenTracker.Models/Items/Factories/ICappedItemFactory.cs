using OpenTracker.Models.AutoTracking.Values;

namespace OpenTracker.Models.Items.Factories
{
    /// <summary>
    ///     This interface contains the creation logic for items with maximum values.
    /// </summary>
    public interface ICappedItemFactory
    {
        /// <summary>
        ///     Returns a new item.
        /// </summary>
        /// <param name="type">
        ///     The item type.
        /// </param>
        /// <param name="starting">
        ///     A 32-bit signed integer representing the starting value.
        /// </param>
        /// <param name="autoTrackValue">
        ///     The auto-track value for the item.
        /// </param>
        /// <returns>
        ///     A new item.
        /// </returns>
        IItem GetItem(ItemType type, int starting, IAutoTrackValue? autoTrackValue);
    }
}