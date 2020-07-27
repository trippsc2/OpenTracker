using OpenTracker.Models.Dungeons;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace OpenTracker.Models.KeyDoors
{
    /// <summary>
    /// This is the dictionary container for key doors.
    /// </summary>
    public class KeyDoorDictionary : IDictionary<KeyDoorID, IKeyDoor>
    {
        private readonly MutableDungeon _dungeonData;
        private readonly ConcurrentDictionary<KeyDoorID, IKeyDoor> _dictionary =
            new ConcurrentDictionary<KeyDoorID, IKeyDoor>();

        public ICollection<KeyDoorID> Keys =>
            ((IDictionary<KeyDoorID, IKeyDoor>)_dictionary).Keys;
        public ICollection<IKeyDoor> Values =>
            ((IDictionary<KeyDoorID, IKeyDoor>)_dictionary).Values;
        public int Count =>
            ((ICollection<KeyValuePair<KeyDoorID, IKeyDoor>>)_dictionary).Count;
        public bool IsReadOnly =>
            ((ICollection<KeyValuePair<KeyDoorID, IKeyDoor>>)_dictionary).IsReadOnly;

        public IKeyDoor this[KeyDoorID key]
        {
            get
            {
                if (!ContainsKey(key))
                {
                    Create(key);
                }

                return ((IDictionary<KeyDoorID, IKeyDoor>)_dictionary)[key];
            }

            set => ((IDictionary<KeyDoorID, IKeyDoor>)_dictionary)[key] = value;
        }

        public event EventHandler<KeyDoorID> DoorCreated;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dungeonData">
        /// The mutable dungeon data parent class.
        /// </param>
        public KeyDoorDictionary(MutableDungeon dungeonData)
        {
            _dungeonData = dungeonData ?? throw new ArgumentNullException(nameof(dungeonData));
        }

        /// <summary>
        /// Creates a new key door at the specified key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        private void Create(KeyDoorID key)
        {
            Add(key, KeyDoorFactory.GetKeyDoor(key, _dungeonData));
            DoorCreated?.Invoke(this, key);
        }

        public void Add(KeyDoorID key, IKeyDoor value)
        {
            ((IDictionary<KeyDoorID, IKeyDoor>)_dictionary).Add(key, value);
        }

        public void Add(KeyValuePair<KeyDoorID, IKeyDoor> item)
        {
            ((ICollection<KeyValuePair<KeyDoorID, IKeyDoor>>)_dictionary).Add(item);
        }

        public void Clear()
        {
            ((ICollection<KeyValuePair<KeyDoorID, IKeyDoor>>)_dictionary).Clear();
        }

        public bool Contains(KeyValuePair<KeyDoorID, IKeyDoor> item)
        {
            return ((ICollection<KeyValuePair<KeyDoorID, IKeyDoor>>)_dictionary).Contains(item);
        }

        public bool ContainsKey(KeyDoorID key)
        {
            return ((IDictionary<KeyDoorID, IKeyDoor>)_dictionary).ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<KeyDoorID, IKeyDoor>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<KeyDoorID, IKeyDoor>>)_dictionary).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<KeyDoorID, IKeyDoor>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<KeyDoorID, IKeyDoor>>)_dictionary).GetEnumerator();
        }

        public bool Remove(KeyDoorID key)
        {
            return ((IDictionary<KeyDoorID, IKeyDoor>)_dictionary).Remove(key);
        }

        public bool Remove(KeyValuePair<KeyDoorID, IKeyDoor> item)
        {
            return ((ICollection<KeyValuePair<KeyDoorID, IKeyDoor>>)_dictionary).Remove(item);
        }

        public bool TryGetValue(KeyDoorID key, out IKeyDoor value)
        {
            if (!ContainsKey(key))
            {
                Create(key);
            }

            return ((IDictionary<KeyDoorID, IKeyDoor>)_dictionary).TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_dictionary).GetEnumerator();
        }
    }
}
