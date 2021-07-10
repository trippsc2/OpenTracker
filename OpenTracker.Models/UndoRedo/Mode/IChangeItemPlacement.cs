using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo.Mode
{
    /// <summary>
    /// This interface contains the <see cref="IUndoable"/> action to change the <see cref="IMode.ItemPlacement"/>
    /// property.
    /// </summary>
    public interface IChangeItemPlacement : IUndoable
    {
        /// <summary>
        /// A factory for creating new <see cref="IChangeItemPlacement"/> objects.
        /// </summary>
        /// <param name="newValue">
        ///     A <see cref="ItemPlacement"/> representing the new <see cref="IMode.ItemPlacement"/> value.
        /// </param>
        /// <returns>
        ///     A new <see cref="IChangeItemPlacement"/> object.
        /// </returns>
        delegate IChangeItemPlacement Factory(ItemPlacement newValue);
    }
}