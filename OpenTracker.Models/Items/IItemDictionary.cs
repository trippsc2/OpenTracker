using System.Collections.Generic;
using OpenTracker.Models.SaveLoad;

namespace OpenTracker.Models.Items
{
    /// <summary>
    /// This interface contains the dictionary container for item data.
    /// </summary>
    public interface IItemDictionary : IDictionary<ItemType, IItem>, ISaveable<Dictionary<ItemType, ItemSaveData>>
    {
        delegate IItemDictionary Factory();
        
        void Reset();
    }
}
