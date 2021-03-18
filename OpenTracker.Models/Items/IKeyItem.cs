using OpenTracker.Models.AutoTracking.Values;

namespace OpenTracker.Models.Items
{
    public interface IKeyItem : ICappedItem
    {
        int EffectiveCurrent { get; }
        
        delegate IKeyItem Factory(
            IItem genericKey, int nonKeyDropMaximum, int keyDropMaximum, IAutoTrackValue? autoTrackValue);
    }
}