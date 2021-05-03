using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;

namespace OpenTracker.Models.UndoRedo.Notes
{
    /// <summary>
    /// This interface contains undoable action to remove a note from a location.
    /// </summary>
    public interface IRemoveNote : IUndoable
    {
        delegate IRemoveNote Factory(IMarking note, ILocation location);
    }
}