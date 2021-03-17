using System;
using System.Collections.Specialized;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Utils;
using ReactiveUI;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This class contains logic managing undo/redo actions.
    /// </summary>
    public class UndoRedoManager : ReactiveObject, IUndoRedoManager
    {
        private readonly ISaveLoadManager _saveLoadManager;

        private readonly ObservableStack<IUndoable> _undoableActions = new ObservableStack<IUndoable>();
        private readonly ObservableStack<IUndoable> _redoableActions = new ObservableStack<IUndoable>();
        
        public bool CanUndo => _undoableActions.Count > 0;
        public bool CanRedo => _redoableActions.Count > 0;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="saveLoadManager">
        /// The save/load manager.
        /// </param>
        public UndoRedoManager(ISaveLoadManager saveLoadManager)
        {
            _saveLoadManager = saveLoadManager;

            _undoableActions.CollectionChanged += OnUndoChanged;
            _redoableActions.CollectionChanged += OnRedoChanged;
        }

        /// <summary>
        /// Subscribes to the CollectionChanged event on the undoable actions observable stack.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the CollectionChanged event.
        /// </param>
        private void OnUndoChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            this.RaisePropertyChanged(nameof(CanUndo));
        }

        /// <summary>
        /// Subscribes to the CollectionChanged event on the redoable actions observable stack.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the CollectionChanged event.
        /// </param>
        private void OnRedoChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            this.RaisePropertyChanged(nameof(CanRedo));
        }

        /// <summary>
        /// Executes a specified action and adds it to the stack of undoable actions.
        /// </summary>
        /// <param name="action">
        /// The action to be executed.
        /// </param>
        public void NewAction(IUndoable action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (!action.CanExecute())
            {
                return;
            }
            
            action.ExecuteDo();
            _undoableActions.Push(action);
            _redoableActions.Clear();
            _saveLoadManager.Unsaved = true;
        }

        /// <summary>
        /// Undo the last action.
        /// </summary>
        public void Undo()
        {
            if (_undoableActions.Count <= 0)
            {
                return;
            }
            
            IUndoable action = _undoableActions.Pop();
            action.ExecuteUndo();
            _redoableActions.Push(action);
            _saveLoadManager.Unsaved = true;
        }

        /// <summary>
        /// Redo the last undone action.
        /// </summary>
        public void Redo()
        {
            if (_redoableActions.Count <= 0)
            {
                return;
            }
            
            IUndoable action = _redoableActions.Pop();
            action.ExecuteDo();
            _undoableActions.Push(action);
            _saveLoadManager.Unsaved = true;
        }

        /// <summary>
        /// Resets the undo/redo stacks to their starting values.
        /// </summary>
        public void Reset()
        {
            _undoableActions.Clear();
            _redoableActions.Clear();
        }
    }
}
