using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;
using System;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This is the class for an undoable action to add a note to a location.
    /// </summary>
    public class AddNote : IUndoable
    {
        private readonly ILocation _location;
        private IMarking _note;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="location">
        /// The location to which the note will be added.
        /// </param>
        public AddNote(ILocation location)
        {
            _location = location ?? throw new ArgumentNullException(nameof(location));
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
            _note = MarkingFactory.GetMarking();
            _location.Notes.Add(_note);
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            _location.Notes.Remove(_note);
        }
    }
}
