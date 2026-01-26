using OpenTracker.Models.Modes;

namespace OpenTracker.Models.Requirements.EnemyShuffle;

/// <summary>
/// This interface contains <see cref="IMode.EnemyShuffle"/> requirement data.
/// </summary>
public interface IEnemyShuffleRequirement : IRequirement
{
    /// <summary>
    /// A factory for creating new <see cref="IEnemyShuffleRequirement"/> objects.
    /// </summary>
    /// <param name="expectedValue">
    ///     A <see cref="bool"/> representing the expected <see cref="IMode.EnemyShuffle"/> value.
    /// </param>
    /// <returns>
    ///     A new <see cref="IEnemyShuffleRequirement"/> object.
    /// </returns>
    delegate IEnemyShuffleRequirement Factory(bool expectedValue);
}