using OpenTracker.Models.Items;

namespace OpenTracker.Models.Undoables
{
    /// <summary>
    /// This is the class for an undoable action to add an item.
    /// </summary>
    public class AddItem : IUndoable
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
