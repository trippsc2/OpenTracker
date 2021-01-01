using OpenTracker.Models.Locations;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace OpenTracker.Models.Connections
{
    /// <summary>
    /// This is the class containing the collection of connections between MapLocation classes.
    /// </summary>
    public class ConnectionCollection : Singleton<ConnectionCollection>,
        ICollection<Connection>, INotifyCollectionChanged
    {
        private static readonly ObservableCollection<Connection> _collection =
            new ObservableCollection<Connection>();

        public int Count =>
            _collection.Count;
        public bool IsReadOnly =>
            ((ICollection<Connection>)_collection).IsReadOnly;

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add => _collection.CollectionChanged += value;
            remove => _collection.CollectionChanged -= value;
        }

        public void Add(Connection item)
        {
            _collection.Add(item);
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public bool Contains(Connection item)
        {
            return _collection.Contains(item);
        }

        public void CopyTo(Connection[] array, int arrayIndex)
        {
            _collection.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Connection> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public bool Remove(Connection item)
        {
            return _collection.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collection.GetEnumerator();
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
                connections.Add(connection.Save());
            }

            return connections;
        }

        /// <summary>
        /// Loads a list of connection save data.
        /// </summary>
        /// <param name="saveData">
        /// A list of connection save data.
        /// </param>
        public void Load(List<ConnectionSaveData> saveData)
        {
            if (saveData == null)
            {
                throw new ArgumentNullException(nameof(saveData));
            }

            Clear();

            foreach (var connection in saveData)
            {
                Add(new Connection(
                    LocationDictionary.Instance[connection.Location1]
                        .MapLocations[connection.Index1],
                    LocationDictionary.Instance[connection.Location2]
                        .MapLocations[connection.Index2]));
            }
        }
    }
}
