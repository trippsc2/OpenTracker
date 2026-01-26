using OpenTracker.Models.Items;

namespace OpenTracker.Models.UndoRedo.Items;

/// <summary>
/// This interface contains the <see cref="IUndoable"/> action to add an <see cref="IItem"/>.
/// </summary>
public interface IAddItem : IUndoable
{
    /// <summary>
    /// A factory for creating new <see cref="IAddItem"/> objects.
    /// </summary>
    /// <param name="item">
    ///     The <see cref="IItem"/>.
    /// </param>
    /// <returns>
    ///     A new <see cref="IAddItem"/> object.
    /// </returns>
    delegate IAddItem Factory(IItem item);
}