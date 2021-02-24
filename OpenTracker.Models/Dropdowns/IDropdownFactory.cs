namespace OpenTracker.Models.Dropdowns
{
    /// <summary>
    /// This is the interface contianing the creation logic for dropdowns.
    /// </summary>
    public interface IDropdownFactory
    {
        delegate IDropdownFactory Factory();

        IDropdown GetDropdown(DropdownID id);
    }
}