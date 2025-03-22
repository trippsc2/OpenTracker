using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo.Mode
{
    /// <summary>
    /// This class contains the <see cref="IUndoable"/> action to change the <see cref="IMode.MapShuffle"/> property.
    /// </summary>
    public class ChangeMapShuffle : IChangeMapShuffle
    {
        private readonly IMode _mode;
        private readonly bool _newValue;
        private bool _previousValue;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        ///     The <see cref="IMode"/> data.
        /// </param>
        /// <param name="newValue">
        ///     A <see cref="bool"/> representing the new <see cref="IMode.MapShuffle"/> value.
        /// </param>
        public ChangeMapShuffle(IMode mode, bool newValue)
        {
            _mode = mode;
            _newValue = newValue;
        }

        public bool CanExecute()
        {
            return true;
        }

        public void ExecuteDo()
        {
            _previousValue = _mode.MapShuffle;
            _mode.MapShuffle = _newValue;
        }

        public void ExecuteUndo()
        {
            _mode.MapShuffle = _previousValue;
        }
    }
}
