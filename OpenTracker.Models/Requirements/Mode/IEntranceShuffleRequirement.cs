using OpenTracker.Models.Modes;

namespace OpenTracker.Models.Requirements.Mode;

/// <summary>
/// This interface contains <see cref="IMode.EntranceShuffle"/> <see cref="IRequirement"/> data.
/// </summary>
public interface IEntranceShuffleRequirement : IRequirement
{
    /// <summary>
    /// A factory for creating new <see cref="IEntranceShuffleRequirement"/> objects.
    /// </summary>
    /// <param name="expectedValue">
    ///     A <see cref="EntranceShuffle"/> representing the expected <see cref="IMode.EntranceShuffle"/> value.
    /// </param>
    /// <returns>
    ///     A new <see cref="IEntranceShuffleRequirement"/> object.
    /// </returns>
    delegate IEntranceShuffleRequirement Factory(EntranceShuffle expectedValue);
}