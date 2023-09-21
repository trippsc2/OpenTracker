using OpenTracker.Models.AutoTracking.Values;

namespace OpenTracker.Models.Items.Factories;

/// <summary>
/// This interface contains the creation logic for <see cref="ICappedItem"/> objects.
/// </summary>
public interface ICappedItemFactory
{
    /// <summary>
    /// Returns a new <see cref="ICappedItem"/> object.
    /// </summary>
    /// <param name="type">
    ///     The <see cref="ItemType"/>.
    /// </param>
    /// <param name="starting">
    ///     A <see cref="int"/> representing the starting value.
    /// </param>
    /// <param name="autoTrackValue">
    ///     The nullable <see cref="IAutoTrackValue"/> for the item.
    /// </param>
    /// <returns>
    ///     A new <see cref="ICappedItem"/> object.
    /// </returns>
    IItem GetItem(ItemType type, int starting, IAutoTrackValue? autoTrackValue);
}