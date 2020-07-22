using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This is the class for an undoable action to change the dungeon item shuffle setting.
    /// </summary>
    public class ChangeDungeonItemShuffle : IUndoable
    {
        private readonly DungeonItemShuffle _dungeonItemShuffle;
        private DungeonItemShuffle _previousDungeonItemShuffle;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dungeonItemShuffle">
        /// The new dungeon item shuffle setting.
        /// </param>
        public ChangeDungeonItemShuffle(DungeonItemShuffle dungeonItemShuffle)
        {
            _dungeonItemShuffle = dungeonItemShuffle;
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
            _previousDungeonItemShuffle = Mode.Instance.DungeonItemShuffle;
            Mode.Instance.DungeonItemShuffle = _dungeonItemShuffle;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            Mode.Instance.DungeonItemShuffle = _previousDungeonItemShuffle;
        }
    }
}
