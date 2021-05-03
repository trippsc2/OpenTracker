using OpenTracker.Models.Items;

namespace OpenTracker.Models.UndoRedo.Items
{
    /// <summary>
    /// This class contains undoable action data to add an item.
    /// </summary>
    public class AddItem : IAddItem
    {
        private readonly IItem _item;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item">
        /// The item to be added.
        /// </param>
        public AddItem(IItem item)
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
            return _item.CanAdd();
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void ExecuteDo()
        {
            _item.Add();
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void ExecuteUndo()
        {
            _item.Remove();
        }
    }
}
