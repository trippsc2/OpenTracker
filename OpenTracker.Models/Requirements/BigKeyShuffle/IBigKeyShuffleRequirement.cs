namespace OpenTracker.Models.Requirements.BigKeyShuffle
{
    /// <summary>
    ///     This interface contains big key shuffle requirement data.
    /// </summary>
    public interface IBigKeyShuffleRequirement : IRequirement
    {
        /// <summary>
        ///     A factory for creating new big key shuffle requirements.
        /// </summary>
        /// <param name="expectedValue">
        ///     The expected big key shuffle value.
        /// </param>
        /// <returns>
        ///     A new big key shuffle requirement.
        /// </returns>
        delegate IBigKeyShuffleRequirement Factory(bool expectedValue);
    }
}