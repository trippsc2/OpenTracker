namespace OpenTracker.Models.UndoRedo.Mode
{
    /// <summary>
    /// This interface contains undoable action data to change the map shuffle setting.
    /// </summary>
    public interface IChangeMapShuffle : IUndoable
    {
        delegate IChangeMapShuffle Factory(bool newValue);
    }
}