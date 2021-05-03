namespace OpenTracker.Models.Dungeons
{
    /// <summary>
    ///     This interface contains the creation logic for dungeons.
    /// </summary>
    public interface IDungeonFactory
    {
        /// <summary>
        ///     A factory for creating the dungeon factory.
        /// </summary>
        /// <returns>
        ///     The dungeon factory.
        /// </returns>
        delegate IDungeonFactory Factory();
        
        /// <summary>
        ///     Returns a new dungeon for the specified ID.
        /// </summary>
        /// <param name="id">
        ///     The dungeon ID.
        /// </param>
        /// <returns>
        ///     A new dungeon.
        /// </returns>
        IDungeon GetDungeon(DungeonID id);
    }
}