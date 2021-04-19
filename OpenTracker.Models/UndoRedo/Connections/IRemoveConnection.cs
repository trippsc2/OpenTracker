using OpenTracker.Models.Locations.Map.Connections;

namespace OpenTracker.Models.UndoRedo.Connections
{
    /// <summary>
    /// This interface contains undoable action to remove a connection between two entrances.
    /// </summary>
    public interface IRemoveConnection : IUndoable
    {
        delegate IRemoveConnection Factory(IConnection connection);
    }
}