using OpenTracker.Models.Interfaces;
using OpenTracker.Models.Sections;

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
            _previousBoss = _bossSection.BossPlacement.Boss;
            _bossSection.BossPlacement.Boss = _boss;
        }

        public void Undo()
        {
            _bossSection.BossPlacement.Boss = _previousBoss;
        }
    }
}
