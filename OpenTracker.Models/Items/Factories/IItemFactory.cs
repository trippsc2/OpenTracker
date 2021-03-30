namespace OpenTracker.Models.Items.Factories
{
    /// <summary>
    /// This interface contains creation logic for item data.
    /// </summary>
    public interface IItemFactory
    {
        IItem GetItem(ItemType type);

        delegate IItemFactory Factory();
    }
}