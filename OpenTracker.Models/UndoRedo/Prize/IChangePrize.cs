using OpenTracker.Models.PrizePlacements;

namespace OpenTracker.Models.UndoRedo.Prize
{
    /// <summary>
    /// This interface contains undoable action data to change the dungeon prize.
    /// </summary>
    public interface IChangePrize : IUndoable
    {
        delegate IChangePrize Factory(IPrizePlacement prizePlacement);
    }
}