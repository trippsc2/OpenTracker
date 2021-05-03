using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo.Mode
{
    /// <summary>
    /// This interface contains undoable action data to change the item placement setting.
    /// </summary>
    public interface IChangeItemPlacement : IUndoable
    {
        delegate IChangeItemPlacement Factory(ItemPlacement newValue);
    }
}