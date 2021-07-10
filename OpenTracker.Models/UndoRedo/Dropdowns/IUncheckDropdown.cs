using OpenTracker.Models.Dropdowns;

namespace OpenTracker.Models.UndoRedo.Dropdowns
{
    /// <summary>
    /// This interface contains the <see cref="IUndoable"/> action to uncheck a <see cref="IDropdown"/>.
    /// </summary>
    public interface IUncheckDropdown : IUndoable
    {
        /// <summary>
        /// A factory for creating new <see cref="IUncheckDropdown"/> objects.
        /// </summary>
        /// <param name="dropdown">
        ///     The <see cref="IDropdown"/>.
        /// </param>
        /// <returns>
        ///     A new <see cref="IUncheckDropdown"/> object.
        /// </returns>
        delegate IUncheckDropdown Factory(IDropdown dropdown);
    }
}