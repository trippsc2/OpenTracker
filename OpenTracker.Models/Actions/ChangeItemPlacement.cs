using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;

namespace OpenTracker.Models.Actions
{
    public class ChangeItemPlacement : IUndoable
    {
        private readonly Mode _mode;
        private readonly ItemPlacement _itemPlacement;
        private ItemPlacement _previousItemPlacement;

        public ChangeItemPlacement(Mode mode, ItemPlacement itemPlacement)
        {
            _mode = mode;
            _itemPlacement = itemPlacement;
        }

        public void Execute()
        {
            _previousItemPlacement = _mode.ItemPlacement.Value;
            _mode.ItemPlacement = _itemPlacement;
        }

        public void Undo()
        {
            _mode.ItemPlacement = _previousItemPlacement;
        }

        public bool Validate(ItemPlacement itemPlacement)
        {
            return _itemPlacement == itemPlacement;
        }
    }
}
