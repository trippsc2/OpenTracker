using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.UndoRedo;

namespace OpenTracker.Models.Items
{
    /// <summary>
    ///     This interface contains item data with a maximum value.
    /// </summary>
    public interface ICappedItem : IItem
    {
        /// <summary>
        ///     A 32-bit signed integer representing the maximum value.
        /// </summary>
        int Maximum { get; }
        
        /// <summary>
        ///     A factory for creating new items with maximum values.
        /// </summary>
        /// <param name="starting">
        ///     A 32-bit signed integer representing the starting value of the item.
        /// </param>
        /// <param name="maximum">
        ///     A 32-bit signed integer representing the maximum value of the item.
        /// </param>
        /// <param name="autoTrackValue">
        ///     The auto-track value.
        /// </param>
        /// <returns>
        ///     A new item with a maximum value.
        /// </returns>
        new delegate ICappedItem Factory(int starting, int maximum, IAutoTrackValue? autoTrackValue);

        /// <summary>
        ///     Returns a new undoable action to cycle the item.
        /// </summary>
        /// <returns>
        ///     A new undoable action to cycle the item.
        /// </returns>
        IUndoable CreateCycleItemAction();

        /// <summary>
        ///     Cycles the item.
        /// </summary>
        /// <param name="reverse">
        ///     A boolean representing whether to cycle in reverse.
        /// </param>
        void Cycle(bool reverse = false);
    }
}