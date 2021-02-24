using OpenTracker.Models.BossPlacements;
using System.Collections.Generic;

namespace OpenTracker.ViewModels.BossSelect
{
    public interface IBossSelectFactory
    {
        List<IBossSelectButtonVM> GetBossSelectButtonVMs(IBossPlacement bossPlacement);
    }
}