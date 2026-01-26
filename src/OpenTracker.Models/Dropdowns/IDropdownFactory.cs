namespace OpenTracker.Models.Dropdowns;

/// <summary>
/// This interface contains the creation logic for <see cref="IDropdown"/> objects.
/// </summary>
public interface IDropdownFactory
{
    /// <summary>
    /// A factory for creating the <see cref="IDropdownFactory"/> object.
    /// </summary>
    /// <returns>
    ///     The <see cref="IDropdownFactory"/> object.
    /// </returns>
    delegate IDropdownFactory Factory();

    /// <summary>
    /// Returns a new <see cref="IDropdown"/> object for the specified <see cref="DropdownID"/>.
    /// </summary>
    /// <param name="id">
    ///     The <see cref="DropdownID"/>
    /// </param>
    /// <returns>
    ///     A new <see cref="IDropdown"/> object.
    /// </returns>
    IDropdown GetDropdown(DropdownID id);
}