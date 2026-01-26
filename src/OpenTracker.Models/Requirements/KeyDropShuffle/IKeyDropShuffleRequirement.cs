using OpenTracker.Models.Modes;

namespace OpenTracker.Models.Requirements.KeyDropShuffle;

/// <summary>
/// This interface contains the <see cref="IMode.KeyDropShuffle"/> <see cref="IRequirement"/> data.
/// </summary>
public interface IKeyDropShuffleRequirement : IRequirement
{
    /// <summary>
    /// A factory for creating new <see cref="IKeyDropShuffleRequirement"/> objects.
    /// </summary>
    /// <param name="expectedValue">
    ///     A <see cref="bool"/> representing the expected <see cref="IMode.KeyDropShuffle"/> value.
    /// </param>
    /// <returns>
    ///     A new <see cref="IKeyDropShuffleRequirement"/> object.
    /// </returns>
    delegate IKeyDropShuffleRequirement Factory(bool expectedValue);
}