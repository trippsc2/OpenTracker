using OpenTracker.Models.Locations;
using OpenTracker.Models.Sections.Entrance;

namespace OpenTracker.Models.Sections.Factories;

/// <summary>
/// This interface contains the creation logic for <see cref="IEntranceSection"/> objects.
/// </summary>
public interface IEntranceSectionFactory
{
    /// <summary>
    /// Returns a new <see cref="IEntranceSection"/> object for the specified <see cref="LocationID"/>.
    /// </summary>
    /// <param name="id">
    ///     The <see cref="LocationID"/>.
    /// </param>
    /// <returns>
    ///     A new <see cref="IEntranceSection"/> object.
    /// </returns>
    IEntranceSection GetEntranceSection(LocationID id);
}