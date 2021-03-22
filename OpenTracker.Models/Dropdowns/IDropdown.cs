using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using ReactiveUI;

namespace OpenTracker.Models.Dropdowns
{
    /// <summary>
    /// This interface contains dropdown data.
    /// </summary>
    public interface IDropdown : IReactiveObject, ISaveable<DropdownSaveData>
    {
        bool Checked { get; set; }
        bool RequirementMet { get; }

        void Reset();

        delegate IDropdown Factory(IRequirement requirement);

        /// <summary>
        /// Returns a new undoable action to check the dropdown.
        /// </summary>
        /// <returns>
        /// A new undoable action to check the dropdown.
        /// </returns>
        IUndoable CreateCheckDropdownAction();

        /// <summary>
        /// Returns a new undoable action to uncheck the dropdown.
        /// </summary>
        /// <returns>
        /// A new undoable action to uncheck the dropdown.
        /// </returns>
        IUndoable CreateUncheckDropdownAction();
    }
}