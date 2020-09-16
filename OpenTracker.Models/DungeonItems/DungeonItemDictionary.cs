using OpenTracker.Models.Dungeons;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace OpenTracker.Models.DungeonItems
{
    /// <summary>
    /// This is the class containing the dictionary of dungeon items.
    /// </summary>
    public class DungeonItemDictionary : IDictionary<DungeonItemID, IDungeonItem>
    {
        private readonly MutableDungeon _dungeonData;
        private readonly ConcurrentDictionary<DungeonItemID, IDungeonItem> _dictionary =
            new ConcurrentDictionary<DungeonItemID, IDungeonItem>();

        public ICollection<DungeonItemID> Keys =>
            ((IDictionary<DungeonItemID, IDungeonItem>)_dictionary).Keys;
        public ICollection<IDungeonItem> Values =>
            ((IDictionary<DungeonItemID, IDungeonItem>)_dictionary).Values;
        public int Count =>
            ((ICollection<KeyValuePair<DungeonItemID, IDungeonItem>>)_dictionary).Count;
        public bool IsReadOnly =>
            ((ICollection<KeyValuePair<DungeonItemID, IDungeonItem>>)_dictionary).IsReadOnly;

        public IDungeonItem this[DungeonItemID key]
        {
            get
            {
                if (!ContainsKey(key))
                {
                    Create(key);
                }

                return ((IDictionary<DungeonItemID, IDungeonItem>)_dictionary)[key];
            }
            set => ((IDictionary<DungeonItemID, IDungeonItem>)_dictionary)[key] = value;
        }

        public event EventHandler<DungeonItemID> DungeonItemCreated;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dungeonData">
        /// The mutable dungeon data parent class.
        /// </param>
        public DungeonItemDictionary(MutableDungeon dungeonData)
        {
            _dungeonData = dungeonData ?? throw new ArgumentNullException(nameof(dungeonData));
        }

        /// <summary>
        /// Creates a new boss placement at the specified key.
        /// </summary>
        /// <param name="key">
        /// The dungeon item ID to be created.
        /// </param>
        private void Create(DungeonItemID key)
        {
            Add(key, DungeonItemFactory.GetDungeonItem(key, _dungeonData));
            DungeonItemCreated?.Invoke(this, key);
        }

        public void Add(DungeonItemID key, IDungeonItem value)
        {
            ((IDictionary<DungeonItemID, IDungeonItem>)_dictionary).Add(key, value);
        }

        public void Add(KeyValuePair<DungeonItemID, IDungeonItem> item)
        {
            ((ICollection<KeyValuePair<DungeonItemID, IDungeonItem>>)_dictionary).Add(item);
        }

        public void Clear()
        {
            _dictionary.Clear();
        }

        public bool Contains(KeyValuePair<DungeonItemID, IDungeonItem> item)
        {
            return ((ICollection<KeyValuePair<DungeonItemID, IDungeonItem>>)_dictionary).Contains(
                item);
        }

        public bool ContainsKey(DungeonItemID key)
        {
            return _dictionary.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<DungeonItemID, IDungeonItem>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<DungeonItemID, IDungeonItem>>)_dictionary).CopyTo(
                array, arrayIndex);
        }

        public bool Remove(DungeonItemID key)
        {
            return ((IDictionary<DungeonItemID, IDungeonItem>)_dictionary).Remove(key);
        }

        public bool Remove(KeyValuePair<DungeonItemID, IDungeonItem> item)
        {
            return ((ICollection<KeyValuePair<DungeonItemID, IDungeonItem>>)_dictionary).Remove(item);
        }

        public bool TryGetValue(DungeonItemID key, out IDungeonItem value)
        {
            if (!ContainsKey(key))
            {
                Create(key);
            }

            return _dictionary.TryGetValue(key, out value);
        }

        public IEnumerator<KeyValuePair<DungeonItemID, IDungeonItem>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }
    }
}
