using OpenTracker.Models.Locations;

namespace OpenTracker.Models.UndoRedo.Locations
{
    /// <summary>
    /// This interface contains undoable action data to pin a location.
    /// </summary>
    public interface IPinLocation : IUndoable
    {
        delegate IPinLocation Factory(ILocation pinnedLocation);
    }
}