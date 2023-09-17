using System.Collections.Generic;
using System.Reactive;
using OpenTracker.Models.BossPlacements;
using ReactiveUI;

namespace OpenTracker.ViewModels.BossSelect;

/// <summary>
/// This interface contains creation logic for the boss select controls.
/// </summary>
public interface IBossSelectFactory
{
    List<IBossSelectButtonVM> GetBossSelectButtonVMs(
        IBossPlacement bossPlacement,
        ReactiveCommand<BossType?, Unit> changeBossCommand);
    
    delegate IBossSelectFactory Factory(IBossSelectPopupVM bossSelectPopup);
}