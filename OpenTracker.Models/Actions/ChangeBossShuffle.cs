using OpenTracker.Models.Interfaces;

namespace OpenTracker.Models.Actions
{
    public class ChangeBossShuffle : IUndoable
    {
        private readonly Mode _mode;
        private readonly bool _bossShuffle;
        private bool _previousBossShuffle;

        public ChangeBossShuffle(Mode mode, bool bossShuffle)
        {
            _mode = mode;
            _bossShuffle = bossShuffle;
        }

        public void Execute()
        {
            _previousBossShuffle = _mode.BossShuffle;
            _mode.BossShuffle = _bossShuffle;
        }

        public void Undo()
        {
            _mode.BossShuffle = _previousBossShuffle;
        }
    }
}
