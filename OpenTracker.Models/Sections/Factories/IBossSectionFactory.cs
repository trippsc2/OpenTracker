using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Dungeons.AccessibilityProvider;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Sections.Boss;

namespace OpenTracker.Models.Sections.Factories
{
    /// <summary>
    ///     This interface contains the creation logic for boss sections.
    /// </summary>
    public interface IBossSectionFactory
    {
        /// <summary>
        ///     Returns a new boss section for the specified location ID and section index.
        /// </summary>
        /// <param name="accessibilityProvider">
        ///     The accessibility provider.
        /// </param>
        /// <param name="autoTrackValue">
        ///     The auto-track value.
        /// </param>
        /// <param name="id">
        ///     The location ID.
        /// </param>
        /// <param name="index">
        ///     The section index.
        /// </param>
        /// <returns>
        ///     A new boss section.
        /// </returns>
        IBossSection GetBossSection(
            IBossAccessibilityProvider accessibilityProvider, IAutoTrackValue? autoTrackValue, LocationID id,
            int index = 1);
    }
}