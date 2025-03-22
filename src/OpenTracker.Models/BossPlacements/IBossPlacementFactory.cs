namespace OpenTracker.Models.BossPlacements
{
    /// <summary>
    /// This interface contains the creation logic for <see cref="IBossPlacement"/> objects.
    /// </summary>
    public interface IBossPlacementFactory
    {
        /// <summary>
        /// A factory for creating the <see cref="IBossPlacementFactory"/> object.
        /// </summary>
        /// <returns>
        ///     The <see cref="IBossPlacementFactory"/> object.
        /// </returns>
        delegate IBossPlacementFactory Factory();
        
        /// <summary>
        /// Returns a new <see cref="IBossPlacement"/> object for the specified <see cref="BossPlacementID"/>.
        /// </summary>
        /// <param name="id">
        ///     The <see cref="BossPlacementID"/>.
        /// </param>
        /// <returns>
        ///     A new <see cref="IBossPlacement"/> object.
        /// </returns>
        IBossPlacement GetBossPlacement(BossPlacementID id);
    }
}