using System.Collections.Generic;
using OpenTracker.Models.Locations;

namespace OpenTracker.Models.Sections.Factories
{
    /// <summary>
    ///     This interface contains creation logic for section data.
    /// </summary>
    public interface ISectionFactory
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
        List<ISection> GetSections(LocationID id);
    }
}