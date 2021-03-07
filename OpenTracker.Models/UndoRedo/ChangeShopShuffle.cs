using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This class contains undoable action data to change the shop shuffle mode setting.
    /// </summary>
    public class ChangeShopShuffle : IUndoable
    {
        private readonly IMode _mode;
        private readonly bool _shopShuffle;
        private bool _previousShopShuffle;

        public delegate ChangeShopShuffle Factory(bool shopShuffle);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode settings.
        /// </param>
        /// <param name="shopShuffle">
        /// The new key drp shuffle setting.
        /// </param>
        public ChangeShopShuffle(IMode mode, bool shopShuffle)
        {
            _mode = mode;
            _shopShuffle = shopShuffle;
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
            _previousShopShuffle = _mode.ShopShuffle;
            _mode.ShopShuffle = _shopShuffle;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void ExecuteUndo()
        {
            _mode.ShopShuffle = _previousShopShuffle;
        }
    }
}
