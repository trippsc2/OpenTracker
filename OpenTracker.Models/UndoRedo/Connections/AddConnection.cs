using OpenTracker.Models.Locations.Map.Connections;

namespace OpenTracker.Models.UndoRedo.Connections
{
    /// <summary>
    /// This class contains undoable action data to create a connection between two entrances.
    /// </summary>
    public class AddConnection : IAddConnection
    {
        private readonly IConnectionCollection _connections;
        private readonly IConnection _connection;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connections">
        /// The connection collection.
        /// </param>
        /// <param name="connection">
        /// A tuple of the two map locations that are being collected.
        /// </param>
        public AddConnection(IConnectionCollection connections, IConnection connection)
        {
            _connections = connections;
            _connection = connection;
        }

        /// <summary>
        /// Returns whether the action can be executed.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the action can be executed.
        /// </returns>
        public bool CanExecute()
        {
            return !_connections.Contains(_connection);
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void ExecuteDo()
        {
            _connections.Add(_connection);
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void ExecuteUndo()
        {
            _connections.Remove(_connection);
        }
    }
}
