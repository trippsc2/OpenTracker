using OpenTracker.Models.Dropdowns;

namespace OpenTracker.Models.UndoRedo.Dropdowns
{
    /// <summary>
    /// This interface contains undoable action data to uncheck a dropdown.
    /// </summary>
    public interface IUncheckDropdown : IUndoable
    {
        delegate IUncheckDropdown Factory(IDropdown dropdown);
    }
}