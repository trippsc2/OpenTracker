using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This is the class for an undoable action to change the world state setting.
    /// </summary>
    public class ChangeWorldState : IUndoable
    {
        private readonly IMode _mode;
        private readonly WorldState _worldState;
        private WorldState _previousWorldState;
        private ItemPlacement _previousItemPlacement;

        public delegate ChangeWorldState Factory(WorldState worldState);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="worldState">
        /// The new world state setting.
        /// </param>
        public ChangeWorldState(IMode mode, WorldState worldState)
        {
            _mode = mode;
            _worldState = worldState;
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
