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
            ((ICollection<Connection>)_collection).Count;
        public bool IsReadOnly =>
            ((ICollection<Connection>)_collection).IsReadOnly;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        /// Constructor
        /// </summary>
        public ConnectionCollection()
        {
            _collection.CollectionChanged += (sender, e) => CollectionChanged?.Invoke(sender, e);
        }

        public void Add(Connection item)
        {
            ((ICollection<Connection>)_collection).Add(item);
        }

        public void Clear()
        {
            ((ICollection<Connection>)_collection).Clear();
        }

        public bool Contains(Connection item)
        {
            return ((ICollection<Connection>)_collection).Contains(item);
        }

        public void CopyTo(Connection[] array, int arrayIndex)
        {
            ((ICollection<Connection>)_collection).CopyTo(array, arrayIndex);
        }

        public IEnumerator<Connection> GetEnumerator()
        {
            return ((IEnumerable<Connection>)_collection).GetEnumerator();
        }

        public bool Remove(Connection item)
        {
            return ((ICollection<Connection>)_collection).Remove(item);
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
                connections.Add(connection.Save());
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
                Add(new Connection(
                    LocationDictionary.Instance[connection.Location1]
                        .MapLocations[connection.Index1],
                    LocationDictionary.Instance[connection.Location2]
                        .MapLocations[connection.Index2]));
            }
        }
    }
}
