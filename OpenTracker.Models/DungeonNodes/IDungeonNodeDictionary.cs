using OpenTracker.Models.Dungeons;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.DungeonNodes
{
    public interface IDungeonNodeDictionary : IDictionary<DungeonNodeID, IDungeonNode>,
        ICollection<KeyValuePair<DungeonNodeID, IDungeonNode>>
    {
        event EventHandler<KeyValuePair<DungeonNodeID, IDungeonNode>> ItemCreated;

        delegate IDungeonNodeDictionary Factory(IMutableDungeon dungeonData);
    }
}