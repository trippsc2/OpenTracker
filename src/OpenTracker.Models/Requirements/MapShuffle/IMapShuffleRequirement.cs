using OpenTracker.Models.Modes;

namespace OpenTracker.Models.Requirements.MapShuffle
{
    /// <summary>
    /// This interface contains the <see cref="IMode.MapShuffle"/> <see cref="IRequirement"/> data.
    /// </summary>
    public interface IMapShuffleRequirement : IRequirement
    {
        /// <summary>
        /// A factory for creating new <see cref="IMapShuffleRequirement"/> objects.
        /// </summary>
        /// <param name="expectedValue">
        ///     A <see cref="bool"/> representing the expected <see cref="IMode.MapShuffle"/> value.
        /// </param>
        /// <returns>
        ///     A new <see cref="IMapShuffleRequirement"/> object.
        /// </returns>
        delegate IMapShuffleRequirement Factory(bool expectedValue);
    }
}