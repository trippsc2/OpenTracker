using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace OpenTracker.Models.Locations
{
    public class PinnedLocationCollection : Singleton<PinnedLocationCollection>,
        IObservableCollection<ILocation>, ISaveable<List<LocationID>>
    {
        private static readonly ObservableCollection<ILocation> _collection =
            new ObservableCollection<ILocation>();

        public int Count =>
            _collection.Count;
        public bool IsReadOnly =>
            ((ICollection<ILocation>)_collection).IsReadOnly;

        public ILocation this[int index]
        {
            get => _collection[index];
            set => _collection[index] = value;
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add => _collection.CollectionChanged += value;
            remove => _collection.CollectionChanged -= value;
        }

        public void Add(ILocation item)
        {
            _collection.Add(item);
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public bool Contains(ILocation item)
        {
            return _collection.Contains(item);
        }

        public void CopyTo(ILocation[] array, int arrayIndex)
        {
            _collection.CopyTo(array, arrayIndex);
        }

        public int IndexOf(ILocation item)
        {
            return _collection.IndexOf(item);
        }

        public void Insert(int index, ILocation item)
        {
            _collection.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _collection.RemoveAt(index);
        }

        public IEnumerator<ILocation> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public bool Remove(ILocation item)
        {
            return _collection.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        /// <summary>
        /// Returns a list of location IDs to save.
        /// </summary>
        public List<LocationID> Save()
        {
            List<LocationID> pinnedLocations = new List<LocationID>();

            foreach (var pinnedLocation in this)
            {
                pinnedLocations.Add(pinnedLocation.ID);
            }

            return pinnedLocations;
        }

        /// <summary>
        /// Loads a list of location IDs.
        /// </summary>
        /// <param name="saveData">
        /// A list of location IDs to pin.
        /// </param>
        public void Load(List<LocationID> saveData)
        {
            if (saveData == null)
            {
                throw new ArgumentNullException(nameof(saveData));
            }

            Clear();

            foreach (var location in saveData)
            {
                Add(LocationDictionary.Instance[location]);
            }
        }
    }
}
