using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This class contains undoable action data for changing the key drop shuffle mode setting.
    /// </summary>
    public class ChangeKeyDropShuffle : IUndoable
    {
        private readonly IMode _mode;
        private readonly bool _keyDropShuffle;
        private bool _previousKeyDropShuffle;

        public delegate ChangeKeyDropShuffle Factory(bool keyDropShuffle);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode settings.
        /// </param>
        /// <param name="keyDropShuffle">
        /// The new key drp shuffle setting.
        /// </param>
        public ChangeKeyDropShuffle(IMode mode, bool keyDropShuffle)
        {
            _mode = mode;
            _keyDropShuffle = keyDropShuffle;
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
            _previousKeyDropShuffle = _mode.KeyDropShuffle;
            _mode.KeyDropShuffle = _keyDropShuffle;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void ExecuteUndo()
        {
            _mode.KeyDropShuffle = _previousKeyDropShuffle;
        }
    }
}
