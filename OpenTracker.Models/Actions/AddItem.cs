using OpenTracker.Models.Interfaces;

namespace OpenTracker.Models.Actions
{
    /// <summary>
    /// This is the class for an undoable action to add an item.
    /// </summary>
    public class AddItem : IUndoable
    {
        private readonly Item _item;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item">
        /// The item to be added.
        /// </param>
        public AddItem(Item item)
        {
            _item = item;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void Execute()
        {
            _item.Change(1);
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            _item.Change(-1);
        }
    }
}
