using OpenTracker.Models.Interfaces;
using OpenTracker.Models.Sections;

namespace OpenTracker.Models.Actions
{
    /// <summary>
    /// This is the class for an undoable action to change the dungeon prize.
    /// </summary>
    public class ChangePrize : IUndoable
    {
        private readonly BossSection _prizeSection;
        private readonly Item _item;
        private Item _previousItem;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="prizeSection">
        /// The boss section data for the dungeon.
        /// </param>
        /// <param name="dungeonItemShuffle">
        /// The item of the prize to be placed.
        /// </param>
        public ChangePrize(BossSection prizeSection, Item item)
        {
            _prizeSection = prizeSection;
            _item = item;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void Execute()
        {
            _previousItem = _prizeSection.Prize;
            _prizeSection.Prize = _item;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            _prizeSection.Prize = _previousItem;
        }
    }
}
