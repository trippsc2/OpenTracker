using OpenTracker.Utils;

namespace OpenTracker.Models.UndoRedo.Actions
{
    public interface IUndoAction : IUndoableAction
    {
        public delegate IUndoAction Factory(ObservableStack<IUndoable> redoableActions, IUndoable action);
    }
}