using OpenTracker.Utils;

namespace OpenTracker.Models.UndoRedo.Actions
{
    /// <summary>
    /// This class contains the logic to do an undoable action.
    /// </summary>
    public class DoAction : IDoAction
    {
        private readonly ObservableStack<IUndoable> _undoableActions;
        private readonly ObservableStack<IUndoable> _redoableActions;
        private readonly IUndoable _action;

        public DoAction(
            ObservableStack<IUndoable> undoableActions, ObservableStack<IUndoable> redoableActions, IUndoable action)
        {
            _undoableActions = undoableActions;
            _redoableActions = redoableActions;
            _action = action;
        }
        
        public void Execute()
        {
            if (!_action.CanExecute())
            {
                return;
            }
            
            _action.ExecuteDo();
            _undoableActions.Push(_action);
            _redoableActions.Clear();
        }
    }
}