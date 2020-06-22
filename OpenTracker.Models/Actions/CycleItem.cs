using OpenTracker.Models.Interfaces;

namespace OpenTracker.Models.Actions
{
    /// <summary>
    /// This is the class for an undoable action to cycle an item through
    /// valid counts.
    /// </summary>
    public class CycleItem : IUndoable
    {
        private readonly Item _item;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item">
        /// The item data to be manipulated.
        /// </param>
        public CycleItem(Item item)
        {
            _item = item;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void Execute()
        {
            if (_item.Current == _item.Maximum)
            {
                _item.SetCurrent();
            }
            else
            {
                _item.Change(1);
            }
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            if (_item.Current == 0)
            {
                _item.SetCurrent(_item.Maximum);
            }
            else
            {
                _item.Change(-1);
            }
        }
    }
}
