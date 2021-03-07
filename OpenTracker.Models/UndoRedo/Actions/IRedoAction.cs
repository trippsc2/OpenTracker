using OpenTracker.Utils;

namespace OpenTracker.Models.UndoRedo.Actions
{
    public interface IRedoAction : IUndoableAction
    {
        public delegate IRedoAction Factory(ObservableStack<IUndoable> undoableActions, IUndoable action);
    }
}