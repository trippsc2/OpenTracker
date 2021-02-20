using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Utils;
using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This is the class for managing undo/redo actions.
    /// </summary>
    public class UndoRedoManager : Singleton<UndoRedoManager>, IUndoRedoManager
    {
        private readonly ISaveLoadManager _saveLoadManager;

        private readonly ObservableStack<IUndoable> _undoableActions =
            new ObservableStack<IUndoable>();
        private readonly ObservableStack<IUndoable> _redoableActions =
            new ObservableStack<IUndoable>();

        public event PropertyChangedEventHandler? PropertyChanged;

        public bool CanUndo =>
            _undoableActions.Count > 0;
        public bool CanRedo =>
            _redoableActions.Count > 0;

        public UndoRedoManager() : this(SaveLoadManager.Instance)
        {
        }

        private UndoRedoManager(ISaveLoadManager saveLoadManager)
        {
            _saveLoadManager = saveLoadManager;

            _undoableActions.CollectionChanged += OnUndoChanged;
            _redoableActions.CollectionChanged += OnRedoChanged;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnUndoChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(CanUndo));
        }

        private void OnRedoChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(CanRedo));
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
                _undoableActions.Push(action);
                _redoableActions.Clear();
                _saveLoadManager.Unsaved = true;
            }
        }

        /// <summary>
        /// Undo the last action.
        /// </summary>
        public void Undo()
        {
            if (_undoableActions.Count > 0)
            {
                IUndoable action = _undoableActions.Pop();
                action.Undo();
                _redoableActions.Push(action);
                _saveLoadManager.Unsaved = true;
            }
        }

        /// <summary>
        /// Redo the last undone action.
        /// </summary>
        public void Redo()
        {
            if (_redoableActions.Count > 0)
            {
                IUndoable action = _redoableActions.Pop();
                action.Execute();
                _undoableActions.Push(action);
                _saveLoadManager.Unsaved = true;
            }
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
