using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo.Mode
{
    /// <summary>
    /// This class contains undoable action data to change the map shuffle setting.
    /// </summary>
    public class ChangeMapShuffle : IUndoable
    {
        private readonly IMode _mode;
        private readonly bool _newValue;
        private bool _previousValue;

        public delegate ChangeMapShuffle Factory(bool newValue);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode settings.
        /// </param>
        /// <param name="newValue">
        /// The new map shuffle setting.
        /// </param>
        public ChangeMapShuffle(IMode mode, bool newValue)
        {
            _mode = mode;
            _newValue = newValue;
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
            _previousValue = _mode.MapShuffle;
            _mode.MapShuffle = _newValue;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void ExecuteUndo()
        {
            _mode.MapShuffle = _previousValue;
        }
    }
}
