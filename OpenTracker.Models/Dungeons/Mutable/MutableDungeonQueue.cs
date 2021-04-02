using System;
using System.Collections.Concurrent;

namespace OpenTracker.Models.Dungeons.Mutable
{
    /// <summary>
    ///     This class contains the queue of mutable dungeon data for the specified dungeon.
    /// </summary>
    public class MutableDungeonQueue : ConcurrentQueue<IMutableDungeon>, IMutableDungeonQueue
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="factory">
        ///     An Autofac factory for creating mutable dungeon data.
        /// </param>
        /// <param name="dungeon">
        ///     The dungeon to which this queue belongs.
        /// </param>
        /// <param name="count">
        ///     A nullable 32-bit signed integer representing the number of instances to create.  This defaults to one
        ///         less than the current processor count with a minimum of 1.
        /// </param>
        public MutableDungeonQueue(IMutableDungeon.Factory factory, IDungeon dungeon, int? count = null)
        {
            count ??= Math.Max(1, Environment.ProcessorCount - 1);

            for (var i = 0; i < count; i++)
            {
                var dungeonData = factory(dungeon);
                dungeonData.InitializeData();
                Enqueue(dungeonData);
            }
        }

        public void Requeue(IMutableDungeon dungeonData)
        {
            Enqueue(dungeonData);
        }

        public IMutableDungeon GetNext()
        {
            while (true)
            {
                if (TryDequeue(out var dungeonData))
                {
                    return dungeonData;
                }
            }
        }
    }
}