using OpenTracker.Models.Interfaces;

namespace OpenTracker.Models.Actions
{
    public class ChangePrize : IUndoable
    {
        private readonly BossSection _prizeSection;
        private readonly Item _item;
        private Item _previousItem;

        public ChangePrize(BossSection prizeSection, Item item)
        {
            _prizeSection = prizeSection;
            _item = item;
        }

        public void Execute()
        {
            _previousItem = _prizeSection.Prize;
            _prizeSection.Prize = _item;
        }

        public void Undo()
        {
            _prizeSection.Prize = _previousItem;
        }
    }
}
