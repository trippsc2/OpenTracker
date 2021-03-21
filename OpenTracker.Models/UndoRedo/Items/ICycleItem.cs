using OpenTracker.Models.Items;

namespace OpenTracker.Models.UndoRedo.Items
{
    /// <summary>
    /// This interface contains undoable action data to cycle an item through valid counts.
    /// </summary>
    public interface ICycleItem : IUndoable
    {
        delegate ICycleItem Factory(IItem item);
    }
}