namespace OpenTracker.Models.Items
{
    public interface IItemFactory
    {
        IItem GetItem(ItemType type);

        delegate IItemFactory Factory();
    }
}