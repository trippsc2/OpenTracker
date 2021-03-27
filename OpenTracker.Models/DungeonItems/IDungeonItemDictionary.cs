using System;
using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Mutable;

namespace OpenTracker.Models.DungeonItems
{
    /// <summary>
    /// This interface contains the dictionary container of dungeon items.
    /// </summary>
    public interface IDungeonItemDictionary : IDictionary<DungeonItemID, IDungeonItem>
    {
        event EventHandler<KeyValuePair<DungeonItemID, IDungeonItem>>? ItemCreated;

        delegate IDungeonItemDictionary Factory(IMutableDungeon dungeonData);
    }
}