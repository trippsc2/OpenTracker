namespace OpenTracker.Models.UndoRedo.Actions
{
    /// <summary>
    /// This interface contains undoable action instance data.
    /// </summary>
    public interface IUndoableAction
    {
        void Execute();
    }
}