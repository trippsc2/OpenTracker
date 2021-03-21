using System;
using OpenTracker.Models.Connections;

namespace OpenTracker.Models.UndoRedo.Connections
{
    /// <summary>
    /// This class contains undoable action data to create a connection between two entrances.
    /// </summary>
    public class AddConnection : IAddConnection
    {
        private readonly Lazy<IConnectionCollection> _connections;
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
        public AddConnection(IConnectionCollection.Factory connections, IConnection connection)
        {
            _connections = new Lazy<IConnectionCollection>(() => connections());
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
            return !_connections.Value.Contains(_connection);
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void ExecuteDo()
        {
            _connections.Value.Add(_connection);
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void ExecuteUndo()
        {
            _connections.Value.Remove(_connection);
        }
    }
}
