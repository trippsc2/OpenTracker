using OpenTracker.Models.Interfaces;

namespace OpenTracker.Models.Actions
{
    public class AddConnection : IUndoable
    {
        private readonly Game _game;
        private readonly (MapLocation, MapLocation) _connection;

        public AddConnection(Game game, (MapLocation, MapLocation) connection)
        {
            _game = game;
            _connection = connection;
        }

        public void Execute()
        {
            _game.Connections.Add(_connection);
        }

        public void Undo()
        {
            _game.Connections.Remove(_connection);
        }
    }
}
