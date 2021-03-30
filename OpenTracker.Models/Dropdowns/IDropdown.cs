using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using ReactiveUI;

namespace OpenTracker.Models.Dropdowns
{
    /// <summary>
    ///     This interface contains dropdown data.
    /// </summary>
    public interface IDropdown : IReactiveObject, ISaveable<DropdownSaveData>
    {
        /// <summary>
        ///     A boolean representing whether the dropdown is checked.
        /// </summary>
        bool Checked { get; set; }
        
        /// <summary>
        ///     A boolean representing whether the dropdown requirement is met.
        /// </summary>
        bool RequirementMet { get; }
        
        /// <summary>
        ///     A factory for creating a new dropdown.
        /// </summary>
        /// <param name="requirement">
        ///     The requirement for the dropdown to be relevant.
        /// </param>
        /// <returns>
        ///     A new dropdown.
        /// </returns>
        delegate IDropdown Factory(IRequirement requirement);

        /// <summary>
        ///     Returns a new undoable action to check the dropdown.
        /// </summary>
        /// <returns>
        ///     A new undoable action to check the dropdown.
        /// </returns>
        IUndoable CreateCheckDropdownAction();

        /// <summary>
        ///     Returns a new undoable action to uncheck the dropdown.
        /// </summary>
        /// <returns>
        ///     A new undoable action to uncheck the dropdown.
        /// </returns>
        IUndoable CreateUncheckDropdownAction();
        
        /// <summary>
        ///     Resets the dropdown.
        /// </summary>
        void Reset();
    }
}