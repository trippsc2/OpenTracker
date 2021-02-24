namespace OpenTracker.Models.BossPlacements
{
    public interface IBossPlacementFactory
    {
        delegate IBossPlacementFactory Factory();

        IBossPlacement GetBossPlacement(BossPlacementID id);
    }
}