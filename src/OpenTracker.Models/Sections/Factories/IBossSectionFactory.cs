using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Dungeons.AccessibilityProvider;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Sections.Boss;

namespace OpenTracker.Models.Sections.Factories;

/// <summary>
/// This interface contains the creation logic for <see cref="IBossSection"/> and <see cref="IPrizeSection"/>
/// objects.
/// </summary>
public interface IBossSectionFactory
{
    /// <summary>
    /// Returns a new <see cref="IBossSection"/> or <see cref="IPrizeSection"/> object for the specified
    /// <see cref="LocationID"/> and section index.
    /// </summary>
    /// <param name="accessibilityProvider">
    ///     The <see cref="BossAccessibilityProvider"/>.
    /// </param>
    /// <param name="autoTrackValue">
    ///     The nullable <see cref="IAutoTrackValue"/>.
    /// </param>
    /// <param name="id">
    ///     The <see cref="LocationID"/>.
    /// </param>
    /// <param name="index">
    ///     A <see cref="int"/> representing the section index.
    /// </param>
    /// <returns>
    ///     A new <see cref="IBossSection"/> or <see cref="IPrizeSection"/> object.
    /// </returns>
    IBossSection GetBossSection(
        BossAccessibilityProvider accessibilityProvider, IAutoTrackValue? autoTrackValue, LocationID id,
        int index = 1);
}