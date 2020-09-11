using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This is the class for an undoable action to change the entrance shuffle setting.
    /// </summary>
    public class ChangeEntranceShuffle : IUndoable
    {
        private readonly EntranceShuffle _entranceShuffle;
        private EntranceShuffle _previousEntranceShuffle;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="entranceShuffle">
        /// The new entrance shuffle setting.
        /// </param>
        public ChangeEntranceShuffle(EntranceShuffle entranceShuffle)
        {
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
            _previousEntranceShuffle = Mode.Instance.EntranceShuffle;
            Mode.Instance.EntranceShuffle = _entranceShuffle;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            Mode.Instance.EntranceShuffle = _previousEntranceShuffle;
        }
    }
}
