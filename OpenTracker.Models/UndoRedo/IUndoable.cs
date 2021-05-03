namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This interface contains undoable action data.
    /// </summary>
    public interface IUndoable
    {
        bool CanExecute();
        void ExecuteDo();
        void ExecuteUndo();
    }
}
