using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;

namespace OpenTracker.Models.UndoRedo.Notes
{
    /// <summary>
    /// This class contains undoable action data to add a note to a location.
    /// </summary>
    public class AddNote : IAddNote
    {
        private readonly IMarking.Factory _factory;
        private readonly ILocationNoteCollection _notes;
        private IMarking? _note;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        /// An Autofac factory for creating markings.
        /// </param>
        /// <param name="location">
        /// The location to which the note will be added.
        /// </param>
        public AddNote(IMarking.Factory factory, ILocation location)
        {
            _factory = factory;
            _notes = location.Notes;
        }

        /// <summary>
        /// Returns whether the action can be executed.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the action can be executed.
        /// </returns>
        public bool CanExecute()
        {
            return _notes.Count < 4;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void ExecuteDo()
        {
            _note = _factory();
            _notes.Add(_note);
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void ExecuteUndo()
        {
            _notes.Remove(_note!);
        }
    }
}
