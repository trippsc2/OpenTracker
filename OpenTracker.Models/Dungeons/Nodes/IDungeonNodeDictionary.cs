using System;
using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Mutable;

namespace OpenTracker.Models.Dungeons.Nodes
{
    /// <summary>
    ///     This interface contains the dictionary container for dungeon nodes.
    /// </summary>
    public interface IDungeonNodeDictionary : IDictionary<DungeonNodeID, IDungeonNode>
    {
        /// <summary>
        ///     The event raised when a new item is created in the dictionary.
        /// </summary>
        event EventHandler<KeyValuePair<DungeonNodeID, IDungeonNode>> ItemCreated;

        /// <summary>
        ///     A factory for creating the dungeon node dictionary for the specified mutable dungeon data instance.
        /// </summary>
        /// <param name="dungeonData">
        ///     The mutable dungeon data.
        /// </param>
        /// <returns>
        ///     The dungeon node dictionary.
        /// </returns>
        delegate IDungeonNodeDictionary Factory(IMutableDungeon dungeonData);

        /// <summary>
        ///     Calls the indexer for each node in the dungeon, so that it is initialized.
        /// </summary>
        /// <param name="dungeonNodes">
        ///     A list of dungeon node IDs for which to call.
        /// </param>
        void PopulateNodes(IList<DungeonNodeID> dungeonNodes);
    }
}