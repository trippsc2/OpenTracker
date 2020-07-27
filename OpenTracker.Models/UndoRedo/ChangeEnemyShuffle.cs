using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This is the class for an undoable action to change the enemy shuffle setting.
    /// </summary>
    public class ChangeEnemyShuffle : IUndoable
    {
        private readonly bool _enemyShuffle;
        private bool _previousEnemyShuffle;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="enemyShuffle">
        /// The new enemy shuffle setting.
        /// </param>
        public ChangeEnemyShuffle(bool enemyShuffle)
        {
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
            _previousEnemyShuffle = Mode.Instance.EnemyShuffle;
            Mode.Instance.EnemyShuffle = _enemyShuffle;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            Mode.Instance.EnemyShuffle = _previousEnemyShuffle;
        }
    }
}
