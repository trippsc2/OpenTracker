using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.KeyDoors;
using OpenTracker.Models.Dungeons.KeyLayouts;
using OpenTracker.Models.Dungeons.Nodes;
using OpenTracker.Models.Items;
using OpenTracker.Models.Items.Keys;
using OpenTracker.Models.Nodes;
using ReactiveUI;

namespace OpenTracker.Models.Dungeons
{
    /// <summary>
    ///     This interface contains the immutable dungeon data.
    /// </summary>
    public interface IDungeon : IReactiveObject
    {
        /// <summary>
        ///     The dungeon ID.
        /// </summary>
        DungeonID ID { get; }

        /// <summary>
        ///     The map item.
        /// </summary>
        ICappedItem? Map { get; }
        
        /// <summary>
        ///     The compass item.
        /// </summary>
        ICappedItem? Compass { get; }
        
        /// <summary>
        ///     The small key item.
        /// </summary>
        ISmallKeyItem SmallKey { get; }
        
        /// <summary>
        ///     The big key item.
        /// </summary>
        IBigKeyItem? BigKey { get; }
        
        /// <summary>
        ///     A list of dungeon item IDs for the dungeon.
        /// </summary>
        IList<DungeonItemID> DungeonItems { get; }
        
        /// <summary>
        ///     A list of boss dungeon item IDs for the dungeon.
        /// </summary>
        IList<DungeonItemID> Bosses { get; }
        
        /// <summary>
        ///     A list of small key drop dungeon item IDs for the dungeon.
        /// </summary>
        IList<DungeonItemID> SmallKeyDrops { get; }
        
        /// <summary>
        ///     A list of big key drop dungeon item IDs for the dungeon.
        /// </summary>
        IList<DungeonItemID> BigKeyDrops { get; }
        
        /// <summary>
        ///     A list of small key door IDs for the dungeon.
        /// </summary>
        IList<KeyDoorID> SmallKeyDoors { get; }
        
        /// <summary>
        ///     A list of big key door IDs for the dungeon.
        /// </summary>
        IList<KeyDoorID> BigKeyDoors { get; }
        
        /// <summary>
        ///     A list of possible key layouts for the dungeon.
        /// </summary>
        IList<IKeyLayout> KeyLayouts { get; }
        
        /// <summary>
        ///     A list of dungeon node IDs for the dungeon.
        /// </summary>
        IList<DungeonNodeID> Nodes { get; }
        
        /// <summary>
        ///     A list of overworld nodes that allow entry to the dungeon.
        /// </summary>
        IList<INode> EntryNodes { get; }

        /// <summary>
        ///     A 32-bit signed integer representing the total item checks in the dungeon.
        /// </summary>
        int Total { get; }
        
        /// <summary>
        ///     A 32-bit signed integer representing the total item checks in the dungeon including the map and compass,
        ///         if not shuffled.
        /// </summary>
        int TotalWithMapAndCompass { get; }

        /// <summary>
        ///     A factory for creating new dungeons.
        /// </summary>
        /// <param name="id">
        ///     The dungeon ID.
        /// </param>
        /// <param name="map">
        ///     The map item for the dungeon.
        /// </param>
        /// <param name="compass">
        ///     The compass item for the dungeon.
        /// </param>
        /// <param name="smallKey">
        ///     The small key item for the dungeon.
        /// </param>
        /// <param name="bigKey">
        ///     The big key item for the dungeon.
        /// </param>
        /// <param name="dungeonItems">
        ///     A list of dungeon item IDs in this dungeon.
        /// </param>
        /// <param name="bosses">
        ///     A list of dungeon item IDs for bosses in this dungeon.
        /// </param>
        /// <param name="smallKeyDrops">
        ///     A list of dungeon item IDs for small key drops in this dungeon.
        /// </param>
        /// <param name="bigKeyDrops">
        ///     A list of dungeon item IDs for big key drops in this dungeon.
        /// </param>
        /// <param name="smallKeyDoors">
        ///     A list of small key door IDs in this dungeon.
        /// </param>
        /// <param name="bigKeyDoors">
        ///     A list of big key door IDs in this dungeon.
        /// </param>
        /// <param name="nodes">
        ///     A list of dungeon nodes in this dungeon.
        /// </param>
        /// <param name="entryNodes">
        ///     A list of entry nodes for this dungeon.
        /// </param>
        /// <returns>
        ///     A new dungeon.
        /// </returns>
        delegate IDungeon Factory(
            DungeonID id, ICappedItem? map, ICappedItem? compass, ISmallKeyItem smallKey, IBigKeyItem? bigKey,
            IList<DungeonItemID> dungeonItems, IList<DungeonItemID> bosses, IList<DungeonItemID> smallKeyDrops,
            IList<DungeonItemID> bigKeyDrops, IList<KeyDoorID> smallKeyDoors, IList<KeyDoorID> bigKeyDoors,
            IList<DungeonNodeID> nodes, IList<INode> entryNodes);
    }
}