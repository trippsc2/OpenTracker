using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.AccessibilityProvider;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Sections.Item;

namespace OpenTracker.Models.Sections.Factories
{
    /// <summary>
    /// This interface contains the creation logic for <see cref="IDungeonItemSection"/> objects.
    /// </summary>
    public interface IDungeonItemSectionFactory
    {
        /// <summary>
        /// Returns a new <see cref="IDungeonItemSection"/> object for the specified <see cref="LocationID"/>.
        /// </summary>
        /// <param name="dungeon">
        ///     The <see cref="IDungeon"/>.
        /// </param>
        /// <param name="accessibilityProvider">
        ///     The <see cref="IDungeonAccessibilityProvider"/>.
        /// </param>
        /// <param name="autoTrackValue">
        ///     The nullable <see cref="IAutoTrackValue"/>.
        /// </param>
        /// <param name="id">
        ///     The <see cref="LocationID"/>.
        /// </param>
        /// <returns>
        ///     A new <see cref="IDungeonItemSection"/> object.
        /// </returns>
        IDungeonItemSection GetDungeonItemSection(
            IDungeon dungeon, IDungeonAccessibilityProvider accessibilityProvider, IAutoTrackValue? autoTrackValue,
            LocationID id);
    }
}