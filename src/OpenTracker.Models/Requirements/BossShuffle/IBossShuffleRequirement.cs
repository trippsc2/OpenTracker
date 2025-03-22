using OpenTracker.Models.Modes;

namespace OpenTracker.Models.Requirements.BossShuffle
{
    /// <summary>
    /// This interface contains <see cref="IMode.BossShuffle"/> requirement data.
    /// </summary>
    public interface IBossShuffleRequirement : IRequirement
    {
        /// <summary>
        ///     A factory for creating new <see cref="IBossShuffleRequirement"/> objects.
        /// </summary>
        /// <param name="expectedValue">
        ///     A <see cref="bool"/> representing the expected <see cref="IMode.BossShuffle"/> value.
        /// </param>
        /// <returns>
        ///     A new <see cref="IBossShuffleRequirement"/> object.
        /// </returns>
        delegate IBossShuffleRequirement Factory(bool expectedValue);
    }
}