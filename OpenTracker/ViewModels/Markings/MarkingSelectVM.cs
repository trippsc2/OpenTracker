using OpenTracker.Models.Markings;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive;

namespace OpenTracker.ViewModels.Markings
{
    /// <summary>
    /// This is the ViewModel class for the marking select popup control.
    /// </summary>
    public class MarkingSelectVM : ViewModelBase
    {
        private readonly IMarking _marking;

        public static double Scale =>
            AppSettings.Instance.Layout.UIScale;
        public ObservableCollection<MarkingSelectItemVMBase> Buttons { get; }
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
            IMarking marking, ObservableCollection<MarkingSelectItemVMBase> buttons,
            double width, double height)
        {
            _marking = marking ?? throw new ArgumentNullException(nameof(marking));
            Buttons = buttons ?? throw new ArgumentNullException(nameof(buttons));
            Width = width;
            Height = height;
            ChangeMarkingCommand = ReactiveCommand.Create<MarkType?>(ChangeMarking);
            ClearMarkingCommand = ReactiveCommand.Create(ClearMarking);

            AppSettings.Instance.Layout.PropertyChanged += OnLayoutChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the LayoutSettings class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnLayoutChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LayoutSettings.UIScale))
            {
                this.RaisePropertyChanged(nameof(Scale));
            }
        }

        /// <summary>
        /// Clears the marking of the section.
        /// </summary>
        private void ClearMarking()
        {
            UndoRedoManager.Instance.Execute(new SetMarking(_marking, MarkType.Unknown));
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

            UndoRedoManager.Instance.Execute(new SetMarking(_marking, marking.Value));
            PopupOpen = false;
        }
    }
}
