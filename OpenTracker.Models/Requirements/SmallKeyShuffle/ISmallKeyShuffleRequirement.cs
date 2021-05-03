namespace OpenTracker.Models.Requirements.SmallKeyShuffle
{
    /// <summary>
    ///     This interface contains small key shuffle requirement data.
    /// </summary>
    public interface ISmallKeyShuffleRequirement : IRequirement
    {
        /// <summary>
        ///     A factory for creating new small key shuffle requirements.
        /// </summary>
        /// <param name="expectedValue">
        ///     A boolean expected small key shuffle value.
        /// </param>
        /// <returns>
        ///     A new small key shuffle requirement.
        /// </returns>
        delegate ISmallKeyShuffleRequirement Factory(bool expectedValue);
    }
}