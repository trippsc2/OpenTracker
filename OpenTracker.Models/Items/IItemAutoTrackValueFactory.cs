using OpenTracker.Models.AutoTracking.Values;

namespace OpenTracker.Models.Items
{
    /// <summary>
    /// This interface contains creation logic for item auto-track values.
    /// </summary>
    public interface IItemAutoTrackValueFactory
    {
        delegate IItemAutoTrackValueFactory Factory();

        IAutoTrackValue? GetAutoTrackValue(ItemType type);
    }
}