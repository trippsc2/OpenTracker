using OpenTracker.Models.Modes;

namespace OpenTracker.Models.Requirements.CompassShuffle
{
    /// <summary>
    /// This interface contains <see cref="IMode.CompassShuffle"/> requirement data.
    /// </summary>
    public interface ICompassShuffleRequirement : IRequirement
    {
        /// <summary>
        /// A factory for creating new <see cref="ICompassShuffleRequirement"/> objects.
        /// </summary>
        /// <param name="expectedValue">
        ///     A <see cref="bool"/> representing the expected <see cref="IMode.CompassShuffle"/> value.
        /// </param>
        /// <returns>
        ///     A new <see cref="ICompassShuffleRequirement"/> object.
        /// </returns>
        delegate ICompassShuffleRequirement Factory(bool expectedValue);
    }
}