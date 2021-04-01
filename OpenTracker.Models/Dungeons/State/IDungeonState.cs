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
            IList<KeyDoorID> unlockedDoors, int keysCollected, bool bigKeyCollected, bool sequenceBreak);

        bool BigKeyCollected { get; }
        int KeysCollected { get; }
        bool SequenceBreak { get; }
        IList<KeyDoorID> UnlockedDoors { get; }
    }
}