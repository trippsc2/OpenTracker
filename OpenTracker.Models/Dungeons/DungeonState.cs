using OpenTracker.Models.KeyDoors;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.Dungeons
{
    public class DungeonState
    {
        public List<KeyDoorID> UnlockedDoors { get; }
        public int KeysCollected { get; }
        public bool BigKeyCollected { get; }
        public bool SequenceBreak { get; }

        public DungeonState(
            List<KeyDoorID> unlockedDoors, int keysCollected, bool bigKeyCollected,
            bool sequenceBreak)
        {
            UnlockedDoors = unlockedDoors ?? throw new ArgumentNullException(nameof(unlockedDoors));
            KeysCollected = keysCollected;
            BigKeyCollected = bigKeyCollected;
            SequenceBreak = sequenceBreak;
        }
    }
}
