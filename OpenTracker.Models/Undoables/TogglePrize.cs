using OpenTracker.Models.Sections;

namespace OpenTracker.Models.Undoables
{
    /// <summary>
    /// This is the class for an undoable action to toggle a dungeon prize.
    /// </summary>
    public class TogglePrize : IUndoable
    {
        private readonly BossSection _prizeSection;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="prizeSection">
        /// The boss section data for the prize to be toggled.
        /// </param>
        public TogglePrize(BossSection prizeSection)
        {
            _prizeSection = prizeSection;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void Execute()
        {
            if (_prizeSection.IsAvailable())
            {
                _prizeSection.Available = 0;
            }
            else
            {
                _prizeSection.Available = 1;
            }
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            if (_prizeSection.IsAvailable())
            {
                _prizeSection.Available = 0;
            }
            else
            {
                _prizeSection.Available = 1;
            }
        }
    }
}
