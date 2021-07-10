using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.AccessibilityProvider;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.Sections.Item
{
    /// <summary>
    /// This interface contains dungeon item section data.
    /// </summary>
    public interface IDungeonItemSection : IItemSection
    {
        /// <summary>
        /// A factory for creating new <see cref="IDungeonItemSection"/> objects.
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
        /// <param name="marking">
        ///     The nullable <see cref="IMarking"/>.
        /// </param>
        /// <param name="requirement">
        ///     The <see cref="IRequirement"/> for this section to be visible.
        /// </param>
        /// <returns>
        ///     A new <see cref="IDungeonItemSection"/> object.
        /// </returns>
        new delegate IDungeonItemSection Factory(
            IDungeon dungeon, IDungeonAccessibilityProvider accessibilityProvider,
            IAutoTrackValue? autoTrackValue = null, IMarking? marking = null, IRequirement? requirement = null);
    }
}
