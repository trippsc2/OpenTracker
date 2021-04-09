namespace OpenTracker.Models.Requirements.KeyDropShuffle
{
    /// <summary>
    ///     This interface contains key drop shuffle setting requirement data.
    /// </summary>
    public interface IKeyDropShuffleRequirement : IRequirement
    {
        /// <summary>
        ///     A factory for creating new key drop shuffle requirements.
        /// </summary>
        /// <param name="expectedValue">
        ///     A boolean expected key door shuffle value.
        /// </param>
        /// <returns>
        ///     A new key drop shuffle requirement.
        /// </returns>
        delegate IKeyDropShuffleRequirement Factory(bool expectedValue);
    }
}