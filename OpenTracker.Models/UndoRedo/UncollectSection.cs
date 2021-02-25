using OpenTracker.Models.Sections;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This class contains undoable action to uncollect a section.
    /// </summary>
    public class UncollectSection : IUndoable
    {
        private readonly ISection _section;

        private bool _previousUserManipulated;

        public delegate UncollectSection Factory(ISection section);

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
        /// Returns whether the action can be executed.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the action can be executed.
        /// </returns>
        public bool CanExecute()
        {
            return _section.CanBeUncleared();
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
