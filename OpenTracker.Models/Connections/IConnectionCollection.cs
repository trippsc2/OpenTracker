﻿using System.Collections.Generic;
using OpenTracker.Models.Locations;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Utils;

namespace OpenTracker.Models.Connections
{
    /// <summary>
    /// This interface contains the collection container for map connections.
    /// </summary>
    public interface IConnectionCollection : IObservableCollection<IConnection>,
        ISaveable<List<ConnectionSaveData>>
    {
        delegate IConnectionCollection Factory();
        
        /// <summary>
        /// Creates an undoable action to add a connection and sends it to the undo/redo manager.
        /// </summary>
        /// <param name="location1">
        /// The first map location of the connection.
        /// </param>
        /// <param name="location2">
        /// The second map location of the connection.
        /// </param>
        void AddConnection(IMapLocation location1, IMapLocation location2);
    }
}
