using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo.Mode
{
    /// <summary>
    /// This class contains undoable action to change the guaranteed boss items setting (Ambrosia).
    /// </summary>
    public class ChangeGuaranteedBossItems : IUndoable
    {
        private readonly IMode _mode;
        private readonly bool _newValue;

        private bool _previousValue;

        public delegate ChangeGuaranteedBossItems Factory(bool newValue);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode settings.
        /// </param>
        /// <param name="newValue">
        /// The new guaranteed boss items setting.
        /// </param>
        public ChangeGuaranteedBossItems(IMode mode, bool newValue)
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
            _previousValue = _mode.GuaranteedBossItems;
            _mode.GuaranteedBossItems = _newValue;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void ExecuteUndo()
        {
            _mode.GuaranteedBossItems = _previousValue;
        }
    }
}
