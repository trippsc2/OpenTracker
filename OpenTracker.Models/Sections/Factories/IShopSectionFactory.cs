using OpenTracker.Models.Locations;
using OpenTracker.Models.Sections.Boolean;

namespace OpenTracker.Models.Sections.Factories
{
    /// <summary>
    ///     This interface contains the creation logic for shop sections.
    /// </summary>
    public interface IShopSectionFactory
    {
        /// <summary>
        ///     Returns a new shop section for the specified location ID.
        /// </summary>
        /// <param name="id">
        ///     The location ID.
        /// </param>
        /// <returns>
        ///     A new shop section.
        /// </returns>
        IShopSection GetShopSection(LocationID id);
    }
}