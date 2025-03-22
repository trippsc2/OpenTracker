using OpenTracker.Models.Locations;
using OpenTracker.Models.Sections.Entrance;

namespace OpenTracker.Models.Sections.Factories
{
    /// <summary>
    /// This interface contains the creation logic for <see cref="IDropdownSection"/> objects.
    /// </summary>
    public interface IDropdownSectionFactory
    {
        /// <summary>
        /// Returns a new <see cref="IDropdownSection"/> object for the specified <see cref="LocationID"/>.
        /// </summary>
        /// <param name="id">
        ///     The <see cref="LocationID"/>.
        /// </param>
        /// <returns>
        ///     A new <see cref="IDropdownSection"/> object.
        /// </returns>
        IDropdownSection GetDropdownSection(LocationID id);
    }
}