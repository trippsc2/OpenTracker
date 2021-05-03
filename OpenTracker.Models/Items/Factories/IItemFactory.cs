namespace OpenTracker.Models.Items.Factories
{
    /// <summary>
    ///     This interface contains creation logic for item data.
    /// </summary>
    public interface IItemFactory
    {
        /// <summary>
        ///     A factory for creating the item factory.
        /// </summary>
        /// <returns>
        ///     The item factory.
        /// </returns>
        delegate IItemFactory Factory();
        
        /// <summary>
        ///     Returns a new item.
        /// </summary>
        /// <param name="type">
        ///     The item type.
        /// </param>
        /// <returns>
        ///     A new item.
        /// </returns>
        IItem GetItem(ItemType type);
    }
}