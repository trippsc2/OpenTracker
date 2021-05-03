using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using ReactiveUI;

namespace OpenTracker.Models.BossPlacements
{
    /// <summary>
    ///     This interface contains boss placement data.
    /// </summary>
    public interface IBossPlacement : IReactiveObject, ISaveable<BossPlacementSaveData>
    {
        /// <summary>
        ///     The default boss type, effective when boss shuffle is disabled.
        /// </summary>
        BossType DefaultBoss { get; }

        /// <summary>
        ///     The current boss type, effective when boss shuffle is enabled.
        /// </summary>
        BossType? Boss { get; set; }
        
        /// <summary>
        ///     A factory for creating boss placements.
        /// </summary>
        /// <param name="defaultBoss">
        ///     The default boss type for the boss placement.
        /// </param>
        /// <returns>
        ///     A new boss placement.
        /// </returns>
        delegate IBossPlacement Factory(BossType defaultBoss);

        /// <summary>
        ///     Returns the current effective boss.
        /// </summary>
        /// <returns>
        ///     The current effective boss.
        /// </returns>
        BossType? GetCurrentBoss();
        
        /// <summary>
        ///     Resets the boss placement to its starting values.
        /// </summary>
        void Reset();

        /// <summary>
        ///     Returns a new undoable action to change the current boss.
        /// </summary>
        /// <param name="boss">
        ///     The new nullable boss type.
        /// </param>
        /// <returns>
        ///     A new undoable action to change the current boss.
        /// </returns>
        IUndoable CreateChangeBossAction(BossType? boss);
    }
}