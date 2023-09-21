using OpenTracker.Models.Dropdowns;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.UndoRedo.Dropdowns;

/// <summary>
/// This class contains the <see cref="IUndoable"/> action to check a <see cref="IDropdown"/>.
/// </summary>
[DependencyInjection]
public sealed class CheckDropdown : ICheckDropdown
{
    private readonly IDropdown _dropdown;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="dropdown">
    ///     The <see cref="IDropdown"/>.
    /// </param>
    public CheckDropdown(IDropdown dropdown)
    {
        _dropdown = dropdown;
    }

    public bool CanExecute()
    {
        return !_dropdown.Checked;
    }

    public void ExecuteDo()
    {
        _dropdown.Checked = true;
    }

    public void ExecuteUndo()
    {
        _dropdown.Checked = false;
    }
}