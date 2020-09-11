using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This is the class for an undoable action to change the map shuffle setting.
    /// </summary>
    public class ChangeMapShuffle : IUndoable
    {
        private readonly bool _mapShuffle;
        private bool _previousMapShuffle;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapShuffle">
        /// The new map shuffle setting.
        /// </param>
        public ChangeMapShuffle(bool mapShuffle)
        {
            _mapShuffle = mapShuffle;
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
            _previousMapShuffle = Mode.Instance.MapShuffle;
            Mode.Instance.MapShuffle = _mapShuffle;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            Mode.Instance.MapShuffle = _previousMapShuffle;
        }
    }
}
