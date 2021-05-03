namespace OpenTracker.Models.Requirements.ShopShuffle
{
    /// <summary>
    ///     This interface contains shop shuffle requirement data.
    /// </summary>
    public interface IShopShuffleRequirement : IRequirement
    {
        /// <summary>
        ///     A factory for creating new shop shuffle requirements.
        /// </summary>
        /// <param name="expectedValue">
        ///     A boolean expected shop shuffle shuffle value.
        /// </param>
        /// <returns>
        ///     A new shop shuffle requirement.
        /// </returns>
        delegate IShopShuffleRequirement Factory(bool expectedValue);
    }
}