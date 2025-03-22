using OpenTracker.Models.BossPlacements;

namespace OpenTracker.Models.Requirements.Boss
{
    /// <summary>
    /// This interface contains the creation logic for <see cref="IRequirement"/> objects for boss type requirements.
    /// </summary>
    public interface IBossTypeRequirementFactory
    {
        /// <summary>
        /// A factory for creating the <see cref="IBossTypeRequirementFactory"/> object.
        /// </summary>
        /// <returns>
        ///     The <see cref="IBossTypeRequirementFactory"/> object.
        /// </returns>
        delegate IBossTypeRequirementFactory Factory();
        
        /// <summary>
        /// Returns a new <see cref="IRequirement"/> for the specified <see cref="BossType"/>.
        /// </summary>
        /// <param name="type">
        ///     The nullable <see cref="BossType"/>.
        /// </param>
        /// <returns>
        ///     A new <see cref="IRequirement"/>.
        /// </returns>
        IRequirement GetBossTypeRequirement(BossType? type);
    }
}