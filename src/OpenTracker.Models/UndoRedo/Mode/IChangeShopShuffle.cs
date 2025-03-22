using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo.Mode
{
    /// <summary>
    /// This interface contains the <see cref="IUndoable"/> action to change the <see cref="IMode.ShopShuffle"/>
    /// property.
    /// </summary>
    public interface IChangeShopShuffle : IUndoable
    {
        /// <summary>
        /// A factory for creating new <see cref="IChangeShopShuffle"/> objects.
        /// </summary>
        /// <param name="newValue">
        ///     A <see cref="bool"/> representing the new <see cref="IMode.ShopShuffle"/> value.
        /// </param>
        /// <returns>
        ///     A new <see cref="IChangeShopShuffle"/> object.
        /// </returns>
        delegate IChangeShopShuffle Factory(bool newValue);
    }
}