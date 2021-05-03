using OpenTracker.Models.Locations;

namespace OpenTracker.Models.UndoRedo.Locations
{
    /// <summary>
    /// This interface contains undoable action data to clear a location.
    /// </summary>
    public interface IClearLocation : IUndoable
    {
        delegate IClearLocation Factory(ILocation location, bool force = false);
    }
}