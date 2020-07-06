namespace OpenTracker.Models.Undoables
{
    /// <summary>
    /// This is the class for an undoable action to create a connection between
    /// two entrances.
    /// </summary>
    public class AddConnection : IUndoable
    {
        private readonly (MapLocation, MapLocation) _connection;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">
        /// The game data parent class.
        /// </param>
        /// <param name="connection">
        /// A tuple of the two map locations that are being collected.
        /// </param>
        public AddConnection((MapLocation, MapLocation) connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void Execute()
        {
            ConnectionCollection.Instance.Add(_connection);
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            ConnectionCollection.Instance.Remove(_connection);
        }
    }
}
