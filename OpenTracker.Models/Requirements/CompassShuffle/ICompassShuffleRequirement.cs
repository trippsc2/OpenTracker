namespace OpenTracker.Models.Requirements.CompassShuffle
{
    /// <summary>
    ///     This interface contains compass shuffle requirement data.
    /// </summary>
    public interface ICompassShuffleRequirement : IRequirement
    {
        /// <summary>
        ///     A factory for creating new compass shuffle requirements.
        /// </summary>
        /// <param name="expectedValue">
        ///     A boolean representing the required compass shuffle value.
        /// </param>
        delegate ICompassShuffleRequirement Factory(bool expectedValue);
    }
}