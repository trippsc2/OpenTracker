using OpenTracker.Models.Locations;
using OpenTracker.Models.Sections.Boolean;

namespace OpenTracker.Models.Sections.Factories
{
    /// <summary>
    ///     This interface contains the creation logic for take any sections.
    /// </summary>
    public interface ITakeAnySectionFactory
    {
        /// <summary>
        ///     Returns a new take any section for the specified location ID.
        /// </summary>
        /// <param name="id">
        ///     The location ID.
        /// </param>
        /// <returns>
        ///     A new take any section.
        /// </returns>
        ITakeAnySection GetTakeAnySection(LocationID id);
    }
}