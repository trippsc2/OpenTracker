using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;

namespace OpenTracker.Models.Actions
{
    /// <summary>
    /// This is the class for an undoable action to change the dungeon item
    /// shuffle setting.
    /// </summary>
    public class ChangeDungeonItemShuffle : IUndoable
    {
        private readonly Mode _mode;
        private readonly DungeonItemShuffle _dungeonItemShuffle;
        private DungeonItemShuffle _previousDungeonItemShuffle;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The game mode data.
        /// </param>
        /// <param name="dungeonItemShuffle">
        /// The new dungeon item shuffle setting.
        /// </param>
        public ChangeDungeonItemShuffle(Mode mode, DungeonItemShuffle dungeonItemShuffle)
        {
            _mode = mode;
            _dungeonItemShuffle = dungeonItemShuffle;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void Execute()
        {
            _previousDungeonItemShuffle = _mode.DungeonItemShuffle;
            _mode.DungeonItemShuffle = _dungeonItemShuffle;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            _mode.DungeonItemShuffle = _previousDungeonItemShuffle;
        }
    }
}
