using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Dungeons.AccessibilityProvider;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.Sections.Boss
{
    /// <summary>
    ///     This interface contains boss section data.  It will be used directly for GT LW boss re-fights and as a base
    ///         class for final bosses that provide a prize.
    /// </summary>
    public interface IBossSection : ISection
    {
        /// <summary>
        ///     The boss placement for this section.
        /// </summary>
        IBossPlacement BossPlacement { get; }

        /// <summary>
        ///     A factory for creating new boss sections.
        /// </summary>
        /// <param name="accessibilityProvider">
        ///     The boss accessibility provider for this section.
        /// </param>
        /// <param name="name">
        ///     A string representing the name of the section.
        /// </param>
        /// <param name="bossPlacement">
        ///     The boss placement for the section.
        /// </param>
        /// <param name="requirement">
        ///     The requirement for the section to be active.
        /// </param>
        /// <returns>
        ///     A new boss section.
        /// </returns>
        delegate IBossSection Factory(
            IBossAccessibilityProvider accessibilityProvider, string name, IBossPlacement bossPlacement, 
            IRequirement? requirement = null);
    }
}