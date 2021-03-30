using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.Nodes;
using OpenTracker.Models.Items;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.KeyLayouts;
using OpenTracker.Models.RequirementNodes;
using ReactiveUI;

namespace OpenTracker.Models.Dungeons
{
    /// <summary>
    /// This interface contains the dungeon data.
    /// </summary>
    public interface IDungeon : IReactiveObject
    {
        ICappedItem? Map { get; }
        ICappedItem? Compass { get; }
        IKeyItem SmallKey { get; }
        ICappedItem? BigKey { get; }
        
        List<DungeonItemID> DungeonItems { get; }
        List<DungeonItemID> Bosses { get; }
        List<DungeonItemID> SmallKeyDrops { get; }
        List<DungeonItemID> BigKeyDrops { get; }
        List<KeyDoorID> SmallKeyDoors { get; }
        List<KeyDoorID> BigKeyDoors { get; }
        List<IKeyLayout> KeyLayouts { get; }
        List<DungeonNodeID> Nodes { get; }
        int Total { get; }
        DungeonID ID { get; }
        List<IRequirementNode> EntryNodes { get; }

        delegate IDungeon Factory(
            DungeonID id, ICappedItem? map, ICappedItem? compass, IKeyItem smallKey, ICappedItem? bigKey,
            List<DungeonItemID> dungeonItems, List<DungeonItemID> bosses, List<DungeonItemID> smallKeyDrops,
            List<DungeonItemID> bigKeyDrops, List<KeyDoorID> smallKeyDoors, List<KeyDoorID> bigKeyDoors,
            List<DungeonNodeID> nodes, List<IRequirementNode> entryNodes);
    }
}