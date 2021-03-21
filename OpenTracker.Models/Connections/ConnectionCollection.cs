﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenTracker.Models.Locations;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Connections;

namespace OpenTracker.Models.Connections
{
    /// <summary>
    /// This class contains the collection container for map connections.
    /// </summary>
    public class ConnectionCollection : ObservableCollection<IConnection>, IConnectionCollection
    {
        private readonly ILocationDictionary _locations;
        private readonly IUndoRedoManager _undoRedoManager;

        private readonly IConnection.Factory _connectionFactory;
        private readonly IAddConnection.Factory _addConnectionFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="locations">
        /// The location dictionary.
        /// </param>
        /// <param name="undoRedoManager">
        /// The undo/redo manager.
        /// </param>
        /// <param name="connectionFactory">
        /// An Autofac factory for creating new connections.
        /// </param>
        /// <param name="addConnectionFactory">
        /// An Autofac factory for creating new add connection undoable actions.
        /// </param>
        public ConnectionCollection(
            ILocationDictionary locations, IUndoRedoManager undoRedoManager, IConnection.Factory connectionFactory,
            IAddConnection.Factory addConnectionFactory)
        {
            _locations = locations;
            _connectionFactory = connectionFactory;
            _addConnectionFactory = addConnectionFactory;
            _undoRedoManager = undoRedoManager;
        }

        /// <summary>
        /// Creates an undoable action to add a connection and sends it to the undo/redo manager.
        /// </summary>
        /// <param name="location1">
        /// The first map location of the connection.
        /// </param>
        /// <param name="location2">
        /// The second map location of the connection.
        /// </param>
        public void AddConnection(IMapLocation location1, IMapLocation location2)
        {
            _undoRedoManager.NewAction(_addConnectionFactory(_connectionFactory(location1, location2)));
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
