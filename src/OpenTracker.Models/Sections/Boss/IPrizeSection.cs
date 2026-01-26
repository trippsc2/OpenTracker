using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Dungeons.AccessibilityProvider;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Sections;

namespace OpenTracker.Models.Sections.Boss;

/// <summary>
/// This interface contains end of dungeon boss section with prize data.
/// </summary>
public interface IPrizeSection : IBossSection
{
    /// <summary>
    /// The <see cref="IPrizePlacement"/>.
    /// </summary>
    IPrizePlacement PrizePlacement { get; }
        
    /// <summary>
    /// A factory for creating new <see cref="IPrizeSection"/> objects.
    /// </summary>
    /// <param name="accessibilityProvider">
    ///     The <see cref="IBossAccessibilityProvider"/>.
    /// </param>
    /// <param name="name">
    ///     A <see cref="string"/> representing the section name.
    /// </param>
    /// <param name="bossPlacement">
    ///     The <see cref="IBossPlacement"/>.
    /// </param>
    /// <param name="prizePlacement">
    ///     The <see cref="IPrizePlacement"/>.
    /// </param>
    /// <param name="autoTrackValue">
    ///     The nullable <see cref="IAutoTrackValue"/>.
    /// </param>
    /// <returns>
    ///     A new <see cref="IPrizeSection"/> object.
    /// </returns>
    new delegate IPrizeSection Factory(
        IBossAccessibilityProvider accessibilityProvider, string name, IBossPlacement bossPlacement,
        IPrizePlacement prizePlacement, IAutoTrackValue? autoTrackValue = null);

    /// <summary>
    /// Returns a new <see cref="ITogglePrizeSection"/> objects.
    /// </summary>
    /// <param name="force">
    ///     A <see cref="bool"/> representing whether to ignore the logic.
    /// </param>
    /// <returns>
    ///     A new <see cref="ITogglePrizeSection"/> object.
    /// </returns>
    IUndoable CreateTogglePrizeSectionAction(bool force);
}