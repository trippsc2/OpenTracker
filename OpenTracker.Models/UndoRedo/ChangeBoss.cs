using OpenTracker.Models.BossPlacements;
using System;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This class contains undoable action to change the boss of a dungeon.
    /// </summary>
    public class ChangeBoss : IUndoable
    {
        private readonly IBossPlacement _bossPlacement;
        private readonly BossType? _boss;

        private BossType? _previousBoss;

        public delegate ChangeBoss Factory(
            IBossPlacement bossPlacement, BossType? boss);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bossPlacement">
        /// The boss section to be changed.
        /// </param>
        /// <param name="boss">
        /// The boss to be assigned to the dungeon.
        /// </param>
        public ChangeBoss(IBossPlacement bossPlacement, BossType? boss)
        {
            _bossPlacement = bossPlacement;
            _boss = boss;
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
            _previousBoss = _bossPlacement.Boss;
            _bossPlacement.Boss = _boss;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void ExecuteUndo()
        {
            _bossPlacement.Boss = _previousBoss;
        }
    }
}
