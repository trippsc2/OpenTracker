using OpenTracker.Models.Markings;
using OpenTracker.Utils;
using OpenTracker.ViewModels.Markings;
using OpenTracker.ViewModels.Markings.Images;
using ReactiveUI;
using System.ComponentModel;

namespace OpenTracker.ViewModels.PinnedLocations.Notes
{
    /// <summary>
    /// This is the ViewModel of the pinned location note control.
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


        public delegate IPinnedLocationNoteVM Factory(
            IMarking marking, INoteMarkingSelectVM markingSelect);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="marking">
        /// The marking to be noted.
        /// </param>
        /// <param name="location">
        /// The location to which the marking belongs.
        /// </param>
        public PinnedLocationNoteVM(
            IMarkingImageDictionary markingImages, IMarking marking,
            INoteMarkingSelectVM markingSelect)
        {
            _markingImages = markingImages;

            Marking = marking;
            MarkingSelect = markingSelect;

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
        /// Handles left clicks and opens the marking select popup.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnLeftClick(bool force)
        {
            MarkingSelect.PopupOpen = true;
        }

        /// <summary>
        /// Handles right clicks.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnRightClick(bool force)
        {
        }
    }
}
