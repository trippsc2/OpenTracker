using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This class contains undoable action data to change the enemy shuffle setting.
    /// </summary>
    public class ChangeEnemyShuffle : IUndoable
    {
        private readonly IMode _mode;
        private readonly bool _enemyShuffle;

        private bool _previousEnemyShuffle;

        public delegate ChangeEnemyShuffle Factory(bool enemyShuffle);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode settings.
        /// </param>
        /// <param name="enemyShuffle">
        /// The new enemy shuffle setting.
        /// </param>
        public ChangeEnemyShuffle(IMode mode, bool enemyShuffle)
        {
            _mode = mode;
            _enemyShuffle = enemyShuffle;
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
