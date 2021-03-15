using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenTracker.Models.Locations;
using OpenTracker.Models.SaveLoad;

namespace OpenTracker.Models.Connections
{
    /// <summary>
    /// This class contains the collection container for map connections.
    /// </summary>
    public class ConnectionCollection : ObservableCollection<IConnection>, IConnectionCollection
    {
        private readonly ILocationDictionary _locations;
        private readonly IConnection.Factory _connectionFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="locations">
        /// The location dictionary.
        /// </param>
        /// <param name="connectionFactory">
        /// An Autofac factory for creating new connections.
        /// </param>
        public ConnectionCollection(
            ILocationDictionary locations, IConnection.Factory connectionFactory)
        {
            _locations = locations;
            _connectionFactory = connectionFactory;
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
        public void Load(List<ConnectionSaveData>? saveData)
        {
            if (saveData == null)
            {
                return;
            }

            Clear();

            foreach (var connection in saveData)
            {
                Add(_connectionFactory(
                    _locations[connection.Location1].MapLocations[connection.Index1],
                    _locations[connection.Location2].MapLocations[connection.Index2]));
            }
        }
    }
}
