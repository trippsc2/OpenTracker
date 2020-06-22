using OpenTracker.Models.Interfaces;

namespace OpenTracker.Models.Actions
{
    /// <summary>
    /// This is the class for an undoable action to change the boss shuffle
    /// setting.
    /// </summary>
    public class ChangeBossShuffle : IUndoable
    {
        private readonly Mode _mode;
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
        public ChangeBossShuffle(Mode mode, bool bossShuffle)
        {
            _mode = mode;
            _bossShuffle = bossShuffle;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void Execute()
        {
            _previousBossShuffle = _mode.BossShuffle;
            _mode.BossShuffle = _bossShuffle;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            _mode.BossShuffle = _previousBossShuffle;
        }
    }
}
