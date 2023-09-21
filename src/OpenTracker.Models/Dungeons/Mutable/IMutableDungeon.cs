using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.KeyDoors;
using OpenTracker.Models.Dungeons.Nodes;
using OpenTracker.Models.Dungeons.Result;
using OpenTracker.Models.Dungeons.State;

namespace OpenTracker.Models.Dungeons.Mutable;

/// <summary>
/// This interface contains the mutable dungeon data.
/// </summary>
public interface IMutableDungeon
{
    /// <summary>
    /// The <see cref="DungeonID"/> to which this data belongs.
    /// </summary>
    DungeonID ID { get; }

    /// <summary>
    /// The <see cref="IDungeonNodeDictionary"/>.
    /// </summary>
    IDungeonNodeDictionary Nodes { get; }
        
    /// <summary>
    /// The <see cref="IDungeonItemDictionary"/>.
    /// </summary>
    IDungeonItemDictionary DungeonItems { get; }
        
    /// <summary>
    /// The <see cref="IKeyDoorDictionary"/>.
    /// </summary>
    IKeyDoorDictionary KeyDoors { get; }
        
    /// <summary>
    /// A factory for creating new <see cref="IMutableDungeon"/> objects.
    /// </summary>
    /// <param name="dungeon">
    ///     The <see cref="IDungeon"/> to which the mutable data belongs.
    /// </param>
    /// <returns>
    ///     A new <see cref="IMutableDungeon"/> object.
    /// </returns>
    delegate IMutableDungeon Factory(IDungeon dungeon);

    /// <summary>
    /// Initializes the dictionary data classes for this dungeon.
    /// </summary>
    void InitializeData();

    /// <summary>
    /// Applies the <see cref="IDungeonState"/> conditions to this instance.
    /// </summary>
    /// <param name="state">
    ///     The <see cref="IDungeonState"/> to be applied.
    /// </param>
    void ApplyState(IDungeonState state);

    /// <summary>
    /// Returns the number of keys that are available to be collected in the dungeon.
    /// </summary>
    /// <param name="sequenceBreak">
    ///     A <see cref="bool"/> representing whether sequence breaking is allowed for this count.
    /// </param>
    /// <returns>
    ///     A <see cref="int"/> representing the number of keys that are available to be collected in the dungeon.
    /// </returns>
    int GetAvailableSmallKeys(bool sequenceBreak = false);
        
    /// <summary>
    /// Returns a <see cref="IList{T}"/> of accessible <see cref="KeyDoorID"/> in the dungeon.
    /// </summary>
    /// <param name="sequenceBreak">
    ///     A <see cref="bool"/> representing whether to return key doors only accessible by sequence break.
    /// </param>
    /// <returns>
    ///     A <see cref="IList{T}"/> of accessible <see cref="KeyDoorID"/>.
    /// </returns>
    IList<KeyDoorID> GetAccessibleKeyDoors(bool sequenceBreak = false);
        
    /// <summary>
    /// Returns whether the specified number of collected keys and big key can occur, based on key layout logic.
    /// </summary>
    /// <param name="state">
    ///     The <see cref="IDungeonState"/>.
    /// </param>
    /// <returns>
    ///     A <see cref="bool"/> representing whether the result can occur.
    /// </returns>
    bool ValidateKeyLayout(IDungeonState state);

    /// <summary>
    /// Returns the <see cref="IDungeonResult"/> based on the specified <see cref="IDungeonState"/>.
    /// </summary>
    /// <param name="state">
    ///     The <see cref="IDungeonState"/>.
    /// </param>
    /// <returns>
    ///     The <see cref="IDungeonResult"/>.
    /// </returns>
    IDungeonResult GetDungeonResult(IDungeonState state);
}