using OpenTracker.Models.Reset;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using ReactiveUI;

namespace OpenTracker.Models.BossPlacements
{
    /// <summary>
    /// This interface contains boss placement data.
    /// </summary>
    public interface IBossPlacement : IReactiveObject, IResettable, ISaveable<BossPlacementSaveData>
    {
        /// <summary>
        /// The default <see cref="BossType"/>, effective when boss shuffle is disabled.
        /// </summary>
        BossType DefaultBoss { get; }

        /// <summary>
        /// The current <see cref="BossType"/>, effective when boss shuffle is enabled.
        /// </summary>
        BossType? Boss { get; set; }
        
        /// <summary>
        /// A factory for creating new <see cref="IBossPlacement"/> objects.
        /// </summary>
        /// <param name="defaultBoss">
        ///     The default <see cref="BossType"/> for the boss placement.
        /// </param>
        /// <returns>
        ///     A new <see cref="BossPlacement"/> object.
        /// </returns>
        delegate IBossPlacement Factory(BossType defaultBoss);

        /// <summary>
        /// Returns the current effective <see cref="BossType"/>.
        /// </summary>
        /// <returns>
        ///     The current effective <see cref="BossType"/>.
        /// </returns>
        BossType? GetCurrentBoss();
        
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