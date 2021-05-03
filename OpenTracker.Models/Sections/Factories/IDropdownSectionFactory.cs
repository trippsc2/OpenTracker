using OpenTracker.Models.Locations;
using OpenTracker.Models.Sections.Entrance;

namespace OpenTracker.Models.Sections.Factories
{
    /// <summary>
    ///     This interface contains the creation logic for dropdown sections.
    /// </summary>
    public interface IDropdownSectionFactory
    {
        /// <summary>
        ///     Returns a new dropdown section for the specified location ID.
        /// </summary>
        /// <param name="id">
        ///     The location ID.
        /// </param>
        /// <returns>
        ///     A new dropdown section.
        /// </returns>
        IDropdownSection GetDropdownSection(LocationID id);
    }
}