using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This class contains undoable action data to change the generic keys mode setting.
    /// </summary>
    public class ChangeGenericKeys : IUndoable
    {
        private readonly IMode _mode;
        private readonly bool _genericKeys;
        private bool _previousGenericKeys;

        public delegate ChangeGenericKeys Factory(bool genericKeys);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode settings.
        /// </param>
        /// <param name="genericKeys">
        /// The new generic keys setting.
        /// </param>
        public ChangeGenericKeys(IMode mode, bool genericKeys)
        {
            _mode = mode;
            _genericKeys = genericKeys;
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
            _previousGenericKeys = _mode.GenericKeys;
            _mode.GenericKeys = _genericKeys;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            _mode.GenericKeys = _previousGenericKeys;
        }
    }
}
