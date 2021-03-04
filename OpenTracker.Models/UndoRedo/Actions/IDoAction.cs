using OpenTracker.Utils;

namespace OpenTracker.Models.UndoRedo.Actions
{
    public interface IDoAction : IUndoableAction
    {
        public delegate IDoAction Factory(
            ObservableStack<IUndoable> undoableActions, ObservableStack<IUndoable> redoableActions, IUndoable action);
    }
}