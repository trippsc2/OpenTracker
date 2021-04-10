namespace OpenTracker.Models.Requirements.BossShuffle
{
    /// <summary>
    ///     This interface contains boss shuffle requirement data.
    /// </summary>
    public interface IBossShuffleRequirement : IRequirement
    {
        /// <summary>
        ///     A factory for creating new boss shuffle requirements.
        /// </summary>
        /// <param name="expectedValue">
        ///     The expected boss shuffle value.
        /// </param>
        /// <returns>
        ///     A new boss shuffle requirement.
        /// </returns>
        delegate IBossShuffleRequirement Factory(bool expectedValue);
    }
}