using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo.Mode
{
    /// <summary>
    /// This interface contains undoable action data to change the entrance shuffle setting.
    /// </summary>
    public interface IChangeEntranceShuffle : IUndoable
    {
        delegate IChangeEntranceShuffle Factory(EntranceShuffle newValue);
    }
}