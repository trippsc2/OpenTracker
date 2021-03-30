using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.Nodes;
using OpenTracker.Models.Dungeons.Result;
using OpenTracker.Models.Dungeons.State;
using OpenTracker.Models.KeyDoors;

namespace OpenTracker.Models.Dungeons.Mutable
{
    /// <summary>
    /// This interface contains the mutable dungeon data.
    /// </summary>
    public interface IMutableDungeon
    {
        IDungeonNodeDictionary Nodes { get; }
        IDungeonItemDictionary DungeonItems { get; }
        IKeyDoorDictionary KeyDoors { get; }

        delegate IMutableDungeon Factory(IDungeon dungeon);

        void ApplyState(IDungeonState state);
        List<KeyDoorID> GetAccessibleKeyDoors(bool sequenceBreak = false);
        int GetAvailableSmallKeys(bool sequenceBreak = false);
        IDungeonResult GetDungeonResult(IDungeonState state);
        bool ValidateKeyLayout(IDungeonState state);
        void Reset();

        /// <summary>
        /// Initialize all data in this dungeon data instance.
        /// </summary>
        void InitializeData();
    }
}