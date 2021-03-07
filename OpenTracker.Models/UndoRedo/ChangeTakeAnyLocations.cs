using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This class contains undoable action data to change the take any locations mode setting.
    /// </summary>
    public class ChangeTakeAnyLocations : IUndoable
    {
        private readonly IMode _mode;
        private readonly bool _takeAnyLocations;
        private bool _previousTakeAnyLocations;

        public delegate ChangeTakeAnyLocations Factory(bool takeAnyLocations);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode settings.
        /// </param>
        /// <param name="takeAnyLocations">
        /// The new generic keys setting.
        /// </param>
        public ChangeTakeAnyLocations(IMode mode, bool takeAnyLocations)
        {
            _mode = mode;
            _takeAnyLocations = takeAnyLocations;
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
            _previousTakeAnyLocations = _mode.TakeAnyLocations;
            _mode.TakeAnyLocations = _takeAnyLocations;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void ExecuteUndo()
        {
            _mode.TakeAnyLocations = _previousTakeAnyLocations;
        }
    }
}
