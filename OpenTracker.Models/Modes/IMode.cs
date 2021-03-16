using OpenTracker.Models.SaveLoad;
using ReactiveUI;

namespace OpenTracker.Models.Modes
{
    /// <summary>
    /// This interface contains game mode settings data.
    /// </summary>
    public interface IMode : IReactiveObject
    {
        bool BigKeyShuffle { get; set; }
        bool BossShuffle { get; set; }
        bool CompassShuffle { get; set; }
        bool EnemyShuffle { get; set; }
        EntranceShuffle EntranceShuffle { get; set; }
        bool GenericKeys { get; set; }
        bool GuaranteedBossItems { get; set; }
        ItemPlacement ItemPlacement { get; set; }
        bool KeyDropShuffle { get; set; }
        bool MapShuffle { get; set; }
        bool ShopShuffle { get; set; }
        bool SmallKeyShuffle { get; set; }
        bool TakeAnyLocations { get; set; }
        WorldState WorldState { get; set; }

        void Load(ModeSaveData? saveData);
        ModeSaveData Save();
    }
}