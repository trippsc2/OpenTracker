using System.Collections.Generic;
using OpenTracker.Models.Locations;

namespace OpenTracker.Models.Sections.Factories
{
    /// <summary>
    ///     This interface contains the creation logic for dungeon sections.
    /// </summary>
    public interface IDungeonSectionFactory
    {
        /// <summary>
        ///     Returns a list of sections for the specified location.
        /// </summary>
        /// <param name="id">
        ///     The location ID.
        /// </param>
        /// <returns>
        ///     A list of sections.
        /// </returns>
        List<ISection> GetDungeonSections(LocationID id);
    }
}