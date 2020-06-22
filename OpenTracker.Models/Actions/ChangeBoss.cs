using OpenTracker.Models.Interfaces;
using OpenTracker.Models.Sections;

namespace OpenTracker.Models.Actions
{
    /// <summary>
    /// This is the class for an undoable action to change the boss
    /// of a dungeon.
    /// </summary>
    public class ChangeBoss : IUndoable
    {
        private readonly BossSection _bossSection;
        private readonly Boss _boss;
        private Boss _previousBoss;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bossSection">
        /// The boss section to be changed.
        /// </param>
        /// <param name="boss">
        /// The boss to be assigned to the dungeon.
        /// </param>
        public ChangeBoss(BossSection bossSection, Boss boss)
        {
            _bossSection = bossSection;
            _boss = boss;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void Execute()
        {
            _previousBoss = _bossSection.BossPlacement.Boss;
            _bossSection.BossPlacement.Boss = _boss;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            _bossSection.BossPlacement.Boss = _previousBoss;
        }
    }
}
