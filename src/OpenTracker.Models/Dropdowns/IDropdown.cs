using System.ComponentModel;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Reset;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;

namespace OpenTracker.Models.Dropdowns;

/// <summary>
/// Represents a dropdown hole entrance in the UI panels, not the actual entrance.
/// </summary>
public interface IDropdown : INotifyPropertyChanged, IResettable, ISaveable<DropdownSaveData>
{
    /// <summary>
    /// A <see cref="IRequirement"/> representing the requirement for the dropdown to be visible.
    /// </summary>
    IRequirement Requirement { get; }
    /// <summary>
    /// A <see cref="bool"/> representing whether the dropdown is checked.
    /// </summary>
    bool Checked { get; set; }
        
    /// <summary>
    /// A factory for creating a new <see cref="IDropdown"/> objects.
    /// </summary>
    /// <param name="requirement">
    ///     The <see cref="IRequirement"/> for the dropdown to be relevant.
    /// </param>
    /// <returns>
    ///     A new <see cref="IDropdown"/> object.
    /// </returns>
    delegate IDropdown Factory(IRequirement requirement);

    /// <summary>
    /// Returns a new <see cref="IUndoable"/> to check the dropdown.
    /// </summary>
    /// <returns>
    ///     A new <see cref="IUndoable"/> to check the dropdown.
    /// </returns>
    IUndoable CreateCheckDropdownAction();

    /// <summary>
    /// Returns a new <see cref="IUndoable"/> to uncheck the dropdown.
    /// </summary>
    /// <returns>
    ///     A new <see cref="IUndoable"/> to uncheck the dropdown.
    /// </returns>
    IUndoable CreateUncheckDropdownAction();
}