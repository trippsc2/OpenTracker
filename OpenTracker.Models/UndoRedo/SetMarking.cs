using OpenTracker.Models.Markings;
using System;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This is the class for an undoable action to set a marking.
    /// </summary>
    public class SetMarking : IUndoable
    {
        private readonly IMarking _marking;
        private readonly MarkingType? _newMarking;
        private MarkingType? _previousMarking;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="marking">
        /// The section data to be marked.
        /// </param>
        /// <param name="newMarking">
        /// The marking to be applied to the section.
        /// </param>
        public SetMarking(IMarking marking, MarkingType? newMarking)
        {
            _marking = marking ?? throw new ArgumentNullException(nameof(marking));
            _newMarking = newMarking;
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
            _previousMarking = _marking.Value;
            _marking.Value = _newMarking;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            _marking.Value = _previousMarking;
        }
    }
}
