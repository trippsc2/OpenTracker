using OpenTracker.Models.Items.Keys;

namespace OpenTracker.Models.Requirements.Item
{
    /// <summary>
    /// This interface contains small key requirement data.
    /// </summary>
    public interface ISmallKeyRequirement
    {
        delegate SmallKeyRequirement Factory(ISmallKeyItem item, int count = 1);
    }
}