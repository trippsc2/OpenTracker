using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This is the class for an undoable action to change the world state setting.
    /// </summary>
    public class ChangeWorldState : IUndoable
    {
        private readonly WorldState _worldState;
        private WorldState _previousWorldState;
        private ItemPlacement _previousItemPlacement;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="worldState">
        /// The new world state setting.
        /// </param>
        public ChangeWorldState(WorldState worldState)
        {
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
            _previousWorldState = Mode.Instance.WorldState;
            _previousItemPlacement = Mode.Instance.ItemPlacement;
            Mode.Instance.WorldState = _worldState;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            Mode.Instance.WorldState = _previousWorldState;
            Mode.Instance.ItemPlacement = _previousItemPlacement;
        }
    }
}
