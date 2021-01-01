using OpenTracker.Models.Locations;
using OpenTracker.Models.Utils;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace OpenTracker.ViewModels.PinnedLocations
{
    public class PinnedLocationDictionary : Singleton<PinnedLocationDictionary>,
        IDictionary<LocationID, PinnedLocationVM>
    {
        private readonly ConcurrentDictionary<LocationID, PinnedLocationVM> _dictionary =
            new ConcurrentDictionary<LocationID, PinnedLocationVM>();

        public ICollection<LocationID> Keys =>
            _dictionary.Keys;
        public ICollection<PinnedLocationVM> Values =>
            _dictionary.Values;
        public int Count =>
            _dictionary.Count;
        public bool IsReadOnly =>
            ((ICollection<KeyValuePair<LocationID, PinnedLocationVM>>)_dictionary).IsReadOnly;

        public PinnedLocationVM this[LocationID key]
        {
            get
            {
                if (!ContainsKey(key))
                {
                    Create(key);
                }

                return _dictionary[key];
            }
            set => _dictionary[key] = value;
        }

        private void Create(LocationID key)
        {
            Add(key, PinnedLocationVMFactory.GetLocationControlVM(
                LocationDictionary.Instance[key]));
        }

        public void Add(LocationID key, PinnedLocationVM value)
        {
            ((IDictionary<LocationID, PinnedLocationVM>)_dictionary).Add(key, value);
        }

        public void Add(KeyValuePair<LocationID, PinnedLocationVM> item)
        {
            ((ICollection<KeyValuePair<LocationID, PinnedLocationVM>>)_dictionary).Add(item);
        }

        public void Clear()
        {
            ((ICollection<KeyValuePair<LocationID, PinnedLocationVM>>)_dictionary).Clear();
        }

        public bool Contains(KeyValuePair<LocationID, PinnedLocationVM> item)
        {
            return ((ICollection<KeyValuePair<LocationID, PinnedLocationVM>>)_dictionary).Contains(item);
        }

        public bool ContainsKey(LocationID key)
        {
            return ((IDictionary<LocationID, PinnedLocationVM>)_dictionary).ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<LocationID, PinnedLocationVM>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<LocationID, PinnedLocationVM>>)_dictionary).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<LocationID, PinnedLocationVM>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<LocationID, PinnedLocationVM>>)_dictionary).GetEnumerator();
        }

        public bool Remove(LocationID key)
        {
            return ((IDictionary<LocationID, PinnedLocationVM>)_dictionary).Remove(key);
        }

        public bool Remove(KeyValuePair<LocationID, PinnedLocationVM> item)
        {
            return ((ICollection<KeyValuePair<LocationID, PinnedLocationVM>>)_dictionary).Remove(item);
        }

        public bool TryGetValue(LocationID key, [MaybeNullWhen(false)] out PinnedLocationVM value)
        {
            if (!ContainsKey(key))
            {
                Create(key);
            }

            return ((IDictionary<LocationID, PinnedLocationVM>)_dictionary).TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_dictionary).GetEnumerator();
        }
    }
}
