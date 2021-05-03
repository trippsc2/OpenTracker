using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.AccessibilityProvider;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Sections.Item;

namespace OpenTracker.Models.Sections.Factories
{
    /// <summary>
    ///     This interface contains the creation logic for dungeon item sections.
    /// </summary>
    public interface IDungeonItemSectionFactory
    {
        /// <summary>
        ///     Returns a new dungeon item section for the specified location ID.
        /// </summary>
        /// <param name="dungeon">
        ///     The dungeon.
        /// </param>
        /// <param name="accessibilityProvider">
        ///     The accessibility provider.
        /// </param>
        /// <param name="autoTrackValue">
        ///     The auto-track value.
        /// </param>
        /// <param name="id">
        ///     The location ID.
        /// </param>
        /// <returns>
        ///     A new dungeon item section.
        /// </returns>
        IDungeonItemSection GetDungeonItemSection(
            IDungeon dungeon, IDungeonAccessibilityProvider accessibilityProvider, IAutoTrackValue? autoTrackValue,
            LocationID id);
    }
}