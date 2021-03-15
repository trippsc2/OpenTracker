using System;
using System.Collections.Generic;
using OpenTracker.Models.Dungeons;

namespace OpenTracker.Models.DungeonItems
{
    /// <summary>
    /// This interface contains the dictionary container of dungeon items.
    /// </summary>
    public interface IDungeonItemDictionary : IDictionary<DungeonItemID, IDungeonItem>,
        ICollection<KeyValuePair<DungeonItemID, IDungeonItem>>
    {
        event EventHandler<KeyValuePair<DungeonItemID, IDungeonItem>>? ItemCreated;

        delegate IDungeonItemDictionary Factory(IMutableDungeon dungeonData);
    }
}