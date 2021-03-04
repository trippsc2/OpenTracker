using OpenTracker.Utils;

namespace OpenTracker.Models.UndoRedo.Actions
{
    public class RedoAction : IRedoAction
    {
        private readonly ObservableStack<IUndoable> _undoableActions;
        private readonly IUndoable _action;

        public RedoAction(ObservableStack<IUndoable> undoableActions, IUndoable action)
        {
            _undoableActions = undoableActions;
            _action = action;
        }

        public void Execute()
        {
            _undoableActions.Push(_action);
            _action.ExecuteDo();
        }
    }
}