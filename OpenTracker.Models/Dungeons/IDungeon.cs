using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using OpenTracker.Models.DungeonItems;
using OpenTracker.Models.DungeonNodes;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Items;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.KeyLayouts;
using OpenTracker.Models.Locations;

namespace OpenTracker.Models.Dungeons
{
    /// <summary>
    /// This interface contains the dungeon data.
    /// </summary>
    public interface IDungeon : ILocation
    {
        int BigKey { get; }
        ICappedItem? BigKeyItem { get; }
        List<DungeonItemID> Bosses { get; }
        int Compass { get; }
        List<DungeonItemID> DungeonItems { get; }
        List<IKeyLayout> KeyLayouts { get; }
        int Map { get; }
        IKeyItem SmallKeyItem { get; }
        int SmallKeys { get; }
        List<KeyDoorID> SmallKeyDoors { get; }
        List<KeyDoorID> BigKeyDoors { get; }
        ConcurrentQueue<IMutableDungeon> DungeonDataQueue { get; }
        List<DungeonNodeID> Nodes { get; }
        ICappedItem? MapItem { get; }
        ICappedItem? CompassItem { get; }
        List<DungeonItemID> SmallKeyDrops { get; }
        List<DungeonItemID> BigKeyDrops { get; }

        event EventHandler<IMutableDungeon>? DungeonDataCreated;

        delegate IDungeon Factory(LocationID id);

        void FinishMutableDungeonCreation(IMutableDungeon dungeonData);
    }
}