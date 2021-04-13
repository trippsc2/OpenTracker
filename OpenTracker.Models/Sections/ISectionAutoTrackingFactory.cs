using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Locations;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    ///     This interface containing creation logic for section auto-tracking.
    /// </summary>
    public interface ISectionAutoTrackingFactory
    {
        /// <summary>
        ///     Returns the auto-tracking value for the specified section.
        /// </summary>
        /// <param name="id">
        ///     The location ID of the section.
        /// </param>
        /// <param name="sectionIndex">
        ///     The index of the section.
        /// </param>
        /// <returns>
        ///     The auto-tracking value for the specified section.
        /// </returns>
        IAutoTrackValue? GetAutoTrackValue(LocationID id, int sectionIndex);
    }
}