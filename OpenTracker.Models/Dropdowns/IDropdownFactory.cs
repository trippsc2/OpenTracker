using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.Dropdowns
{
    public interface IDropdownFactory
    {
        delegate IDropdownFactory Factory();

        IDropdown GetDropdown(DropdownID id);
    }
}