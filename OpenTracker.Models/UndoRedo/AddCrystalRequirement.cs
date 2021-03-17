using OpenTracker.Models.Items;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This class contains undoable action data to "add" crystal requirement item.
    /// </summary>
    public class AddCrystalRequirement : IUndoable
    {
        private readonly ICrystalRequirementItem _item;

        public delegate AddCrystalRequirement Factory(ICrystalRequirementItem item);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item">
        /// The crystal requirement to be added.
        /// </param>
        public AddCrystalRequirement(ICrystalRequirementItem item)
        {
            _item = item;
        }

        /// <summary>
        /// Returns whether the action can be executed.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the action can be executed.
        /// </returns>
        public bool CanExecute()
        {
            return _item.CanAdd();
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void ExecuteDo()
        {
            if (_item.Known)
            {
                _item.Add();
                return;
            }

            _item.Known = true;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void ExecuteUndo()
        {
            if (_item.CanRemove())
            {
                _item.Remove();
                return;
            }

            _item.Known = false;
        }
    }
}
