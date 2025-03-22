using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Items.Keys;

namespace OpenTracker.Models.Items.Factories
{
    /// <summary>
    /// This interface contains the creation logic for <see cref="ISmallKeyItem"/> and <see cref="IBigKeyItem"/>
    /// objects.
    /// </summary>
    public interface IKeyItemFactory
    {
        /// <summary>
        /// Returns a new <see cref="ISmallKeyItem"/> or <see cref="IBigKeyItem"/> object for the specified
        /// <see cref="ItemType"/>.
        /// </summary>
        /// <param name="type">
        ///     The <see cref="ItemType"/>.
        /// </param>
        /// <param name="autoTrackValue">
        ///     The nullable <see cref="IAutoTrackValue"/> for the item.
        /// </param>
        /// <returns>
        ///     A new <see cref="ISmallKeyItem"/> or <see cref="IBigKeyItem"/> object.
        /// </returns>
        IItem GetItem(ItemType type, IAutoTrackValue? autoTrackValue);
    }
}