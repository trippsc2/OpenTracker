using System.Reactive;
using OpenTracker.Models.BossPlacements;
using ReactiveUI;

namespace OpenTracker.ViewModels.BossSelect;

/// <summary>
/// This interface contains the boss select button control ViewModel data.
/// </summary>
public interface IBossSelectButtonVM
{
    ReactiveCommand<BossType?, Unit> ChangeBossCommand { get; }
    delegate IBossSelectButtonVM Factory(
        IBossPlacement bossPlacement,
        BossType? boss,
        ReactiveCommand<BossType?, Unit> changeBossCommand);
}