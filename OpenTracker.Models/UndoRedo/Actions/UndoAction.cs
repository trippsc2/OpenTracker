using OpenTracker.Utils;

namespace OpenTracker.Models.UndoRedo.Actions
{
    public class UndoAction : IUndoAction
    {
        private readonly ObservableStack<IUndoable> _redoableActions;
        private readonly IUndoable _action;

        public UndoAction(ObservableStack<IUndoable> redoableActions, IUndoable action)
        {
            _redoableActions = redoableActions;
            _action = action;
        }
        
        public void Execute()
        {
            _redoableActions.Push(_action);
            _action.ExecuteUndo();
        }
    }
}