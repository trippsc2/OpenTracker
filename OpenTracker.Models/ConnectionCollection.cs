using OpenTracker.Models.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace OpenTracker.Models
{
    public class ConnectionCollection : Singleton<ConnectionCollection>,
        ICollection<(MapLocation, MapLocation)>, INotifyCollectionChanged
    {
        private static readonly ObservableCollection<(MapLocation, MapLocation)> _collection =
            new ObservableCollection<(MapLocation, MapLocation)>();

        public int Count =>
            ((ICollection<(MapLocation, MapLocation)>)_collection).Count;

        public bool IsReadOnly =>
            ((ICollection<(MapLocation, MapLocation)>)_collection).IsReadOnly;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public ConnectionCollection()
        {
            _collection.CollectionChanged += (sender, e) => CollectionChanged?.Invoke(sender, e);
        }

        public void Add((MapLocation, MapLocation) item)
        {
            ((ICollection<(MapLocation, MapLocation)>)_collection).Add(item);
        }

        public void Clear()
        {
            ((ICollection<(MapLocation, MapLocation)>)_collection).Clear();
        }

        public bool Contains((MapLocation, MapLocation) item)
        {
            return ((ICollection<(MapLocation, MapLocation)>)_collection).Contains(item);
        }

        public void CopyTo((MapLocation, MapLocation)[] array, int arrayIndex)
        {
            ((ICollection<(MapLocation, MapLocation)>)_collection).CopyTo(array, arrayIndex);
        }

        public IEnumerator<(MapLocation, MapLocation)> GetEnumerator()
        {
            return ((IEnumerable<(MapLocation, MapLocation)>)_collection).GetEnumerator();
        }

        public bool Remove((MapLocation, MapLocation) item)
        {
            return ((ICollection<(MapLocation, MapLocation)>)_collection).Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_collection).GetEnumerator();
        }
    }
}
