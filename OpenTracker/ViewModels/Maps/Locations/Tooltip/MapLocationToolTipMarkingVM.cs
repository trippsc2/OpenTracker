using Avalonia.Threading;
using OpenTracker.Models.Markings;
using OpenTracker.Utils;
using OpenTracker.ViewModels.Markings.Images;
using ReactiveUI;
using System.ComponentModel;
using System.Threading.Tasks;

namespace OpenTracker.ViewModels.Maps.Locations.Tooltip
{
    /// <summary>
    /// This is the ViewModel class for the map location tooltip markings.
    /// </summary>
    public class MapLocationToolTipMarkingVM : ViewModelBase, IMapLocationToolTipMarkingVM
    {
        private readonly IMarkingImageDictionary _markingImages;

        public object Model =>
            Marking;

        public IMarking Marking { get; }

        private IMarkingImageVMBase? _image;
        public IMarkingImageVMBase Image
        {
            get => _image!;
            private set => this.RaiseAndSetIfChanged(ref _image, value);
        }

        public delegate IMapLocationToolTipMarkingVM Factory(IMarking marking);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="markingImages">
        /// The marking image control dictionary.
        /// </param>
        /// <param name="marking">
        /// The marking to be represented.
        /// </param>
        public MapLocationToolTipMarkingVM(
            IMarkingImageDictionary markingImages, IMarking marking)
        {
            _markingImages = markingImages;

            Marking = marking;

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
        private async void OnMarkingChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IMarking.Mark))
            {
                await UpdateImageAsync();
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
        /// Updates the image asynchronously.
        /// </summary>
        private async Task UpdateImageAsync()
        {
            await Dispatcher.UIThread.InvokeAsync(UpdateImage);
        }
    }
}
