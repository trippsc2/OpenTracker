using System.Collections.Generic;
using OpenTracker.Models.SaveLoad;
using ReactiveUI;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This class contains logic managing <see cref="IUndoable"/> actions.
    /// </summary>
    public class UndoRedoManager : ReactiveObject, IUndoRedoManager
    {
        private readonly ISaveLoadManager _saveLoadManager;

        private readonly object _syncLock = new();
        private readonly Stack<IUndoable> _undoableActions = new();
        private readonly Stack<IUndoable> _redoableActions = new();

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
        ///     The <see cref="ISaveLoadManager"/>.
        /// </param>
        public UndoRedoManager(ISaveLoadManager saveLoadManager)
        {
            _saveLoadManager = saveLoadManager;
        }

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
        /// Updates the <see cref="CanUndo"/> property value.
        /// </summary>
        private void UpdateCanUndo()
        {
            CanUndo = _undoableActions.Count > 0;
        }

        /// <summary>
        /// Updates the <see cref="CanRedo"/> property value. 
        /// </summary>
        private void UpdateCanRedo()
        {
            CanRedo = _redoableActions.Count > 0;
        }
    }
}
