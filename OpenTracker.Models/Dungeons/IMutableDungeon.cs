﻿using OpenTracker.Models.DungeonItems;
using OpenTracker.Models.DungeonNodes;
using OpenTracker.Models.KeyDoors;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.Dungeons
{
    /// <summary>
    /// This is the interface for the mutable dungeon data.
    /// </summary>
    public interface IMutableDungeon : IDisposable
    {
        Dictionary<KeyDoorID, IKeyDoor> BigKeyDoors { get; }
        List<IDungeonItem> BossItems { get; }
        Dictionary<DungeonItemID, IDungeonItem> Items { get; }
        DungeonNodeDictionary Nodes { get; }
        Dictionary<KeyDoorID, IKeyDoor> SmallKeyDoors { get; }
        DungeonItemDictionary ItemDictionary { get; }
        KeyDoorDictionary KeyDoorDictionary { get; }

        void ApplyState(DungeonState state);
        List<KeyDoorID> GetAccessibleKeyDoors(bool sequenceBreak = false);
        int GetAvailableSmallKeys(bool sequenceBreak = false);
        DungeonResult GetDungeonResult(DungeonState state);
        bool ValidateKeyLayout(DungeonState state);
        void Reset();
    }
}