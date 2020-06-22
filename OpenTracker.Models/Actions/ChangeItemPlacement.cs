using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;

namespace OpenTracker.Models.Actions
{
    /// <summary>
    /// This is the class for an undoable action to change the item placement
    /// setting.
    /// </summary>
    public class ChangeItemPlacement : IUndoable
    {
        private readonly Mode _mode;
        private readonly ItemPlacement _itemPlacement;
        private ItemPlacement _previousItemPlacement;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The game mode data.
        /// </param>
        /// <param name="dungeonItemShuffle">
        /// The new item placement setting.
        /// </param>
        public ChangeItemPlacement(Mode mode, ItemPlacement itemPlacement)
        {
            _mode = mode;
            _itemPlacement = itemPlacement;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void Execute()
        {
            _previousItemPlacement = _mode.ItemPlacement;
            _mode.ItemPlacement = _itemPlacement;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            _mode.ItemPlacement = _previousItemPlacement;
        }
    }
}
