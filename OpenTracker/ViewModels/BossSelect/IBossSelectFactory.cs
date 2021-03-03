using OpenTracker.Models.BossPlacements;
using System.Collections.Generic;

namespace OpenTracker.ViewModels.BossSelect
{
    /// <summary>
    /// This interface contains creation logic for the boss select controls.
    /// </summary>
    public interface IBossSelectFactory
    {
        List<IBossSelectButtonVM> GetBossSelectButtonVMs(IBossPlacement bossPlacement);
    }
}