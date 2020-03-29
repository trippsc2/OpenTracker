using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Interfaces;

namespace OpenTracker.Actions
{
    public class UncollectSection : IUndoable
    {
        private readonly ISection _section;

        public UncollectSection(ISection section)
        {
            _section = section;
        }

        public void Execute()
        {
            switch (_section)
            {
                case BossSection bossSection:
                    bossSection.Available = true;
                    break;
                case EntranceSection entranceSection:
                    entranceSection.Available = true;
                    break;
                case ItemSection itemSection:
                    itemSection.Available++;
                    break;
            }
        }

        public void Undo()
        {
            switch (_section)
            {
                case BossSection bossSection:
                    bossSection.Available = false;
                    break;
                case EntranceSection entranceSection:
                    entranceSection.Available = false;
                    break;
                case ItemSection itemSection:
                    itemSection.Available--;
                    break;
            }
        }
    }
}
