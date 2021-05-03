using OpenTracker.Models.Items;

namespace OpenTracker.Models.UndoRedo.Items
{
    /// <summary>
    /// This class contains undoable action data to cycle an item through valid counts.
    /// </summary>
    public class CycleItem : ICycleItem
    {
        private readonly ICappedItem _item;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item">
        /// The item data to be manipulated.
        /// </param>
        public CycleItem(ICappedItem item)
        {
            _item = item;
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
            _item.Cycle();
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void ExecuteUndo()
        {
            _item.Cycle(true);
        }
    }
}
