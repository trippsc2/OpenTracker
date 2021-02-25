using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Locations;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This interface containing creation logic for section autotracking.
    /// </summary>
    public interface ISectionAutoTrackingFactory
    {
        IAutoTrackValue? GetAutoTrackValue(LocationID id, int sectionIndex);
    }
}