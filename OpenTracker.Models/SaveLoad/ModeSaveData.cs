using OpenTracker.Models.Modes;

namespace OpenTracker.Models.SaveLoad
{
    /// <summary>
    /// This is the class for containing save data for the Mode class.
    /// </summary>
    public class ModeSaveData
    {
        public ItemPlacement ItemPlacement { get; set; } =
            ItemPlacement.Advanced;
        public DungeonItemShuffle DungeonItemShuffle { get; set; } =
            DungeonItemShuffle.Standard;
        public WorldState WorldState { get; set; } =
            WorldState.StandardOpen;
        public bool EntranceShuffle { get; set; }
        public bool BossShuffle { get; set; }
        public bool EnemyShuffle { get; set; }
        public bool GuaranteedBossItems { get; set; }
    }
}
