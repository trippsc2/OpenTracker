using OpenTracker.Models.AccessibilityLevels;
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

        List<(KeyDoorID, bool)> GetAccessibleKeyDoors();
        List<AccessibilityLevel> GetBossAccessibility();
        int GetFreeKeys();
        (AccessibilityLevel, int, bool) GetItemAccessibility(
            int smallKeyValue, bool bigKeyValue, bool sequenceBroken);
        void SetBigKeyDoorState(bool unlocked);
        void SetSmallKeyDoorState(List<KeyDoorID> unlockedDoors);
        bool ValidateKeyLayout(int keysCollected, bool bigKeyCollected);
    }
}