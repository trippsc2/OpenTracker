using OpenTracker.Models.Markings;

namespace OpenTracker.Models.UndoRedo.Markings
{
    /// <summary>
    /// This class contains <see cref="IUndoable"/> action to change a <see cref="IMarking"/>.
    /// </summary>
    public class ChangeMarking : IChangeMarking
    {
        private readonly IMarking _marking;
        private readonly MarkType _newValue;

        private MarkType _previousMarking;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="marking">
        ///     The <see cref="IMarking"/>.
        /// </param>
        /// <param name="newValue">
        ///     The new <see cref="MarkType"/> value.
        /// </param>
        public ChangeMarking(IMarking marking, MarkType newValue)
        {
            _marking = marking;
            _newValue = newValue;
        }

        public bool CanExecute()
        {
            return true;
        }

        public void ExecuteDo()
        {
            _previousMarking = _marking.Mark;
            _marking.Mark = _newValue;
        }

        public void ExecuteUndo()
        {
            _marking.Mark = _previousMarking;
        }
    }
}
