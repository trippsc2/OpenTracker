using OpenTracker.Models.Sections;
using System;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This is the class for an undoable action to mark a location section.
    /// </summary>
    public class MarkSection : IUndoable
    {
        private readonly IMarkableSection _section;
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
        public MarkSection(IMarkableSection section, MarkingType? marking)
        {
            _section = section ?? throw new ArgumentNullException(nameof(section));
            _marking = marking;
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
