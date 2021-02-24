using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This is the class for an undoable action to add a note to a location.
    /// </summary>
    public class AddNote : IUndoable
    {
        private readonly IMarking.Factory _factory;
        private readonly ILocation _location;
        private IMarking? _note;

        public delegate AddNote Factory(ILocation location);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="location">
        /// The location to which the note will be added.
        /// </param>
        public AddNote(IMarking.Factory factory, ILocation location)
        {
            _factory = factory;
            _location = location;
        }

        /// <summary>
        /// Returns whether the action can be executed.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the action can be executed.
        /// </returns>
        public bool CanExecute()
        {
            return _location.Notes.Count < 4;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void Execute()
        {
            _note = _factory();
            _location.Notes.Add(_note);
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            _location.Notes.Remove(_note!);
        }
    }
}
