namespace OpenTracker.Models.UndoRedo.Mode
{
    /// <summary>
    /// This interface contains undoable action data to change the small key shuffle setting.
    /// </summary>
    public interface IChangeSmallKeyShuffle : IUndoable
    {
        delegate IChangeSmallKeyShuffle Factory(bool newValue);
    }
}