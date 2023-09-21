using System;
using System.Collections.Generic;
using OpenTracker.Models.Dungeons.KeyDoors;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Dungeons.State;

/// <summary>
/// This class contains dungeon state data.
/// </summary>
[DependencyInjection]
public sealed class DungeonState : IDungeonState
{
    public IList<KeyDoorID> UnlockedDoors { get; }
    public int KeysCollected { get; }
    public bool BigKeyCollected { get; }
    public bool SequenceBreak { get; }

    /// <summary>
    /// Constructor
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
    public DungeonState(
        IList<KeyDoorID> unlockedDoors, int keysCollected, bool bigKeyCollected,
        bool sequenceBreak)
    {
        UnlockedDoors = unlockedDoors ?? throw new ArgumentNullException(nameof(unlockedDoors));
        KeysCollected = keysCollected;
        BigKeyCollected = bigKeyCollected;
        SequenceBreak = sequenceBreak;
    }
}