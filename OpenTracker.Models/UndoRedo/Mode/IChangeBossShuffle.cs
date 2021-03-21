namespace OpenTracker.Models.UndoRedo.Mode
{
    /// <summary>
    /// This interface contains undoable action to change the boss shuffle setting.
    /// </summary>
    public interface IChangeBossShuffle : IUndoable
    {
        delegate IChangeBossShuffle Factory(bool newValue);
    }
}