using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Sections.Item;

namespace OpenTracker.Models.Sections.Factories;

/// <summary>
/// This interface contains the creation logic for <see cref="IItemSection"/> objects.
/// </summary>
public interface IItemSectionFactory
{
    /// <summary>
    /// Returns a new <see cref="IItemSection"/> object.
    /// </summary>
    /// <param name="autoTrackValue">
    ///     The nullable <see cref="IAutoTrackValue"/>.
    /// </param>
    /// <param name="id">
    ///     The <see cref="LocationID"/>.
    /// </param>
    /// <param name="index">
    ///     A <see cref="int"/> representing the section index.
    /// </param>
    /// <returns>
    ///     A new <see cref="IItemSection"/> object.
    /// </returns>
    IItemSection GetItemSection(IAutoTrackValue? autoTrackValue, LocationID id, int index = 0);
}