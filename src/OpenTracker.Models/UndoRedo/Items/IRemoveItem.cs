using OpenTracker.Models.Items;

namespace OpenTracker.Models.UndoRedo.Items
{
    /// <summary>
    /// This interface contains the <see cref="IUndoable"/> action to remove an <see cref="IItem"/>.
    /// </summary>
    public interface IRemoveItem : IUndoable
    {
        /// <summary>
        /// A factory for creating new <see cref="IRemoveItem"/> objects.
        /// </summary>
        /// <param name="item">
        ///     The <see cref="IItem"/>.
        /// </param>
        /// <returns>
        ///     A new <see cref="IRemoveItem"/> object.
        /// </returns>
        delegate IRemoveItem Factory(IItem item);
    }
}