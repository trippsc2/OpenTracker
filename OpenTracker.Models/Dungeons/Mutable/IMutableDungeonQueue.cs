namespace OpenTracker.Models.Dungeons.Mutable
{
    /// <summary>
    /// This interface contains the queue of <see cref="IMutableDungeon"/> objects for the specified dungeon.
    /// </summary>
    public interface IMutableDungeonQueue
    {
        /// <summary>
        /// Re-queues the <see cref="IMutableDungeon"/> object.
        /// </summary>
        /// <param name="dungeonData">
        ///     The <see cref="IMutableDungeon"/> object to be re-queued.
        /// </param>
        void Requeue(IMutableDungeon dungeonData);
        
        /// <summary>
        /// Returns the next available <see cref="IMutableDungeon"/> object.
        /// </summary>
        /// <returns>
        ///     A mutable dungeon data instance.
        /// </returns>
        IMutableDungeon GetNext();

        /// <summary>
        /// A factory for creating new <see cref="IMutableDungeonQueue"/> objects.
        /// </summary>
        /// <param name="dungeon">
        ///     The <see cref="IDungeon"/> to which this queue belongs.
        /// </param>
        /// <returns>
        ///     A new <see cref="IMutableDungeonQueue"/> object.
        /// </returns>
        delegate IMutableDungeonQueue Factory(IDungeon dungeon);
    }
}