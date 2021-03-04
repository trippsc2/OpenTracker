using System.Collections.Specialized;
using Avalonia.Layout;
using OpenTracker.Models.Locations;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Threading;

namespace OpenTracker.ViewModels.PinnedLocations.Notes
{
    /// <summary>
    /// This is the ViewModel for the pinned location note area control.
    /// </summary>
    public class PinnedLocationNoteAreaVM : ViewModelBase, IPinnedLocationNoteAreaVM
    {
        private readonly IUndoRedoManager _undoRedoManager;
        private readonly IUndoableFactory _undoableFactory;

        private readonly ILocation _location;

        public IPinnedLocationNoteVMCollection Notes { get; }
        public HorizontalAlignment Alignment =>
            Notes.Count == 0 ? HorizontalAlignment.Center : HorizontalAlignment.Left;

        public ReactiveCommand<Unit, Unit> AddCommand { get; }

        private bool _canAdd;
        public bool CanAdd
        {
            get => _canAdd;
            set => this.RaiseAndSetIfChanged(ref _canAdd, value);
        }

        public delegate IPinnedLocationNoteAreaVM Factory(ILocation location);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="location">
        /// The location to be represented.
        /// </param>
        public PinnedLocationNoteAreaVM(
            IUndoRedoManager undoRedoManager, IUndoableFactory undoableFactory,
            IPinnedLocationNoteVMCollection.Factory notesFactory, ILocation location)
        {
            _undoRedoManager = undoRedoManager;
            _undoableFactory = undoableFactory;

            _location = location;

            Notes = notesFactory(location);

            AddCommand = ReactiveCommand.Create(Add, this.WhenAnyValue(x => x.CanAdd));

            Notes.CollectionChanged += OnNotesChanged;

            UpdateCanAdd();
        }

        /// <summary>
        /// Subscribes to the CollectionChanged event on the IPinnedLocationNoteVMCollection interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the CollectionChanged event.
        /// </param>
        private async void OnNotesChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            await UpdateCanAddAsync();
        }

        /// <summary>
        /// Updates the CanAdd property for whether a note can be added.
        /// </summary>
        private void UpdateCanAdd()
        {
            CanAdd = Notes.Count < 4;
        }

        /// <summary>
        /// Updates the CanAdd property for whether a note can be added asynchronously.
        /// </summary>
        private async Task UpdateCanAddAsync()
        {
            await Dispatcher.UIThread.InvokeAsync(UpdateCanAdd);
        }

        /// <summary>
        /// Adds a new note to the location.
        /// </summary>
        private void Add()
        {
            _undoRedoManager.NewAction(_undoableFactory.GetAddNote(_location));
        }
    }
}
