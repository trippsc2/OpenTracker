using OpenTracker.Models.BossPlacements;

namespace OpenTracker.Models.Requirements.Boss
{
    /// <summary>
    /// This interface contains <see cref="IBossPlacement"/> requirement data.
    /// </summary>
    public interface IBossRequirement : IRequirement
    {
        /// <summary>
        /// A factory for creating new <see cref="IBossRequirement"/> objects.
        /// </summary>
        /// <param name="bossPlacement">
        ///     The <see cref="IBossPlacement"/>.
        /// </param>
        /// <returns>
        ///     A new <see cref="IBossRequirement"/> object.
        /// </returns>
        delegate IBossRequirement Factory(IBossPlacement bossPlacement);
    }
}