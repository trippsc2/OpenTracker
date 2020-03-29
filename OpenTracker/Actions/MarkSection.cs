using OpenTracker.Interfaces;
using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;

namespace OpenTracker.Actions
{
    public class MarkSection : IUndoable
    {
        private readonly ISection _section;
        private readonly MarkingType? _marking;
        private MarkingType? _previousMarking;

        public MarkSection(ISection section, MarkingType? marking)
        {
            _section = section;
            _marking = marking;
        }

        public void Execute()
        {
            _previousMarking = _section.Marking;
            _section.Marking = _marking;
        }

        public void Undo()
        {
            _section.Marking = _previousMarking;
        }
    }
}
