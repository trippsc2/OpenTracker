using OpenTracker.Interfaces;
using OpenTracker.Models;

namespace OpenTracker.Actions
{
    public class CycleItem : IUndoable
    {
        private readonly Item _item;

        public CycleItem(Item item)
        {
            _item = item;
        }

        public void Execute()
        {
            if (_item.Current == _item.Maximum)
                _item.SetCurrent();
            else
                _item.Change(1);
        }

        public void Undo()
        {
            if (_item.Current == 0)
                _item.SetCurrent(_item.Maximum);
            else
                _item.Change(-1);
        }
    }
}
