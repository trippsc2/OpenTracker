using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;

namespace OpenTracker.Models.UndoRedo.Notes
{
    /// <summary>
    /// This class contains undoable action to remove a note from a location.
    /// </summary>
    public class RemoveNote : IUndoable
    {
        private readonly ILocation _location;
        private readonly IMarking _note;
        private int _index;

        public delegate RemoveNote Factory(IMarking note, ILocation location);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="note">
        /// The note to be removed.
        /// </param>
        /// <param name="location">
        /// The location from which the note will be removed.
        /// </param>
        public RemoveNote(IMarking note, ILocation location)
        {
            _note = note;
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
            return true;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void ExecuteDo()
        {
            _index = _location.Notes.IndexOf(_note);
            _location.Notes.Remove(_note);
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void ExecuteUndo()
        {
            _location.Notes.Insert(_index, _note);
        }
    }
}
