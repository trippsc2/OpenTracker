using OpenTracker.Models.AutoTracking.Values;

namespace OpenTracker.Models.Items
{
    public interface ICappedItem : IItem
    {
        int Maximum { get; }
        
        delegate ICappedItem Factory(int starting, int maximum, IAutoTrackValue? autoTrackValue);
    }
}