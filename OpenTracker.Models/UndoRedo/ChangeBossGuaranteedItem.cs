using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This is the class for an undoable action to change the guaranteed boss items setting
    /// (Ambrosia).
    /// </summary>
    public class ChangeGuaranteedBossItems : IUndoable
    {
        private readonly bool _guaranteedBossItems;
        private bool _previousGuaranteedBossItems;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="guaranteedBossItems">
        /// The new guaranteed boss items setting.
        /// </param>
        public ChangeGuaranteedBossItems(bool guaranteedBossItems)
        {
            _guaranteedBossItems = guaranteedBossItems;
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
            _previousGuaranteedBossItems = Mode.Instance.GuaranteedBossItems;
            Mode.Instance.GuaranteedBossItems = _guaranteedBossItems;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            Mode.Instance.GuaranteedBossItems = _previousGuaranteedBossItems;
        }
    }
}
