using OpenTracker.Models.Items;

namespace OpenTracker.Models.Requirements.Item.Exact
{
    /// <summary>
    /// This interface contains item exact value requirement data.
    /// </summary>
    public interface IItemExactRequirement : IRequirement
    {
        delegate IItemExactRequirement Factory(IItem item, int count);
    }
}