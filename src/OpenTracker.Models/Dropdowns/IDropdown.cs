using OpenTracker.Models.Requirements;
using OpenTracker.Models.Reset;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using ReactiveUI;

namespace OpenTracker.Models.Dropdowns
{
    /// <summary>
    /// This interface contains dropdown data.
    /// </summary>
    public interface IDropdown : IReactiveObject, IResettable, ISaveable<DropdownSaveData>
    {
        /// <summary>
        /// A <see cref="bool"/> representing whether the dropdown is checked.
        /// </summary>
        bool Checked { get; set; }
        
        /// <summary>
        /// A <see cref="bool"/> representing whether the dropdown requirement is met.
        /// </summary>
        bool RequirementMet { get; }
        
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
}