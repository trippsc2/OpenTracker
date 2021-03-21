using OpenTracker.Models.Items;

namespace OpenTracker.Models.UndoRedo.Items
{
    /// <summary>
    /// This interface contains undoable action data to "add" crystal requirement item.
    /// </summary>
    public interface IAddCrystalRequirement : IUndoable
    {
        delegate IAddCrystalRequirement Factory(ICrystalRequirementItem item);
    }
}