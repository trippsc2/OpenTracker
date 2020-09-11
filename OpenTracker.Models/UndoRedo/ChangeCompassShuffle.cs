using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This is the class for an undoable action to change the compass shuffle setting.
    /// </summary>
    public class ChangeCompassShuffle : IUndoable
    {
        private readonly bool _compassShuffle;
        private bool _previousCompassShuffle;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="compassShuffle">
        /// The new compass shuffle setting.
        /// </param>
        public ChangeCompassShuffle(bool compassShuffle)
        {
            _compassShuffle = compassShuffle;
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
            _previousCompassShuffle = Mode.Instance.CompassShuffle;
            Mode.Instance.CompassShuffle = _compassShuffle;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            Mode.Instance.CompassShuffle = _previousCompassShuffle;
        }
    }
}
