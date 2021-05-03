using OpenTracker.Models.Locations;

namespace OpenTracker.Models.UndoRedo.Notes
{
    /// <summary>
    /// This interface contains undoable action data to add a note to a location.
    /// </summary>
    public interface IAddNote : IUndoable
    {
        delegate IAddNote Factory(ILocation location);
    }
}