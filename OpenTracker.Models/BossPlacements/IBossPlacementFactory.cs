namespace OpenTracker.Models.BossPlacements
{
    /// <summary>
    /// This is the interface for creation logic for boss placements.
    /// </summary>
    public interface IBossPlacementFactory
    {
        delegate IBossPlacementFactory Factory();

        IBossPlacement GetBossPlacement(BossPlacementID id);
    }
}