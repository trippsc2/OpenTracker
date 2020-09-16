using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This is the class for the undoable action of changing the take any locations mode setting.
    /// </summary>
    public class ChangeTakeAnyLocations : IUndoable
    {
        private readonly bool _takeAnyLocations;
        private bool _previousTakeAnyLocations;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="takeAnyLocations">
        /// The new generic keys setting.
        /// </param>
        public ChangeTakeAnyLocations(bool takeAnyLocations)
        {
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
        public void Execute()
        {
            _previousTakeAnyLocations = Mode.Instance.TakeAnyLocations;
            Mode.Instance.TakeAnyLocations = _takeAnyLocations;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            Mode.Instance.TakeAnyLocations = _previousTakeAnyLocations;
        }
    }
}
