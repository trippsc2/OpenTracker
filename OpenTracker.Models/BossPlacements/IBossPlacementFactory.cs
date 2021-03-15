namespace OpenTracker.Models.BossPlacements
{
    /// <summary>
    /// This interface contains the creation logic for boss placements.
    /// </summary>
    public interface IBossPlacementFactory
    {
        delegate IBossPlacementFactory Factory();

        IBossPlacement GetBossPlacement(BossPlacementID id);
    }
}