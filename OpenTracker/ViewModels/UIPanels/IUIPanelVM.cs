using OpenTracker.Models.Requirements;

namespace OpenTracker.ViewModels.UIPanels
{
    public interface IUIPanelVM
    {
        public delegate IUIPanelVM Factory(
            IRequirement? requirement, string title, IModeSettingsVM? modeSettings, bool alternateBodyColor,
            IUIPanelBodyVMBase body);
    }
}