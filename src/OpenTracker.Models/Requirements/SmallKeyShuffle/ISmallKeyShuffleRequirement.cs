using OpenTracker.Models.Modes;

namespace OpenTracker.Models.Requirements.SmallKeyShuffle
{
    /// <summary>
    /// This interface contains the <see cref="IMode.SmallKeyShuffle"/> <see cref="IRequirement"/> data.
    /// </summary>
    public interface ISmallKeyShuffleRequirement : IRequirement
    {
        /// <summary>
        /// A factory for creating new <see cref="ISmallKeyShuffleRequirement"/> objects.
        /// </summary>
        /// <param name="expectedValue">
        ///     A <see cref="bool"/> expected <see cref="IMode.SmallKeyShuffle"/> value.
        /// </param>
        /// <returns>
        ///     A new <see cref="ISmallKeyShuffleRequirement"/> object.
        /// </returns>
        delegate ISmallKeyShuffleRequirement Factory(bool expectedValue);
    }
}