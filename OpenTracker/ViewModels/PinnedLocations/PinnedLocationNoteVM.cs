using OpenTracker.Interfaces;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;
using OpenTracker.Utils;
using OpenTracker.ViewModels.Markings;
using OpenTracker.ViewModels.Markings.Images;
using ReactiveUI;
using System;
using System.ComponentModel;

namespace OpenTracker.ViewModels.PinnedLocations
{
    /// <summary>
    /// This is the ViewModel of the pinned location note control.
    /// </summary>
    public class PinnedLocationNoteVM : ViewModelBase, IClickHandler
    {
        public IMarking Marking { get; }
        public NoteMarkingSelectVM MarkingSelect { get; }

        private MarkingImageVMBase _image;
        public MarkingImageVMBase Image
        {
            get => _image;
            set => this.RaiseAndSetIfChanged(ref _image, value);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="marking">
        /// The marking to be noted.
        /// </param>
        /// <param name="location">
        /// The location to which the marking belongs.
        /// </param>
        public PinnedLocationNoteVM(IMarking marking, ILocation location)
        {
            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            Marking = marking ?? throw new ArgumentNullException(nameof(marking));
            MarkingSelect = MarkingSelectVMFactory.GetNoteMarkingSelectVM(marking, location);
            UpdateImage();

            Marking.PropertyChanged += OnMarkingChanged;
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
            Image = MarkingImageDictionary.Instance[Marking.Mark];
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
