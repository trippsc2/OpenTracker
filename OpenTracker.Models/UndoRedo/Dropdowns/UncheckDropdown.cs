using OpenTracker.Models.Dropdowns;

namespace OpenTracker.Models.UndoRedo.Dropdowns;

/// <summary>
/// This class contains the <see cref="IUndoable"/> action to uncheck a <see cref="IDropdown"/>.
/// </summary>
public class UncheckDropdown : IUncheckDropdown
{
    private readonly IDropdown _dropdown;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="dropdown">
    ///     The <see cref="IDropdown"/>.
    /// </param>
    public UncheckDropdown(IDropdown dropdown)
    {
        _dropdown = dropdown;
    }

    public bool CanExecute()
    {
        return _dropdown.Checked;
    }

    public void ExecuteDo()
    {
        _dropdown.Checked = false;
    }

    public void ExecuteUndo()
    {
        _dropdown.Checked = true;
    }
}