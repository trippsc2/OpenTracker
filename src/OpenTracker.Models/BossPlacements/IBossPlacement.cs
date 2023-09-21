using System;
using System.ComponentModel;
using OpenTracker.Models.Reset;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;

namespace OpenTracker.Models.BossPlacements;

/// <summary>
/// This interface contains boss placement data.
/// </summary>
public interface IBossPlacement : IDisposable, INotifyPropertyChanged, IResettable, ISaveable<BossPlacementSaveData>
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
    /// A <see cref="Nullable{T}"/> of <see cref="BossType"/> representing the current effective boss type.
    /// </summary>
    BossType? CurrentBoss { get; }
    
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