using OpenTracker.Models.SaveLoad;
using OpenTracker.Utils;
using System.Collections.Generic;

namespace OpenTracker.Models.Items
{
    /// <summary>
    /// This is the dictionary container for items, both tracked and untracked
    /// </summary>
    public class ItemDictionary : LazyDictionary<ItemType, IItem>, IItemDictionary
    {
        private readonly IItemFactory _factory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        /// Factory for creating items.
        /// </param>
        public ItemDictionary(IItemFactory factory) : base(new Dictionary<ItemType, IItem>())
        {
            _factory = factory;
        }

        protected override IItem Create(ItemType key)
        {
            return _factory.GetItem(key);
        }

        /// <summary>
        /// Resets all contained items to their starting values.
        /// </summary>
        public void Reset()
        {
            foreach (var item in Values)
            {
                item.Reset();
            }
        }

        /// <summary>
        /// Returns a dictionary of item save data.
        /// </summary>
        /// <returns>
        /// A dictionary of item save data.
        /// </returns>
        public Dictionary<ItemType, ItemSaveData> Save()
        {
            Dictionary<ItemType, ItemSaveData> items = new Dictionary<ItemType, ItemSaveData>();

            foreach (var type in Keys)
            {
                items.Add(type, this[type].Save());
            }

            return items;
        }

        /// <summary>
        /// Loads a dictionary of item save data.
        /// </summary>
        public void Load(Dictionary<ItemType, ItemSaveData>? saveData)
        {
            if (saveData == null)
            {
                return;
            }

            foreach (var item in saveData.Keys)
            {
                this[item].Load(saveData[item]);
            }
        }
    }
}
