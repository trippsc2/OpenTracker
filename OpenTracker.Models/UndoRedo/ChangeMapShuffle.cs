using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This is the class for an undoable action to change the map shuffle setting.
    /// </summary>
    public class ChangeMapShuffle : IUndoable
    {
        private readonly IMode _mode;
        private readonly bool _mapShuffle;
        private bool _previousMapShuffle;

        public delegate ChangeMapShuffle Factory(bool mapShuffle);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapShuffle">
        /// The new map shuffle setting.
        /// </param>
        public ChangeMapShuffle(IMode mode, bool mapShuffle)
        {
            _mode = mode;
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
            _previousMapShuffle = _mode.MapShuffle;
            _mode.MapShuffle = _mapShuffle;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            _mode.MapShuffle = _previousMapShuffle;
        }
    }
}
