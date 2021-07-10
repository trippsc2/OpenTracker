using OpenTracker.Models.Modes;

namespace OpenTracker.Models.Requirements.BigKeyShuffle
{
    /// <summary>
    /// This interface contains <see cref="IMode.BigKeyShuffle"/> requirement data.
    /// </summary>
    public interface IBigKeyShuffleRequirement : IRequirement
    {
        /// <summary>
        /// A factory for creating new <see cref="IBigKeyShuffleRequirement"/> objects.
        /// </summary>
        /// <param name="expectedValue">
        ///     A <see cref="bool"/> representing the expected value.
        /// </param>
        /// <returns>
        ///     A new <see cref="IBigKeyShuffleRequirement"/> object.
        /// </returns>
        delegate IBigKeyShuffleRequirement Factory(bool expectedValue);
    }
}