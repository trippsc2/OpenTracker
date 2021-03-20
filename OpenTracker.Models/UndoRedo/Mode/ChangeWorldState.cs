using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo.Mode
{
    /// <summary>
    /// This class contains undoable action data to change the world state setting.
    /// </summary>
    public class ChangeWorldState : IUndoable
    {
        private readonly IMode _mode;
        private readonly WorldState _newValue;
        private WorldState _previousValue;
        private ItemPlacement _previousItemPlacement;

        public delegate ChangeWorldState Factory(WorldState newValue);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode settings data.
        /// </param>
        /// <param name="newValue">
        /// The new world state setting.
        /// </param>
        public ChangeWorldState(IMode mode, WorldState newValue)
        {
            _mode = mode;
            _newValue = newValue;
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
        public void ExecuteDo()
        {
            _previousValue = _mode.WorldState;
            _previousItemPlacement = _mode.ItemPlacement;
            _mode.WorldState = _newValue;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void ExecuteUndo()
        {
            _mode.WorldState = _previousValue;
            _mode.ItemPlacement = _previousItemPlacement;
        }
    }
}
