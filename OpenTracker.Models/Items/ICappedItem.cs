using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.UndoRedo;

namespace OpenTracker.Models.Items
{
    public interface ICappedItem : IItem
    {
        int Maximum { get; }
        
        delegate ICappedItem Factory(int starting, int maximum, IAutoTrackValue? autoTrackValue);

        /// <summary>
        /// Cycles the item.
        /// </summary>
        /// <param name="reverse">
        /// A boolean representing whether to cycle in reverse.
        /// </param>
        void Cycle(bool reverse = false);

        /// <summary>
        /// Returns a new undoable action to cycle the item.
        /// </summary>
        /// <returns>
        /// A new undoable action to cycle the item.
        /// </returns>
        IUndoable CreateCycleItemAction();
    }
}