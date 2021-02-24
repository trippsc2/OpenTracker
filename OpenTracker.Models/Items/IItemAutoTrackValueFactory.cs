using OpenTracker.Models.AutoTracking.Values;

namespace OpenTracker.Models.Items
{
    /// <summary>
    /// This interface contains creation logic for item autotrack values.
    /// </summary>
    public interface IItemAutoTrackValueFactory
    {
        IAutoTrackValue? GetAutoTrackValue(ItemType type);
    }
}