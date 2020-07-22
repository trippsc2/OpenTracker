using OpenTracker.Models.Locations;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace OpenTracker.Models
{
    /// <summary>
    /// This is the class containing the collection of connections between MapLocation classes.
    /// </summary>
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

        /// <summary>
        /// Constructor
        /// </summary>
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

        /// <summary>
        /// Returns a list of connection save data.
        /// </summary>
        /// <returns>
        /// A list of connection save data.
        /// </returns>
        public List<ConnectionSaveData> Save()
        {
            List<ConnectionSaveData> connections = new List<ConnectionSaveData>();

            foreach (var connection in this)
            {
                connections.Add(new ConnectionSaveData()
                {
                    Location1 = connection.Item1.Location.ID,
                    Location2 = connection.Item2.Location.ID,
                    Index1 = connection.Item1.Location.MapLocations.IndexOf(connection.Item1),
                    Index2 = connection.Item2.Location.MapLocations.IndexOf(connection.Item2)
                });
            }

            return connections;
        }

        /// <summary>
        /// Loads a list of connection save data.
        /// </summary>
        public void Load(List<ConnectionSaveData> saveData)
        {
            if (saveData == null)
            {
                throw new ArgumentNullException(nameof(saveData));
            }

            Clear();

            foreach (var connection in saveData)
            {
                Add((LocationDictionary.Instance[connection.Location1].MapLocations[connection.Index1],
                    LocationDictionary.Instance[connection.Location2].MapLocations[connection.Index2]));
            }
        }
    }
}
