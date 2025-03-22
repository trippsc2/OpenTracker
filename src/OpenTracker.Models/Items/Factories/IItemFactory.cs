namespace OpenTracker.Models.Items.Factories
{
    /// <summary>
    /// This interface contains creation logic for <see cref="IItem"/> objects.
    /// </summary>
    public interface IItemFactory
    {
        /// <summary>
        /// A factory for creating the <see cref="IItemFactory"/> object.
        /// </summary>
        /// <returns>
        ///     The <see cref="IItemFactory"/> object.
        /// </returns>
        delegate IItemFactory Factory();
        
        /// <summary>
        /// Returns a new <see cref="IItem"/> object for the specified <see cref="ItemType"/>.
        /// </summary>
        /// <param name="type">
        ///     The <see cref="ItemType"/>.
        /// </param>
        /// <returns>
        ///     A new <see cref="IItem"/> object.
        /// </returns>
        IItem GetItem(ItemType type);
    }
}