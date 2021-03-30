namespace OpenTracker.Models.Dropdowns
{
    /// <summary>
    ///     This interface contains the creation logic for dropdowns.
    /// </summary>
    public interface IDropdownFactory
    {
        /// <summary>
        ///     A factory for creating the dropdown factory.
        /// </summary>
        /// <returns>
        ///     The dropdown factory.
        /// </returns>
        delegate IDropdownFactory Factory();

        /// <summary>
        ///     Returns a new dropdown for the given ID.
        /// </summary>
        /// <param name="id">
        ///     The dropdown ID
        /// </param>
        /// <returns>
        ///     A new dropdown.
        /// </returns>
        IDropdown GetDropdown(DropdownID id);
    }
}