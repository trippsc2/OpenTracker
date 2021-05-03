using OpenTracker.Models.Modes;

namespace OpenTracker.Models.Requirements.Mode
{
    /// <summary>
    ///     This interface contains world state requirement data.
    /// </summary>
    public interface IWorldStateRequirement : IRequirement
    {
        /// <summary>
        ///     A factory for creating new world state requirements.
        /// </summary>
        /// <param name="expectedValue">
        ///     The required world state.
        /// </param>
        /// <returns>
        ///     A new world state requirement.
        /// </returns>
        delegate IWorldStateRequirement Factory(WorldState expectedValue);
    }
}