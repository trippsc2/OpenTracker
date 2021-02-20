using Avalonia.Layout;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Reactive;

namespace OpenTracker.ViewModels.PinnedLocations
{
    /// <summary>
    /// This is the ViewModel for the pinned location note area control.
    /// </summary>
    public class PinnedLocationNoteAreaVM : ViewModelBase
    {
        private readonly ILocation _location;

        public ObservableCollection<PinnedLocationNoteVM> Notes { get; } =
            new ObservableCollection<PinnedLocationNoteVM>();
        public HorizontalAlignment Alignment =>
            Notes.Count == 0 ? HorizontalAlignment.Center : HorizontalAlignment.Left;

        public ReactiveCommand<Unit, Unit> AddCommand { get; }

        private bool _canAdd;
        public bool CanAdd
        {
            get => _canAdd;
            set => this.RaiseAndSetIfChanged(ref _canAdd, value);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="location">
        /// The location to be represented.
        /// </param>
        public PinnedLocationNoteAreaVM(ILocation location)
        {
            _location = location ?? throw new ArgumentNullException(nameof(location));
            AddCommand = ReactiveCommand.Create(Add, this.WhenAnyValue(x => x.CanAdd));
            UpdateCanAdd();
            RefreshNotes();
            _location.Notes.CollectionChanged += OnNotesChanged;
        }

        /// <summary>
        /// Subscribes to the CollectionChanged event on the ObservableCollection of notes.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnNotesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (object item in e.NewItems)
                {
                    Notes.Add(new PinnedLocationNoteVM((IMarking)item, _location));
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (object item in e.OldItems)
                {
                    IMarking marking = (IMarking)item;

                    foreach (var note in Notes)
                    {
                        if (note.Marking == marking)
                        {
                            Notes.Remove(note);
                            break;
                        }
                    }
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                RefreshNotes();
            }

            UpdateCanAdd();
            this.RaisePropertyChanged(nameof(Alignment));
        }

        /// <summary>
        /// Updates the CanAdd property for whether a note can be added.
        /// </summary>
        private void UpdateCanAdd()
        {
            CanAdd = Notes.Count < 4;
        }

        /// <summary>
        /// Refreshes the observable collection of pinned location note ViewModel instances to
        /// match the location.
        /// </summary>
        private void RefreshNotes()
        {
            Notes.Clear();

            foreach (var note in _location.Notes)
            {
                Notes.Add(new PinnedLocationNoteVM(note, _location));
            }
        }

        /// <summary>
        /// Adds a new note to the location.
        /// </summary>
        private void Add()
        {
            UndoRedoManager.Instance.Execute(new AddNote(_location));
        }
    }
}
