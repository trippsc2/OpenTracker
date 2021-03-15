namespace OpenTracker.Models.Dropdowns
{
    /// <summary>
    /// This interface contains the creation logic for dropdowns.
    /// </summary>
    public interface IDropdownFactory
    {
        delegate IDropdownFactory Factory();

        IDropdown GetDropdown(DropdownID id);
    }
}