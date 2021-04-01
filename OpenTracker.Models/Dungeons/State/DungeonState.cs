using System;
using System.Collections.Generic;
using OpenTracker.Models.KeyDoors;

namespace OpenTracker.Models.Dungeons.State
{
    /// <summary>
    /// This class contains dungeon state data.
    /// </summary>
    public class DungeonState : IDungeonState
    {
        public IList<KeyDoorID> UnlockedDoors { get; }
        public int KeysCollected { get; }
        public bool BigKeyCollected { get; }
        public bool SequenceBreak { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unlockedDoors">
        /// A list of key door IDs of doors that are unlocked.
        /// </param>
        /// <param name="keysCollected">
        /// A 32-bit signed integer representing the number of small keys collected from dungeon
        /// item checks.
        /// </param>
        /// <param name="bigKeyCollected">
        /// A boolean representing whether the big key is collected.
        /// </param>
        /// <param name="sequenceBreak">
        /// A boolean representing whether sequence breaks are allowed.
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
}
