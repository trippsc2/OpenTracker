using OpenTracker.Models.BossPlacements;

namespace OpenTracker.ViewModels.BossSelect
{
    public interface IBossSelectButtonVM
    {
        delegate IBossSelectButtonVM Factory(IBossPlacement bossPlacement, BossType? boss);
    }
}
