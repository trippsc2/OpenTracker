using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This class contains undoable action data to change the small key shuffle setting.
    /// </summary>
    public class ChangeSmallKeyShuffle : IUndoable
    {
        private readonly IMode _mode;
        private readonly bool _smallKeyShuffle;
        private bool _previousSmallKeyShuffle;

        public delegate ChangeSmallKeyShuffle Factory(bool smallKeyShuffle);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode settings.
        /// </param>
        /// <param name="smallKeyShuffle">
        /// The new small key shuffle setting.
        /// </param>
        public ChangeSmallKeyShuffle(IMode mode, bool smallKeyShuffle)
        {
            _mode = mode;
            _smallKeyShuffle = smallKeyShuffle;
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
            _previousSmallKeyShuffle = _mode.SmallKeyShuffle;
            _mode.SmallKeyShuffle = _smallKeyShuffle;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            _mode.SmallKeyShuffle = _previousSmallKeyShuffle;
        }
    }
}
