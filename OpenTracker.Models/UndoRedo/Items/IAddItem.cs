using OpenTracker.Models.Items;

namespace OpenTracker.Models.UndoRedo.Items
{
    /// <summary>
    /// This interface contains undoable action data to add an item.
    /// </summary>
    public interface IAddItem : IUndoable
    {
        delegate IAddItem Factory(IItem item);
    }
}