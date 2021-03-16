using System;
using System.Collections.Generic;
using OpenTracker.Models.Dungeons;

namespace OpenTracker.Models.DungeonNodes
{
    /// <summary>
    /// This interface contains the dictionary container for dungeon nodes.
    /// </summary>
    public interface IDungeonNodeDictionary : IDictionary<DungeonNodeID, IDungeonNode>
    {
        event EventHandler<KeyValuePair<DungeonNodeID, IDungeonNode>> ItemCreated;

        delegate IDungeonNodeDictionary Factory(IMutableDungeon dungeonData);
    }
}