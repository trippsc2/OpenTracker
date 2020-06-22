using OpenTracker.Models.Interfaces;

namespace OpenTracker.Models.Actions
{
    /// <summary>
    /// This is the class for an undoable action to remove a connection between
    /// two entrances.
    /// </summary>
    public class RemoveConnection : IUndoable
    {
        private readonly Game _game;
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
        public RemoveConnection(Game game, (MapLocation, MapLocation) connection)
        {
            _game = game;
            _connection = connection;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void Execute()
        {
            _game.Connections.Remove(_connection);
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            _game.Connections.Add(_connection);
        }
    }
}
