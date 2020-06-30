namespace OpenTracker.Models.Undoables
{
    /// <summary>
    /// This is the interface for undoable actions.
    /// </summary>
    public interface IUndoable
    {
        void Execute();
        void Undo();
    }
}
