using OpenTracker.Models.AutoTracking.Values;

namespace OpenTracker.Models.Items.Factories
{
    /// <summary>
    ///     This interface contains creation logic for item auto-track values.
    /// </summary>
    public interface IItemAutoTrackValueFactory
    {
        /// <summary>
        ///     A factory for creating the item auto-track value factory.
        /// </summary>
        /// <returns>
        ///     The item auto-track value factory.
        /// </returns>
        delegate IItemAutoTrackValueFactory Factory();

        /// <summary>
        ///     Returns the auto-tracking value for the specified item.
        /// </summary>
        /// <param name="type">
        ///     The item type.
        /// </param>
        /// <returns>
        ///     The auto-tracking value for the specified item.
        /// </returns>
        IAutoTrackValue? GetAutoTrackValue(ItemType type);
    }
}