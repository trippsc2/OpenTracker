using OpenTracker.Models.Modes;

namespace OpenTracker.Models.Requirements.Mode
{
    /// <summary>
    ///     This interface contains entrance shuffle requirement data.
    /// </summary>
    public interface IEntranceShuffleRequirement : IRequirement
    {
        /// <summary>
        ///     A factory for creating new entrance shuffle requirements.
        /// </summary>
        /// <param name="expectedValue">
        ///     The required entrance shuffle value.
        /// </param>
        /// <returns>
        ///     A new entrance shuffle requirement.
        /// </returns>
        delegate IEntranceShuffleRequirement Factory(EntranceShuffle expectedValue);
    }
}