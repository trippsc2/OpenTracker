using OpenTracker.Models.Connections;
using System;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This is the class for an undoable action to create a connection between
    /// two entrances.
    /// </summary>
    public class AddConnection : IUndoable
    {
        private readonly Connection _connection;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connection">
        /// A tuple of the two map locations that are being collected.
        /// </param>
        public AddConnection(Connection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        /// <summary>
        /// Returns whether the action can be executed.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the action can be executed.
        /// </returns>
        public bool CanExecute()
        {
            return !ConnectionCollection.Instance.Contains(_connection);
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
