namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This is the interface for undoable actions.
    /// </summary>
    public interface IUndoable
    {
        bool CanExecute();
        void Execute();
        void Undo();
    }
}
