using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Enums;

namespace OpenTracker.Actions
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
            _previousWorldState = _mode.WorldState.Value;
            _mode.WorldState = _worldState;
        }

        public void Undo()
        {
            _mode.WorldState = _previousWorldState;
        }
    }
}
