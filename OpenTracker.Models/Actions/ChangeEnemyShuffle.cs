using OpenTracker.Models.Interfaces;

namespace OpenTracker.Models.Actions
{
    public class ChangeEnemyShuffle : IUndoable
    {
        private readonly Mode _mode;
        private readonly bool _enemyShuffle;
        private bool _previousEnemyShuffle;

        public ChangeEnemyShuffle(Mode mode, bool enemyShuffle)
        {
            _mode = mode;
            _enemyShuffle = enemyShuffle;
        }

        public void Execute()
        {
            _previousEnemyShuffle = _mode.EnemyShuffle.Value;
            _mode.EnemyShuffle = _enemyShuffle;
        }

        public void Undo()
        {
            _mode.EnemyShuffle = _previousEnemyShuffle;
        }
    }
}
