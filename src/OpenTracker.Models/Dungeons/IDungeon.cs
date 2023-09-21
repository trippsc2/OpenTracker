using System.Collections.Generic;
using System.ComponentModel;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.KeyDoors;
using OpenTracker.Models.Dungeons.KeyLayouts;
using OpenTracker.Models.Dungeons.Nodes;
using OpenTracker.Models.Items;
using OpenTracker.Models.Items.Keys;
using OpenTracker.Models.Nodes;

namespace OpenTracker.Models.Dungeons;

/// <summary>
/// This interface contains the immutable dungeon data.
/// </summary>
public interface IDungeon : INotifyPropertyChanged
{
    /// <summary>
    /// The <see cref="DungeonID"/>.
    /// </summary>
    DungeonID ID { get; }

    /// <summary>
    /// The nullable <see cref="ICappedItem"/> representing the dungeon map.
    /// </summary>
    ICappedItem? Map { get; }
        
    /// <summary>
    /// The nullable <see cref="ICappedItem"/> representing the dungeon compass.
    /// </summary>
    ICappedItem? Compass { get; }
        
    /// <summary>
    /// The <see cref="ISmallKeyItem"/> representing the dungeon small key.
    /// </summary>
    ISmallKeyItem SmallKey { get; }
        
    /// <summary>
    /// The nullable <see cref="IBigKeyItem"/> representing the dungeon big key.
    /// </summary>
    IBigKeyItem? BigKey { get; }
        
    /// <summary>
    /// A <see cref="IList{T}"/> of <see cref="DungeonItemID"/> representing the dungeon items.
    /// </summary>
    IList<DungeonItemID> DungeonItems { get; }
        
    /// <summary>
    /// A <see cref="IList{T}"/> of <see cref="DungeonItemID"/> representing the dungeon bosses.
    /// </summary>
    IList<DungeonItemID> Bosses { get; }
        
    /// <summary>
    /// A <see cref="IList{T}"/> of <see cref="DungeonItemID"/> representing the dungeon small key drops.
    /// </summary>
    IList<DungeonItemID> SmallKeyDrops { get; }
        
    /// <summary>
    /// A <see cref="IList{T}"/> of <see cref="DungeonItemID"/> representing the dungeon big key drops.
    /// </summary>
    IList<DungeonItemID> BigKeyDrops { get; }
        
    /// <summary>
    /// A <see cref="IList{T}"/> of <see cref="KeyDoorID"/> representing the dungeon small key doors.
    /// </summary>
    IList<KeyDoorID> SmallKeyDoors { get; }
        
    /// <summary>
    /// A <see cref="IList{T}"/> of <see cref="KeyDoorID"/> representing the dungeon big key doors.
    /// </summary>
    IList<KeyDoorID> BigKeyDoors { get; }
        
    /// <summary>
    /// A <see cref="IList{T}"/> of <see cref="IKeyLayout"/> representing the possible dungeon key layouts.
    /// </summary>
    IList<IKeyLayout> KeyLayouts { get; }
        
    /// <summary>
    /// A <see cref="IList{T}"/> of <see cref="DungeonNodeID"/> representing the dungeon nodes.
    /// </summary>
    IList<DungeonNodeID> Nodes { get; }
        
    /// <summary>
    /// A <see cref="IList{T}"/> of <see cref="IOverworldNode"/> representing the dungeon entry nodes.
    /// </summary>
    IList<IOverworldNode> EntryNodes { get; }

    /// <summary>
    /// A <see cref="int"/> representing the total item checks in the dungeon.
    /// </summary>
    int Total { get; }
        
    /// <summary>
    /// A <see cref="int"/> representing the total item checks in the dungeon including the map and compass, if not
    /// shuffled.
    /// </summary>
    int TotalWithMapAndCompass { get; }

    /// <summary>
    /// A factory for creating new <see cref="IDungeon"/> objects.
    /// </summary>
    /// <param name="id">
    ///     The <see cref="DungeonID"/>.
    /// </param>
    /// <param name="map">
    ///     The nullable <see cref="ICappedItem"/> representing the dungeon map.
    /// </param>
    /// <param name="compass">
    ///     The nullable <see cref="ICappedItem"/> representing the dungeon compass.
    /// </param>
    /// <param name="smallKey">
    ///     The <see cref="ISmallKeyItem"/> representing the dungeon small key.
    /// </param>
    /// <param name="bigKey">
    ///     The nullable <see cref="IBigKeyItem"/> representing the dungeon big key.
    /// </param>
    /// <param name="dungeonItems">
    ///     A <see cref="IList{T}"/> of <see cref="DungeonItemID"/> representing the dungeon items.
    /// </param>
    /// <param name="bosses">
    ///     A <see cref="IList{T}"/> of <see cref="DungeonItemID"/> representing the dungeon bosses.
    /// </param>
    /// <param name="smallKeyDrops">
    ///     A <see cref="IList{T}"/> of <see cref="DungeonItemID"/> representing the dungeon small key drops.
    /// </param>
    /// <param name="bigKeyDrops">
    ///     A <see cref="IList{T}"/> of <see cref="DungeonItemID"/> representing the dungeon big key drops.
    /// </param>
    /// <param name="smallKeyDoors">
    ///     A <see cref="IList{T}"/> of <see cref="KeyDoorID"/> representing the dungeon small key doors.
    /// </param>
    /// <param name="bigKeyDoors">
    ///     A <see cref="IList{T}"/> of <see cref="KeyDoorID"/> representing the dungeon big key doors.
    /// </param>
    /// <param name="nodes">
    ///     A <see cref="IList{T}"/> of <see cref="DungeonNodeID"/> representing the dungeon nodes.
    /// </param>
    /// <param name="entryNodes">
    ///     A <see cref="IList{T}"/> of <see cref="IOverworldNode"/> representing the dungeon entry nodes.
    /// </param>
    /// <returns>
    ///     A new <see cref="IDungeon"/> object.
    /// </returns>
    delegate IDungeon Factory(
        DungeonID id, ICappedItem? map, ICappedItem? compass, ISmallKeyItem smallKey, IBigKeyItem? bigKey,
        IList<DungeonItemID> dungeonItems, IList<DungeonItemID> bosses, IList<DungeonItemID> smallKeyDrops,
        IList<DungeonItemID> bigKeyDrops, IList<KeyDoorID> smallKeyDoors, IList<KeyDoorID> bigKeyDoors,
        IList<DungeonNodeID> nodes, IList<IOverworldNode> entryNodes);
}