using OpenTracker.Models.Items.Keys;

namespace OpenTracker.Models.Requirements.Item
{
    /// <summary>
    /// This interface contains small key requirement data.
    /// </summary>
    public interface ISmallKeyRequirement : IRequirement
    {
        delegate ISmallKeyRequirement Factory(ISmallKeyItem item, int count = 1);
    }
}