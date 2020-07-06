using OpenTracker.Models.Enums;
using OpenTracker.Models.Utils;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace OpenTracker.Models.Locations
{
    /// <summary>
    /// This is the dictionary of location data
    /// </summary>
    public class LocationDictionary : Singleton<LocationDictionary>,
        IDictionary<LocationID, ILocation>
    {
        private static readonly ConcurrentDictionary<LocationID, ILocation> _dictionary =
            new ConcurrentDictionary<LocationID, ILocation>();

        public ICollection<LocationID> Keys =>
            ((IDictionary<LocationID, ILocation>)_dictionary).Keys;

        public ICollection<ILocation> Values =>
            ((IDictionary<LocationID, ILocation>)_dictionary).Values;

        public int Count =>
            ((ICollection<KeyValuePair<LocationID, ILocation>>)_dictionary).Count;

        public bool IsReadOnly =>
            ((ICollection<KeyValuePair<LocationID, ILocation>>)_dictionary).IsReadOnly;

        public ILocation this[LocationID key]
        {
            get
            {
                if (!ContainsKey(key))
                {
                    Create(key);
                }

                return ((IDictionary<LocationID, ILocation>)_dictionary)[key];
            }
            set => ((IDictionary<LocationID, ILocation>)_dictionary)[key] = value;
        }

        public event EventHandler<LocationID> LocationCreated;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">
        /// The game data parent class.
        /// </param>
        public LocationDictionary()
        {
        }

        private void Create(LocationID key)
        {
            Add(key, LocationFactory.GetLocation(key));
        }

        /// <summary>
        /// Resets all locations to starting values.
        /// </summary>
        public void Reset()
        {
            foreach (Location location in Values)
            {
                location.Reset();
            }
        }

        public void Add(LocationID key, ILocation value)
        {
            ((IDictionary<LocationID, ILocation>)_dictionary).Add(key, value);
        }

        public bool ContainsKey(LocationID key)
        {
            return ((IDictionary<LocationID, ILocation>)_dictionary).ContainsKey(key);
        }

        public bool Remove(LocationID key)
        {
            return ((IDictionary<LocationID, ILocation>)_dictionary).Remove(key);
        }

        public bool TryGetValue(LocationID key, out ILocation value)
        {
            if (!ContainsKey(key))
            {
                Create(key);
            }

            return ((IDictionary<LocationID, ILocation>)_dictionary).TryGetValue(key, out value);
        }

        public void Add(KeyValuePair<LocationID, ILocation> item)
        {
            ((ICollection<KeyValuePair<LocationID, ILocation>>)_dictionary).Add(item);
        }

        public void Clear()
        {
            ((ICollection<KeyValuePair<LocationID, ILocation>>)_dictionary).Clear();
        }

        public bool Contains(KeyValuePair<LocationID, ILocation> item)
        {
            return ((ICollection<KeyValuePair<LocationID, ILocation>>)_dictionary).Contains(item);
        }

        public void CopyTo(KeyValuePair<LocationID, ILocation>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<LocationID, ILocation>>)_dictionary).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<LocationID, ILocation> item)
        {
            return ((ICollection<KeyValuePair<LocationID, ILocation>>)_dictionary).Remove(item);
        }

        public IEnumerator<KeyValuePair<LocationID, ILocation>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<LocationID, ILocation>>)_dictionary).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_dictionary).GetEnumerator();
        }
    }
}
