using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;

namespace OpenTracker.Models.Actions
{
    /// <summary>
    /// This is the class for an undoable action to change the world state
    /// setting.
    /// </summary>
    public class ChangeWorldState : IUndoable
    {
        private readonly Mode _mode;
        private readonly WorldState _worldState;
        private WorldState _previousWorldState;
        private ItemPlacement _previousItemPlacement;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The game mode data.
        /// </param>
        /// <param name="worldState">
        /// The new world state setting.
        /// </param>
        public ChangeWorldState(Mode mode, WorldState worldState)
        {
            _mode = mode;
            _worldState = worldState;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void Execute()
        {
            _previousWorldState = _mode.WorldState;
            _previousItemPlacement = _mode.ItemPlacement;
            _mode.WorldState = _worldState;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            _mode.WorldState = _previousWorldState;
            _mode.ItemPlacement = _previousItemPlacement;
        }
    }
}
