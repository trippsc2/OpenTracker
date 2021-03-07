using OpenTracker.Models.Dungeons;

namespace OpenTracker.Models.DungeonItems
{
    /// <summary>
    /// This is the interface for the creation logic for dungeon items.
    /// </summary>
    public interface IDungeonItemFactory
    {
        delegate IDungeonItemFactory Factory();

        IDungeonItem GetDungeonItem(IMutableDungeon dungeonData, DungeonItemID id);
    }
}