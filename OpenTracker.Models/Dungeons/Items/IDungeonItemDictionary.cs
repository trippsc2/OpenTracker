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
        ///     The mutable dungeon data.
        /// </param>
        /// <param name="dungeon">
        ///     The dungeon data.
        /// </param>
        /// <returns>
        ///     The dungeon item dictionary.
        /// </returns>
        delegate IDungeonItemDictionary Factory(IMutableDungeon dungeonData, IDungeon dungeon);

        /// <summary>
        ///     Calls the indexer for each item in the specified list, so that it is initialized.
        /// </summary>
        /// <param name="items">
        ///     A list of dungeon item IDs for which to call.
        /// </param>
        void PopulateItems(IList<DungeonItemID> items);
    }
}