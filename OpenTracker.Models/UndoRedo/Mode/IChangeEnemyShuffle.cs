namespace OpenTracker.Models.UndoRedo.Mode
{
    /// <summary>
    /// This interface contains undoable action data to change the enemy shuffle setting.
    /// </summary>
    public interface IChangeEnemyShuffle : IUndoable
    {
        delegate IChangeEnemyShuffle Factory(bool newValue);
    }
}