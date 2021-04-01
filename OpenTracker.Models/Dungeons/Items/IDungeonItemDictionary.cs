using System;
using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Mutable;

namespace OpenTracker.Models.Dungeons.Items
{
    /// <summary>
    ///     This interface contains the dictionary container of dungeon items.
    /// </summary>
    public interface IDungeonItemDictionary : IDictionary<DungeonItemID, IDungeonItem>
    {
        /// <summary>
        ///     An event indicating that a new dungeon item was created.
        /// </summary>
        event EventHandler<KeyValuePair<DungeonItemID, IDungeonItem>>? ItemCreated;

        /// <summary>
        ///     A factory for creating the dungeon item dictionary.
        /// </summary>
        /// <param name="dungeonData">
        ///     The mutable dungeon data parent class.
        /// </param>
        /// <returns>
        ///     The dungeon item dictionary.
        /// </returns>
        delegate IDungeonItemDictionary Factory(IMutableDungeon dungeonData);
    }
}