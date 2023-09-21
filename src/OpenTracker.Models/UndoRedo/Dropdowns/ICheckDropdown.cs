using OpenTracker.Models.Dropdowns;

namespace OpenTracker.Models.UndoRedo.Dropdowns;

/// <summary>
/// This interface contains the <see cref="IUndoable"/> action to check a <see cref="IDropdown"/>.
/// </summary>
public interface ICheckDropdown : IUndoable
{
    /// <summary>
    /// A factory for creating new <see cref="ICheckDropdown"/> objects.
    /// </summary>
    /// <param name="dropdown">
    ///     The <see cref="IDropdown"/>.
    /// </param>
    /// <returns>
    ///     A new <see cref="ICheckDropdown"/> object.
    /// </returns>
    delegate ICheckDropdown Factory(IDropdown dropdown);
}