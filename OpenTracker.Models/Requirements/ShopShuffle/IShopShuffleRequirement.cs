using OpenTracker.Models.Modes;

namespace OpenTracker.Models.Requirements.ShopShuffle
{
    /// <summary>
    /// This interface contains <see cref="IMode.ShopShuffle"/> <see cref="IRequirement"/> data.
    /// </summary>
    public interface IShopShuffleRequirement : IRequirement
    {
        /// <summary>
        /// A factory for creating new <see cref="IShopShuffleRequirement"/> objects.
        /// </summary>
        /// <param name="expectedValue">
        ///     A <see cref="bool"/> expected <see cref="IMode.ShopShuffle"/> value.
        /// </param>
        /// <returns>
        ///     A new <see cref="IShopShuffleRequirement"/> object.
        /// </returns>
        delegate IShopShuffleRequirement Factory(bool expectedValue);
    }
}