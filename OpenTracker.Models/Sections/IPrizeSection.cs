using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.UndoRedo;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This interface contains boss/prize section data (end of each dungeon).
    /// </summary>
    public interface IPrizeSection : IBossSection
    {
        IPrizePlacement PrizePlacement { get; }

        /// <summary>
        /// Returns a new undoable action to toggle the prize.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether to ignore the logic.
        /// </param>
        /// <returns>
        /// An undoable action to toggle the prize.
        /// </returns>
        IUndoable CreateTogglePrizeSectionAction(bool force);
    }
}
