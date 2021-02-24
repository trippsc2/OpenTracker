using OpenTracker.Interfaces;
using OpenTracker.Models.Dropdowns;

namespace OpenTracker.ViewModels.Dropdowns
{
    public interface IDropdownVM : IClickHandler
    {
        delegate IDropdownVM Factory(IDropdown dropdown, string imageSourceBase);
    }
}
