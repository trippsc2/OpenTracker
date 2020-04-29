using OpenTracker.Models.Interfaces;
using OpenTracker.Models.Utils;

namespace OpenTracker.ViewModels
{
    public class UndoRedoManager
    {
        public ObservableStack<IUndoable> UndoableActions { get; }
        public ObservableStack<IUndoable> RedoableActions { get; }

        public UndoRedoManager()
        {
            UndoableActions = new ObservableStack<IUndoable>();
            RedoableActions = new ObservableStack<IUndoable>();
        }

        public bool CanUndo()
        {
            return UndoableActions.Count > 0;
        }

        public bool CanRedo()
        {
            return RedoableActions.Count > 0;
        }

        public void Execute(IUndoable action, bool clearRedo = true)
        {
            UndoableActions.Push(action);
            action.Execute();

            if (clearRedo)
                RedoableActions.Clear();
        }

        public void Undo()
        {
            if (UndoableActions.Count > 0)
            {
                IUndoable action = UndoableActions.Pop();
                RedoableActions.Push(action);
                action.Undo();
            }
        }

        public void Redo()
        {
            if (RedoableActions.Count > 0)
            {
                IUndoable action = RedoableActions.Pop();
                Execute(action, false);
            }
        }

        public void Reset()
        {
            UndoableActions.Clear();
            RedoableActions.Clear();
        }
    }
}
