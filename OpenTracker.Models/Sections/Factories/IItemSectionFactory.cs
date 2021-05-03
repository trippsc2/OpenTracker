using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Sections.Item;

namespace OpenTracker.Models.Sections.Factories
{
    /// <summary>
    ///     This interface contains the creation logic for item sections.
    /// </summary>
    public interface IItemSectionFactory
    {
        /// <summary>
        ///     Returns a new item section.
        /// </summary>
        /// <param name="autoTrackValue">
        ///     The auto-track value.
        /// </param>
        /// <param name="id">
        ///     The location ID.
        /// </param>
        /// <param name="index">
        ///     The section index.
        /// </param>
        /// <returns>
        ///     A new item section.
        /// </returns>
        IItemSection GetItemSection(IAutoTrackValue? autoTrackValue, LocationID id, int index = 0);
    }
}