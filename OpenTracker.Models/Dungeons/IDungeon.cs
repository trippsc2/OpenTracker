using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using OpenTracker.Models.DungeonItems;
using OpenTracker.Models.DungeonNodes;
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
        IItem? BigKeyItem { get; }
        List<DungeonItemID> Bosses { get; }
        int Compass { get; }
        List<DungeonItemID> DungeonItems { get; }
        List<IKeyLayout> KeyLayouts { get; }
        int Map { get; }
        IItem SmallKeyItem { get; }
        int SmallKeys { get; }
        List<KeyDoorID> SmallKeyDoors { get; }
        List<KeyDoorID> BigKeyDoors { get; }
        ConcurrentQueue<IMutableDungeon> DungeonDataQueue { get; }
        List<DungeonNodeID> Nodes { get; }
        IItem? MapItem { get; }
        IItem? CompassItem { get; }
        List<DungeonItemID> SmallKeyDrops { get; }
        List<DungeonItemID> BigKeyDrops { get; }

        event EventHandler<IMutableDungeon>? DungeonDataCreated;

        new delegate IDungeon Factory(LocationID id);

        void FinishMutableDungeonCreation(IMutableDungeon dungeonData);
    }
}