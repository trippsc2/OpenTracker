using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Utils;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace OpenTracker.Models.Items
{
    /// <summary>
    /// This is the dictionary container for items, both tracked and untracked
    /// </summary>
    public class ItemDictionary : Singleton<ItemDictionary>, IDictionary<ItemType, IItem>,
        ISaveable<Dictionary<ItemType, ItemSaveData>>
    {
        private static readonly ConcurrentDictionary<ItemType, IItem> _dictionary =
            new ConcurrentDictionary<ItemType, IItem>();

        public ICollection<ItemType> Keys =>
            ((IDictionary<ItemType, IItem>)_dictionary).Keys;
        public ICollection<IItem> Values =>
            ((IDictionary<ItemType, IItem>)_dictionary).Values;
        public int Count =>
            ((ICollection<KeyValuePair<ItemType, IItem>>)_dictionary).Count;
        public bool IsReadOnly =>
            ((ICollection<KeyValuePair<ItemType, IItem>>)_dictionary).IsReadOnly;

        public IItem this[ItemType key]
        {
            get
            {
                if (!ContainsKey(key))
                {
                    Create(key);
                }

                return ((IDictionary<ItemType, IItem>)_dictionary)[key];
            }
            set => ((IDictionary<ItemType, IItem>)_dictionary)[key] = value;
        }

        public event EventHandler<ItemType> ItemCreated;

        /// <summary>
        /// Creates a new item at the specified key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        private void Create(ItemType key)
        {
            Add(key, ItemFactory.GetItem(key));
            ItemCreated?.Invoke(this, key);
        }

        /// <summary>
        /// Resets all contained items to their starting values.
        /// </summary>
        public void Reset()
        {
            foreach (IItem item in Values)
            {
                item.Reset();
            }
        }

        public void Add(ItemType key, IItem value)
        {
            ((IDictionary<ItemType, IItem>)_dictionary).Add(key, value);
        }

        public bool ContainsKey(ItemType key)
        {
            return ((IDictionary<ItemType, IItem>)_dictionary).ContainsKey(key);
        }

        public bool Remove(ItemType key)
        {
            return ((IDictionary<ItemType, IItem>)_dictionary).Remove(key);
        }

        public bool TryGetValue(ItemType key, out IItem value)
        {
            if (!ContainsKey(key))
            {
                Create(key);
            }

            return ((IDictionary<ItemType, IItem>)_dictionary).TryGetValue(key, out value);
        }

        public void Add(KeyValuePair<ItemType, IItem> item)
        {
            ((ICollection<KeyValuePair<ItemType, IItem>>)_dictionary).Add(item);
        }

        public void Clear()
        {
            ((ICollection<KeyValuePair<ItemType, IItem>>)_dictionary).Clear();
        }

        public bool Contains(KeyValuePair<ItemType, IItem> item)
        {
            return ((ICollection<KeyValuePair<ItemType, IItem>>)_dictionary).Contains(item);
        }

        public void CopyTo(KeyValuePair<ItemType, IItem>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<ItemType, IItem>>)_dictionary).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<ItemType, IItem> item)
        {
            return ((ICollection<KeyValuePair<ItemType, IItem>>)_dictionary).Remove(item);
        }

        public IEnumerator<KeyValuePair<ItemType, IItem>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<ItemType, IItem>>)_dictionary).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_dictionary).GetEnumerator();
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
        public void Load(Dictionary<ItemType, ItemSaveData> saveData)
        {
            if (saveData == null)
            {
                throw new ArgumentNullException(nameof(saveData));
            }

            foreach (var item in saveData.Keys)
            {
                this[item].Load(saveData[item]);
            }
        }
    }
}
