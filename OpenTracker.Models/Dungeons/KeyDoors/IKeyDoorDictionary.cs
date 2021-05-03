using System;
using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Mutable;

namespace OpenTracker.Models.Dungeons.KeyDoors
{
    /// <summary>
    ///     This interface contains the dictionary container for key door data.
    /// </summary>
    public interface IKeyDoorDictionary : IDictionary<KeyDoorID, IKeyDoor>
    {
        /// <summary>
        ///     An event indicating that a new key door was created.
        /// </summary>
        event EventHandler<KeyValuePair<KeyDoorID, IKeyDoor>> ItemCreated;

        /// <summary>
        ///     A factory for creating a key door dictionary.
        /// </summary>
        /// <param name="dungeonData">
        ///     The mutable dungeon data.
        /// </param>
        /// <returns>
        ///     A key door dictionary.
        /// </returns>
        delegate IKeyDoorDictionary Factory(IMutableDungeon dungeonData);

        /// <summary>
        ///     Calls the indexer for each door in the specified list, so that it is initialized.
        /// </summary>
        /// <param name="keyDoors">
        ///     A list of key door IDs for which to call.
        /// </param>
        void PopulateDoors(IList<KeyDoorID> keyDoors);
    }
}