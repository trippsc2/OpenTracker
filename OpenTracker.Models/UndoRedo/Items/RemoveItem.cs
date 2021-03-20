using OpenTracker.Models.Items;

namespace OpenTracker.Models.UndoRedo.Items
{
    /// <summary>
    /// This class contains undoable action to remove an item.
    /// </summary>
    public class RemoveItem : IUndoable
    {
        private readonly IItem _item;

        public delegate RemoveItem Factory(IItem item);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item">
        /// The item data to be manipulated.
        /// </param>
        public RemoveItem(IItem item)
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
            return _item.CanRemove();
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void ExecuteDo()
        {
            _item.Remove();
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void ExecuteUndo()
        {
            _item.Add();
        }
    }
}
