using OpenTracker.Models.AutoTracking.Values;

namespace OpenTracker.Models.Items.Factories
{
    /// <summary>
    ///     This interface contains the creation logic for key items.
    /// </summary>
    public interface IKeyItemFactory
    {
        /// <summary>
        /// Returns a new key item.
        /// </summary>
        /// <param name="type">
        ///     The item type.
        /// </param>
        /// <param name="autoTrackValue">
        ///     The auto-track value.
        /// </param>
        /// <returns>
        ///     A new key item.
        /// </returns>
        IItem GetItem(ItemType type, IAutoTrackValue? autoTrackValue);
    }
}