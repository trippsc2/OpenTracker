using OpenTracker.Models.Sections;

namespace OpenTracker.Models.UndoRedo.Sections
{
    /// <summary>
    /// This interface contains undoable action to toggle a dungeon prize.
    /// </summary>
    public interface ITogglePrizeSection : IUndoable
    {
        delegate ITogglePrizeSection Factory(ISection section, bool force);
    }
}