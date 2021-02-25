using OpenTracker.Models.Dropdowns;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This class contains undoable action data to check a dropdown.
    /// </summary>
    public class CheckDropdown : IUndoable
    {
        private readonly IDropdown _dropdown;

        public delegate CheckDropdown Factory(IDropdown dropdown);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dropdown">
        /// The dropdown to be checked.
        /// </param>
        public CheckDropdown(IDropdown dropdown)
        {
            _dropdown = dropdown;
        }

        /// <summary>
        /// Returns whether the action can be executed.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the action can be executed.
        /// </returns>
        public bool CanExecute()
        {
            return !_dropdown.Checked;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void Execute()
        {
            _dropdown.Checked = true;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            _dropdown.Checked = false;
        }
    }
}
