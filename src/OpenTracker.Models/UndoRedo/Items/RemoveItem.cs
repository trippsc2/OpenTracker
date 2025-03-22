using OpenTracker.Models.Items;

namespace OpenTracker.Models.UndoRedo.Items
{
    /// <summary>
    /// This class contains the <see cref="IUndoable"/> action to remove an <see cref="IItem"/>.
    /// </summary>
    public class RemoveItem : IRemoveItem
    {
        private readonly IItem _item;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item">
        ///     The <see cref="IItem"/>.
        /// </param>
        public RemoveItem(IItem item)
        {
            _item = item;
        }

        public bool CanExecute()
        {
            return _item.CanRemove();
        }

        public void ExecuteDo()
        {
            _item.Remove();
        }

        public void ExecuteUndo()
        {
            _item.Add();
        }
    }
}
