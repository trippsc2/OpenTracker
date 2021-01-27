using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This is the class for the undoable action of changing the key drop shuffle mode setting.
    /// </summary>
    public class ChangeKeyDropShuffle : IUndoable
    {
        private readonly bool _keyDropShuffle;
        private bool _previousKeyDropShuffle;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="keyDropShuffle">
        /// The new key drp shuffle setting.
        /// </param>
        public ChangeKeyDropShuffle(bool keyDropShuffle)
        {
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
        public void Execute()
        {
            _previousKeyDropShuffle = Mode.Instance.KeyDropShuffle;
            Mode.Instance.KeyDropShuffle = _keyDropShuffle;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            Mode.Instance.KeyDropShuffle = _previousKeyDropShuffle;
        }
    }
}
