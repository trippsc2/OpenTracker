using OpenTracker.Models.Interfaces;

namespace OpenTracker.Models.Actions
{
    /// <summary>
    /// This is the class for an undoable action to uncollect items/entrances
    /// in a section.
    /// </summary>
    public class UncollectSection : IUndoable
    {
        private readonly ISection _section;
        private bool _previousUserManipulated;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="section">
        /// The section data to be manipulated.
        /// </param>
        public UncollectSection(ISection section)
        {
            _section = section;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void Execute()
        {
            _previousUserManipulated = _section.UserManipulated;
            _section.UserManipulated = true;
            _section.Available++;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            _section.UserManipulated = _previousUserManipulated;
            _section.Available--;
        }
    }
}
