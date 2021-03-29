using System.Collections.Generic;
using OpenTracker.Models.KeyDoors;

namespace OpenTracker.Models.Dungeons.State
{
    /// <summary>
    /// This interface contains dungeon state data.
    /// </summary>
    public interface IDungeonState
    {
        delegate IDungeonState Factory(
            List<KeyDoorID> unlockedDoors, int keysCollected, bool bigKeyCollected, bool sequenceBreak);

        bool BigKeyCollected { get; }
        int KeysCollected { get; }
        bool SequenceBreak { get; }
        List<KeyDoorID> UnlockedDoors { get; }
    }
}