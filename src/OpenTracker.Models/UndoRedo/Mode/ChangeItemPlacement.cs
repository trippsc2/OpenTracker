using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo.Mode
{
    /// <summary>
    /// This class contains the <see cref="IUndoable"/> action to change the <see cref="IMode.ItemPlacement"/> property.
    /// </summary>
    public class ChangeItemPlacement : IChangeItemPlacement
    {
        private readonly IMode _mode;
        private readonly ItemPlacement _newValue;
        private ItemPlacement _previousValue;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        ///     The <see cref="IMode"/> data.
        /// </param>
        /// <param name="newValue">
        ///     A <see cref="ItemPlacement"/> representing the new <see cref="IMode.ItemPlacement"/> value.
        /// </param>
        public ChangeItemPlacement(IMode mode, ItemPlacement newValue)
        {
            _mode = mode;
            _newValue = newValue;
        }

        public bool CanExecute()
        {
            return true;
        }

        public void ExecuteDo()
        {
            _previousValue = _mode.ItemPlacement;
            _mode.ItemPlacement = _newValue;
        }

        public void ExecuteUndo()
        {
            _mode.ItemPlacement = _previousValue;
        }
    }
}
