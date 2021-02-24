using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.RequirementNodes;
using System.ComponentModel;

namespace OpenTracker.Models.DungeonItems
{
    /// <summary>
    /// This is the interface for a dungeon item.
    /// </summary>
    public interface IDungeonItem : INotifyPropertyChanged
    {
        AccessibilityLevel Accessibility { get; }

        delegate IDungeonItem Factory(
            IMutableDungeon dungeonData, DungeonItemID id, IRequirementNode node);
    }
}