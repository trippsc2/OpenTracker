using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo.Mode
{
    /// <summary>
    /// This class contains the <see cref="IUndoable"/> action to change the <see cref="IMode.KeyDropShuffle"/>
    /// property.
    /// </summary>
    public class ChangeKeyDropShuffle : IChangeKeyDropShuffle
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
        ///     A <see cref="bool"/> representing the new <see cref="IMode.KeyDropShuffle"/> value.
        /// </param>
        public ChangeKeyDropShuffle(IMode mode, bool newValue)
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
            _previousValue = _mode.KeyDropShuffle;
            _mode.KeyDropShuffle = _newValue;
        }

        public void ExecuteUndo()
        {
            _mode.KeyDropShuffle = _previousValue;
        }
    }
}
