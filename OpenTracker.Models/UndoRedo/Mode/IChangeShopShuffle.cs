namespace OpenTracker.Models.UndoRedo.Mode
{
    /// <summary>
    /// This interface contains undoable action data to change the shop shuffle mode setting.
    /// </summary>
    public interface IChangeShopShuffle : IUndoable
    {
        delegate IChangeShopShuffle Factory(bool newValue);
    }
}