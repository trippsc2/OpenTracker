using OpenTracker.Models.Locations;
using OpenTracker.Models.Sections.Entrance;

namespace OpenTracker.Models.Sections.Factories
{
    /// <summary>
    ///     This interface contains the creation logic for entrance sections.
    /// </summary>
    public interface IEntranceSectionFactory
    {
        /// <summary>
        ///     Returns a new entrance section for the specified location ID.
        /// </summary>
        /// <param name="id">
        ///     The location ID.
        /// </param>
        /// <returns>
        ///     A new entrance section.
        /// </returns>
        IEntranceSection GetEntranceSection(LocationID id);
    }
}