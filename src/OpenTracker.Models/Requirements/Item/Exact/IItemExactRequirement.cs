using OpenTracker.Models.Items;

namespace OpenTracker.Models.Requirements.Item.Exact
{
    /// <summary>
    /// This interface contains <see cref="IItem"/> exact value <see cref="IRequirement"/> data.
    /// </summary>
    public interface IItemExactRequirement : IRequirement
    {
        /// <summary>
        /// A factory for creating new <see cref="IItemExactRequirement"/> objects.
        /// </summary>
        /// <param name="item">
        ///     The <see cref="IItem"/>.
        /// </param>
        /// <param name="count">
        ///     A <see cref="int"/> representing the number of the item required.
        /// </param>
        /// <returns>
        ///     A new <see cref="IItemExactRequirement"/> object.
        /// </returns>
        delegate IItemExactRequirement Factory(IItem item, int count);
    }
}