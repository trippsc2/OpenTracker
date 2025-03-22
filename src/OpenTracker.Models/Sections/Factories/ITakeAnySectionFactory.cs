using OpenTracker.Models.Locations;
using OpenTracker.Models.Sections.Boolean;

namespace OpenTracker.Models.Sections.Factories
{
    /// <summary>
    /// This interface contains the creation logic for <see cref="ITakeAnySection"/> objects.
    /// </summary>
    public interface ITakeAnySectionFactory
    {
        /// <summary>
        /// Returns a new <see cref="ITakeAnySection"/> object for the specified <see cref="LocationID"/>.
        /// </summary>
        /// <param name="id">
        ///     The <see cref="LocationID"/>.
        /// </param>
        /// <returns>
        ///     A new <see cref="ITakeAnySection"/> object.
        /// </returns>
        ITakeAnySection GetTakeAnySection(LocationID id);
    }
}