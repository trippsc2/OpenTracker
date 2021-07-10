using System.Collections.Generic;
using OpenTracker.Models.Reset;
using OpenTracker.Models.SaveLoad;

namespace OpenTracker.Models.Items
{
    /// <summary>
    /// This interface contains the <see cref="IDictionary{TKey,TValue}"/> container for <see cref="IItem"/> objects
    /// indexed by <see cref="ItemType"/>.
    /// </summary>
    public interface IItemDictionary : IDictionary<ItemType, IItem>, IResettable,
        ISaveable<IDictionary<ItemType, ItemSaveData>>
    {
    }
}
