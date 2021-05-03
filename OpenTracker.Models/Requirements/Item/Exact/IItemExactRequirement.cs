using OpenTracker.Models.Items;

namespace OpenTracker.Models.Requirements.Item.Exact
{
    /// <summary>
    ///     This interface contains item exact value requirement data.
    /// </summary>
    public interface IItemExactRequirement : IRequirement
    {
        /// <summary>
        ///     A factory for creating new item exact requirements.
        /// </summary>
        /// <param name="item">
        ///     The item of the requirement.
        /// </param>
        /// <param name="count">
        ///     A 32-bit integer representing the number of the item required.
        /// </param>
        /// <returns>
        ///     A new item exact requirement.
        /// </returns>
        delegate IItemExactRequirement Factory(IItem item, int count);
    }
}