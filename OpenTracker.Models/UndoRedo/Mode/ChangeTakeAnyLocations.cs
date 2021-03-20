using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo.Mode
{
    /// <summary>
    /// This class contains undoable action data to change the take any locations mode setting.
    /// </summary>
    public class ChangeTakeAnyLocations : IUndoable
    {
        private readonly IMode _mode;
        private readonly bool _newValue;
        private bool _previousValue;

        public delegate ChangeTakeAnyLocations Factory(bool newValue);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode settings.
        /// </param>
        /// <param name="newValue">
        /// The new generic keys setting.
        /// </param>
        public ChangeTakeAnyLocations(IMode mode, bool newValue)
        {
            _mode = mode;
            _newValue = newValue;
        }

        /// <summary>
        /// Returns whether the action can be executed.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the action can be executed.
        /// </returns>
        public bool CanExecute()
        {
            return true;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void ExecuteDo()
        {
            _previousValue = _mode.TakeAnyLocations;
            _mode.TakeAnyLocations = _newValue;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void ExecuteUndo()
        {
            _mode.TakeAnyLocations = _previousValue;
        }
    }
}
