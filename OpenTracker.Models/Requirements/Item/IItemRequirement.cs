using OpenTracker.Models.Items;

namespace OpenTracker.Models.Requirements.Item
{
    /// <summary>
    /// This interface contains item requirement data.
    /// </summary>
    public interface IItemRequirement : IRequirement
    {
        delegate IItemRequirement Factory(IItem item, int count = 1);
    }
}