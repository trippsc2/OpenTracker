using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This class contains undoable action to change the boss shuffle setting.
    /// </summary>
    public class ChangeBossShuffle : IUndoable
    {
        private readonly IMode _mode;

        private readonly bool _bossShuffle;

        private bool _previousBossShuffle;

        public delegate ChangeBossShuffle Factory(bool bossShuffle);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The game mode data.
        /// </param>
        /// <param name="bossShuffle">
        /// A boolean representing the new boss shuffle setting.
        /// </param>
        public ChangeBossShuffle(IMode mode, bool bossShuffle)
        {
            _mode = mode;
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
        public void ExecuteDo()
        {
            _previousBossShuffle = _mode.BossShuffle;
            _mode.BossShuffle = _bossShuffle;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void ExecuteUndo()
        {
            _mode.BossShuffle = _previousBossShuffle;
        }
    }
}
