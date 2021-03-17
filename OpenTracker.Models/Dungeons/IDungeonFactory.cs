using System.Collections.Generic;
using OpenTracker.Models.DungeonItems;
using OpenTracker.Models.DungeonNodes;
using OpenTracker.Models.Items;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.Locations;
using OpenTracker.Models.RequirementNodes;

namespace OpenTracker.Models.Dungeons
{
    /// <summary>
    /// This interface contains the creation logic for dungeons.
    /// </summary>
    public interface IDungeonFactory
    {
        delegate IDungeonFactory Factory();

        int GetDungeonBigKeyCount(LocationID id);
        List<KeyDoorID> GetDungeonBigKeyDoors(LocationID id);
        List<DungeonItemID> GetDungeonBigKeyDrops(LocationID id);
        IItem? GetDungeonBigKeyItem(LocationID id);
        List<DungeonItemID> GetDungeonBosses(LocationID id);
        int GetDungeonCompassCount(LocationID id);
        IItem? GetDungeonCompassItem(LocationID id);
        List<IRequirementNode> GetDungeonEntryNodes(LocationID id);
        List<DungeonItemID> GetDungeonItems(LocationID id);
        int GetDungeonMapCount(LocationID id);
        IItem? GetDungeonMapItem(LocationID id);
        List<DungeonNodeID> GetDungeonNodes(LocationID id);
        int GetDungeonSmallKeyCount(LocationID id);
        List<KeyDoorID> GetDungeonSmallKeyDoors(LocationID id);
        List<DungeonItemID> GetDungeonSmallKeyDrops(LocationID id);
        IItem GetDungeonSmallKeyItem(LocationID id);
    }
}