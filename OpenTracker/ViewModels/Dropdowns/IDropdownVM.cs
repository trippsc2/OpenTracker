using OpenTracker.Interfaces;
using OpenTracker.Models.Dropdowns;

namespace OpenTracker.ViewModels.Dropdowns
{
    public interface IDropdownVM
    {
        delegate IDropdownVM Factory(IDropdown dropdown, string imageSourceBase);
    }
}
