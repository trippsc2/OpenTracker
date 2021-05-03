using OpenTracker.Models.Items;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using ReactiveUI;

namespace OpenTracker.Models.PrizePlacements
{
    /// <summary>
    ///     This interface contains prize placement data.
    /// </summary>
    public interface IPrizePlacement : IReactiveObject, ISaveable<PrizePlacementSaveData>
    {
        /// <summary>
        ///     The current prize item.
        /// </summary>
        IItem? Prize { get; }

        /// <summary>
        ///     A factory for creating new prize placements.
        /// </summary>
        /// <param name="startingPrize">
        ///     The starting prize item.
        /// </param>
        /// <returns>
        ///     A new prize placement.
        /// </returns>
        delegate IPrizePlacement Factory(IItem? startingPrize = null);

        /// <summary>
        ///     Returns whether the prize can be cycled.
        /// </summary>
        /// <returns>
        ///     A boolean representing whether the prize can be cycled.
        /// </returns>
        bool CanCycle();

        /// <summary>
        ///     Cycles the prize.
        /// </summary>
        /// <param name="reverse"></param>
        void Cycle(bool reverse = false);
        
        /// <summary>
        ///     Resets the prize placement to its starting values.
        /// </summary>
        void Reset();

        /// <summary>
        ///     Creates a new undoable action to change the prize.
        /// </summary>
        /// <returns>
        ///     A new undoable action.
        /// </returns>
        IUndoable CreateChangePrizeAction();
    }
}