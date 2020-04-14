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
            _section.Available++;
        }

        public void Undo()
        {
            _section.Available--;
        }
    }
}
