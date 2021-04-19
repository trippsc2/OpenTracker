using OpenTracker.Models.PrizePlacements;

namespace OpenTracker.Models.UndoRedo.Prize
{
    /// <summary>
    ///     This class contains undoable action data to change the dungeon prize.
    /// </summary>
    public class ChangePrize : IChangePrize
    {
        private readonly IPrizePlacement _prizePlacement;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="prizePlacement">
        ///     The prize placement to be changed.
        /// </param>
        public ChangePrize(IPrizePlacement prizePlacement)
        {
            _prizePlacement = prizePlacement;
        }

        /// <summary>
        ///     Returns whether the action can be executed.
        /// </summary>
        /// <returns>
        ///     A boolean representing whether the action can be executed.
        /// </returns>
        public bool CanExecute()
        {
            return _prizePlacement.CanCycle();
        }

        /// <summary>
        ///     Executes the action.
        /// </summary>
        public void ExecuteDo()
        {
            _prizePlacement.Cycle();
        }

        /// <summary>
        ///     Undoes the action.
        /// </summary>
        public void ExecuteUndo()
        {
            _prizePlacement.Cycle(true);
        }
    }
}
