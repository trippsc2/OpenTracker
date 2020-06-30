namespace OpenTracker.Models.Undoables
{
    /// <summary>
    /// This is the class for an undoable action to create a connection between
    /// two entrances.
    /// </summary>
    public class AddConnection : IUndoable
    {
        private readonly Game _game;
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
        public AddConnection(Game game, (MapLocation, MapLocation) connection)
        {
            _game = game;
            _connection = connection;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void Execute()
        {
            _game.Connections.Add(_connection);
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            _game.Connections.Remove(_connection);
        }
    }
}
