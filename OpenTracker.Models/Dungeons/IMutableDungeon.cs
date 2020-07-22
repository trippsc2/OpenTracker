using OpenTracker.Models.DungeonItems;
using OpenTracker.Models.DungeonNodes;
using OpenTracker.Models.KeyDoors;
using System.Collections.Generic;

namespace OpenTracker.Models.Dungeons
{
    /// <summary>
    /// This is the interface for the mutable dungeon data.
    /// </summary>
    public interface IMutableDungeon
    {
        Dictionary<KeyDoorID, IKeyDoor> BigKeyDoors { get; }
        List<IDungeonItem> BossItems { get; }
        Dictionary<DungeonItemID, IDungeonItem> Items { get; }
        DungeonNodeDictionary RequirementNodes { get; }
        Dictionary<KeyDoorID, IKeyDoor> SmallKeyDoors { get; }
        DungeonItemDictionary ItemDictionary { get; }
        KeyDoorDictionary KeyDoorDictionary { get; }

        List<KeyDoorID> GetAccessibleKeyDoors();
        List<AccessibilityLevel> GetBossAccessibility();
        int GetFreeKeys();
        (AccessibilityLevel, int) GetItemAccessibility(int smallKeyValue, bool bigKeyValue);
        void SetBigKeyDoorState(bool unlocked);
        void SetSmallKeyDoorState(List<KeyDoorID> unlockedDoors);
        bool ValidateKeyLayout(int keysCollected, bool bigKeyCollected);
    }
}