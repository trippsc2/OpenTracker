using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo.Mode
{
    /// <summary>
    /// This interface contains undoable action data to change the world state setting.
    /// </summary>
    public interface IChangeWorldState : IUndoable
    {
        delegate IChangeWorldState Factory(WorldState newValue);
    }
}