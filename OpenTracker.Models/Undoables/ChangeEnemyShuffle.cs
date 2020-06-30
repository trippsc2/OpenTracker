namespace OpenTracker.Models.Undoables
{
    /// <summary>
    /// This is the class for an undoable action to change the enemy shuffle
    /// setting.
    /// </summary>
    public class ChangeEnemyShuffle : IUndoable
    {
        private readonly Mode _mode;
        private readonly bool _enemyShuffle;
        private bool _previousEnemyShuffle;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The game mode data.
        /// </param>
        /// <param name="enemyShuffle">
        /// The new enemy shuffle setting.
        /// </param>
        public ChangeEnemyShuffle(Mode mode, bool enemyShuffle)
        {
            _mode = mode;
            _enemyShuffle = enemyShuffle;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void Execute()
        {
            _previousEnemyShuffle = _mode.EnemyShuffle;
            _mode.EnemyShuffle = _enemyShuffle;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            _mode.EnemyShuffle = _previousEnemyShuffle;
        }
    }
}
