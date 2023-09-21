using System.Reactive;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using ReactiveUI;

namespace OpenTracker.ViewModels.BossSelect;

/// <summary>
/// This class contains the boss select button control ViewModel data.
/// </summary>
[DependencyInjection]
public sealed class BossSelectButtonVM : ViewModel
{
    private readonly IUndoRedoManager _undoRedoManager;
    private readonly IBossPlacement _bossPlacement;
    
    public BossType? Boss { get; }
    public string ImageSource { get; }

    public ReactiveCommand<Unit, Unit> ChangeBossCommand { get; }
    
    public delegate BossSelectButtonVM Factory(IBossPlacement bossPlacement, BossType? boss);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="undoRedoManager">
    ///     The <see cref="IUndoRedoManager"/>
    /// </param>
    /// <param name="bossPlacement">
    ///     The boss placement to be manipulated.
    /// </param>
    /// <param name="boss">
    ///     The boss to be represented by this button.
    /// </param>
    public BossSelectButtonVM(
        IUndoRedoManager undoRedoManager,
        IBossPlacement bossPlacement,
        BossType? boss)
    {
        _undoRedoManager = undoRedoManager;
        _bossPlacement = bossPlacement;
        
        Boss = boss;
        ImageSource = Boss is not null
            ? $"avares://OpenTracker/Assets/Images/Bosses/{Boss.ToString()!.ToLowerInvariant()}1.png"
            : $"avares://OpenTracker/Assets/Images/Bosses/{bossPlacement.DefaultBoss.ToString().ToLowerInvariant()}0.png";

        ChangeBossCommand = ReactiveCommand.Create(ChangeBoss);
    }
    
    private void ChangeBoss()
    {
        if (_bossPlacement.Boss == Boss)
        {
            return;
        }
        
        _undoRedoManager.NewAction(_bossPlacement.CreateChangeBossAction(Boss));
    }
}