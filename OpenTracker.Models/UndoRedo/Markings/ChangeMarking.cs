using OpenTracker.Models.Markings;

namespace OpenTracker.Models.UndoRedo.Markings
{
    /// <summary>
    /// This class contains undoable action data to change a marking.
    /// </summary>
    public class ChangeMarking : IChangeMarking
    {
        private readonly IMarking _marking;
        private readonly MarkType _newMarking;

        private MarkType _previousMarking;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="marking">
        /// The section data to be marked.
        /// </param>
        /// <param name="newMarking">
        /// The marking to be applied to the section.
        /// </param>
        public ChangeMarking(IMarking marking, MarkType newMarking)
        {
            _marking = marking;
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
        public void ExecuteDo()
        {
            _previousMarking = _marking.Mark;
            _marking.Mark = _newMarking;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void ExecuteUndo()
        {
            _marking.Mark = _previousMarking;
        }
    }
}
