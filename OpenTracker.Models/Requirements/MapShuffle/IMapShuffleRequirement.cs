namespace OpenTracker.Models.Requirements.MapShuffle
{
    /// <summary>
    ///     This interface contains map shuffle setting requirement data.
    /// </summary>
    public interface IMapShuffleRequirement : IRequirement
    {
        /// <summary>
        ///     A factory for creating new map shuffle requirements.
        /// </summary>
        /// <param name="expectedValue">
        ///     A boolean expected map shuffle value.
        /// </param>
        /// <returns>
        ///     A new map shuffle requirement.
        /// </returns>
        delegate IMapShuffleRequirement Factory(bool expectedValue);
    }
}