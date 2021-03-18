using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.SaveLoad;
using ReactiveUI;

namespace OpenTracker.Models.Items
{
    /// <summary>
    /// This interface contains item data.
    /// </summary>
    public interface IItem : IReactiveObject, ISaveable<ItemSaveData>
    {
        int Current { get; set; }
        
        delegate IItem Factory(int starting, IAutoTrackValue? autoTrackValue);

        void Add();
        bool CanAdd();
        bool CanRemove();
        void Remove();
        void Reset();
    }
}