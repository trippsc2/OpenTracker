using OpenTracker.Models.BossPlacements;

namespace OpenTracker.ViewModels.BossSelect;

public interface IBossSelectPopupVM
{
    bool PopupOpen { get; set; }

    delegate IBossSelectPopupVM Factory(IBossPlacement bossPlacement);
}