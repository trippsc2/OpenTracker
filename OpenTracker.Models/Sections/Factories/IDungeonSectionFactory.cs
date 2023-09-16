using System.Collections.Generic;
using OpenTracker.Models.Locations;

namespace OpenTracker.Models.Sections.Factories;

/// <summary>
/// This interface contains the creation logic for dungeon sections.
/// </summary>
public interface IDungeonSectionFactory
{
    /// <summary>
    /// Returns a <see cref="List{T}"/> of <see cref="ISection"/> for the specified <see cref="LocationID"/>.
    /// </summary>
    /// <param name="id">
    ///     The <see cref="LocationID"/>.
    /// </param>
    /// <returns>
    ///     A <see cref="List{T}"/> of <see cref="ISection"/>.
    /// </returns>
    List<ISection> GetDungeonSections(LocationID id);
}