using OpenTracker.Models.BossPlacements;

namespace OpenTracker.Models.Requirements.Boss
{
    /// <summary>
    ///     This interface contains the creation logic for boss type requirements.
    /// </summary>
    public interface IBossTypeRequirementFactory
    {
        /// <summary>
        ///     A factory for creating the boss type requirement factory.
        /// </summary>
        /// <returns>
        ///     The boss type requirement factory.
        /// </returns>
        delegate IBossTypeRequirementFactory Factory();
        
        /// <summary>
        ///     Returns a new boss type requirement for the specified type.
        /// </summary>
        /// <param name="type">
        ///     A nullable boss type.
        /// </param>
        /// <returns>
        ///     A new boss type requirement.
        /// </returns>
        IRequirement GetBossTypeRequirement(BossType? type);
    }
}