using OpenTracker.Models.Connections;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This is the class for an undoable action to remove a connection between
    /// two entrances.
    /// </summary>
    public class RemoveConnection : IUndoable
    {
        private readonly IConnectionCollection _connections;
        private readonly IConnection _connection;

        public delegate RemoveConnection Factory(IConnection connection);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connection">
        /// The connection tuple to be removed.
        /// </param>
        public RemoveConnection(
            IConnectionCollection connections, IConnection connection)
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
            return true;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void Execute()
        {
            _connections.Remove(_connection);
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            _connections.Add(_connection);
        }
    }
}
