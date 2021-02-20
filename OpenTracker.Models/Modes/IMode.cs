using OpenTracker.Models.SaveLoad;
using System.ComponentModel;

namespace OpenTracker.Models.Modes
{
    public interface IMode : INotifyPropertyChanged
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

        void Load(ModeSaveData saveData);
        ModeSaveData Save();
    }
}