using System.ComponentModel;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Dropdowns;
using ReactiveUI;

namespace OpenTracker.Models.Dropdowns;

/// <summary>
/// This class contains dropdown data.
/// </summary>
public class Dropdown : ReactiveObject, IDropdown
{
    private readonly IRequirement _requirement;

    private readonly ICheckDropdown.Factory _checkDropdownFactory;
    private readonly IUncheckDropdown.Factory _uncheckDropdownFactory;

    public bool RequirementMet => _requirement.Met;

    private bool _checked;
    public bool Checked
    {
        get => _checked;
        set => this.RaiseAndSetIfChanged(ref _checked, value);
    }

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
        _requirement = requirement;
        _checkDropdownFactory = checkDropdownFactory;
        _uncheckDropdownFactory = uncheckDropdownFactory;

        _requirement.PropertyChanged += OnRequirementChanged;
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
        return new()
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

    /// <summary>
    /// Subscribes to the <see cref="IRequirement.PropertyChanged"/> event.
    /// </summary>
    /// <param name="sender">
    ///     The <see cref="object"/> from which the event is sent.
    /// </param>
    /// <param name="e">
    ///     The <see cref="PropertyChangedEventArgs"/>.
    /// </param>
    private void OnRequirementChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IRequirement.Met))
        {
            this.RaisePropertyChanged(nameof(RequirementMet));
        }
    }
}