using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This is the class for an undoable action to change the small key shuffle setting.
    /// </summary>
    public class ChangeSmallKeyShuffle : IUndoable
    {
        private readonly bool _smallKeyShuffle;
        private bool _previousSmallKeyShuffle;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="smallKeyShuffle">
        /// The new small key shuffle setting.
        /// </param>
        public ChangeSmallKeyShuffle(bool smallKeyShuffle)
        {
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
            _previousSmallKeyShuffle = Mode.Instance.SmallKeyShuffle;
            Mode.Instance.SmallKeyShuffle = _smallKeyShuffle;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            Mode.Instance.SmallKeyShuffle = _previousSmallKeyShuffle;
        }
    }
}
