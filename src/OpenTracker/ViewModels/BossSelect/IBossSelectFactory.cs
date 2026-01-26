using System.Collections.Generic;
using OpenTracker.Models.BossPlacements;

namespace OpenTracker.ViewModels.BossSelect;

/// <summary>
/// This interface contains creation logic for the boss select controls.
/// </summary>
public interface IBossSelectFactory
{
    List<IBossSelectButtonVM> GetBossSelectButtonVMs(IBossPlacement bossPlacement);
}