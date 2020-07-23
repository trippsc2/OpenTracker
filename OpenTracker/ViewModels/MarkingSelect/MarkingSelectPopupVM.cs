using OpenTracker.Models.Sections;
using OpenTracker.Models.UndoRedo;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive;

namespace OpenTracker.ViewModels.MarkingSelect
{
    /// <summary>
    /// This is the ViewModel class for the marking select popup control.
    /// </summary>
    public class MarkingSelectPopupVM : ViewModelBase
    {
        private readonly IMarkableSection _section;

        public ObservableCollection<MarkingSelectButtonVM> Buttons { get; }
        public double Width { get; }
        public double Height { get; }

        private bool _popupOpen;
        public bool PopupOpen
        {
            get => _popupOpen;
            set => this.RaiseAndSetIfChanged(ref _popupOpen, value);
        }

        public ReactiveCommand<MarkingType?, Unit> ChangeMarkingCommand { get; }
        public ReactiveCommand<Unit, Unit> ClearMarkingCommand { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="section">
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
        public MarkingSelectPopupVM(
            IMarkableSection section, ObservableCollection<MarkingSelectButtonVM> buttons,
            double width, double height)
        {
            _section = section ?? throw new ArgumentNullException(nameof(section));
            Buttons = buttons ?? throw new ArgumentNullException(nameof(buttons));
            Width = width;
            Height = height;

            ChangeMarkingCommand = ReactiveCommand.Create<MarkingType?>(ChangeMarking);
            ClearMarkingCommand = ReactiveCommand.Create(ClearMarking);
        }

        /// <summary>
        /// Clears the marking of the section.
        /// </summary>
        private void ClearMarking()
        {
            UndoRedoManager.Instance.Execute(new MarkSection(_section, null));
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

            UndoRedoManager.Instance.Execute(new MarkSection(_section, marking));
            PopupOpen = false;
        }
    }
}
