namespace OpenTracker.Models.Dungeons
{
    /// <summary>
    /// This interface contains the creation logic for <see cref="IDungeon"/> objects.
    /// </summary>
    public interface IDungeonFactory
    {
        /// <summary>
        /// A factory for creating the <see cref="IDungeonFactory"/> object.
        /// </summary>
        /// <returns>
        ///     The <see cref="IDungeonFactory"/> object.
        /// </returns>
        delegate IDungeonFactory Factory();
        
        /// <summary>
        /// Returns a new <see cref="IDungeon"/> object for the specified <see cref="DungeonID"/>.
        /// </summary>
        /// <param name="id">
        ///     The <see cref="DungeonID"/>.
        /// </param>
        /// <returns>
        ///     A new <see cref="IDungeon"/> object.
        /// </returns>
        IDungeon GetDungeon(DungeonID id);
    }
}