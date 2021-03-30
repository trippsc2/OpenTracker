using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using ReactiveUI;

namespace OpenTracker.Models.Items
{
    /// <summary>
    ///     This interface contains item data.
    /// </summary>
    public interface IItem : IReactiveObject, ISaveable<ItemSaveData>
    {
        /// <summary>
        ///     A 32-bit signed integer representing the current item count.
        /// </summary>
        int Current { get; set; }
        
        /// <summary>
        ///     A factory for creating items.
        /// </summary>
        /// <param name="starting">
        ///     A 32-bit signed integer representing the starting value of the item.
        /// </param>
        /// <param name="autoTrackValue">
        ///     The auto-track value.
        /// </param>
        /// <returns>
        ///     A new item.
        /// </returns>
        delegate IItem Factory(int starting, IAutoTrackValue? autoTrackValue);

        /// <summary>
        ///     Creates a new undoable action to add an item.
        /// </summary>
        /// <returns>
        ///     A new undoable action to add an item.
        /// </returns>
        IUndoable CreateAddItemAction();

        /// <summary>
        ///     Returns whether an item can be added.
        /// </summary>
        /// <returns>
        ///     A boolean representing whether an item can be added.
        /// </returns>
        bool CanAdd();

        /// <summary>
        ///     Adds an item.
        /// </summary>
        void Add();

        /// <summary>
        ///     Creates a new undoable action to remove an item.
        /// </summary>
        /// <returns>
        ///     A new undoable action to remove an item.
        /// </returns>
        IUndoable CreateRemoveItemAction();
        
        /// <summary>
        ///     Returns whether an item can be removed.
        /// </summary>
        /// <returns>
        ///     A boolean representing whether an item can be removed.
        /// </returns>
        bool CanRemove();
        
        /// <summary>
        ///     Removes an item.
        /// </summary>
        void Remove();
        
        /// <summary>
        ///     Resets the item to its starting values.
        /// </summary>
        void Reset();
    }
}