namespace OpenTracker.Models.BossPlacements
{
    /// <summary>
    ///     This interface contains the creation logic for boss placements.
    /// </summary>
    public interface IBossPlacementFactory
    {
        /// <summary>
        ///     A factory for creating a new boss placement factory.
        /// </summary>
        /// <returns>
        ///     A new boss placement factory.
        /// </returns>
        delegate IBossPlacementFactory Factory();
        
        /// <summary>
        ///     Returns a new boss placement.
        /// </summary>
        /// <param name="id">
        ///     The boss placement ID.
        /// </param>
        /// <returns>
        ///     A new boss placement.
        /// </returns>
        IBossPlacement GetBossPlacement(BossPlacementID id);
    }
}