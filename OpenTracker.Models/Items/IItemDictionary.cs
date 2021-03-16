using OpenTracker.Models.SaveLoad;
using System.Collections.Generic;

namespace OpenTracker.Models.Items
{
    /// <summary>
    /// This interface contains the dictionary container for item data.
    /// </summary>
    public interface IItemDictionary : IDictionary<ItemType, IItem>,
        ISaveable<Dictionary<ItemType, ItemSaveData>>
    {
        void Reset();
    }
}
