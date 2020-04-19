using OpenTracker.Interfaces;
using OpenTracker.Models.Interfaces;

namespace OpenTracker.Actions
{
    public class UncollectSection : IUndoable
    {
        private readonly ISection _section;
        private bool _previousUserManipulated;

        public UncollectSection(ISection section)
        {
            _section = section;
        }

        public void Execute()
        {
            _previousUserManipulated = _section.UserManipulated;
            _section.UserManipulated = true;
            _section.Available++;
        }

        public void Undo()
        {
            _section.UserManipulated = _previousUserManipulated;
            _section.Available--;
        }
    }
}
