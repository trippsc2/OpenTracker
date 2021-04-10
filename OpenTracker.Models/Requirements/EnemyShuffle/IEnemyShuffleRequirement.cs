namespace OpenTracker.Models.Requirements.EnemyShuffle
{
    /// <summary>
    ///     This interface contains enemy shuffle requirement data.
    /// </summary>
    public interface IEnemyShuffleRequirement : IRequirement
    {
        /// <summary>
        ///     A factory for creating new enemy shuffle requirements.
        /// </summary>
        /// <param name="expectedValue">
        ///     The required enemy shuffle value.
        /// </param>
        /// <returns>
        ///     A new enemy shuffle requirement.
        /// </returns>
        delegate IEnemyShuffleRequirement Factory(bool expectedValue);
    }
}