using OpenTracker.Models.Items.Keys;

namespace OpenTracker.Models.Requirements.Item.SmallKey
{
    /// <summary>
    /// This interface contains small key <see cref="IRequirement"/> data.
    /// </summary>
    public interface ISmallKeyRequirement : IRequirement
    {
        /// <summary>
        /// A factory for creating new <see cref="ISmallKeyRequirement"/> objects.
        /// </summary>
        /// <param name="item">
        ///     The <see cref="ISmallKeyItem"/>.
        /// </param>
        /// <param name="count">
        ///     A <see cref="int"/> representing the number of the item required.
        /// </param>
        /// <returns>
        ///     A new <see cref="ISmallKeyRequirement"/> object.
        /// </returns>
        delegate ISmallKeyRequirement Factory(ISmallKeyItem item, int count = 1);
    }
}