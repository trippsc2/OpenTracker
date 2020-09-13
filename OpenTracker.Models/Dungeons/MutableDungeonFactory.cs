using System;

namespace OpenTracker.Models.Dungeons
{
    /// <summary>
    /// This is the class for creating mutable dungeon data.
    /// </summary>
    public static class MutableDungeonFactory
    {
        /// <summary>
        /// Returns a new mutable dungeon data instance for the specified dungeon.
        /// </summary>
        /// <param name="dungeon">
        /// The dungeon immutable dungeon data parent class.
        /// </param>
        /// <returns>
        /// A new mutable dungeon data instance.
        /// </returns>
        public static IMutableDungeon GetMutableDungeon(IDungeon dungeon)
        {
            if (dungeon == null)
            {
                throw new ArgumentNullException(nameof(dungeon));
            }

            return new MutableDungeon(dungeon);
        }
    }
}
