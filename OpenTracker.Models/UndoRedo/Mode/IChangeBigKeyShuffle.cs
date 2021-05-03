namespace OpenTracker.Models.UndoRedo.Mode
{
    /// <summary>
    /// This interface contains undoable action data to change the big key shuffle setting.
    /// </summary>
    public interface IChangeBigKeyShuffle : IUndoable
    {
        delegate IChangeBigKeyShuffle Factory(bool newValue);
    }
}