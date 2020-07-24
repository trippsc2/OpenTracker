using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;
using OpenTracker.Models.UndoRedo;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
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

        public ObservableCollection<MarkingSelectButtonVM> Buttons { get; }

        private bool _popupOpen;
        public bool PopupOpen
        {
            get => _popupOpen;
            set => this.RaiseAndSetIfChanged(ref _popupOpen, value);
        }

        public ReactiveCommand<MarkingType?, Unit> ChangeMarkingCommand { get; }
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

            ChangeMarkingCommand = ReactiveCommand.Create<MarkingType?>(ChangeMarking);
            RemoveNoteCommand = ReactiveCommand.Create(RemoveNote);
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
        private void ChangeMarking(MarkingType? marking)
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
