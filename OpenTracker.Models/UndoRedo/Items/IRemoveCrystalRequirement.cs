using OpenTracker.Models.Items;

namespace OpenTracker.Models.UndoRedo.Items
{
    /// <summary>
    /// This interface contains undoable action data to "remove" crystal requirement item.
    /// </summary>
    public interface IRemoveCrystalRequirement : IUndoable
    {
        delegate IRemoveCrystalRequirement Factory(ICrystalRequirementItem item);
    }
}