using OpenTracker.Models.Items;

namespace OpenTracker.Models.UndoRedo.Items
{
    /// <summary>
    /// This interface contains undoable action to remove an item.
    /// </summary>
    public interface IRemoveItem : IUndoable
    {
        delegate IRemoveItem Factory(IItem item);
    }
}