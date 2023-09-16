using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Dungeons.AccessibilityProvider;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.Sections.Boss;

/// <summary>
/// This interface contains boss section data.
/// </summary>
public interface IBossSection : ISection
{
    /// <summary>
    /// The <see cref="IBossPlacement"/>.
    /// </summary>
    IBossPlacement BossPlacement { get; }

    /// <summary>
    /// A factory for creating new <see cref="IBossSection"/> objects.
    /// </summary>
    /// <param name="accessibilityProvider">
    ///     The <see cref="BossAccessibilityProvider"/>.
    /// </param>
    /// <param name="name">
    ///     A <see cref="string"/> representing the section name.
    /// </param>
    /// <param name="bossPlacement">
    ///     The <see cref="IBossPlacement"/>.
    /// </param>
    /// <param name="requirement">
    ///     The <see cref="IRequirement"/> for the section to be active.
    /// </param>
    /// <returns>
    ///     A new <see cref="IBossSection"/> object.
    /// </returns>
    delegate IBossSection Factory(
        BossAccessibilityProvider accessibilityProvider, string name, IBossPlacement bossPlacement, 
        IRequirement? requirement = null);
}