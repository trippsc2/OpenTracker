using OpenTracker.Models.Dungeons.Mutable;

namespace OpenTracker.Models.DungeonItems
{
    /// <summary>
    /// This interface contains the creation logic for dungeon items.
    /// </summary>
    public interface IDungeonItemFactory
    {
        delegate IDungeonItemFactory Factory();

        IDungeonItem GetDungeonItem(IMutableDungeon dungeonData, DungeonItemID id);
    }
}