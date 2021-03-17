using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive;
using Avalonia.Threading;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;

namespace OpenTracker.ViewModels.Markings
{
    /// <summary>
    /// This class contains the note marking select popup control ViewModel data.
    /// </summary>
    public class NoteMarkingSelectVM : ViewModelBase, INoteMarkingSelectVM
    {
        private readonly ILayoutSettings _layoutSettings;
        private readonly IUndoRedoManager _undoRedoManager;
        private readonly IUndoableFactory _undoableFactory;

        private readonly IMarking _marking;
        private readonly ILocation _location;

        public double Scale => _layoutSettings.UIScale;

        public List<IMarkingSelectItemVMBase> Buttons { get; }

        private bool _popupOpen;
        public bool PopupOpen
        {
            get => _popupOpen;
            set => this.RaiseAndSetIfChanged(ref _popupOpen, value);
        }

        public ReactiveCommand<MarkType?, Unit> ChangeMarking { get; }
        public ReactiveCommand<Unit, Unit> RemoveNote { get; }

        public delegate INoteMarkingSelectVM Factory(
            IMarking marking, List<IMarkingSelectItemVMBase> buttons, ILocation location);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="layoutSettings">
        /// The layout settings data.
        /// </param>
        /// <param name="undoRedoManager">
        /// The undo/redo manager.
        /// </param>
        /// <param name="undoableFactory">
        /// A factory for creating undoable actions.
        /// </param>
        /// <param name="marking">
        /// The marking to be represented.
        /// </param>
        /// <param name="buttons">
        /// The observable collection of marking select buttons.
        /// </param>
        /// <param name="location">
        /// The location.
        /// </param>
        public NoteMarkingSelectVM(
            ILayoutSettings layoutSettings, IUndoRedoManager undoRedoManager, IUndoableFactory undoableFactory,
            IMarking marking, List<IMarkingSelectItemVMBase> buttons, ILocation location)
        {
            _layoutSettings = layoutSettings;
            _undoRedoManager = undoRedoManager;
            _undoableFactory = undoableFactory;

            _marking = marking;
            _location = location;

            Buttons = buttons;

            ChangeMarking = ReactiveCommand.Create<MarkType?>(ChangeMarkingImpl);
            RemoveNote = ReactiveCommand.Create(RemoveNoteImpl);

            _layoutSettings.PropertyChanged += OnLayoutChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ILayoutSettings interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnLayoutChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ILayoutSettings.UIScale))
            {
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Scale)));
            }
        }

        /// <summary>
        /// Remove the note.
        /// </summary>
        private void RemoveNoteImpl()
        {
            _undoRedoManager.NewAction(_undoableFactory.GetRemoveNote(_marking, _location));
            PopupOpen = false;
        }

        /// <summary>
        /// Changes the marking of the section to the specified marking.
        /// </summary>
        /// <param name="marking">
        /// The marking to be set.
        /// </param>
        private void ChangeMarkingImpl(MarkType? marking)
        {
            if (marking == null)
            {
                return;
            }

            _undoRedoManager.NewAction(_undoableFactory.GetSetMarking(_marking, marking.Value));
            PopupOpen = false;
        }
    }
}
