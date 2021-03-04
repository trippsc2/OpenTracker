using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This class contains undoable action data to change the map shuffle setting.
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
        /// <param name="mode">
        /// The mode settings.
        /// </param>
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
        public void ExecuteDo()
        {
            _previousMapShuffle = _mode.MapShuffle;
            _mode.MapShuffle = _mapShuffle;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void ExecuteUndo()
        {
            _mode.MapShuffle = _previousMapShuffle;
        }
    }
}
