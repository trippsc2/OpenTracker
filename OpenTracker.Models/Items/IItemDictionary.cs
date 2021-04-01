using System.Collections.Generic;
using OpenTracker.Models.SaveLoad;

namespace OpenTracker.Models.Items
{
    /// <summary>
    ///     This interface contains the dictionary container for item data.
    /// </summary>
    public interface IItemDictionary : IDictionary<ItemType, IItem>, ISaveable<IDictionary<ItemType, ItemSaveData>>
    {
        /// <summary>
        ///     A factory for creating the item dictionary.
        /// </summary>
        /// <returns>
        ///     The item dictionary.
        /// </returns>
        delegate IItemDictionary Factory();
        
        /// <summary>
        ///     Resets all items to their starting values.
        /// </summary>
        void Reset();
    }
}
