using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.AccessibilityProvider;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.Sections.Item
{
    /// <summary>
    ///     This interface contains dungeon item section data.
    /// </summary>
    public interface IDungeonItemSection : IItemSection
    {
        /// <summary>
        ///     A factory for creating new dungeon item sections.
        /// </summary>
        /// <param name="dungeon">
        ///     The dungeon data.
        /// </param>
        /// <param name="accessibilityProvider">
        ///     The dungeon accessibility provider.
        /// </param>
        /// <param name="autoTrackValue">
        ///     The section auto track value.
        /// </param>
        /// <param name="marking">
        ///     The section marking.
        /// </param>
        /// <param name="requirement">
        ///     The requirement for this section to be visible.
        /// </param>
        /// <returns>
        ///     A new dungeon item section.
        /// </returns>
        new delegate IDungeonItemSection Factory(
            IDungeon dungeon, IDungeonAccessibilityProvider accessibilityProvider,
            IAutoTrackValue? autoTrackValue = null, IMarking? marking = null, IRequirement? requirement = null);
    }
}
