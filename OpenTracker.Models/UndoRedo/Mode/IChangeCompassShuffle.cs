namespace OpenTracker.Models.UndoRedo.Mode
{
    /// <summary>
    /// This interface contains undoable action data to change the compass shuffle setting.
    /// </summary>
    public interface IChangeCompassShuffle : IUndoable
    {
        delegate IChangeCompassShuffle Factory(bool newValue);
    }
}