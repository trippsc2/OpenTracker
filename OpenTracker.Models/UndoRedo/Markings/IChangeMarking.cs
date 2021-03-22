using OpenTracker.Models.Markings;

namespace OpenTracker.Models.UndoRedo.Markings
{
    /// <summary>
    /// This interface contains undoable action data to set a marking.
    /// </summary>
    public interface IChangeMarking : IUndoable
    {
        delegate IChangeMarking Factory(IMarking marking, MarkType newMarking);
    }
}