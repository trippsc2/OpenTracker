using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This class contains undoable action data to change the compass shuffle setting.
    /// </summary>
    public class ChangeCompassShuffle : IUndoable
    {
        private readonly IMode _mode;
        private readonly bool _newValue;

        private bool _previousValue;

        public delegate ChangeCompassShuffle Factory(bool newValue);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode settings.
        /// </param>
        /// <param name="newValue">
        /// The new compass shuffle setting.
        /// </param>
        public ChangeCompassShuffle(IMode mode, bool newValue)
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
            _previousValue = _mode.CompassShuffle;
            _mode.CompassShuffle = _newValue;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void ExecuteUndo()
        {
            _mode.CompassShuffle = _previousValue;
        }
    }
}
