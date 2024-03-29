using OpenTracker.Models.Requirements;
using OpenTracker.Utils;

namespace OpenTracker.ViewModels.UIPanels;

public interface IUIPanelVM
{
    public delegate IUIPanelVM Factory(
        IRequirement? requirement,
        string title,
        IModeSettingsVM? modeSettings,
        bool alternateBodyColor,
        IViewModel body);
}