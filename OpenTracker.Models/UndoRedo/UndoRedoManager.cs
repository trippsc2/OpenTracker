using OpenTracker.Models.SaveLoad;
using OpenTracker.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using OpenTracker.Models.UndoRedo.Actions;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This class contains logic managing undo/redo actions.
    /// </summary>
    public class UndoRedoManager : IUndoRedoManager
    {
        private readonly ISaveLoadManager _saveLoadManager;

        private readonly IDoAction.Factory _doFactory;
        private readonly IUndoAction.Factory _undoFactory;
        private readonly IRedoAction.Factory _redoFactory;

        private readonly ObservableStack<IUndoable> _undoableActions =
            new ObservableStack<IUndoable>();
        private readonly ObservableStack<IUndoable> _redoableActions =
            new ObservableStack<IUndoable>();

        private CancellationTokenSource _cancellationTokenSource;
        private BlockingCollection<IUndoableAction> _taskQueue;
        private Task _queueTask;

        public event PropertyChangedEventHandler? PropertyChanged;

        public bool CanUndo =>
            _undoableActions.Count > 0;
        public bool CanRedo =>
            _redoableActions.Count > 0;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="saveLoadManager">
        /// The save/load manager.
        /// </param>
        public UndoRedoManager(
            ISaveLoadManager saveLoadManager, IDoAction.Factory doFactory, IUndoAction.Factory undoFactory,
            IRedoAction.Factory redoFactory)
        {
            _saveLoadManager = saveLoadManager;

            _doFactory = doFactory;
            _undoFactory = undoFactory;
            _redoFactory = redoFactory;

            _undoableActions.CollectionChanged += OnUndoChanged;
            _redoableActions.CollectionChanged += OnRedoChanged;
            
            StartProcessing();
        }

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The string of the property name of the changed property.
        /// </param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Subscribes to the CollectionChanged event on the _undoableActions observable stack.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the CollectionChanged event.
        /// </param>
        private void OnUndoChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(CanUndo));
        }

        /// <summary>
        /// Subscribes to the CollectionChanged event on the _redoableActions observable stack.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the CollectionChanged event.
        /// </param>
        private void OnRedoChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(CanRedo));
        }

        /// <summary>
        /// Initializes the queue and starts processing new actions.
        /// </summary>
        private void StartProcessing()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _taskQueue = new BlockingCollection<IUndoableAction>();
            _queueTask = Task.Factory.StartNew(() =>
            {
                ExecuteQueue();
            }, _cancellationTokenSource.Token);
        }

        /// <summary>
        /// Executes new actions.
        /// </summary>
        private void ExecuteQueue()
        {
            foreach (var action in _taskQueue.GetConsumingEnumerable())
            {
                action.Execute();
            }
        }

        /// <summary>
        /// Executes a specified action and adds it to the stack of undoable actions.
        /// </summary>
        /// <param name="action">
        /// The action to be executed.
        /// </param>
        public void NewAction(IUndoable action)
        {
            _taskQueue.Add(_doFactory(_undoableActions, _redoableActions, action));
            _saveLoadManager.Unsaved = true;
        }

        /// <summary>
        /// Undo the last action.
        /// </summary>
        public void Undo()
        {
            var action = _undoableActions.Pop();
            _taskQueue!.Add(_undoFactory(_redoableActions, action));
            _saveLoadManager.Unsaved = true;
        }

        /// <summary>
        /// Redo the last undone action.
        /// </summary>
        public void Redo()
        {
            var action = _redoableActions.Pop();
            _taskQueue!.Add(_redoFactory(_undoableActions, action));
            _saveLoadManager.Unsaved = true;
        }

        /// <summary>
        /// Resets the undo/redo stacks to their starting values.
        /// </summary>
        public void Reset()
        {
            _taskQueue?.CompleteAdding();

            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();

            _undoableActions.Clear();
            _redoableActions.Clear();

            _queueTask.Wait();
            _queueTask.Dispose();

            _taskQueue?.Dispose();

            StartProcessing();
        }
    }
}
