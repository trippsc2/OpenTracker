using OpenTracker.Models.Modes;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Boss;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Models.BossPlacements;

/// <summary>
/// This class contains boss placement data.
/// </summary>
public sealed class BossPlacement : ReactiveObject, IBossPlacement
{
    private readonly IMode _mode;

    private readonly IChangeBoss.Factory _changeBossFactory;

    public BossType DefaultBoss { get; }

    [Reactive]
    public BossType? Boss { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mode">
    ///     The <see cref="IMode"/>.
    /// </param>
    /// <param name="changeBossFactory">
    ///     An Autofac factory for creating new <see cref="IChangeBoss"/> objects.
    /// </param>
    /// <param name="defaultBoss">
    ///     The default <see cref="BossType"/> for the boss placement.
    /// </param>
    public BossPlacement(IMode mode, IChangeBoss.Factory changeBossFactory, BossType defaultBoss)
    {
        _mode = mode;

        DefaultBoss = defaultBoss;
        _changeBossFactory = changeBossFactory;

        if (DefaultBoss == BossType.Aga)
        {
            Boss = BossType.Aga;
        }
    }

    public BossType? GetCurrentBoss()
    {
        return _mode.BossShuffle ? Boss : DefaultBoss;
    }

    public IUndoable CreateChangeBossAction(BossType? boss)
    {
        return _changeBossFactory(this, boss);
    }

    public void Reset()
    {
        if (DefaultBoss == BossType.Aga)
        {
            Boss = BossType.Aga;
            return;
        }

        Boss = null;
    }

    public BossPlacementSaveData Save()
    {
        return new BossPlacementSaveData
        {
            Boss = Boss
        };
    }

    public void Load(BossPlacementSaveData? saveData)
    {
        if (saveData == null)
        {
            return;
        }
            
        Boss = saveData.Boss;
    }
}