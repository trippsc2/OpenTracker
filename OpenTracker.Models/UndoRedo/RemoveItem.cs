using OpenTracker.Models.Items;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This is the class for an undoable action to remove an item.
    /// </summary>
    public class RemoveItem : IUndoable
    {
        private readonly IItem _item;

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
        public void Execute()
        {
            _item.Current--;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            _item.Current++;
        }
    }
}
