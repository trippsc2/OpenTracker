using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Dropdowns;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Models.Dropdowns;

public sealed class Dropdown : ReactiveObject, IDropdown
{

    private readonly ICheckDropdown.Factory _checkDropdownFactory;
    private readonly IUncheckDropdown.Factory _uncheckDropdownFactory;

    public IRequirement Requirement { get; }
    [Reactive]
    public bool Checked { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="checkDropdownFactory">
    ///     An Autofac factory for creating new <see cref="ICheckDropdown"/> objects.
    /// </param>
    /// <param name="uncheckDropdownFactory">
    ///     An Autofac factory for creating new <see cref="IUncheckDropdown"/> objects.
    /// </param>
    /// <param name="requirement">
    ///     The <see cref="IRequirement"/> for the dropdown to be relevant.
    /// </param>
    public Dropdown(
        ICheckDropdown.Factory checkDropdownFactory, IUncheckDropdown.Factory uncheckDropdownFactory,
        IRequirement requirement)
    {
        _checkDropdownFactory = checkDropdownFactory;
        _uncheckDropdownFactory = uncheckDropdownFactory;
        
        Requirement = requirement;
    }

    public IUndoable CreateCheckDropdownAction()
    {
        return _checkDropdownFactory(this);
    }

    public IUndoable CreateUncheckDropdownAction()
    {
        return _uncheckDropdownFactory(this);
    }

    public void Reset()
    {
        Checked = false;
    }

    public DropdownSaveData Save()
    {
        return new DropdownSaveData
        {
            Checked = Checked
        };
    }

    public void Load(DropdownSaveData? saveData)
    {
        if (saveData == null)
        {
            return;
        }

        Checked = saveData.Checked;
    }
}