using OpenTracker.Models.SaveLoad;
using ReactiveUI;

namespace OpenTracker.Models.BossPlacements
{
    /// <summary>
    /// This is the interface for a boss placement.
    /// </summary>
    public interface IBossPlacement : IReactiveObject, ISaveable<BossPlacementSaveData>
    {
        BossType? Boss { get; set; }
        BossType DefaultBoss { get; }

        delegate IBossPlacement Factory(BossType defaultBoss);

        BossType? GetCurrentBoss();
        void Reset();
    }
}