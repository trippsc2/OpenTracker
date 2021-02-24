using OpenTracker.Models.SaveLoad;
using System.Collections.Generic;

namespace OpenTracker.Models.Items
{
    public interface IItemDictionary : IDictionary<ItemType, IItem>,
        ICollection<KeyValuePair<ItemType, IItem>>,
        ISaveable<Dictionary<ItemType, ItemSaveData>>
    {
        void Reset();
    }
}
