using OpenTracker.Models.Dropdowns;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This class contains undoable action data to uncheck a dropdown.
    /// </summary>
    public class UncheckDropdown : IUndoable
    {
        private readonly IDropdown _dropdown;

        public delegate UncheckDropdown Factory(IDropdown dropdown);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dropdown">
        /// The dropdown to be checked.
        /// </param>
        public UncheckDropdown(IDropdown dropdown)
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
            return _dropdown.Checked;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void ExecuteDo()
        {
            _dropdown.Checked = false;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void ExecuteUndo()
        {
            _dropdown.Checked = true;
        }
    }
}
