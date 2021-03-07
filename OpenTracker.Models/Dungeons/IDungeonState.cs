using OpenTracker.Models.KeyDoors;
using System.Collections.Generic;

namespace OpenTracker.Models.Dungeons
{
    /// <summary>
    /// This interface contains dungeon state data.
    /// </summary>
    public interface IDungeonState
    {
        delegate IDungeonState Factory(
            List<KeyDoorID> unlockedDoors, int keysCollected, bool bigKeyCollected,
            bool sequenceBreak);

        bool BigKeyCollected { get; }
        int KeysCollected { get; }
        bool SequenceBreak { get; }
        List<KeyDoorID> UnlockedDoors { get; }
    }
}