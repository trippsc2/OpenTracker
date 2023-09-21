using System.Collections.Generic;
using OpenTracker.Models.Dungeons.KeyDoors;

namespace OpenTracker.Models.Dungeons.State;

/// <summary>
/// This interface contains dungeon state data.
/// </summary>
public interface IDungeonState
{
    /// <summary>
    /// The <see cref="IList{T}"/> of unlocked <see cref="KeyDoorID"/>.
    /// </summary>
    IList<KeyDoorID> UnlockedDoors { get; }
        
    /// <summary>
    /// A <see cref="int"/> representing the number of small keys collected from dungeon item checks.
    /// </summary>
    int KeysCollected { get; }
        
    /// <summary>
    /// A <see cref="bool"/> representing whether the big key is collected.
    /// </summary>
    bool BigKeyCollected { get; }
        
    /// <summary>
    /// A <see cref="bool"/> representing whether sequence breaks are allowed.
    /// </summary>
    bool SequenceBreak { get; }

    /// <summary>
    /// A factory for creating new <see cref="IDungeonState"/> objects.
    /// </summary>
    /// <param name="unlockedDoors">
    ///     A <see cref="IList{T}"/> of <see cref="KeyDoorID"/> of doors that are unlocked.
    /// </param>
    /// <param name="keysCollected">
    ///     A <see cref="int"/> representing the number of small keys collected from dungeon item checks.
    /// </param>
    /// <param name="bigKeyCollected">
    ///     A <see cref="bool"/> representing whether the big key is collected.
    /// </param>
    /// <param name="sequenceBreak">
    ///     A <see cref="bool"/> representing whether sequence breaks are allowed.
    /// </param>
    /// <returns>
    ///     A new <see cref="IDungeonState"/> object.
    /// </returns>
    delegate IDungeonState Factory(
        IList<KeyDoorID> unlockedDoors, int keysCollected, bool bigKeyCollected, bool sequenceBreak);

}