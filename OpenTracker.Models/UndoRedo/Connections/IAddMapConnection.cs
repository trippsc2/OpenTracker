using OpenTracker.Models.Locations.Map.Connections;

namespace OpenTracker.Models.UndoRedo.Connections
{
    /// <summary>
    /// This interface contains undoable action data to create a connection between two entrances.
    /// </summary>
    public interface IAddMapConnection : IUndoable
    {
        delegate IAddMapConnection Factory(IMapConnection connection);
    }
}