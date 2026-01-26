using OpenTracker.Models.Items;

namespace OpenTracker.Models.UndoRedo.Items;

/// <summary>
/// This interface contains the <see cref="IUndoable"/> action to cycle a <see cref="ICappedItem"/>.
/// </summary>
public interface ICycleItem : IUndoable
{
    /// <summary>
    /// A factory for creating new <see cref="ICycleItem"/> objects.
    /// </summary>
    /// <param name="item">
    ///     The <see cref="ICappedItem"/>.
    /// </param>
    /// <returns>
    ///     A new <see cref="ICycleItem"/> object.
    /// </returns>
    delegate ICycleItem Factory(ICappedItem item);
}