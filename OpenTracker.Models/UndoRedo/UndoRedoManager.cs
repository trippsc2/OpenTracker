using OpenTracker.Models.Utils;
using System;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This is the class for managing undo/redo actions.
    /// </summary>
    public class UndoRedoManager : Singleton<UndoRedoManager>
    {
        public ObservableStack<IUndoable> UndoableActions { get; } =
            new ObservableStack<IUndoable>();
        public ObservableStack<IUndoable> RedoableActions { get; } =
            new ObservableStack<IUndoable>();

        /// <summary>
        /// Returns whether any actions can be undone.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the actions can be undone.
        /// </returns>
        public bool CanUndo()
        {
            return UndoableActions.Count > 0;
        }

        /// <summary>
        /// Returns whether any actions can be redone.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the actions can be redone.
        /// </returns>
        public bool CanRedo()
        {
            return RedoableActions.Count > 0;
        }

        /// <summary>
        /// Executes a specified action and adds it to the stack of undoable actions.
        /// </summary>
        /// <param name="action">
        /// The action to be executed.
        /// </param>
        /// <param name="clearRedo">
        /// A boolean representing whether to clear the redo stack.
        /// </param>
        public void Execute(IUndoable action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (action.CanExecute())
            {
                action.Execute();
                UndoableActions.Push(action);
                RedoableActions.Clear();
            }
        }

        /// <summary>
        /// Undo the last action.
        /// </summary>
        public void Undo()
        {
            if (UndoableActions.Count > 0)
            {
                IUndoable action = UndoableActions.Pop();
                action.Undo();
                RedoableActions.Push(action);
            }
        }

        /// <summary>
        /// Redo the last undone action.
        /// </summary>
        public void Redo()
        {
            if (RedoableActions.Count > 0)
            {
                IUndoable action = RedoableActions.Pop();
                action.Execute();
                UndoableActions.Push(action);
            }
        }

        /// <summary>
        /// Resets the undo/redo stacks to their starting values.
        /// </summary>
        public void Reset()
        {
            UndoableActions.Clear();
            RedoableActions.Clear();
        }
    }
}
