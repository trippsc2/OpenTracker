using OpenTracker.Models.Sections;

namespace OpenTracker.Models.UndoRedo.Sections
{
    /// <summary>
    /// This class contains undoable action to toggle a dungeon prize.
    /// </summary>
    public class TogglePrizeSection : ITogglePrizeSection
    {
        private readonly ISection _section;
        private readonly bool _force;

        private bool _previousUserManipulated;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="section">
        /// The boss section data for the prize to be toggled.
        /// </param>
        /// <param name="force">
        /// A boolean representing whether to bypass logic on the prize.
        /// </param>
        public TogglePrizeSection(ISection section, bool force)
        {
            _section = section;
            _force = force;
        }

        /// <summary>
        /// Returns whether the action can be executed.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the action can be executed.
        /// </returns>
        public bool CanExecute()
        {
            return _section.CanBeUncleared() || _section.CanBeCleared(_force);
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void ExecuteDo()
        {
            _section.Available = _section.IsAvailable() ? 0 : 1;
            _previousUserManipulated = _section.UserManipulated;
            _section.UserManipulated = true;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void ExecuteUndo()
        {
            _section.Available = _section.IsAvailable() ? 0 : 1;
            _section.UserManipulated = _previousUserManipulated;
        }
    }
}
