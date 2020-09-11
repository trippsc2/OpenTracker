using OpenTracker.Interfaces;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Sections;
using OpenTracker.ViewModels.Markings;
using OpenTracker.ViewModels.Markings.Images;
using ReactiveUI;
using System;
using System.ComponentModel;

namespace OpenTracker.ViewModels.PinnedLocations.SectionIcons
{
    /// <summary>
    /// This is the ViewModel of the section icon control representing a section marking.
    /// </summary>
    public class MarkingSectionIconVM : SectionIconVMBase, IClickHandler
    {
        private readonly IMarking _marking;

        private MarkingImageVMBase _image;
        public MarkingImageVMBase Image
        {
            get => _image;
            private set => this.RaiseAndSetIfChanged(ref _image, value);
        }

        public MarkingSelectVM MarkingSelect { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="section">
        /// The marking to be represented.
        /// </param>
        public MarkingSectionIconVM(IMarkableSection section)
        {
            if (section == null)
            {
                throw new ArgumentNullException(nameof(section));
            }

            _marking = section.Marking;
            MarkingSelect = MarkingSelectVMFactory.GetMarkingSelectVM(section);
            UpdateImage();

            _marking.PropertyChanged += OnMarkingChanged;
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
            Image = MarkingImageDictionary.Instance[_marking.Mark];
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
