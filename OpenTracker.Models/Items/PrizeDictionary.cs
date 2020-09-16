using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Utils;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace OpenTracker.Models.Items
{
    /// <summary>
    /// This is the dictionary container for prizes.
    /// </summary>
    public class PrizeDictionary : Singleton<PrizeDictionary>, IDictionary<PrizeType, IItem>
    {
        private static readonly ConcurrentDictionary<PrizeType, IItem> _dictionary =
            new ConcurrentDictionary<PrizeType, IItem>();

        public ICollection<PrizeType> Keys =>
            ((IDictionary<PrizeType, IItem>)_dictionary).Keys;
        public ICollection<IItem> Values =>
            ((IDictionary<PrizeType, IItem>)_dictionary).Values;
        public int Count =>
            ((ICollection<KeyValuePair<PrizeType, IItem>>)_dictionary).Count;
        public bool IsReadOnly =>
            ((ICollection<KeyValuePair<PrizeType, IItem>>)_dictionary).IsReadOnly;

        public IItem this[PrizeType key]
        {
            get
            {
                if (!ContainsKey(key))
                {
                    Create(key);
                }

                return ((IDictionary<PrizeType, IItem>)_dictionary)[key];
            }
            set => ((IDictionary<PrizeType, IItem>)_dictionary)[key] = value;
        }

        public event EventHandler<PrizeType> ItemCreated;

        /// <summary>
        /// Creates a new item at the specified key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        private void Create(PrizeType key)
        {
            Add(key, new Item(0, null));
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

        public void Add(PrizeType key, IItem value)
        {
            ((IDictionary<PrizeType, IItem>)_dictionary).Add(key, value);
        }

        public bool ContainsKey(PrizeType key)
        {
            return ((IDictionary<PrizeType, IItem>)_dictionary).ContainsKey(key);
        }

        public bool Remove(PrizeType key)
        {
            return ((IDictionary<PrizeType, IItem>)_dictionary).Remove(key);
        }

        public bool TryGetValue(PrizeType key, out IItem value)
        {
            if (!ContainsKey(key))
            {
                Create(key);
            }

            return ((IDictionary<PrizeType, IItem>)_dictionary).TryGetValue(key, out value);
        }

        public void Add(KeyValuePair<PrizeType, IItem> item)
        {
            ((ICollection<KeyValuePair<PrizeType, IItem>>)_dictionary).Add(item);
        }

        public void Clear()
        {
            ((ICollection<KeyValuePair<PrizeType, IItem>>)_dictionary).Clear();
        }

        public bool Contains(KeyValuePair<PrizeType, IItem> item)
        {
            return ((ICollection<KeyValuePair<PrizeType, IItem>>)_dictionary).Contains(item);
        }

        public void CopyTo(KeyValuePair<PrizeType, IItem>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<PrizeType, IItem>>)_dictionary).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<PrizeType, IItem> item)
        {
            return ((ICollection<KeyValuePair<PrizeType, IItem>>)_dictionary).Remove(item);
        }

        public IEnumerator<KeyValuePair<PrizeType, IItem>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<PrizeType, IItem>>)_dictionary).GetEnumerator();
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
        public Dictionary<PrizeType, ItemSaveData> Save()
        {
            var items = new Dictionary<PrizeType, ItemSaveData>();

            foreach (var type in Keys)
            {
                items.Add(type, this[type].Save());
            }

            return items;
        }

        /// <summary>
        /// Loads a dictionary of item save data.
        /// </summary>
        public void Load(Dictionary<PrizeType, ItemSaveData> saveData)
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
