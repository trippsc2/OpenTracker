using OpenTracker.Models.Locations;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Connections;

namespace OpenTracker.Models.Connections
{
    /// <summary>
    /// This class contains the map location connection data.
    /// </summary>
    public class Connection : IConnection
    {
        private readonly IRemoveConnection.Factory _removeConnectionFactory;
        
        public IMapLocation Location1 { get; }
        public IMapLocation Location2 { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="removeConnectionFactory">
        /// An Autofac factory for creating remove connection undoable actions.
        /// </param>
        /// <param name="location1">
        /// The first location to connect.
        /// </param>
        /// <param name="location2">
        /// The second location to connect.
        /// </param>
        public Connection(
            IRemoveConnection.Factory removeConnectionFactory, IMapLocation location1, IMapLocation location2)
        {
            Location1 = location1;
            Location2 = location2;
            _removeConnectionFactory = removeConnectionFactory;
        }

        public override bool Equals(object? obj)
        {
            return obj switch
            {
                null => false,
                IConnection connection => (Location1 == connection.Location1 || Location1 == connection.Location2) &&
                    (Location2 == connection.Location2 || Location2 == connection.Location1),
                _ => false
            };
        }

        public override int GetHashCode()
        {
            return Location1.GetHashCode() ^ Location2.GetHashCode();
        }

        /// <summary>
        /// Creates an undoable action to remove the connection and sends it to the undo/redo manager.
        /// </summary>
        public IUndoable CreateRemoveConnectionAction()
        {
            return _removeConnectionFactory(this);
        }

        /// <summary>
        /// Returns a new connection save data instance for this connection.
        /// </summary>
        /// <returns>
        /// A new connection save data instance.
        /// </returns>
        public ConnectionSaveData Save()
        {
            return new()
            {
                Location1 = Location1.Location.ID,
                Location2 = Location2.Location.ID,
                Index1 = Location1.Location.MapLocations.IndexOf(Location1),
                Index2 = Location2.Location.MapLocations.IndexOf(Location2)
            };
        }
    }
}
