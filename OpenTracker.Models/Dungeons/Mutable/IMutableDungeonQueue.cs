namespace OpenTracker.Models.Dungeons.Mutable
{
    /// <summary>
    ///     This interface contains the queue of mutable dungeon data for the specified dungeon.
    /// </summary>
    public interface IMutableDungeonQueue
    {
        /// <summary>
        ///     Re-queues a mutable dungeon data instance.
        /// </summary>
        /// <param name="dungeonData">
        ///     The mutable dungeon data instance to be re-queue.
        /// </param>
        void Requeue(IMutableDungeon dungeonData);
        
        /// <summary>
        ///     Returns the next available mutable dungeon data instance.
        /// </summary>
        /// <returns>
        ///     A mutable dungeon data instance.
        /// </returns>
        IMutableDungeon GetNext();

        /// <summary>
        ///     A factory for creating the mutable dungeon queue for the dungeon.
        /// </summary>
        /// <param name="dungeon">
        ///     The dungeon to which this queue belongs.
        /// </param>
        /// <returns>
        ///     The mutable dungeon queue for the dungeon.
        /// </returns>
        delegate IMutableDungeonQueue Factory(IDungeon dungeon);
    }
}