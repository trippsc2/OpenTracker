using OpenTracker.Interfaces;
using OpenTracker.Models;

namespace OpenTracker.Actions
{
    public class TogglePrize : IUndoable
    {
        private readonly BossSection _prizeSection;

        public TogglePrize(BossSection prizeSection)
        {
            _prizeSection = prizeSection;
        }

        public void Execute()
        {
            _prizeSection.Available = !_prizeSection.Available;
        }

        public void Undo()
        {
            _prizeSection.Available = !_prizeSection.Available;
        }
    }
}
