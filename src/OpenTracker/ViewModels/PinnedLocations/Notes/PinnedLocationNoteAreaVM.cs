using System.Collections.Specialized;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Layout;
using OpenTracker.Models.Locations;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.PinnedLocations.Notes;

/// <summary>
/// This class contains the pinned location note area control ViewModel data.
/// </summary>
[DependencyInjection]
public sealed class PinnedLocationNoteAreaVM : ViewModel, IPinnedLocationNoteAreaVM
{
    private readonly ILocation _location;
    private readonly IUndoRedoManager _undoRedoManager;

    public IPinnedLocationNoteVMCollection Notes { get; }
    
    [ObservableAsProperty]
    public HorizontalAlignment Alignment { get; }

    [Reactive]
    public ReactiveCommand<Unit, Unit> AddCommand { get; private set; } = default!;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="undoRedoManager">
    /// The undo/redo manager.
    /// </param>
    /// <param name="location">
    /// The location to be represented.
    /// </param>
    /// <param name="notesFactory">
    /// An Autofac factory for creating the notes collection.
    /// </param>
    public PinnedLocationNoteAreaVM(
        ILocation location,
        IUndoRedoManager undoRedoManager,
        IPinnedLocationNoteVMCollection.Factory notesFactory)
    {
        _location = location;
        _undoRedoManager = undoRedoManager;
        Notes = notesFactory(location);
        
        this.WhenActivated(disposables =>
        {
            var collectionChanged = Observable
                .FromEventPattern<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>(
                    handler => Notes.CollectionChanged += handler,
                    handler => Notes.CollectionChanged -= handler);

            collectionChanged
                .Select(_ => Notes.Count == 0 ? HorizontalAlignment.Center : HorizontalAlignment.Left)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Alignment)
                .DisposeWith(disposables);

            var canAdd = collectionChanged
                .Select(_ => Notes.Count < 4)
                .ObserveOn(RxApp.MainThreadScheduler);

            AddCommand = ReactiveCommand
                .Create(Add, canAdd)
                .DisposeWith(disposables);
        });
    }

    /// <summary>
    /// Adds a new note to the location.
    /// </summary>
    private void Add()
    {
        _undoRedoManager.NewAction(_location.CreateAddNoteAction());
    }
}