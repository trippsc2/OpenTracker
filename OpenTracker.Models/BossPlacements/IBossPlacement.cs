using OpenTracker.Models.SaveLoad;
using System.ComponentModel;

namespace OpenTracker.Models.BossPlacements
{
    /// <summary>
    /// This is the interface for a boss placement.
    /// </summary>
    public interface IBossPlacement : INotifyPropertyChanged
    {
        BossType? Boss { get; set; }
        BossType DefaultBoss { get; }

        BossType? GetCurrentBoss();
        void Load(BossPlacementSaveData saveData);
        void Reset();
        BossPlacementSaveData Save();
    }
}