using OpenTracker.Models.Items;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This is the class for an undoable action to cycle an item through valid counts.
    /// </summary>
    public class CycleItem : IUndoable
    {
        private readonly IItem _item;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item">
        /// The item data to be manipulated.
        /// </param>
        public CycleItem(IItem item)
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
        public void Execute()
        {
            if (_item.Current == _item.Maximum)
            {
                _item.Current = 0;
            }
            else
            {
                _item.Current++;
            }
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            if (_item.Current == 0)
            {
                _item.Current = _item.Maximum;
            }
            else
            {
                _item.Current--;
            }
        }
    }
}
