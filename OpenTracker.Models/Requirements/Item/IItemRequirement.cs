using OpenTracker.Models.Items;

namespace OpenTracker.Models.Requirements.Item
{
    /// <summary>
    ///     This interface contains item requirement data.
    /// </summary>
    public interface IItemRequirement : IRequirement
    {
        /// <summary>
        ///     A factory for creating new item requirements.
        /// </summary>
        /// <param name="item">
        ///     The item of the requirement.
        /// </param>
        /// <param name="count">
        ///     A 32-bit integer representing the number of the item required.
        /// </param>
        /// <returns>
        ///     A new item requirement.
        /// </returns>
        delegate IItemRequirement Factory(IItem item, int count = 1);
    }
}