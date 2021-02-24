using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Locations;

namespace OpenTracker.Models.Sections
{
    public interface ISectionAutoTrackingFactory
    {
        IAutoTrackValue? GetAutoTrackValue(LocationID id, int sectionIndex);
    }
}