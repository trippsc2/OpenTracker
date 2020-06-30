namespace OpenTracker.Models.Undoables
{
    /// <summary>
    /// This is the class for an undoable action to change the entrance shuffle
    /// setting.
    /// </summary>
    public class ChangeEntranceShuffle : IUndoable
    {
        private readonly Mode _mode;
        private readonly bool _entranceShuffle;
        private bool _previousEntranceShuffle;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The game mode data.
        /// </param>
        /// <param name="entranceShuffle">
        /// The new entrance shuffle setting.
        /// </param>
        public ChangeEntranceShuffle(Mode mode, bool entranceShuffle)
        {
            _mode = mode;
            _entranceShuffle = entranceShuffle;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void Execute()
        {
            _previousEntranceShuffle = _mode.EntranceShuffle;
            _mode.EntranceShuffle = _entranceShuffle;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            _mode.EntranceShuffle = _previousEntranceShuffle;
        }
    }
}
