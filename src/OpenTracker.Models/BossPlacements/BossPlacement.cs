using System.Reactive.Disposables;
using OpenTracker.Models.Modes;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Boss;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Models.BossPlacements;

/// <summary>
/// This class contains boss placement data.
/// </summary>
[DependencyInjection]
public sealed class BossPlacement : ReactiveObject, IBossPlacement
{
    private readonly CompositeDisposable _disposables = new();
    
    private readonly IChangeBoss.Factory _changeBossFactory;

    private IMode Mode { get; }
    public BossType DefaultBoss { get; }

    [Reactive]
    public BossType? Boss { get; set; }
    [ObservableAsProperty]
    public BossType? CurrentBoss { get; }

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
        _changeBossFactory = changeBossFactory;
        Mode = mode;
        DefaultBoss = defaultBoss;

        if (DefaultBoss == BossType.Aga)
        {
            Boss = BossType.Aga;
        }

        this.WhenAnyValue(
                x => x.Mode.BossShuffle,
                x => x.Boss,
                (bossShuffle, boss) => bossShuffle ? boss : DefaultBoss)
            .ToPropertyEx(this, x => x.CurrentBoss)
            .DisposeWith(_disposables);
    }

    public void Dispose()
    {
        _disposables.Dispose();
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