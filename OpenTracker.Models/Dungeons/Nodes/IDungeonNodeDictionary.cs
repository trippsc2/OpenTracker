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
        event EventHandler<KeyValuePair<DungeonNodeID, IDungeonNode>> ItemCreated;

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