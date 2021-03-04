using Avalonia.Threading;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive;

namespace OpenTracker.ViewModels.Markings
{
    /// <summary>
    /// This is the ViewModel class for the marking select popup control.
    /// </summary>
    public class MarkingSelectVM : ViewModelBase, IMarkingSelectVM
    {
        private readonly ILayoutSettings _layoutSettings;
        private readonly IUndoRedoManager _undoRedoManager;
        private readonly IUndoableFactory _undoableFactory;
        private readonly IMarking _marking;

        public double Scale =>
            _layoutSettings.UIScale;
        public List<IMarkingSelectItemVMBase> Buttons { get; }
        public double Width { get; }
        public double Height { get; }

        private bool _popupOpen;
        public bool PopupOpen
        {
            get => _popupOpen;
            set => this.RaiseAndSetIfChanged(ref _popupOpen, value);
        }

        public ReactiveCommand<MarkType?, Unit> ChangeMarkingCommand { get; }
        public ReactiveCommand<Unit, Unit> ClearMarkingCommand { get; }

        public delegate IMarkingSelectVM Factory(
            IMarking marking, List<IMarkingSelectItemVMBase> buttons, double width, double height);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="marking">
        /// The marking to be represented.
        /// </param>
        /// <param name="buttons">
        /// The observable collection of marking select button ViewModel instances.
        /// </param>
        /// <param name="width">
        /// The width of the popup.
        /// </param>
        /// <param name="height">
        /// The height of the popup.
        /// </param>
        public MarkingSelectVM(
            ILayoutSettings layoutSettings, IUndoRedoManager undoRedoManager, IUndoableFactory undoableFactory,
            IMarking marking, List<IMarkingSelectItemVMBase> buttons, double width, double height)
        {
            _layoutSettings = layoutSettings;
            _undoRedoManager = undoRedoManager;
            _undoableFactory = undoableFactory;

            _marking = marking;

            Buttons = buttons;
            Width = width;
            Height = height;

            ChangeMarkingCommand = ReactiveCommand.Create<MarkType?>(ChangeMarking);
            ClearMarkingCommand = ReactiveCommand.Create(ClearMarking);

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
        private async void OnLayoutChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ILayoutSettings.UIScale))
            {
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Scale)));
            }
        }

        /// <summary>
        /// Clears the marking of the section.
        /// </summary>
        private void ClearMarking()
        {
            _undoRedoManager.NewAction(_undoableFactory.GetSetMarking(_marking, MarkType.Unknown));
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
            if (!marking.HasValue)
            {
                return;
            }

            _undoRedoManager.NewAction(_undoableFactory.GetSetMarking(_marking, marking.Value));
            PopupOpen = false;
        }
    }
}
