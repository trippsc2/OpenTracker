using OpenTracker.Models.Interfaces;

namespace OpenTracker.Models.Actions
{
    public class ChangeBoss : IUndoable
    {
        private readonly BossSection _bossSection;
        private readonly Boss _boss;
        private Boss _previousBoss;

        public ChangeBoss(BossSection bossSection, Boss boss)
        {
            _bossSection = bossSection;
            _boss = boss;
        }

        public void Execute()
        {
            _previousBoss = _bossSection.Boss;
            _bossSection.Boss = _boss;
        }

        public void Undo()
        {
            _bossSection.Boss = _previousBoss;
        }
    }
}
