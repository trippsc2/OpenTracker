using OpenTracker.Models.Dungeons.Mutable;

namespace OpenTracker.Models.Dungeons.Items
{
    /// <summary>
    /// This interface contains the creation logic for <see cref="IDungeonItem"/> objects.
    /// </summary>
    public interface IDungeonItemFactory
    {
        /// <summary>
        /// A factory for creating the <see cref="IDungeonItemFactory"/> objects.
        /// </summary>
        /// <returns>
        ///     The <see cref="IDungeonItemFactory"/> object.
        /// </returns>
        delegate IDungeonItemFactory Factory();

        /// <summary>
        /// Returns a new dungeon item for the specified dungeon data and dungeon item ID.
        /// </summary>
        /// <param name="dungeonData">
        ///     The dungeon data.
        /// </param>
        /// <param name="id">
        ///     The dungeon item ID.
        /// </param>
        /// <returns>
        ///     A new dungeon item.
        /// </returns>
        IDungeonItem GetDungeonItem(IMutableDungeon dungeonData, DungeonItemID id);
    }
}