namespace OpenTracker.Models.Interfaces
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
