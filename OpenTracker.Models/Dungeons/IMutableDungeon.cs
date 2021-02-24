using OpenTracker.Models.DungeonItems;
using OpenTracker.Models.DungeonNodes;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.Locations;
using System.Collections.Generic;

namespace OpenTracker.Models.Dungeons
{
    /// <summary>
    /// This is the interface for the mutable dungeon data.
    /// </summary>
    public interface IMutableDungeon
    {
        LocationID ID { get; }
        Dictionary<KeyDoorID, IKeyDoor> BigKeyDoors { get; }
        List<IDungeonItem> Bosses { get; }
        Dictionary<DungeonItemID, IDungeonItem> Items { get; }
        IDungeonNodeDictionary Nodes { get; }
        Dictionary<KeyDoorID, IKeyDoor> SmallKeyDoors { get; }
        IDungeonItemDictionary DungeonItems { get; }
        IKeyDoorDictionary KeyDoors { get; }

        delegate IMutableDungeon Factory(IDungeon dungeon);

        void ApplyState(IDungeonState state);
        List<KeyDoorID> GetAccessibleKeyDoors(bool sequenceBreak = false);
        int GetAvailableSmallKeys(bool sequenceBreak = false);
        IDungeonResult GetDungeonResult(IDungeonState state);
        bool ValidateKeyLayout(IDungeonState state);
        void Reset();
    }
}