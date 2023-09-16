using OpenTracker.Models.BossPlacements;

namespace OpenTracker.ViewModels.BossSelect;

/// <summary>
/// This interface contains the boss select button control ViewModel data.
/// </summary>
public interface IBossSelectButtonVM
{
    delegate IBossSelectButtonVM Factory(IBossPlacement bossPlacement, BossType? boss);
}