using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.RequirementNodes;
using ReactiveUI;

namespace OpenTracker.Models.Dungeons.Items
{
    /// <summary>
    ///     This interface contains mutable dungeon item data.
    /// </summary>
    public interface IDungeonItem : IReactiveObject
    {
        /// <summary>
        ///     The accessibility level of the dungeon item.
        /// </summary>
        AccessibilityLevel Accessibility { get; }

        /// <summary>
        ///     A factory for creating dungeon items.
        /// </summary>
        /// <param name="dungeonData">
        ///     The mutable dungeon data parent class.
        /// </param>
        /// <param name="node">
        ///     The dungeon node to which this item belongs.
        /// </param>
        /// <returns>
        ///     A new dungeon item.
        /// </returns>
        delegate IDungeonItem Factory(IMutableDungeon dungeonData, IRequirementNode node);
    }
}