using OpenTracker.Models.Markings;
using OpenTracker.Utils;
using OpenTracker.ViewModels.Markings.Images;
using ReactiveUI;
using System;
using System.ComponentModel;

namespace OpenTracker.ViewModels.Maps.MapLocations
{
    /// <summary>
    /// This is the ViewModel class for the map location tooltip markings.
    /// </summary>
    public class MapLocationToolTipMarkingVM : ViewModelBase
    {
        public IMarking Marking { get; }

        private MarkingImageVMBase _image;
        public MarkingImageVMBase Image
        {
            get => _image;
            private set => this.RaiseAndSetIfChanged(ref _image, value);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="marking">
        /// The marking to be represented.
        /// </param>
        public MapLocationToolTipMarkingVM(IMarking marking)
        {
            Marking = marking ?? throw new ArgumentNullException(nameof(marking));
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
    }
}
