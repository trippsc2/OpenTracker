using OpenTracker.Models.Dungeons;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.DungeonItems
{
    /// <summary>
    /// This is the interface is the dictionary container of dungeon items.
    /// </summary>
    public interface IDungeonItemDictionary : IDictionary<DungeonItemID, IDungeonItem>,
        ICollection<KeyValuePair<DungeonItemID, IDungeonItem>>
    {
        event EventHandler<KeyValuePair<DungeonItemID, IDungeonItem>>? ItemCreated;

        delegate IDungeonItemDictionary Factory(IMutableDungeon dungeonData);
    }
}