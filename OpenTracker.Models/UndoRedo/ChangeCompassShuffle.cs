using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This class contains undoable action data to change the compass shuffle setting.
    /// </summary>
    public class ChangeCompassShuffle : IUndoable
    {
        private readonly IMode _mode;
        private readonly bool _compassShuffle;

        private bool _previousCompassShuffle;

        public delegate ChangeCompassShuffle Factory(bool compassShuffle);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode settings.
        /// </param>
        /// <param name="compassShuffle">
        /// The new compass shuffle setting.
        /// </param>
        public ChangeCompassShuffle(IMode mode, bool compassShuffle)
        {
            _mode = mode;
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
            _previousCompassShuffle = _mode.CompassShuffle;
            _mode.CompassShuffle = _compassShuffle;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            _mode.CompassShuffle = _previousCompassShuffle;
        }
    }
}
