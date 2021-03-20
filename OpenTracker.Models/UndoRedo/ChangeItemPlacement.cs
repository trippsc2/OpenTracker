using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This class contains undoable action data to change the item placement setting.
    /// </summary>
    public class ChangeItemPlacement : IUndoable
    {
        private readonly IMode _mode;
        private readonly ItemPlacement _newValue;
        private ItemPlacement _previousValue;

        public delegate ChangeItemPlacement Factory(ItemPlacement newValue);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode settings.
        /// </param>
        /// <param name="newValue">
        /// The new item placement setting.
        /// </param>
        public ChangeItemPlacement(IMode mode, ItemPlacement newValue)
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
            _previousValue = _mode.ItemPlacement;
            _mode.ItemPlacement = _newValue;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void ExecuteUndo()
        {
            _mode.ItemPlacement = _previousValue;
        }
    }
}
