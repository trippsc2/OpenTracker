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

        /// <summary>
        /// Creates a new undoable action to add an item and sends it to the undo/redo manager.
        /// </summary>
        void AddItem();

        /// <summary>
        /// Creates a new undoable action to remove an item and sends it to the undo/redo manager.
        /// </summary>
        void RemoveItem();
    }
}