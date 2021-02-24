using OpenTracker.Models.Items;
using System;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This is the class for an undoable action to "remove" crystal requirement item.
    /// </summary>
    public class RemoveCrystalRequirement : IUndoable
    {
        private readonly ICrystalRequirementItem _item;

        public delegate RemoveCrystalRequirement Factory(ICrystalRequirementItem item);

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
        public void Execute()
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
        public void Undo()
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
