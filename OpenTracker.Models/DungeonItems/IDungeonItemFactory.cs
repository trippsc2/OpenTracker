using OpenTracker.Models.Dungeons;
using OpenTracker.Models.RequirementNodes;

namespace OpenTracker.Models.DungeonItems
{
    /// <summary>
    /// This is the interface for creating dungeon items.
    /// </summary>
    public interface IDungeonItemFactory
    {
        delegate IDungeonItemFactory Factory();

        IDungeonItem GetDungeonItem(IMutableDungeon dungeonData, DungeonItemID id);
    }
}