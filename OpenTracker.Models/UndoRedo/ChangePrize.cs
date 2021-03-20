using OpenTracker.Models.Items;
using OpenTracker.Models.PrizePlacements;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This class contains undoable action data to change the dungeon prize.
    /// </summary>
    public class ChangePrize : IUndoable
    {
        private readonly IPrizePlacement _prizePlacement;
        private IItem? _previousValue;

        public delegate ChangePrize Factory(IPrizePlacement prizePlacement);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="prizePlacement">
        /// The boss section data for the dungeon.
        /// </param>
        public ChangePrize(IPrizePlacement prizePlacement)
        {
            _prizePlacement = prizePlacement;
        }

        /// <summary>
        /// Returns whether the action can be executed.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the action can be executed.
        /// </returns>
        public bool CanExecute()
        {
            return _prizePlacement.CanCycle();
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void ExecuteDo()
        {
            _previousValue = _prizePlacement.Prize;
            _prizePlacement.Cycle();
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void ExecuteUndo()
        {
            _prizePlacement.Prize = _previousValue;
        }
    }
}
