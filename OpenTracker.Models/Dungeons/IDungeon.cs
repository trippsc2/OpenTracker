using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.Nodes;
using OpenTracker.Models.Items;
using OpenTracker.Models.Items.Keys;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.KeyLayouts;
using OpenTracker.Models.Nodes;
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
        ISmallKeyItem SmallKey { get; }
        IBigKeyItem? BigKey { get; }
        
        IList<DungeonItemID> DungeonItems { get; }
        IList<DungeonItemID> Bosses { get; }
        IList<DungeonItemID> SmallKeyDrops { get; }
        IList<DungeonItemID> BigKeyDrops { get; }
        IList<KeyDoorID> SmallKeyDoors { get; }
        IList<KeyDoorID> BigKeyDoors { get; }
        IList<IKeyLayout> KeyLayouts { get; }
        IList<DungeonNodeID> Nodes { get; }
        int Total { get; }
        DungeonID ID { get; }
        IList<INode> EntryNodes { get; }
        int TotalWithMapAndCompass { get; }

        delegate IDungeon Factory(
            DungeonID id, ICappedItem? map, ICappedItem? compass, ISmallKeyItem smallKey, IBigKeyItem? bigKey,
            IList<DungeonItemID> dungeonItems, IList<DungeonItemID> bosses, IList<DungeonItemID> smallKeyDrops,
            IList<DungeonItemID> bigKeyDrops, IList<KeyDoorID> smallKeyDoors, IList<KeyDoorID> bigKeyDoors,
            IList<DungeonNodeID> nodes, IList<INode> entryNodes);
    }
}