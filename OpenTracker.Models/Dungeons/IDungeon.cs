using OpenTracker.Models.DungeonItems;
using OpenTracker.Models.Items;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.KeyLayouts;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Sections;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace OpenTracker.Models.Dungeons
{
    /// <summary>
    /// This is the interface for dungeon data.
    /// </summary>
    public interface IDungeon : ILocation
    {
        int BigKey { get; }
        IItem BigKeyItem { get; }
        List<DungeonItemID> Bosses { get; }
        int Compass { get; }
        List<DungeonItemID> Items { get; }
        List<IKeyLayout> KeyLayouts { get; }
        int Map { get; }
        IItem SmallKeyItem { get; }
        int SmallKeys { get; }
        List<KeyDoorID> SmallKeyDoors { get; }
        List<KeyDoorID> BigKeyDoors { get; }
        ConcurrentQueue<IMutableDungeon> DungeonDataQueue { get; }

        event EventHandler<IMutableDungeon> DungeonDataCreated;
    }
}