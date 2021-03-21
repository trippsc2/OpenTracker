using System;
using OpenTracker.Models.Items;

namespace OpenTracker.Models.UndoRedo.Items
{
    /// <summary>
    /// This class contains undoable action data to "remove" crystal requirement item.
    /// </summary>
    public class RemoveCrystalRequirement : IRemoveCrystalRequirement
    {
        private readonly ICrystalRequirementItem _item;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item">
        /// The item data to be manipulated.
        /// </param>
        public RemoveCrystalRequirement(ICrystalRequirementItem item)
        {
            _item = item ?? throw new ArgumentNullException(nameof(item));
        }

        /// <summary>
        /// Returns whether the action can be executed.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the action can be executed.
        /// </returns>
        public bool CanExecute()
        {
            return _item.Known;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void ExecuteDo()
        {
            if (_item.CanRemove())
            {
                _item.Remove();
            }
            else
            {
                _item.Known = false;
            }
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void ExecuteUndo()
        {
            if (_item.Known)
            {
                _item.Add();
            }
            else
            {
                _item.Known = true;
            }
        }
    }
}
