using OpenTracker.Models.Dropdowns;

namespace OpenTracker.Models.UndoRedo.Dropdowns
{
    /// <summary>
    /// This interface contains undoable action data to check a dropdown.
    /// </summary>
    public interface ICheckDropdown : IUndoable
    {
        delegate ICheckDropdown Factory(IDropdown dropdown);
    }
}