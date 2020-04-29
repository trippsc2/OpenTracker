using OpenTracker.Models.Interfaces;

namespace OpenTracker.Models.Actions
{
    public class RemoveItem : IUndoable
    {
        private readonly Item _item;

        public RemoveItem(Item item)
        {
            _item = item;
        }

        public void Execute()
        {
            _item.Change(-1);
        }

        public void Undo()
        {
            _item.Change(1);
        }
    }
}
