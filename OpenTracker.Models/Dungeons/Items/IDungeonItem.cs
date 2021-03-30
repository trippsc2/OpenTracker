using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.RequirementNodes;
using ReactiveUI;

namespace OpenTracker.Models.Dungeons.Items
{
    /// <summary>
    /// This is the interface for a dungeon item.
    /// </summary>
    public interface IDungeonItem : IReactiveObject
    {
        AccessibilityLevel Accessibility { get; }

        delegate IDungeonItem Factory(
            IMutableDungeon dungeonData, DungeonItemID id, IRequirementNode node);
    }
}