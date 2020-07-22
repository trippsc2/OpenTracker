using OpenTracker.Models.Modes;

namespace OpenTracker.Models.SaveLoad
{
    /// <summary>
    /// This is the class for containing save data for the Mode class.
    /// </summary>
    public class ModeSaveData
    {
        public ItemPlacement ItemPlacement { get; set; }
        public DungeonItemShuffle DungeonItemShuffle { get; set; }
        public WorldState WorldState { get; set; }
        public bool EntranceShuffle { get; set; }
        public bool BossShuffle { get; set; }
        public bool EnemyShuffle { get; set; }
    }
}
