using OpenTracker.Models.AutoTracking.Values;

namespace OpenTracker.Models.Items
{
    public interface IItemAutoTrackValueFactory
    {
        IAutoTrackValue? GetAutoTrackValue(ItemType type);
    }
}