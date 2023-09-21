using OpenTracker.Models.AutoTracking.Values;

namespace OpenTracker.Models.Items.Factories;

/// <summary>
/// This interface contains creation logic for item <see cref="IAutoTrackValue"/> objects.
/// </summary>
public interface IItemAutoTrackValueFactory
{
    /// <summary>
    /// A factory for creating the <see cref="IItemAutoTrackValueFactory"/> object.
    /// </summary>
    /// <returns>
    ///     The <see cref="IItemAutoTrackValueFactory"/> object.
    /// </returns>
    delegate IItemAutoTrackValueFactory Factory();

    /// <summary>
    /// Returns the <see cref="IAutoTrackValue"/> for the specified <see cref="ItemType"/>.
    /// </summary>
    /// <param name="type">
    ///     The <see cref="ItemType"/>.
    /// </param>
    /// <returns>
    ///     The nullable <see cref="IAutoTrackValue"/> for the item.
    /// </returns>
    IAutoTrackValue? GetAutoTrackValue(ItemType type);
}