namespace OpenTracker.Models.Undoables
{
    /// <summary>
    /// This is the class for an undoable action to remove a connection between
    /// two entrances.
    /// </summary>
    public class RemoveConnection : IUndoable
    {
        private readonly (MapLocation, MapLocation) _connection;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">
        /// The game data.
        /// </param>
        /// <param name="connection">
        /// The connection tuple to be removed.
        /// </param>
        public RemoveConnection((MapLocation, MapLocation) connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void Execute()
        {
            ConnectionCollection.Instance.Remove(_connection);
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            ConnectionCollection.Instance.Add(_connection);
        }
    }
}
