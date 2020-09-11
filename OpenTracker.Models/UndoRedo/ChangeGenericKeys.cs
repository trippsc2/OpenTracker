using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo
{
    public class ChangeGenericKeys : IUndoable
    {
        private readonly bool _genericKeys;
        private bool _previousGenericKeys;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="genericKeys">
        /// The new generic keys setting.
        /// </param>
        public ChangeGenericKeys(bool genericKeys)
        {
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
            _previousGenericKeys = Mode.Instance.GenericKeys;
            Mode.Instance.GenericKeys = _genericKeys;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            Mode.Instance.GenericKeys = _previousGenericKeys;
        }
    }
}
