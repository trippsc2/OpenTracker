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
        public bool MapShuffle { get; set; }
        public bool CompassShuffle { get; set; }
        public bool SmallKeyShuffle { get; set; }
        public bool BigKeyShuffle { get; set; }
        public WorldState WorldState { get; set; } =
            WorldState.StandardOpen;
        public EntranceShuffle EntranceShuffle { get; set; }
        public bool BossShuffle { get; set; }
        public bool EnemyShuffle { get; set; }
        public bool GuaranteedBossItems { get; set; }
        public bool GenericKeys { get; set; }
        public bool TakeAnyLocations { get; set; }
    }
}
