using OpenTracker.Models.Interfaces;

namespace OpenTracker.Models.Actions
{
    /// <summary>
    /// This is the class for an undoable action to remove an item.
    /// </summary>
    public class RemoveItem : IUndoable
    {
        private readonly Item _item;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item">
        /// The item data to be manipulated.
        /// </param>
        public RemoveItem(Item item)
        {
            _item = item;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void Execute()
        {
            _item.Change(-1);
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            _item.Change(1);
        }
    }
}
