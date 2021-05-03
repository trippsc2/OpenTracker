using OpenTracker.Models.Dungeons.Mutable;

namespace OpenTracker.Models.Dungeons.KeyDoors
{
    /// <summary>
    ///     This interface contains creation logic for key door data.
    /// </summary>
    public interface IKeyDoorFactory
    {
        /// <summary>
        ///     A factory for creating the key door factory.
        /// </summary>
        /// <returns>
        ///     The key door factory.
        /// </returns>
        delegate IKeyDoorFactory Factory();

        /// <summary>
        ///     Returns a new key door for the specified key door ID.
        /// </summary>
        /// <param name="id">
        ///     The key door ID.
        /// </param>
        /// <param name="dungeonData">
        ///     The mutable dungeon data parent class.
        /// </param>
        /// <returns>
        ///     A new key door for the specified key door ID.
        /// </returns>
        IKeyDoor GetKeyDoor(KeyDoorID id, IMutableDungeon dungeonData);
    }
}