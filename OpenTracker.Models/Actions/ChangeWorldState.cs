using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;

namespace OpenTracker.Models.Actions
{
    public class ChangeWorldState : IUndoable
    {
        private readonly Mode _mode;
        private readonly WorldState _worldState;
        private WorldState _previousWorldState;

        public ChangeWorldState(Mode mode, WorldState worldState)
        {
            _mode = mode;
            _worldState = worldState;
        }

        public void Execute()
        {
            _previousWorldState = _mode.WorldState;
            _mode.WorldState = _worldState;
        }

        public void Undo()
        {
            _mode.WorldState = _previousWorldState;
        }
    }
}
