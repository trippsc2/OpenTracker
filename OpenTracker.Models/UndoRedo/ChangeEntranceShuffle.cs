using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This is the class for an undoable action to change the entrance shuffle setting.
    /// </summary>
    public class ChangeEntranceShuffle : IUndoable
    {
        private readonly IMode _mode;
        private readonly EntranceShuffle _entranceShuffle;
        private EntranceShuffle _previousEntranceShuffle;

        public delegate ChangeEntranceShuffle Factory(EntranceShuffle entranceShuffle);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="entranceShuffle">
        /// The new entrance shuffle setting.
        /// </param>
        public ChangeEntranceShuffle(IMode mode, EntranceShuffle entranceShuffle)
        {
            _mode = mode;
            _entranceShuffle = entranceShuffle;
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
