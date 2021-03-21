using OpenTracker.Models.Locations;

namespace OpenTracker.Models.UndoRedo.Locations
{
    /// <summary>
    /// This interface contains undoable action data to unpin a location.
    /// </summary>
    public interface IUnpinLocation : IUndoable
    {
        delegate IUnpinLocation Factory(ILocation pinnedLocation);
    }
}