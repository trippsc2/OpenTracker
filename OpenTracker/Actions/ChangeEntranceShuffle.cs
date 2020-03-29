using OpenTracker.Interfaces;
using OpenTracker.Models;

namespace OpenTracker.Actions
{
    public class ChangeEntranceShuffle : IUndoable
    {
        private readonly Mode _mode;
        private readonly bool _entranceShuffle;
        private bool _previousEntranceShuffle;

        public ChangeEntranceShuffle(Mode mode, bool entranceShuffle)
        {
            _mode = mode;
            _entranceShuffle = entranceShuffle;
        }

        public void Execute()
        {
            _previousEntranceShuffle = _mode.EntranceShuffle.Value;
            _mode.EntranceShuffle = _entranceShuffle;
        }

        public void Undo()
        {
            _mode.EntranceShuffle = _previousEntranceShuffle;
        }
    }
}
