using OpenTracker.Models.Locations;
using OpenTracker.Models.Sections.Boolean;

namespace OpenTracker.Models.Sections.Factories;

/// <summary>
/// This interface contains the creation logic for <see cref="IShopSection"/> objects.
/// </summary>
public interface IShopSectionFactory
{
    /// <summary>
    /// Returns a new <see cref="IShopSection"/> object for the specified <see cref="LocationID"/>.
    /// </summary>
    /// <param name="id">
    ///     The <see cref="LocationID"/>.
    /// </param>
    /// <returns>
    ///     A new <see cref="IShopSection"/> object.
    /// </returns>
    IShopSection GetShopSection(LocationID id);
}