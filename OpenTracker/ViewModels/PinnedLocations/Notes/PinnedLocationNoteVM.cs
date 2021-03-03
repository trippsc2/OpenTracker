using OpenTracker.Models.Markings;
using OpenTracker.Utils;
using OpenTracker.ViewModels.Markings;
using OpenTracker.ViewModels.Markings.Images;
using ReactiveUI;
using System.ComponentModel;
using System.Reactive;
using Avalonia.Input;

namespace OpenTracker.ViewModels.PinnedLocations.Notes
{
    /// <summary>
    /// This class contains pinned location note control ViewModel data.
    /// </summary>
    public class PinnedLocationNoteVM : ViewModelBase, IPinnedLocationNoteVM
    {
        private readonly IMarkingImageDictionary _markingImages;

        public object Model =>
            Marking;

        public IMarking Marking { get; }
        public INoteMarkingSelectVM MarkingSelect { get; }

        private IMarkingImageVMBase? _image;
        public IMarkingImageVMBase Image
        {
            get => _image!;
            set => this.RaiseAndSetIfChanged(ref _image, value);
        }

        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="markingImages">
        /// The marking image control dictionary.
        /// </param>
        /// <param name="marking">
        /// The marking to be noted.
        /// </param>
        /// <param name="markingSelect">
        /// The note marking select control.
        /// </param>
        public PinnedLocationNoteVM(
            IMarkingImageDictionary markingImages, IMarking marking, INoteMarkingSelectVM markingSelect)
        {
            _markingImages = markingImages;

            Marking = marking;
            MarkingSelect = markingSelect;
            
            HandleClickCommand = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClick);

            Marking.PropertyChanged += OnMarkingChanged;

            UpdateImage();
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IMarking interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnMarkingChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IMarking.Mark))
            {
                UpdateImage();
            }
        }

        /// <summary>
        /// Updates the image.
        /// </summary>
        private void UpdateImage()
        {
            Image = _markingImages[Marking.Mark];
        }

        /// <summary>
        /// Opens the marking select popup.
        /// </summary>
        private void OpenMarkingSelect()
        {
            MarkingSelect.PopupOpen = true;
        }

        /// <summary>
        /// Handles clicking the control.
        /// </summary>
        /// <param name="e">
        /// The PointerReleased event args.
        /// </param>
        private void HandleClick(PointerReleasedEventArgs e)
        {
            if (e.InitialPressMouseButton == MouseButton.Left)
            {
                OpenMarkingSelect();
            }
        }
    }
}
