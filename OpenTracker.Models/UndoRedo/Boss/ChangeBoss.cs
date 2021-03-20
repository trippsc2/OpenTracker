using OpenTracker.Models.BossPlacements;

namespace OpenTracker.Models.UndoRedo.Boss
{
    /// <summary>
    /// This class contains undoable action to change the boss of a dungeon.
    /// </summary>
    public class ChangeBoss : IUndoable
    {
        private readonly IBossPlacement _bossPlacement;
        private readonly BossType? _newValue;

        private BossType? _previousValue;

        public delegate ChangeBoss Factory(IBossPlacement bossPlacement, BossType? newValue);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bossPlacement">
        /// The boss section to be changed.
        /// </param>
        /// <param name="newValue">
        /// The boss to be assigned to the dungeon.
        /// </param>
        public ChangeBoss(IBossPlacement bossPlacement, BossType? newValue)
        {
            _bossPlacement = bossPlacement;
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
            _previousValue = _bossPlacement.Boss;
            _bossPlacement.Boss = _newValue;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void ExecuteUndo()
        {
            _bossPlacement.Boss = _previousValue;
        }
    }
}
