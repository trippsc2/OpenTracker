using OpenTracker.Models;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;
using OpenTracker.Models.UndoRedo;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive;

namespace OpenTracker.ViewModels.Markings
{
    /// <summary>
    /// This is the ViewModel class for the note marking select popup control.
    /// </summary>
    public class NoteMarkingSelectVM : ViewModelBase
    {
        private readonly IMarking _marking;
        private readonly ILocation _location;

        public double Scale =>
            AppSettings.Instance.UIScale;

        public ObservableCollection<MarkingSelectButtonVM> Buttons { get; }

        private bool _popupOpen;
        public bool PopupOpen
        {
            get => _popupOpen;
            set => this.RaiseAndSetIfChanged(ref _popupOpen, value);
        }

        public ReactiveCommand<MarkType?, Unit> ChangeMarkingCommand { get; }
        public ReactiveCommand<Unit, Unit> RemoveNoteCommand { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="marking">
        /// The marking to be represented.
        /// </param>
        /// <param name="buttons">
        /// The observable collection of marking select button ViewModel instances.
        /// </param>
        /// <param name="location">
        /// The location.
        /// </param>
        public NoteMarkingSelectVM(
            IMarking marking, ObservableCollection<MarkingSelectButtonVM> buttons,
            ILocation location)
        {
            _marking = marking ?? throw new ArgumentNullException(nameof(marking));
            Buttons = buttons ?? throw new ArgumentNullException(nameof(buttons));
            _location = location ?? throw new ArgumentNullException(nameof(location));
            ChangeMarkingCommand = ReactiveCommand.Create<MarkType?>(ChangeMarking);
            RemoveNoteCommand = ReactiveCommand.Create(RemoveNote);

            AppSettings.Instance.PropertyChanged += OnAppSettingsChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the AppSettings class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnAppSettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AppSettings.UIScale))
            {
                this.RaisePropertyChanged(nameof(Scale));
            }
        }

        /// <summary>
        /// Remove the note.
        /// </summary>
        private void RemoveNote()
        {
            UndoRedoManager.Instance.Execute(new RemoveNote(_marking, _location));
            PopupOpen = false;
        }

        /// <summary>
        /// Changes the marking of the section to the specified marking.
        /// </summary>
        /// <param name="marking">
        /// The marking to be set.
        /// </param>
        private void ChangeMarking(MarkType? marking)
        {
            if (marking == null)
            {
                return;
            }

            UndoRedoManager.Instance.Execute(new SetMarking(_marking, marking));
            PopupOpen = false;
        }
    }
}
