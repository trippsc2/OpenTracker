using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This is the class for an undoable action to change the boss shuffle
    /// setting.
    /// </summary>
    public class ChangeBossShuffle : IUndoable
    {
        private readonly bool _bossShuffle;
        private bool _previousBossShuffle;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The game mode data.
        /// </param>
        /// <param name="bossShuffle">
        /// A boolean representing the new boss shuffle setting.
        /// </param>
        public ChangeBossShuffle(bool bossShuffle)
        {
            _bossShuffle = bossShuffle;
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
            _previousBossShuffle = Mode.Instance.BossShuffle;
            Mode.Instance.BossShuffle = _bossShuffle;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            Mode.Instance.BossShuffle = _previousBossShuffle;
        }
    }
}
