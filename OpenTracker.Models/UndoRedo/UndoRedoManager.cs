using System.Collections.Generic;
using OpenTracker.Models.SaveLoad;
using ReactiveUI;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This class contains logic managing undo/redo actions.
    /// </summary>
    public class UndoRedoManager : ReactiveObject, IUndoRedoManager
    {
        private readonly ISaveLoadManager _saveLoadManager;

        private readonly object _syncLock = new object();
        private readonly Stack<IUndoable> _undoableActions = new Stack<IUndoable>();
        private readonly Stack<IUndoable> _redoableActions = new Stack<IUndoable>();

        private bool _canUndo;
        public bool CanUndo
        {
            get => _canUndo;
            private set => this.RaiseAndSetIfChanged(ref _canUndo, value);
        }

        private bool _canRedo;
        public bool CanRedo
        {
            get => _canRedo;
            private set => this.RaiseAndSetIfChanged(ref _canRedo, value);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="saveLoadManager">
        /// The save/load manager.
        /// </param>
        public UndoRedoManager(ISaveLoadManager saveLoadManager)
        {
            _saveLoadManager = saveLoadManager;
        }

        /// <summary>
        /// Executes a specified action and adds it to the stack of undoable actions.
        /// </summary>
        /// <param name="action">
        /// The action to be executed.
        /// </param>
        public void NewAction(IUndoable action)
        {
            lock (_syncLock)
            {
                if (!action.CanExecute())
                {
                    return;
                }
                
                action.ExecuteDo();
                
                _undoableActions.Push(action);
                UpdateCanUndo();
                
                _redoableActions.Clear();
                UpdateCanRedo();
                
                _saveLoadManager.Unsaved = true;
            }
        }

        /// <summary>
        /// Undo the last action.
        /// </summary>
        public void Undo()
        {
            lock (_syncLock)
            {
                if (_undoableActions.Count <= 0)
                {
                    return;
                }

                IUndoable action = _undoableActions.Pop();
                UpdateCanUndo();
                
                action.ExecuteUndo();
                
                _redoableActions.Push(action);
                UpdateCanRedo();
                
                _saveLoadManager.Unsaved = true;
            }
        }

        /// <summary>
        /// Redo the last undone action.
        /// </summary>
        public void Redo()
        {
            lock (_syncLock)
            {
                if (_redoableActions.Count <= 0)
                {
                    return;
                }

                IUndoable action = _redoableActions.Pop();
                UpdateCanRedo();
                
                action.ExecuteDo();
                
                _undoableActions.Push(action);
                UpdateCanUndo();
                
                _saveLoadManager.Unsaved = true;
            }
        }

        /// <summary>
        /// Resets the undo/redo stacks to their starting values.
        /// </summary>
        public void Reset()
        {
            lock (_syncLock)
            {
                _undoableActions.Clear();
                UpdateCanUndo();
                
                _redoableActions.Clear();
                UpdateCanRedo();
            }
        }

        /// <summary>
        /// Updates the CanUndo property value.
        /// </summary>
        private void UpdateCanUndo()
        {
            CanUndo = _undoableActions.Count > 0;
        }

        /// <summary>
        /// Updates the CanRedo property value. 
        /// </summary>
        private void UpdateCanRedo()
        {
            CanRedo = _redoableActions.Count > 0;
        }
    }
}
