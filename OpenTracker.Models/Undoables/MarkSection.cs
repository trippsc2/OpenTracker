using OpenTracker.Models.Enums;
using OpenTracker.Models.Sections;

namespace OpenTracker.Models.Undoables
{
    /// <summary>
    /// This is the class for an undoable action to mark a location section.
    /// </summary>
    public class MarkSection : IUndoable
    {
        private readonly ISection _section;
        private readonly MarkingType? _marking;
        private MarkingType? _previousMarking;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="section">
        /// The section data to be marked.
        /// </param>
        /// <param name="marking">
        /// The marking to be applied to the section.
        /// </param>
        public MarkSection(ISection section, MarkingType? marking)
        {
            _section = section;
            _marking = marking;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void Execute()
        {
            _previousMarking = _section.Marking;
            _section.Marking = _marking;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            _section.Marking = _previousMarking;
        }
    }
}
