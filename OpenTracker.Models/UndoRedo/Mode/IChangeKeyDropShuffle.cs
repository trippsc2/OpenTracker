namespace OpenTracker.Models.UndoRedo.Mode
{
    /// <summary>
    /// This interface contains undoable action data for changing the key drop shuffle mode setting.
    /// </summary>
    public interface IChangeKeyDropShuffle : IUndoable
    {
        delegate IChangeKeyDropShuffle Factory(bool newValue);
    }
}