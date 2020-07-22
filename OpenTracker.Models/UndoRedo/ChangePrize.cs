using OpenTracker.Models.Items;
using OpenTracker.Models.PrizePlacements;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This is the class for an undoable action to change the dungeon prize.
    /// </summary>
    public class ChangePrize : IUndoable
    {
        private readonly IPrizePlacement _prizePlacement;
        private readonly IItem _item;
        private IItem _previousItem;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="prizePlacement">
        /// The boss section data for the dungeon.
        /// </param>
        /// <param name="item">
        /// The item of the prize to be placed.
        /// </param>
        public ChangePrize(IPrizePlacement prizePlacement, IItem item)
        {
            _prizePlacement = prizePlacement;
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
            _previousItem = _prizePlacement.Prize;
            _prizePlacement.Prize = _item;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            _prizePlacement.Prize = _previousItem;
        }
    }
}
