using OpenTracker.Models.Modes;

namespace OpenTracker.Models.Requirements.Mode;

/// <summary>
/// This interface contains <see cref="IMode.WorldState"/> <see cref="IRequirement"/> data.
/// </summary>
public interface IWorldStateRequirement : IRequirement
{
    /// <summary>
    /// A factory for creating new <see cref="IWorldStateRequirement"/> objects.
    /// </summary>
    /// <param name="expectedValue">
    ///     A <see cref="WorldState"/> representing the expected <see cref="IMode.WorldState"/> value.
    /// </param>
    /// <returns>
    ///     A new <see cref="IWorldStateRequirement"/> object.
    /// </returns>
    delegate IWorldStateRequirement Factory(WorldState expectedValue);
}