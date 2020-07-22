using OpenTracker.Models.Locations;

namespace OpenTracker.Models.UndoRedo
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
        /// <param name="connection">
        /// The connection tuple to be removed.
        /// </param>
        public RemoveConnection((MapLocation, MapLocation) connection)
        {
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
